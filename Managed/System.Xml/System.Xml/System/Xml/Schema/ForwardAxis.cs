using System;

namespace System.Xml.Schema
{
	// Token: 0x02000387 RID: 903
	internal class ForwardAxis
	{
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x000DE0FE File Offset: 0x000DC2FE
		internal DoubleLinkAxis RootNode
		{
			get
			{
				return this.rootNode;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x000DE106 File Offset: 0x000DC306
		internal DoubleLinkAxis TopNode
		{
			get
			{
				return this.topNode;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x000DE10E File Offset: 0x000DC30E
		internal bool IsAttribute
		{
			get
			{
				return this.isAttribute;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000DE116 File Offset: 0x000DC316
		internal bool IsDss
		{
			get
			{
				return this.isDss;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000DE11E File Offset: 0x000DC31E
		internal bool IsSelfAxis
		{
			get
			{
				return this.isSelfAxis;
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000DE128 File Offset: 0x000DC328
		public ForwardAxis(DoubleLinkAxis axis, bool isdesorself)
		{
			this.isDss = isdesorself;
			this.isAttribute = Asttree.IsAttribute(axis);
			this.topNode = axis;
			this.rootNode = axis;
			while (this.rootNode.Input != null)
			{
				this.rootNode = (DoubleLinkAxis)this.rootNode.Input;
			}
			this.isSelfAxis = Asttree.IsSelf(this.topNode);
		}

		// Token: 0x040018D3 RID: 6355
		private DoubleLinkAxis topNode;

		// Token: 0x040018D4 RID: 6356
		private DoubleLinkAxis rootNode;

		// Token: 0x040018D5 RID: 6357
		private bool isAttribute;

		// Token: 0x040018D6 RID: 6358
		private bool isDss;

		// Token: 0x040018D7 RID: 6359
		private bool isSelfAxis;
	}
}
