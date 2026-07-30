using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000630 RID: 1584
	internal class QilFunction : QilReference
	{
		// Token: 0x06003E84 RID: 16004 RVA: 0x00157776 File Offset: 0x00155976
		public QilFunction(QilNodeType nodeType, QilNode arguments, QilNode definition, QilNode sideEffects, XmlQueryType resultType)
			: base(nodeType)
		{
			this.arguments = arguments;
			this.definition = definition;
			this.sideEffects = sideEffects;
			this.xmlType = resultType;
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06003E85 RID: 16005 RVA: 0x0000226F File Offset: 0x0000046F
		public override int Count
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000C81 RID: 3201
		public override QilNode this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.arguments;
				case 1:
					return this.definition;
				case 2:
					return this.sideEffects;
				default:
					throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.arguments = value;
					return;
				case 1:
					this.definition = value;
					return;
				case 2:
					this.sideEffects = value;
					return;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x00157800 File Offset: 0x00155A00
		// (set) Token: 0x06003E89 RID: 16009 RVA: 0x0015780D File Offset: 0x00155A0D
		public QilList Arguments
		{
			get
			{
				return (QilList)this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x00157816 File Offset: 0x00155A16
		// (set) Token: 0x06003E8B RID: 16011 RVA: 0x0015781E File Offset: 0x00155A1E
		public QilNode Definition
		{
			get
			{
				return this.definition;
			}
			set
			{
				this.definition = value;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x00157827 File Offset: 0x00155A27
		// (set) Token: 0x06003E8D RID: 16013 RVA: 0x00157838 File Offset: 0x00155A38
		public bool MaybeSideEffects
		{
			get
			{
				return this.sideEffects.NodeType == QilNodeType.True;
			}
			set
			{
				this.sideEffects.NodeType = (value ? QilNodeType.True : QilNodeType.False);
			}
		}

		// Token: 0x04002843 RID: 10307
		private QilNode arguments;

		// Token: 0x04002844 RID: 10308
		private QilNode definition;

		// Token: 0x04002845 RID: 10309
		private QilNode sideEffects;
	}
}
