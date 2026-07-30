using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200002B RID: 43
	public struct CollationSequence
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000C0AC File Offset: 0x0000A2AC
		public int Compare(string s1, string s2)
		{
			return this._func._base.ContextCollateCompare(this.Encoding, this._func._context, s1, s2);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C0D1 File Offset: 0x0000A2D1
		public int Compare(char[] c1, char[] c2)
		{
			return this._func._base.ContextCollateCompare(this.Encoding, this._func._context, c1, c2);
		}

		// Token: 0x040000DC RID: 220
		public string Name;

		// Token: 0x040000DD RID: 221
		public CollationTypeEnum Type;

		// Token: 0x040000DE RID: 222
		public CollationEncodingEnum Encoding;

		// Token: 0x040000DF RID: 223
		internal SqliteFunction _func;
	}
}
