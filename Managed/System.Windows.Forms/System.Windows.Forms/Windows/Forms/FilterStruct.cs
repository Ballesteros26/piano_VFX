using System;
using System.Collections.Specialized;

namespace System.Windows.Forms
{
	// Token: 0x0200016C RID: 364
	internal struct FilterStruct
	{
		// Token: 0x06001861 RID: 6241 RVA: 0x0005B1FC File Offset: 0x000593FC
		public FilterStruct(string filterName, string filter)
		{
			this.filterName = filterName;
			this.filters = new StringCollection();
			this.SplitFilters(filter);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0005B218 File Offset: 0x00059418
		private void SplitFilters(string filter)
		{
			string[] array = filter.Split(new char[] { ';' });
			foreach (string text in array)
			{
				this.filters.Add(text.Trim());
			}
		}

		// Token: 0x04000D99 RID: 3481
		public string filterName;

		// Token: 0x04000D9A RID: 3482
		public StringCollection filters;
	}
}
