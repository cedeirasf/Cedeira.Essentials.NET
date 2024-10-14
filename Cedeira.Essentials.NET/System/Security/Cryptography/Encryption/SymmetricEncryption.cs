using Cedeira.Essentials.NET.Diagnostics.Invariants;
using Cedeira.Essentials.NET.Extensions.System.Security.Cryptografy.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption
{
    /// <summary>
    /// Provides methods for symmetric encryption and decryption of data using a specified symmetric algorithm.
    /// </summary>
    public class SymmetricEncryption : ISymmetricEncryption
    {
        /// <summary>
        /// Gets the symmetric algorithm used for encryption and decryption.
        /// </summary>
        private readonly SymmetricAlgorithm _symetricAlgortihm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricEncryption"/> class with the specified symmetric algorithm.
        /// </summary>
        /// <param name="symetricAlgortihm">The symmetric algorithm used for encryption and decryption.</param>
        public SymmetricEncryption(SymmetricAlgorithm symetricAlgortihm)
        {
            _symetricAlgortihm = symetricAlgortihm;
        }

        /// <summary>
        /// Encrypts the specified byte array.
        /// </summary>
        /// <param name="input">The byte array to encrypt.</param>
        /// <returns>The encrypted byte array.</returns>
        public byte[] Encrypt(byte[] input)
        {
            ValidateNull(input);

            return Encryption(input);
        }

        /// <summary>
        /// Decrypts the specified byte array.
        /// </summary>
        /// <param name="input">The byte array to decrypt.</param>
        /// <returns>The decrypted byte array.</returns>
        public byte[] Decrypt(byte[] input)
        {
            ValidateNull(input);

            return Decryption(input);
        }

        /// <summary>
        /// Encrypts the specified string.
        /// </summary>
        /// <param name="input">The string to encrypt.</param>
        /// <returns>The encrypted string represented in hexadecimal format.</returns>
        public string Encrypt(string input)
        {
            ValidateNull(input);

            byte[] plainBytes = Encoding.UTF8.GetBytes(input);

            return Convert.ToHexString(Encrypt(plainBytes));
        }

        /// <summary>
        /// Decrypts the specified string represented in hexadecimal format.
        /// </summary>
        /// <param name="input">The hexadecimal string to decrypt.</param>
        /// <returns>The decrypted string.</returns>
        public string Decrypt(string input)
        {
            ValidateNull(input);

            byte[] plainBytes = Convert.FromHexString(input);

            return Encoding.UTF8.GetString(Decryption(plainBytes));
        }
        /// <summary>
        /// Encrypts the specified SecureString.
        /// </summary>
        /// <param name="input">The SecureString to encrypt.</param>
        /// <returns>The encrypted SecureString.</returns>
        public SecureString Encrypt(SecureString input)
        {
            ValidateNull(input);

            var encryptor= _symetricAlgortihm.CreateEncryptor(_symetricAlgortihm.Key, _symetricAlgortihm.IV);

            return input.EncryptSecureString(encryptor);    

        }

        /// <summary>
        /// Decrypts the specified SecureString.
        /// </summary>
        /// <param name="input">The SecureString to decrypt.</param>
        /// <returns>The decrypted SecureString.</returns>
        public SecureString Decrypt(SecureString input)
        {
            ValidateNull(input);

            var decryptor = _symetricAlgortihm.CreateDecryptor(_symetricAlgortihm.Key, _symetricAlgortihm.IV);

            return input.DecryptSecureString(decryptor);    
        }

        /// <summary>
        /// Encrypts the data from the specified StreamReader.
        /// </summary>
        /// <param name="input">The StreamReader containing the data to encrypt.</param>
        /// <returns>A StreamReader containing the encrypted data.</returns>
        public StreamReader Encrypt(StreamReader input)
        {
            ValidateNull(input);

            using (var memoryStream = new MemoryStream())
            {
                input.BaseStream.CopyTo(memoryStream);

                var encryptedStream = new MemoryStream(Encrypt(memoryStream.ToArray()));
                return new StreamReader(encryptedStream);
            }
        }

        /// <summary>
        /// Decrypts the data from the specified StreamReader.
        /// </summary>
        /// <param name="input">The StreamReader containing the data to decrypt.</param>
        /// <returns>A StreamReader containing the decrypted data.</returns>
        public StreamReader Decrypt(StreamReader input)
        {
            ValidateNull(input);

            using (var memoryStream = new MemoryStream())
            {
                input.BaseStream.CopyTo(memoryStream);

                var decryptedStream = new MemoryStream(Decryption(memoryStream.ToArray()));
                return new StreamReader(decryptedStream);
            }
        }

        /// <summary>
        /// Encrypts the specified byte array using the symmetric algorithm.
        /// </summary>
        /// <param name="input">The byte array to encrypt.</param>
        /// <returns>The encrypted byte array.</returns>
        private byte[] Encryption(byte[] input)
        {
            var decryptor = _symetricAlgortihm.CreateDecryptor(_symetricAlgortihm.Key, _symetricAlgortihm.IV);

            using (var inputMemoryStream = new MemoryStream(input))
            using (var cryptoStream = new CryptoStream(inputMemoryStream, decryptor, CryptoStreamMode.Read))
            using (var outputMemoryStream = new MemoryStream())
            {
                cryptoStream.CopyTo(outputMemoryStream);
                return outputMemoryStream.ToArray();
            }
        }

        /// <summary>
        /// Decrypts the specified byte array using the symmetric algorithm.
        /// </summary>
        /// <param name="input">The byte array to decrypt.</param>
        /// <returns>The decrypted byte array.</returns>
        private byte[] Decryption(byte[] input)
        {
            var encryptor = _symetricAlgortihm.CreateEncryptor(_symetricAlgortihm.Key, _symetricAlgortihm.IV);

            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(input, 0, input.Length);
                cryptoStream.FlushFinalBlock();

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Validates that the specified input is not null.
        /// </summary>
        /// <typeparam name="T">The type of the input.</typeparam>
        /// <param name="input">The input to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when the input is null.</exception>
        private void ValidateNull<T>(T input)
        {
            Invariants.For(input).IsNotNull($"{nameof(input)} cannot be null.");
        }
    }
}

