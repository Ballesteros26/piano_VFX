using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

// Token: 0x02000004 RID: 4
public class ApplyColorProfile : MonoBehaviour
{
	// Token: 0x06000005 RID: 5 RVA: 0x00002280 File Offset: 0x00000480
	private void Start()
	{
		this.myMainCamera = Camera.main;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002290 File Offset: 0x00000490
	private void Update()
	{
		if (Input.GetMouseButtonDown(1) && this.canDrag)
		{
			this.OnMouseDownFunction();
			this.dragging = true;
			this.UpdatePreview();
			this.UpdateEffects();
		}
		if (Input.GetMouseButtonUp(1))
		{
			this.Snap();
			this.dragging = false;
		}
		if (this.dragging)
		{
			this.OnMouseDragFunction();
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000022E9 File Offset: 0x000004E9
	private void OnMouseEnter()
	{
		this.canDrag = true;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000022F2 File Offset: 0x000004F2
	private void OnMouseExit()
	{
		this.canDrag = false;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000022FC File Offset: 0x000004FC
	private void OnMouseDownFunction()
	{
		this.dragPlane = new Plane(this.myMainCamera.transform.forward, base.transform.position);
		Ray ray = this.myMainCamera.ScreenPointToRay(Input.mousePosition);
		float num;
		this.dragPlane.Raycast(ray, out num);
		this.offset = base.transform.position - ray.GetPoint(num);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002370 File Offset: 0x00000570
	private void OnMouseDragFunction()
	{
		Ray ray = this.myMainCamera.ScreenPointToRay(Input.mousePosition);
		float num;
		this.dragPlane.Raycast(ray, out num);
		base.transform.position = ray.GetPoint(num) + this.offset;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000023BB File Offset: 0x000005BB
	public void Delete()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000023C8 File Offset: 0x000005C8
	public void Snap()
	{
		for (int i = 0; i < this.slots.Length; i++)
		{
			if (Mathf.Abs(base.transform.position.x - this.slots[i].transform.position.x) < 0.75f && Mathf.Abs(base.transform.position.y - this.slots[i].transform.position.y) < 0.75f)
			{
				this.trackIndex = (byte)(i + 1);
				Debug.Log(this.trackIndex);
				base.transform.position = this.slots[i].transform.position;
				base.gameObject.tag = "ColorProfileApplied";
				Debug.Log(base.gameObject.tag);
				return;
			}
		}
		if (base.gameObject.tag == "ColorProfileApplied")
		{
			base.gameObject.tag = "ColorProfile";
			Debug.Log(base.gameObject.tag);
			this.trackIndex = 0;
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000024F0 File Offset: 0x000006F0
	public void ApplyTileColor(string color)
	{
		if (!(color == "red"))
		{
			if (!(color == "green"))
			{
				if (!(color == "blue"))
				{
					if (color == "glow")
					{
						this.tileGlow = this.glowSlider.value;
					}
				}
				else
				{
					this.tileBlue = this.blueSlider.value;
				}
			}
			else
			{
				this.tileGreen = this.greenSlider.value;
			}
		}
		else
		{
			this.tileRed = this.redSlider.value;
		}
		this.tileColor = new Color(this.tileRed, this.tileGreen, this.tileBlue) * this.tileGlow * 130f;
		this.keyColor = new Color(this.tileRed, this.tileGreen, this.tileBlue);
		this.UpdatePreview();
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000025D4 File Offset: 0x000007D4
	public void ApplyEffectColor(string color)
	{
		if (!(color == "red"))
		{
			if (!(color == "green"))
			{
				if (!(color == "blue"))
				{
					if (color == "glow")
					{
						this.effect1Glow = this.e1glowSlider.value;
						this.effect2Glow = this.e2glowSlider.value;
					}
				}
				else
				{
					this.effect1Blue = this.e1blueSlider.value;
					this.effect2Blue = this.e2blueSlider.value;
				}
			}
			else
			{
				this.effect1Green = this.e1greenSlider.value;
				this.effect2Green = this.e2greenSlider.value;
			}
		}
		else
		{
			this.effect1Red = this.e1redSlider.value;
			this.effect2Red = this.e2redSlider.value;
		}
		this.effectColor = new Gradient();
		GradientColorKey[] array = new GradientColorKey[2];
		array[0].color = new Color(this.effect1Red, this.effect1Green, this.effect1Blue) * this.effect1Glow / 5f;
		array[0].time = 0f;
		array[1].color = new Color(this.effect2Red, this.effect2Green, this.effect2Blue) * this.effect2Glow / 5f;
		array[1].time = 0.8f;
		GradientAlphaKey[] array2 = new GradientAlphaKey[3];
		array2[0].alpha = 1f;
		array2[0].time = 0f;
		array2[1].alpha = 0f;
		array2[1].time = 0.8f;
		array2[2].alpha = 0f;
		array2[2].time = 1f;
		this.effectColor.SetKeys(array, array2);
		this.UpdateEffects();
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000027CC File Offset: 0x000009CC
	public void UpdatePreview()
	{
		this.fileManager.GetComponent<FileManager>().lastActiveProfile = base.gameObject;
		this.tileObj.GetComponent<Renderer>().material.SetVector("_GlowColor", this.tileColor);
		this.prevKey.GetComponent<SpriteRenderer>().color = this.keyColor;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000282C File Offset: 0x00000A2C
	private void UpdateEffects()
	{
		this.fire.SetGradient("Gradient", this.effectColor);
		this.dust.SetGradient("Gradient", this.effectColor);
		this.turbulence.SetGradient("Gradient", this.effectColor);
		this.glowBall.SetGradient("Gradient", this.effectColor);
		this.rousseau.SetGradient("Gradient", this.effectColor);
		this.rousseau.SetVector4("Color", this.tileColor);
		this.smoke.SetGradient("Gradient", this.effectColor);
		this.plasma.SetGradient("Gradient", this.effectColor);
		this.patrik.SetGradient("Gradient", this.effectColor);
		this.userEffect.SetGradient("Gradient", this.effectColor);
		this.userEffect.SetVector4("Color", this.tileColor);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002938 File Offset: 0x00000B38
	public string ProfileToJSON()
	{
		return JsonUtility.ToJson(new ColorProfileObject
		{
			tileRed = this.tileRed,
			tileGreen = this.tileGreen,
			tileBlue = this.tileBlue,
			tileGlow = this.tileGlow,
			effectRed1 = this.effect1Red,
			effectGreen1 = this.effect1Green,
			effectBlue1 = this.effect1Blue,
			effectGlow1 = this.effect1Glow,
			effectRed2 = this.effect2Red,
			effectGreen2 = this.effect2Green,
			effectBlue2 = this.effect2Blue,
			effectGlow2 = this.effect2Glow,
			posX = base.transform.position.x,
			posY = base.transform.position.y,
			posZ = base.transform.position.z
		});
	}

	// Token: 0x04000006 RID: 6
	public GameObject[] slots = new GameObject[10];

	// Token: 0x04000007 RID: 7
	private bool dragging;

	// Token: 0x04000008 RID: 8
	private bool canDrag;

	// Token: 0x04000009 RID: 9
	public byte trackIndex;

	// Token: 0x0400000A RID: 10
	private float tileRed;

	// Token: 0x0400000B RID: 11
	private float tileGreen;

	// Token: 0x0400000C RID: 12
	private float tileBlue;

	// Token: 0x0400000D RID: 13
	private float tileGlow = 0.023f;

	// Token: 0x0400000E RID: 14
	private float effect1Red;

	// Token: 0x0400000F RID: 15
	private float effect1Green;

	// Token: 0x04000010 RID: 16
	private float effect1Blue;

	// Token: 0x04000011 RID: 17
	private float effect1Glow;

	// Token: 0x04000012 RID: 18
	private float effect2Red;

	// Token: 0x04000013 RID: 19
	private float effect2Green;

	// Token: 0x04000014 RID: 20
	private float effect2Blue;

	// Token: 0x04000015 RID: 21
	private float effect2Glow;

	// Token: 0x04000016 RID: 22
	public Slider redSlider;

	// Token: 0x04000017 RID: 23
	public Slider greenSlider;

	// Token: 0x04000018 RID: 24
	public Slider blueSlider;

	// Token: 0x04000019 RID: 25
	public Slider glowSlider;

	// Token: 0x0400001A RID: 26
	public Slider e1redSlider;

	// Token: 0x0400001B RID: 27
	public Slider e1greenSlider;

	// Token: 0x0400001C RID: 28
	public Slider e1blueSlider;

	// Token: 0x0400001D RID: 29
	public Slider e1glowSlider;

	// Token: 0x0400001E RID: 30
	public Slider e2redSlider;

	// Token: 0x0400001F RID: 31
	public Slider e2greenSlider;

	// Token: 0x04000020 RID: 32
	public Slider e2blueSlider;

	// Token: 0x04000021 RID: 33
	public Slider e2glowSlider;

	// Token: 0x04000022 RID: 34
	public Color tileColor;

	// Token: 0x04000023 RID: 35
	public Color keyColor;

	// Token: 0x04000024 RID: 36
	public Gradient effectColor;

	// Token: 0x04000025 RID: 37
	public GameObject prevKey;

	// Token: 0x04000026 RID: 38
	public GameObject tileObj;

	// Token: 0x04000027 RID: 39
	public GameObject fileManager;

	// Token: 0x04000028 RID: 40
	public VisualEffect fire;

	// Token: 0x04000029 RID: 41
	public VisualEffect turbulence;

	// Token: 0x0400002A RID: 42
	public VisualEffect dust;

	// Token: 0x0400002B RID: 43
	public VisualEffect glowBall;

	// Token: 0x0400002C RID: 44
	public VisualEffect rousseau;

	// Token: 0x0400002D RID: 45
	public VisualEffect smoke;

	// Token: 0x0400002E RID: 46
	public VisualEffect plasma;

	// Token: 0x0400002F RID: 47
	public VisualEffect patrik;

	// Token: 0x04000030 RID: 48
	public VisualEffect userEffect;

	// Token: 0x04000031 RID: 49
	private Plane dragPlane;

	// Token: 0x04000032 RID: 50
	private Vector3 offset;

	// Token: 0x04000033 RID: 51
	private Camera myMainCamera;
}
