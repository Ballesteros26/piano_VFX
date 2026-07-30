using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000351 RID: 849
	internal class CollectionDataSource : IDataSource
	{
		// Token: 0x06001F6C RID: 8044 RVA: 0x0004F900 File Offset: 0x0004DB00
		public CollectionDataSource(IEnumerable collection)
		{
			this.collection = collection;
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06001F6D RID: 8045 RVA: 0x0000393A File Offset: 0x00001B3A
		// (remove) Token: 0x06001F6E RID: 8046 RVA: 0x0000393A File Offset: 0x00001B3A
		public event EventHandler DataSourceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0004F90F File Offset: 0x0004DB0F
		public DataSourceView GetView(string viewName)
		{
			return new CollectionDataSourceView(this, viewName, this.collection);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0004F91E File Offset: 0x0004DB1E
		public ICollection GetViewNames()
		{
			return CollectionDataSource.names;
		}

		// Token: 0x04001888 RID: 6280
		private static readonly string[] names = new string[0];

		// Token: 0x04001889 RID: 6281
		private IEnumerable collection;
	}
}
