using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200000A RID: 10
	internal class RenderGraphDebugParams
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000024D4 File Offset: 0x000006D4
		public void RegisterDebug()
		{
			List<DebugUI.Widget> list = new List<DebugUI.Widget>();
			list.Add(new DebugUI.BoolField
			{
				displayName = "Enable Render Graph",
				getter = () => this.enableRenderGraph,
				setter = delegate(bool value)
				{
					this.enableRenderGraph = value;
				}
			});
			list.Add(new DebugUI.BoolField
			{
				displayName = "Tag Resources with RG",
				getter = () => this.tagResourceNamesWithRG,
				setter = delegate(bool value)
				{
					this.tagResourceNamesWithRG = value;
				}
			});
			list.Add(new DebugUI.BoolField
			{
				displayName = "Clear Render Targets at creation",
				getter = () => this.clearRenderTargetsAtCreation,
				setter = delegate(bool value)
				{
					this.clearRenderTargetsAtCreation = value;
				}
			});
			list.Add(new DebugUI.BoolField
			{
				displayName = "Clear Render Targets at release",
				getter = () => this.clearRenderTargetsAtRelease,
				setter = delegate(bool value)
				{
					this.clearRenderTargetsAtRelease = value;
				}
			});
			list.Add(new DebugUI.BoolField
			{
				displayName = "Unbind Global Textures",
				getter = () => this.unbindGlobalTextures,
				setter = delegate(bool value)
				{
					this.unbindGlobalTextures = value;
				}
			});
			list.Add(new DebugUI.Button
			{
				displayName = "Log Frame Information",
				action = delegate
				{
					this.logFrameInformation = true;
				}
			});
			list.Add(new DebugUI.Button
			{
				displayName = "Log Resources",
				action = delegate
				{
					this.logResources = true;
				}
			});
			DebugManager.instance.GetPanel("Render Graph", true, 0, false).children.Add(list.ToArray());
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000267B File Offset: 0x0000087B
		public void UnRegisterDebug()
		{
			DebugManager.instance.RemovePanel("Render Graph");
		}

		// Token: 0x04000027 RID: 39
		public bool enableRenderGraph;

		// Token: 0x04000028 RID: 40
		public bool tagResourceNamesWithRG;

		// Token: 0x04000029 RID: 41
		public bool clearRenderTargetsAtCreation;

		// Token: 0x0400002A RID: 42
		public bool clearRenderTargetsAtRelease;

		// Token: 0x0400002B RID: 43
		public bool unbindGlobalTextures;

		// Token: 0x0400002C RID: 44
		public bool logFrameInformation;

		// Token: 0x0400002D RID: 45
		public bool logResources;
	}
}
