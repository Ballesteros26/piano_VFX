using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000647 RID: 1607
	internal class QilUnary : QilNode
	{
		// Token: 0x06004080 RID: 16512 RVA: 0x00159C47 File Offset: 0x00157E47
		public QilUnary(QilNodeType nodeType, QilNode child)
			: base(nodeType)
		{
			this.child = child;
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x00003242 File Offset: 0x00001442
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		public override QilNode this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this.child;
			}
			set
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				this.child = value;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x00159C7A File Offset: 0x00157E7A
		// (set) Token: 0x06004085 RID: 16517 RVA: 0x00159C82 File Offset: 0x00157E82
		public QilNode Child
		{
			get
			{
				return this.child;
			}
			set
			{
				this.child = value;
			}
		}

		// Token: 0x040028CB RID: 10443
		private QilNode child;
	}
}
