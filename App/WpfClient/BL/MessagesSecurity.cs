using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.BL
{
    public class MessagesSecurity
    {
        const int KEY_SIZE = 1024;

        private RSACryptoServiceProvider _decryptor { get; set; }

        private RSACryptoServiceProvider _encryptor { get; set; }
        public MessagesSecurity()
        {
            _decryptor = new RSACryptoServiceProvider(KEY_SIZE);
        }

        public void SetEncryptor(string publicKey)
        {
            _encryptor = new RSACryptoServiceProvider(KEY_SIZE);

            _encryptor.FromXmlString(publicKey);
        }

        public string DecryptorPublicKey
        {
            get
            {
                return _decryptor.ToXmlString(false);
            }
        }


        public string Encrypt(string message)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = ByteConverter.GetBytes(message);

            return Convert.ToBase64String(_encryptor.Encrypt(dataToEncrypt, false));
        }

        public string Decrypt(string message)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] dataToDecrypt = Convert.FromBase64String(message);

            return ByteConverter.GetString(_decryptor.Decrypt(dataToDecrypt,false));
        }

    }
}
