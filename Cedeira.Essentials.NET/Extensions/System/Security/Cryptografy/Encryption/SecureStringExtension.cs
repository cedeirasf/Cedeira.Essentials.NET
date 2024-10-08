using System.Runtime.InteropServices;
using System.Security;

namespace Cedeira.Essentials.NET.Extensions.System.Security.Cryptografy.Encryption
{
    public static class SecureStringExtension
    {

        public static byte[] SecureStringToBytes(this SecureString input)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(input);
                int length = input.Length * 2; // Cada char son 2 bytes
                byte[] plainBytes = new byte[length];

                Marshal.Copy(unmanagedString, plainBytes, 0, length);
                return plainBytes;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static SecureString BytesToSecureString(this byte[] encryptedBytes)
        {
            var secureEncryptedString = new SecureString();

            foreach (byte b in encryptedBytes)
                secureEncryptedString.AppendChar((char)b); // Convertir bytes a chars

            secureEncryptedString.MakeReadOnly();
            return secureEncryptedString;
        }
    }
}
