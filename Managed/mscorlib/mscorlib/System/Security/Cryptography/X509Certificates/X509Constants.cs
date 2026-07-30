using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020006A9 RID: 1705
	internal static class X509Constants
	{
		// Token: 0x04002626 RID: 9766
		internal const uint CRYPT_EXPORTABLE = 1U;

		// Token: 0x04002627 RID: 9767
		internal const uint CRYPT_USER_PROTECTED = 2U;

		// Token: 0x04002628 RID: 9768
		internal const uint CRYPT_MACHINE_KEYSET = 32U;

		// Token: 0x04002629 RID: 9769
		internal const uint CRYPT_USER_KEYSET = 4096U;

		// Token: 0x0400262A RID: 9770
		internal const uint CERT_QUERY_CONTENT_CERT = 1U;

		// Token: 0x0400262B RID: 9771
		internal const uint CERT_QUERY_CONTENT_CTL = 2U;

		// Token: 0x0400262C RID: 9772
		internal const uint CERT_QUERY_CONTENT_CRL = 3U;

		// Token: 0x0400262D RID: 9773
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_STORE = 4U;

		// Token: 0x0400262E RID: 9774
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_CERT = 5U;

		// Token: 0x0400262F RID: 9775
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_CTL = 6U;

		// Token: 0x04002630 RID: 9776
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_CRL = 7U;

		// Token: 0x04002631 RID: 9777
		internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED = 8U;

		// Token: 0x04002632 RID: 9778
		internal const uint CERT_QUERY_CONTENT_PKCS7_UNSIGNED = 9U;

		// Token: 0x04002633 RID: 9779
		internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED_EMBED = 10U;

		// Token: 0x04002634 RID: 9780
		internal const uint CERT_QUERY_CONTENT_PKCS10 = 11U;

		// Token: 0x04002635 RID: 9781
		internal const uint CERT_QUERY_CONTENT_PFX = 12U;

		// Token: 0x04002636 RID: 9782
		internal const uint CERT_QUERY_CONTENT_CERT_PAIR = 13U;

		// Token: 0x04002637 RID: 9783
		internal const uint CERT_STORE_PROV_MEMORY = 2U;

		// Token: 0x04002638 RID: 9784
		internal const uint CERT_STORE_PROV_SYSTEM = 10U;

		// Token: 0x04002639 RID: 9785
		internal const uint CERT_STORE_NO_CRYPT_RELEASE_FLAG = 1U;

		// Token: 0x0400263A RID: 9786
		internal const uint CERT_STORE_SET_LOCALIZED_NAME_FLAG = 2U;

		// Token: 0x0400263B RID: 9787
		internal const uint CERT_STORE_DEFER_CLOSE_UNTIL_LAST_FREE_FLAG = 4U;

		// Token: 0x0400263C RID: 9788
		internal const uint CERT_STORE_DELETE_FLAG = 16U;

		// Token: 0x0400263D RID: 9789
		internal const uint CERT_STORE_SHARE_STORE_FLAG = 64U;

		// Token: 0x0400263E RID: 9790
		internal const uint CERT_STORE_SHARE_CONTEXT_FLAG = 128U;

		// Token: 0x0400263F RID: 9791
		internal const uint CERT_STORE_MANIFOLD_FLAG = 256U;

		// Token: 0x04002640 RID: 9792
		internal const uint CERT_STORE_ENUM_ARCHIVED_FLAG = 512U;

		// Token: 0x04002641 RID: 9793
		internal const uint CERT_STORE_UPDATE_KEYID_FLAG = 1024U;

		// Token: 0x04002642 RID: 9794
		internal const uint CERT_STORE_BACKUP_RESTORE_FLAG = 2048U;

		// Token: 0x04002643 RID: 9795
		internal const uint CERT_STORE_READONLY_FLAG = 32768U;

		// Token: 0x04002644 RID: 9796
		internal const uint CERT_STORE_OPEN_EXISTING_FLAG = 16384U;

		// Token: 0x04002645 RID: 9797
		internal const uint CERT_STORE_CREATE_NEW_FLAG = 8192U;

		// Token: 0x04002646 RID: 9798
		internal const uint CERT_STORE_MAXIMUM_ALLOWED_FLAG = 4096U;

		// Token: 0x04002647 RID: 9799
		internal const uint CERT_NAME_EMAIL_TYPE = 1U;

		// Token: 0x04002648 RID: 9800
		internal const uint CERT_NAME_RDN_TYPE = 2U;

		// Token: 0x04002649 RID: 9801
		internal const uint CERT_NAME_SIMPLE_DISPLAY_TYPE = 4U;

		// Token: 0x0400264A RID: 9802
		internal const uint CERT_NAME_FRIENDLY_DISPLAY_TYPE = 5U;

		// Token: 0x0400264B RID: 9803
		internal const uint CERT_NAME_DNS_TYPE = 6U;

		// Token: 0x0400264C RID: 9804
		internal const uint CERT_NAME_URL_TYPE = 7U;

		// Token: 0x0400264D RID: 9805
		internal const uint CERT_NAME_UPN_TYPE = 8U;
	}
}
