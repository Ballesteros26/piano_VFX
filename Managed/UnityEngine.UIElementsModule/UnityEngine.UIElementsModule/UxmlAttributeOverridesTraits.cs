using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DF RID: 479
	public class UxmlAttributeOverridesTraits : UxmlTraits
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00037744 File Offset: 0x00035944
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x04000610 RID: 1552
		internal const string k_ElementNameAttributeName = "element-name";

		// Token: 0x04000611 RID: 1553
		private UxmlStringAttributeDescription m_ElementName = new UxmlStringAttributeDescription
		{
			name = "element-name",
			use = UxmlAttributeDescription.Use.Required
		};
	}
}
