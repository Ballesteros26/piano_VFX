using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000F RID: 15
public class HUDController : MonoBehaviour
{
	// Token: 0x0600006F RID: 111 RVA: 0x00007A4C File Offset: 0x00005C4C
	private void Update()
	{
		this.ScreenController();
		if (this.rendering || this.livePlay)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.H) && !this.userInputTextField.isFocused && !this.projectInputTextField.isFocused && !this.particleInputTextField.isFocused)
		{
			if (!this.hidden)
			{
				this.hidden = true;
				this.info.SetActive(false);
				this.playPause.SetActive(false);
				this.scroll.SetActive(false);
				this.editor.SetActive(false);
				return;
			}
			this.hidden = false;
			this.info.SetActive(true);
			this.playPause.SetActive(true);
			this.scroll.SetActive(true);
			this.editor.SetActive(true);
		}
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00007B20 File Offset: 0x00005D20
	public void LivePlayHide()
	{
		this.info.SetActive(false);
		this.playPause.SetActive(false);
		this.scroll.SetActive(false);
		this.editor.SetActive(false);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00007B54 File Offset: 0x00005D54
	private void ScreenController()
	{
		if (Input.GetKeyDown(KeyCode.S) && !this.userInputTextField.isFocused && !this.projectInputTextField.isFocused && !this.particleInputTextField.isFocused)
		{
			if (!this.changeResolution)
			{
				Screen.SetResolution(1280, 720, false);
			}
			else
			{
				Screen.SetResolution(1280, 720, true);
			}
		}
		if (Screen.fullScreen)
		{
			if (this.changeResolution)
			{
				this.changeResolution = false;
				Screen.SetResolution(PlayerPrefs.GetInt("UserScreenWidth"), PlayerPrefs.GetInt("UserScreenHeight"), true);
				return;
			}
		}
		else
		{
			this.changeResolution = true;
		}
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00007BF4 File Offset: 0x00005DF4
	public void SetNotesPlayed(int np)
	{
		this.notesPlayed = np;
		this.midiInfo.GetComponent<Text>().text = string.Concat(new string[]
		{
			"Notes played: ",
			this.notesPlayed.ToString(),
			Environment.NewLine,
			"Pedal: ",
			this.pedal
		});
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00007C54 File Offset: 0x00005E54
	public void IncreaseNotesPlayed()
	{
		this.notesPlayed++;
		this.midiInfo.GetComponent<Text>().text = string.Concat(new string[]
		{
			"Notes played: ",
			this.notesPlayed.ToString(),
			Environment.NewLine,
			"Pedal: ",
			this.pedal
		});
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00007CBC File Offset: 0x00005EBC
	public void SetPedalState(string state)
	{
		this.pedal = state;
		this.midiInfo.GetComponent<Text>().text = string.Concat(new string[]
		{
			"Notes played: ",
			this.notesPlayed.ToString(),
			Environment.NewLine,
			"Pedal: ",
			this.pedal
		});
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00007D1C File Offset: 0x00005F1C
	public void ChangePedalInfo()
	{
		if (this.pedal == "ON")
		{
			this.pedal = "OFF";
		}
		else
		{
			this.pedal = "ON";
		}
		this.midiInfo.GetComponent<Text>().text = string.Concat(new string[]
		{
			"Notes played: ",
			this.notesPlayed.ToString(),
			Environment.NewLine,
			"Pedal: ",
			this.pedal
		});
	}

	// Token: 0x0400015C RID: 348
	public GameObject info;

	// Token: 0x0400015D RID: 349
	public GameObject playPause;

	// Token: 0x0400015E RID: 350
	public GameObject scroll;

	// Token: 0x0400015F RID: 351
	public GameObject editor;

	// Token: 0x04000160 RID: 352
	private bool hidden;

	// Token: 0x04000161 RID: 353
	public int notesPlayed;

	// Token: 0x04000162 RID: 354
	public string pedal = "OFF";

	// Token: 0x04000163 RID: 355
	public Text midiInfo;

	// Token: 0x04000164 RID: 356
	public bool rendering;

	// Token: 0x04000165 RID: 357
	public bool livePlay;

	// Token: 0x04000166 RID: 358
	private bool changeResolution;

	// Token: 0x04000167 RID: 359
	public InputField userInputTextField;

	// Token: 0x04000168 RID: 360
	public InputField projectInputTextField;

	// Token: 0x04000169 RID: 361
	public InputField particleInputTextField;
}
