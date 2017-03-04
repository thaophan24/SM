using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
namespace SM.Security.Crypto
{
    public class RSACryptophy
    {
        private int EncryptBlockSize;
        private int DecryptBlockSize;
        private int KeySize;
        private string Key;
        public RSACryptophy(string key) : this(key, 2048)
        { }
        public RSACryptophy(string key, int keySize)
        {
            this.KeySize = keySize;
            this.Key = key;
            EncryptBlockSize = (this.KeySize / 8) - 42;
            DecryptBlockSize = EncryptBlockSize + 42;
        }
        public string Encrypt(string plainText)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySize);
            RSA.FromXmlString(Key);
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
            List<byte> encryptedBytes = new List<byte>();
            int block = plainBytes.Length / EncryptBlockSize + ((plainBytes.Length % EncryptBlockSize) > 0 ? 1 : 0);
            for (int i = 0; i < block; i++)
            {
                int copyLength = (plainBytes.Length - (i * EncryptBlockSize)) / EncryptBlockSize;
                copyLength = copyLength > 0 ? EncryptBlockSize : (plainBytes.Length % EncryptBlockSize);
                byte[] plainBlock = new byte[EncryptBlockSize];
                Array.Copy(plainBytes, i * EncryptBlockSize, plainBlock, 0, copyLength);
                byte[] encBytes = RSA.Encrypt(plainBlock, true);
                encryptedBytes.AddRange(encBytes);
            }
            string res = Convert.ToBase64String(encryptedBytes.ToArray());
            res = res.TrimEnd('\0'); // remove padding in encrypted text
            return res;
        }
        public string Decrypt(string encryptedText)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySize);
            RSA.FromXmlString(Key);
            byte[] encryptedBytes = Encoding.Unicode.GetBytes(encryptedText);
            List<byte> plainBytes = new List<byte>();
            int block = encryptedBytes.Length / DecryptBlockSize + ((encryptedBytes.Length % DecryptBlockSize) > 0 ? 1 : 0);
            for (int i = 0; i < block; i++)
            {
                int copyLength = (encryptedBytes.Length - (i * DecryptBlockSize)) / DecryptBlockSize;
                copyLength = copyLength > 0 ? DecryptBlockSize : (encryptedBytes.Length % DecryptBlockSize);
                byte[] encBlock = new byte[DecryptBlockSize];
                Array.Copy(encryptedBytes, i * DecryptBlockSize, encBlock, 0, copyLength);
                byte[] decBytes = RSA.Decrypt(encBlock, true);
                plainBytes.AddRange(decBytes);
            }
            string res = Convert.ToBase64String(plainBytes.ToArray());
            res = res.TrimEnd('\0'); // remove padding in decrypted text
            return res;
        }
    }
}