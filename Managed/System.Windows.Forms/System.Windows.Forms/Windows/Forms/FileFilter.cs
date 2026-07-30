using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x0200016D RID: 365
	internal class FileFilter
	{
		// Token: 0x06001863 RID: 6243 RVA: 0x0005B264 File Offset: 0x00059464
		public FileFilter()
		{
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0005B278 File Offset: 0x00059478
		public FileFilter(string filter)
		{
			this.filter = filter;
			this.SplitFilter();
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0005B298 File Offset: 0x00059498
		public static bool CheckFilter(string val)
		{
			if (val.Length == 0)
			{
				return true;
			}
			string[] array = val.Split(new char[] { '|' });
			return array.Length % 2 == 0;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0005B2E0 File Offset: 0x000594E0
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x0005B2D4 File Offset: 0x000594D4
		public ArrayList FilterArrayList
		{
			get
			{
				return this.filterArrayList;
			}
			set
			{
				this.filterArrayList = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x0005B2F8 File Offset: 0x000594F8
		// (set) Token: 0x06001868 RID: 6248 RVA: 0x0005B2E8 File Offset: 0x000594E8
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
				this.SplitFilter();
			}
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0005B300 File Offset: 0x00059500
		private void SplitFilter()
		{
			this.filterArrayList.Clear();
			if (this.filter.Length == 0)
			{
				return;
			}
			string[] array = this.filter.Split(new char[] { '|' });
			for (int i = 0; i < array.Length; i += 2)
			{
				FilterStruct filterStruct = new FilterStruct(array[i], array[i + 1]);
				this.filterArrayList.Add(filterStruct);
			}
		}

		// Token: 0x04000D9B RID: 3483
		private ArrayList filterArrayList = new ArrayList();

		// Token: 0x04000D9C RID: 3484
		private string filter;
	}
}
