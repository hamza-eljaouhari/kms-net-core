using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

public class KeyArchive
{
    private readonly string _storagePath;
    private Dictionary<string, byte[]> _keyStore;

    public KeyArchive(string storagePath)
    {
        _storagePath = storagePath;
        _keyStore = new Dictionary<string, byte[]>();
        LoadKeysFromStorage();
    }

    public string StoreKey(byte[] key)
    {
        string keyId = Guid.NewGuid().ToString();
        _keyStore[keyId] = key;
        SaveKeyToStorage(keyId, key);
        return keyId;
    }

    public byte[] RetrieveKey(string keyId)
    {
        if (_keyStore.TryGetValue(keyId, out byte[] key))
        {
            return key;
        }

        throw new KeyNotFoundException("Key not found in archive.");
    }

    public void DeleteKey(string keyId)
    {
        if (_keyStore.ContainsKey(keyId))
        {
            _keyStore.Remove(keyId);
            DeleteKeyFromStorage(keyId);
        }
        else
        {
            throw new KeyNotFoundException("Key not found in archive.");
        }
    }

    private void LoadKeysFromStorage()
    {
        // Load keys from the storage location
        // This is a placeholder for actual storage logic
    }

    private void SaveKeyToStorage(string keyId, byte[] key)
    {
        // Save the key to the storage location
        // This is a placeholder for actual storage logic
        string filePath = Path.Combine(_storagePath, keyId);
        File.WriteAllBytes(filePath, key);
    }

    private void DeleteKeyFromStorage(string keyId)
    {
        // Delete the key from the storage location
        // This is a placeholder for actual storage logic
        string filePath = Path.Combine(_storagePath, keyId);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
