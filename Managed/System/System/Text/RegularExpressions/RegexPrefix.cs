using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000148 RID: 328
	internal sealed class RegexPrefix
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x00031590 File Offset: 0x0002F790
		internal RegexPrefix(string prefix, bool ci)
		{
			this._prefix = prefix;
			this._caseInsensitive = ci;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x000315A6 File Offset: 0x0002F7A6
		internal string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000315AE File Offset: 0x0002F7AE
		internal bool CaseInsensitive
		{
			get
			{
				return this._caseInsensitive;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x000315B6 File Offset: 0x0002F7B6
		internal static RegexPrefix Empty
		{
			get
			{
				return RegexPrefix._empty;
			}
		}

		// Token: 0x04000EB4 RID: 3764
		internal string _prefix;

		// Token: 0x04000EB5 RID: 3765
		internal bool _caseInsensitive;

		// Token: 0x04000EB6 RID: 3766
		internal static RegexPrefix _empty = new RegexPrefix(string.Empty, false);
	}
}
