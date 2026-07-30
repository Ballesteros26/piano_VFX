using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000029 RID: 41
	public abstract class ImmediateModeElement : VisualElement
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005898 File Offset: 0x00003A98
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000058B0 File Offset: 0x00003AB0
		public bool cullingEnabled
		{
			get
			{
				return this.m_CullingEnabled;
			}
			set
			{
				this.m_CullingEnabled = value;
				base.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000058C6 File Offset: 0x00003AC6
		public ImmediateModeElement()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000058FA File Offset: 0x00003AFA
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			mgc.painter.DrawImmediate(new Action(this.ImmediateRepaint), this.cullingEnabled);
		}

		// Token: 0x060000F0 RID: 240
		protected abstract void ImmediateRepaint();

		// Token: 0x04000071 RID: 113
		private bool m_CullingEnabled = false;
	}
}
