using System;

namespace System.Security.Cryptography
{
	/// <summary>Identifies Windows cryptographic object identifier (OID) groups.</summary>
	// Token: 0x0200038D RID: 909
	public enum OidGroup
	{
		/// <summary>All the groups.</summary>
		// Token: 0x040018DA RID: 6362
		All,
		/// <summary>The Windows group that is represented by CRYPT_HASH_ALG_OID_GROUP_ID.</summary>
		// Token: 0x040018DB RID: 6363
		HashAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_ENCRYPT_ALG_OID_GROUP_ID.</summary>
		// Token: 0x040018DC RID: 6364
		EncryptionAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_PUBKEY_ALG_OID_GROUP_ID.</summary>
		// Token: 0x040018DD RID: 6365
		PublicKeyAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_SIGN_ALG_OID_GROUP_ID.</summary>
		// Token: 0x040018DE RID: 6366
		SignatureAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_RDN_ATTR_OID_GROUP_ID.</summary>
		// Token: 0x040018DF RID: 6367
		Attribute,
		/// <summary>The Windows group that is represented by CRYPT_EXT_OR_ATTR_OID_GROUP_ID.</summary>
		// Token: 0x040018E0 RID: 6368
		ExtensionOrAttribute,
		/// <summary>The Windows group that is represented by CRYPT_ENHKEY_USAGE_OID_GROUP_ID.</summary>
		// Token: 0x040018E1 RID: 6369
		EnhancedKeyUsage,
		/// <summary>The Windows group that is represented by CRYPT_POLICY_OID_GROUP_ID.</summary>
		// Token: 0x040018E2 RID: 6370
		Policy,
		/// <summary>The Windows group that is represented by CRYPT_TEMPLATE_OID_GROUP_ID.</summary>
		// Token: 0x040018E3 RID: 6371
		Template,
		/// <summary>The Windows group that is represented by CRYPT_KDF_OID_GROUP_ID.</summary>
		// Token: 0x040018E4 RID: 6372
		KeyDerivationFunction
	}
}
