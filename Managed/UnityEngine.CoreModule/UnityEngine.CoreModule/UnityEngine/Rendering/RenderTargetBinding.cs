using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000333 RID: 819
	public struct RenderTargetBinding
	{
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0002C95C File Offset: 0x0002AB5C
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x0002C974 File Offset: 0x0002AB74
		public RenderTargetIdentifier[] colorRenderTargets
		{
			get
			{
				return this.m_ColorRenderTargets;
			}
			set
			{
				this.m_ColorRenderTargets = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0002C980 File Offset: 0x0002AB80
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x0002C998 File Offset: 0x0002AB98
		public RenderTargetIdentifier depthRenderTarget
		{
			get
			{
				return this.m_DepthRenderTarget;
			}
			set
			{
				this.m_DepthRenderTarget = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0002C9A4 File Offset: 0x0002ABA4
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x0002C9BC File Offset: 0x0002ABBC
		public RenderBufferLoadAction[] colorLoadActions
		{
			get
			{
				return this.m_ColorLoadActions;
			}
			set
			{
				this.m_ColorLoadActions = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0002C9C8 File Offset: 0x0002ABC8
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		public RenderBufferStoreAction[] colorStoreActions
		{
			get
			{
				return this.m_ColorStoreActions;
			}
			set
			{
				this.m_ColorStoreActions = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x0002CA04 File Offset: 0x0002AC04
		public RenderBufferLoadAction depthLoadAction
		{
			get
			{
				return this.m_DepthLoadAction;
			}
			set
			{
				this.m_DepthLoadAction = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0002CA10 File Offset: 0x0002AC10
		// (set) Token: 0x06001B2E RID: 6958 RVA: 0x0002CA28 File Offset: 0x0002AC28
		public RenderBufferStoreAction depthStoreAction
		{
			get
			{
				return this.m_DepthStoreAction;
			}
			set
			{
				this.m_DepthStoreAction = value;
			}
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0002CA32 File Offset: 0x0002AC32
		public RenderTargetBinding(RenderTargetIdentifier[] colorRenderTargets, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.m_ColorRenderTargets = colorRenderTargets;
			this.m_DepthRenderTarget = depthRenderTarget;
			this.m_ColorLoadActions = colorLoadActions;
			this.m_ColorStoreActions = colorStoreActions;
			this.m_DepthLoadAction = depthLoadAction;
			this.m_DepthStoreAction = depthStoreAction;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0002CA62 File Offset: 0x0002AC62
		public RenderTargetBinding(RenderTargetIdentifier colorRenderTarget, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this = new RenderTargetBinding(new RenderTargetIdentifier[] { colorRenderTarget }, new RenderBufferLoadAction[] { colorLoadAction }, new RenderBufferStoreAction[] { colorStoreAction }, depthRenderTarget, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0002CA94 File Offset: 0x0002AC94
		public RenderTargetBinding(RenderTargetSetup setup)
		{
			this.m_ColorRenderTargets = new RenderTargetIdentifier[setup.color.Length];
			for (int i = 0; i < this.m_ColorRenderTargets.Length; i++)
			{
				this.m_ColorRenderTargets[i] = new RenderTargetIdentifier(setup.color[i], setup.mipLevel, setup.cubemapFace, setup.depthSlice);
			}
			this.m_DepthRenderTarget = setup.depth;
			this.m_ColorLoadActions = (RenderBufferLoadAction[])setup.colorLoad.Clone();
			this.m_ColorStoreActions = (RenderBufferStoreAction[])setup.colorStore.Clone();
			this.m_DepthLoadAction = setup.depthLoad;
			this.m_DepthStoreAction = setup.depthStore;
		}

		// Token: 0x04000969 RID: 2409
		private RenderTargetIdentifier[] m_ColorRenderTargets;

		// Token: 0x0400096A RID: 2410
		private RenderTargetIdentifier m_DepthRenderTarget;

		// Token: 0x0400096B RID: 2411
		private RenderBufferLoadAction[] m_ColorLoadActions;

		// Token: 0x0400096C RID: 2412
		private RenderBufferStoreAction[] m_ColorStoreActions;

		// Token: 0x0400096D RID: 2413
		private RenderBufferLoadAction m_DepthLoadAction;

		// Token: 0x0400096E RID: 2414
		private RenderBufferStoreAction m_DepthStoreAction;
	}
}
