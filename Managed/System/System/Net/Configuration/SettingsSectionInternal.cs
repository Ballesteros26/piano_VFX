using System;
using System.Net.Security;
using System.Net.Sockets;

namespace System.Net.Configuration
{
	// Token: 0x02000690 RID: 1680
	internal sealed class SettingsSectionInternal
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x000C3A68 File Offset: 0x000C1C68
		internal static SettingsSectionInternal Section
		{
			get
			{
				return SettingsSectionInternal.instance;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x000C3A6F File Offset: 0x000C1C6F
		// (set) Token: 0x060034AB RID: 13483 RVA: 0x000C3A77 File Offset: 0x000C1C77
		internal bool UseNagleAlgorithm { get; set; }

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x000C3A80 File Offset: 0x000C1C80
		// (set) Token: 0x060034AD RID: 13485 RVA: 0x000C3A88 File Offset: 0x000C1C88
		internal bool Expect100Continue { get; set; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060034AE RID: 13486 RVA: 0x000C3A91 File Offset: 0x000C1C91
		// (set) Token: 0x060034AF RID: 13487 RVA: 0x000C3A99 File Offset: 0x000C1C99
		internal bool CheckCertificateName { get; private set; }

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x000C3AA2 File Offset: 0x000C1CA2
		// (set) Token: 0x060034B1 RID: 13489 RVA: 0x000C3AAA File Offset: 0x000C1CAA
		internal int DnsRefreshTimeout { get; set; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060034B2 RID: 13490 RVA: 0x000C3AB3 File Offset: 0x000C1CB3
		// (set) Token: 0x060034B3 RID: 13491 RVA: 0x000C3ABB File Offset: 0x000C1CBB
		internal bool EnableDnsRoundRobin { get; set; }

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x000C3AC4 File Offset: 0x000C1CC4
		// (set) Token: 0x060034B5 RID: 13493 RVA: 0x000C3ACC File Offset: 0x000C1CCC
		internal bool CheckCertificateRevocationList { get; set; }

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060034B6 RID: 13494 RVA: 0x000C3AD5 File Offset: 0x000C1CD5
		// (set) Token: 0x060034B7 RID: 13495 RVA: 0x000C3ADD File Offset: 0x000C1CDD
		internal EncryptionPolicy EncryptionPolicy { get; private set; }

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060034B8 RID: 13496 RVA: 0x000027E2 File Offset: 0x000009E2
		internal bool Ipv6Enabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002A46 RID: 10822
		private static readonly SettingsSectionInternal instance = new SettingsSectionInternal();

		// Token: 0x04002A47 RID: 10823
		internal UnicodeEncodingConformance WebUtilityUnicodeEncodingConformance;

		// Token: 0x04002A48 RID: 10824
		internal UnicodeDecodingConformance WebUtilityUnicodeDecodingConformance;

		// Token: 0x04002A49 RID: 10825
		internal readonly bool HttpListenerUnescapeRequestUrl = true;

		// Token: 0x04002A4A RID: 10826
		internal readonly IPProtectionLevel IPProtectionLevel = IPProtectionLevel.Unspecified;
	}
}
