using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[NativeType(CodegenOptions.Custom, "MonoHumanLimit")]
	public struct HumanLimit
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00003B28 File Offset: 0x00001D28
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00003B43 File Offset: 0x00001D43
		public bool useDefaultValues
		{
			get
			{
				return this.m_UseDefaultValues != 0;
			}
			set
			{
				this.m_UseDefaultValues = (value ? 1 : 0);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00003B54 File Offset: 0x00001D54
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00003B6C File Offset: 0x00001D6C
		public Vector3 min
		{
			get
			{
				return this.m_Min;
			}
			set
			{
				this.m_Min = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00003B78 File Offset: 0x00001D78
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00003B90 File Offset: 0x00001D90
		public Vector3 max
		{
			get
			{
				return this.m_Max;
			}
			set
			{
				this.m_Max = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00003B9C File Offset: 0x00001D9C
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00003BC0 File Offset: 0x00001DC0
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public float axisLength
		{
			get
			{
				return this.m_AxisLength;
			}
			set
			{
				this.m_AxisLength = value;
			}
		}

		// Token: 0x0400010D RID: 269
		private Vector3 m_Min;

		// Token: 0x0400010E RID: 270
		private Vector3 m_Max;

		// Token: 0x0400010F RID: 271
		private Vector3 m_Center;

		// Token: 0x04000110 RID: 272
		private float m_AxisLength;

		// Token: 0x04000111 RID: 273
		private int m_UseDefaultValues;
	}
}
