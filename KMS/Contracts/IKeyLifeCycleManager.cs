using System.Security.Cryptography;

namespace KMS.Contracts
{
    public interface IKeyLifeCycleManager
    {
        // Creates a new cryptographic key and returns its unique identifier
        string CreateKey(string keyType, int keySize);

        // Encrypts data using a specified key
        byte[] Encrypt(byte[] data, string keyId);

        // Decrypts data using a specified key
        byte[] Decrypt(byte[] data, string keyId);

        // Retrieves information about a cryptographic key
        byte[] GetKeyInfo(string keyId);

        // Activates a cryptographic key, making it available for use
        void ActivateKey(string keyId);

        // Deactivates a cryptographic key, making it unavailable for use
        void DeactivateKey(string keyId);

        // Marks a cryptographic key for destruction. It cannot be used after this operation.
        void DestroyKey(string keyId);

        // Revokes a cryptographic key, often used in case of compromise or other security incidents
        void RevokeKey(string keyId);

        // Archives a cryptographic key, effectively taking it out of operational use but still keeping it stored
        void ArchiveKey(string keyId);

        // Recovers an archived cryptographic key, bringing it back into operational use
        void RecoverKey(string keyId);

        // Logs and audits key usage
        void LogKeyUsage(string keyId, string operation);

        // Manages access controls for the keys
        void SetAccessControl(string keyId, string accessPolicy);
    }
}
