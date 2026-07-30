using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E5 RID: 997
	internal class Datatype_nonNegativeInteger : Datatype_integer
	{
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060026F4 RID: 9972 RVA: 0x000E5032 File Offset: 0x000E3232
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonNegativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x000E5039 File Offset: 0x000E3239
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonNegativeInteger;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001A0B RID: 6667
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, decimal.MaxValue);
	}
}
