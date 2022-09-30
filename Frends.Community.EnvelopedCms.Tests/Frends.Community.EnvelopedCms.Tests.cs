using NUnit.Framework;
using System;

namespace Frends.Community.EnvelopedCms.Tests
{
    [TestFixture]
    internal class DecryptTests
    {
        private static string DecryptedFileBytesAsBase64String = @"PG5vdGU+DQo8dG8+VG92ZTwvdG8+DQo8ZnJvbT5KYW5pPC9mcm9tPg0KPGhlYWRpbmc+UmVtaW5kZXI8L2hlYWRpbmc+DQo8Ym9keT5Eb24ndCBmb3JnZXQgbWUgdGhpcyB3ZWVrZW5kITwvYm9keT4NCjwvbm90ZT4=";

        private static string PrivateKey = @"-----BEGIN PRIVATE KEY-----
MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDCwfhe+YjFWQTb
V3LqAKWtk6HSGJyySJ2fo//BOVe/fqwBtlkLTieFasQDXDjMnf5iqlsE+WRfs/BC
gdyx6PjGMbiA2a0kt2Mx/lAK6FMYES11mB12IcAqeI/UOURpDSqPfHxdmyr6uSWA
nT1nW2lf4IflATOhgUU5FUJiF5y/sIYgcV+ZOEgUXUw1LXFn74iESRSh4LRodfr4
JW/ooCbs4SCMis5GSZPfN2bwePc9PlTTg2OlvWZ6ojqcCs3lIlshTCXU9vBBKJV2
DajU5T6XB3UACJixJk91S9LOyJqYsSNfRRntcEwRpekh8l4uwZkOmJ1qGcI8o7dg
rfVorh0PAgMBAAECggEALi77K+J2mX3Llgv9txniL/AjXoPxnKF1p2anfnWPplxu
g5VvNiwZWR0BUF6tJXCN423MWbieIce0wMC0ygeZ/r3Jp9x2n49RWYiaCIwhMELa
idlBm0LTo/6k9MmDvHmfzxjREQ17uqrvBrnC1uCpc0/2ScroKHNUK/4rmMFKrXXP
dgjGspBYa+bIIjZV2wgGd4RpqTPcHYn82F+ILnbOzxDdbTUmbJUtSqc0AOzMMGrb
GyTAwxWqW1tMg+RXu1vcfdzS89F45HtzwHqa4QR5tIHDWpWayESogrj6mcnh2Vt6
y4zlqp2u8dkQF7pviJS4WjDbHihbyDI/CmHmTt3bwQKBgQD0JWqILRuO3OUuwFUF
5ySzeQp3aUn0I0n9IuLca7byH0ZPRCW+N0A2J/VFzfilH+NdBuz/VnV8HBEC23cE
mYmYUuqd/+jzRBvBx+Uo8hu03VrCH9Be217BpF7JzCZq2hBF7hS+QL38cKiHGvoC
Ugtve7d6yM4EOR/Ka63CryVEoQKBgQDMNq+4+bWnq1msVugzFBu8uLPc5NE9Z9hJ
w/2YrUZJxliB4kWbMLrC1/sr2zYX8k/S6sizWQJYOXoLAnhIiMQaS+WLchSdbpLC
6ZGfG4u8zpZIKTGZEKUptMQpVVFclDj8ju1LdQet19fgOheek2NW6F5axLAC/d1i
WLoDgHRTrwKBgQCqNL+NSi0xLuyT6EAeCmO6v0K9tuh4zHVThZO06V80cvs4XaEW
eDxKxRaoYIzcyVtfymlFJYhoSsTNQiU6TrPvPrcUCYnktnHHac1n+0m3kK4qMbW5
u6PWyfnPI4UQ7w9zQSSy8j3BFOVYs0Tig0I3qX9jmUSPCygPnXwZArOvoQKBgAGt
9XYE/7x4SU2EjXZWFQBRWJFZhGsH3pyEsyI9UpWqvvGahc4H8ZFGq3cp32EFCikG
602mTsGg/2L1yz11en9o3mNuNcMxtJ1LxHnVhSmxXUUMqa7Dl0gnh3F7lSa4xVs2
Zbc4KC8C1nCuqaERP0DoulXuxi/QHLEBX85FkS9bAoGBAIhxeDdz0xKHSvn2Z60C
J3AD8Ux1WVhxK853oh+dqILvkW1TfcVrV/1gjRHl2VSm2afDQ4UlhO//CQQrgBlv
XgJww2EjkrVHrpBLQUYvoPNo8v6Ux5nlzt9g4+gkT0feXFOEk3WdYgJHulbRYpEH
JDpgjqQA9bhUy4dG/9XFt6DS
-----END PRIVATE KEY-----";

        private static string EncryptedFileAsBase64String = @"MIICOgYJKoZIhvcNAQcDoIICKzCCAicCAQAxggF5MIIBdQIBADBdMEUxCzAJBgNVBAYTAkFVMRMwEQYDVQQIDApTb21lLVN0YXRlMSEwHwYDVQQKDBhJbnRlcm5ldCBXaWRnaXRzIFB0eSBMdGQCFHGP8M6Y11j+39AHtadITrcMqRV1MA0GCSqGSIb3DQEBAQUABIIBAIoF53QFpeeg+KZT4HDc+qh4sM2VS2FDyNbuYQCf707Zd/0XSzDD/qJ7qjuMKgX7rOlJ0XtOp8lHhRSIqK0TgR/Lg7oqr9m8KHik0xy86l9JAcDxNXJKUxPDPhGlbUqVf4oUEk9UD64AcOlC7GUIefh1OYw9rsQ7yZnv/rx5U6BW+THhWJ1d5bPoqPPyKTvxZzKBlzL9A3O3lgK9H6YsYWdT9hDfPH+1OZR/a76U/6p7ac5A+jrizyFICaHV/1Ga/OsF39kw9y81vEaqp11+ZR1sT6MBqk6oNZ2oDlZDvbSRSWo8JbSgC8NH9mk2hwZLckm7vMbX3d0nmsT7ghm+3AowgaQGCSqGSIb3DQEHATAUBggqhkiG9w0DBwQI76CXbSiLo42AgYA2sMm2dnJ6bePI+dQIBpr6ScLZNIT8vWDLXzoVFo/eYPwi/N1mu83Fbus/qC1DfTtkd/L2hhDmsVv4+4/BtTYlehmwR0jnvu5RB0642eIezZBuLrfi0gNH5vIvOmWadwL4zKWWBnn9R92xxnL/mrh6v4K9nICT+PzAad2pYC8Jeg==";

        [Test]
        public void Decryption_DecryptDEREncryptedFileFromContentBytes()
        {
            var input = new DecryptDEREncryptedFileInput
            {
                EncryptedContentBytes = Convert.FromBase64String(EncryptedFileAsBase64String),
                PrivateKey = PrivateKey
            };

            var ret = EnvelopedCms.DecryptDEREncryptedFile(input, new System.Threading.CancellationToken());

            Assert.AreEqual(ret.DecryptedFileContentBytes, Convert.FromBase64String(DecryptedFileBytesAsBase64String));
        }

        //TODO: Unit test for reading file from filesystem
    }
}