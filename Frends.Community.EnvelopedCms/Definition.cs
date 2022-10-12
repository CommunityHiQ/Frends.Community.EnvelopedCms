#pragma warning disable 1591

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Community.EnvelopedCms
{
    /// <summary>
    /// Input parameters for DecryptDEREncryptedFile task.
    /// </summary>
    public class DecryptDEREncryptedFileInput
    {
        /// <summary>
        /// Byte array of the file to decrypt
        /// </summary>
        [DisplayFormat(DataFormatString = "Expression")]
        public byte[] EncryptedContentBytes { get; set; }

        /// <summary>
        /// File path to the encrypted file. Can be left empty if EncryptedContentBytes  is given.
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string EncryptedFilePath { get; set; }

        /// <summary>
        /// File path to the private key file. Must be in .pem format.
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string PrivateKeyFilePath { get; set; }

        /// <summary>
        /// Private key files content as Base64 Encoded String. Can be left empty if PrivateKeyFilePath is given.
        /// </summary>
        [DisplayFormat(DataFormatString = "Expression")]
        public string PrivateKeyAsBase64EncodedString { get; set; }
    }

    /// <summary>
    /// The result object for method DecryptDEREncryptedFile
    /// </summary>
    public class DecryptDEREncryptedFileResult
    {
        /// <summary>
        /// Decrypted file as byte array
        /// </summary>
        public byte[] DecryptedFileContentBytes { get; set; }
    }
}