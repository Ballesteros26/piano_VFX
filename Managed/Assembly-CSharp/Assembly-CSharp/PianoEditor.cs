using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using UnityEngine.VFX;

// Token: 0x0200001D RID: 29
public class PianoEditor : MonoBehaviour
{
	// Token: 0x060000E9 RID: 233 RVA: 0x0000C77C File Offset: 0x0000A97C
	private void Start()
	{
		this.fm = this.fileManagerObj.GetComponent<FileManager>();
		this.pms = this.playerObj.GetComponent<PlayMidiSound>();
		this.midiStartSpeed = this.tileObj.GetComponent<MoveTile>().speed;
		this.postVolume = this.postVolumeObject.GetComponent<Volume>();
	}

	// Token: 0x060000EA RID: 234 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
	public void OpenAudioEdit()
	{
		if (this.fm.audioSource.clip == null && !this.fm.usingAudioFile)
		{
			return;
		}
		if (!this.audioEditor.activeSelf)
		{
			this.textEditor.SetActive(false);
			this.midiEditor.SetActive(false);
			this.imageEditor.SetActive(false);
			this.audioEditor.SetActive(true);
			this.videoEditor.SetActive(false);
			this.postEditor.SetActive(false);
			this.audioEndTrimText.text = "Trim end: " + Math.Round((double)(this.audioEndTrimSlider.value * this.fm.audioSource.clip.length), 2) + "s";
			this.pms.trimEnd = this.audioEndTrimSlider.value * this.fm.audioSource.clip.length;
			return;
		}
		this.audioEditor.SetActive(false);
	}

