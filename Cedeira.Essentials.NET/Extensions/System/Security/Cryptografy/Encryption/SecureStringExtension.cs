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
                int length = input.Length * 2; 
                byte[] plainBytes = new byte[length];

                Marshal.Copy(unmanagedString, plainBytes, 0, length);
                return plainBytes;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
