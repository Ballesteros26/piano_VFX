using System;

namespace UnityEngine.Lumin
{
	// Token: 0x02000308 RID: 776
	[AttributeUsage(4, AllowMultiple = false)]
	public sealed class UsesLuminPlatformLevelAttribute : Attribute
	{
		// Token: 0x06001A9D RID: 6813 RVA: 0x0002B987 File Offset: 0x00029B87
		public UsesLuminPlatformLevelAttribute(uint platformLevel)
		{
			this.m_PlatformLevel = platformLevel;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0002B998 File Offset: 0x00029B98
		public uint platformLevel
		{
			get
			{
				return this.m_PlatformLevel;
			}
		}

		// Token: 0x04000833 RID: 2099
		private readonly uint m_PlatformLevel;
	}
}
