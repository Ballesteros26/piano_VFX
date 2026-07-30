using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001E RID: 30
public class PlayMidiSound : MonoBehaviour
{
	// Token: 0x06000126 RID: 294 RVA: 0x0000EEAE File Offset: 0x0000D0AE
	private void Awake()
	{
		this.userMidiSpeed = 2.5f;
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000EEBB File Offset: 0x0000D0BB
	private void Start()
	{
		this.outputDevice = OutputDevice.GetById(0);
		this.outputDevice.SendEvent(new ControlChangeEvent((SevenBitNumber)64, (SevenBitNumber)0));
		this.pe = this.editor.GetComponent<PianoEditor>();
	}

	// Token: 0x06000128 RID: 296 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
	public string[] GetDevices()
	{
		string[] array = new string[OutputDevice.GetDevicesCount()];
		int num = 0;
		foreach (OutputDevice outputDevice in OutputDevice.GetAll())
		{
			array[num] = outputDevice.Name;
			num++;
		}
		return array;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0000EF58 File Offset: 0x0000D158
	public void SetDevice(int deviceIndex)
	{
		this.outputDevice.Dispose();
		this.outputDevice = OutputDevice.GetById(deviceIndex);
		this.outputDevice.SendEvent(new ControlChangeEvent((SevenBitNumber)64, (SevenBitNumber)0));
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0000EF90 File Offset: 0x0000D190
	public void PlayMidiEvent(int parameter1, int parameter2, EventName eventName)
	{
		if (this.rendering || this.audioOn)
		{
			return;
		}
		if (eventName == EventName.VoiceControlChange)
		{
			this.outputDevice.SendEvent(new ControlChangeEvent((SevenBitNumber)((byte)parameter1), (SevenBitNumber)((byte)parameter2)));
			return;
		}
		if (eventName == EventName.VoiceNoteOn)
		{
			this.outputDevice.SendEvent(new NoteOnEvent((SevenBitNumber)((byte)parameter1), (SevenBitNumber)((byte)parameter2)));
			return;
		}
		if (eventName == EventName.VoiceNoteOff)
		{
			this.outputDevice.SendEvent(new NoteOffEvent((SevenBitNumber)((byte)parameter1), (SevenBitNumber)((byte)parameter2)));
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000F022 File Offset: 0x0000D222
	public void RenderPlay()
	{
		this.playing = true;
		this.playButton.image.sprite = this.pause;
		this.tilesObj.GetComponent<MoveTile>().speed = this.userMidiSpeed;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000F057 File Offset: 0x0000D257
	public void RenderStop()
	{
		this.playing = false;
		this.playButton.image.sprite = this.play;
		this.tilesObj.GetComponent<MoveTile>().speed = 0f;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000F08C File Offset: 0x0000D28C
	public void PlayPause()
	{
		if (this.rendering)
		{
			return;
		}
		if (this.playing)
		{
			if (this.audioOn)
			{
				this.pe.fm.audioSource.Pause();
			}
			if (this.videoOn)
			{
				this.pe.fm.videoPlayer.Pause();
			}
			this.RenderStop();
			return;
		}
		if (this.audioOn)
		{
			float num = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.audioOffset;
			if (num >= this.trimStart && num <= this.trimEnd)
			{
				this.pe.fm.audioSource.time = num;
				this.pe.fm.audioSource.Play();
			}
		}
		if (this.videoOn)
		{
			this.pe.fm.videoPlayer.Play();
			float num2 = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.videoOffset;
			if (num2 >= 0f && (double)num2 <= this.pe.fm.videoPlayer.length)
			{
				this.pe.fm.videoPlayer.time = (double)num2;
			}
			else
			{
				this.pe.fm.videoPlayer.Pause();
			}
		}
		this.RenderPlay();
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000F20F File Offset: 0x0000D40F
	public void StartScroll()
	{
		if (this.rendering)
		{
			return;
		}
		this.isScrolling = true;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000F224 File Offset: 0x0000D424
	public void EndScroll()
	{
		if (this.moveDown)
		{
			this.tilesObj.transform.position = new Vector3(this.tilesObj.transform.position.x, -(this.midiController.GetComponent<MidiController>().fileLength * this.progress.value), this.tilesObj.transform.position.z);
		}
		else
		{
			this.tilesObj.transform.position = new Vector3(this.tilesObj.transform.position.x, this.midiController.GetComponent<MidiController>().fileLength * this.progress.value, this.tilesObj.transform.position.z);
		}
		this.isScrolling = false;
		HUDController component = this.hudController.GetComponent<HUDController>();
		this.tileController.GetComponent<ActivateTile>().setNotesPlayed = true;
		component.SetPedalState("OFF");
		if (this.audioOn && !this.rendering)
		{
			float num = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.audioOffset;
			if (num >= this.trimStart && num <= this.trimEnd)
			{
				this.pe.fm.audioSource.time = num;
				if (this.playing && !this.pe.fm.audioSource.isPlaying)
				{
					this.pe.fm.audioSource.Play();
				}
			}
		}
		if (this.videoOn && !this.rendering)
		{
			float num2 = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.videoOffset;
			if (num2 >= 0f && (double)num2 <= this.pe.fm.videoPlayer.length)
			{
				this.pe.fm.videoPlayer.time = (double)num2;
				if (this.playing && !this.pe.fm.videoPlayer.isPlaying)
				{
					this.pe.fm.videoPlayer.Play();
				}
			}
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0000F480 File Offset: 0x0000D680
	public void UpdateProgress()
	{
		if (!this.isScrolling)
		{
			this.progress.value = Mathf.Abs(this.tilesObj.transform.position.y) / this.midiController.GetComponent<MidiController>().fileLength;
			if (this.audioOn && !this.rendering)
			{
				if (!this.pe.fm.audioSource.isPlaying)
				{
					float num = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.audioOffset;
					if (num >= this.trimStart && num <= this.trimEnd)
					{
						this.pe.fm.audioSource.time = num;
						this.pe.fm.audioSource.Play();
					}
				}
				else
				{
					float num2 = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.audioOffset;
					if (num2 < this.trimStart || num2 > this.trimEnd)
					{
						this.pe.fm.audioSource.Stop();
					}
				}
			}
			if (this.videoOn && !this.rendering)
			{
				if (!this.pe.fm.videoPlayer.isPlaying)
				{
					float num3 = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.videoOffset;
					if (num3 >= 0f && (double)num3 <= this.pe.fm.videoPlayer.length)
					{
						this.pe.fm.videoPlayer.time = (double)num3;
						this.pe.fm.videoPlayer.Play();
						return;
					}
				}
				else
				{
					float num4 = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.videoOffset;
					if (num4 < 0f)
					{
						this.pe.fm.videoPlayer.time = 0.0;
						this.pe.fm.videoPlayer.Pause();
						return;
					}
					if ((double)num4 > this.pe.fm.videoPlayer.length)
					{
						this.pe.fm.videoPlayer.time = this.pe.fm.videoPlayer.length;
						this.pe.fm.videoPlayer.Pause();
					}
				}
			}
		}
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000F753 File Offset: 0x0000D953
	public void StopMidiDevice()
	{
		this.outputDevice.Dispose();
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0000F760 File Offset: 0x0000D960
	public void ToggleAudio()
	{
		if (this.audioOn)
		{
			this.audioOn = false;
			this.pe.fm.audioSource.Stop();
			return;
		}
		this.audioOn = true;
		if (this.playing)
		{
			this.pe.fm.audioSource.Play();
		}
		float num = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.audioOffset;
		if (num >= this.trimStart && num <= this.trimEnd)
		{
			this.pe.fm.audioSource.time = num;
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000F814 File Offset: 0x0000DA14
	public void ToggleVideo()
	{
		if (this.videoOn)
		{
			this.videoOn = false;
			this.pe.fm.videoPlayer.Stop();
			this.pe.videoObject.SetActive(false);
			this.HideEveryChild(false, this.pe.pianoKeysWhite);
			this.HideEveryChild(false, this.pe.pianoKeysBlack);
			this.HideEveryChild(false, this.pe.pianoShadows);
			return;
		}
		this.pe.videoObject.SetActive(true);
		this.HideEveryChild(true, this.pe.pianoKeysWhite);
		this.HideEveryChild(true, this.pe.pianoKeysBlack);
		this.HideEveryChild(true, this.pe.pianoShadows);
		this.videoOn = true;
		if (this.playing)
		{
			this.pe.fm.videoPlayer.Play();
		}
		float num = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.videoOffset;
		if (num >= 0f && (double)num <= this.pe.fm.videoPlayer.length)
		{
			this.pe.fm.videoPlayer.time = (double)num;
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000F968 File Offset: 0x0000DB68
	public void HideEveryChild(bool hide, GameObject obj)
	{
		foreach (object obj2 in obj.transform)
		{
			Transform transform = (Transform)obj2;
			if (hide)
			{
				transform.GetComponent<SpriteRenderer>().enabled = false;
			}
			else
			{
				transform.GetComponent<SpriteRenderer>().enabled = true;
			}
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
	public void AudioOffsetChanges()
	{
		float num = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.audioOffset;
		if (num >= this.trimStart && num <= this.trimEnd)
		{
			this.pe.fm.audioSource.time = num;
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000FA44 File Offset: 0x0000DC44
	public void VideoOffsetChanges()
	{
		float num = this.tilesObj.transform.position.y * this.posMult / this.tilesObj.GetComponent<MoveTile>().speed + this.videoOffset;
		if (num >= 0f && (double)num <= this.pe.fm.videoPlayer.length)
		{
			this.pe.fm.videoPlayer.time = (double)num;
		}
	}

	// Token: 0x040002D8 RID: 728
	public bool moveDown;

	// Token: 0x040002D9 RID: 729
	public float posMult = -1f;

	// Token: 0x040002DA RID: 730
	private OutputDevice outputDevice;

	// Token: 0x040002DB RID: 731
	private bool playing = true;

	// Token: 0x040002DC RID: 732
	private float speedBefore;

	// Token: 0x040002DD RID: 733
	public float userMidiSpeed;

	// Token: 0x040002DE RID: 734
	public Button playButton;

	// Token: 0x040002DF RID: 735
	public Sprite pause;

	// Token: 0x040002E0 RID: 736
	public Sprite play;

	// Token: 0x040002E1 RID: 737
	public GameObject tilesObj;

	// Token: 0x040002E2 RID: 738
	public Slider progress;

	// Token: 0x040002E3 RID: 739
	public GameObject midiController;

	// Token: 0x040002E4 RID: 740
	private bool isScrolling;

	// Token: 0x040002E5 RID: 741
	public GameObject hudController;

	// Token: 0x040002E6 RID: 742
	public GameObject tileController;

	// Token: 0x040002E7 RID: 743
	public bool rendering;

	// Token: 0x040002E8 RID: 744
	public GameObject editor;

	// Token: 0x040002E9 RID: 745
	public PianoEditor pe;

	// Token: 0x040002EA RID: 746
	public bool audioOn;

	// Token: 0x040002EB RID: 747
	public float audioOffset;

	// Token: 0x040002EC RID: 748
	public float trimStart;

	// Token: 0x040002ED RID: 749
	public float trimEnd;

	// Token: 0x040002EE RID: 750
	public bool videoOn;

	// Token: 0x040002EF RID: 751
	public float videoOffset;
}
