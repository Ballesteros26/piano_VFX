using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

// Token: 0x02000018 RID: 24
public class ParticleEditor : MonoBehaviour
{
	// Token: 0x06000099 RID: 153 RVA: 0x0000A27C File Offset: 0x0000847C
	private void Start()
	{
		if (PlayerPrefs.GetString("ParticleEffects").Length > 0)
		{
			this.particleSlots = JsonUtility.FromJson<ParticleSlots>(PlayerPrefs.GetString("ParticleEffects"));
		}
		else
		{
			this.particleSlots = new ParticleSlots();
			this.particleSlots.slot1 = new ParticleObject("Empty Slot").GetJSON();
			this.particleSlots.slot2 = new ParticleObject("Empty Slot").GetJSON();
			this.particleSlots.slot3 = new ParticleObject("Empty Slot").GetJSON();
			this.particleSlots.slot4 = new ParticleObject("Empty Slot").GetJSON();
			this.particleSlots.slot5 = new ParticleObject("Empty Slot").GetJSON();
			this.particleSlots.slot6 = new ParticleObject("Empty Slot").GetJSON();
			this.particleSlots.slot7 = new ParticleObject("Empty Slot").GetJSON();
			this.particleSlots.slot8 = new ParticleObject("Empty Slot").GetJSON();
			this.particleSlots.slot9 = new ParticleObject("Empty Slot").GetJSON();
			this.particleSlots.slot10 = new ParticleObject("Empty Slot").GetJSON();
		}
		this.LoadParticleEffectNames();
	}

