using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000352 RID: 850
	internal class CollectionDataSourceView : DataSourceView
	{
		// Token: 0x06001F72 RID: 8050 RVA: 0x0004F932 File Offset: 0x0004DB32
		public CollectionDataSourceView(IDataSource owner, string viewName, IEnumerable collection)
			: base(owner, viewName)
		{
			this.collection = collection;
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0004F943 File Offset: 0x0004DB43
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			return this.collection;
		}

		// Token: 0x0400188A RID: 6282
		private IEnumerable collection;
	}
}
