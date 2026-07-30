using System;

namespace UnityEngine.Rendering.HighDefinition.Attributes
{
	// Token: 0x02000187 RID: 391
	internal class MaterialSharedPropertyMappingAttribute : Attribute
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x00054801 File Offset: 0x00052A01
		public MaterialSharedPropertyMappingAttribute(MaterialSharedProperty property)
		{
			this.property = property;
		}

		// Token: 0x0400108E RID: 4238
		public readonly MaterialSharedProperty property;
	}
}
