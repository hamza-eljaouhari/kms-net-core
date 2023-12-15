using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using KMS.Contracts;

namespace KMS.CryptographyProviders
{
    public abstract class ProviderBase : IKeyLifeCycleManager
    {
        protected Dictionary<string, byte[]> KeyStore = new Dictionary<string, byte[]>();
        protected Dictionary<string, string> KeyStatus = new Dictionary<string, string>();
        private const string LogFilePath = "key_usage_log.txt"; // File path for logging

        public abstract string CreateKey(string keyType, int keySize);
        public abstract byte[] Encrypt(byte[] data, string keyId);
        public abstract byte[] Decrypt(byte[] data, string keyId);

        public void ActivateKey(string keyId)
        {
            ValidateKeyExists(keyId);
            KeyStatus[keyId] = "Active";
        }

        public void DeactivateKey(string keyId)
        {
            ValidateKeyExists(keyId);
            KeyStatus[keyId] = "Inactive";
        }

        public void DestroyKey(string keyId)
        {
            ValidateKeyExists(keyId);
            KeyStore.Remove(keyId);
            KeyStatus.Remove(keyId);
        }

        public void RevokeKey(string keyId)
        {
            ValidateKeyExists(keyId);
            KeyStatus[keyId] = "Revoked";
        }

        public void ArchiveKey(string keyId)
        {
            ValidateKeyExists(keyId);
            KeyStatus[keyId] = "Archived";
        }

        public void RecoverKey(string keyId)
        {
            ValidateKeyExists(keyId);
            if (KeyStatus[keyId] != "Archived")
            {
                throw new InvalidOperationException("Only archived keys can be recovered.");
            }
            KeyStatus[keyId] = "Active";
        }

        public byte[] GetKeyInfo(string keyId)
        {
            ValidateKeyExists(keyId);

            // Example: Returning key information (replace with your actual key information structure)
            if (KeyStatus.TryGetValue(keyId, out var keyInfo))
            {
                return Encoding.UTF8.GetBytes(keyInfo);
            }

            throw new InvalidOperationException("Unable to retrieve key information.");
        }

        public void LogKeyUsage(string keyId, string operation)
        {
            // Log the operation (Append to a file or a logging system)
            File.AppendAllText(LogFilePath, $"{DateTime.Now}: KeyID {keyId} used for {operation}\n");
        }

        public void SetAccessControl(string keyId, string accessPolicy)
        {
            ValidateKeyExists(keyId);

            // Example: Setting access control (this is a placeholder, replace with actual access control logic)
            KeyStatus[keyId] = accessPolicy;
        }

        protected void ValidateKeyExists(string keyId)
        {
            if (!KeyStore.ContainsKey(keyId))
            {
                throw new KeyNotFoundException($"Key with ID {keyId} does not exist.");
            }
        }

    }
}
