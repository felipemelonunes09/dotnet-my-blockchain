using System;
using System.Security.Cryptography;
using System.Text;

namespace my_blockchain.blockchain.core {
    static class Hash {

        public struct ValidatePack
        {
            public string hash;
            public char prefix;
            public int level;
        }

        public static string GenHash(object data) {
            
            byte[] bytes = Encoding.Unicode.GetBytes( data.ToString() );
            SHA256Managed hashstring = new SHA256Managed();

            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;

            foreach(byte x in hash) {
                hashString += String.Format("{0:x2}", x);
            }
 
            return hashString;
        }

        public static bool validateHash(ValidatePack pack) {
            string check = new string(pack.prefix, pack.level);
            return pack.hash.StartsWith(check);
        }
    }
    
}