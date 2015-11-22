﻿// Copyright (c) to owners found in https://github.com/AArnott/pinvoke/blob/master/COPYRIGHT.md. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

namespace PInvoke
{
    using System;
    using System.Runtime.InteropServices;

    /// <content>
    /// Methods and nested types that are not strictly P/Invokes but provide
    /// a slightly higher level of functionality to ease calling into native code.
    /// </content>
    public static partial class BCrypt
    {
        /// <summary>
        /// Loads and initializes a CNG provider.
        /// </summary>
        /// <param name="pszAlgId">
        /// A pointer to a null-terminated Unicode string that identifies the requested
        /// cryptographic algorithm. This can be one of the standard
        /// CNG Algorithm Identifiers defined in <see cref="AlgorithmIdentifiers"/>
        /// or the identifier for another registered algorithm.
        /// </param>
        /// <param name="pszImplementation">
        /// <para>
        /// A pointer to a null-terminated Unicode string that identifies the specific provider
        /// to load. This is the registered alias of the cryptographic primitive provider.
        /// This parameter is optional and can be NULL if it is not needed. If this parameter
        /// is NULL, the default provider for the specified algorithm will be loaded.
        /// </para>
        /// <para>
        /// Note If the <paramref name="pszImplementation"/> parameter value is NULL, CNG attempts to open each
        /// registered provider, in order of priority, for the algorithm specified by the
        /// <paramref name="pszAlgId"/> parameter and returns the handle of the first provider that is successfully
        /// opened.For the lifetime of the handle, any BCrypt*** cryptographic APIs will use the
        /// provider that was successfully opened.
        /// </para>
        /// </param>
        /// <param name="dwFlags">Options for the function.</param>
        /// <returns>
        /// A pointer to a BCRYPT_ALG_HANDLE variable that receives the CNG provider handle.
        /// When you have finished using this handle, release it by passing it to the
        /// BCryptCloseAlgorithmProvider function.
        /// </returns>
        public static SafeAlgorithmHandle BCryptOpenAlgorithmProvider(
            string pszAlgId,
            string pszImplementation = null,
            BCryptOpenAlgorithmProviderFlags dwFlags = BCryptOpenAlgorithmProviderFlags.None)
        {
            SafeAlgorithmHandle handle;
            BCryptOpenAlgorithmProvider(
                out handle,
                pszAlgId,
                pszImplementation,
                dwFlags).ThrowOnError();
            return handle;
        }

        /// <summary>
        /// Create a hash or Message Authentication Code (MAC) object.
        /// </summary>
        /// <param name="algorithm">
        /// The handle of an algorithm provider created by using the <see cref="BCryptOpenAlgorithmProvider(string, string, BCryptOpenAlgorithmProviderFlags)"/> function. The algorithm that was specified when the provider was created must support the hash interface.
        /// </param>
        /// <param name="hashObject">
        /// A pointer to a buffer that receives the hash or MAC object. The required size of this buffer can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/> function to get the <see cref="PropertyNames.ObjectLength"/> property. This will provide the size of the hash or MAC object for the specified algorithm.
        /// This memory can only be freed after the handle pointed to by the return value is destroyed.
        /// If the value of this parameter is NULL, the memory for the hash object is allocated and freed by this function.
        /// Windows 7:  This memory management functionality is available beginning with Windows 7.
        /// </param>
        /// <param name="secret">
        /// A pointer to a buffer that contains the key to use for the hash or MAC. This key only applies to hash algorithms opened by the BCryptOpenAlgorithmProvider function by using the <see cref="BCryptOpenAlgorithmProviderFlags.AlgorithmHandleHmac"/> flag. Otherwise, set this parameter to NULL.
        /// </param>
        /// <param name="flags">Flags that modify the behavior of the function.</param>
        /// <returns>
        /// A pointer to a <see cref="SafeHashHandle"/> value that receives a handle that represents the hash or MAC object. This handle is used in subsequent hashing or MAC functions, such as the <see cref="BCryptHashData"/> function. When you have finished using this handle, release it by passing it to the <see cref="BCryptDestroyHash"/> function.
        /// </returns>
        public static SafeHashHandle BCryptCreateHash(
            SafeAlgorithmHandle algorithm,
            byte[] hashObject = null,
            byte[] secret = null,
            BCryptCreateHashFlags flags = BCryptCreateHashFlags.None)
        {
            SafeHashHandle result;
            BCryptCreateHash(
                algorithm,
                out result,
                hashObject,
                hashObject?.Length ?? 0,
                secret,
                secret?.Length ?? 0,
                flags).ThrowOnError();
            return result;
        }

