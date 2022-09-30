using System.IO;
using System.Threading;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;

#pragma warning disable 1591

namespace Frends.Community.EnvelopedCms
{
    public static class EnvelopedCms
    {
        /// <summary>
        /// This is task
        /// Documentation: https://github.com/CommunityHiQ/Frends.Community.EnvelopedCms
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>{string Replication} </returns>
        public static DecryptDEREncryptedFileResult DecryptDEREncryptedFile(DecryptDEREncryptedFileInput input, CancellationToken cancellationToken)
        {
            byte[] fileContent;

            if (!string.IsNullOrEmpty(input.EncryptedFilePath))
            {
                fileContent = File.ReadAllBytes(input.EncryptedFilePath);
            }
            else
            {
                fileContent = input.EncryptedContentBytes;
            }

            var privateKey = input.PrivateKey;

            AsymmetricKeyParameter key = null;

            var pem = new PemReader(new StringReader(privateKey));

            var keyObject = pem.ReadObject();

            if (keyObject is AsymmetricCipherKeyPair pair)
            {
                key = pair.Private;
            }
            else if (keyObject is AsymmetricKeyParameter)
            {
                key = (AsymmetricKeyParameter)keyObject;
            }

            var encryptedData = fileContent;

            var parser = new CmsEnvelopedDataParser(encryptedData);
            var recipients = parser.GetRecipientInfos();
            byte[] decryptedData = null;

            foreach (RecipientInformation recipient in recipients.GetRecipients())
            {
                cancellationToken.ThrowIfCancellationRequested();

                decryptedData = recipient.GetContent(key);
                break;
            }

            return new DecryptDEREncryptedFileResult()
            {
                DecryptedFileContentBytes = decryptedData
            };
        }
    }
}