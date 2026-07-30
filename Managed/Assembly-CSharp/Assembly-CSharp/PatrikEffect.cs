using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Token: 0x0200001B RID: 27
public class PatrikEffect : MonoBehaviour
{
	// Token: 0x060000E1 RID: 225 RVA: 0x0000C46C File Offset: 0x0000A66C
	private void Update()
	{
		this.patrikEffects = GameObject.FindGameObjectsWithTag("PatrikEffect");
		this.addedVelocity = 0f;
		foreach (GameObject gameObject in this.patrikEffects)
		{
			if (gameObject != base.gameObject && !gameObject.GetComponent<VisualEffect>().GetBool("Stay"))
			{
				this.processedEffects.Add(gameObject);
				if (Mathf.Abs(gameObject.transform.position.x - base.transform.position.x) < this.maxDist)
				{
					float num = gameObject.transform.position.x - base.transform.position.x;
					this.addedVelocity += num;
				}
			}
		}
		if (this.addedVelocity == 0f)
		{
			base.gameObject.GetComponent<VisualEffect>().SetVector2("RepelX", new Vector2(-0.15f, 0.15f));
			base.gameObject.GetComponent<VisualEffect>().SetVector2("RepelY", new Vector2(0.1f, 0.3f));
			return;
		}
		this.addedVelocity *= -this.repelValue;
		base.gameObject.GetComponent<VisualEffect>().SetVector2("RepelX", new Vector2(-this.addedVelocity / 10f, this.addedVelocity));
		base.gameObject.GetComponent<VisualEffect>().SetVector2("RepelY", new Vector2(0f, 3f * Mathf.Abs(this.addedVelocity)));
		this.RemoveOldEffects();
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x0000C614 File Offset: 0x0000A814
	private void RemoveOldEffects()
	{
		for (int i = 0; i < this.processedEffects.Count; i++)
		{
			if (this.processedEffects[i].GetComponent<VisualEffect>().GetBool("Stay"))
			{
				this.processedEffects.RemoveAt(i);
			}
		}
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x0000C660 File Offset: 0x0000A860
	public float GetDistBetweenPoints(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(x1 - x2), 2f) + Mathf.Pow(Mathf.Abs(y1 - y2), 2f));
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x0000C690 File Offset: 0x0000A890
	public float GetAngleBetweenPoints(float x1, float y1, float x2, float y2)
	{
		float num = x2 - x1;
		return Mathf.Atan2(y2 - y1, num) * 180f / 3.1415927f;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00008854 File Offset: 0x00006A54
	public float Remap(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	// Token: 0x0400025B RID: 603
	private GameObject[] patrikEffects;

	// Token: 0x0400025C RID: 604
	private List<GameObject> processedEffects = new List<GameObject>();

	// Token: 0x0400025D RID: 605
	public float maxDist = 5f;

	// Token: 0x0400025E RID: 606
	public float repelValue = 1f;

	// Token: 0x0400025F RID: 607
	private float addedVelocity;
}
