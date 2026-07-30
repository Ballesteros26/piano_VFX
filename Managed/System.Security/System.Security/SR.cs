using System;

// Token: 0x02000004 RID: 4
internal class SR
{
	// Token: 0x0400002A RID: 42
	public const string ArgumentOutOfRange_Index = "Index was out of range.  Must be non-negative and less than the size of the collection.";

	// Token: 0x0400002B RID: 43
	public const string Arg_EmptyOrNullString = "String cannot be empty or null.";

	// Token: 0x0400002C RID: 44
	public const string Cryptography_Partial_Chain = "A certificate chain could not be built to a trusted root authority.";

	// Token: 0x0400002D RID: 45
	public const string Cryptography_Xml_BadWrappedKeySize = "Bad wrapped key size.";

	// Token: 0x0400002E RID: 46
	public const string Cryptography_Xml_CipherValueElementRequired = "A Cipher Data element should have either a CipherValue or a CipherReference element.";

	// Token: 0x0400002F RID: 47
	public const string Cryptography_Xml_CreateHashAlgorithmFailed = "Could not create hash algorithm object.";

	// Token: 0x04000030 RID: 48
	public const string Cryptography_Xml_CreateTransformFailed = "Could not create the XML transformation identified by the URI {0}.";

	// Token: 0x04000031 RID: 49
	public const string Cryptography_Xml_CreatedKeyFailed = "Failed to create signing key.";

	// Token: 0x04000032 RID: 50
	public const string Cryptography_Xml_DigestMethodRequired = "A DigestMethod must be specified on a Reference prior to generating XML.";

	// Token: 0x04000033 RID: 51
	public const string Cryptography_Xml_DigestValueRequired = "A Reference must contain a DigestValue.";

	// Token: 0x04000034 RID: 52
	public const string Cryptography_Xml_EnvelopedSignatureRequiresContext = "An XmlDocument context is required for enveloped transforms.";

	// Token: 0x04000035 RID: 53
	public const string Cryptography_Xml_InvalidElement = "Malformed element {0}.";

	// Token: 0x04000036 RID: 54
	public const string Cryptography_Xml_InvalidEncryptionProperty = "Malformed encryption property element.";

	// Token: 0x04000037 RID: 55
	public const string Cryptography_Xml_InvalidKeySize = "The key size should be a non negative integer.";

	// Token: 0x04000038 RID: 56
	public const string Cryptography_Xml_InvalidReference = "Malformed reference element.";

	// Token: 0x04000039 RID: 57
	public const string Cryptography_Xml_InvalidSignatureLength = "The length of the signature with a MAC should be less than the hash output length.";

	// Token: 0x0400003A RID: 58
	public const string Cryptography_Xml_InvalidSignatureLength2 = "The length in bits of the signature with a MAC should be a multiple of 8.";

	// Token: 0x0400003B RID: 59
	public const string Cryptography_Xml_InvalidX509IssuerSerialNumber = "X509 issuer serial number is invalid.";

	// Token: 0x0400003C RID: 60
	public const string Cryptography_Xml_KeyInfoRequired = "A KeyInfo element is required to check the signature.";

	// Token: 0x0400003D RID: 61
	public const string Cryptography_Xml_KW_BadKeySize = "The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.";

	// Token: 0x0400003E RID: 62
	public const string Cryptography_Xml_LoadKeyFailed = "Signing key is not loaded.";

	// Token: 0x0400003F RID: 63
	public const string Cryptography_Xml_MissingAlgorithm = "Symmetric algorithm is not specified.";

	// Token: 0x04000040 RID: 64
	public const string Cryptography_Xml_MissingCipherData = "Cipher data is not specified.";

	// Token: 0x04000041 RID: 65
	public const string Cryptography_Xml_MissingDecryptionKey = "Unable to retrieve the decryption key.";

	// Token: 0x04000042 RID: 66
	public const string Cryptography_Xml_MissingEncryptionKey = "Unable to retrieve the encryption key.";

	// Token: 0x04000043 RID: 67
	public const string Cryptography_Xml_NotSupportedCryptographicTransform = "The specified cryptographic transform is not supported.";

	// Token: 0x04000044 RID: 68
	public const string Cryptography_Xml_ReferenceElementRequired = "At least one Reference element is required.";