        public static byte[] BCryptExportKey(SafeKeyHandle key, SafeKeyHandle exportKey, string blobType)
        {
            int lengthRequired;
            exportKey = exportKey ?? SafeKeyHandle.NullHandle;
            BCryptExportKey(
                key,
                exportKey,
                blobType,
                null,
                0,
                out lengthRequired,
                0).ThrowOnError();
            byte[] keyBuffer = new byte[lengthRequired];
            BCryptExportKey(
                key,
                exportKey,
                AsymmetricKeyBlobTypes.EccPublic,
                keyBuffer,
                keyBuffer.Length,
                out lengthRequired,
                0).ThrowOnError();

            return keyBuffer;
        }

        /// <summary>
        /// Creates an empty public/private key pair.
        /// </summary>
        /// <param name="algorithm">The handle to the algorithm previously opened by <see cref="BCryptOpenAlgorithmProvider(string, string, BCryptOpenAlgorithmProviderFlags)"/></param>
        /// <param name="keyLength">The length of the key, in bits.</param>
        /// <returns>A handle to the generated key pair.</returns>
        /// <remarks>
        /// After you create a key by using this function, you can use the BCryptSetProperty
        /// function to set its properties; however, the key cannot be used until the
        /// BCryptFinalizeKeyPair function is called.
        /// </remarks>
        public static SafeKeyHandle BCryptGenerateKeyPair(
            SafeAlgorithmHandle algorithm,
            int keyLength)
        {
            SafeKeyHandle result;
            var error = BCryptGenerateKeyPair(algorithm, out result, keyLength, 0);
            error.ThrowOnError();
            return result;
        }

        /// <summary>
        /// Creates a key object for use with a symmetrical key encryption algorithm from a supplied key.
        /// </summary>
        /// <param name="algorithm">
        /// The handle of an algorithm provider created with the <see cref="BCryptOpenAlgorithmProvider(string, string, BCryptOpenAlgorithmProviderFlags)"/> function. The algorithm specified when the provider was created must support symmetric key encryption.
        /// </param>
        /// <param name="secret">
        /// A buffer that contains the key from which to create the key object. This is normally a hash of a password or some other reproducible data. If the data passed in exceeds the target key size, the data will be truncated and the excess will be ignored.
        /// Note: We strongly recommended that applications pass in the exact number of bytes required by the target key.
        /// </param>
        /// <param name="keyObject">
        /// A pointer to a buffer that receives the key object. The required size of this buffer can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/> function to get the BCRYPT_OBJECT_LENGTH property. This will provide the size of the key object for the specified algorithm.
        /// This memory can only be freed after the returned key handle is destroyed.
        /// If the value of this parameter is NULL, the memory for the key object is allocated and freed by this function.
        /// </param>
        /// <param name="flags">A set of flags that modify the behavior of this function. No flags are currently defined, so this parameter should be zero.</param>
        /// <returns>A handle to the generated key.</returns>
        public static SafeKeyHandle BCryptGenerateSymmetricKey(
            SafeAlgorithmHandle algorithm,
            byte[] secret,
            byte[] keyObject = null,
            BCryptGenerateSymmetricKeyFlags flags = BCryptGenerateSymmetricKeyFlags.None)
        {
            SafeKeyHandle hKey;
            BCryptGenerateSymmetricKey(
                algorithm,
                out hKey,
                keyObject,
                keyObject?.Length ?? 0,
                secret,
                secret.Length,
                flags).ThrowOnError();
            return hKey;
        }

        public static SafeKeyHandle BCryptImportKeyPair(
            SafeAlgorithmHandle algorithm,
            string blobType,
            byte[] input,
            BCryptImportKeyPairFlags flags)
        {
            SafeKeyHandle result;
            var error = BCryptImportKeyPair(
                algorithm,
                SafeKeyHandle.NullHandle,
                blobType,
                out result,
                input,
                input.Length,
                flags);
            error.ThrowOnError();
            return result;
        }

