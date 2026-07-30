using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	internal class StyleRule
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00035ED4 File Offset: 0x000340D4
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x00035EEC File Offset: 0x000340EC
		public StyleProperty[] properties
		{
			get
			{
				return this.m_Properties;
			}
			internal set
			{
				this.m_Properties = value;
			}
		}

		// Token: 0x04000599 RID: 1433
		[SerializeField]
		private StyleProperty[] m_Properties;

		// Token: 0x0400059A RID: 1434
		[SerializeField]
		internal int line;

		// Token: 0x0400059B RID: 1435
		[NonSerialized]
		internal int customPropertiesCount;
	}
}
