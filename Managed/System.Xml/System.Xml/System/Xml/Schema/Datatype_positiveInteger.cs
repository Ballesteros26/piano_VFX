using System;

namespace System.Xml.Schema
{
	// Token: 0x020003EA RID: 1002
	internal class Datatype_positiveInteger : Datatype_nonNegativeInteger
	{
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x000E5363 File Offset: 0x000E3563
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_positiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x000E536A File Offset: 0x000E356A
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.PositiveInteger;
			}
		}

		// Token: 0x04001A18 RID: 6680
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(1m, decimal.MaxValue);
	}
}
