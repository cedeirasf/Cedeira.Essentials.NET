using System.Security;

namespace Cedeira.Essentials.NET.Extensions.System.Security.Cryptografy.Encryption
{
    public static class ByteExtension
    {
        public static SecureString BytesToSecureString(this byte[] encryptedBytes)
        {
            var secureEncryptedString = new SecureString();

            foreach (byte b in encryptedBytes)
                secureEncryptedString.AppendChar((char)b); 

            secureEncryptedString.MakeReadOnly();
            return secureEncryptedString;
        }
    }
}
