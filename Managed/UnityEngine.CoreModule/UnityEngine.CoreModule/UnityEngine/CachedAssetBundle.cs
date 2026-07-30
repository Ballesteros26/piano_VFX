using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000A7 RID: 167
	[UsedByNativeCode]
	public struct CachedAssetBundle
	{
		// Token: 0x06000295 RID: 661 RVA: 0x0000528A File Offset: 0x0000348A
		public CachedAssetBundle(string name, Hash128 hash)
		{
			this.m_Name = name;
			this.m_Hash = hash;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000529C File Offset: 0x0000349C
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000052B4 File Offset: 0x000034B4
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000052C0 File Offset: 0x000034C0
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000052D8 File Offset: 0x000034D8
		public Hash128 hash
		{
			get
			{
				return this.m_Hash;
			}
			set
			{
				this.m_Hash = value;
			}
		}

		// Token: 0x040001F2 RID: 498
		private string m_Name;

		// Token: 0x040001F3 RID: 499
		private Hash128 m_Hash;
	}
}
