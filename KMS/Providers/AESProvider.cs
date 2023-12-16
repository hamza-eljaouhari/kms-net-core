﻿using System;
using System.IO;
using System.Security.Cryptography;
using KMS.Contracts;

namespace KMS.CryptographyProviders
{
    public class AESProvider : ProviderBase
    {
        public AESProvider(KeyStoreManager keyStoreManager)
            : base(keyStoreManager)
        {
        }

        public override string CreateKey(string keyType, int keySize)
        {
            if (keyType != "AES")
                throw new ArgumentException("Unsupported key type for AESProvider.");

            using (var aes = Aes.Create())
            {
                aes.KeySize = keySize;
                aes.GenerateKey();
                string keyId = Guid.NewGuid().ToString();
                keyStoreManager.AddKey(keyId, aes.Key, "AES");
                return keyId;
            }
        }

        public override byte[] Encrypt(byte[] data, string keyId)
        {
            var keyInfo = keyStoreManager.GetKeyInfo(keyId);
            using (var aes = Aes.Create())
            {
                aes.Key = keyInfo.KeyData;
                aes.GenerateIV();
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length); // Prepend the IV
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                    }
                    return ms.ToArray();
                }
            }
        }

        public override byte[] Decrypt(byte[] data, string keyId)
        {
            var keyInfo = keyStoreManager.GetKeyInfo(keyId);
            using (var aes = Aes.Create())
            {
                aes.Key = keyInfo.KeyData;
                byte[] iv = new byte[aes.BlockSize / 8];
                Array.Copy(data, iv, iv.Length);
                aes.IV = iv;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, iv.Length, data.Length - iv.Length);
                    }
                    return ms.ToArray();
                }
            }
        }
    }
}
