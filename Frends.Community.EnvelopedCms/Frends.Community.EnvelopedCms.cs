using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;

#pragma warning disable 1591

namespace Frends.Community.EnvelopedCms
{
    /// <summary>
    /// Main class for enveloped data operations.
    /// </summary>
    public class EnvelopedCms
    {
        /// <summary>
        /// Decrypts a DER encrypted file.
        /// Documentation: https://github.com/CommunityHiQ/Frends.Community.EnvelopedCms
        /// </summary>
        /// <param name="input">Input parameters needed for decryption</param>
        /// <param name="cancellationToken"></param>
        /// <returns>DecryptDEREncryptedFileResult object {byte[] DecryptedFileContentBytes}</returns>
        public static DecryptDEREncryptedFileResult DecryptDEREncryptedFile([PropertyTab] DecryptDEREncryptedFileInput input, CancellationToken cancellationToken)
        {
            byte[] encryptedData = GetEncryptedContent(input); // Retrieve the content of the encrypted file.
            string encodedPrivateKey = GetEncodedPrivateKey(input); // Retrieve the encoded private key content.

            string decodedPrivateKey = Encoding.UTF8.GetString(Convert.FromBase64String(encodedPrivateKey));
            byte[] decryptedData = null;

            try
            {
                AsymmetricKeyParameter key = null;

                // Initialize PemReader: PemReader parses the content by removing unnecessary headers/footers & decodes the pem data into binary format.
                var pem = new PemReader(new StringReader(decodedPrivateKey));
                
                var keyObject = pem.ReadObject();

                if (keyObject is AsymmetricCipherKeyPair pair) // AsymmetricCipherKeyPair holds private/public key pair.
                {
                    key = pair.Private;
                }
                else if (keyObject is AsymmetricKeyParameter) // AsymmetricKeyParameter holds asymmetric private or public key.
                {
                    key = (AsymmetricKeyParameter)keyObject;
                }

                var parser = new CmsEnvelopedDataParser(encryptedData); // Initialize CmsEnvelopedDataParser: It parses the CMS Enveloped data.
                var recipients = parser.GetRecipientInfos(); // Returns a store of the intended recipients for this message/data.

                foreach (RecipientInformation recipient in recipients.GetRecipients())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    decryptedData = recipient.GetContent(key); // Decrypt the data and retrieve it.
                    break;
                }
            }
            catch (Exception exception)
            {
                throw new ArgumentException($"Couldn't decrypt the content. Exception: {exception.Message}, Stack Trace: {exception.StackTrace}");
            }

            return new DecryptDEREncryptedFileResult()
            {
                DecryptedFileContentBytes = decryptedData
            };
        }

        /// <summary>
        /// Retrieves the encrypted content from the DecryptDEREncryptedFileInput object, throws an exception when there is no content to retrieve.
        /// </summary>
        /// <param name="input">DecryptDEREncryptedFileInput object containing the input data</param>
        /// <returns>byte[] containing the encrypted content</returns>
        private static byte[] GetEncryptedContent(DecryptDEREncryptedFileInput input)
        {
            byte[] encryptedContent;
            if (!string.IsNullOrEmpty(input.EncryptedFilePath))
            {
                encryptedContent = File.ReadAllBytes(input.EncryptedFilePath);

                if (encryptedContent == null || encryptedContent.Length == 0) throw new ArgumentException($"Error while reading content from a given filepath: {input.EncryptedFilePath}. No content found.");    
            }
            else
            {
                encryptedContent = input.EncryptedContentBytes;

                if (encryptedContent == null || encryptedContent.Length == 0) throw new ArgumentException("Given EncryptedContentBytes was empty.");
            }

            return encryptedContent;
        }

        /// <summary>
        /// Retrieves the encoded private key content from the DecryptDEREncryptedFileInput object, throws an exception when there is no content to retrieve.
        /// </summary>
        /// <param name="input">DecryptDEREncryptFileInput object containing the input data</param>
        /// <returns>string containing the encoded private key content</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string GetEncodedPrivateKey(DecryptDEREncryptedFileInput input)
        {
            string privateKey;

            if (!string.IsNullOrEmpty(input.PrivateKeyFilePath))
            {
                privateKey = File.ReadAllText(input.PrivateKeyFilePath);

                if (privateKey == null || privateKey.Length == 0) throw new ArgumentException($"Error while reading Private Key from filepath: {input.PrivateKeyFilePath}. No content found");
            }
            else
            {
                privateKey = input.PrivateKeyAsBase64EncodedString;

                if (privateKey == null || privateKey.Length == 0) throw new ArgumentException("Given PrivateKeyAsBase64EncodedString was empty");
            }

            return privateKey;
        }
    }
}