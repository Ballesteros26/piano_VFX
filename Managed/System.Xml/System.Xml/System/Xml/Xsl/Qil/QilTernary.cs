using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000645 RID: 1605
	internal class QilTernary : QilNode
	{
		// Token: 0x06003FFD RID: 16381 RVA: 0x00158E19 File Offset: 0x00157019
		public QilTernary(QilNodeType nodeType, QilNode left, QilNode center, QilNode right)
			: base(nodeType)
		{
			this.left = left;
			this.center = center;
			this.right = right;
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x0000226F File Offset: 0x0000046F
		public override int Count
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		public override QilNode this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.left;
				case 1:
					return this.center;
				case 2:
					return this.right;
				default:
					throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.left = value;
					return;
				case 1:
					this.center = value;
					return;
				case 2:
					this.right = value;
					return;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004001 RID: 16385 RVA: 0x00158E9B File Offset: 0x0015709B
		// (set) Token: 0x06004002 RID: 16386 RVA: 0x00158EA3 File Offset: 0x001570A3
		public QilNode Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x00158EAC File Offset: 0x001570AC
		// (set) Token: 0x06004004 RID: 16388 RVA: 0x00158EB4 File Offset: 0x001570B4
		public QilNode Center
		{
			get
			{
				return this.center;
			}
			set
			{
				this.center = value;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x00158EBD File Offset: 0x001570BD
		// (set) Token: 0x06004006 RID: 16390 RVA: 0x00158EC5 File Offset: 0x001570C5
		public QilNode Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		// Token: 0x040028C8 RID: 10440
		private QilNode left;

		// Token: 0x040028C9 RID: 10441
		private QilNode center;

		// Token: 0x040028CA RID: 10442
		private QilNode right;
	}
}
