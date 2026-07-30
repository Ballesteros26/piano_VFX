using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class ControlDissolve : MonoBehaviour
{
	// Token: 0x06000022 RID: 34 RVA: 0x00003352 File Offset: 0x00001552
	private void Start()
	{
		this.r = base.GetComponent<Renderer>();
		this.r.material.SetFloat("_DissolveAmount", -0.2f);
		this.pms = this.playerObj.GetComponent<PlayMidiSound>();
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000338C File Offset: 0x0000158C
	private void Update()
	{
		if ((double)this.pms.progress.value > 0.9)
		{
			this.isZero = false;
			this.r.material.SetFloat("_DissolveAmount", (1f - this.pms.progress.value) * 20f);
			return;
		}
		if (!this.isZero)
		{
			this.isZero = true;
			this.r.material.SetFloat("_DissolveAmount", -0.2f);
		}
	}

	// Token: 0x04000067 RID: 103
	private Renderer r;

	// Token: 0x04000068 RID: 104
	public GameObject playerObj;

	// Token: 0x04000069 RID: 105
	private PlayMidiSound pms;

	// Token: 0x0400006A RID: 106
	private float duration;

	// Token: 0x0400006B RID: 107
	private bool isZero = true;
}
