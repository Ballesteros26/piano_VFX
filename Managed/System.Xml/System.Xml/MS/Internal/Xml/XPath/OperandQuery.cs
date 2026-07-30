using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000033 RID: 51
	internal sealed class OperandQuery : ValueQuery
	{
		// Token: 0x06000164 RID: 356 RVA: 0x000058F0 File Offset: 0x00003AF0
		public OperandQuery(object val)
		{
			this.val = val;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000058FF File Offset: 0x00003AFF
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.val;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005907 File Offset: 0x00003B07
		public override XPathResultType StaticType
		{
			get
			{
				return base.GetXPathType(this.val);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00002068 File Offset: 0x00000268
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005915 File Offset: 0x00003B15
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("value", Convert.ToString(this.val, CultureInfo.InvariantCulture));
			w.WriteEndElement();
		}

		// Token: 0x040000C4 RID: 196
		internal object val;
	}
}
