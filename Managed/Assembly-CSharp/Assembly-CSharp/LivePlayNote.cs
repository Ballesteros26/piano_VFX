using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class LivePlayNote : MonoBehaviour
{
	// Token: 0x06000087 RID: 135 RVA: 0x00008B8C File Offset: 0x00006D8C
	public void CopyData()
	{
		SpawnEffect component = base.gameObject.GetComponent<SpawnEffect>();
		SpawnEffect component2 = this.loadObj.GetComponent<SpawnEffect>();
		component.blackKeyDown = component2.blackKeyDown;
		component.blackKeyUp = component2.blackKeyUp;
		component.white = component2.white;
		component.effect = component2.effect;
		component.effect2 = component2.effect2;
		component.light = component2.light;
		component.hitEffect = component2.hitEffect;
		component.effectIndex = component2.effectIndex;
		component.canDeactivate = component2.canDeactivate;
		component.defaultColor = component2.defaultColor;
		component.activeColor = component2.activeColor;
		component.effectColor = component2.effectColor;
		component.GetComponent<Renderer>().material = component2.GetComponent<Renderer>().material;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00008C58 File Offset: 0x00006E58
	private void Update()
	{
		if (this.play)
		{
			base.transform.Translate(Vector2.down * Time.deltaTime * this.speed);
		}
		if (Time.time > this.lastTimeCheck)
		{
			this.lastTimeCheck = Time.time + 10f;
			float num = (base.gameObject.transform.position.y - -3.15f) * 2.11f / 10.529711f;
			if (num - base.gameObject.transform.localScale.y < num / 2f)
			{
				return;
			}
			if (base.transform.position.y > 13f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04000183 RID: 387
	private float speed = 2.5f;

	// Token: 0x04000184 RID: 388
	public bool play;

	// Token: 0x04000185 RID: 389
	public GameObject soundObj;

	// Token: 0x04000186 RID: 390
	public bool moveDown = true;

	// Token: 0x04000187 RID: 391
	private float lastTimeCheck;

	// Token: 0x04000188 RID: 392
	public GameObject loadObj;
}
