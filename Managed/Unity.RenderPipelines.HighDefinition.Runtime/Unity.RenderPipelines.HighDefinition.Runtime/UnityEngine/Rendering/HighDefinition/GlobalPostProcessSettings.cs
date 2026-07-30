using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public struct GlobalPostProcessSettings
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x000391A4 File Offset: 0x000373A4
		internal static GlobalPostProcessSettings NewDefault()
		{
			return new GlobalPostProcessSettings
			{
				lutSize = 32,
				lutFormat = GradingLutFormat.R16G16B16A16,
				bufferFormat = PostProcessBufferFormat.R11G11B10
			};
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x000391D5 File Offset: 0x000373D5
		internal bool supportsAlpha
		{
			get
			{
				return this.bufferFormat != PostProcessBufferFormat.R11G11B10;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000391E4 File Offset: 0x000373E4
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x000391EC File Offset: 0x000373EC
		public int lutSize
		{
			get
			{
				return this.m_LutSize;
			}
			set
			{
				this.m_LutSize = Mathf.Clamp(value, 16, 65);
			}
		}

		// Token: 0x04000803 RID: 2051
		public const int k_MinLutSize = 16;

		// Token: 0x04000804 RID: 2052
		public const int k_MaxLutSize = 65;

		// Token: 0x04000805 RID: 2053
		[SerializeField]
		private int m_LutSize;

		// Token: 0x04000806 RID: 2054
		[FormerlySerializedAs("m_LutFormat")]
		public GradingLutFormat lutFormat;

		// Token: 0x04000807 RID: 2055
		public PostProcessBufferFormat bufferFormat;
	}
}
