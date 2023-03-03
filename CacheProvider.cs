using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lesson_4
{
    public class CacheProvider
    {
        static byte[] additionalEntropy = { 1, 34, 3, 1, 6 };

        public void CacheConnections(List<ConnectionString> connections)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                MemoryStream memoryStream = new MemoryStream();
                XmlWriter xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xmlSerializer.Serialize(xmlWriter, connections);
                byte[] protectedData = Protect(memoryStream.ToArray());
                File.WriteAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}data.protected", protectedData);
            }
            catch (Exception)
            {
                Console.WriteLine("Serialize error.");
            }
        }

        public List<ConnectionString> GetConnectionFromCache() 
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                byte[] data =  File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}data.protected");
                byte[] unprotectedData = Unprotect(data);
                return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(unprotectedData));
            }
            catch (Exception)
            {
                Console.WriteLine("Serialize error.");
                return null;
            }
        }

        private byte[] Protect(byte[] data) 
        {
            try
            {
                return ProtectedData.Protect(data, additionalEntropy, DataProtectionScope.LocalMachine);
            }
            catch (CryptographicException)
            {
                Console.WriteLine($"Protect error.");
                return null;
            }
        }
        private byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, additionalEntropy, DataProtectionScope.LocalMachine);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine($"Unprotect error.\n{e.Message}.");
                return null;
            }
        }
    }
}
