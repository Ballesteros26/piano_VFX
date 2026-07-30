using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200034E RID: 846
	[RequiredByNativeCode]
	public class OnDemandRendering
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x0002CD88 File Offset: 0x0002AF88
		public static bool willCurrentFrameRender
		{
			get
			{
				return Time.frameCount % OnDemandRendering.renderFrameInterval == 0;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x0002CDA8 File Offset: 0x0002AFA8
		// (set) Token: 0x06001B5F RID: 7007 RVA: 0x0002CDBF File Offset: 0x0002AFBF
		public static int renderFrameInterval
		{
			get
			{
				return OnDemandRendering.m_RenderFrameInterval;
			}
			set
			{
				OnDemandRendering.m_RenderFrameInterval = Math.Max(1, value);
			}
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0002CDCE File Offset: 0x0002AFCE
		[RequiredByNativeCode]
		internal static void GetRenderFrameInterval(out int frameInterval)
		{
			frameInterval = OnDemandRendering.renderFrameInterval;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0002CDD8 File Offset: 0x0002AFD8
		public static int effectiveRenderFrameRate
		{
			get
			{
				bool flag = QualitySettings.vSyncCount > 0;
				int num;
				if (flag)
				{
					num = Screen.currentResolution.refreshRate / QualitySettings.vSyncCount / OnDemandRendering.renderFrameInterval;
				}
				else
				{
					bool flag2 = Application.targetFrameRate <= 0;
					if (flag2)
					{
						num = Application.targetFrameRate;
					}
					else
					{
						num = Application.targetFrameRate / OnDemandRendering.renderFrameInterval;
					}
				}
				return num;
			}
		}

		// Token: 0x040009FB RID: 2555
		private static int m_RenderFrameInterval = 1;
	}
}
