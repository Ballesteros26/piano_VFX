using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000071 RID: 113
	internal class CategoryGridEntry : GridEntry
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00016A1C File Offset: 0x00014C1C
		public CategoryGridEntry(PropertyGrid owner, string category, GridEntry parent)
			: base(owner, parent)
		{
			this.label = category;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00016A30 File Offset: 0x00014C30
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.Category;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00016A34 File Offset: 0x00014C34
		public override bool Expandable
		{
			get
			{
				return this.GridItems.Count > 0;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00016A44 File Offset: 0x00014C44
		public override string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00016A4C File Offset: 0x00014C4C
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00016A50 File Offset: 0x00014C50
		public override bool IsEditable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00016A54 File Offset: 0x00014C54
		public override bool IsResetable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040006B3 RID: 1715
		private string label;
	}
}
