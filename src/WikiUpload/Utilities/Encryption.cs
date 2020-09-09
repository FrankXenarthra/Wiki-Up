﻿using System;
using System.Text;
using System.Security.Cryptography;
using WikiUpload.Utilities;

namespace WikiUpload
{
    public static partial class Encryption
    {
        // If you get a build error here you need to create Utilities\Entropy.cs containing something
        // like the following, with your own set of entropy data because the actual data is a secret.
        //
        //namespace WikiUpload
        //{
        //    public partial class Encryption
        //    {
        //        private static readonly byte[] entropy = { 12, 222, 41, 108, 99, 63, 11, 12, 244, 201, 63 };
        //    }
        //}

        public static SecureCharArray Decrypt(string text)
        {
            byte[] data = Convert.FromBase64String(text);
            byte[] unencrypted = ProtectedData.Unprotect(data, entropy, DataProtectionScope.CurrentUser);
            return new SecureCharArray(DecodeAndClear(unencrypted));
        }

        public static string Encrypt(string text)
        {
            byte[] data = Encode(text);
            byte[] encrypted = ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser);
            Array.Clear(data, 0, data.Length);
            return Convert.ToBase64String(encrypted);
        }

        private static byte[] Encode(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        private static char[] DecodeAndClear(byte[] data)
        {
            char[] decoded = Encoding.UTF8.GetChars(data);
            Array.Clear(data, 0, data.Length);
            return decoded;
        }
    }
}