	// Token: 0x060000EB RID: 235 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
	public void OpenVideoEdit()
	{
		if (this.fm.videoPlayer == null && !this.fm.usingVideoFile)
		{
			return;
		}
		if (!this.videoEditor.activeSelf)
		{
			this.textEditor.SetActive(false);
			this.midiEditor.SetActive(false);
			this.imageEditor.SetActive(false);
			this.videoEditor.SetActive(true);
			this.audioEditor.SetActive(false);
			this.postEditor.SetActive(false);
			return;
		}
		this.videoEditor.SetActive(false);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x0000C970 File Offset: 0x0000AB70
	public void OpenImageEdit()
	{
		if (!this.fm.usingImageFile)
		{
			return;
		}
		if (!this.imageEditor.activeSelf)
		{
			this.textEditor.SetActive(false);
			this.midiEditor.SetActive(false);
			this.imageEditor.SetActive(true);
			this.videoEditor.SetActive(false);
			this.audioEditor.SetActive(false);
			this.postEditor.SetActive(false);
			return;
		}
		this.imageEditor.SetActive(false);
	}

	// Token: 0x060000ED RID: 237 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
	public void OpenMidiEdit()
	{
		if (!this.fm.usingMidiFile)
		{
			return;
		}
		if (!this.midiEditor.activeSelf)
		{
			this.textEditor.SetActive(false);
			this.midiEditor.SetActive(true);
			this.imageEditor.SetActive(false);
			this.videoEditor.SetActive(false);
			this.audioEditor.SetActive(false);
			this.postEditor.SetActive(false);
			return;
		}
		this.midiEditor.SetActive(false);
	}

	// Token: 0x060000EE RID: 238 RVA: 0x0000CA70 File Offset: 0x0000AC70
	public void OpenTextEdit()
	{
		if (!this.textEditor.activeSelf)
		{
			this.textEditor.SetActive(true);
			this.midiEditor.SetActive(false);
			this.imageEditor.SetActive(false);
			this.videoEditor.SetActive(false);
			this.audioEditor.SetActive(false);
			this.postEditor.SetActive(false);
			return;
		}
		this.textEditor.SetActive(false);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
	public void OpenPostEdit()
	{
		if (!this.postEditor.activeSelf)
		{
			this.postEditor.SetActive(true);
			this.textEditor.SetActive(false);
			this.midiEditor.SetActive(false);
			this.imageEditor.SetActive(false);
			this.videoEditor.SetActive(false);
			this.audioEditor.SetActive(false);
			return;
		}
		this.postEditor.SetActive(false);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000CB50 File Offset: 0x0000AD50
	public void OpenProperties()
	{
		if (!this.propertiesObj.activeSelf)
		{
			this.propertiesObj.SetActive(true);
			this.moveObj.SetActive(false);
			this.sizeObj.SetActive(false);
			this.cropObj.SetActive(false);
			return;
		}
		this.propertiesObj.SetActive(false);
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
	public void OpenMove()
	{
		if (!this.moveObj.activeSelf)
		{
			this.propertiesObj.SetActive(false);
			this.moveObj.SetActive(true);
			this.sizeObj.SetActive(false);
			this.cropObj.SetActive(false);
			return;
		}
		this.moveObj.SetActive(false);
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x0000CC00 File Offset: 0x0000AE00
	public void OpenSize()
	{
		if (!this.sizeObj.activeSelf)
		{
			this.propertiesObj.SetActive(false);
			this.moveObj.SetActive(false);
			this.sizeObj.SetActive(true);
			this.cropObj.SetActive(false);
			return;
		}
		this.sizeObj.SetActive(false);
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x0000CC58 File Offset: 0x0000AE58
	public void OpenCrop()
	{
		if (!this.cropObj.activeSelf)
		{
			this.propertiesObj.SetActive(false);
			this.moveObj.SetActive(false);
			this.sizeObj.SetActive(false);
			this.cropObj.SetActive(true);
			return;
		}
		this.cropObj.SetActive(false);
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
	public void ChangeAudioOffset()
	{
		if (this.fm.audioSource.clip == null)
		{
			return;
		}
		this.audioOffsetText.text = "Offset: " + Math.Round((double)(this.audioOffsetSlider.value * this.fm.audioSource.clip.length), 2) + "s";
		this.pms.audioOffset = this.audioOffsetSlider.value * this.fm.audioSource.clip.length;
		this.pms.AudioOffsetChanges();
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x0000CD54 File Offset: 0x0000AF54
	public void AddAudioOffset()
	{
		if (this.fm.audioSource.clip == null)
		{
			return;
		}
		this.pms.audioOffset += 0.1f;
		this.audioOffsetText.text = "Offset: " + Math.Round((double)this.pms.audioOffset, 2) + "s";
		this.pms.AudioOffsetChanges();
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
	public void RemoveAudioOffset()
	{
		if (this.fm.audioSource.clip == null)
		{
			return;
		}
		this.pms.audioOffset -= 0.1f;
		this.audioOffsetText.text = "Offset: " + Math.Round((double)this.pms.audioOffset, 2) + "s";
		this.pms.AudioOffsetChanges();
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0000CE4C File Offset: 0x0000B04C
	public void ChangeVideoOffset()
	{
		if (this.fm.videoPlayer == null)
		{
			return;
		}
		this.videoOffsetText.text = "Offset: " + Math.Round((double)this.videoOffsetSlider.value * this.fm.videoPlayer.length, 2) + "s";
		this.pms.videoOffset = this.videoOffsetSlider.value * (float)this.fm.videoPlayer.length;
		this.pms.VideoOffsetChanges();
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
	public void AddVideoffset()
	{
		if (this.fm.videoPlayer == null)
		{
			return;
		}
		this.pms.videoOffset += 0.1f;
		this.videoOffsetText.text = "Offset: " + Math.Round((double)this.pms.videoOffset, 2) + "s";
		this.pms.VideoOffsetChanges();
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x0000CF58 File Offset: 0x0000B158
	public void RemoveVideoOffset()
	{
		if (this.fm.videoPlayer == null)
		{
			return;
		}
		this.pms.videoOffset -= 0.1f;
		this.videoOffsetText.text = "Offset: " + Math.Round((double)this.pms.videoOffset, 2) + "s";
		this.pms.VideoOffsetChanges();
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0000CFCC File Offset: 0x0000B1CC
	public void ChangeAudioVolume()
	{
		if (this.fm.audioSource.clip == null)
		{
			return;
		}
		this.audioVolumeText.text = "Volume: " + Math.Round((double)this.audioVolumeSlider.value, 2);
		this.fm.audioSource.volume = this.audioVolumeSlider.value;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0000D03C File Offset: 0x0000B23C
	public void ChangeAudioStartTrim()
	{
		if (this.fm.audioSource.clip == null)
		{
			return;
		}
		if (this.audioStartTrimSlider.value <= this.audioEndTrimSlider.value)
		{
			this.audioStartTrimText.text = "Trim start: " + Math.Round((double)(this.audioStartTrimSlider.value * this.fm.audioSource.clip.length), 2) + "s";
			this.pms.trimStart = this.audioStartTrimSlider.value * this.fm.audioSource.clip.length;
			return;
		}
		this.audioStartTrimSlider.value = this.audioEndTrimSlider.value;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000D104 File Offset: 0x0000B304
	public void ChangeAudioEndTrim()
	{
		if (this.fm.audioSource.clip == null)
		{
			return;
		}
		if (this.audioEndTrimSlider.value >= this.audioStartTrimSlider.value)
		{
			this.audioEndTrimText.text = "Trim end: " + Math.Round((double)(this.audioEndTrimSlider.value * this.fm.audioSource.clip.length), 2) + "s";
			this.pms.trimEnd = this.audioEndTrimSlider.value * this.fm.audioSource.clip.length;
			return;
		}
		this.audioEndTrimSlider.value = this.audioStartTrimSlider.value;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0000D1CC File Offset: 0x0000B3CC
	public void ChangeVideoBrightness()
	{
		this.videoDisplay.GetComponent<SpriteRenderer>().material.color = new Color(this.videoBrightnessSlider.value, this.videoBrightnessSlider.value, this.videoBrightnessSlider.value, 1f);
		this.videoBrightnessText.text = "Brightness: " + Math.Round((double)this.videoBrightnessSlider.value, 2).ToString();
	}

	// Token: 0x060000FE RID: 254 RVA: 0x0000D248 File Offset: 0x0000B448
	public void ChangeMidiY()
	{
		Vector3 position = this.fm.camera.transform.position;
		Vector3 position2 = this.playerGUI.transform.position;
		Vector3 position3 = this.videoObject.transform.position;
		this.fm.camera.transform.position = new Vector3(position.x, this.videoMidiYSlider.value, position.z);
		this.playerGUI.transform.position = new Vector3(position2.x, this.videoMidiYSlider.value, position2.z);
		this.moveYSlider.value = position3.y + (this.fm.camera.transform.position.y - position.y);
		this.videoMidiYText.text = "Midi Y: " + Math.Round((double)this.videoMidiYSlider.value, 2).ToString();
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000D34C File Offset: 0x0000B54C
	public void RotateVideo()
	{
		this.videoDisplay.transform.eulerAngles = new Vector3(this.videoDisplay.transform.eulerAngles.x, this.videoDisplay.transform.eulerAngles.y, this.videoRotateSlider.value);
		this.videoRotateText.text = "Rotate: " + Math.Round((double)this.videoRotateSlider.value, 2).ToString();
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
	public void MoveX()
	{
		Vector3 position = this.videoObject.transform.position;
		this.videoObject.transform.position = new Vector3(this.moveXSlider.value, position.y, position.z);
		this.moveXText.text = "Pos X: " + Math.Round((double)this.moveXSlider.value, 2).ToString();
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000D450 File Offset: 0x0000B650
	public void MoveY()
	{
		Vector3 position = this.videoObject.transform.position;
		this.videoObject.transform.position = new Vector3(position.x, this.moveYSlider.value, position.z);
		this.moveYText.text = "Pos Y: " + Math.Round((double)this.moveYSlider.value, 2).ToString();
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000D4CC File Offset: 0x0000B6CC
	public void ChangeWidth()
	{
		Vector3 localScale = this.videoObject.transform.localScale;
		this.videoObject.transform.localScale = new Vector3(this.widthSlider.value, localScale.y, localScale.z);
		this.widthText.text = "Width: " + Math.Round((double)this.widthSlider.value, 2).ToString();
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000D548 File Offset: 0x0000B748
	public void ChangeHeight()
	{
		Vector3 localScale = this.videoObject.transform.localScale;
		this.videoObject.transform.localScale = new Vector3(localScale.x, this.heightSlider.value, localScale.z);
		this.heightText.text = "Height: " + Math.Round((double)this.heightSlider.value, 2).ToString();
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
	public void CropRight()
	{
		if (this.cropRightSlider.value <= this.cropLeftSlider.value * -1f)
		{
			this.cropRightSlider.value = this.cropLeftSlider.value * -1f;
			return;
		}
		Vector3 vector = new Vector3(this.videoObject.transform.eulerAngles.x, this.videoObject.transform.eulerAngles.y, this.videoObject.transform.eulerAngles.z);
		this.videoObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		Vector3 position = this.videoMask.transform.position;
		Vector3 localScale = this.videoMask.transform.localScale;
		this.videoMask.transform.localScale = new Vector3(1.6f * ((this.cropRightSlider.value + this.cropLeftSlider.value) / 2f), localScale.y, localScale.z);
		this.videoMask.transform.position = new Vector3(this.videoObject.transform.position.x + (this.cropRightSlider.value * 2f - 2f + (2f - this.cropLeftSlider.value * 2f)) * this.videoObject.transform.localScale.x, position.y, position.z);
		this.cropRightText.text = "Right: " + Math.Round((double)this.cropRightSlider.value, 2).ToString();
		this.videoObject.transform.eulerAngles = vector;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000D798 File Offset: 0x0000B998
	public void CropLeft()
	{
		if (this.cropLeftSlider.value <= this.cropRightSlider.value * -1f)
		{
			this.cropLeftSlider.value = this.cropRightSlider.value * -1f;
			return;
		}
		Vector3 vector = new Vector3(this.videoObject.transform.eulerAngles.x, this.videoObject.transform.eulerAngles.y, this.videoObject.transform.eulerAngles.z);
		this.videoObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		Vector3 position = this.videoMask.transform.position;
		Vector3 localScale = this.videoMask.transform.localScale;
		this.videoMask.transform.localScale = new Vector3(1.6f * ((this.cropLeftSlider.value + this.cropRightSlider.value) / 2f), localScale.y, localScale.z);
		this.videoMask.transform.position = new Vector3(this.videoObject.transform.position.x + (2f - this.cropLeftSlider.value * 2f + (this.cropRightSlider.value * 2f - 2f)) * this.videoObject.transform.localScale.x, position.y, position.z);
		this.cropLeftText.text = "Left: " + Math.Round((double)this.cropLeftSlider.value, 2).ToString();
		this.videoObject.transform.eulerAngles = vector;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000D96C File Offset: 0x0000BB6C
	public void CropTop()
	{
		if (this.cropTopSlider.value <= this.cropBottomSlider.value * -1f)
		{
			this.cropTopSlider.value = this.cropBottomSlider.value * -1f;
			return;
		}
		Vector3 vector = new Vector3(this.videoObject.transform.eulerAngles.x, this.videoObject.transform.eulerAngles.y, this.videoObject.transform.eulerAngles.z);
		this.videoObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		Vector3 position = this.videoMask.transform.position;
		Vector3 localScale = this.videoMask.transform.localScale;
		this.videoMask.transform.localScale = new Vector3(localScale.x, 0.9f * ((this.cropTopSlider.value + this.cropBottomSlider.value) / 2f), localScale.z);
		this.cropTopText.text = "Top: " + Math.Round((double)this.cropTopSlider.value, 2).ToString();
		this.videoObject.transform.eulerAngles = vector;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
	public void CropBottom()
	{
		if (this.cropBottomSlider.value <= this.cropTopSlider.value * -1f)
		{
			this.cropBottomSlider.value = this.cropTopSlider.value * -1f;
			return;
		}
		Vector3 vector = new Vector3(this.videoObject.transform.eulerAngles.x, this.videoObject.transform.eulerAngles.y, this.videoObject.transform.eulerAngles.z);
		this.videoObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		Vector3 position = this.videoMask.transform.position;
		Vector3 localScale = this.videoMask.transform.localScale;
		this.videoMask.transform.localScale = new Vector3(localScale.x, 0.9f * ((this.cropTopSlider.value + this.cropBottomSlider.value) / 2f), localScale.z);
		this.videoMask.transform.position = new Vector3(position.x, this.videoObject.transform.position.y + (2.25f * this.videoObject.transform.localScale.y - this.cropBottomSlider.value * 2.25f * this.videoObject.transform.localScale.y), position.z);
		this.cropBottomText.text = "Bottom: " + Math.Round((double)this.cropBottomSlider.value, 2).ToString();
		this.videoObject.transform.eulerAngles = vector;
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0000DC96 File Offset: 0x0000BE96
	public void UseImageFile()
	{
		if (this.useImageFileToggle.isOn)
		{
			this.imageDisplay.SetActive(true);
			return;
		}
		this.imageDisplay.SetActive(false);
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
	public void MoveXImage()
	{
		Vector3 position = this.imageDisplay.transform.position;
		this.imageDisplay.transform.position = new Vector3(this.imagePosXSlider.value, position.y, position.z);
		this.imagePosXText.text = "Pos X: " + Math.Round((double)this.imagePosXSlider.value, 2).ToString();
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000DD3C File Offset: 0x0000BF3C
	public void MoveYImage()
	{
		Vector3 position = this.imageDisplay.transform.position;
		this.imageDisplay.transform.position = new Vector3(position.x, this.imagePosYSlider.value, position.z);
		this.imagePosYText.text = "Pos Y: " + Math.Round((double)this.imagePosYSlider.value, 2).ToString();
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
	public void ResizeImage()
	{
		Vector3 localScale = this.imageDisplay.transform.localScale;
		this.imageDisplay.transform.localScale = new Vector3(this.imageSizeSlider.value, this.imageSizeSlider.value, localScale.z);
		this.imageSizeText.text = "Size: " + Math.Round((double)this.imageSizeSlider.value, 2).ToString();
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000DE38 File Offset: 0x0000C038
	public void RotateImage()
	{
		this.imageDisplay.transform.eulerAngles = new Vector3(this.imageDisplay.transform.eulerAngles.x, this.imageDisplay.transform.eulerAngles.y, this.imageRotationSlider.value);
		this.imageRotationText.text = "Rotate: " + Math.Round((double)this.imageRotationSlider.value, 2).ToString();
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
	public void OpacityImage()
	{
		this.imageDisplay.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, this.imageOpacitySlider.value);
		this.imageOpacityText.text = "Opacity: " + Math.Round((double)this.imageOpacitySlider.value, 2).ToString();
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000DF2B File Offset: 0x0000C12B
	public void UseAudioReactor()
	{
		if (this.useAudioReactorToggle.isOn)
		{
			this.audioReactorObj.SetActive(true);
			return;
		}
		this.audioReactorObj.SetActive(false);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000DF54 File Offset: 0x0000C154
	public void AudioReactorOnVideo()
	{
		Vector3 position = this.audioReactorObj.transform.position;
		if (position.z == 0f)
		{
			this.audioReactorObj.transform.position = new Vector3(position.x, position.y, -3f);
			return;
		}
		this.audioReactorObj.transform.position = new Vector3(position.x, position.y, 0f);
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000DFCC File Offset: 0x0000C1CC
	public void MoveXAudioReactor()
	{
		Vector3 position = this.audioReactorObj.transform.position;
		this.audioReactorObj.transform.position = new Vector3(this.audioReactorPosXSlider.value, position.y, position.z);
		this.audioReactorPosXText.text = "R Pos X: " + Math.Round((double)this.audioReactorPosXSlider.value, 2).ToString();
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000E048 File Offset: 0x0000C248
	public void MoveYAudioReactor()
	{
		Vector3 position = this.audioReactorObj.transform.position;
		this.audioReactorObj.transform.position = new Vector3(position.x, this.audioReactorPosYSlider.value, position.z);
		this.audioReactorPosYText.text = "R Pos Y: " + Math.Round((double)this.audioReactorPosYSlider.value, 2).ToString();
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000E0C1 File Offset: 0x0000C2C1
	public void UseVirtualLights()
	{
		if (this.useVirtualLights.isOn)
		{
			this.fm.useVirtualLights = true;
			return;
		}
		this.fm.useVirtualLights = false;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000E0E9 File Offset: 0x0000C2E9
	public void UseHitEffect()
	{
		if (this.useHitEffectToggle.isOn)
		{
			this.fm.useHitEffect = true;
			return;
		}
		this.fm.useHitEffect = false;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000E114 File Offset: 0x0000C314
	public void ResetTempo()
	{
		this.midiTempoSlider.value = 1f;
		this.playerObj.GetComponent<PlayMidiSound>().userMidiSpeed = this.midiStartSpeed;
		this.playerObj.GetComponent<PlayMidiSound>().RenderPlay();
		this.midiTempoText.text = "Speed: " + Math.Round((double)this.midiTempoSlider.value, 4).ToString();
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000E188 File Offset: 0x0000C388
	public void ChangeMidiTempo()
	{
		this.playerObj.GetComponent<PlayMidiSound>().userMidiSpeed = this.midiStartSpeed * this.midiTempoSlider.value;
		this.playerObj.GetComponent<PlayMidiSound>().RenderPlay();
		this.midiTempoText.text = "Speed: " + Math.Round((double)this.midiTempoSlider.value, 4).ToString();
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
	public void ChangeLightSize()
	{
		this.lightObj.GetComponent<VisualEffect>().SetFloat("Size", this.virtualLightSizeSlider.value);
		this.virtualLightSizeText.text = "Light size: " + Math.Round((double)this.virtualLightSizeSlider.value, 2).ToString();
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000E254 File Offset: 0x0000C454
	public void ChangeLightIntensity()
	{
		this.lightIntensityValue = this.virtualLightIntensitySlider.value;
		this.virtualLightIntensityText.text = "Light intensity: " + Math.Round((double)this.virtualLightIntensitySlider.value, 2).ToString();
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
	public void ChangeLightPosY()
	{
		this.lightYPosValue = this.virtualLightPosYSlider.value;
		this.virtualLightPosYText.text = "Light Y: " + Math.Round((double)this.virtualLightPosYSlider.value, 2).ToString();
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
	public void UseFog()
	{
		if (this.useFogToggle.isOn)
		{
			this.fog.gameObject.SetActive(true);
			this.fog.SetVector4("FogColor", this.fm.fogColor);
			return;
		}
		this.fog.gameObject.SetActive(false);
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000E351 File Offset: 0x0000C551
	public void ChangeUserText()
	{
		this.userText.text = this.userTextInput.text;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000E36C File Offset: 0x0000C56C
	public void ChangeUserTextPosX()
	{
		Vector3 position = this.userText.transform.position;
		this.userText.transform.position = new Vector3(this.userTextPosXSlider.value, position.y, position.z);
		this.userTextPosXText.text = "Pos X: " + Math.Round((double)this.userTextPosXSlider.value, 2).ToString();
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
	public void ChangeUserTextPosY()
	{
		Vector3 position = this.userText.transform.position;
		this.userText.transform.position = new Vector3(position.x, this.userTextPosYSlider.value, position.z);
		this.userTextPosYText.text = "Pos Y: " + Math.Round((double)this.userTextPosYSlider.value, 2).ToString();
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000E464 File Offset: 0x0000C664
	public void ChangeUserTextSize()
	{
		Vector3 localScale = this.userText.transform.localScale;
		this.userText.transform.localScale = new Vector3(this.userTextSizeSlider.value * 0.001f, this.userTextSizeSlider.value * 0.001f, localScale.z);
		this.userTextSizeText.text = "Size: " + Math.Round((double)this.userTextSizeSlider.value, 2).ToString();
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
	public void ChangeUserTextColor()
	{
		Color color = new Color(this.userTextRedSlider.value, this.userTextGreenSlider.value, this.userTextBlueSlider.value);
		this.userText.color = color;
		this.userTextRedText.text = "Red: " + Math.Round((double)this.userTextRedSlider.value, 2).ToString();
		this.userTextGreenText.text = "Green: " + Math.Round((double)this.userTextGreenSlider.value, 2).ToString();
		this.userTextBlueText.text = "Blue: " + Math.Round((double)this.userTextBlueSlider.value, 2).ToString();
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0000E5BE File Offset: 0x0000C7BE
	public void TogglePostEffects()
	{
		this.postVolumeObject.SetActive(!this.postVolumeObject.activeSelf);
	}

	// Token: 0x06000120 RID: 288 RVA: 0x0000E5DC File Offset: 0x0000C7DC
	public void ChangeExposure()
	{
		if (this.postVolume)
		{
			ColorAdjustments colorAdjustments;
			if (this.postVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
			{
				colorAdjustments.postExposure.value = this.exposureSlider.value;
			}
			this.exposureText.text = "Exposure: " + Math.Round((double)this.exposureSlider.value, 2).ToString();
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000E650 File Offset: 0x0000C850
	public void ChangeContrast()
	{
		if (this.postVolume)
		{
			ColorAdjustments colorAdjustments;
			if (this.postVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
			{
				colorAdjustments.contrast.value = this.contrastSlider.value;
			}
			this.contrastText.text = "Contrast: " + Math.Round((double)this.contrastSlider.value, 2).ToString();
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
	public void ChangeSaturation()
	{
		if (this.postVolume)
		{
			ColorAdjustments colorAdjustments;
			if (this.postVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
			{
				colorAdjustments.saturation.value = this.saturationSlider.value;
			}
			this.saturationText.text = "Saturation: " + Math.Round((double)this.saturationSlider.value, 2).ToString();
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000E738 File Offset: 0x0000C938
	public void SaveEditorValues()
	{
		string text = JsonUtility.ToJson(new EditorObject
		{
			audioOn = this.pms.audioOn,
			audioOffset = this.pms.audioOffset,
			audioTrimStart = this.audioStartTrimSlider.value,
			audioTrimEnd = this.audioEndTrimSlider.value,
			audioVolume = this.audioVolumeSlider.value,
			videoOn = this.pms.videoOn,
			videoOffset = this.pms.videoOffset,
			videoOffsetSlider = this.videoOffsetSlider.value,
			videoBrightness = this.videoBrightnessSlider.value,
			videoMidiY = this.videoMidiYSlider.value,
			videoRotation = this.videoRotateSlider.value,
			videoPosX = this.moveXSlider.value,
			videoPosY = this.moveYSlider.value,
			videoWidth = this.widthSlider.value,
			videoHeight = this.heightSlider.value,
			videoCropRight = this.cropRightSlider.value,
			videoCropLeft = this.cropLeftSlider.value,
			videoCropTop = this.cropTopSlider.value,
			videoCropBottom = this.cropBottomSlider.value,
			imageOn = this.useImageFileToggle.isOn,
			imagePosX = this.imagePosXSlider.value,
			imagePosY = this.imagePosYSlider.value,
			imageSize = this.imageSizeSlider.value,
			imageRotation = this.imageRotationSlider.value,
			imageOpacity = this.imageOpacitySlider.value,
			reactorOn = this.useAudioReactorToggle.isOn,
			reactorOnVideo = this.useAudioReactorOnVideoToggle.isOn,
			reactorPosX = this.audioReactorPosXSlider.value,
			reactorPosY = this.audioReactorPosYSlider.value,
			useVirtualLightsToggle = this.useVirtualLights.isOn,
			useHitEffectToggle = this.useHitEffectToggle.isOn,
			midiTempo = this.midiTempoSlider.value,
			lightSize = this.virtualLightSizeSlider.value,
			lightIntensity = this.virtualLightIntensitySlider.value,
			lightPosY = this.virtualLightPosYSlider.value,
			useFog = this.useFogToggle.isOn,
			userText = this.userText.text,
			userTextPosX = this.userTextPosXSlider.value,
			userTextPosY = this.userTextPosYSlider.value,
			userTextSize = this.userTextSizeSlider.value,
			userTextRed = this.userTextRedSlider.value,
			userTextGreen = this.userTextGreenSlider.value,
			userTextBlue = this.userTextBlueSlider.value,
			usePostEffects = this.usePostEffects.isOn,
			exposure = this.exposureSlider.value,
			contrast = this.contrastSlider.value,
			saturation = this.saturationSlider.value
		});
		PlayerPrefs.SetString("EditorValues", text);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000EA80 File Offset: 0x0000CC80
	public void RestoreEditorValues(string json)
	{
		EditorObject editorObject = new EditorObject();
		editorObject = JsonUtility.FromJson<EditorObject>(json);
		if (this.fm.usingAudioFile)
		{
			this.audioOffsetSlider.value = editorObject.audioOffset / this.fm.audioSource.clip.length;
		}
		this.pms.audioOffset = editorObject.audioOffset;
		this.audioStartTrimSlider.value = editorObject.audioTrimStart;
		this.audioEndTrimSlider.value = editorObject.audioTrimEnd;
		this.audioVolumeSlider.value = editorObject.audioVolume;
		if (this.fm.usingAudioFile)
		{
			this.pms.trimEnd = this.audioEndTrimSlider.value * this.fm.audioSource.clip.length;
		}
		if (this.fm.usingAudioFile)
		{
			this.audioOnToggle.isOn = editorObject.audioOn;
		}
		this.videoOffsetSlider.value = editorObject.videoOffsetSlider;
		this.videoOffsetText.text = "Offset: " + Math.Round((double)editorObject.videoOffset, 2).ToString() + "s";
		this.pms.videoOffset = editorObject.videoOffset;
		this.videoBrightnessSlider.value = editorObject.videoBrightness;
		this.videoMidiYSlider.value = editorObject.videoMidiY;
		this.videoRotateSlider.value = editorObject.videoRotation;
		this.moveXSlider.value = editorObject.videoPosX;
		this.moveYSlider.value = editorObject.videoPosY;
		this.widthSlider.value = editorObject.videoWidth;
		this.heightSlider.value = editorObject.videoHeight;
		this.cropRightSlider.value = editorObject.videoCropRight;
		this.cropLeftSlider.value = editorObject.videoCropLeft;
		this.cropTopSlider.value = editorObject.videoCropTop;
		this.cropBottomSlider.value = editorObject.videoCropBottom;
		if (this.fm.usingVideoFile)
		{
			this.videoOnToggle.isOn = editorObject.videoOn;
		}
		if (this.fm.usingImageFile)
		{
			this.useImageFileToggle.isOn = editorObject.imageOn;
		}
		this.imagePosXSlider.value = editorObject.imagePosX;
		this.imagePosYSlider.value = editorObject.imagePosY;
		this.imageSizeSlider.value = editorObject.imageSize;
		this.imageRotationSlider.value = editorObject.imageRotation;
		this.imageOpacitySlider.value = editorObject.imageOpacity;
		this.useAudioReactorToggle.isOn = editorObject.reactorOn;
		this.useAudioReactorOnVideoToggle.isOn = editorObject.reactorOnVideo;
		this.audioReactorPosXSlider.value = editorObject.reactorPosX;
		this.audioReactorPosYSlider.value = editorObject.reactorPosY;
		this.useVirtualLights.isOn = editorObject.useVirtualLightsToggle;
		this.useHitEffectToggle.isOn = editorObject.useHitEffectToggle;
		this.midiTempoSlider.value = ((editorObject.midiTempo == 0f) ? 1f : editorObject.midiTempo);
		this.virtualLightSizeSlider.value = editorObject.lightSize;
		this.virtualLightIntensitySlider.value = editorObject.lightIntensity;
		this.virtualLightPosYSlider.value = editorObject.lightPosY;
		this.useFogToggle.isOn = editorObject.useFog;
		this.userText.text = editorObject.userText;
		this.userTextPosXSlider.value = editorObject.userTextPosX;
		this.userTextPosYSlider.value = editorObject.userTextPosY;
		this.userTextSizeSlider.value = editorObject.userTextSize;
		this.userTextRedSlider.value = editorObject.userTextRed;
		this.userTextGreenSlider.value = editorObject.userTextGreen;
		this.userTextBlueSlider.value = editorObject.userTextBlue;
		this.usePostEffects.isOn = editorObject.usePostEffects;
		this.exposureSlider.value = editorObject.exposure;
		this.contrastSlider.value = editorObject.contrast;
		this.saturationSlider.value = editorObject.saturation;
	}

	// Token: 0x04000263 RID: 611
	public GameObject audioEditor;

	// Token: 0x04000264 RID: 612
	public GameObject videoEditor;

	// Token: 0x04000265 RID: 613
	public GameObject imageEditor;

	// Token: 0x04000266 RID: 614
	public GameObject midiEditor;

	// Token: 0x04000267 RID: 615
	public GameObject textEditor;

	// Token: 0x04000268 RID: 616
	public GameObject postEditor;

	// Token: 0x04000269 RID: 617
	public GameObject videoObject;

	// Token: 0x0400026A RID: 618
	public GameObject videoDisplay;

	// Token: 0x0400026B RID: 619
	public GameObject imageDisplay;

	// Token: 0x0400026C RID: 620
	public GameObject videoMask;

	// Token: 0x0400026D RID: 621
	public GameObject fileManagerObj;

	// Token: 0x0400026E RID: 622
	public FileManager fm;

	// Token: 0x0400026F RID: 623
	public GameObject playerObj;

	// Token: 0x04000270 RID: 624
	public PlayMidiSound pms;

	// Token: 0x04000271 RID: 625
	public Text audioVolumeText;

	// Token: 0x04000272 RID: 626
	public Text audioOffsetText;

	// Token: 0x04000273 RID: 627
	public Text audioStartTrimText;

	// Token: 0x04000274 RID: 628
	public Text audioEndTrimText;

	// Token: 0x04000275 RID: 629
	public Toggle audioOnToggle;

	// Token: 0x04000276 RID: 630
	public Slider audioVolumeSlider;

	// Token: 0x04000277 RID: 631
	public Slider audioOffsetSlider;

	// Token: 0x04000278 RID: 632
	public Slider audioStartTrimSlider;

	// Token: 0x04000279 RID: 633
	public Slider audioEndTrimSlider;

	// Token: 0x0400027A RID: 634
	public Text videoOffsetText;

	// Token: 0x0400027B RID: 635
	public Text videoBrightnessText;

	// Token: 0x0400027C RID: 636
	public Text videoMidiYText;

	// Token: 0x0400027D RID: 637
	public Text videoRotateText;

	// Token: 0x0400027E RID: 638
	public Toggle videoOnToggle;

	// Token: 0x0400027F RID: 639
	public Slider videoOffsetSlider;

	// Token: 0x04000280 RID: 640
	public Slider videoBrightnessSlider;

	// Token: 0x04000281 RID: 641
	public Slider videoMidiYSlider;

	// Token: 0x04000282 RID: 642
	public Slider videoRotateSlider;

	// Token: 0x04000283 RID: 643
	public Vector2 videoPosition;

	// Token: 0x04000284 RID: 644
	public Vector2 videoSize;

	// Token: 0x04000285 RID: 645
	public Vector2 cropSize;

	// Token: 0x04000286 RID: 646
	public GameObject playerGUI;

	// Token: 0x04000287 RID: 647
	public GameObject pianoKeysWhite;

	// Token: 0x04000288 RID: 648
	public GameObject pianoKeysBlack;

	// Token: 0x04000289 RID: 649
	public GameObject pianoShadows;

	// Token: 0x0400028A RID: 650
	public GameObject propertiesObj;

	// Token: 0x0400028B RID: 651
	public GameObject moveObj;

	// Token: 0x0400028C RID: 652
	public GameObject sizeObj;

	// Token: 0x0400028D RID: 653
	public GameObject cropObj;

	// Token: 0x0400028E RID: 654
	public Slider moveXSlider;

	// Token: 0x0400028F RID: 655
	public Slider moveYSlider;

	// Token: 0x04000290 RID: 656
	public Slider widthSlider;

	// Token: 0x04000291 RID: 657
	public Slider heightSlider;

	// Token: 0x04000292 RID: 658
	public Slider cropRightSlider;

	// Token: 0x04000293 RID: 659
	public Slider cropLeftSlider;

	// Token: 0x04000294 RID: 660
	public Slider cropTopSlider;

	// Token: 0x04000295 RID: 661
	public Slider cropBottomSlider;

	// Token: 0x04000296 RID: 662
	public Text moveXText;

	// Token: 0x04000297 RID: 663
	public Text moveYText;

	// Token: 0x04000298 RID: 664
	public Text widthText;

	// Token: 0x04000299 RID: 665
	public Text heightText;

	// Token: 0x0400029A RID: 666
	public Text cropRightText;

	// Token: 0x0400029B RID: 667
	public Text cropLeftText;

	// Token: 0x0400029C RID: 668
	public Text cropTopText;

	// Token: 0x0400029D RID: 669
	public Text cropBottomText;

	// Token: 0x0400029E RID: 670
	public Toggle useImageFileToggle;

	// Token: 0x0400029F RID: 671
	public Slider imagePosXSlider;

	// Token: 0x040002A0 RID: 672
	public Slider imagePosYSlider;

	// Token: 0x040002A1 RID: 673
	public Slider imageSizeSlider;

	// Token: 0x040002A2 RID: 674
	public Slider imageRotationSlider;

	// Token: 0x040002A3 RID: 675
	public Slider imageOpacitySlider;

	// Token: 0x040002A4 RID: 676
	public Text imagePosXText;

	// Token: 0x040002A5 RID: 677
	public Text imagePosYText;

	// Token: 0x040002A6 RID: 678
	public Text imageSizeText;

	// Token: 0x040002A7 RID: 679
	public Text imageRotationText;

	// Token: 0x040002A8 RID: 680
	public Text imageOpacityText;

	// Token: 0x040002A9 RID: 681
	public GameObject audioReactorObj;

	// Token: 0x040002AA RID: 682
	public Toggle useAudioReactorToggle;

	// Token: 0x040002AB RID: 683
	public Toggle useAudioReactorOnVideoToggle;

	// Token: 0x040002AC RID: 684
	public Slider audioReactorPosXSlider;

	// Token: 0x040002AD RID: 685
	public Slider audioReactorPosYSlider;

	// Token: 0x040002AE RID: 686
	public Text audioReactorPosXText;

	// Token: 0x040002AF RID: 687
	public Text audioReactorPosYText;

	// Token: 0x040002B0 RID: 688
	public Toggle useVirtualLights;

	// Token: 0x040002B1 RID: 689
	public Toggle useHitEffectToggle;

	// Token: 0x040002B2 RID: 690
	public float midiStartSpeed;

	// Token: 0x040002B3 RID: 691
	public float lightIntensityValue = 1f;

	// Token: 0x040002B4 RID: 692
	public float lightYPosValue = 1f;

	// Token: 0x040002B5 RID: 693
	public GameObject tileObj;

	// Token: 0x040002B6 RID: 694
	public VisualEffect lightObj;

	// Token: 0x040002B7 RID: 695
	public Slider midiTempoSlider;

	// Token: 0x040002B8 RID: 696
	public Text midiTempoText;

	// Token: 0x040002B9 RID: 697
	public Slider virtualLightSizeSlider;

	// Token: 0x040002BA RID: 698
	public Text virtualLightSizeText;

	// Token: 0x040002BB RID: 699
	public Slider virtualLightIntensitySlider;

	// Token: 0x040002BC RID: 700
	public Text virtualLightIntensityText;

	// Token: 0x040002BD RID: 701
	public Slider virtualLightPosYSlider;

	// Token: 0x040002BE RID: 702
	public Text virtualLightPosYText;

	// Token: 0x040002BF RID: 703
	public Toggle useFogToggle;

	// Token: 0x040002C0 RID: 704
	public VisualEffect fog;

	// Token: 0x040002C1 RID: 705
	public Text userText;

	// Token: 0x040002C2 RID: 706
	public InputField userTextInput;

	// Token: 0x040002C3 RID: 707
	public Slider userTextPosXSlider;

	// Token: 0x040002C4 RID: 708
	public Slider userTextPosYSlider;

	// Token: 0x040002C5 RID: 709
	public Slider userTextSizeSlider;

	// Token: 0x040002C6 RID: 710
	public Slider userTextRedSlider;

	// Token: 0x040002C7 RID: 711
	public Slider userTextGreenSlider;

	// Token: 0x040002C8 RID: 712
	public Slider userTextBlueSlider;

	// Token: 0x040002C9 RID: 713
	public Text userTextPosXText;

	// Token: 0x040002CA RID: 714
	public Text userTextPosYText;

	// Token: 0x040002CB RID: 715
	public Text userTextSizeText;

	// Token: 0x040002CC RID: 716
	public Text userTextRedText;

	// Token: 0x040002CD RID: 717
	public Text userTextGreenText;

	// Token: 0x040002CE RID: 718
	public Text userTextBlueText;

	// Token: 0x040002CF RID: 719
	private Volume postVolume;

	// Token: 0x040002D0 RID: 720
	public GameObject postVolumeObject;

	// Token: 0x040002D1 RID: 721
	public Toggle usePostEffects;

	// Token: 0x040002D2 RID: 722
	public Slider exposureSlider;

	// Token: 0x040002D3 RID: 723
	public Slider contrastSlider;

	// Token: 0x040002D4 RID: 724
	public Slider saturationSlider;

	// Token: 0x040002D5 RID: 725
	public Text exposureText;

	// Token: 0x040002D6 RID: 726
	public Text contrastText;

	// Token: 0x040002D7 RID: 727
	public Text saturationText;
}
