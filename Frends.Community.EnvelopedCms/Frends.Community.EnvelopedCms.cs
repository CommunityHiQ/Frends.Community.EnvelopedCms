using System;
using System.ComponentModel;
using System.IO;
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
            byte[] encryptedData = GetEncryptedContent(input);

            if (string.IsNullOrEmpty(input.PrivateKey)) throw new ArgumentException("PrivateKey must be provided.");

            var privateKey = input.PrivateKey;
            byte[] decryptedData = null;

            try
            {
                AsymmetricKeyParameter key = null;

                // Initialize PemReader: PemReader parses the content by removing unnecessary headers/footers & decodes the pem data into binary format.
                var pem = new PemReader(new StringReader(privateKey));
                
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
    }
}