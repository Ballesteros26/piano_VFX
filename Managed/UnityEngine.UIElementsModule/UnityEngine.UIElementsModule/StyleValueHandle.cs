using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	internal struct StyleValueHandle
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00036A50 File Offset: 0x00034C50
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x00036A68 File Offset: 0x00034C68
		public StyleValueType valueType
		{
			get
			{
				return this.m_ValueType;
			}
			internal set
			{
				this.m_ValueType = value;
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00036A72 File Offset: 0x00034C72
		internal StyleValueHandle(int valueIndex, StyleValueType valueType)
		{
			this.valueIndex = valueIndex;
			this.m_ValueType = valueType;
		}

		// Token: 0x040005C8 RID: 1480
		[SerializeField]
		private StyleValueType m_ValueType;

		// Token: 0x040005C9 RID: 1481
		[SerializeField]
		internal int valueIndex;
	}
}
