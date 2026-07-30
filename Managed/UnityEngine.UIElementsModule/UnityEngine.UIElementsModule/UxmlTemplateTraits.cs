using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DC RID: 476
	public class UxmlTemplateTraits : UxmlTraits
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x000375E4 File Offset: 0x000357E4
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x04000608 RID: 1544
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name",
			use = UxmlAttributeDescription.Use.Required
		};

		// Token: 0x04000609 RID: 1545
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x0400060A RID: 1546
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};
	}
}