        /// <summary>
        /// Imports a symmetric key from a key BLOB. The BCryptImportKeyPair function is used to import a public/private key pair.
        /// </summary>
        /// <param name="hAlgorithm">
        /// The handle of the algorithm provider to import the key. This handle is obtained by calling the <see cref="BCryptOpenAlgorithmProvider(string, string, BCryptOpenAlgorithmProviderFlags)"/> function.
        /// </param>
        /// <param name="pszBlobType">
        /// An identifier that specifies the type of BLOB that is contained in the pbInput buffer.
        /// This can be one of the values defined in <see cref="SymmetricKeyBlobTypes"/>.
        /// </param>
        /// <param name="pbInput">
        /// The address of a buffer that contains the key BLOB to import.
        /// The <paramref name="pszBlobType"/> parameter specifies the type of key BLOB this buffer contains.
        /// </param>
        /// <param name="hImportKey">
        /// The handle of the key encryption key needed to unwrap the key BLOB in the pbInput parameter.
        /// Note The handle must be supplied by the same provider that supplied the key that is being imported.
        /// </param>
        /// <param name="pbKeyObject">
        /// A pointer to a buffer that receives the imported key object.
        /// The required size of this buffer can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/>
        /// function to get the BCRYPT_OBJECT_LENGTH property. This will provide the size of the
        /// key object for the specified algorithm.
        /// This memory can only be freed after the phKey key handle is destroyed.
        /// </param>
        /// <param name="dwFlags">A set of flags that modify the behavior of this function.</param>
        /// <returns>The imported key.</returns>
        /// <exception cref="Win32Exception">If an error occurs.</exception>
        public static SafeKeyHandle BCryptImportKey(
            SafeAlgorithmHandle hAlgorithm,
            [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType,
            byte[] pbInput,
            SafeKeyHandle hImportKey = null,
            byte[] pbKeyObject = null,
            BCryptImportKeyFlags dwFlags = BCryptImportKeyFlags.None)
        {
            SafeKeyHandle importedKey;
            BCryptImportKey(
                hAlgorithm,
                hImportKey ?? new SafeKeyHandle(),
                pszBlobType,
                out importedKey,
                pbKeyObject,
                pbKeyObject?.Length ?? 0,
                pbInput,
                pbInput.Length,
                dwFlags).ThrowOnError();
            return importedKey;
        }

        /// <summary>
        /// Encrypts a block of data.
        /// </summary>
        /// <param name="hKey">
        /// The handle of the key to use to encrypt the data. This handle is obtained from one of the key creation functions, such as <see cref="BCryptGenerateSymmetricKey(SafeAlgorithmHandle, byte[], byte[], BCryptGenerateSymmetricKeyFlags)"/>, <see cref="BCryptGenerateKeyPair(SafeAlgorithmHandle, int)"/>, or <see cref="BCryptImportKey(SafeAlgorithmHandle, string, byte[], SafeKeyHandle, byte[], BCryptImportKeyFlags)"/>.
        /// </param>
        /// <param name="pbInput">
        /// The address of a buffer that contains the plaintext to be encrypted. The cbInput parameter contains the size of the plaintext to encrypt.
        /// </param>
        /// <param name="pPaddingInfo">
        /// A pointer to a structure that contains padding information. This parameter is only used with asymmetric keys and authenticated encryption modes. If an authenticated encryption mode is used, this parameter must point to a BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO structure. If asymmetric keys are used, the type of structure this parameter points to is determined by the value of the dwFlags parameter. Otherwise, the parameter must be set to NULL.
        /// </param>
        /// <param name="pbIV">
        /// The address of a buffer that contains the initialization vector (IV) to use during encryption. The cbIV parameter contains the size of this buffer. This function will modify the contents of this buffer. If you need to reuse the IV later, make sure you make a copy of this buffer before calling this function.
        /// This parameter is optional and can be NULL if no IV is used.
        /// The required size of the IV can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/> function to get the BCRYPT_BLOCK_LENGTH property.This will provide the size of a block for the algorithm, which is also the size of the IV.
        /// </param>
        /// <param name="dwFlags">
        /// A set of flags that modify the behavior of this function. The allowed set of flags depends on the type of key specified by the hKey parameter.
        /// </param>
        /// <returns>The encrypted ciphertext.</returns>
        public static byte[] BCryptEncrypt(
            SafeKeyHandle hKey,
            byte[] pbInput,
            IntPtr pPaddingInfo,
            byte[] pbIV,
            BCryptEncryptFlags dwFlags)
        {
            int cipherTextLength;
            BCryptEncrypt(
                hKey,
                pbInput,
                pbInput.Length,
                pPaddingInfo,
                pbIV,
                pbIV?.Length ?? 0,
                null,
                0,
                out cipherTextLength,
                dwFlags).ThrowOnError();

            byte[] cipherText = new byte[cipherTextLength];
            BCryptEncrypt(
                hKey,
                pbInput,
                pbInput.Length,
                pPaddingInfo,
                pbIV,
                pbIV?.Length ?? 0,
                cipherText,
                cipherText.Length,
                out cipherTextLength,
                dwFlags).ThrowOnError();

            return cipherText;
        }

        /// <summary>
        /// Decrypts a block of data.
        /// </summary>
        /// <param name="hKey">
        /// The handle of the key to use to decrypt the data. This handle is obtained from one of the key creation functions, such as <see cref="BCryptGenerateSymmetricKey(SafeAlgorithmHandle, byte[], byte[], BCryptGenerateSymmetricKeyFlags)"/>, <see cref="BCryptGenerateKeyPair(SafeAlgorithmHandle, int)"/>, or <see cref="BCryptImportKey(SafeAlgorithmHandle, string, byte[], SafeKeyHandle, byte[], BCryptImportKeyFlags)"/>.
        /// </param>
        /// <param name="pbInput">
        /// The address of a buffer that contains the ciphertext to be decrypted. For more information, see Remarks.
        /// </param>
        /// <param name="pPaddingInfo">
        /// A pointer to a structure that contains padding information. This parameter is only used with asymmetric keys and authenticated encryption modes. If an authenticated encryption mode is used, this parameter must point to a BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO structure. If asymmetric keys are used, the type of structure this parameter points to is determined by the value of the <paramref name="dwFlags"/> parameter. Otherwise, the parameter must be set to NULL.
        /// </param>
        /// <param name="pbIV">
        /// The address of a buffer that contains the initialization vector (IV) to use during decryption. This function will modify the contents of this buffer. If you need to reuse the IV later, make sure you make a copy of this buffer before calling this function.
        /// This parameter is optional and can be NULL if no IV is used.
        /// The required size of the IV can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/> function to get the <see cref="PropertyNames.BlockLength"/> property. This will provide the size of a block for the algorithm, which is also the size of the IV.
        /// </param>
        /// <param name="dwFlags">
        /// A set of flags that modify the behavior of this function. The allowed set of flags depends on the type of key specified by the <paramref name="hKey"/> parameter.
        /// </param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        public static byte[] BCryptDecrypt(
            SafeKeyHandle hKey,
            byte[] pbInput,
            IntPtr pPaddingInfo,
            byte[] pbIV,
            BCryptEncryptFlags dwFlags)
        {
            int length;
            BCryptDecrypt(
                hKey,
                pbInput,
                pbInput.Length,
                pPaddingInfo,
                pbIV,
                pbIV?.Length ?? 0,
                null,
                0,
                out length,
                dwFlags).ThrowOnError();

            byte[] plainText = new byte[length];
            BCryptDecrypt(
                hKey,
                pbInput,
                pbInput.Length,
                pPaddingInfo,
                pbIV,
                pbIV?.Length ?? 0,
                plainText,
                plainText.Length,
                out length,
                dwFlags).ThrowOnError();

            return plainText;
        }

