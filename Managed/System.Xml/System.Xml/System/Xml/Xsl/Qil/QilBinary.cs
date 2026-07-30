using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200062A RID: 1578
	internal class QilBinary : QilNode
	{
		// Token: 0x06003DD4 RID: 15828 RVA: 0x00155F78 File Offset: 0x00154178
		public QilBinary(QilNodeType nodeType, QilNode left, QilNode right)
			: base(nodeType)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x000026AE File Offset: 0x000008AE
		public override int Count
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000C6D RID: 3181
		public override QilNode this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.left;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.right;
			}
			set
			{
				if (index == 0)
				{
					this.left = value;
					return;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				this.right = value;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06003DD8 RID: 15832 RVA: 0x00155FCD File Offset: 0x001541CD
		// (set) Token: 0x06003DD9 RID: 15833 RVA: 0x00155FD5 File Offset: 0x001541D5
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

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06003DDA RID: 15834 RVA: 0x00155FDE File Offset: 0x001541DE
		// (set) Token: 0x06003DDB RID: 15835 RVA: 0x00155FE6 File Offset: 0x001541E6
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

		// Token: 0x04002835 RID: 10293
		private QilNode left;

		// Token: 0x04002836 RID: 10294
		private QilNode right;
	}
}
