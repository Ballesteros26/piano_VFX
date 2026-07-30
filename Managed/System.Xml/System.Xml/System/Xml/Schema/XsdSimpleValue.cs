using System;

namespace System.Xml.Schema
{
	// Token: 0x020003AE RID: 942
	internal class XsdSimpleValue
	{
		// Token: 0x060025A1 RID: 9633 RVA: 0x000E23EC File Offset: 0x000E05EC
		public XsdSimpleValue(XmlSchemaSimpleType st, object value)
		{
			this.xmlType = st;
			this.typedValue = value;
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000E2402 File Offset: 0x000E0602
		public XmlSchemaSimpleType XmlType
		{
			get
			{
				return this.xmlType;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000E240A File Offset: 0x000E060A
		public object TypedValue
		{
			get
			{
				return this.typedValue;
			}
		}

		// Token: 0x0400195E RID: 6494
		private XmlSchemaSimpleType xmlType;

		// Token: 0x0400195F RID: 6495
		private object typedValue;
	}
}