        /// <summary>
        /// Retrieves the hash or Message Authentication Code (MAC) value for the data accumulated from prior calls to <see cref="BCryptHashData(SafeHashHandle, byte[], int, BCryptHashDataFlags)"/>.
        /// </summary>
        /// <param name="hHash">
        /// The handle of the hash or MAC object to use to compute the hash or MAC. This handle is obtained by calling the <see cref="BCryptCreateHash(SafeAlgorithmHandle, byte[], byte[], BCryptCreateHashFlags)"/> function. After this function has been called, the hash handle passed to this function cannot be used again except in a call to <see cref="BCryptDestroyHash"/>.
        /// </param>
        /// <param name="flags">A set of flags that modify the behavior of this function.</param>
        /// <returns>The hash or MAC value.</returns>
        public static byte[] BCryptFinishHash(
            SafeHashHandle hHash,
            BCryptFinishHashFlags flags = BCryptFinishHashFlags.None)
        {
            int hashLength = BCryptGetProperty<int>(hHash, PropertyNames.HashLength);
            byte[] result = new byte[hashLength];
            BCryptFinishHash(hHash, result, result.Length, flags).ThrowOnError();
            return result;
        }

        /// <summary>
        /// Creates a signature of a hash value.
        /// </summary>
        /// <param name="key">The handle of the key to use to sign the hash.</param>
        /// <param name="hash">
        /// A pointer to a buffer that contains the hash value to sign.
        /// </param>
        /// <param name="paddingInfo">
        /// A pointer to a structure that contains padding information. The actual type of structure this parameter points to depends on the value of the <paramref name="flags"/> parameter. This parameter is only used with asymmetric keys and must be NULL otherwise.
        /// </param>
        /// <param name="flags">
        /// A set of flags that modify the behavior of this function. The allowed set of flags depends on the type of key specified by the <paramref name="key"/> parameter.
        /// </param>
        /// <returns>
        /// The signature produced by this function.
        /// </returns>
        /// <remarks>
        /// To later verify that the signature is valid, call the <see cref="BCryptVerifySignature"/> function with an identical key and an identical hash of the original data.
        /// </remarks>
        public static byte[] BCryptSignHash(
            SafeKeyHandle key,
            byte[] hash,
            IntPtr paddingInfo = default(IntPtr),
            BCryptSignHashFlags flags = BCryptSignHashFlags.None)
        {
            int outputLength;
            BCryptSignHash(
                key,
                paddingInfo,
                hash,
                hash.Length,
                null,
                0,
                out outputLength,
                flags).ThrowOnError();

            byte[] pbOutput = new byte[outputLength];
            BCryptSignHash(
                key,
                paddingInfo,
                hash,
                hash.Length,
                pbOutput,
                pbOutput.Length,
                out outputLength,
                flags).ThrowOnError();

            // The size should be as expected, but just in case:
            Array.Resize(ref pbOutput, outputLength);

            return pbOutput;
        }

