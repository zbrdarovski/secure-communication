using System;
using ExpressEncription;
using System.IO;
using System.Text;

namespace SecureCommunication
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var publicKeyPath = @"Alice\public.key";    //  Alice's public key location
            var privateKeyPath = @"Alice\private.key";  //  Alice's private key location
            ExpressEncription.RSAEncription.MakeKey(publicKeyPath, privateKeyPath); //  Alice's key pair generation

            Encription();
            Decription();
        }

        private static void Encription()
        {
            var sourceFileName = @"Alice\public.key";
            var destinationFileName = @"Bob\alicePublic.key";
            File.Copy(sourceFileName, destinationFileName); //  Alice sends a copy of her public key to Bob
            var publicKeyPath = destinationFileName;

            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < 12; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            var password = sb.ToString(); // Bob's newly created one time password
            var plaintext = password;
            var inputFile = @"Bob\test.zip";
            ExpressEncription.AESEncription.AES_Encrypt(inputFile, password); //    Bob encrypts the file using AES algorithm and sends it to Alice
            var ciphertext = ExpressEncription.RSAEncription.EncryptString(plaintext, publicKeyPath); //    Bob encrypts his password with Alice's public key
            sourceFileName = @"Bob\test.zip.aes";
            destinationFileName = @"Alice\test.zip.aes";
            File.Move(sourceFileName, destinationFileName); // Bob sends his encrypted file to Alice
            string path = @"C:Alice\ciphertext.txt";
            string contents = ciphertext;
            File.WriteAllText(path, contents); //   Bob sends his encrypted password to Alice
        }

        private static void Decription()
        {
            string path = @"Alice\ciphertext.txt";
            string privateKeyPath = @"Alice\private.key";
            var ciphertext = File.ReadAllText(path); // Alice receives Bob's encrypted password
            var password = ExpressEncription.RSAEncription.DecryptString(ciphertext, privateKeyPath); //    Alice decrypts Bob's password using her private key
            var inputFile = @"Alice\test.zip.aes";
            ExpressEncription.AESEncription.AES_Decrypt(inputFile, password); //    Alice decrypts Bob's file using his password
        }
    }
}