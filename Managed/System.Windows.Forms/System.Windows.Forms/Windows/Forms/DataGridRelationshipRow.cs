using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020000BD RID: 189
	internal class DataGridRelationshipRow
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x0002FB84 File Offset: 0x0002DD84
		public DataGridRelationshipRow(DataGrid owner)
		{
			this.owner = owner;
			this.IsSelected = false;
			this.IsExpanded = false;
			this.height = 0;
			this.VerticalOffset = 0;
			this.RelationHeight = 0;
			this.relation_area = Rectangle.Empty;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002FBC4 File Offset: 0x0002DDC4
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x0002FBCC File Offset: 0x0002DDCC
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				if (this.height != value)
				{
					this.height = value;
					this.owner.UpdateRowsFrom(this);
				}
			}
		}

		// Token: 0x040008CD RID: 2253
		private DataGrid owner;

		// Token: 0x040008CE RID: 2254
		public int height;

		// Token: 0x040008CF RID: 2255
		public bool IsSelected;

		// Token: 0x040008D0 RID: 2256
		public bool IsExpanded;

		// Token: 0x040008D1 RID: 2257
		public int VerticalOffset;

		// Token: 0x040008D2 RID: 2258
		public int RelationHeight;

		// Token: 0x040008D3 RID: 2259
		public Rectangle relation_area;
	}
}