	// Token: 0x04000045 RID: 69
	public const string Cryptography_Xml_ReferenceTypeRequired = "The Reference type must be set in an EncryptedReference object.";

	// Token: 0x04000046 RID: 70
	public const string Cryptography_Xml_SelfReferenceRequiresContext = "An XmlDocument context is required to resolve the Reference Uri {0}.";

	// Token: 0x04000047 RID: 71
	public const string Cryptography_Xml_SignatureDescriptionNotCreated = "SignatureDescription could not be created for the signature algorithm supplied.";

	// Token: 0x04000048 RID: 72
	public const string Cryptography_Xml_SignatureMethodKeyMismatch = "The key does not fit the SignatureMethod.";

	// Token: 0x04000049 RID: 73
	public const string Cryptography_Xml_SignatureMethodRequired = "A signature method is required.";

	// Token: 0x0400004A RID: 74
	public const string Cryptography_Xml_SignatureValueRequired = "Signature requires a SignatureValue.";

	// Token: 0x0400004B RID: 75
	public const string Cryptography_Xml_SignedInfoRequired = "Signature requires a SignedInfo.";

	// Token: 0x0400004C RID: 76
	public const string Cryptography_Xml_TransformIncorrectInputType = "The input type was invalid for this transform.";

	// Token: 0x0400004D RID: 77
	public const string Cryptography_Xml_IncorrectObjectType = "Type of input object is invalid.";

	// Token: 0x0400004E RID: 78
	public const string Cryptography_Xml_UnknownTransform = "Unknown transform has been encountered.";

	// Token: 0x0400004F RID: 79
	public const string Cryptography_Xml_UriNotResolved = "Unable to resolve Uri {0}.";

	// Token: 0x04000050 RID: 80
	public const string Cryptography_Xml_UriNotSupported = " The specified Uri is not supported.";

	// Token: 0x04000051 RID: 81
	public const string Cryptography_Xml_UriRequired = "A Uri attribute is required for a CipherReference element.";

	// Token: 0x04000052 RID: 82
	public const string Cryptography_Xml_XrmlMissingContext = "Null Context property encountered.";

	// Token: 0x04000053 RID: 83
	public const string Cryptography_Xml_XrmlMissingIRelDecryptor = "IRelDecryptor is required.";

	// Token: 0x04000054 RID: 84
	public const string Cryptography_Xml_XrmlMissingIssuer = "Issuer node is required.";

	// Token: 0x04000055 RID: 85
	public const string Cryptography_Xml_XrmlMissingLicence = "License node is required.";

	// Token: 0x04000056 RID: 86
	public const string Cryptography_Xml_XrmlUnableToDecryptGrant = "Unable to decrypt grant content.";

	// Token: 0x04000057 RID: 87
	public const string NotSupported_KeyAlgorithm = "The certificate key algorithm is not supported.";

	// Token: 0x04000058 RID: 88
	public const string Log_ActualHashValue = "Actual hash value: {0}";

	// Token: 0x04000059 RID: 89
	public const string Log_BeginCanonicalization = "Beginning canonicalization using \"{0}\" ({1}).";

	// Token: 0x0400005A RID: 90
	public const string Log_BeginSignatureComputation = "Beginning signature computation.";

	// Token: 0x0400005B RID: 91
	public const string Log_BeginSignatureVerification = "Beginning signature verification.";

	// Token: 0x0400005C RID: 92
	public const string Log_BuildX509Chain = "Building and verifying the X509 chain for certificate {0}.";

	// Token: 0x0400005D RID: 93
	public const string Log_CanonicalizationSettings = "Canonicalization transform is using resolver {0} and base URI \"{1}\".";

	// Token: 0x0400005E RID: 94
	public const string Log_CanonicalizedOutput = "Output of canonicalization transform: {0}";

	// Token: 0x0400005F RID: 95
	public const string Log_CertificateChain = "Certificate chain:";

	// Token: 0x04000060 RID: 96
	public const string Log_CheckSignatureFormat = "Checking signature format using format validator \"[{0}] {1}.{2}\".";

	// Token: 0x04000061 RID: 97
	public const string Log_CheckSignedInfo = "Checking signature on SignedInfo with id \"{0}\".";

	// Token: 0x04000062 RID: 98
	public const string Log_FormatValidationSuccessful = "Signature format validation was successful.";

