namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Enum
{
    /// <summary>
    /// Defines the supported symmetric encryption algorithm types.
    /// </summary>
    /// <remarks>
    /// This enumeration lists the various types of symmetric algorithms that can be utilized
    /// for encryption and decryption processes. The available options include:
    /// <list type="bullet">
    ///     <item><description><see cref="AES"/>: Advanced Encryption Standard.</description></item>
    ///     <item><description><see cref="DES"/>: Data Encryption Standard.</description></item>
    ///     <item><description><see cref="TripleDES"/>: Triple Data Encryption Standard.</description></item>
    ///     <item><description><see cref="TripleDesGNC"/>: A variant of Triple Data Encryption Standard.</description></item>
    /// </list>
    /// </remarks>
    public enum SymmetricAlgorithmTypeEnum
    {
        AES,
        DES,
        TripleDES,
        TripleDesGNC
    }
}