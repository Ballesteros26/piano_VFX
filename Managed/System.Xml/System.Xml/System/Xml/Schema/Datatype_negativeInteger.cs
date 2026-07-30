using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E0 RID: 992
	internal class Datatype_negativeInteger : Datatype_nonPositiveInteger
	{
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x000E4CDE File Offset: 0x000E2EDE
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_negativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x000E4CE5 File Offset: 0x000E2EE5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NegativeInteger;
			}
		}

		// Token: 0x040019FE RID: 6654
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, -1m);
	}
}
