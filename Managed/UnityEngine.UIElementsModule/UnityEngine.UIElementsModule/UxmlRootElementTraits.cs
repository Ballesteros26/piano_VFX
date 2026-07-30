using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D6 RID: 470
	public class UxmlRootElementTraits : UxmlTraits
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x000372F0 File Offset: 0x000354F0
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield return new UxmlChildElementDescription(typeof(VisualElement));
				yield break;
			}
		}

		// Token: 0x040005F9 RID: 1529
		protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x040005FA RID: 1530
		private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
		{
			name = "class"
		};
	}
}