	// Token: 0x04000063 RID: 99
	public const string Log_FormatValidationNotSuccessful = "Signature format validation failed.";

	// Token: 0x04000064 RID: 100
	public const string Log_KeyUsages = "Found key usages \"{0}\" in extension {1} on certificate {2}.";

	// Token: 0x04000065 RID: 101
	public const string Log_NoNamespacesPropagated = "No namespaces are being propagated.";

	// Token: 0x04000066 RID: 102
	public const string Log_PropagatingNamespace = "Propagating namespace {0}=\"{1}\".";

	// Token: 0x04000067 RID: 103
	public const string Log_RawSignatureValue = "Raw signature: {0}";

	// Token: 0x04000068 RID: 104
	public const string Log_ReferenceHash = "Reference {0} hashed with \"{1}\" ({2}) has hash value {3}, expected hash value {4}.";

	// Token: 0x04000069 RID: 105
	public const string Log_RevocationMode = "Revocation mode for chain building: {0}.";

	// Token: 0x0400006A RID: 106
	public const string Log_RevocationFlag = "Revocation flag for chain building: {0}.";

	// Token: 0x0400006B RID: 107
	public const string Log_SigningAsymmetric = "Calculating signature with key {0} using signature description {1}, hash algorithm {2}, and asymmetric signature formatter {3}.";

	// Token: 0x0400006C RID: 108
	public const string Log_SigningHmac = "Calculating signature using keyed hash algorithm {0}.";

	// Token: 0x0400006D RID: 109
	public const string Log_SigningReference = "Hashing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\" with hash algorithm \"{4}\" ({5}).";

	// Token: 0x0400006E RID: 110
	public const string Log_TransformedReferenceContents = "Transformed reference contents: {0}";

	// Token: 0x0400006F RID: 111
	public const string Log_UnsafeCanonicalizationMethod = "Canonicalization method \"{0}\" is not on the safe list. Safe canonicalization methods are: {1}.";

	// Token: 0x04000070 RID: 112
	public const string Log_UrlTimeout = "URL retrieval timeout for chain building: {0}.";

	// Token: 0x04000071 RID: 113
	public const string Log_VerificationFailed = "Verification failed checking {0}.";

	// Token: 0x04000072 RID: 114
	public const string Log_VerificationFailed_References = "references";

	// Token: 0x04000073 RID: 115
	public const string Log_VerificationFailed_SignedInfo = "SignedInfo";

	// Token: 0x04000074 RID: 116
	public const string Log_VerificationFailed_X509Chain = "X509 chain verification";

	// Token: 0x04000075 RID: 117
	public const string Log_VerificationFailed_X509KeyUsage = "X509 key usage verification";

	// Token: 0x04000076 RID: 118
	public const string Log_VerificationFlag = "Verification flags for chain building: {0}.";

	// Token: 0x04000077 RID: 119
	public const string Log_VerificationTime = "Verification time for chain building: {0}.";

	// Token: 0x04000078 RID: 120
	public const string Log_VerificationWithKeySuccessful = "Verification with key {0} was successful.";

	// Token: 0x04000079 RID: 121
	public const string Log_VerificationWithKeyNotSuccessful = "Verification with key {0} was not successful.";

	// Token: 0x0400007A RID: 122
	public const string Log_VerifyReference = "Processing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\".";

	// Token: 0x0400007B RID: 123
	public const string Log_VerifySignedInfoAsymmetric = "Verifying SignedInfo using key {0}, signature description {1}, hash algorithm {2}, and asymmetric signature deformatter {3}.";

	// Token: 0x0400007C RID: 124
	public const string Log_VerifySignedInfoHmac = "Verifying SignedInfo using keyed hash algorithm {0}.";

	// Token: 0x0400007D RID: 125
	public const string Log_X509ChainError = "Error building X509 chain: {0}: {1}.";

	// Token: 0x0400007E RID: 126
	public const string Log_XmlContext = "Using context: {0}";

	// Token: 0x0400007F RID: 127
	public const string Log_SignedXmlRecursionLimit = "Signed xml recursion limit hit while trying to decrypt the key. Reference {0} hashed with \"{1}\" and ({2}).";

	// Token: 0x04000080 RID: 128
	public const string Log_UnsafeTransformMethod = "Transform method \"{0}\" is not on the safe list. Safe transform methods are: {1}.";
}
