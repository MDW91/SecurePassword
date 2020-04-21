using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HashingOgSalting
{
    class Encryption
    {
        
        public static string ComputeSHA2Hash(string passwordtohash)
        {
            
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //User user = new User();
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(passwordtohash));
                

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                User.Hashedbytepassword = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(passwordtohash));
                return builder.ToString();
            }
        }

        public static byte[] GenerateSalt()
        {
            const int saltLength = 32;

            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);


            return ret;
        }

        public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Combine(toBeHashed, salt));
            }
        }

    }
}
