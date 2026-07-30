using System;

namespace System.Xml.Schema
{
	// Token: 0x020003C0 RID: 960
	internal class Datatype_dayTimeDuration : Datatype_duration
	{
		// Token: 0x0600264D RID: 9805 RVA: 0x000E459C File Offset: 0x000E279C
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
				ex = XsdDuration.TryParse(s, XsdDuration.DurationType.DayTimeDuration, out xsdDuration);
				if (ex == null)
				{
					TimeSpan timeSpan;
					ex = xsdDuration.TryToTimeSpan(XsdDuration.DurationType.DayTimeDuration, out timeSpan);
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

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x000E4610 File Offset: 0x000E2810
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DayTimeDuration;
			}
		}
	}
}
