using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002E9 RID: 745
	internal class NullableMapping : TypeMapping
	{
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00099F34 File Offset: 0x00098134
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x00099F3C File Offset: 0x0009813C
		internal TypeMapping BaseMapping
		{
			get
			{
				return this.baseMapping;
			}
			set
			{
				this.baseMapping = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00099F45 File Offset: 0x00098145
		internal override string DefaultElementName
		{
			get
			{
				return this.BaseMapping.DefaultElementName;
			}
		}

		// Token: 0x04001612 RID: 5650
		private TypeMapping baseMapping;
	}
}
