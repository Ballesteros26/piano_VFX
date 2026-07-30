using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200033C RID: 828
	internal class ImportStructWorkItem
	{
		// Token: 0x06001FD5 RID: 8149 RVA: 0x000AEEAC File Offset: 0x000AD0AC
		internal ImportStructWorkItem(StructModel model, StructMapping mapping)
		{
			this.model = model;
			this.mapping = mapping;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x000AEEC2 File Offset: 0x000AD0C2
		internal StructModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x000AEECA File Offset: 0x000AD0CA
		internal StructMapping Mapping
		{
			get
			{
				return this.mapping;
			}
		}

		// Token: 0x0400175F RID: 5983
		private StructModel model;

		// Token: 0x04001760 RID: 5984
		private StructMapping mapping;
	}
}