	// Token: 0x0600009A RID: 154 RVA: 0x0000A3CC File Offset: 0x000085CC
	public void LoadParticleEffectNames()
	{
		this.userParticleEffectsDropdown.ClearOptions();
		for (int i = 0; i < 10; i++)
		{
			string text;
			switch (i)
			{
			case 0:
				text = this.particleSlots.slot1;
				break;
			case 1:
				text = this.particleSlots.slot2;
				break;
			case 2:
				text = this.particleSlots.slot3;
				break;
			case 3:
				text = this.particleSlots.slot4;
				break;
			case 4:
				text = this.particleSlots.slot5;
				break;
			case 5:
				text = this.particleSlots.slot6;
				break;
			case 6:
				text = this.particleSlots.slot7;
				break;
			case 7:
				text = this.particleSlots.slot8;
				break;
			case 8:
				text = this.particleSlots.slot9;
				break;
			default:
				text = this.particleSlots.slot10;
				break;
			}
			ParticleObject particleObject = JsonUtility.FromJson<ParticleObject>(text);
			string text2;
			if (particleObject == null)
			{
				text2 = "Empty Slot";
			}
			else
			{
				text2 = particleObject.name;
			}
			this.userParticleEffectsDropdown.options.Add(new Dropdown.OptionData
			{
				text = text2
			});
			this.userParticleEffectsDropdown.RefreshShownValue();
		}
		this.userParticleEffectsDropdown.value = this.particleSlots.selectedParticleSlot;
		this.currentParticleSlot = this.particleSlots.selectedParticleSlot;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000A514 File Offset: 0x00008714
	public void SaveUserParticleEffects(int selectedEffects, string name)
	{
		this.particleEffectName = name;
		string text = this.SaveParticleValues();
		switch (this.particleSlots.selectedParticleSlot)
		{
		case 0:
			this.particleSlots.slot1 = text;
			break;
		case 1:
			this.particleSlots.slot2 = text;
			break;
		case 2:
			this.particleSlots.slot3 = text;
			break;
		case 3:
			this.particleSlots.slot4 = text;
			break;
		case 4:
			this.particleSlots.slot5 = text;
			break;
		case 5:
			this.particleSlots.slot6 = text;
			break;
		case 6:
			this.particleSlots.slot7 = text;
			break;
		case 7:
			this.particleSlots.slot8 = text;
			break;
		case 8:
			this.particleSlots.slot9 = text;
			break;
		default:
			this.particleSlots.slot10 = text;
			break;
		}
		this.particleSlots.selectedParticleSlot = selectedEffects;
		PlayerPrefs.SetString("ParticleEffects", JsonUtility.ToJson(this.particleSlots));
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000A614 File Offset: 0x00008814
	public void LoadUserParticleEffects()
	{
		if (this.particleSlots.selectedParticleSlot == this.userParticleEffectsDropdown.value)
		{
			return;
		}
		string text;
		switch (this.userParticleEffectsDropdown.value)
		{
		case 0:
			text = this.particleSlots.slot1;
			break;
		case 1:
			text = this.particleSlots.slot2;
			break;
		case 2:
			text = this.particleSlots.slot3;
			break;
		case 3:
			text = this.particleSlots.slot4;
			break;
		case 4:
			text = this.particleSlots.slot5;
			break;
		case 5:
			text = this.particleSlots.slot6;
			break;
		case 6:
			text = this.particleSlots.slot7;
			break;
		case 7:
			text = this.particleSlots.slot8;
			break;
		case 8:
			text = this.particleSlots.slot9;
			break;
		default:
			text = this.particleSlots.slot10;
			break;
		}
		JsonUtility.FromJson<ProjectObject>(text);
		this.SaveUserParticleEffects(this.userParticleEffectsDropdown.value, this.userParticleEffectsDropdown.options[this.particleSlots.selectedParticleSlot].text);
		this.currentParticleSlot = this.userParticleEffectsDropdown.value;
		this.RestoreParticleValues(text);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000A74C File Offset: 0x0000894C
	public void ChangeEffectName()
	{
		if (this.userParticleNameInputField.text.Length == 0)
		{
			this.userParticleNameInputField.text = "Empty Slot";
		}
		this.userParticleEffectsDropdown.options[this.particleSlots.selectedParticleSlot].text = this.userParticleNameInputField.text;
		this.userParticleEffectsDropdown.RefreshShownValue();
		this.SaveUserParticleEffects(this.particleSlots.selectedParticleSlot, this.userParticleEffectsDropdown.options[this.particleSlots.selectedParticleSlot].text);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000A7E2 File Offset: 0x000089E2
	public void OpenMainEffect()
	{
		this.mainEffectObj.SetActive(true);
		this.childEffectObj.SetActive(false);
		this.fogEffectObj.SetActive(false);
		this.trailEffectObj.SetActive(false);
	}

	// Token: 0x0600009F RID: 159 RVA: 0x0000A814 File Offset: 0x00008A14
	public void OpenChildEffect()
	{
		this.mainEffectObj.SetActive(false);
		this.childEffectObj.SetActive(true);
		this.fogEffectObj.SetActive(false);
		this.trailEffectObj.SetActive(false);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000A846 File Offset: 0x00008A46
	public void OpenFogEffect()
	{
		this.mainEffectObj.SetActive(false);
		this.childEffectObj.SetActive(false);
		this.fogEffectObj.SetActive(true);
		this.trailEffectObj.SetActive(false);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000A878 File Offset: 0x00008A78
	public void OpenTrailEffect()
	{
		this.mainEffectObj.SetActive(false);
		this.childEffectObj.SetActive(false);
		this.fogEffectObj.SetActive(false);
		this.trailEffectObj.SetActive(true);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000A8AC File Offset: 0x00008AAC
	public void ChangeMainRate()
	{
		this.mainRateText.text = "S Rate: " + Math.Round((double)this.mainRateSlider.value, 2);
		this.userEffect.SetFloat("Rate", this.mainRateSlider.value);
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000A900 File Offset: 0x00008B00
	public void ChangeMainVel1X()
	{
		Vector3 vector = this.userEffect.GetVector3("Velocity1");
		this.mainVel1XText.text = "Vel1 X: " + Math.Round((double)this.mainVel1XSlider.value, 2);
		this.userEffect.SetVector3("Velocity1", new Vector3(this.mainVel1XSlider.value, vector.y, vector.z));
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x0000A978 File Offset: 0x00008B78
	public void ChangeMainVel2X()
	{
		Vector3 vector = this.userEffect.GetVector3("Velocity2");
		this.mainVel2XText.text = "Vel2 X: " + Math.Round((double)this.mainVel2XSlider.value, 2);
		this.userEffect.SetVector3("Velocity2", new Vector3(this.mainVel2XSlider.value, vector.y, vector.z));
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x0000A9F0 File Offset: 0x00008BF0
	public void ChangeMainVel1Y()
	{
		Vector3 vector = this.userEffect.GetVector3("Velocity1");
		this.mainVel1YText.text = "Vel1 Y: " + Math.Round((double)this.mainVel1YSlider.value, 2);
		this.userEffect.SetVector3("Velocity1", new Vector3(vector.x, this.mainVel1YSlider.value, vector.z));
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000AA68 File Offset: 0x00008C68
	public void ChangeMainVel2Y()
	{
		Vector3 vector = this.userEffect.GetVector3("Velocity2");
		this.mainVel2YText.text = "Vel2 Y: " + Math.Round((double)this.mainVel2YSlider.value, 2);
		this.userEffect.SetVector3("Velocity2", new Vector3(vector.x, this.mainVel2YSlider.value, vector.z));
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x0000AAE0 File Offset: 0x00008CE0
	public void ChangeMainLifetime1()
	{
		this.mainLifetime1Text.text = "Lifetime1: " + Math.Round((double)this.mainLifetime1Slider.value, 2);
		this.userEffect.SetFloat("Lifetime1", this.mainLifetime1Slider.value);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000AB34 File Offset: 0x00008D34
	public void ChangeMainLifetime2()
	{
		this.mainLifetime2Text.text = "Lifetime2: " + Math.Round((double)this.mainLifetime2Slider.value, 2);
		this.userEffect.SetFloat("Lifetime2", this.mainLifetime2Slider.value);
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000AB88 File Offset: 0x00008D88
	public void ChangeMainSpawnRad()
	{
		this.mainSpawnRadText.text = "Spawn Rad: " + Math.Round((double)this.mainSpawnRadSlider.value, 2);
		this.userEffect.SetVector3("Width", new Vector3(this.mainSpawnRadSlider.value, 0f, 0f));
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000ABEC File Offset: 0x00008DEC
	public void ChangeMainTurbulence()
	{
		this.mainTurbulenceText.text = "Turbulence: " + Math.Round((double)this.mainTurbulenceSlider.value, 2);
		this.userEffect.SetFloat("TurbulenceIntensity", this.mainTurbulenceSlider.value);
	}

	// Token: 0x060000AB RID: 171 RVA: 0x0000AC40 File Offset: 0x00008E40
	public void ChangeMainTOctaves()
	{
		this.mainTOctavesText.text = "T Octaves: " + Math.Round((double)this.mainTOctavesSlider.value, 2);
		this.userEffect.SetFloat("TurbulenceOctavesMain", this.mainTOctavesSlider.value);
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000AC94 File Offset: 0x00008E94
	public void ChangeMainTDrag()
	{
		this.mainTDragText.text = "T Drag: " + Math.Round((double)this.mainTDragSlider.value, 2);
		this.userEffect.SetFloat("TurbulenceDrag", this.mainTDragSlider.value);
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000ACE8 File Offset: 0x00008EE8
	public void ChangeMainTFrequency()
	{
		this.mainTFrequencyText.text = "T Frequency: " + Math.Round((double)this.mainTFrequencySlider.value, 2);
		this.userEffect.SetFloat("TurbulenceFrequency", this.mainTFrequencySlider.value);
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0000AD3C File Offset: 0x00008F3C
	public void ChangeMainChildSR()
	{
		this.mainChildSRText.text = "S Rate: " + Math.Round((double)this.mainChildSRSlider.value, 2);
		this.userEffect.SetFloat("EventSpawnRate", this.mainChildSRSlider.value);
	}

	// Token: 0x060000AF RID: 175 RVA: 0x0000AD90 File Offset: 0x00008F90
	public void ChangeMainSize()
	{
		this.mainSizeText.text = "Size: " + Math.Round((double)this.mainSizeSlider.value, 2);
		this.userEffect.SetFloat("Size", this.mainSizeSlider.value);
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000ADE4 File Offset: 0x00008FE4
	public void ChangeMainNoise()
	{
		this.mainNoiseText.text = "Noise: " + Math.Round((double)this.mainNoiseSlider.value, 2);
		this.userEffect.SetFloat("NoiseFrequency", this.mainNoiseSlider.value);
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0000AE38 File Offset: 0x00009038
	public void ChangeMainGravity()
	{
		this.mainGravityText.text = "Gravity: " + Math.Round((double)this.mainGravitySlider.value, 2);
		this.userEffect.SetFloat("Gravity", this.mainGravitySlider.value);
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x0000AE8C File Offset: 0x0000908C
	public void ChangeChildVel1X()
	{
		Vector3 vector = this.userEffect.GetVector3("ChildVelocity1");
		this.childVel1XText.text = "Vel1 X: " + Math.Round((double)this.childVel1XSlider.value, 2);
		this.userEffect.SetVector3("ChildVelocity1", new Vector3(this.childVel1XSlider.value, vector.y, vector.z));
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0000AF04 File Offset: 0x00009104
	public void ChangeChildVel2X()
	{
		Vector3 vector = this.userEffect.GetVector3("ChildVelocity2");
		this.childVel2XText.text = "Vel2 X: " + Math.Round((double)this.childVel2XSlider.value, 2);
		this.userEffect.SetVector3("ChildVelocity2", new Vector3(this.childVel2XSlider.value, vector.y, vector.z));
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x0000AF7C File Offset: 0x0000917C
	public void ChangeChildVel1Y()
	{
		Vector3 vector = this.userEffect.GetVector3("ChildVelocity1");
		this.childVel1YText.text = "Vel1 Y: " + Math.Round((double)this.childVel1YSlider.value, 2);
		this.userEffect.SetVector3("ChildVelocity1", new Vector3(vector.x, this.childVel1YSlider.value, vector.z));
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x0000AFF4 File Offset: 0x000091F4
	public void ChangeChildVel2Y()
	{
		Vector3 vector = this.userEffect.GetVector3("ChildVelocity2");
		this.childVel2YText.text = "Vel2 Y: " + Math.Round((double)this.childVel2YSlider.value, 2);
		this.userEffect.SetVector3("ChildVelocity2", new Vector3(vector.x, this.childVel2YSlider.value, vector.z));
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x0000B06C File Offset: 0x0000926C
	public void ChangeChildLifetime1()
	{
		this.childLifetime1Text.text = "Lifetime1: " + Math.Round((double)this.childLifetime1Slider.value, 2);
		this.userEffect.SetFloat("ChildLifetime1", this.childLifetime1Slider.value);
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x0000B0C0 File Offset: 0x000092C0
	public void ChangeChildLifetime2()
	{
		this.childLifetime2Text.text = "Lifetime2: " + Math.Round((double)this.childLifetime2Slider.value, 2);
		this.userEffect.SetFloat("ChildLifetime2", this.childLifetime2Slider.value);
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x0000B114 File Offset: 0x00009314
	public void ChangeChildSpawnRad()
	{
		this.childSpawnRadText.text = "Spawn Rad: " + Math.Round((double)this.childSpawnRadSlider.value, 2);
		this.userEffect.SetFloat("ChildNoiseRadiusFrom", this.childSpawnRadSlider.value);
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000B168 File Offset: 0x00009368
	public void ChangeChildTurbulence()
	{
		this.childTurbulenceText.text = "Turbulence: " + Math.Round((double)this.childTurbulenceSlider.value, 2);
		this.userEffect.SetFloat("ChildTurbulenceItensity", this.childTurbulenceSlider.value);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000B1BC File Offset: 0x000093BC
	public void ChangeChildTOctaves()
	{
		this.childTOctavesText.text = "T Octaves: " + Math.Round((double)this.childTOctavesSlider.value, 2);
		this.userEffect.SetFloat("ChildTurbulenceOctaves", this.childTOctavesSlider.value);
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000B210 File Offset: 0x00009410
	public void ChangeChildTDrag()
	{
		this.childTDragText.text = "T Drag: " + Math.Round((double)this.childTDragSlider.value, 2);
		this.userEffect.SetFloat("ChildTurbulenceDrag", this.childTDragSlider.value);
	}

	// Token: 0x060000BC RID: 188 RVA: 0x0000B264 File Offset: 0x00009464
	public void ChangeChildTFrequency()
	{
		this.childTFrequencyText.text = "T Frequency: " + Math.Round((double)this.childTFrequencySlider.value, 2);
		this.userEffect.SetFloat("ChildTurbulenceFrequency", this.childTFrequencySlider.value);
	}

	// Token: 0x060000BD RID: 189 RVA: 0x0000B2B8 File Offset: 0x000094B8
	public void ChangeChildSize()
	{
		this.childSizeText.text = "Size: " + Math.Round((double)this.childSizeSlider.value, 2);
		this.userEffect.SetFloat("ChildSize", this.childSizeSlider.value);
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000B30C File Offset: 0x0000950C
	public void ChangeChildNoise()
	{
		this.childNoiseText.text = "Noise: " + Math.Round((double)this.childNoiseSlider.value, 2);
		this.userEffect.SetFloat("ChildNoiseFrequency", this.childNoiseSlider.value);
	}

	// Token: 0x060000BF RID: 191 RVA: 0x0000B360 File Offset: 0x00009560
	public void ChangeChildGravity()
	{
		this.childGravityText.text = "Gravity: " + Math.Round((double)this.childGravitySlider.value, 2);
		this.userEffect.SetFloat("ChildGravity", this.childGravitySlider.value);
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0000B3B4 File Offset: 0x000095B4
	public void ChangeFogRate()
	{
		this.fogRateText.text = "S Rate: " + Math.Round((double)this.fogRateSlider.value, 2);
		this.userEffect.SetFloat("FogRate", this.fogRateSlider.value);
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000B408 File Offset: 0x00009608
	public void ChangeFogVel1X()
	{
		Vector3 vector = this.userEffect.GetVector3("FogVelocity1");
		this.fogVel1XText.text = "Vel1 X: " + Math.Round((double)this.fogVel1XSlider.value, 2);
		this.userEffect.SetVector3("FogVelocity1", new Vector3(this.fogVel1XSlider.value, vector.y, vector.z));
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000B480 File Offset: 0x00009680
	public void ChangeFogVel2X()
	{
		Vector3 vector = this.userEffect.GetVector3("FogVelocity2");
		this.fogVel2XText.text = "Vel2 X: " + Math.Round((double)this.fogVel2XSlider.value, 2);
		this.userEffect.SetVector3("FogVelocity2", new Vector3(this.fogVel2XSlider.value, vector.y, vector.z));
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000B4F8 File Offset: 0x000096F8
	public void ChangeFogVel1Y()
	{
		Vector3 vector = this.userEffect.GetVector3("FogVelocity1");
		this.fogVel1YText.text = "Vel1 Y: " + Math.Round((double)this.fogVel1YSlider.value, 2);
		this.userEffect.SetVector3("FogVelocity1", new Vector3(vector.x, this.fogVel1YSlider.value, vector.z));
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000B570 File Offset: 0x00009770
	public void ChangeFogVel2Y()
	{
		Vector3 vector = this.userEffect.GetVector3("FogVelocity2");
		this.fogVel2YText.text = "Vel2 Y: " + Math.Round((double)this.fogVel2YSlider.value, 2);
		this.userEffect.SetVector3("FogVelocity2", new Vector3(vector.x, this.fogVel2YSlider.value, vector.z));
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000B5E8 File Offset: 0x000097E8
	public void ChangeFogLifetime1()
	{
		this.fogLifetime1Text.text = "Lifetime1: " + Math.Round((double)this.fogLifetime1Slider.value, 2);
		this.userEffect.SetFloat("FogLifetime1", this.fogLifetime1Slider.value);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x0000B63C File Offset: 0x0000983C
	public void ChangeFogLifetime2()
	{
		this.fogLifetime2Text.text = "Lifetime2: " + Math.Round((double)this.fogLifetime2Slider.value, 2);
		this.userEffect.SetFloat("FogLifetime2", this.fogLifetime2Slider.value);
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x0000B690 File Offset: 0x00009890
	public void ChangeFogSpawnRad()
	{
		this.fogSpawnRadText.text = "Spawn Rad: " + Math.Round((double)this.fogSpawnRadSlider.value, 2);
		this.userEffect.SetFloat("FogSize", this.fogSpawnRadSlider.value);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0000B6E4 File Offset: 0x000098E4
	public void ChangeTrailRate()
	{
		this.trailRateText.text = "S Rate: " + Math.Round((double)this.trailRateSlider.value, 2);
		this.userEffect.SetFloat("TrailRate", this.trailRateSlider.value);
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x0000B738 File Offset: 0x00009938
	public void ChangeTrailVel1X()
	{
		Vector3 vector = this.userEffect.GetVector3("TrailVelocity1");
		this.trailVel1XText.text = "Vel1 X: " + Math.Round((double)this.trailVel1XSlider.value, 2);
		this.userEffect.SetVector3("TrailVelocity1", new Vector3(this.trailVel1XSlider.value, vector.y, vector.z));
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0000B7B0 File Offset: 0x000099B0
	public void ChangeTrailVel2X()
	{
		Vector3 vector = this.userEffect.GetVector3("TrailVelocity2");
		this.trailVel2XText.text = "Vel2 X: " + Math.Round((double)this.trailVel2XSlider.value, 2);
		this.userEffect.SetVector3("TrailVelocity2", new Vector3(this.trailVel2XSlider.value, vector.y, vector.z));
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000B828 File Offset: 0x00009A28
	public void ChangeTrailVel1Y()
	{
		Vector3 vector = this.userEffect.GetVector3("TrailVelocity1");
		this.trailVel1YText.text = "Vel1 Y: " + Math.Round((double)this.trailVel1YSlider.value, 2);
		this.userEffect.SetVector3("TrailVelocity1", new Vector3(vector.x, this.trailVel1YSlider.value, vector.z));
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000B8A0 File Offset: 0x00009AA0
	public void ChangeTrailVel2Y()
	{
		Vector3 vector = this.userEffect.GetVector3("TrailVelocity2");
		this.trailVel2YText.text = "Vel2 Y: " + Math.Round((double)this.trailVel2YSlider.value, 2);
		this.userEffect.SetVector3("TrailVelocity2", new Vector3(vector.x, this.trailVel2YSlider.value, vector.z));
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0000B918 File Offset: 0x00009B18
	public void ChangeTrailLifetime1()
	{
		this.trailLifetime1Text.text = "Lifetime1: " + Math.Round((double)this.trailLifetime1Slider.value, 2);
		this.userEffect.SetFloat("TrailLifetime1", this.trailLifetime1Slider.value);
	}

	// Token: 0x060000CE RID: 206 RVA: 0x0000B96C File Offset: 0x00009B6C
	public void ChangeTrailLifetime2()
	{
		this.trailLifetime2Text.text = "Lifetime2: " + Math.Round((double)this.trailLifetime2Slider.value, 2);
		this.userEffect.SetFloat("TrailLifetime2", this.trailLifetime2Slider.value);
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0000B9C0 File Offset: 0x00009BC0
	public void ChangeTrailTurbulence()
	{
		this.trailTurbulenceText.text = "Turbulence: " + Math.Round((double)this.trailTurbulenceSlider.value, 2);
		this.userEffect.SetFloat("TrailTurbulenceItensity", this.trailTurbulenceSlider.value);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0000BA14 File Offset: 0x00009C14
	public void ChangeTrailLength()
	{
		this.trailLengthText.text = "Length: " + Math.Round((double)this.trailLengthSlider.value, 2);
		this.userEffect.SetFloat("TrailLenght", this.trailLengthSlider.value);
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x0000BA68 File Offset: 0x00009C68
	public string SaveParticleValues()
	{
		return JsonUtility.ToJson(new ParticleObject(this.particleEffectName)
		{
			mainRate = this.mainRateSlider.value,
			mainVel1X = this.mainVel1XSlider.value,
			mainVel2X = this.mainVel2XSlider.value,
			mainVel1Y = this.mainVel1YSlider.value,
			mainVel2Y = this.mainVel2YSlider.value,
			mainLifetime1 = this.mainLifetime1Slider.value,
			mainLifetime2 = this.mainLifetime2Slider.value,
			mainSpawnRad = this.mainSpawnRadSlider.value,
			mainTurbulence = this.mainTurbulenceSlider.value,
			mainTOctaves = this.mainTOctavesSlider.value,
			mainTDrag = this.mainTDragSlider.value,
			mainTFrequency = this.mainTFrequencySlider.value,
			mainSize = this.mainSizeSlider.value,
			mainNoise = this.mainNoiseSlider.value,
			mainGravity = this.mainGravitySlider.value,
			childRate = this.mainChildSRSlider.value,
			childVel1X = this.childVel1XSlider.value,
			childVel2X = this.childVel2XSlider.value,
			childVel1Y = this.childVel1YSlider.value,
			childVel2Y = this.childVel2YSlider.value,
			childLifetime1 = this.childLifetime1Slider.value,
			childLifetime2 = this.childLifetime2Slider.value,
			childSpawnRad = this.childSpawnRadSlider.value,
			childTurbulence = this.childTurbulenceSlider.value,
			childTOctaves = this.childTOctavesSlider.value,
			childTDrag = this.childTDragSlider.value,
			childTFrequency = this.childTFrequencySlider.value,
			childSize = this.childSizeSlider.value,
			childNoise = this.childNoiseSlider.value,
			childGravity = this.childGravitySlider.value,
			fogRate = this.fogRateSlider.value,
			fogVel1X = this.fogVel1XSlider.value,
			fogVel2X = this.fogVel2XSlider.value,
			fogVel1Y = this.fogVel1YSlider.value,
			fogVel2Y = this.fogVel2YSlider.value,
			fogLifetime1 = this.fogLifetime1Slider.value,
			fogLifetime2 = this.fogLifetime2Slider.value,
			fogSpawnRad = this.fogSpawnRadSlider.value,
			trailRate = this.trailRateSlider.value,
			trailVel1X = this.trailVel1XSlider.value,
			trailVel2X = this.trailVel2XSlider.value,
			trailVel1Y = this.trailVel1YSlider.value,
			trailVel2Y = this.trailVel2YSlider.value,
			trailLifetime1 = this.trailLifetime1Slider.value,
			trailLifetime2 = this.trailLifetime2Slider.value,
			trailTurbulence = this.trailTurbulenceSlider.value,
			trailLength = this.trailLengthSlider.value
		});
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000BDA4 File Offset: 0x00009FA4
	public void RestoreParticleValues(string json)
	{
		ParticleObject particleObject = new ParticleObject("");
		particleObject = JsonUtility.FromJson<ParticleObject>(json);
		this.particleEffectName = particleObject.name;
		this.mainRateSlider.value = particleObject.mainRate;
		this.mainVel1XSlider.value = particleObject.mainVel1X;
		this.mainVel2XSlider.value = particleObject.mainVel2X;
		this.mainVel1YSlider.value = particleObject.mainVel1Y;
		this.mainVel2YSlider.value = particleObject.mainVel2Y;
		this.mainLifetime1Slider.value = particleObject.mainLifetime1;
		this.mainLifetime2Slider.value = particleObject.mainLifetime2;
		this.mainSpawnRadSlider.value = particleObject.mainSpawnRad;
		this.mainTurbulenceSlider.value = particleObject.mainTurbulence;
		this.mainTOctavesSlider.value = particleObject.mainTOctaves;
		this.mainTDragSlider.value = particleObject.mainTDrag;
		this.mainTFrequencySlider.value = particleObject.mainTFrequency;
		this.mainSizeSlider.value = particleObject.mainSize;
		this.mainNoiseSlider.value = particleObject.mainNoise;
		this.mainGravitySlider.value = particleObject.mainGravity;
		this.mainChildSRSlider.value = particleObject.childRate;
		this.childVel1XSlider.value = particleObject.childVel1X;
		this.childVel2XSlider.value = particleObject.childVel2X;
		this.childVel1YSlider.value = particleObject.childVel1Y;
		this.childVel2YSlider.value = particleObject.childVel2Y;
		this.childLifetime1Slider.value = particleObject.childLifetime1;
		this.childLifetime2Slider.value = particleObject.childLifetime2;
		this.childSpawnRadSlider.value = particleObject.childSpawnRad;
		this.childTurbulenceSlider.value = particleObject.childTurbulence;
		this.childTOctavesSlider.value = particleObject.childTOctaves;
		this.childTDragSlider.value = particleObject.childTDrag;
		this.childTFrequencySlider.value = particleObject.childTFrequency;
		this.childSizeSlider.value = particleObject.childSize;
		this.childNoiseSlider.value = particleObject.childNoise;
		this.childGravitySlider.value = particleObject.childGravity;
		this.fogRateSlider.value = particleObject.fogRate;
		this.fogVel1XSlider.value = particleObject.fogVel1X;
		this.fogVel2XSlider.value = particleObject.fogVel2X;
		this.fogVel1YSlider.value = particleObject.fogVel1Y;
		this.fogVel2YSlider.value = particleObject.fogVel2Y;
		this.fogLifetime1Slider.value = particleObject.fogLifetime1;
		this.fogLifetime2Slider.value = particleObject.fogLifetime2;
		this.fogSpawnRadSlider.value = particleObject.fogSpawnRad;
		this.trailRateSlider.value = particleObject.trailRate;
		this.trailVel1XSlider.value = particleObject.trailVel1X;
		this.trailVel2XSlider.value = particleObject.trailVel2X;
		this.trailVel1YSlider.value = particleObject.trailVel1Y;
		this.trailVel2YSlider.value = particleObject.trailVel2Y;
		this.trailLifetime1Slider.value = particleObject.trailLifetime1;
		this.trailLifetime2Slider.value = particleObject.trailLifetime2;
		this.trailTurbulenceSlider.value = particleObject.trailTurbulence;
		this.trailLengthSlider.value = particleObject.trailLength;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000C0EE File Offset: 0x0000A2EE
	public void BackButtonSaveEffects()
	{
		this.SaveUserParticleEffects(this.userParticleEffectsDropdown.value, this.userParticleEffectsDropdown.options[this.particleSlots.selectedParticleSlot].text);
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000C124 File Offset: 0x0000A324
	public void LoadLastEffect()
	{
		string text;
		switch (this.particleSlots.selectedParticleSlot)
		{
		case 0:
			text = this.particleSlots.slot1;
			break;
		case 1:
			text = this.particleSlots.slot2;
			break;
		case 2:
			text = this.particleSlots.slot3;
			break;
		case 3:
			text = this.particleSlots.slot4;
			break;
		case 4:
			text = this.particleSlots.slot5;
			break;
		case 5:
			text = this.particleSlots.slot6;
			break;
		case 6:
			text = this.particleSlots.slot7;
			break;
		case 7:
			text = this.particleSlots.slot8;
			break;
		case 8:
			text = this.particleSlots.slot9;
			break;
		default:
			text = this.particleSlots.slot10;
			break;
		}
		JsonUtility.FromJson<ProjectObject>(text);
		this.RestoreParticleValues(text);
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x0000C204 File Offset: 0x0000A404
	public void GetEffectString()
	{
		string text = this.SaveParticleValues();
		Clipboard.SetText(this.EncodeTo64(text));
		this.effectStringInputField.text = "Effect string copied to clipboard.";
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000C234 File Offset: 0x0000A434
	public void SetEffectString()
	{
		if (this.effectStringInputField.text.Length > 100)
		{
			try
			{
				string text = this.DecodeFrom64(this.effectStringInputField.text);
				this.RestoreParticleValues(text);
				this.effectStringInputField.text = "Effect string accepted.";
				return;
			}
			catch (Exception)
			{
				this.effectStringInputField.text = "Effect string is invalid.";
				return;
			}
		}
		this.effectStringInputField.text = "Effect string is invalid.";
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
	public void TestStringEffect()
	{
		string text = this.SaveParticleValues();
		string text2 = this.EncodeTo64(text);
		Debug.Log(text2);
		Debug.Log(this.DecodeFrom64(text2));
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000C2E2 File Offset: 0x0000A4E2
	private string EncodeTo64(string toEncode)
	{
		return Convert.ToBase64String(ParticleEditor.Zip(toEncode));
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000C2EF File Offset: 0x0000A4EF
	private string DecodeFrom64(string encodedData)
	{
		return ParticleEditor.Unzip(Convert.FromBase64String(encodedData));
	}

	// Token: 0x060000DA RID: 218 RVA: 0x0000C2FC File Offset: 0x0000A4FC
	public static void CopyTo(Stream src, Stream dest)
	{
		byte[] array = new byte[4096];
		int num;
		while ((num = src.Read(array, 0, array.Length)) != 0)
		{
			dest.Write(array, 0, num);
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000C330 File Offset: 0x0000A530
	public static byte[] Zip(string str)
	{
		byte[] array;
		using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(str)))
		{
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream2, CompressionMode.Compress))
				{
					ParticleEditor.CopyTo(memoryStream, gzipStream);
				}
				array = memoryStream2.ToArray();
			}
		}
		return array;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
	public static string Unzip(byte[] bytes)
	{
		string @string;
		using (MemoryStream memoryStream = new MemoryStream(bytes))
		{
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					ParticleEditor.CopyTo(gzipStream, memoryStream2);
				}
				@string = Encoding.UTF8.GetString(memoryStream2.ToArray());
			}
		}
		return @string;
	}

	// Token: 0x040001B7 RID: 439
	private int currentParticleSlot;

	// Token: 0x040001B8 RID: 440
	private ParticleSlots particleSlots;

	// Token: 0x040001B9 RID: 441
	private string particleEffectName = "";

	// Token: 0x040001BA RID: 442
	public InputField userParticleNameInputField;

	// Token: 0x040001BB RID: 443
	public Dropdown userParticleEffectsDropdown;

	// Token: 0x040001BC RID: 444
	public GameObject mainEffectObj;

	// Token: 0x040001BD RID: 445
	public GameObject childEffectObj;

	// Token: 0x040001BE RID: 446
	public GameObject fogEffectObj;

	// Token: 0x040001BF RID: 447
	public GameObject trailEffectObj;

	// Token: 0x040001C0 RID: 448
	public VisualEffect userEffect;

	// Token: 0x040001C1 RID: 449
	public Slider mainRateSlider;

	// Token: 0x040001C2 RID: 450
	public Slider mainVel1XSlider;

	// Token: 0x040001C3 RID: 451
	public Slider mainVel2XSlider;

	// Token: 0x040001C4 RID: 452
	public Slider mainVel1YSlider;

	// Token: 0x040001C5 RID: 453
	public Slider mainVel2YSlider;

	// Token: 0x040001C6 RID: 454
	public Slider mainLifetime1Slider;

	// Token: 0x040001C7 RID: 455
	public Slider mainLifetime2Slider;

	// Token: 0x040001C8 RID: 456
	public Slider mainSpawnRadSlider;

	// Token: 0x040001C9 RID: 457
	public Slider mainTurbulenceSlider;

	// Token: 0x040001CA RID: 458
	public Slider mainTOctavesSlider;

	// Token: 0x040001CB RID: 459
	public Slider mainTDragSlider;

	// Token: 0x040001CC RID: 460
	public Slider mainTFrequencySlider;

	// Token: 0x040001CD RID: 461
	public Slider mainChildSRSlider;

	// Token: 0x040001CE RID: 462
	public Slider mainSizeSlider;

	// Token: 0x040001CF RID: 463
	public Slider mainNoiseSlider;

	// Token: 0x040001D0 RID: 464
	public Slider mainGravitySlider;

	// Token: 0x040001D1 RID: 465
	public Text mainRateText;

	// Token: 0x040001D2 RID: 466
	public Text mainVel1XText;

	// Token: 0x040001D3 RID: 467
	public Text mainVel2XText;

	// Token: 0x040001D4 RID: 468
	public Text mainVel1YText;

	// Token: 0x040001D5 RID: 469
	public Text mainVel2YText;

	// Token: 0x040001D6 RID: 470
	public Text mainLifetime1Text;

	// Token: 0x040001D7 RID: 471
	public Text mainLifetime2Text;

	// Token: 0x040001D8 RID: 472
	public Text mainSpawnRadText;

	// Token: 0x040001D9 RID: 473
	public Text mainTurbulenceText;

	// Token: 0x040001DA RID: 474
	public Text mainTOctavesText;

	// Token: 0x040001DB RID: 475
	public Text mainTDragText;

	// Token: 0x040001DC RID: 476
	public Text mainTFrequencyText;

	// Token: 0x040001DD RID: 477
	public Text mainChildSRText;

	// Token: 0x040001DE RID: 478
	public Text mainSizeText;

	// Token: 0x040001DF RID: 479
	public Text mainNoiseText;

	// Token: 0x040001E0 RID: 480
	public Text mainGravityText;

	// Token: 0x040001E1 RID: 481
	public Slider childVel1XSlider;

	// Token: 0x040001E2 RID: 482
	public Slider childVel2XSlider;

	// Token: 0x040001E3 RID: 483
	public Slider childVel1YSlider;

	// Token: 0x040001E4 RID: 484
	public Slider childVel2YSlider;

	// Token: 0x040001E5 RID: 485
	public Slider childLifetime1Slider;

	// Token: 0x040001E6 RID: 486
	public Slider childLifetime2Slider;

	// Token: 0x040001E7 RID: 487
	public Slider childSpawnRadSlider;

	// Token: 0x040001E8 RID: 488
	public Slider childTurbulenceSlider;

	// Token: 0x040001E9 RID: 489
	public Slider childTOctavesSlider;

	// Token: 0x040001EA RID: 490
	public Slider childTDragSlider;

	// Token: 0x040001EB RID: 491
	public Slider childTFrequencySlider;

	// Token: 0x040001EC RID: 492
	public Slider childSizeSlider;

	// Token: 0x040001ED RID: 493
	public Slider childNoiseSlider;

	// Token: 0x040001EE RID: 494
	public Slider childGravitySlider;

	// Token: 0x040001EF RID: 495
	public Text childVel1XText;

	// Token: 0x040001F0 RID: 496
	public Text childVel2XText;

	// Token: 0x040001F1 RID: 497
	public Text childVel1YText;

	// Token: 0x040001F2 RID: 498
	public Text childVel2YText;

	// Token: 0x040001F3 RID: 499
	public Text childLifetime1Text;

	// Token: 0x040001F4 RID: 500
	public Text childLifetime2Text;

	// Token: 0x040001F5 RID: 501
	public Text childSpawnRadText;

	// Token: 0x040001F6 RID: 502
	public Text childTurbulenceText;

	// Token: 0x040001F7 RID: 503
	public Text childTOctavesText;

	// Token: 0x040001F8 RID: 504
	public Text childTDragText;

	// Token: 0x040001F9 RID: 505
	public Text childTFrequencyText;

	// Token: 0x040001FA RID: 506
	public Text childSizeText;

	// Token: 0x040001FB RID: 507
	public Text childNoiseText;

	// Token: 0x040001FC RID: 508
	public Text childGravityText;

	// Token: 0x040001FD RID: 509
	public Slider fogRateSlider;

	// Token: 0x040001FE RID: 510
	public Slider fogVel1XSlider;

	// Token: 0x040001FF RID: 511
	public Slider fogVel2XSlider;

	// Token: 0x04000200 RID: 512
	public Slider fogVel1YSlider;

	// Token: 0x04000201 RID: 513
	public Slider fogVel2YSlider;

	// Token: 0x04000202 RID: 514
	public Slider fogLifetime1Slider;

	// Token: 0x04000203 RID: 515
	public Slider fogLifetime2Slider;

	// Token: 0x04000204 RID: 516
	public Slider fogSpawnRadSlider;

	// Token: 0x04000205 RID: 517
	public Text fogRateText;

	// Token: 0x04000206 RID: 518
	public Text fogVel1XText;

	// Token: 0x04000207 RID: 519
	public Text fogVel2XText;

	// Token: 0x04000208 RID: 520
	public Text fogVel1YText;

	// Token: 0x04000209 RID: 521
	public Text fogVel2YText;

	// Token: 0x0400020A RID: 522
	public Text fogLifetime1Text;

	// Token: 0x0400020B RID: 523
	public Text fogLifetime2Text;

	// Token: 0x0400020C RID: 524
	public Text fogSpawnRadText;

	// Token: 0x0400020D RID: 525
	public Slider trailRateSlider;

	// Token: 0x0400020E RID: 526
	public Slider trailVel1XSlider;

	// Token: 0x0400020F RID: 527
	public Slider trailVel2XSlider;

	// Token: 0x04000210 RID: 528
	public Slider trailVel1YSlider;

	// Token: 0x04000211 RID: 529
	public Slider trailVel2YSlider;

	// Token: 0x04000212 RID: 530
	public Slider trailLifetime1Slider;

	// Token: 0x04000213 RID: 531
	public Slider trailLifetime2Slider;

	// Token: 0x04000214 RID: 532
	public Slider trailTurbulenceSlider;

	// Token: 0x04000215 RID: 533
	public Slider trailLengthSlider;

	// Token: 0x04000216 RID: 534
	public Text trailRateText;

	// Token: 0x04000217 RID: 535
	public Text trailVel1XText;

	// Token: 0x04000218 RID: 536
	public Text trailVel2XText;

	// Token: 0x04000219 RID: 537
	public Text trailVel1YText;

	// Token: 0x0400021A RID: 538
	public Text trailVel2YText;

	// Token: 0x0400021B RID: 539
	public Text trailLifetime1Text;

	// Token: 0x0400021C RID: 540
	public Text trailLifetime2Text;

	// Token: 0x0400021D RID: 541
	public Text trailTurbulenceText;

	// Token: 0x0400021E RID: 542
	public Text trailLengthText;

	// Token: 0x0400021F RID: 543
	public InputField effectStringInputField;
}
