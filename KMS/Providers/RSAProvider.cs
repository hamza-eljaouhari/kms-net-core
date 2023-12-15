using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using KMS.Contracts;

namespace KMS.CryptographyProviders
{
    public class RSAProvider : ProviderBase
    {
        public override string CreateKey(string keyType, int keySize)
        {
            if (keyType != "RSA")
                throw new ArgumentException("Unsupported key type for RSAProvider.");

            using (var rsa = RSA.Create(keySize))
            {
                string keyId = Guid.NewGuid().ToString();
                RSAParameters parameters = rsa.ExportParameters(true);
                KeyStore[keyId] = SerializeRsaParameters(parameters);
                KeyStatus[keyId] = "Created";
                return keyId;
            }
        }

        public override byte[] Encrypt(byte[] data, string keyId)
        {
            if (!KeyStore.TryGetValue(keyId, out var keyParameters))
                throw new KeyNotFoundException($"Key with ID {keyId} does not exist.");

            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(DeserializeRsaParameters(keyParameters));
                return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
            }
        }

        public override byte[] Decrypt(byte[] data, string keyId)
        {
            if (!KeyStore.TryGetValue(keyId, out var keyParameters))
                throw new KeyNotFoundException($"Key with ID {keyId} does not exist.");

            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(DeserializeRsaParameters(keyParameters));
                return rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
            }
        }
        private byte[] SerializeRsaParameters(RSAParameters parameters)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    WriteIfNotNull(writer, parameters.Modulus);
                    WriteIfNotNull(writer, parameters.Exponent);
                    WriteIfNotNull(writer, parameters.P);
                    WriteIfNotNull(writer, parameters.Q);
                    WriteIfNotNull(writer, parameters.DP);
                    WriteIfNotNull(writer, parameters.DQ);
                    WriteIfNotNull(writer, parameters.InverseQ);
                    WriteIfNotNull(writer, parameters.D);
                }
                return ms.ToArray();
            }
        }

        private void WriteIfNotNull(BinaryWriter writer, byte[] data)
        {
            if (data != null)
            {
                writer.Write(data.Length);
                writer.Write(data);
            }
            else
            {
                writer.Write(0); // Indicate that the data is null or empty
            }
        }

        private RSAParameters DeserializeRsaParameters(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    RSAParameters parameters = new RSAParameters
                    {
                        Modulus = ReadNextArray(reader),
                        Exponent = ReadNextArray(reader),
                        P = ReadNextArray(reader),
                        Q = ReadNextArray(reader),
                        DP = ReadNextArray(reader),
                        DQ = ReadNextArray(reader),
                        InverseQ = ReadNextArray(reader),
                        D = ReadNextArray(reader)
                    };
                    return parameters;
                }
            }
        }

        private byte[] ReadNextArray(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            return length > 0 ? reader.ReadBytes(length) : null;
        }

    }
}