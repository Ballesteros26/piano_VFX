using System;

namespace System.Xml.Schema
{
	// Token: 0x020003B8 RID: 952
	internal class Datatype_untypedAtomicType : Datatype_anyAtomicType
	{
		// Token: 0x06002607 RID: 9735 RVA: 0x000E40FC File Offset: 0x000E22FC
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x0000226C File Offset: 0x0000046C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x00074D91 File Offset: 0x00072F91
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UntypedAtomic;
			}
		}
	}
}
