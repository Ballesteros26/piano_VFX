using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200005F RID: 95
	public sealed class PropertySheet
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000E3F5 File Offset: 0x0000C5F5
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000E3FD File Offset: 0x0000C5FD
		public MaterialPropertyBlock properties { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000E406 File Offset: 0x0000C606
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000E40E File Offset: 0x0000C60E
		internal Material material { get; private set; }

		// Token: 0x060001C9 RID: 457 RVA: 0x0000E417 File Offset: 0x0000C617
		internal PropertySheet(Material material)
		{
			this.material = material;
			this.properties = new MaterialPropertyBlock();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000E431 File Offset: 0x0000C631
		public void ClearKeywords()
		{
			this.material.shaderKeywords = null;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000E43F File Offset: 0x0000C63F
		public void EnableKeyword(string keyword)
		{
			this.material.EnableKeyword(keyword);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000E44D File Offset: 0x0000C64D
		public void DisableKeyword(string keyword)
		{
			this.material.DisableKeyword(keyword);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000E45B File Offset: 0x0000C65B
		internal void Release()
		{
			RuntimeUtilities.Destroy(this.material);
			this.material = null;
		}
	}
}
