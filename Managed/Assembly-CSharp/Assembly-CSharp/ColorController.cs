using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class ColorController : MonoBehaviour
{
	// Token: 0x0600001D RID: 29 RVA: 0x00003190 File Offset: 0x00001390
	public void CreateNewColorProfile()
	{
		GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.colorProfileObj, new Vector2(-18f, 0f), Quaternion.identity);
		gameObject.tag = "ColorProfile";
		gameObject.transform.parent = this.canvas.transform;
		gameObject.transform.localScale = new Vector2(32f, 26f);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00003200 File Offset: 0x00001400
	public void LoadColorProfiles(ColorProfileObject obj)
	{
		GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.colorProfileObj, new Vector3(obj.posX, obj.posY, obj.posZ), Quaternion.identity);
		gameObject.tag = "ColorProfile";
		gameObject.transform.parent = this.canvas.transform;
		gameObject.transform.localScale = new Vector2(32f, 26f);
		ApplyColorProfile component = gameObject.GetComponent<ApplyColorProfile>();
		component.redSlider.value = obj.tileRed;
		component.greenSlider.value = obj.tileGreen;
		component.blueSlider.value = obj.tileBlue;
		component.glowSlider.value = obj.tileGlow;
		component.e1redSlider.value = obj.effectRed1;
		component.e1greenSlider.value = obj.effectGreen1;
		component.e1blueSlider.value = obj.effectBlue1;
		component.e1glowSlider.value = obj.effectGlow1;
		component.e2redSlider.value = obj.effectRed2;
		component.e2greenSlider.value = obj.effectGreen2;
		component.e2blueSlider.value = obj.effectBlue2;
		component.e2glowSlider.value = obj.effectGlow2;
		component.Snap();
	}

	// Token: 0x04000050 RID: 80
	public GameObject colorProfileObj;

	// Token: 0x04000051 RID: 81
	public GameObject canvas;
}
