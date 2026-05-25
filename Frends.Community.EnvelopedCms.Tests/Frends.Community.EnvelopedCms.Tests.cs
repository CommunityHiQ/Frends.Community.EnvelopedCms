using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace Frends.Community.EnvelopedCms.Tests
{
    [TestFixture]
    public class DecryptTests
    {
        private static string DecryptedFileBytesAsBase64String = @"PG5vdGU+DQo8dG8+VG92ZTwvdG8+DQo8ZnJvbT5KYW5pPC9mcm9tPg0KPGhlYWRpbmc+UmVtaW5kZXI8L2hlYWRpbmc+DQo8Ym9keT5Eb24ndCBmb3JnZXQgbWUgdGhpcyB3ZWVrZW5kITwvYm9keT4NCjwvbm90ZT4=";
        private static string PrivateKeyAsBase64String = @"LS0tLS1CRUdJTiBQUklWQVRFIEtFWS0tLS0tDQpNSUlFdmdJQkFEQU5CZ2txaGtpRzl3MEJBUUVGQUFTQ0JLZ3dnZ1NrQWdFQUFvSUJBUURDd2ZoZStZakZXUVRiDQpWM0xxQUtXdGs2SFNHSnl5U0oyZm8vL0JPVmUvZnF3QnRsa0xUaWVGYXNRRFhEak1uZjVpcWxzRStXUmZzL0JDDQpnZHl4NlBqR01iaUEyYTBrdDJNeC9sQUs2Rk1ZRVMxMW1CMTJJY0FxZUkvVU9VUnBEU3FQZkh4ZG15cjZ1U1dBDQpuVDFuVzJsZjRJZmxBVE9oZ1VVNUZVSmlGNXkvc0lZZ2NWK1pPRWdVWFV3MUxYRm43NGlFU1JTaDRMUm9kZnI0DQpKVy9vb0NiczRTQ01pczVHU1pQZk4yYndlUGM5UGxUVGcyT2x2V1o2b2pxY0NzM2xJbHNoVENYVTl2QkJLSlYyDQpEYWpVNVQ2WEIzVUFDSml4Sms5MVM5TE95SnFZc1NOZlJSbnRjRXdScGVraDhsNHV3WmtPbUoxcUdjSThvN2RnDQpyZlZvcmgwUEFnTUJBQUVDZ2dFQUxpNzdLK0oybVgzTGxndjl0eG5pTC9BalhvUHhuS0YxcDJhbmZuV1BwbHh1DQpnNVZ2Tml3WldSMEJVRjZ0SlhDTjQyM01XYmllSWNlMHdNQzB5Z2VaL3IzSnA5eDJuNDlSV1lpYUNJd2hNRUxhDQppZGxCbTBMVG8vNms5TW1EdkhtZnp4alJFUTE3dXFydkJybkMxdUNwYzAvMlNjcm9LSE5VSy80cm1NRktyWFhQDQpkZ2pHc3BCWWErYklJalpWMndnR2Q0UnBxVFBjSFluODJGK0lMbmJPenhEZGJUVW1iSlV0U3FjMEFPek1NR3JiDQpHeVRBd3hXcVcxdE1nK1JYdTF2Y2ZkelM4OUY0NUh0endIcWE0UVI1dElIRFdwV2F5RVNvZ3JqNm1jbmgyVnQ2DQp5NHpscXAydThka1FGN3B2aUpTNFdqRGJIaWhieURJL0NtSG1UdDNid1FLQmdRRDBKV3FJTFJ1TzNPVXV3RlVGDQo1eVN6ZVFwM2FVbjBJMG45SXVMY2E3YnlIMFpQUkNXK04wQTJKL1ZGemZpbEgrTmRCdXovVm5WOEhCRUMyM2NFDQptWW1ZVXVxZC8ranpSQnZCeCtVbzhodTAzVnJDSDlCZTIxN0JwRjdKekNacTJoQkY3aFMrUUwzOGNLaUhHdm9DDQpVZ3R2ZTdkNnlNNEVPUi9LYTYzQ3J5VkVvUUtCZ1FETU5xKzQrYlducTFtc1Z1Z3pGQnU4dUxQYzVORTlaOWhKDQp3LzJZclVaSnhsaUI0a1diTUxyQzEvc3IyellYOGsvUzZzaXpXUUpZT1hvTEFuaElpTVFhUytXTGNoU2RicExDDQo2WkdmRzR1OHpwWklLVEdaRUtVcHRNUXBWVkZjbERqOGp1MUxkUWV0MTlmZ09oZWVrMk5XNkY1YXhMQUMvZDFpDQpXTG9EZ0hSVHJ3S0JnUUNxTkwrTlNpMHhMdXlUNkVBZUNtTzZ2MEs5dHVoNHpIVlRoWk8wNlY4MGN2czRYYUVXDQplRHhLeFJhb1lJemN5VnRmeW1sRkpZaG9Tc1ROUWlVNlRyUHZQcmNVQ1lua3RuSEhhYzFuKzBtM2tLNHFNYlc1DQp1NlBXeWZuUEk0VVE3dzl6UVNTeThqM0JGT1ZZczBUaWcwSTNxWDlqbVVTUEN5Z1BuWHdaQXJPdm9RS0JnQUd0DQo5WFlFLzd4NFNVMkVqWFpXRlFCUldKRlpoR3NIM3B5RXN5STlVcFdxdnZHYWhjNEg4WkZHcTNjcDMyRUZDaWtHDQo2MDJtVHNHZy8yTDF5ejExZW45bzNtTnVOY014dEoxTHhIblZoU214WFVVTXFhN0RsMGduaDNGN2xTYTR4VnMyDQpaYmM0S0M4QzFuQ3VxYUVSUDBEb3VsWHV4aS9RSExFQlg4NUZrUzliQW9HQkFJaHhlRGR6MHhLSFN2bjJaNjBDDQpKM0FEOFV4MVdWaHhLODUzb2grZHFJTHZrVzFUZmNWclYvMWdqUkhsMlZTbTJhZkRRNFVsaE8vL0NRUXJnQmx2DQpYZ0p3dzJFamtyVkhycEJMUVVZdm9QTm84djZVeDVubHp0OWc0K2drVDBmZVhGT0VrM1dkWWdKSHVsYlJZcEVIDQpKRHBnanFRQTliaFV5NGRHLzlYRnQ2RFMNCi0tLS0tRU5EIFBSSVZBVEUgS0VZLS0tLS0=";
        private static string EncryptedFileAsBase64String = @"MIICOgYJKoZIhvcNAQcDoIICKzCCAicCAQAxggF5MIIBdQIBADBdMEUxCzAJBgNVBAYTAkFVMRMwEQYDVQQIDApTb21lLVN0YXRlMSEwHwYDVQQKDBhJbnRlcm5ldCBXaWRnaXRzIFB0eSBMdGQCFHGP8M6Y11j+39AHtadITrcMqRV1MA0GCSqGSIb3DQEBAQUABIIBAIoF53QFpeeg+KZT4HDc+qh4sM2VS2FDyNbuYQCf707Zd/0XSzDD/qJ7qjuMKgX7rOlJ0XtOp8lHhRSIqK0TgR/Lg7oqr9m8KHik0xy86l9JAcDxNXJKUxPDPhGlbUqVf4oUEk9UD64AcOlC7GUIefh1OYw9rsQ7yZnv/rx5U6BW+THhWJ1d5bPoqPPyKTvxZzKBlzL9A3O3lgK9H6YsYWdT9hDfPH+1OZR/a76U/6p7ac5A+jrizyFICaHV/1Ga/OsF39kw9y81vEaqp11+ZR1sT6MBqk6oNZ2oDlZDvbSRSWo8JbSgC8NH9mk2hwZLckm7vMbX3d0nmsT7ghm+3AowgaQGCSqGSIb3DQEHATAUBggqhkiG9w0DBwQI76CXbSiLo42AgYA2sMm2dnJ6bePI+dQIBpr6ScLZNIT8vWDLXzoVFo/eYPwi/N1mu83Fbus/qC1DfTtkd/L2hhDmsVv4+4/BtTYlehmwR0jnvu5RB0642eIezZBuLrfi0gNH5vIvOmWadwL4zKWWBnn9R92xxnL/mrh6v4K9nICT+PzAad2pYC8Jeg==";


        /*--------------- TESTS THAT CONTAIN CORRECT INPUTS ---------------*/


        // Testcase: Valid EncryptedContentBytes & PrivateKeyAsBase64EncodedString
        [Test]
        public void Decryption_DecryptDEREncryptedFileFromContentBytes_PrivateKeyFromString()
        {
            var input = new DecryptDEREncryptedFileInput
            {
                EncryptedContentBytes = Convert.FromBase64String(EncryptedFileAsBase64String),
                PrivateKeyAsBase64EncodedString = PrivateKeyAsBase64String
            };

            var ret = EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());

            Assert.AreEqual(ret.DecryptedFileContentBytes, Convert.FromBase64String(DecryptedFileBytesAsBase64String));
        }

        // Testcase: Valid EncryptedContentBytes & PrivateKeyFilePath
        [Test]
        public void Decryption_DecryptDEREncryptedFileFromContentBytes_PrivateKeyFromFile()
        {
            var privateKeyFilePath = Path.Combine(Path.GetTempPath(), "privateKeyTestFile.pem");
            File.WriteAllBytes(privateKeyFilePath, Convert.FromBase64String(PrivateKeyAsBase64String));

            var input = new DecryptDEREncryptedFileInput
            {
                EncryptedContentBytes = Convert.FromBase64String(EncryptedFileAsBase64String),
                PrivateKeyFilePath = privateKeyFilePath
            };

            var ret = EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());

            File.Delete(privateKeyFilePath);
            Assert.AreEqual(ret.DecryptedFileContentBytes, Convert.FromBase64String(DecryptedFileBytesAsBase64String));
        }

        // Testcase: Valid EncryptedFilePath & PrivateKeyAsBase64EncodedString
        [Test]
        public void Decryption_DecryptDEREncryptedFileFromFilePath_PrivateKeyFromString()
        {
            var encryptedFilePath = Path.Combine(Path.GetTempPath(), "encryptedFileTest.encrypt");
            File.WriteAllBytes(encryptedFilePath, Convert.FromBase64String(EncryptedFileAsBase64String));

            var input = new DecryptDEREncryptedFileInput
            {
                EncryptedFilePath = encryptedFilePath,
                PrivateKeyAsBase64EncodedString = PrivateKeyAsBase64String
            };

            var result = EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());

            File.Delete(encryptedFilePath);
            Assert.AreEqual(result.DecryptedFileContentBytes, Convert.FromBase64String(DecryptedFileBytesAsBase64String));
        }

        // Testcase: Valid EncryptedFilePath & PrivateKeyFilePath
        [Test]
        public void Decryption_DecryptDEREncryptedFileFromFilePath_PrivateKeyFromFile()
        {
            var encryptedFilePath = Path.Combine(Path.GetTempPath(), "encryptedFileTest.encrypt");
            File.WriteAllBytes(encryptedFilePath, Convert.FromBase64String(EncryptedFileAsBase64String));

            var privateKeyFilePath = Path.Combine(Path.GetTempPath(), "privateKeyFileTest.pem");
            File.WriteAllBytes(privateKeyFilePath, Convert.FromBase64String(PrivateKeyAsBase64String));

            var input = new DecryptDEREncryptedFileInput
            {
                EncryptedFilePath = encryptedFilePath,
                PrivateKeyFilePath = privateKeyFilePath
            };

            var result = EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());

            File.Delete(encryptedFilePath);
            File.Delete(privateKeyFilePath);
            Assert.AreEqual(result.DecryptedFileContentBytes, Convert.FromBase64String(DecryptedFileBytesAsBase64String));
        }


        /*--------------- TESTS THAT CONTAIN INVALID ENCRYPTED CONTENT & VALID PRIVATE KEY ---------------*/


        // Testcase: Invalid EncryptedContentBytes & Valid PrivateKeyAsBase64EncodedString
        [Test]
        public void Decryption_DecryptDEREncryptedFileFromContentBytes_InvalidContentBytes()
        {
            var input = new DecryptDEREncryptedFileInput
            {
                EncryptedContentBytes = Encoding.UTF8.GetBytes("Invalid Content"),
                PrivateKeyAsBase64EncodedString = PrivateKeyAsBase64String
            };

            Assert.Throws<ArgumentException>(() =>
            {
                EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());
            });
        }

        // Testcase: No encrypted content given & Valid PrivateKeyAsBase64EncodedString
        [Test]
        public void Decryption_EmptyContent()
        {
            var input = new DecryptDEREncryptedFileInput
            {
                PrivateKeyAsBase64EncodedString = PrivateKeyAsBase64String
            };

            Assert.Throws<ArgumentException>(() =>
            {
                EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());
            });
        }

        // Testcase: Invalid EncryptedFilePath & Valid PrivateKeyAsBase64EncodedString
        [Test]
        public void Decryption_DecryptDEREncryptedFileFromFilePath_InvalidFilePath()
        {
            var invalidFilePath = Path.Combine(Path.GetTempPath(), "Th1sF1leIsN0tVal1d.txt");

            var input = new DecryptDEREncryptedFileInput
            {
                EncryptedFilePath = invalidFilePath,
                PrivateKeyAsBase64EncodedString = PrivateKeyAsBase64String
            };

            Assert.Throws<FileNotFoundException>(() =>
            {
                EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());
            });
        }

        /*--------------- TESTS THAT CONTAIN VALID ENCRYPTED CONTENT & INVALID PRIVATE KEY ---------------*/

        // Testcase: Valid EncryptedContentBytes & Invalid PrivateKeyAsBase64EncodedString
        [Test]
        public void Decryption_InvalidPrivateKey()
        {
            var input = new DecryptDEREncryptedFileInput
            {
                EncryptedContentBytes = Convert.FromBase64String(EncryptedFileAsBase64String),
                PrivateKeyAsBase64EncodedString = "This is not a valid private key"
            };

            Assert.Throws<FormatException>(() =>
            {
                EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());
            });  
        }

        // Testcase: Valid EncryptedContentBytes & No private key given
        [Test]
        public void Decryption_EmptyPrivateKey()
        {
            var input = new DecryptDEREncryptedFileInput
            {
                EncryptedContentBytes = Convert.FromBase64String(EncryptedFileAsBase64String)
            };

            Assert.Throws<ArgumentException>(() =>
            {
                EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());
            });
        }
    }
}