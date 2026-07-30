using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000051 RID: 81
	internal class KeyValueInternalCollection : NameValueCollection
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00008503 File Offset: 0x00006703
		public void SetReadOnly()
		{
			base.IsReadOnly = true;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000850C File Offset: 0x0000670C
		public override void Add(string name, string val)
		{
			this.Remove(name);
			base.Add(name, val);
		}
	}
}
