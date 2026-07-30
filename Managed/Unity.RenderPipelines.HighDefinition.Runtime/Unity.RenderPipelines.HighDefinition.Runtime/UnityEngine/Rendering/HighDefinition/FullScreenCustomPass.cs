using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	internal class FullScreenCustomPass : CustomPass
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x0004976C File Offset: 0x0004796C
		protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			this.fadeValueId = Shader.PropertyToID("_FadeValue");
			if (string.IsNullOrEmpty(this.materialPassName) && this.fullscreenPassMaterial != null)
			{
				this.materialPassName = this.fullscreenPassMaterial.GetPassName(this.materialPassIndex);
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000497BC File Offset: 0x000479BC
		protected override void Execute(ScriptableRenderContext renderContext, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResult)
		{
			if (this.fullscreenPassMaterial != null)
			{
				if (this.fetchColorBuffer)
				{
					base.ResolveMSAAColorBuffer(cmd, hdCamera);
					base.SetRenderTargetAuto(cmd);
				}
				this.fullscreenPassMaterial.SetFloat(this.fadeValueId, base.fadeValue);
				CoreUtils.DrawFullScreen(cmd, this.fullscreenPassMaterial, null, this.fullscreenPassMaterial.FindPass(this.materialPassName));
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00049823 File Offset: 0x00047A23
		public override IEnumerable<Material> RegisterMaterialForInspector()
		{
			yield return this.fullscreenPassMaterial;
			yield break;
		}

		// Token: 0x04000DD0 RID: 3536
		public Material fullscreenPassMaterial;

		// Token: 0x04000DD1 RID: 3537
		[SerializeField]
		private int materialPassIndex;

		// Token: 0x04000DD2 RID: 3538
		public string materialPassName = "Custom Pass 0";

		// Token: 0x04000DD3 RID: 3539
		public bool fetchColorBuffer;

		// Token: 0x04000DD4 RID: 3540
		private int fadeValueId;
	}
}
