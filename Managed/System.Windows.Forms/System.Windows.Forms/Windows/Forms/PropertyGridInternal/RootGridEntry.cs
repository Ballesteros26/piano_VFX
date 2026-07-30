using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020002C1 RID: 705
	[MonoInternalNote("needs to implement IRootGridEntry")]
	internal class RootGridEntry : GridEntry
	{
		// Token: 0x06002EC3 RID: 11971 RVA: 0x000B4ADC File Offset: 0x000B2CDC
		public RootGridEntry(PropertyGrid owner, object[] obj)
			: base(owner, null)
		{
			if (obj == null || obj.Length == 0)
			{
				throw new ArgumentNullException("obj");
			}
			this.val = obj;
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000B4B14 File Offset: 0x000B2D14
		public override bool Expandable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x000B4B18 File Offset: 0x000B2D18
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.Root;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06002EC6 RID: 11974 RVA: 0x000B4B1C File Offset: 0x000B2D1C
		public override string Label
		{
			get
			{
				return (this.val.Length <= 1) ? this.val[0].GetType().ToString() : this.val.GetType().ToString();
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000B4B60 File Offset: 0x000B2D60
		public override object Value
		{
			get
			{
				return (this.val.Length <= 1) ? this.val[0] : this.val;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x000B4B84 File Offset: 0x000B2D84
		public override object[] Values
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000B4B8C File Offset: 0x000B2D8C
		public override bool Select()
		{
			return false;
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x000B4B90 File Offset: 0x000B2D90
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000B4B94 File Offset: 0x000B2D94
		public override bool IsEditable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06002ECC RID: 11980 RVA: 0x000B4B98 File Offset: 0x000B2D98
		public override bool IsResetable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x000B4B9C File Offset: 0x000B2D9C
		public override bool IsMerged
		{
			get
			{
				return this.val.Length > 1;
			}
		}

		// Token: 0x04001674 RID: 5748
		private object[] val;
	}
}
