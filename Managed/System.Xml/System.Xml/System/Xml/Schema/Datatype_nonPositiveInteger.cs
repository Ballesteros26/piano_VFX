using System;

namespace System.Xml.Schema
{
	// Token: 0x020003DF RID: 991
	internal class Datatype_nonPositiveInteger : Datatype_integer
	{
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x000E4CB0 File Offset: 0x000E2EB0
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonPositiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x000E4CB7 File Offset: 0x000E2EB7
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonPositiveInteger;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040019FD RID: 6653
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, 0m);
	}
}
