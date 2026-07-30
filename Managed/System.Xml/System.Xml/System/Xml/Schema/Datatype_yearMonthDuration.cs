using System;

namespace System.Xml.Schema
{
	// Token: 0x020003BF RID: 959
	internal class Datatype_yearMonthDuration : Datatype_duration
	{
		// Token: 0x0600264A RID: 9802 RVA: 0x000E451C File Offset: 0x000E271C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.durationFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XsdDuration xsdDuration;
				ex = XsdDuration.TryParse(s, XsdDuration.DurationType.YearMonthDuration, out xsdDuration);
				if (ex == null)
				{
					TimeSpan timeSpan;
					ex = xsdDuration.TryToTimeSpan(XsdDuration.DurationType.YearMonthDuration, out timeSpan);
					if (ex == null)
					{
						ex = DatatypeImplementation.durationFacetsChecker.CheckValueFacets(timeSpan, this);
						if (ex == null)
						{
							typedValue = timeSpan;
							return null;
						}
					}
				}
			}
			return ex;
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x000E4590 File Offset: 0x000E2790
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.YearMonthDuration;
			}
		}
	}
}
