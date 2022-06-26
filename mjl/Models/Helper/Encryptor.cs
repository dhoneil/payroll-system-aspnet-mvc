using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace mjl.Models
{
    public class Encryptor
    {
        public static string Encrypt(string raw_text, string encryption_key)
        {
            byte[] raw_bytes = Encoding.Unicode.GetBytes(raw_text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pbd = new Rfc2898DeriveBytes(encryption_key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pbd.GetBytes(32);
                encryptor.IV = pbd.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(raw_bytes, 0, raw_bytes.Length);
                        cs.Close();
                    }
                    raw_text = Convert.ToBase64String(ms.ToArray());
                }
            }
            return raw_text;
        }

        public static string Decrypt(string encrypted_text, string encryption_key)
        {
            byte[] encrypted_bytes = Convert.FromBase64String(encrypted_text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryption_key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encrypted_bytes, 0, encrypted_bytes.Length);
                        cs.Close();
                    }
                    encrypted_text = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return encrypted_text;
        }
    }
}