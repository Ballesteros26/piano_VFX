using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003A3 RID: 931
	internal class X509Utils
	{
		// Token: 0x06001BD0 RID: 7120 RVA: 0x000020EB File Offset: 0x000002EB
		private X509Utils()
		{
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0006EDB6 File Offset: 0x0006CFB6
		internal static string FindOidInfo(uint keyType, string keyValue, OidGroup oidGroup)
		{
			if (keyValue == null)
			{
				throw new ArgumentNullException("keyValue");
			}
			if (keyValue.Length == 0)
			{
				return null;
			}
			if (keyType == 1U)
			{
				return CAPI.CryptFindOIDInfoNameFromKey(keyValue, oidGroup);
			}
			if (keyType != 2U)
			{
				throw new NotImplementedException(keyType.ToString());
			}
			return CAPI.CryptFindOIDInfoKeyFromName(keyValue, oidGroup);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0006EDF8 File Offset: 0x0006CFF8
		internal static string FindOidInfoWithFallback(uint key, string value, OidGroup group)
		{
			string text = X509Utils.FindOidInfo(key, value, group);
			if (text == null && group != OidGroup.All)
			{
				text = X509Utils.FindOidInfo(key, value, OidGroup.All);
			}
			return text;
		}
	}
}
