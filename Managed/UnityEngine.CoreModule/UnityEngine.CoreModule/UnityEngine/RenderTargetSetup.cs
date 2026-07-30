using System;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000E9 RID: 233
	public struct RenderTargetSetup
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, RenderBufferLoadAction[] colorLoad, RenderBufferStoreAction[] colorStore, RenderBufferLoadAction depthLoad, RenderBufferStoreAction depthStore)
		{
			this.color = color;
			this.depth = depth;
			this.mipLevel = mip;
			this.cubemapFace = face;
			this.depthSlice = 0;
			this.colorLoad = colorLoad;
			this.colorStore = colorStore;
			this.depthLoad = depthLoad;
			this.depthStore = depthStore;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		internal static RenderBufferLoadAction[] LoadActions(RenderBuffer[] buf)
		{
			RenderBufferLoadAction[] array = new RenderBufferLoadAction[buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i].loadAction;
				buf[i].loadAction = RenderBufferLoadAction.Load;
			}
			return array;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0000C444 File Offset: 0x0000A644
		internal static RenderBufferStoreAction[] StoreActions(RenderBuffer[] buf)
		{
			RenderBufferStoreAction[] array = new RenderBufferStoreAction[buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i].storeAction;
				buf[i].storeAction = RenderBufferStoreAction.Store;
			}
			return array;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0000C491 File Offset: 0x0000A691
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth)
		{
			this = new RenderTargetSetup(new RenderBuffer[] { color }, depth);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0000C4AA File Offset: 0x0000A6AA
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel)
		{
			this = new RenderTargetSetup(new RenderBuffer[] { color }, depth, mipLevel);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel, CubemapFace face)
		{
			this = new RenderTargetSetup(new RenderBuffer[] { color }, depth, mipLevel, face);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel, CubemapFace face, int depthSlice)
		{
			this = new RenderTargetSetup(new RenderBuffer[] { color }, depth, mipLevel, face);
			this.depthSlice = depthSlice;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000C504 File Offset: 0x0000A704
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth)
		{
			this = new RenderTargetSetup(color, depth, 0, CubemapFace.Unknown);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0000C512 File Offset: 0x0000A712
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mipLevel)
		{
			this = new RenderTargetSetup(color, depth, mipLevel, CubemapFace.Unknown);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0000C520 File Offset: 0x0000A720
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face)
		{
			this = new RenderTargetSetup(color, depth, mip, face, RenderTargetSetup.LoadActions(color), RenderTargetSetup.StoreActions(color), depth.loadAction, depth.storeAction);
		}

		// Token: 0x04000287 RID: 647
		public RenderBuffer[] color;

		// Token: 0x04000288 RID: 648
		public RenderBuffer depth;

		// Token: 0x04000289 RID: 649
		public int mipLevel;

		// Token: 0x0400028A RID: 650
		public CubemapFace cubemapFace;

		// Token: 0x0400028B RID: 651
		public int depthSlice;

		// Token: 0x0400028C RID: 652
		public RenderBufferLoadAction[] colorLoad;

		// Token: 0x0400028D RID: 653
		public RenderBufferStoreAction[] colorStore;

		// Token: 0x0400028E RID: 654
		public RenderBufferLoadAction depthLoad;

		// Token: 0x0400028F RID: 655
		public RenderBufferStoreAction depthStore;
	}
}
