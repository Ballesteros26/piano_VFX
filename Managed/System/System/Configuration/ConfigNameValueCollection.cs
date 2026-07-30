using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000167 RID: 359
	internal class ConfigNameValueCollection : NameValueCollection
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0003986C File Offset: 0x00037A6C
		public ConfigNameValueCollection()
		{
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00039874 File Offset: 0x00037A74
		public ConfigNameValueCollection(ConfigNameValueCollection col)
			: base(col.Count, col)
		{
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00039883 File Offset: 0x00037A83
		public ConfigNameValueCollection(IHashCodeProvider hashProvider, IComparer comparer)
			: base(hashProvider, comparer)
		{
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0003988D File Offset: 0x00037A8D
		public void ResetModified()
		{
			this.modified = false;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00039896 File Offset: 0x00037A96
		public bool IsModified
		{
			get
			{
				return this.modified;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0003989E File Offset: 0x00037A9E
		public override void Set(string name, string value)
		{
			base.Set(name, value);
			this.modified = true;
		}

		// Token: 0x04000F7B RID: 3963
		private bool modified;
	}
}
