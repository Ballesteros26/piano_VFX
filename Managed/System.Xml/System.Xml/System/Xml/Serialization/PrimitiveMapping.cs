using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002E8 RID: 744
	internal class PrimitiveMapping : TypeMapping
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00099F1B File Offset: 0x0009811B
		// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x00099F23 File Offset: 0x00098123
		internal override bool IsList
		{
			get
			{
				return this.isList;
			}
			set
			{
				this.isList = value;
			}
		}

		// Token: 0x04001611 RID: 5649
		private bool isList;
	}
}
