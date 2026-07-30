using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000145 RID: 325
	[Serializable]
	public class ScalableSettingValue<T>
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0004C0FC File Offset: 0x0004A2FC
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x0004C104 File Offset: 0x0004A304
		public int level
		{
			get
			{
				return this.m_Level;
			}
			set
			{
				this.m_Level = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0004C10D File Offset: 0x0004A30D
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x0004C115 File Offset: 0x0004A315
		public bool useOverride
		{
			get
			{
				return this.m_UseOverride;
			}
			set
			{
				this.m_UseOverride = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0004C11E File Offset: 0x0004A31E
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0004C126 File Offset: 0x0004A326
		public T @override
		{
			get
			{
				return this.m_Override;
			}
			set
			{
				this.m_Override = value;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0004C12F File Offset: 0x0004A32F
		public T Value(ScalableSetting<T> source)
		{
			if (!this.m_UseOverride && source != null)
			{
				return source[this.m_Level];
			}
			return this.m_Override;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0004C14F File Offset: 0x0004A34F
		public void CopyTo(ScalableSettingValue<T> target)
		{
			target.m_Override = this.m_Override;
			target.m_UseOverride = this.m_UseOverride;
			target.m_Level = this.m_Level;
		}

		// Token: 0x04000EFC RID: 3836
		[SerializeField]
		private T m_Override;

		// Token: 0x04000EFD RID: 3837
		[SerializeField]
		private bool m_UseOverride;

		// Token: 0x04000EFE RID: 3838
		[SerializeField]
		private int m_Level;
	}
}
