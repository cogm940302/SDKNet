using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Globalization;

namespace SdkNet.AES
{
    public class AESHelper
    {
        string key;

        /// <summary>
        /// Genera instancia para el Cifrado y Decifrado
        /// </summary>
        /// <param name="key"></param>
        public AESHelper(string key) {
            this.key = key;
        }

        /// <summary>
        /// Metodo de cifrado para AES CBC
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        public string Encrypt(string text) {
            byte[] encrypted = null;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider()) {
                aes.KeySize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = ConvertHexStringToByte(key);

                byte[] randomIV = new Byte[16];
                RandomNumberGenerator rng = RandomNumberGenerator.Create(); ;
                rng.GetBytes(randomIV);
                aes.IV = randomIV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream()) {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(text);
                        }
                        encrypted = ms.ToArray();
                    }
                    encrypted = ConcatenateByteArrays(aes.IV, encrypted);
                }

            }
            string resultado = Convert.ToBase64String(encrypted);
            return resultado;
        }

        /// <summary>
        /// Metodo de decifrado para AES CBC
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        public string Decrypt(string text)
        {
            string plaintext = null;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                byte[] fullData = Convert.FromBase64String(text);
                byte[] cipherData = new byte[fullData.Length - 16];
                byte[] ivByte = new byte[16];
                Buffer.BlockCopy(fullData, 16, cipherData, 0, cipherData.Length);
                Buffer.BlockCopy(fullData, 0, ivByte, 0, 16);
                aes.KeySize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = ConvertHexStringToByte(key);
                aes.IV = ivByte;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] result = decryptor.TransformFinalBlock(cipherData, 0 , cipherData.Length);
                plaintext = Encoding.ASCII.GetString(result);
            }
            return plaintext;
        }

        public byte[] ConcatenateByteArrays(byte[] first, byte[] second) {
            byte[] result = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, result, 0, first.Length);
            Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
            return result;
        }

        public byte[] ConvertHexStringToByte(string hexString)
        {
            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }
    }
}
