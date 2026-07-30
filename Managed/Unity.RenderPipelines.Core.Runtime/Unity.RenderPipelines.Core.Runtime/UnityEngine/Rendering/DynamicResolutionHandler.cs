using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000021 RID: 33
	public class DynamicResolutionHandler
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004E74 File Offset: 0x00003074
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00004E7C File Offset: 0x0000307C
		public DynamicResUpscaleFilter filter { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004E85 File Offset: 0x00003085
		public static DynamicResolutionHandler instance
		{
			get
			{
				return DynamicResolutionHandler.s_Instance;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004E8C File Offset: 0x0000308C
		private DynamicResolutionHandler()
		{
			this.m_DynamicResMethod = new PerformDynamicRes(DynamicResolutionHandler.DefaultDynamicResMethod);
			this.filter = DynamicResUpscaleFilter.Bilinear;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004F15 File Offset: 0x00003115
		private static float DefaultDynamicResMethod()
		{
			return 1f;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004F1C File Offset: 0x0000311C
		private void ProcessSettings(GlobalDynamicResolutionSettings settings)
		{
			this.m_Enabled = settings.enabled;
			if (!this.m_Enabled)
			{
				this.m_CurrentFraction = 1f;
				return;
			}
			this.type = settings.dynResType;
			float num = Mathf.Clamp(settings.minPercentage / 100f, 0.1f, 1f);
			this.m_MinScreenFraction = num;
			float num2 = Mathf.Clamp(settings.maxPercentage / 100f, this.m_MinScreenFraction, 3f);
			this.m_MaxScreenFraction = num2;
			this.filter = settings.upsampleFilter;
			this.m_ForcingRes = settings.forceResolution;
			if (this.m_ForcingRes)
			{
				float num3 = Mathf.Clamp(settings.forcedPercentage / 100f, 0.1f, 1.5f);
				this.m_CurrentFraction = num3;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004FDF File Offset: 0x000031DF
		public static void SetDynamicResScaler(PerformDynamicRes scaler, DynamicResScalePolicyType scalerType = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
		{
			DynamicResolutionHandler.s_Instance.m_ScalerType = scalerType;
			DynamicResolutionHandler.s_Instance.m_DynamicResMethod = scaler;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004FF7 File Offset: 0x000031F7
		public void SetCurrentCameraRequest(bool cameraRequest)
		{
			this.m_CurrentCameraRequest = cameraRequest;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005000 File Offset: 0x00003200
		public void Update(GlobalDynamicResolutionSettings settings, Action OnResolutionChange = null)
		{
			this.ProcessSettings(settings);
			if (!this.m_Enabled)
			{
				return;
			}
			if (!this.m_ForcingRes)
			{
				if (this.m_ScalerType == DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
				{
					float num = Mathf.Clamp(this.m_DynamicResMethod(), 0f, 1f);
					this.m_CurrentFraction = Mathf.Lerp(this.m_MinScreenFraction, this.m_MaxScreenFraction, num);
				}
				else if (this.m_ScalerType == DynamicResScalePolicyType.ReturnsPercentage)
				{
					float num2 = Mathf.Max(this.m_DynamicResMethod(), 5f);
					this.m_CurrentFraction = Mathf.Clamp(num2 / 100f, this.m_MinScreenFraction, this.m_MaxScreenFraction);
				}
			}
			if (this.m_CurrentFraction != this.m_PrevFraction)
			{
				this.m_PrevFraction = this.m_CurrentFraction;
				if (!this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware)
				{
					ScalableBufferManager.ResizeBuffers(this.m_CurrentFraction, this.m_CurrentFraction);
				}
				OnResolutionChange();
			}
			else if (!this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware && (ScalableBufferManager.widthScaleFactor != this.m_PrevHWScaleWidth || ScalableBufferManager.heightScaleFactor != this.m_PrevHWScaleHeight))
			{
				OnResolutionChange();
			}
			this.m_PrevHWScaleWidth = ScalableBufferManager.widthScaleFactor;
			this.m_PrevHWScaleHeight = ScalableBufferManager.heightScaleFactor;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000512A File Offset: 0x0000332A
		public bool SoftwareDynamicResIsEnabled()
		{
			return this.m_CurrentCameraRequest && this.m_Enabled && this.m_CurrentFraction != 1f && (this.m_ForceSoftwareFallback || this.type == DynamicResolutionType.Software);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000515E File Offset: 0x0000335E
		public bool HardwareDynamicResIsEnabled()
		{
			return !this.m_ForceSoftwareFallback && this.m_CurrentCameraRequest && this.m_Enabled && this.type == DynamicResolutionType.Hardware;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005183 File Offset: 0x00003383
		public bool RequestsHardwareDynamicResolution()
		{
			return !this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005198 File Offset: 0x00003398
		public bool DynamicResolutionEnabled()
		{
			return this.m_CurrentCameraRequest && this.m_Enabled && this.m_CurrentFraction != 1f;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000051BC File Offset: 0x000033BC
		public void ForceSoftwareFallback()
		{
			this.m_ForceSoftwareFallback = true;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000051C8 File Offset: 0x000033C8
		public Vector2Int GetScaledSize(Vector2Int size)
		{
			this.cachedOriginalSize = size;
			if (!this.m_Enabled || !this.m_CurrentCameraRequest)
			{
				return size;
			}
			float num = this.m_CurrentFraction;
			float num2 = this.m_CurrentFraction;
			if (!this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware)
			{
				num = ScalableBufferManager.widthScaleFactor;
				num2 = ScalableBufferManager.heightScaleFactor;
			}
			Vector2Int vector2Int = new Vector2Int(Mathf.CeilToInt((float)size.x * num), Mathf.CeilToInt((float)size.y * num2));
			if (this.m_ForceSoftwareFallback || this.type != DynamicResolutionType.Hardware)
			{
				vector2Int.x += 1 & vector2Int.x;
				vector2Int.y += 1 & vector2Int.y;
			}
			this.m_LastScaledSize = vector2Int;
			return vector2Int;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005285 File Offset: 0x00003485
		public float GetCurrentScale()
		{
			if (!this.m_Enabled || !this.m_CurrentCameraRequest)
			{
				return 1f;
			}
			return this.m_CurrentFraction;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000052A3 File Offset: 0x000034A3
		public Vector2Int GetLastScaledSize()
		{
			return this.m_LastScaledSize;
		}

		// Token: 0x04000094 RID: 148
		private bool m_Enabled;

		// Token: 0x04000095 RID: 149
		private float m_MinScreenFraction = 1f;

		// Token: 0x04000096 RID: 150
		private float m_MaxScreenFraction = 1f;

		// Token: 0x04000097 RID: 151
		private float m_CurrentFraction = 1f;

		// Token: 0x04000098 RID: 152
		private float m_PrevFraction = -1f;

		// Token: 0x04000099 RID: 153
		private bool m_ForcingRes;

		// Token: 0x0400009A RID: 154
		private bool m_CurrentCameraRequest = true;

		// Token: 0x0400009B RID: 155
		private bool m_ForceSoftwareFallback;

		// Token: 0x0400009C RID: 156
		private float m_PrevHWScaleWidth = 1f;

		// Token: 0x0400009D RID: 157
		private float m_PrevHWScaleHeight = 1f;

		// Token: 0x0400009E RID: 158
		private Vector2Int m_LastScaledSize = new Vector2Int(0, 0);

		// Token: 0x0400009F RID: 159
		private DynamicResScalePolicyType m_ScalerType = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor;

		// Token: 0x040000A0 RID: 160
		private Vector2Int cachedOriginalSize;

		// Token: 0x040000A2 RID: 162
		private DynamicResolutionType type;

		// Token: 0x040000A3 RID: 163
		private PerformDynamicRes m_DynamicResMethod;

		// Token: 0x040000A4 RID: 164
		private static DynamicResolutionHandler s_Instance = new DynamicResolutionHandler();
	}
}
