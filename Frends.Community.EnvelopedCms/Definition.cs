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
        /// Byte array of file to decrypt
        /// </summary>
        [DisplayFormat(DataFormatString = "Expression")]
        public byte[] EncryptedContentBytes { get; set; }

        /// <summary>
        /// File path to encrypted file. Can be left empty if EncryptedContentBytes  is given.
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string EncryptedFilePath { get; set; }

        /// <summary>
        /// Private key for decryption. 
        /// Content of the private key file as a string that starts with -----BEGIN PRIVATE KEY----- and ends in -----END PRIVATE KEY-----.
        /// </summary>
        [PasswordPropertyText]
        public string PrivateKey { get; set; }
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