using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200063B RID: 1595
	internal class QilParameter : QilIterator
	{
		// Token: 0x06003EE3 RID: 16099 RVA: 0x00157F04 File Offset: 0x00156104
		public QilParameter(QilNodeType nodeType, QilNode defaultValue, QilNode name, XmlQueryType xmlType)
			: base(nodeType, defaultValue)
		{
			this.name = name;
			this.xmlType = xmlType;
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x000026AE File Offset: 0x000008AE
		public override int Count
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		public override QilNode this[int index]
		{
			get
			{
				if (index == 0)
				{
					return base.Binding;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.name;
			}
			set
			{
				if (index == 0)
				{
					base.Binding = value;
					return;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				this.name = value;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x00157F5B File Offset: 0x0015615B
		// (set) Token: 0x06003EE8 RID: 16104 RVA: 0x00157F63 File Offset: 0x00156163
		public QilNode DefaultValue
		{
			get
			{
				return base.Binding;
			}
			set
			{
				base.Binding = value;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x00157F6C File Offset: 0x0015616C
		// (set) Token: 0x06003EEA RID: 16106 RVA: 0x00157F79 File Offset: 0x00156179
		public QilName Name
		{
			get
			{
				return (QilName)this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x040028BD RID: 10429
		private QilNode name;
	}
}
