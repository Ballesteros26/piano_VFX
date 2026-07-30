using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000636 RID: 1590
	internal class QilLiteral : QilNode
	{
		// Token: 0x06003EAD RID: 16045 RVA: 0x00157B51 File Offset: 0x00155D51
		public QilLiteral(QilNodeType nodeType, object value)
			: base(nodeType)
		{
			this.Value = value;
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06003EAE RID: 16046 RVA: 0x00157B61 File Offset: 0x00155D61
		// (set) Token: 0x06003EAF RID: 16047 RVA: 0x00157B69 File Offset: 0x00155D69
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x00157B72 File Offset: 0x00155D72
		public static implicit operator string(QilLiteral literal)
		{
			return (string)literal.value;
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x00157B7F File Offset: 0x00155D7F
		public static implicit operator int(QilLiteral literal)
		{
			return (int)literal.value;
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x00157B8C File Offset: 0x00155D8C
		public static implicit operator long(QilLiteral literal)
		{
			return (long)literal.value;
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00157B99 File Offset: 0x00155D99
		public static implicit operator double(QilLiteral literal)
		{
			return (double)literal.value;
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x00157BA6 File Offset: 0x00155DA6
		public static implicit operator decimal(QilLiteral literal)
		{
			return (decimal)literal.value;
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x00157BB3 File Offset: 0x00155DB3
		public static implicit operator XmlQueryType(QilLiteral literal)
		{
			return (XmlQueryType)literal.value;
		}

		// Token: 0x04002849 RID: 10313
		private object value;
	}
}
