using System;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	internal class StyleSelector
	{
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00035EF8 File Offset: 0x000340F8
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00035F10 File Offset: 0x00034110
		public StyleSelectorPart[] parts
		{
			get
			{
				return this.m_Parts;
			}
			internal set
			{
				this.m_Parts = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00035F1C File Offset: 0x0003411C
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00035F34 File Offset: 0x00034134
		public StyleSelectorRelationship previousRelationship
		{
			get
			{
				return this.m_PreviousRelationship;
			}
			internal set
			{
				this.m_PreviousRelationship = value;
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00035F40 File Offset: 0x00034140
		public override string ToString()
		{
			return string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<StyleSelectorPart, string>(this.parts, (StyleSelectorPart p) => p.ToString())));
		}

		// Token: 0x0400059C RID: 1436
		[SerializeField]
		private StyleSelectorPart[] m_Parts;

		// Token: 0x0400059D RID: 1437
		[SerializeField]
		private StyleSelectorRelationship m_PreviousRelationship;

		// Token: 0x0400059E RID: 1438
		internal int pseudoStateMask = -1;

		// Token: 0x0400059F RID: 1439
		internal int negatedPseudoStateMask = -1;
	}
}
