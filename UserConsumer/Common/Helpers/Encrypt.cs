using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
    public static class Encrypt
    {
        public static string TEncrypt(string plaintext)
        {
            byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] counter = new byte[16];
            counter[15] = 5;

            plaintext += "--fiap";

            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] encryptedBytes = new byte[plaintextBytes.Length];

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    int blockSize = 16;
                    int blocks = (plaintextBytes.Length + blockSize - 1) / blockSize;

                    for (int i = 0; i < blocks; i++)
                    {
                        byte[] keystreamBlock = new byte[blockSize];
                        encryptor.TransformBlock(counter, 0, blockSize, keystreamBlock, 0);

                        int offset = i * blockSize;
                        int remainingBytes = Math.Min(blockSize, plaintextBytes.Length - offset);

                        for (int j = 0; j < remainingBytes; j++)
                        {
                            encryptedBytes[offset + j] = (byte)(plaintextBytes[offset + j] ^ keystreamBlock[j]);
                        }

                        IncrementCounter(counter);
                    }
                }
            }

            return BitConverter.ToString(encryptedBytes).Replace("-", "").ToLower();
        }

        public static string TDecrypt(string encryptedHex)
        {
            byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] counter = new byte[16];
            counter[15] = 5;
            byte[] encryptedBytes = HexStringToByteArray(encryptedHex);
            byte[] decryptedBytes = new byte[encryptedBytes.Length];

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    int blockSize = 16;
                    int blocks = encryptedBytes.Length / blockSize;

                    for (int i = 0; i <= blocks; i++)
                    {
                        byte[] keystreamBlock = new byte[blockSize];
                        encryptor.TransformBlock(counter, 0, blockSize, keystreamBlock, 0);

                        int offset = i * blockSize;
                        int remainingBytes = Math.Min(blockSize, encryptedBytes.Length - offset);

                        for (int j = 0; j < remainingBytes; j++)
                        {
                            decryptedBytes[offset + j] = (byte)(encryptedBytes[offset + j] ^ keystreamBlock[j]);
                        }

                        IncrementCounter(counter);
                    }
                }
            }

            var strFinal = Encoding.UTF8.GetString(decryptedBytes);
            return strFinal.Replace("--fiap", "");
        }

        private static byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private static void IncrementCounter(byte[] counter)
        {
            for (int i = counter.Length - 1; i >= 0; i--)
            {
                if (++counter[i] != 0)
                    break;
            }
        }
    }
}
