using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

// Token: 0x02000023 RID: 35
public class SpawnEffect : MonoBehaviour
{
	// Token: 0x06000148 RID: 328 RVA: 0x0000FF84 File Offset: 0x0000E184
	public void ApplyColor(byte trackIndex)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("ColorProfileApplied");
		foreach (GameObject gameObject in array)
		{
			if (gameObject.GetComponent<ApplyColorProfile>().trackIndex == trackIndex)
			{
				if (this.allowColorTransition)
				{
					base.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", this.fileManager.GetComponent<FileManager>().transitionColor);
					this.startColor = gameObject.GetComponent<ApplyColorProfile>().tileColor;
				}
				else
				{
					base.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", gameObject.GetComponent<ApplyColorProfile>().tileColor);
				}
				if (this.white)
				{
					this.activeColor = gameObject.GetComponent<ApplyColorProfile>().keyColor;
				}
				else
				{
					this.activeColor = gameObject.GetComponent<ApplyColorProfile>().keyColor;
				}
				foreach (object obj in base.transform)
				{
					Transform transform = (Transform)obj;
					if (this.allowColorTransition)
					{
						transform.GetComponent<Renderer>().material.SetVector("_GlowColor", this.fileManager.GetComponent<FileManager>().transitionColor);
					}
					else
					{
						transform.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", gameObject.GetComponent<ApplyColorProfile>().tileColor);
					}
				}
				this.effectColor = gameObject.GetComponent<ApplyColorProfile>().effectColor;
				return;
			}
		}
		if (array.Length != 0)
		{
			GameObject gameObject2 = array[array.Length - 1];
			if (this.allowColorTransition)
			{
				base.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", this.fileManager.GetComponent<FileManager>().transitionColor);
				this.startColor = gameObject2.GetComponent<ApplyColorProfile>().tileColor;
			}
			else
			{
				base.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", gameObject2.GetComponent<ApplyColorProfile>().tileColor);
			}
			if (this.white)
			{
				this.activeColor = gameObject2.GetComponent<ApplyColorProfile>().keyColor;
			}
			else
			{
				this.activeColor = gameObject2.GetComponent<ApplyColorProfile>().keyColor * 0.6f;
				this.activeColor.a = 1f;
			}
			foreach (object obj2 in base.transform)
			{
				Transform transform2 = (Transform)obj2;
				if (this.allowColorTransition)
				{
					transform2.GetComponent<Renderer>().material.SetVector("_GlowColor", this.fileManager.GetComponent<FileManager>().transitionColor);
				}
				else
				{
					transform2.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", gameObject2.GetComponent<ApplyColorProfile>().tileColor);
				}
			}
			this.effectColor = gameObject2.GetComponent<ApplyColorProfile>().effectColor;
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000102B0 File Offset: 0x0000E4B0
	private void Update()
	{
		if (base.transform.position.y < 2f && this.allowColorTransition && this.moveDown)
		{
			this.t += Time.deltaTime / 1f;
			base.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", Color.Lerp(this.fileManager.GetComponent<FileManager>().transitionColor, this.startColor, this.t));
			using (IEnumerator enumerator = base.transform.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					((Transform)obj).GetComponent<Renderer>().material.SetVector("_GlowColor", Color.Lerp(this.fileManager.GetComponent<FileManager>().transitionColor, this.startColor, this.t));
				}
				return;
			}
		}
		if (base.transform.position.y > 2f && this.allowColorTransition && !this.moveDown)
		{
			this.t += Time.deltaTime / 1f;
			base.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", Color.Lerp(this.startColor, this.fileManager.GetComponent<FileManager>().transitionColor, this.t));
			using (IEnumerator enumerator = base.transform.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj2 = enumerator.Current;
					((Transform)obj2).GetComponent<Renderer>().material.SetVector("_GlowColor", Color.Lerp(this.startColor, this.fileManager.GetComponent<FileManager>().transitionColor, this.t));
				}
				return;
			}
		}
		if (base.transform.position.y > 2f && this.allowColorTransition && this.moveDown)
		{
			this.t = 0f;
			base.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", this.fileManager.GetComponent<FileManager>().transitionColor);
			using (IEnumerator enumerator = base.transform.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj3 = enumerator.Current;
					((Transform)obj3).GetComponent<Renderer>().material.SetVector("_GlowColor", this.fileManager.GetComponent<FileManager>().transitionColor);
				}
				return;
			}
		}
		if (base.transform.position.y < 2f && this.allowColorTransition && !this.moveDown)
		{
			this.t = 0f;
			base.gameObject.GetComponent<Renderer>().material.SetVector("_GlowColor", this.startColor);
			foreach (object obj4 in base.transform)
			{
				((Transform)obj4).GetComponent<Renderer>().material.SetVector("_GlowColor", this.startColor);
			}
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00010660 File Offset: 0x0000E860
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "key" && other.gameObject.GetComponent<SpriteRenderer>().color != this.activeColor)
		{
			if (!this.white)
			{
				other.gameObject.GetComponent<SpriteRenderer>().sprite = this.blackKeyDown;
			}
			other.gameObject.GetComponent<SpriteRenderer>().color = this.activeColor;
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000106D8 File Offset: 0x0000E8D8
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "RedBar")
		{
			if (Mathf.Abs(other.transform.position.y - base.transform.position.y) > 0.5f)
			{
				return;
			}
			this.hudController.GetComponent<HUDController>().IncreaseNotesPlayed();
			this.canDeactivate = false;
			if (this.fileManager.GetComponent<FileManager>().useVirtualLights)
			{
				this.light.SetVector4("LightColor", this.activeColor * this.editorObj.GetComponent<PianoEditor>().lightIntensityValue);
				this.lightClone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.light, new Vector3(base.transform.position.x, base.transform.position.y - this.editorObj.GetComponent<PianoEditor>().lightYPosValue, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
			}
			if (this.fileManager.GetComponent<FileManager>().useHitEffect)
			{
				this.hitEffectClone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.hitEffect, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
				this.hitEffectClone.SetGradient("Gradient", this.effectColor);
			}
			switch (this.effectIndex)
			{
			case 1:
				this.clone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.effect, new Vector3(base.transform.position.x + 1f, base.transform.position.y, base.transform.position.z), Quaternion.Euler(0f, 0f, 90f));
				this.clone.SetGradient("Gradient", this.effectColor);
				break;
			case 2:
				this.turbulenceEffectClone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.turbulenceEffect, new Vector3(base.transform.position.x, base.transform.position.y - 0.25f, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
				this.turbulenceEffectClone.SetGradient("Gradient", this.effectColor);
				break;
			case 3:
				this.clone2 = global::UnityEngine.Object.Instantiate<VisualEffect>(this.effect2, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
				this.clone2.SetGradient("Gradient", this.effectColor);
				break;
			case 4:
				this.glowBallClone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.glowBall, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
				this.glowBallClone.SetGradient("Gradient", this.effectColor);
				break;
			case 5:
				this.rousseauClone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.rousseau, new Vector3(base.transform.position.x, base.transform.position.y + 0.25f, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
				this.rousseauClone.SetGradient("Gradient", this.effectColor);
				this.rousseauClone.SetVector4("Color", base.gameObject.GetComponent<Renderer>().material.GetVector("_GlowColor"));
				break;
			case 6:
				this.smokeClone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.smoke, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
				this.smokeClone.SetGradient("Gradient", this.effectColor);
				break;
			case 7:
				this.plasmaClone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.plasma, new Vector3(base.transform.position.x, base.transform.position.y - 0.25f, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
				this.plasmaClone.SetGradient("Gradient", this.effectColor);
				break;
			case 8:
				this.patrikClone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.patrik, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
				this.patrikClone.SetGradient("Gradient", this.effectColor);
				this.patrikClone.SetVector4("Color", base.gameObject.GetComponent<Renderer>().material.GetVector("_GlowColor"));
				break;
			case 9:
				this.userEffectClone = global::UnityEngine.Object.Instantiate<VisualEffect>(this.userEffect, new Vector3(base.transform.position.x, base.transform.position.y - 0.25f, base.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
				this.userEffectClone.SetGradient("Gradient", this.effectColor);
				this.userEffectClone.SetVector4("Color", base.gameObject.GetComponent<Renderer>().material.GetVector("_GlowColor"));
				break;
			}
			if (this.trails)
			{
				this.trailEffectClone = global::UnityEngine.Object.Instantiate<GameObject>(this.trailEffect, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z), Quaternion.Euler(-90f, 0f, 0f));
			}
			this.soundObj.GetComponent<PlayMidiSound>().PlayMidiEvent((int)this.note, (int)this.velocity, EventName.VoiceNoteOn);
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00010DE8 File Offset: 0x0000EFE8
	private void OnTriggerExit2D(Collider2D other)
	{
		try
		{
			if (other.gameObject.name == "RedBar")
			{
				if (this.lightClone != null)
				{
					this.lightClone.GetComponent<VisualEffect>().Stop();
				}
				if (this.hitEffectClone != null)
				{
					this.hitEffectClone.GetComponent<VisualEffect>().Stop();
				}
				switch (this.effectIndex)
				{
				case 1:
					this.clone.GetComponent<VisualEffect>().Stop();
					break;
				case 2:
					this.turbulenceEffectClone.GetComponent<VisualEffect>().Stop();
					break;
				case 3:
					this.clone2.GetComponent<VisualEffect>().Stop();
					break;
				case 4:
					this.glowBallClone.GetComponent<VisualEffect>().Stop();
					break;
				case 5:
					this.rousseauClone.GetComponent<VisualEffect>().Stop();
					break;
				case 6:
					this.smokeClone.GetComponent<VisualEffect>().Stop();
					break;
				case 7:
					this.plasmaClone.GetComponent<VisualEffect>().Stop();
					break;
				case 8:
					this.patrikClone.SetBool("Stay", true);
					this.patrikClone.GetComponent<VisualEffect>().Stop();
					break;
				case 9:
					this.userEffectClone.GetComponent<VisualEffect>().Stop();
					break;
				}
				this.soundObj.GetComponent<PlayMidiSound>().PlayMidiEvent((int)this.note, (int)this.velocity, EventName.VoiceNoteOff);
				base.StartCoroutine(this.Dest());
			}
			else if (other.gameObject.tag == "key")
			{
				if (!this.white)
				{
					other.gameObject.GetComponent<SpriteRenderer>().sprite = this.blackKeyUp;
				}
				other.gameObject.GetComponent<SpriteRenderer>().color = this.defaultColor;
			}
		}
		catch
		{
			Debug.Log("Effect dest fail.");
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00010FE0 File Offset: 0x0000F1E0
	private IEnumerator Dest()
	{
		yield return new WaitForSeconds(6f);
		if (this.lightClone != null)
		{
			global::UnityEngine.Object.Destroy(this.lightClone.gameObject);
		}
		if (this.hitEffectClone != null)
		{
			global::UnityEngine.Object.Destroy(this.hitEffectClone.gameObject);
		}
		switch (this.effectIndex)
		{
		case 1:
			global::UnityEngine.Object.Destroy(this.clone.gameObject);
			break;
		case 2:
			global::UnityEngine.Object.Destroy(this.turbulenceEffectClone);
			break;
		case 3:
			global::UnityEngine.Object.Destroy(this.clone2.gameObject);
			break;
		case 4:
			global::UnityEngine.Object.Destroy(this.glowBallClone.gameObject);
			break;
		case 5:
			global::UnityEngine.Object.Destroy(this.rousseauClone.gameObject);
			break;
		case 6:
			global::UnityEngine.Object.Destroy(this.smokeClone.gameObject);
			break;
		case 7:
			global::UnityEngine.Object.Destroy(this.plasmaClone.gameObject);
			break;
		case 8:
			global::UnityEngine.Object.Destroy(this.patrikClone.gameObject);
			break;
		case 9:
			global::UnityEngine.Object.Destroy(this.userEffectClone.gameObject);
			break;
		}
		if (this.trails)
		{
			global::UnityEngine.Object.Destroy(this.trailEffectClone);
		}
		this.canDeactivate = true;
		yield break;
	}

	// Token: 0x04000309 RID: 777
	public Sprite blackKeyDown;

	// Token: 0x0400030A RID: 778
	public Sprite blackKeyUp;

	// Token: 0x0400030B RID: 779
	public bool white;

	// Token: 0x0400030C RID: 780
	public VisualEffect effect;

	// Token: 0x0400030D RID: 781
	private VisualEffect clone;

	// Token: 0x0400030E RID: 782
	public VisualEffect effect2;

	// Token: 0x0400030F RID: 783
	private VisualEffect clone2;

	// Token: 0x04000310 RID: 784
	public VisualEffect turbulenceEffect;

	// Token: 0x04000311 RID: 785
	private VisualEffect turbulenceEffectClone;

	// Token: 0x04000312 RID: 786
	public VisualEffect glowBall;

	// Token: 0x04000313 RID: 787
	private VisualEffect glowBallClone;

	// Token: 0x04000314 RID: 788
	public VisualEffect rousseau;

	// Token: 0x04000315 RID: 789
	private VisualEffect rousseauClone;

	// Token: 0x04000316 RID: 790
	public VisualEffect smoke;

	// Token: 0x04000317 RID: 791
	private VisualEffect smokeClone;

	// Token: 0x04000318 RID: 792
	public VisualEffect plasma;

	// Token: 0x04000319 RID: 793
	private VisualEffect plasmaClone;

	// Token: 0x0400031A RID: 794
	public VisualEffect patrik;

	// Token: 0x0400031B RID: 795
	private VisualEffect patrikClone;

	// Token: 0x0400031C RID: 796
	public VisualEffect userEffect;

	// Token: 0x0400031D RID: 797
	private VisualEffect userEffectClone;

	// Token: 0x0400031E RID: 798
	public VisualEffect light;

	// Token: 0x0400031F RID: 799
	private VisualEffect lightClone;

	// Token: 0x04000320 RID: 800
	public VisualEffect hitEffect;

	// Token: 0x04000321 RID: 801
	private VisualEffect hitEffectClone;

	// Token: 0x04000322 RID: 802
	public GameObject trailEffect;

	// Token: 0x04000323 RID: 803
	private GameObject trailEffectClone;

	// Token: 0x04000324 RID: 804
	public int effectIndex;

	// Token: 0x04000325 RID: 805
	public bool canDeactivate = true;

	// Token: 0x04000326 RID: 806
	public Color defaultColor;

	// Token: 0x04000327 RID: 807
	public Color activeColor;

	// Token: 0x04000328 RID: 808
	public bool trails;

	// Token: 0x04000329 RID: 809
	public Gradient effectColor;

	// Token: 0x0400032A RID: 810
	public byte note;

	// Token: 0x0400032B RID: 811
	public byte velocity;

	// Token: 0x0400032C RID: 812
	public GameObject soundObj;

	// Token: 0x0400032D RID: 813
	public bool allowColorTransition;

	// Token: 0x0400032E RID: 814
	public GameObject fileManager;

	// Token: 0x0400032F RID: 815
	public GameObject editorObj;

	// Token: 0x04000330 RID: 816
	private Color startColor;

	// Token: 0x04000331 RID: 817
	public GameObject hudController;

	// Token: 0x04000332 RID: 818
	public bool moveDown;

	// Token: 0x04000333 RID: 819
	private float t;
}
