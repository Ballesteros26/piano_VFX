using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C1 RID: 449
	[Serializable]
	internal class StyleProperty
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00035E68 File Offset: 0x00034068
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00035E80 File Offset: 0x00034080
		public string name
		{
			get
			{
				return this.m_Name;
			}
			internal set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00035E8C File Offset: 0x0003408C
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00035EA4 File Offset: 0x000340A4
		public int line
		{
			get
			{
				return this.m_Line;
			}
			internal set
			{
				this.m_Line = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00035EB0 File Offset: 0x000340B0
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00035EC8 File Offset: 0x000340C8
		public StyleValueHandle[] values
		{
			get
			{
				return this.m_Values;
			}
			internal set
			{
				this.m_Values = value;
			}
		}

		// Token: 0x04000594 RID: 1428
		[SerializeField]
		private string m_Name;

		// Token: 0x04000595 RID: 1429
		[SerializeField]
		private int m_Line;

		// Token: 0x04000596 RID: 1430
		[SerializeField]
		private StyleValueHandle[] m_Values;

		// Token: 0x04000597 RID: 1431
		[NonSerialized]
		internal bool isCustomProperty;

		// Token: 0x04000598 RID: 1432
		[NonSerialized]
		internal bool requireVariableResolve;
	}
}
