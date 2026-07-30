using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002E6 RID: 742
	internal abstract class Mapping
	{
		// Token: 0x06001BC0 RID: 7104 RVA: 0x000020FD File Offset: 0x000002FD
		internal Mapping()
		{
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00099E2B File Offset: 0x0009802B
		protected Mapping(Mapping mapping)
		{
			this.isSoap = mapping.isSoap;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x00099E3F File Offset: 0x0009803F
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x00099E47 File Offset: 0x00098047
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x04001609 RID: 5641
		private bool isSoap;
	}
}
