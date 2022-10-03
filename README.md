# Frends.Community.EnvelopedCms

frends Community Task for EnvelopedCms

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

DecryptDEREncryptedFileInput consists of three properties EncryptedContentBytes, EncryptedFilePath and PrivateKey. 
If the EncryptedFilePath is given, the EncryptedContentBytes is ignored & when the EncryptedFilePath is left empty, the EncryptedContentBytes will be used.
PrivateKey is mandatory.

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| EncryptedContentBytes | `byte[]` | Content of the encrypted file as byte array. | `[104,84,105,115]` |
| EncryptedFilePath | `string` | Full path to the encrypted file location. | `C:\temp\encrypted_file.txt` |
| PrivateKey | `string` | Content of the private key file as a string. | this string should start with `-----BEGIN PRIVATE KEY-----` and ends in `-----END PRIVATE KEY-----` |


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
