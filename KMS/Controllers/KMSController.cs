using System;
using Microsoft.AspNetCore.Mvc;
using KMS.Contracts;
using KMS.Factory;

namespace KMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptographyController : ControllerBase
    {
        private readonly CryptographyProviderFactory _factory;

        public CryptographyController(CryptographyProviderFactory factory)
        {
            _factory = factory;
        }

        [HttpPost("create")]
        public IActionResult CreateKey(string algorithm, int keySize)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                var keyId = provider.CreateKey(algorithm, keySize);
                return Ok(keyId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("encrypt")]
        public IActionResult Encrypt(string algorithm, string keyId, [FromBody] byte[] data)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                var encryptedData = provider.Encrypt(data, keyId);
                return Ok(encryptedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("decrypt")]
        public IActionResult Decrypt(string algorithm, string keyId, [FromBody] byte[] data)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                var decryptedData = provider.Decrypt(data, keyId);
                return Ok(decryptedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("activate")]
        public IActionResult ActivateKey(string algorithm, string keyId)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                provider.ActivateKey(keyId);
                return Ok($"Key {keyId} activated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deactivate")]
        public IActionResult DeactivateKey(string algorithm, string keyId)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                provider.DeactivateKey(keyId);
                return Ok($"Key {keyId} deactivated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("destroy")]
        public IActionResult DestroyKey(string algorithm, string keyId)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                provider.DestroyKey(keyId);
                return Ok($"Key {keyId} destroyed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("revoke")]
        public IActionResult RevokeKey(string algorithm, string keyId)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                provider.RevokeKey(keyId);
                return Ok($"Key {keyId} revoked.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("archive")]
        public IActionResult ArchiveKey(string algorithm, string keyId)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                provider.ArchiveKey(keyId);
                return Ok($"Key {keyId} archived.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("recover")]
        public IActionResult RecoverKey(string algorithm, string keyId)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                provider.RecoverKey(keyId);
                return Ok($"Key {keyId} recovered.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("info")]
        public IActionResult GetKeyInfo(string algorithm, string keyId)
        {
            try
            {
                var provider = _factory.GetCryptographyProvider(algorithm);
                var keyInfo = provider.GetKeyInfo(keyId);
                return Ok(keyInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
