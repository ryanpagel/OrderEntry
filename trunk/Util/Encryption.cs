using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e = EncryptionClassLibrary.Encryption;

namespace QuickBooks.Util
{
    public class Encryption : QuickBooks.Util.IEncryption
    {
        ILogger _logger;

        e.Symmetric _symmetricEncryption;
        e.Data _key = new e.Data("ec8fcf0644624e08a1d6bbb855b34f37");
        public Encryption(ILogger logger)
        {
            _logger = logger;
            _symmetricEncryption = new e.Symmetric(e.Symmetric.Provider.Rijndael, true);
        }

        public string EncryptData(string sDecryptedData)
        {
            if (string.IsNullOrEmpty(sDecryptedData))
                return "";

            e.Data decryptedData = new e.Data(sDecryptedData);
            e.Data encryptedData = _symmetricEncryption.Encrypt(decryptedData, _key);
            return encryptedData.Base64;
        }

        public string DecryptData(string sEncryptedData)
        {
            if (string.IsNullOrEmpty(sEncryptedData))
                return "";

            e.Data encryptedData = new e.Data();
            encryptedData.Base64 = sEncryptedData;
            e.Data decryptedData = _symmetricEncryption.Decrypt(encryptedData, _key);
            return decryptedData.Text;
        }
        
    }
}
