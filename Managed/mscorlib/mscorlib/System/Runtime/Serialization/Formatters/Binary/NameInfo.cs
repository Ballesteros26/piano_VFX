using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000744 RID: 1860
	internal sealed class NameInfo
	{
		// Token: 0x06004D1B RID: 19739 RVA: 0x00002111 File Offset: 0x00000311
		internal NameInfo()
		{
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x001167C0 File Offset: 0x001149C0
		internal void Init()
		{
			this.NIFullName = null;
			this.NIobjectId = 0L;
			this.NIassemId = 0L;
			this.NIprimitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
			this.NItype = null;
			this.NIisSealed = false;
			this.NItransmitTypeOnObject = false;
			this.NItransmitTypeOnMember = false;
			this.NIisParentTypeOnObject = false;
			this.NIisArray = false;
			this.NIisArrayItem = false;
			this.NIarrayEnum = InternalArrayTypeE.Empty;
			this.NIsealedStatusChecked = false;
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004D1D RID: 19741 RVA: 0x0011682A File Offset: 0x00114A2A
		public bool IsSealed
		{
			get
			{
				if (!this.NIsealedStatusChecked)
				{
					this.NIisSealed = this.NItype.IsSealed;
					this.NIsealedStatusChecked = true;
				}
				return this.NIisSealed;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x00116852 File Offset: 0x00114A52
		// (set) Token: 0x06004D1F RID: 19743 RVA: 0x00116873 File Offset: 0x00114A73
		public string NIname
		{
			get
			{
				if (this.NIFullName == null)
				{
					this.NIFullName = this.NItype.FullName;
				}
				return this.NIFullName;
			}
			set
			{
				this.NIFullName = value;
			}
		}

		// Token: 0x0400296F RID: 10607
		internal string NIFullName;

		// Token: 0x04002970 RID: 10608
		internal long NIobjectId;

		// Token: 0x04002971 RID: 10609
		internal long NIassemId;

		// Token: 0x04002972 RID: 10610
		internal InternalPrimitiveTypeE NIprimitiveTypeEnum;

		// Token: 0x04002973 RID: 10611
		internal Type NItype;

		// Token: 0x04002974 RID: 10612
		internal bool NIisSealed;

		// Token: 0x04002975 RID: 10613
		internal bool NIisArray;

		// Token: 0x04002976 RID: 10614
		internal bool NIisArrayItem;

		// Token: 0x04002977 RID: 10615
		internal bool NItransmitTypeOnObject;

		// Token: 0x04002978 RID: 10616
		internal bool NItransmitTypeOnMember;

		// Token: 0x04002979 RID: 10617
		internal bool NIisParentTypeOnObject;

		// Token: 0x0400297A RID: 10618
		internal InternalArrayTypeE NIarrayEnum;

		// Token: 0x0400297B RID: 10619
		private bool NIsealedStatusChecked;
	}
}
