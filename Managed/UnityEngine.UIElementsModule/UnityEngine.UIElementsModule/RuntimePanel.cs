using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000043 RID: 67
	internal class RuntimePanel : Panel
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00006E0B File Offset: 0x0000500B
		public RuntimePanel(ScriptableObject ownerObject, EventDispatcher dispatcher = null)
			: base(ownerObject, ContextType.Player, dispatcher)
		{
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006E2C File Offset: 0x0000502C
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00006E44 File Offset: 0x00005044
		internal override Shader standardWorldSpaceShader
		{
			get
			{
				return this.m_StandardWorldSpaceShader;
			}
			set
			{
				bool flag = this.m_StandardWorldSpaceShader != value;
				if (flag)
				{
					this.m_StandardWorldSpaceShader = value;
					base.InvokeStandardWorldSpaceShaderChanged();
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006E74 File Offset: 0x00005074
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00006ECC File Offset: 0x000050CC
		internal bool drawToCameras
		{
			get
			{
				UIRRepaintUpdater uirrepaintUpdater = this.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
				bool flag;
				if (uirrepaintUpdater == null)
				{
					flag = false;
				}
				else
				{
					RenderChain renderChain = uirrepaintUpdater.renderChain;
					bool? flag2 = ((renderChain != null) ? new bool?(renderChain.drawInCameras) : default(bool?));
					bool flag3 = true;
					flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
				}
				return flag;
			}
			set
			{
				UIRRepaintUpdater uirrepaintUpdater = this.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
				RenderChain renderChain = ((uirrepaintUpdater != null) ? uirrepaintUpdater.renderChain : null);
				bool flag = renderChain != null;
				if (flag)
				{
					renderChain.drawInCameras = value;
				}
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006F04 File Offset: 0x00005104
		public override void Repaint(Event e)
		{
			bool flag = this.targetTexture == null;
			if (flag)
			{
				base.clearFlags = PanelClearFlags.Depth;
				base.Repaint(e);
			}
			else
			{
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = this.targetTexture;
				base.clearFlags = PanelClearFlags.All;
				base.Repaint(e);
				RenderTexture.active = active;
			}
		}

		// Token: 0x040000CE RID: 206
		private Shader m_StandardWorldSpaceShader;

		// Token: 0x040000CF RID: 207
		internal RenderTexture targetTexture = null;

		// Token: 0x040000D0 RID: 208
		internal Matrix4x4 panelToWorld = Matrix4x4.identity;
	}
}
