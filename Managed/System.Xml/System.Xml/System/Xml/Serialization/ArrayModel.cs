using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002F7 RID: 759
	internal class ArrayModel : TypeModel
	{
		// Token: 0x06001C6F RID: 7279 RVA: 0x0009B6B0 File Offset: 0x000998B0
		internal ArrayModel(Type type, TypeDesc typeDesc, ModelScope scope)
			: base(type, typeDesc, scope)
		{
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0009B6BB File Offset: 0x000998BB
		internal TypeModel Element
		{
			get
			{
				return base.ModelScope.GetTypeModel(TypeScope.GetArrayElementType(base.Type, null));
			}
		}
	}
}
