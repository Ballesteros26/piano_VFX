using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D9 RID: 473
	public class UxmlStyleTraits : UxmlTraits
	{
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0003748C File Offset: 0x0003568C
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x04000600 RID: 1536
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x04000601 RID: 1537
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x04000602 RID: 1538
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};
	}
}
