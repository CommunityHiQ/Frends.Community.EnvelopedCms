# Frends.Community.EnvelopedCms

Frends Community tasks for handling EnvelopedCms assymmetric encryption and decryption of files

[![Actions Status](https://github.com/CommunityHiQ/Frends.Community.EnvelopedCms/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/CommunityHiQ/Frends.Community.EnvelopedCms/actions) ![MyGet](https://img.shields.io/myget/frends-community/v/Frends.Community.EnvelopedCms) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
     - [EnvelopedCms](#EnvelopedCms)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the Task via frends UI Task View or you can find the NuGet package from the following NuGet feed
https://www.myget.org/F/frends-community/api/v3/index.json and in Gallery view in MyGet https://www.myget.org/feed/frends-community/package/nuget/Frends.Community.EnvelopedCms

# Tasks

## DecryptDEREncryptedFile

Decrypts a DER encrypted file

### Task Parameters

### DecryptDEREncryptedFileInput

DecryptDEREncryptedFileInput consists of four properties EncryptedContentBytes, EncryptedFilePath, PrivateKeyFilePath & PrivateKeyAsBase64EncodedString. 
The task needs either EncryptedContentBytes or EncryptedFilePath to be provided AND for the private key either PrivateKeyFilePath or PrivateKeyAsBase64EncodedString.
If all parameters are given then the EncryptedFilePath & PrivateKeyFilePath are chosen for the decryption.

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| EncryptedContentBytes | `byte[]` | Content of the encrypted file as byte array. | `[104,84,105,115]` |
| EncryptedFilePath | `string` | Full path to the encrypted files location. | `C:\temp\encrypted_file.txt` |
| PrivateKeyFilePath | `string` | Full path to the private key files location. Private key must be in .pem format. | `C:\temp\privateKey.pem` |
| PrivateKeyAsBase64EncodedString | `string` | Private Key files content as a Base64 Encoded String. Must be a valid Base64 string. | `LS0tLS1CRUdJTiBQUklWQVRFIEtFWS0tLS0tDQpNSUlFdmdJQkFEQU5CZ2txaGtpRzl3MEJBUUVGQUFTQ0JLZ3dnZ1NrQWdFQUFvSUJBUURDd2ZoZStZakZXUVRiDQpWM0xxQUtXdGs2SFNHSnl5U0oyZm8vL0JPVmUvZnF3QnRsa0xUaWVGYXNRRFhEak1uZjVpcWxzRStXUmZzL0JDDQpnZHl4NlBqR01iaUEyYTBrdDJNeC9sQUs2Rk1ZRVMxMW1CMTJJY0FxZUkvVU9VUnBEU3FQZkh4ZG15cjZ1U1dBDQpuVDFuVzJsZjRJZmxBVE9oZ1VVNUZVSmlGNXkvc0lZZ2NWK1pPRWdVWFV3MUxYRm43NGlFU1JTaDRMUm9kZnI0DQpKVy9vb0NiczRTQ01pczVHU1pQZk4yYndlUGM5UGxUVGcyT2x2V1o2b2pxY0NzM2xJbHNoVENYVTl2QkJLSlYyDQpEYWpVNVQ2WEIzVUFDSml4Sms5MVM5TE95SnFZc1NOZlJSbnRjRXdScGVraDhsNHV3WmtPbUoxcUdjSThvN2RnDQpyZlZvcmgwUEFnTUJBQUVDZ2dFQUxpNzdLK0oybVgzTGxndjl0eG5pTC9BalhvUHhuS0YxcDJhbmZuV1BwbHh1DQpnNVZ2Tml3WldSMEJVRjZ0SlhDTjQyM01XYmllSWNlMHdNQzB5Z2VaL3IzSnA5eDJuNDlSV1lpYUNJd2hNRUxhDQppZGxCbTBMVG8vNms5TW1EdkhtZnp4alJFUTE3dXFydkJybkMxdUNwYzAvMlNjcm9LSE5VSy80cm1NRktyWFhQDQpkZ2pHc3BCWWErYklJalpWMndnR2Q0UnBxVFBjSFluODJGK0lMbmJPenhEZGJUVW1iSlV0U3FjMEFPek1NR3JiDQpHeVRBd3hXcVcxdE1nK1JYdTF2Y2ZkelM4OUY0NUh0endIcWE0UVI1dElIRFdwV2F5RVNvZ3JqNm1jbmgyVnQ2DQp5NHpscXAydThka1FGN3B2aUpTNFdqRGJIaWhieURJL0NtSG1UdDNid1FLQmdRRDBKV3FJTFJ1TzNPVXV3RlVGDQo1eVN6ZVFwM2FVbjBJMG45SXVMY2E3YnlIMFpQUkNXK04wQTJKL1ZGemZpbEgrTmRCdXovVm5WOEhCRUMyM2NFDQptWW1ZVXVxZC8ranpSQnZCeCtVbzhodTAzVnJDSDlCZTIxN0JwRjdKekNacTJoQkY3aFMrUUwzOGNLaUhHdm9DDQpVZ3R2ZTdkNnlNNEVPUi9LYTYzQ3J5VkVvUUtCZ1FETU5xKzQrYlducTFtc1Z1Z3pGQnU4dUxQYzVORTlaOWhKDQp3LzJZclVaSnhsaUI0a1diTUxyQzEvc3IyellYOGsvUzZzaXpXUUpZT1hvTEFuaElpTVFhUytXTGNoU2RicExDDQo2WkdmRzR1OHpwWklLVEdaRUtVcHRNUXBWVkZjbERqOGp1MUxkUWV0MTlmZ09oZWVrMk5XNkY1YXhMQUMvZDFpDQpXTG9EZ0hSVHJ3S0JnUUNxTkwrTlNpMHhMdXlUNkVBZUNtTzZ2MEs5dHVoNHpIVlRoWk8wNlY4MGN2czRYYUVXDQplRHhLeFJhb1lJemN5VnRmeW1sRkpZaG9Tc1ROUWlVNlRyUHZQcmNVQ1lua3RuSEhhYzFuKzBtM2tLNHFNYlc1DQp1NlBXeWZuUEk0VVE3dzl6UVNTeThqM0JGT1ZZczBUaWcwSTNxWDlqbVVTUEN5Z1BuWHdaQXJPdm9RS0JnQUd0DQo5WFlFLzd4NFNVMkVqWFpXRlFCUldKRlpoR3NIM3B5RXN5STlVcFdxdnZHYWhjNEg4WkZHcTNjcDMyRUZDaWtHDQo2MDJtVHNHZy8yTDF5ejExZW45bzNtTnVOY014dEoxTHhIblZoU214WFVVTXFhN0RsMGduaDNGN2xTYTR4VnMyDQpaYmM0S0M4QzFuQ3VxYUVSUDBEb3VsWHV4aS9RSExFQlg4NUZrUzliQW9HQkFJaHhlRGR6MHhLSFN2bjJaNjBDDQpKM0FEOFV4MVdWaHhLODUzb2grZHFJTHZrVzFUZmNWclYvMWdqUkhsMlZTbTJhZkRRNFVsaE8vL0NRUXJnQmx2DQpYZ0p3dzJFamtyVkhycEJMUVVZdm9QTm84djZVeDVubHp0OWc0K2drVDBmZVhGT0VrM1dkWWdKSHVsYlJZcEVIDQpKRHBnanFRQTliaFV5NGRHLzlYRnQ2RFMNCi0tLS0tRU5EIFBSSVZBVEUgS0VZLS0tLS0=` |

### Returns

A DecryptDEREncryptedFileResult object with parameters.

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| DecryptedFileContentBytes | `byte[]` | Decrypted data as byte array. | `[84,104,105,115]` |

Usage:
To fetch result use syntax:

`#result.DecryptedFileContentBytes`

# Building

Clone a copy of the repository

`git clone https://github.com/CommunityHiQ/Frends.Community.EnvelopedCms.git`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repository on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ------- | ------- |
| 0.0.1   | Development still going on |
