using System;

namespace System.Xml.Schema
{
	// Token: 0x020003B7 RID: 951
	internal class Datatype_anyAtomicType : Datatype_anySimpleType
	{
		// Token: 0x06002603 RID: 9731 RVA: 0x000E4159 File Offset: 0x000E2359
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlAnyConverter.AnyAtomic;
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x0000226C File Offset: 0x0000046C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x00074F5D File Offset: 0x0007315D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}
	}
}
