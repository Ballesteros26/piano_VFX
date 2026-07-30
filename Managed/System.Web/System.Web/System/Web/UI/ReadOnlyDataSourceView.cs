using System;

namespace System.Web.UI
{
	// Token: 0x0200021F RID: 543
	internal class ReadOnlyDataSourceView : HierarchicalDataSourceView
	{
		// Token: 0x0600164F RID: 5711 RVA: 0x0003BB64 File Offset: 0x00039D64
		public ReadOnlyDataSourceView(IHierarchicalEnumerable datasource)
		{
			this.datasource = datasource;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0003BB73 File Offset: 0x00039D73
		public override IHierarchicalEnumerable Select()
		{
			return this.datasource;
		}

		// Token: 0x04001563 RID: 5475
		private IHierarchicalEnumerable datasource;
	}
}