        /// <summary>
        /// Creates a secret agreement value from a private and a public key.
        /// </summary>
        /// <param name="privateKey">
        /// The handle of the private key to use to create the secret agreement value.
        /// This key and the hPubKey key must come from the same CNG cryptographic algorithm provider.
        /// </param>
        /// <param name="publicKey">
        /// The handle of the public key to use to create the secret agreement value.
        /// This key and the hPrivKey key must come from the same CNG cryptographic algorithm provider.
        /// </param>
        /// <returns>
        /// A handle to the shared secret.
        /// </returns>
        public static SafeSecretHandle BCryptSecretAgreement(
            SafeKeyHandle privateKey,
            SafeKeyHandle publicKey)
        {
            SafeSecretHandle result;
            BCryptSecretAgreement(
                  privateKey,
                  publicKey,
                  out result).ThrowOnError();
            return result;
        }

        /// <summary>
        /// Sets the value of a named property for a CNG object.
        /// </summary>
        /// <param name="hObject">A handle that represents the CNG object to set the property value for.</param>
        /// <param name="propertyName">
        /// The name of the property to set. This can be one of the predefined <see cref="PropertyNames"/> or a custom property identifier.
        /// </param>
        /// <param name="propertyValue">The new property value.</param>
        public static void BCryptSetProperty(SafeHandle hObject, string propertyName, string propertyValue)
        {
            var error = BCryptSetProperty(
                hObject,
                propertyName,
                propertyValue,
                propertyValue != null ? (propertyValue.Length + 1) * sizeof(char) : 0,
                0);
            error.ThrowOnError();
        }

        /// <summary>
        /// Retrieves the value of a named property for a CNG object.
        /// </summary>
        /// <param name="hObject">A handle that represents the CNG object to obtain the property value for.</param>
        /// <param name="propertyName">A pointer to a null-terminated Unicode string that contains the name of the property to retrieve. This can be one of the predefined <see cref="PropertyNames"/> or a custom property identifier.</param>
        /// <param name="flags">A set of flags that modify the behavior of this function. No flags are defined for this function.</param>
        /// <returns>The property value.</returns>
        public static byte[] BCryptGetProperty(SafeHandle hObject, string propertyName, BCryptGetPropertyFlags flags = BCryptGetPropertyFlags.None)
        {
            int requiredSize;
            BCryptGetProperty(hObject, propertyName, null, 0, out requiredSize, flags).ThrowOnError();
            byte[] result = new byte[requiredSize];
            BCryptGetProperty(hObject, propertyName, result, result.Length, out requiredSize, flags).ThrowOnError();
            return result;
        }

        /// <summary>
        /// Retrieves the value of a named property for a CNG object.
        /// </summary>
        /// <typeparam name="T">The type of struct to return the property value as.</typeparam>
        /// <param name="hObject">A handle that represents the CNG object to obtain the property value for.</param>
        /// <param name="propertyName">A pointer to a null-terminated Unicode string that contains the name of the property to retrieve. This can be one of the predefined <see cref="PropertyNames"/> or a custom property identifier.</param>
        /// <param name="flags">A set of flags that modify the behavior of this function. No flags are defined for this function.</param>
        /// <returns>The property value.</returns>
        public static T BCryptGetProperty<T>(SafeHandle hObject, string propertyName, BCryptGetPropertyFlags flags = BCryptGetPropertyFlags.None)
            where T : struct
        {
            byte[] value = BCryptGetProperty(hObject, propertyName, flags);
            unsafe
            {
                fixed (byte* pValue = value)
                {
                    return (T)Marshal.PtrToStructure(new IntPtr(pValue), typeof(T));
                }
            }
        }
    }
}
