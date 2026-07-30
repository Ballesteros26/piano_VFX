using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class PostEffect : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, this.material);
	}

	// Token: 0x04000001 RID: 1
	public Material material;
}
