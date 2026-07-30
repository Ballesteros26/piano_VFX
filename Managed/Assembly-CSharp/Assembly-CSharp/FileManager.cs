using System;
using System.Collections;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.Video;

// Token: 0x0200000E RID: 14
public class FileManager : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00003438 File Offset: 0x00001638
	private void Awake()
	{
		PlayerPrefs.SetInt("UserScreenWidth", Screen.currentResolution.width);
		PlayerPrefs.SetInt("UserScreenHeight", Screen.currentResolution.height);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00003474 File Offset: 0x00001674
	private void Start()
	{
		this.OpenMedia();
		this.audioSource = this.camera.GetComponent<AudioSource>();
		this.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("UserAudioVolume", 0f);
		this.videoPlayer = this.camera.GetComponent<VideoPlayer>();
		Time.captureDeltaTime = 0f;
		if (PlayerPrefs.GetString("Projects").Length > 0)
		{
			this.userProjects = JsonUtility.FromJson<ProjectSlots>(PlayerPrefs.GetString("Projects"));
		}
		else
		{
			this.userProjects = new ProjectSlots();
			this.userProjects.slot1 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
			this.userProjects.slot2 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
			this.userProjects.slot3 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
			this.userProjects.slot4 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
			this.userProjects.slot5 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
			this.userProjects.slot6 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
			this.userProjects.slot7 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
			this.userProjects.slot8 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
			this.userProjects.slot9 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
			this.userProjects.slot10 = new ProjectObject("Empty Project", "", "", "", "", "", "", "").GetJSON();
		}
		this.LoadUserProjectNames();
		this.filePath = PlayerPrefs.GetString("filePath");
		this.audioPath = PlayerPrefs.GetString("audioPath");
		this.videoPath = PlayerPrefs.GetString("videoPath");
		this.imagePath = PlayerPrefs.GetString("imagePath");
		if (this.filePath != null && this.filePath != "")
		{
			if (File.Exists(this.filePath))
			{
				this.filePathText.text = this.filePath;
				this.usingMidiFile = true;
			}
			else
			{
				this.filePathText.text = "File is missing.";
				this.filePath = "";
			}
		}
		if (this.audioPath != null && this.audioPath != "")
		{
			if (File.Exists(this.audioPath))
			{
				this.audioPathText.text = this.audioPath;
				base.StartCoroutine(this.OpenAudioFile());
			}
			else
			{
				this.audioPathText.text = "File is missing.";
				this.audioPath = "";
			}
		}
		if (this.videoPath != null && this.videoPath != "")
		{
			if (File.Exists(this.videoPath))
			{
				this.videoPathText.text = this.videoPath;
				this.OpenVideoFile();
			}
			else
			{
				this.videoPathText.text = "File is missing.";
				this.videoPath = "";
			}
		}
		if (this.imagePath != null && this.imagePath != "")
		{
			if (File.Exists(this.imagePath))
			{
				this.imagePathText.text = this.imagePath;
				this.OpenImageFile();
			}
			else
			{
				this.imagePathText.text = "File is missing.";
				this.imagePath = "";
			}
		}
		this.ChangeColorProfile("white");
		this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 0;
		this.blackTile.GetComponent<SpawnEffect>().effectIndex = 0;
		this.fire.GetComponent<VisualEffect>().Stop();
		this.turbulence.GetComponent<VisualEffect>().Stop();
		this.dust.GetComponent<VisualEffect>().Stop();
		this.glowBall.GetComponent<VisualEffect>().Stop();
		this.rousseau.GetComponent<VisualEffect>().Stop();
		this.smoke.GetComponent<VisualEffect>().Stop();
		this.plasma.GetComponent<VisualEffect>().Stop();
		this.patrik.GetComponent<VisualEffect>().Stop();
		this.userEffect.GetComponent<VisualEffect>().Stop();
		this.RefreshDevices();
		this.SelectAudioDevice();
		if (PlayerPrefs.GetString("ColorProfiles").Length > 0)
		{
			this.RestoreColorProfiles(PlayerPrefs.GetString("ColorProfiles"));
		}
		if (PlayerPrefs.GetString("UserValues").Length > 0)
		{
			this.RestoreUserValues(PlayerPrefs.GetString("UserValues"));
		}
		if (PlayerPrefs.GetString("LedValues").Length > 0)
		{
			this.ledObj.GetComponent<LEDController>().LoadLed(PlayerPrefs.GetString("LedValues"));
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003A60 File Offset: 0x00001C60
	private void Update()
	{
		if (this.isPlaying && Input.GetKeyDown(KeyCode.Escape))
		{
			if (this.renderObj.GetComponent<VideoRenderer>().renderingInProgress && !this.renderObj.GetComponent<VideoRenderer>().exitMessage && !this.renderObj.GetComponent<VideoRenderer>().writingAudio)
			{
				this.renderObj.GetComponent<VideoRenderer>().ExitRenderer();
			}
			else
			{
				this.soundObj.GetComponent<PlayMidiSound>().editor.GetComponent<PianoEditor>().SaveEditorValues();
				this.soundObj.GetComponent<PlayMidiSound>().StopMidiDevice();
				SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
			}
		}
		else if (this.recordingAudio)
		{
			if (Time.time - this.timeWhenRecordingStarted < (float)this.recordingTimeRemaining)
			{
				this.recordingAudioTime.text = "Time: " + (int)(Time.time - this.timeWhenRecordingStarted) + "s / 1800s";
			}
			else
			{
				this.StopAudioRecording();
			}
		}
		else if (this.isLivePlaying && Input.GetKeyDown(KeyCode.Escape))
		{
			this.ledObj.GetComponent<LEDController>().ClosePort();
			this.SaveColorProfiles();
			SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
		}
		if (this.recordingMidi)
		{
			this.recordingMidiTime.text = "Time: " + (int)(Time.time - this.timeWhenMidiRecordingStarted) + "s";
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003BC4 File Offset: 0x00001DC4
	public void SaveUserProjects(int selectedProject, string name, bool reset)
	{
		ProjectObject projectObject;
		if (reset)
		{
			projectObject = new ProjectObject("Empty Project", "", "", "", "", "", "", "");
		}
		else
		{
			projectObject = new ProjectObject(name, PlayerPrefs.GetString("filePath"), PlayerPrefs.GetString("audioPath"), PlayerPrefs.GetString("videoPath"), PlayerPrefs.GetString("imagePath"), PlayerPrefs.GetString("ColorProfiles"), PlayerPrefs.GetString("UserValues"), PlayerPrefs.GetString("EditorValues"));
		}
		switch (this.userProjects.selectedProfile)
		{
		case 0:
			this.userProjects.slot1 = projectObject.GetJSON();
			break;
		case 1:
			this.userProjects.slot2 = projectObject.GetJSON();
			break;
		case 2:
			this.userProjects.slot3 = projectObject.GetJSON();
			break;
		case 3:
			this.userProjects.slot4 = projectObject.GetJSON();
			break;
		case 4:
			this.userProjects.slot5 = projectObject.GetJSON();
			break;
		case 5:
			this.userProjects.slot6 = projectObject.GetJSON();
			break;
		case 6:
			this.userProjects.slot7 = projectObject.GetJSON();
			break;
		case 7:
			this.userProjects.slot8 = projectObject.GetJSON();
			break;
		case 8:
			this.userProjects.slot9 = projectObject.GetJSON();
			break;
		default:
			this.userProjects.slot10 = projectObject.GetJSON();
			break;
		}
		this.userProjects.selectedProfile = selectedProject;
		PlayerPrefs.SetString("Projects", JsonUtility.ToJson(this.userProjects));
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003D74 File Offset: 0x00001F74
	public void LoadUserProjectNames()
	{
		this.userProjectsDropdown.ClearOptions();
		for (int i = 0; i < 10; i++)
		{
			string text;
			switch (i)
			{
			case 0:
				text = this.userProjects.slot1;
				break;
			case 1:
				text = this.userProjects.slot2;
				break;
			case 2:
				text = this.userProjects.slot3;
				break;
			case 3:
				text = this.userProjects.slot4;
				break;
			case 4:
				text = this.userProjects.slot5;
				break;
			case 5:
				text = this.userProjects.slot6;
				break;
			case 6:
				text = this.userProjects.slot7;
				break;
			case 7:
				text = this.userProjects.slot8;
				break;
			case 8:
				text = this.userProjects.slot9;
				break;
			default:
				text = this.userProjects.slot10;
				break;
			}
			ProjectObject projectObject = JsonUtility.FromJson<ProjectObject>(text);
			string text2;
			if (projectObject == null)
			{
				text2 = "Empty Slot";
			}
			else
			{
				text2 = projectObject.name;
			}
			this.userProjectsDropdown.options.Add(new Dropdown.OptionData
			{
				text = text2
			});
			this.userProjectsDropdown.RefreshShownValue();
		}
		this.userProjectsDropdown.value = this.userProjects.selectedProfile;
		this.projectNameText.text = "Loaded project: " + this.userProjectsDropdown.options[this.userProjects.selectedProfile].text;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00003EE0 File Offset: 0x000020E0
	public void LoadUserProject()
	{
		if (this.userProjects.selectedProfile == this.userProjectsDropdown.value)
		{
			return;
		}
		string text;
		switch (this.userProjectsDropdown.value)
		{
		case 0:
			text = this.userProjects.slot1;
			break;
		case 1:
			text = this.userProjects.slot2;
			break;
		case 2:
			text = this.userProjects.slot3;
			break;
		case 3:
			text = this.userProjects.slot4;
			break;
		case 4:
			text = this.userProjects.slot5;
			break;
		case 5:
			text = this.userProjects.slot6;
			break;
		case 6:
			text = this.userProjects.slot7;
			break;
		case 7:
			text = this.userProjects.slot8;
			break;
		case 8:
			text = this.userProjects.slot9;
			break;
		default:
			text = this.userProjects.slot10;
			break;
		}
		ProjectObject projectObject = JsonUtility.FromJson<ProjectObject>(text);
		this.SaveUserProjects(this.userProjectsDropdown.value, this.userProjectsDropdown.options[this.userProjects.selectedProfile].text, false);
		PlayerPrefs.SetString("filePath", projectObject.midiPath);
		PlayerPrefs.SetString("audioPath", projectObject.audioPath);
		PlayerPrefs.SetString("videoPath", projectObject.videoPath);
		PlayerPrefs.SetString("imagePath", projectObject.imagePath);
		PlayerPrefs.SetString("ColorProfiles", projectObject.colorProfileData);
		PlayerPrefs.SetString("UserValues", projectObject.userValuesData);
		PlayerPrefs.SetString("EditorValues", projectObject.editorValuesData);
		this.soundObj.GetComponent<PlayMidiSound>().StopMidiDevice();
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x0000408C File Offset: 0x0000228C
	public void ChangeProjectName()
	{
		if (this.userProjectNameInputField.text.Length == 0)
		{
			return;
		}
		this.userProjectsDropdown.options[this.userProjects.selectedProfile].text = this.userProjectNameInputField.text;
		this.userProjectsDropdown.RefreshShownValue();
		this.projectNameText.text = "Loaded project: " + this.userProjectNameInputField.text;
		this.SaveUserProjects(this.userProjects.selectedProfile, this.userProjectsDropdown.options[this.userProjects.selectedProfile].text, false);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00004134 File Offset: 0x00002334
	public void OpenResetOption()
	{
		this.lastProjectText = this.projectNameText.text;
		this.projectNameText.text = "Are you sure you want to delete this project?";
		this.resetOptionObj.SetActive(true);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00004163 File Offset: 0x00002363
	public void CancelReset()
	{
		this.projectNameText.text = this.lastProjectText;
		this.resetOptionObj.SetActive(false);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00004184 File Offset: 0x00002384
	public void ResetProject()
	{
		string text;
		switch (this.userProjectsDropdown.value)
		{
		case 0:
			text = this.userProjects.slot1;
			break;
		case 1:
			text = this.userProjects.slot2;
			break;
		case 2:
			text = this.userProjects.slot3;
			break;
		case 3:
			text = this.userProjects.slot4;
			break;
		case 4:
			text = this.userProjects.slot5;
			break;
		case 5:
			text = this.userProjects.slot6;
			break;
		case 6:
			text = this.userProjects.slot7;
			break;
		case 7:
			text = this.userProjects.slot8;
			break;
		case 8:
			text = this.userProjects.slot9;
			break;
		default:
			text = this.userProjects.slot10;
			break;
		}
		JsonUtility.FromJson<ProjectObject>(text);
		this.SaveUserProjects(this.userProjectsDropdown.value, "Empty Project", true);
		PlayerPrefs.SetString("filePath", "");
		PlayerPrefs.SetString("audioPath", "");
		PlayerPrefs.SetString("videoPath", "");
		PlayerPrefs.SetString("imagePath", "");
		PlayerPrefs.SetString("ColorProfiles", "");
		PlayerPrefs.SetString("UserValues", "");
		PlayerPrefs.SetString("EditorValues", "");
		this.soundObj.GetComponent<PlayMidiSound>().StopMidiDevice();
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000042F8 File Offset: 0x000024F8
	public void OpenExplorer()
	{
		ExtensionFilter[] array = new ExtensionFilter[]
		{
			new ExtensionFilter("Midi File", new string[] { "mid", "midi" })
		};
		StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", array, false, delegate(string[] paths)
		{
			if (paths.Length == 0)
			{
				return;
			}
			this.usingMidiFile = true;
			this.filePath = paths[0];
			this.filePathText.text = paths[0];
			PlayerPrefs.SetString("filePath", paths[0]);
		});
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00004350 File Offset: 0x00002550
	public void OpenExplorerAudio()
	{
		ExtensionFilter[] array = new ExtensionFilter[]
		{
			new ExtensionFilter("Audio File", new string[] { "wav", "ogg" })
		};
		StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", array, false, delegate(string[] paths)
		{
			if (paths.Length == 0)
			{
				return;
			}
			this.audioPath = paths[0];
			this.audioPathText.text = paths[0];
			PlayerPrefs.SetString("audioPath", paths[0]);
			base.StartCoroutine(this.OpenAudioFile());
		});
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000043A8 File Offset: 0x000025A8
	public void OpenExplorerVideo()
	{
		ExtensionFilter[] array = new ExtensionFilter[]
		{
			new ExtensionFilter("Video File", new string[] { "mp4", "avi", "mov" })
		};
		StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", array, false, delegate(string[] paths)
		{
			if (paths.Length == 0)
			{
				return;
			}
			this.videoPath = paths[0];
			this.videoPathText.text = paths[0];
			PlayerPrefs.SetString("videoPath", paths[0]);
			this.OpenVideoFile();
		});
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00004408 File Offset: 0x00002608
	public void OpenExplorerImage()
	{
		ExtensionFilter[] array = new ExtensionFilter[]
		{
			new ExtensionFilter("Image File", new string[] { "png", "jpg", "jpeg" })
		};
		StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", array, false, delegate(string[] paths)
		{
			if (paths.Length == 0)
			{
				return;
			}
			this.imagePath = paths[0];
			this.imagePathText.text = paths[0];
			PlayerPrefs.SetString("imagePath", paths[0]);
			this.OpenImageFile();
		});
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004468 File Offset: 0x00002668
	public void SelectAudioToPath()
	{
		StandaloneFileBrowser.OpenFolderPanelAsync("Choose Path", "", false, delegate(string[] paths)
		{
			if (paths.Length == 0)
			{
				return;
			}
			this.audioToPath = paths[0];
			this.audioToPathText.text = paths[0];
		});
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00004486 File Offset: 0x00002686
	public void SelectMidiToPath()
	{
		StandaloneFileBrowser.OpenFolderPanelAsync("Save File", "", false, delegate(string[] selectedPaths)
		{
			if (selectedPaths.Length == 0)
			{
				return;
			}
			this.midiToPath = selectedPaths[0];
			this.midiToPathText.text = selectedPaths[0];
		});
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000044A4 File Offset: 0x000026A4
	public void PlayMidi()
	{
		bool portOpen = this.ledObj.GetComponent<LEDController>().portOpen;
		if (this.filePath != null && this.filePath != "" && !this.recordingAudio && !this.isRecordingMidi && !portOpen && this.usingMidiFile)
		{
			this.OpenDesign();
			this.particleEditor.GetComponent<ParticleEditor>().LoadLastEffect();
			this.isPlaying = true;
			this.renderObj.SetActive(false);
			this.renderGUIObj.SetActive(false);
			this.camera.transform.position = new Vector3(0f, 0f, -10f);
			this.midiController.GetComponent<MidiController>().GenerateTiles(this.filePath);
			this.moveTiles.GetComponent<MoveTile>().play = true;
			this.audioReactorObj.GetComponent<AudioVisualizer>().SpawnRing();
			this.audioReactorObj.SetActive(false);
			if (PlayerPrefs.GetString("EditorValues").Length > 0)
			{
				this.soundObj.GetComponent<PlayMidiSound>().editor.GetComponent<PianoEditor>().RestoreEditorValues(PlayerPrefs.GetString("EditorValues"));
			}
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000045DC File Offset: 0x000027DC
	public void LivePlay()
	{
		if (!this.recordingAudio && !this.isRecordingMidi && this.ledObj.GetComponent<LEDController>().GetDevices().Length != 0)
		{
			this.OpenDesign();
			this.particleEditor.GetComponent<ParticleEditor>().LoadLastEffect();
			this.isLivePlaying = true;
			this.renderObj.SetActive(false);
			this.renderGUIObj.SetActive(false);
			this.camera.transform.position = new Vector3(0f, 0f, -10f);
			this.hudControllerObj.GetComponent<HUDController>().livePlay = true;
			this.hudControllerObj.GetComponent<HUDController>().LivePlayHide();
			this.ledObj.GetComponent<LEDController>().SetInputDeviceLivePlay();
			int length = PlayerPrefs.GetString("EditorValues").Length;
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000046B4 File Offset: 0x000028B4
	public void RenderMidi()
	{
		bool portOpen = this.ledObj.GetComponent<LEDController>().portOpen;
		if (this.filePath != null && this.filePath != "" && !this.recordingAudio && !this.isRecordingMidi && !portOpen && this.usingMidiFile)
		{
			this.OpenDesign();
			this.particleEditor.GetComponent<ParticleEditor>().LoadLastEffect();
			this.EditorGuiObj.SetActive(false);
			this.hudControllerObj.GetComponent<HUDController>().rendering = true;
			this.rendering = true;
			this.infoObj.SetActive(false);
			this.isPlaying = true;
			this.camera.transform.position = new Vector3(0f, 0f, -10f);
			this.midiController.GetComponent<MidiController>().GenerateTiles(this.filePath);
			this.moveTiles.GetComponent<MoveTile>().play = true;
			this.audioReactorObj.GetComponent<AudioVisualizer>().SpawnRing();
			this.audioReactorObj.SetActive(false);
			if (PlayerPrefs.GetString("EditorValues").Length > 0)
			{
				try
				{
					this.soundObj.GetComponent<PlayMidiSound>().editor.GetComponent<PianoEditor>().RestoreEditorValues(PlayerPrefs.GetString("EditorValues"));
				}
				catch (Exception ex)
				{
					Debug.Log("Error loading user data: " + ex);
				}
			}
			this.renderObj.GetComponent<VideoRenderer>().PrepearRendering();
			this.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("UserAudioVolume", -80f);
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00004858 File Offset: 0x00002A58
	public IEnumerator OpenAudioFile()
	{
		string text = "file:///" + this.audioPath;
		using (WWW www = new WWW(text))
		{
			yield return www;
			try
			{
				this.audioSource.clip = www.GetAudioClip();
				this.usingAudioFile = true;
			}
			catch (Exception ex)
			{
				Debug.Log(ex);
				this.audioSource.clip = null;
			}
		}
		WWW www = null;
		yield break;
		yield break;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004868 File Offset: 0x00002A68
	public void OpenVideoFile()
	{
		string text = "file:///" + this.videoPath;
		this.videoPlayer.url = text;
		this.usingVideoFile = true;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000489C File Offset: 0x00002A9C
	public void OpenImageFile()
	{
		this.usingImageFile = true;
		byte[] array = File.ReadAllBytes(this.imagePath);
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.LoadImage(array);
		Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
		this.imageSprite.GetComponent<SpriteRenderer>().sprite = sprite;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00004910 File Offset: 0x00002B10
	public void SelectDevice()
	{
		this.soundObj.GetComponent<PlayMidiSound>().SetDevice(this.devices.value);
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000492D File Offset: 0x00002B2D
	public void SelectAudioDevice()
	{
		if (Microphone.devices.Length != 0)
		{
			this.audioDevice = Microphone.devices[this.audioDevices.value];
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00004950 File Offset: 0x00002B50
	public void RefreshDevices()
	{
		if (this.isRecordingAudio || this.isRecordingMidi || this.ledObj.GetComponent<LEDController>().portOpen)
		{
			return;
		}
		this.devices.options.Clear();
		this.audioDevices.options.Clear();
		this.inputDevices.options.Clear();
		this.serialPorts.options.Clear();
		foreach (string text in this.soundObj.GetComponent<PlayMidiSound>().GetDevices())
		{
			this.devices.options.Add(new Dropdown.OptionData
			{
				text = text
			});
		}
		foreach (string text2 in Microphone.devices)
		{
			this.audioDevices.options.Add(new Dropdown.OptionData
			{
				text = text2
			});
		}
		foreach (string text3 in this.ledObj.GetComponent<LEDController>().GetDevices())
		{
			this.inputDevices.options.Add(new Dropdown.OptionData
			{
				text = text3
			});
		}
		foreach (string text4 in this.ledObj.GetComponent<LEDController>().GetSerialPorts())
		{
			this.serialPorts.options.Add(new Dropdown.OptionData
			{
				text = text4
			});
		}
		this.devices.RefreshShownValue();
		this.audioDevices.RefreshShownValue();
		this.inputDevices.RefreshShownValue();
		this.serialPorts.RefreshShownValue();
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00004AE1 File Offset: 0x00002CE1
	public void ExitApp()
	{
		Application.Quit();
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00004AE8 File Offset: 0x00002CE8
	public void ToggleAudioRecording()
	{
		if (this.audioToPath == null || !(this.audioToPath != ""))
		{
			this.recordingAudioTime.text = "No path selected.";
			return;
		}
		if (!this.isRecordingAudio && Microphone.devices.Length != 0)
		{
			this.RecordAudio();
			this.isRecordingAudio = true;
			return;
		}
		if (this.isRecordingAudio && Microphone.devices.Length != 0)
		{
			this.StopAudioRecording();
			this.isRecordingAudio = false;
			return;
		}
		this.recordingAudioTime.text = "No audio input device.";
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00004B6C File Offset: 0x00002D6C
	public void ToggleMidiRecording()
	{
		if (this.midiToPath != null && this.midiToPath != "")
		{
			if (!this.isRecordingMidi && this.ledObj.GetComponent<LEDController>().GetDevices().Length != 0)
			{
				this.RecordMidi();
				return;
			}
			if (this.isRecordingMidi && this.ledObj.GetComponent<LEDController>().GetDevices().Length != 0)
			{
				this.StopMidiRecording();
				return;
			}
			if (this.ledObj.GetComponent<LEDController>().GetDevices().Length == 0)
			{
				this.recordingMidiTime.text = "No midi input device.";
				return;
			}
		}
		else
		{
			this.recordingMidiTime.text = "No path selected.";
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00004C0B File Offset: 0x00002E0B
	private void RecordAudio()
	{
		Debug.Log("Recording...");
		this.timeWhenRecordingStarted = Time.time;
		this.recordingAudio = true;
		this.recording = Microphone.Start(this.audioDevice, false, this.recordingTimeRemaining, 44100);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00004C48 File Offset: 0x00002E48
	private void RecordMidi()
	{
		if (this.ledObj.GetComponent<LEDController>().RecordMidi())
		{
			this.isRecordingMidi = true;
			this.recordingMidi = true;
			Debug.Log("Recording...");
			this.timeWhenMidiRecordingStarted = Time.time;
			return;
		}
		Debug.Log("No input device found!");
		this.recordingMidiTime.text = "No input device found!";
		this.isRecordingMidi = false;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00004CAC File Offset: 0x00002EAC
	private void StopAudioRecording()
	{
		if (Microphone.IsRecording(this.audioDevice))
		{
			this.recordingAudio = false;
			this.recordingTimeRemaining = 1800;
			int position = Microphone.GetPosition(null);
			if (position == 0)
			{
				return;
			}
			Debug.Log("Recording stopped.");
			Microphone.End(this.audioDevice);
			float[] array = new float[this.recording.samples];
			this.recording.GetData(array, 0);
			float[] array2 = new float[position];
			Array.Copy(array, array2, array2.Length - 1);
			this.recording = AudioClip.Create("playRecordClip", array2.Length, 1, 44100, false, false);
			this.recording.SetData(array2, 0);
			string text = SavWav.Save("audioFile", this.recording);
			Debug.Log(text);
			try
			{
				File.Move(text, this.audioToPath + "/Piano-VFX_" + DateTime.Now.ToString("MM_dd_yyyy_h_mm_ss") + ".wav");
				this.recordingAudioTime.text = "Recording finished. File moved to desired location.";
			}
			catch
			{
				this.recordingAudioTime.text = "File moving failed. Your file location: " + text;
			}
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00004DD8 File Offset: 0x00002FD8
	private void StopMidiRecording()
	{
		this.isRecordingMidi = false;
		if (this.ledObj.GetComponent<LEDController>().isRecordingMidi)
		{
			this.recordingMidi = false;
			Debug.Log("Recording stopped.");
			try
			{
				this.recordingMidiTime.text = this.ledObj.GetComponent<LEDController>().StopMidiRecording(this.midiToPath);
			}
			catch
			{
				this.recordingMidiTime.text = "File recording failed.";
			}
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00004E58 File Offset: 0x00003058
	public void HandleInputData()
	{
		switch (this.dropDown.value)
		{
		case 0:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 0;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 0;
			this.fire.GetComponent<VisualEffect>().Stop();
			this.turbulence.GetComponent<VisualEffect>().Stop();
			this.dust.GetComponent<VisualEffect>().Stop();
			this.glowBall.GetComponent<VisualEffect>().Stop();
			this.rousseau.GetComponent<VisualEffect>().Stop();
			this.smoke.GetComponent<VisualEffect>().Stop();
			this.plasma.GetComponent<VisualEffect>().Stop();
			this.patrik.GetComponent<VisualEffect>().Stop();
			this.userEffect.GetComponent<VisualEffect>().Stop();
			PlayerPrefs.SetInt("EffectIndex", 0);
			return;
		case 1:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 1;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 1;
			this.fire.GetComponent<VisualEffect>().Play();
			this.turbulence.GetComponent<VisualEffect>().Stop();
			this.dust.GetComponent<VisualEffect>().Stop();
			this.glowBall.GetComponent<VisualEffect>().Stop();
			this.rousseau.GetComponent<VisualEffect>().Stop();
			this.smoke.GetComponent<VisualEffect>().Stop();
			this.plasma.GetComponent<VisualEffect>().Stop();
			this.patrik.GetComponent<VisualEffect>().Stop();
			this.userEffect.GetComponent<VisualEffect>().Stop();
			PlayerPrefs.SetInt("EffectIndex", 1);
			return;
		case 2:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 2;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 2;
			this.fire.GetComponent<VisualEffect>().Stop();
			this.turbulence.GetComponent<VisualEffect>().Play();
			this.dust.GetComponent<VisualEffect>().Stop();
			this.glowBall.GetComponent<VisualEffect>().Stop();
			this.rousseau.GetComponent<VisualEffect>().Stop();
			this.smoke.GetComponent<VisualEffect>().Stop();
			this.plasma.GetComponent<VisualEffect>().Stop();
			this.patrik.GetComponent<VisualEffect>().Stop();
			this.userEffect.GetComponent<VisualEffect>().Stop();
			PlayerPrefs.SetInt("EffectIndex", 2);
			return;
		case 3:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 3;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 3;
			this.fire.GetComponent<VisualEffect>().Stop();
			this.turbulence.GetComponent<VisualEffect>().Stop();
			this.dust.GetComponent<VisualEffect>().Play();
			this.glowBall.GetComponent<VisualEffect>().Stop();
			this.rousseau.GetComponent<VisualEffect>().Stop();
			this.smoke.GetComponent<VisualEffect>().Stop();
			this.plasma.GetComponent<VisualEffect>().Stop();
			this.patrik.GetComponent<VisualEffect>().Stop();
			this.userEffect.GetComponent<VisualEffect>().Stop();
			PlayerPrefs.SetInt("EffectIndex", 3);
			return;
		case 4:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 4;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 4;
			this.fire.GetComponent<VisualEffect>().Stop();
			this.turbulence.GetComponent<VisualEffect>().Stop();
			this.dust.GetComponent<VisualEffect>().Stop();
			this.glowBall.GetComponent<VisualEffect>().Play();
			this.rousseau.GetComponent<VisualEffect>().Stop();
			this.smoke.GetComponent<VisualEffect>().Stop();
			this.plasma.GetComponent<VisualEffect>().Stop();
			this.patrik.GetComponent<VisualEffect>().Stop();
			this.userEffect.GetComponent<VisualEffect>().Stop();
			PlayerPrefs.SetInt("EffectIndex", 4);
			return;
		case 5:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 5;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 5;
			this.fire.GetComponent<VisualEffect>().Stop();
			this.turbulence.GetComponent<VisualEffect>().Stop();
			this.dust.GetComponent<VisualEffect>().Stop();
			this.glowBall.GetComponent<VisualEffect>().Stop();
			this.rousseau.GetComponent<VisualEffect>().Play();
			this.smoke.GetComponent<VisualEffect>().Stop();
			this.plasma.GetComponent<VisualEffect>().Stop();
			this.patrik.GetComponent<VisualEffect>().Stop();
			this.userEffect.GetComponent<VisualEffect>().Stop();
			PlayerPrefs.SetInt("EffectIndex", 5);
			return;
		case 6:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 6;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 6;
			this.fire.GetComponent<VisualEffect>().Stop();
			this.turbulence.GetComponent<VisualEffect>().Stop();
			this.dust.GetComponent<VisualEffect>().Stop();
			this.glowBall.GetComponent<VisualEffect>().Stop();
			this.rousseau.GetComponent<VisualEffect>().Stop();
			this.smoke.GetComponent<VisualEffect>().Play();
			this.plasma.GetComponent<VisualEffect>().Stop();
			this.patrik.GetComponent<VisualEffect>().Stop();
			this.userEffect.GetComponent<VisualEffect>().Stop();
			PlayerPrefs.SetInt("EffectIndex", 6);
			return;
		case 7:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 7;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 7;
			this.fire.GetComponent<VisualEffect>().Stop();
			this.turbulence.GetComponent<VisualEffect>().Stop();
			this.dust.GetComponent<VisualEffect>().Stop();
			this.glowBall.GetComponent<VisualEffect>().Stop();
			this.rousseau.GetComponent<VisualEffect>().Stop();
			this.smoke.GetComponent<VisualEffect>().Stop();
			this.plasma.GetComponent<VisualEffect>().Play();
			this.patrik.GetComponent<VisualEffect>().Stop();
			this.userEffect.GetComponent<VisualEffect>().Stop();
			PlayerPrefs.SetInt("EffectIndex", 7);
			return;
		case 8:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 8;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 8;
			this.fire.GetComponent<VisualEffect>().Stop();
			this.turbulence.GetComponent<VisualEffect>().Stop();
			this.dust.GetComponent<VisualEffect>().Stop();
			this.glowBall.GetComponent<VisualEffect>().Stop();
			this.rousseau.GetComponent<VisualEffect>().Stop();
			this.smoke.GetComponent<VisualEffect>().Stop();
			this.plasma.GetComponent<VisualEffect>().Stop();
			this.patrik.GetComponent<VisualEffect>().Play();
			this.userEffect.GetComponent<VisualEffect>().Stop();
			PlayerPrefs.SetInt("EffectIndex", 8);
			return;
		default:
			this.whiteTile.GetComponent<SpawnEffect>().effectIndex = 9;
			this.blackTile.GetComponent<SpawnEffect>().effectIndex = 9;
			this.fire.GetComponent<VisualEffect>().Stop();
			this.turbulence.GetComponent<VisualEffect>().Stop();
			this.dust.GetComponent<VisualEffect>().Stop();
			this.glowBall.GetComponent<VisualEffect>().Stop();
			this.rousseau.GetComponent<VisualEffect>().Stop();
			this.smoke.GetComponent<VisualEffect>().Stop();
			this.plasma.GetComponent<VisualEffect>().Stop();
			this.patrik.GetComponent<VisualEffect>().Stop();
			this.userEffect.GetComponent<VisualEffect>().Play();
			PlayerPrefs.SetInt("EffectIndex", 9);
			return;
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00005610 File Offset: 0x00003810
	public void HandleReactorData()
	{
		int value = this.reactorDropDown.value;
		if (value == 0)
		{
			this.audioReactorObj.GetComponent<AudioVisualizer>().visualizationMode = VisualizationMode.Line;
			return;
		}
		if (value != 1)
		{
			return;
		}
		this.audioReactorObj.GetComponent<AudioVisualizer>().visualizationMode = VisualizationMode.Ring;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00005654 File Offset: 0x00003854
	public void HandleReactorSize()
	{
		switch (this.reactorSizeDropDown.value)
		{
		case 0:
			this.audioReactorObj.GetComponent<AudioVisualizer>().sizeIndex = 5f;
			return;
		case 1:
			this.audioReactorObj.GetComponent<AudioVisualizer>().sizeIndex = 2.5f;
			return;
		case 2:
			this.audioReactorObj.GetComponent<AudioVisualizer>().sizeIndex = 1f;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000056C4 File Offset: 0x000038C4
	public void ChangeColorProfile(string colorName)
	{
		if (colorName == "white")
		{
			this.fire.SetGradient("Gradient", this.white);
			this.turbulence.SetGradient("Gradient", this.white);
			this.dust.SetGradient("Gradient", this.white);
			this.glowBall.SetGradient("Gradient", this.white);
			this.rousseau.SetGradient("Gradient", this.white);
			this.smoke.SetGradient("Gradient", this.white);
			this.plasma.SetGradient("Gradient", this.white);
			this.redBar.GetComponent<Renderer>().material.SetVector("_BarColor", this.whiteC * 10f);
			this.redBarPrev.GetComponent<Renderer>().material.SetVector("_BarColor", this.whiteC * 10f);
			this.prevKey.GetComponent<SpriteRenderer>().color = this.whiteC;
			this.whiteTile.GetComponent<SpawnEffect>().activeColor = this.whiteC;
			this.blackTile.GetComponent<SpawnEffect>().activeColor = this.whiteC * 0.6f;
			this.blackTile.GetComponent<SpawnEffect>().activeColor.a = 1f;
			this.graphGlow.SetVector("_GlowColor", this.whiteC * 10f);
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00005864 File Offset: 0x00003A64
	public void SaveColorProfiles()
	{
		string text = "";
		GameObject[] array = GameObject.FindGameObjectsWithTag("ColorProfileApplied");
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("ColorProfile");
		GameObject[] array3 = new GameObject[array2.Length + array.Length];
		Array.Copy(array, array3, array.Length);
		Array.Copy(array2, 0, array3, array.Length, array2.Length);
		foreach (GameObject gameObject in array3)
		{
			text = text + "|" + gameObject.GetComponent<ApplyColorProfile>().ProfileToJSON();
		}
		PlayerPrefs.SetString("ColorProfiles", text);
		this.SaveUserValues();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000058F8 File Offset: 0x00003AF8
	public void SaveUserValues()
	{
		string text = JsonUtility.ToJson(new UserValues
		{
			effectIndex = this.dropDown.value,
			redBarAnim = this.isAnimatedRedBar,
			redBarRed = this.redBarRed,
			redBarGreen = this.redBarGreen,
			redBarBlue = this.redBarBlue,
			redBarGlow = this.redBarGlow,
			TileColorTransition = this.allowColorTrans,
			transitionRed = this.colorTransRed,
			transitionGreen = this.colorTransGreen,
			transitionBlue = this.colorTransBlue,
			transitionGlow = this.colorTransGlow,
			textureValue = this.animatedTextures.value,
			animTexRed = this.tileEffectRed,
			animTexGreen = this.tileEffectGreen,
			animTexBlue = this.tileEffectBlue,
			animTexGlow = this.tileEffectGlow,
			backRed = this.colorBackRed,
			backGreen = this.colorBackGreen,
			backBlue = this.colorBackBlue,
			midiInfo = this.allowMidiInfo,
			reactorType = this.reactorDropDown.value,
			reactorSize = this.reactorSizeDropDown.value,
			reactorRed = this.reactorRedSlider.value,
			reactorGreen = this.reactorGreenSlider.value,
			reactorBlue = this.reactorBlueSlider.value,
			reactorGlow = this.reactorGlowSlider.value,
			reverseAnimation = this.reverseAnimationToggle.isOn,
			noteLength = this.noteLengthSlider.value,
			keyDarnkess = this.keyDarnkess.value,
			tileRoundness = this.tileRoundnessSlider.value,
			tileOutline = this.tileOutlineSlider.value
		});
		PlayerPrefs.SetString("UserValues", text);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00005AD8 File Offset: 0x00003CD8
	private void RestoreColorProfiles(string json)
	{
		foreach (string text in json.Split(new char[] { '|' }))
		{
			if (text.Length > 0)
			{
				ColorProfileObject colorProfileObject = JsonUtility.FromJson<ColorProfileObject>(text);
				this.colorControllerObj.GetComponent<ColorController>().LoadColorProfiles(colorProfileObject);
			}
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00005B2C File Offset: 0x00003D2C
	private void RestoreUserValues(string json)
	{
		UserValues userValues = new UserValues();
		userValues = JsonUtility.FromJson<UserValues>(json);
		this.dropDown.value = userValues.effectIndex;
		this.redBarRed = userValues.redBarRed;
		this.redBarGreen = userValues.redBarGreen;
		this.redBarBlue = userValues.redBarBlue;
		this.redBarGlow = userValues.redBarGlow;
		this.colorTransRed = userValues.transitionRed;
		this.colorTransGreen = userValues.transitionGreen;
		this.colorTransBlue = userValues.transitionBlue;
		this.colorTransGlow = userValues.transitionGlow;
		this.tileEffectRed = userValues.animTexRed;
		this.tileEffectGreen = userValues.animTexGreen;
		this.tileEffectBlue = userValues.animTexBlue;
		this.tileEffectGlow = userValues.animTexGlow;
		this.colorBackRed = userValues.backRed;
		this.colorBackGreen = userValues.backGreen;
		this.colorBackBlue = userValues.backBlue;
		this.redBarAnim.isOn = userValues.redBarAnim;
		Debug.Log("red bar " + userValues.redBarAnim.ToString());
		this.colorTrans.isOn = userValues.TileColorTransition;
		this.animatedTextures.value = userValues.textureValue;
		this.redBarRedSlider.value = this.redBarRed;
		this.redBarGreenSlider.value = this.redBarGreen;
		this.redBarBlueSlider.value = this.redBarBlue;
		this.redBarGlowSlider.value = this.redBarGlow;
		this.colorTransRedSlider.value = this.colorTransRed;
		this.colorTransGreenSlider.value = this.colorTransGreen;
		this.colorTransBlueSlider.value = this.colorTransBlue;
		this.colorTransGlowSlider.value = this.colorTransGlow;
		this.tileEffectRedSlider.value = this.tileEffectRed;
		this.tileEffectGreenSlider.value = this.tileEffectGreen;
		this.tileEffectBlueSlider.value = this.tileEffectBlue;
		this.tileEffectGlowSlider.value = this.tileEffectGlow;
		this.colorBackRedSlider.value = this.colorBackRed;
		this.colorBackGreenSlider.value = this.colorBackGreen;
		this.colorBackBlueSlider.value = this.colorBackBlue;
		this.midiInfo.isOn = userValues.midiInfo;
		this.reactorDropDown.value = userValues.reactorType;
		this.reactorSizeDropDown.value = userValues.reactorSize;
		this.reactorRedSlider.value = userValues.reactorRed;
		this.reactorGreenSlider.value = userValues.reactorGreen;
		this.reactorBlueSlider.value = userValues.reactorBlue;
		this.reactorGlowSlider.value = userValues.reactorGlow;
		this.reverseAnimationToggle.isOn = userValues.reverseAnimation;
		if (userValues.noteLength < 0.5f)
		{
			this.noteLengthSlider.value = 1f;
		}
		else
		{
			this.noteLengthSlider.value = userValues.noteLength;
		}
		this.keyDarnkess.value = userValues.keyDarnkess;
		this.tileRoundnessSlider.value = userValues.tileRoundness;
		this.tileOutlineSlider.value = userValues.tileOutline;
		this.outlineTex = true;
		this.ChangeTileTexture();
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00005E4F File Offset: 0x0000404F
	public void OpenTutorial()
	{
		Application.OpenURL("https://www.piano-vfx.com/manual/");
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00005E5B File Offset: 0x0000405B
	public void OpenEffectBrowser()
	{
		Application.OpenURL("https://www.piano-vfx.com/manual/effects.php");
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00005E68 File Offset: 0x00004068
	public void ChangeRedBarColor(string color)
	{
		if (!(color == "red"))
		{
			if (!(color == "green"))
			{
				if (!(color == "blue"))
				{
					if (color == "glow")
					{
						this.redBarGlow = this.redBarGlowSlider.value;
					}
				}
				else
				{
					this.redBarBlue = this.redBarBlueSlider.value;
				}
			}
			else
			{
				this.redBarGreen = this.redBarGreenSlider.value;
			}
		}
		else
		{
			this.redBarRed = this.redBarRedSlider.value;
		}
		this.redBarPrev.GetComponent<Renderer>().material.SetVector("_BarColor", new Color(this.redBarRed, this.redBarGreen, this.redBarBlue) * this.redBarGlow * 1300f);
		this.animatedRedBarPrev.GetComponent<Renderer>().material.SetVector("_SaberColor", new Color(this.redBarRed, this.redBarGreen, this.redBarBlue) * this.redBarGlow * 1300f);
		this.redBar.GetComponent<Renderer>().material.SetVector("_BarColor", new Color(this.redBarRed, this.redBarGreen, this.redBarBlue) * this.redBarGlow * 1300f);
		this.animatedRedBar.GetComponent<Renderer>().material.SetVector("_SaberColor", new Color(this.redBarRed, this.redBarGreen, this.redBarBlue) * this.redBarGlow * 1300f);
		this.fogColor = new Color(this.redBarRed, this.redBarGreen, this.redBarBlue);
		Color color2 = new Color(this.redBarRed, this.redBarGreen, this.redBarBlue);
		color2.a = 0.1f;
		this.lightObj.GetComponent<SpriteRenderer>().color = color2;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00006074 File Offset: 0x00004274
	public void ChangeReactorColor(string color)
	{
		if (!(color == "red"))
		{
			if (!(color == "green"))
			{
				if (!(color == "blue"))
				{
					if (color == "glow")
					{
						this.reactorGlow = this.reactorGlowSlider.value;
					}
				}
				else
				{
					this.reactorBlue = this.reactorBlueSlider.value;
				}
			}
			else
			{
				this.reactorGreen = this.reactorGreenSlider.value;
			}
		}
		else
		{
			this.reactorRed = this.reactorRedSlider.value;
		}
		Color color2 = new Color(this.reactorRed, this.reactorGreen, this.reactorBlue) * this.reactorGlow * 130f;
		this.audioReactorPrevObj.GetComponent<Renderer>().material.SetVector("_GlowColor", color2);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00006150 File Offset: 0x00004350
	public void HandleTextureInputData()
	{
		switch (this.animatedTextures.value)
		{
		case 0:
			this.RestoreTexture();
			this.whiteTile.GetComponent<Renderer>().material = this.defaultMaterial;
			this.blackTile.GetComponent<Renderer>().material = this.defaultMaterial;
			this.prevTile.GetComponent<Renderer>().material = this.defaultMaterial;
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_Tiling", this.prevTile.transform.localScale);
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerTiling", new Vector2(1f, this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x));
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerOffset", new Vector2(0f, (1f - this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x) / 2f));
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerHeight", this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_BorderSize", this.tileOutlineSlider.value);
			break;
		case 1:
			this.RemoveTexture();
			break;
		case 2:
			this.RestoreTexture();
			this.whiteTile.GetComponent<Renderer>().material = this.animatedMaterial;
			this.blackTile.GetComponent<Renderer>().material = this.animatedMaterial;
			this.prevTile.GetComponent<Renderer>().material = this.animatedMaterial;
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_Tiling", this.prevTile.transform.localScale);
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerTiling", new Vector2(1f, this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x));
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerOffset", new Vector2(0f, (1f - this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x) / 2f));
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerHeight", this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_BorderSize", this.tileOutlineSlider.value);
			break;
		case 3:
			this.RestoreTexture();
			this.whiteTile.GetComponent<Renderer>().material = this.animatedMaterial2;
			this.blackTile.GetComponent<Renderer>().material = this.animatedMaterial2;
			this.prevTile.GetComponent<Renderer>().material = this.animatedMaterial2;
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_Tiling", this.prevTile.transform.localScale);
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerTiling", new Vector2(1f, this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x));
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerOffset", new Vector2(0f, (1f - this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x) / 2f));
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerHeight", this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_BorderSize", this.tileOutlineSlider.value);
			break;
		case 4:
			this.RestoreTexture();
			this.whiteTile.GetComponent<Renderer>().material = this.animatedMaterial3;
			this.blackTile.GetComponent<Renderer>().material = this.animatedMaterial3;
			this.prevTile.GetComponent<Renderer>().material = this.animatedMaterial3;
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_Tiling", this.prevTile.transform.localScale);
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerTiling", new Vector2(1f, this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x));
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerOffset", new Vector2(0f, (1f - this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x) / 2f));
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerHeight", this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_BorderSize", this.tileOutlineSlider.value);
			break;
		case 5:
			this.RestoreTexture();
			this.whiteTile.GetComponent<Renderer>().material = this.animatedMaterial4;
			this.blackTile.GetComponent<Renderer>().material = this.animatedMaterial4;
			this.prevTile.GetComponent<Renderer>().material = this.animatedMaterial4;
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_Tiling", new Vector2(this.prevTile.transform.localScale.x + 1.7f, this.prevTile.transform.localScale.y));
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerTiling", new Vector2(1f, this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x));
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_CornerOffset", new Vector2(0f, (1f - this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x) / 2f));
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerHeight", this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
			this.prevTile.transform.GetComponent<Renderer>().material.SetFloat("_BorderSize", this.tileOutlineSlider.value);
			break;
		}
		if (this.lastActiveProfile != null)
		{
			this.lastActiveProfile.GetComponent<ApplyColorProfile>().UpdatePreview();
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00006B80 File Offset: 0x00004D80
	public void ChangeTileMaterial()
	{
		this.animatedMaterial.SetVector("_DissolveColor", new Color(this.tileEffectRed, this.tileEffectGreen, this.tileEffectBlue) * this.tileEffectGlow * 130f);
		if (!this.animatedTex)
		{
			this.animatedTex = true;
			this.whiteTile.GetComponent<Renderer>().material = this.animatedMaterial;
			this.blackTile.GetComponent<Renderer>().material = this.animatedMaterial;
			this.prevTile.GetComponent<Renderer>().material = this.animatedMaterial;
			this.prevTile.transform.GetComponent<Renderer>().material.SetVector("_Tiling", this.prevTile.transform.localScale);
		}
		else
		{
			this.animatedTex = false;
			this.whiteTile.GetComponent<Renderer>().material = this.defaultMaterial;
			this.blackTile.GetComponent<Renderer>().material = this.defaultMaterial;
			this.prevTile.GetComponent<Renderer>().material = this.defaultMaterial;
		}
		if (this.lastActiveProfile != null)
		{
			this.lastActiveProfile.GetComponent<ApplyColorProfile>().UpdatePreview();
		}
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00006CBC File Offset: 0x00004EBC
	public void RemoveTexture()
	{
		this.whiteTile.GetComponent<SpriteRenderer>().enabled = false;
		this.blackTile.GetComponent<SpriteRenderer>().enabled = false;
		this.prevTile.GetComponent<SpriteRenderer>().enabled = false;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00006CF1 File Offset: 0x00004EF1
	public void RestoreTexture()
	{
		this.whiteTile.GetComponent<SpriteRenderer>().enabled = true;
		this.blackTile.GetComponent<SpriteRenderer>().enabled = true;
		this.prevTile.GetComponent<SpriteRenderer>().enabled = true;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00006D28 File Offset: 0x00004F28
	public void ChangeTileTexture()
	{
		if (!this.outlineTex)
		{
			this.outlineTex = true;
			this.whiteTile.GetComponent<SpriteRenderer>().sprite = this.outlineTexture;
			this.blackTile.GetComponent<SpriteRenderer>().sprite = this.outlineTexture;
			this.prevTile.GetComponent<SpriteRenderer>().sprite = this.prevOutlineTexture;
			return;
		}
		this.outlineTex = false;
		this.whiteTile.GetComponent<SpriteRenderer>().sprite = this.defaultTexture;
		this.blackTile.GetComponent<SpriteRenderer>().sprite = this.defaultTexture;
		this.prevTile.GetComponent<SpriteRenderer>().sprite = this.defaultTexture;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00006DD0 File Offset: 0x00004FD0
	public void ChangeRedBar()
	{
		if (!this.isAnimatedRedBar)
		{
			this.isAnimatedRedBar = true;
			this.animatedRedBarPrev.SetActive(true);
			this.animatedRedBar.SetActive(true);
			this.redBar.GetComponent<SpriteRenderer>().enabled = false;
			this.redBarPrev.SetActive(false);
			return;
		}
		this.isAnimatedRedBar = false;
		this.animatedRedBarPrev.SetActive(false);
		this.animatedRedBar.SetActive(false);
		this.redBar.GetComponent<SpriteRenderer>().enabled = true;
		this.redBarPrev.SetActive(true);
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00006E60 File Offset: 0x00005060
	public void AllowColorTransition()
	{
		if (!this.allowColorTrans)
		{
			this.allowColorTrans = true;
			this.whiteTile.GetComponent<SpawnEffect>().allowColorTransition = true;
			this.blackTile.GetComponent<SpawnEffect>().allowColorTransition = true;
			this.colorTransObj.SetActive(true);
			this.colorTransObj.GetComponent<Renderer>().material.SetVector("_GlowColor", this.transitionColor);
			return;
		}
		this.allowColorTrans = false;
		this.whiteTile.GetComponent<SpawnEffect>().allowColorTransition = false;
		this.blackTile.GetComponent<SpawnEffect>().allowColorTransition = false;
		this.colorTransObj.SetActive(false);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00006F08 File Offset: 0x00005108
	public void ChangeTileAnimationColor(string color)
	{
		if (!(color == "red"))
		{
			if (!(color == "green"))
			{
				if (!(color == "blue"))
				{
					if (color == "glow")
					{
						this.tileEffectGlow = this.tileEffectGlowSlider.value;
					}
				}
				else
				{
					this.tileEffectBlue = this.tileEffectBlueSlider.value;
				}
			}
			else
			{
				this.tileEffectGreen = this.tileEffectGreenSlider.value;
			}
		}
		else
		{
			this.tileEffectRed = this.tileEffectRedSlider.value;
		}
		this.animatedMaterial.SetVector("_DissolveColor", new Color(this.tileEffectRed, this.tileEffectGreen, this.tileEffectBlue) * this.tileEffectGlow * 130f);
		this.animatedMaterial2.SetVector("_DissolveColor", new Color(this.tileEffectRed, this.tileEffectGreen, this.tileEffectBlue) * this.tileEffectGlow * 130f);
		this.animatedMaterial3.SetVector("_DissolveColor", new Color(this.tileEffectRed, this.tileEffectGreen, this.tileEffectBlue) * this.tileEffectGlow * 130f);
		this.animatedMaterial4.SetVector("_DissolveColor", new Color(this.tileEffectRed, this.tileEffectGreen, this.tileEffectBlue) * this.tileEffectGlow * 130f);
		if (this.tileEffectRed + this.tileEffectGreen + this.tileEffectBlue + this.tileEffectGlow == 0f)
		{
			this.animatedMaterial4.SetVector("_DissolveColor", Color.white);
		}
		this.HandleTextureInputData();
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000070DC File Offset: 0x000052DC
	public void ChangeTransitionColor(string color)
	{
		if (!(color == "red"))
		{
			if (!(color == "green"))
			{
				if (!(color == "blue"))
				{
					if (color == "glow")
					{
						this.colorTransGlow = this.colorTransGlowSlider.value;
					}
				}
				else
				{
					this.colorTransBlue = this.colorTransBlueSlider.value;
				}
			}
			else
			{
				this.colorTransGreen = this.colorTransGreenSlider.value;
			}
		}
		else
		{
			this.colorTransRed = this.colorTransRedSlider.value;
		}
		this.transitionColor = new Color(this.colorTransRed, this.colorTransGreen, this.colorTransBlue) * this.colorTransGlow * 130f;
		this.colorTransObj.GetComponent<Renderer>().material.SetVector("_GlowColor", this.transitionColor);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x000071C0 File Offset: 0x000053C0
	public void ChangeBackgroundColor(string color)
	{
		if (!(color == "red"))
		{
			if (!(color == "green"))
			{
				if (color == "blue")
				{
					this.colorBackBlue = this.colorBackBlueSlider.value;
				}
			}
			else
			{
				this.colorBackGreen = this.colorBackGreenSlider.value;
			}
		}
		else
		{
			this.colorBackRed = this.colorBackRedSlider.value;
		}
		Color color2 = new Color(this.colorBackRed, this.colorBackGreen, this.colorBackBlue);
		this.background.GetComponent<SpriteRenderer>().color = color2;
		this.backgroundPrev.GetComponent<SpriteRenderer>().color = color2;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00007268 File Offset: 0x00005468
	public void AllowMidiInfo()
	{
		if (!this.allowMidiInfo)
		{
			this.allowMidiInfo = true;
			this.midiInfoObj.SetActive(true);
			return;
		}
		this.allowMidiInfo = false;
		this.midiInfoObj.SetActive(false);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000729C File Offset: 0x0000549C
	public void ReverseAnimation()
	{
		if (this.reverseAnimationToggle.isOn)
		{
			this.midiController.GetComponent<MidiController>().moveDown = false;
			this.moveTiles.GetComponent<MoveTile>().moveDown = false;
			this.moveTiles.GetComponent<ActivateTile>().moveDown = false;
			this.midiPlayer.GetComponent<PlayMidiSound>().moveDown = false;
			this.midiPlayer.GetComponent<PlayMidiSound>().posMult = 1f;
			this.whiteTile.GetComponent<SpawnEffect>().moveDown = false;
			this.blackTile.GetComponent<SpawnEffect>().moveDown = false;
			return;
		}
		this.midiController.GetComponent<MidiController>().moveDown = true;
		this.moveTiles.GetComponent<MoveTile>().moveDown = true;
		this.moveTiles.GetComponent<ActivateTile>().moveDown = true;
		this.midiPlayer.GetComponent<PlayMidiSound>().moveDown = true;
		this.midiPlayer.GetComponent<PlayMidiSound>().posMult = -1f;
		this.whiteTile.GetComponent<SpawnEffect>().moveDown = true;
		this.blackTile.GetComponent<SpawnEffect>().moveDown = true;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000073B0 File Offset: 0x000055B0
	public void OpenDesign()
	{
		this.effectObj.transform.position = new Vector3(5.021f, 0.182f, 0f);
		this.effectObj.SetActive(true);
		this.designObj.SetActive(true);
		this.mediaObj.SetActive(false);
		this.particleObj.SetActive(false);
		this.textureObj.SetActive(false);
		this.HandleInputData();
		this.HandleTextureInputData();
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0000742C File Offset: 0x0000562C
	public void OpenMedia()
	{
		this.effectObj.transform.position = new Vector3(-1800.0232f, -1.982f, 0f);
		this.designObj.SetActive(false);
		this.mediaObj.SetActive(true);
		this.particleObj.SetActive(false);
		this.textureObj.SetActive(false);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00007490 File Offset: 0x00005690
	public void OpenParticleCreator()
	{
		this.effectObj.transform.position = new Vector3(5.021f, 0.182f, 0f);
		this.effectObj.SetActive(true);
		this.designObj.SetActive(false);
		this.mediaObj.SetActive(false);
		this.particleObj.SetActive(true);
		this.textureObj.SetActive(false);
		this.fire.GetComponent<VisualEffect>().Stop();
		this.turbulence.GetComponent<VisualEffect>().Stop();
		this.dust.GetComponent<VisualEffect>().Stop();
		this.glowBall.GetComponent<VisualEffect>().Stop();
		this.rousseau.GetComponent<VisualEffect>().Stop();
		this.smoke.GetComponent<VisualEffect>().Stop();
		this.plasma.GetComponent<VisualEffect>().Stop();
		this.patrik.GetComponent<VisualEffect>().Stop();
		this.userEffect.GetComponent<VisualEffect>().Play();
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00007590 File Offset: 0x00005790
	public void OpenTextureCreator()
	{
		this.effectObj.transform.position = new Vector3(-1800.0232f, -1.982f, 0f);
		this.designObj.SetActive(false);
		this.mediaObj.SetActive(false);
		this.particleObj.SetActive(false);
		this.textureObj.SetActive(true);
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000075F1 File Offset: 0x000057F1
	private void OnDestroy()
	{
		this.StopAudioRecording();
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000075FC File Offset: 0x000057FC
	public void ChangeNoteLength()
	{
		this.moveTiles.GetComponent<MoveTile>().speed = 2.5f * this.noteLengthSlider.value;
		this.noteLengthText.text = "Tile Length: " + Math.Round((double)this.noteLengthSlider.value, 2).ToString();
		this.pianoEditorObj.GetComponent<PianoEditor>().midiStartSpeed = 2.5f * this.noteLengthSlider.value;
		this.pianoEditorObj.GetComponent<PianoEditor>().ChangeMidiTempo();
	}

	// Token: 0x06000064 RID: 100 RVA: 0x0000768A File Offset: 0x0000588A
	public void ResetNoteLength()
	{
		this.noteLengthSlider.value = 1f;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000769C File Offset: 0x0000589C
	public void ChangeKeyDarkness()
	{
		this.whiteTile.GetComponent<SpawnEffect>().defaultColor = new Color(this.keyDarnkess.value, this.keyDarnkess.value, this.keyDarnkess.value, 1f);
		foreach (object obj in this.whiteKeys.transform)
		{
			((Transform)obj).GetComponent<SpriteRenderer>().color = new Color(this.keyDarnkess.value, this.keyDarnkess.value, this.keyDarnkess.value, 1f);
		}
		foreach (object obj2 in this.whiteKeysPrev.transform)
		{
			Transform transform = (Transform)obj2;
			if (transform.name != "whiteKey (4)")
			{
				transform.GetComponent<SpriteRenderer>().color = new Color(this.keyDarnkess.value, this.keyDarnkess.value, this.keyDarnkess.value, 1f);
			}
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000077F0 File Offset: 0x000059F0
	public void ChangeTileRoundness()
	{
		this.defaultMaterial.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
		this.animatedMaterial.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
		this.animatedMaterial2.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
		this.animatedMaterial3.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
		this.animatedMaterial4.SetFloat("_CornerRadius", this.tileRoundnessSlider.value);
		this.HandleTextureInputData();
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000788C File Offset: 0x00005A8C
	public void ChangeTileOutline()
	{
		this.defaultMaterial.SetFloat("_BorderSize", this.tileOutlineSlider.value);
		this.animatedMaterial.SetFloat("_BorderSize", this.tileOutlineSlider.value);
		this.animatedMaterial2.SetFloat("_BorderSize", this.tileOutlineSlider.value);
		this.animatedMaterial3.SetFloat("_BorderSize", this.tileOutlineSlider.value);
		this.animatedMaterial4.SetFloat("_BorderSize", this.tileOutlineSlider.value);
		this.HandleTextureInputData();
	}

	// Token: 0x040000B4 RID: 180
	private bool isPlaying;

	// Token: 0x040000B5 RID: 181
	private bool isLivePlaying;

	// Token: 0x040000B6 RID: 182
	private string filePath;

	// Token: 0x040000B7 RID: 183
	private string audioPath;

	// Token: 0x040000B8 RID: 184
	private string videoPath;

	// Token: 0x040000B9 RID: 185
	private string imagePath;

	// Token: 0x040000BA RID: 186
	private string audioToPath;

	// Token: 0x040000BB RID: 187
	private string midiToPath;

	// Token: 0x040000BC RID: 188
	public GameObject camera;

	// Token: 0x040000BD RID: 189
	public GameObject midiController;

	// Token: 0x040000BE RID: 190
	public GameObject moveTiles;

	// Token: 0x040000BF RID: 191
	public GameObject midiPlayer;

	// Token: 0x040000C0 RID: 192
	public Text filePathText;

	// Token: 0x040000C1 RID: 193
	public Text audioPathText;

	// Token: 0x040000C2 RID: 194
	public Text videoPathText;

	// Token: 0x040000C3 RID: 195
	public Text imagePathText;

	// Token: 0x040000C4 RID: 196
	public Text audioToPathText;

	// Token: 0x040000C5 RID: 197
	public Text midiToPathText;

	// Token: 0x040000C6 RID: 198
	public Material graphGlow;

	// Token: 0x040000C7 RID: 199
	public bool usingMidiFile;

	// Token: 0x040000C8 RID: 200
	public bool usingAudioFile;

	// Token: 0x040000C9 RID: 201
	public bool usingVideoFile;

	// Token: 0x040000CA RID: 202
	public bool usingImageFile;

	// Token: 0x040000CB RID: 203
	public VisualEffect fire;

	// Token: 0x040000CC RID: 204
	public VisualEffect turbulence;

	// Token: 0x040000CD RID: 205
	public VisualEffect dust;

	// Token: 0x040000CE RID: 206
	public VisualEffect glowBall;

	// Token: 0x040000CF RID: 207
	public VisualEffect rousseau;

	// Token: 0x040000D0 RID: 208
	public VisualEffect smoke;

	// Token: 0x040000D1 RID: 209
	public VisualEffect plasma;

	// Token: 0x040000D2 RID: 210
	public VisualEffect patrik;

	// Token: 0x040000D3 RID: 211
	public VisualEffect userEffect;

	// Token: 0x040000D4 RID: 212
	public Dropdown dropDown;

	// Token: 0x040000D5 RID: 213
	public GameObject redBar;

	// Token: 0x040000D6 RID: 214
	public GameObject redBarPrev;

	// Token: 0x040000D7 RID: 215
	public GameObject whiteTile;

	// Token: 0x040000D8 RID: 216
	public GameObject blackTile;

	// Token: 0x040000D9 RID: 217
	public GameObject prevTile;

	// Token: 0x040000DA RID: 218
	public GameObject prevTileMaker;

	// Token: 0x040000DB RID: 219
	public GameObject prevKey;

	// Token: 0x040000DC RID: 220
	public GameObject EditorGuiObj;

	// Token: 0x040000DD RID: 221
	public AudioSource audioSource;

	// Token: 0x040000DE RID: 222
	public GameObject soundObj;

	// Token: 0x040000DF RID: 223
	private string audioDevice;

	// Token: 0x040000E0 RID: 224
	private bool isRecordingAudio;

	// Token: 0x040000E1 RID: 225
	private AudioClip recording;

	// Token: 0x040000E2 RID: 226
	private int recordingTimeRemaining = 1800;

	// Token: 0x040000E3 RID: 227
	private float timeWhenRecordingStarted;

	// Token: 0x040000E4 RID: 228
	private bool recordingAudio;

	// Token: 0x040000E5 RID: 229
	public Text recordingAudioTime;

	// Token: 0x040000E6 RID: 230
	private float timeWhenMidiRecordingStarted;

	// Token: 0x040000E7 RID: 231
	private bool isRecordingMidi;

	// Token: 0x040000E8 RID: 232
	private bool recordingMidi;

	// Token: 0x040000E9 RID: 233
	public Text recordingMidiTime;

	// Token: 0x040000EA RID: 234
	public Dropdown devices;

	// Token: 0x040000EB RID: 235
	public Dropdown audioDevices;

	// Token: 0x040000EC RID: 236
	public Dropdown inputDevices;

	// Token: 0x040000ED RID: 237
	public Dropdown serialPorts;

	// Token: 0x040000EE RID: 238
	public GameObject trackObj;

	// Token: 0x040000EF RID: 239
	public Color whiteC;

	// Token: 0x040000F0 RID: 240
	public Gradient white;

	// Token: 0x040000F1 RID: 241
	public GameObject colorControllerObj;

	// Token: 0x040000F2 RID: 242
	private float redBarRed;

	// Token: 0x040000F3 RID: 243
	private float redBarGreen;

	// Token: 0x040000F4 RID: 244
	private float redBarBlue;

	// Token: 0x040000F5 RID: 245
	private float redBarGlow;

	// Token: 0x040000F6 RID: 246
	public Slider redBarRedSlider;

	// Token: 0x040000F7 RID: 247
	public Slider redBarGreenSlider;

	// Token: 0x040000F8 RID: 248
	public Slider redBarBlueSlider;

	// Token: 0x040000F9 RID: 249
	public Slider redBarGlowSlider;

	// Token: 0x040000FA RID: 250
	public Dropdown animatedTextures;

	// Token: 0x040000FB RID: 251
	private bool animatedTex;

	// Token: 0x040000FC RID: 252
	public Material defaultMaterial;

	// Token: 0x040000FD RID: 253
	public Material animatedMaterial;

	// Token: 0x040000FE RID: 254
	public Material animatedMaterial2;

	// Token: 0x040000FF RID: 255
	public Material animatedMaterial3;

	// Token: 0x04000100 RID: 256
	public Material animatedMaterial4;

	// Token: 0x04000101 RID: 257
	public GameObject lastActiveProfile;

	// Token: 0x04000102 RID: 258
	private float tileEffectRed;

	// Token: 0x04000103 RID: 259
	private float tileEffectGreen;

	// Token: 0x04000104 RID: 260
	private float tileEffectBlue;

	// Token: 0x04000105 RID: 261
	private float tileEffectGlow;

	// Token: 0x04000106 RID: 262
	public Slider tileEffectRedSlider;

	// Token: 0x04000107 RID: 263
	public Slider tileEffectGreenSlider;

	// Token: 0x04000108 RID: 264
	public Slider tileEffectBlueSlider;

	// Token: 0x04000109 RID: 265
	public Slider tileEffectGlowSlider;

	// Token: 0x0400010A RID: 266
	private bool outlineTex;

	// Token: 0x0400010B RID: 267
	public Sprite outlineTexture;

	// Token: 0x0400010C RID: 268
	public Sprite defaultTexture;

	// Token: 0x0400010D RID: 269
	public Sprite prevOutlineTexture;

	// Token: 0x0400010E RID: 270
	private bool removeTex;

	// Token: 0x0400010F RID: 271
	private bool isAnimatedRedBar;

	// Token: 0x04000110 RID: 272
	public GameObject animatedRedBar;

	// Token: 0x04000111 RID: 273
	public GameObject animatedRedBarPrev;

	// Token: 0x04000112 RID: 274
	private bool allowColorTrans;

	// Token: 0x04000113 RID: 275
	public Slider colorTransRedSlider;

	// Token: 0x04000114 RID: 276
	public Slider colorTransGreenSlider;

	// Token: 0x04000115 RID: 277
	public Slider colorTransBlueSlider;

	// Token: 0x04000116 RID: 278
	public Slider colorTransGlowSlider;

	// Token: 0x04000117 RID: 279
	private float colorTransRed;

	// Token: 0x04000118 RID: 280
	private float colorTransGreen;

	// Token: 0x04000119 RID: 281
	private float colorTransBlue;

	// Token: 0x0400011A RID: 282
	private float colorTransGlow;

	// Token: 0x0400011B RID: 283
	public Color transitionColor = Color.white;

	// Token: 0x0400011C RID: 284
	public GameObject colorTransObj;

	// Token: 0x0400011D RID: 285
	public GameObject background;

	// Token: 0x0400011E RID: 286
	public GameObject backgroundPrev;

	// Token: 0x0400011F RID: 287
	public Slider colorBackRedSlider;

	// Token: 0x04000120 RID: 288
	public Slider colorBackGreenSlider;

	// Token: 0x04000121 RID: 289
	public Slider colorBackBlueSlider;

	// Token: 0x04000122 RID: 290
	private float colorBackRed;

	// Token: 0x04000123 RID: 291
	private float colorBackGreen;

	// Token: 0x04000124 RID: 292
	private float colorBackBlue;

	// Token: 0x04000125 RID: 293
	private bool allowMidiInfo;

	// Token: 0x04000126 RID: 294
	public GameObject midiInfoObj;

	// Token: 0x04000127 RID: 295
	public Toggle redBarAnim;

	// Token: 0x04000128 RID: 296
	public Toggle colorTrans;

	// Token: 0x04000129 RID: 297
	public Toggle outlines;

	// Token: 0x0400012A RID: 298
	public Toggle animTex;

	// Token: 0x0400012B RID: 299
	public Toggle removeTexture;

	// Token: 0x0400012C RID: 300
	public Toggle midiInfo;

	// Token: 0x0400012D RID: 301
	public GameObject renderObj;

	// Token: 0x0400012E RID: 302
	public GameObject renderGUIObj;

	// Token: 0x0400012F RID: 303
	public GameObject infoObj;

	// Token: 0x04000130 RID: 304
	public GameObject hudControllerObj;

	// Token: 0x04000131 RID: 305
	public bool rendering;

	// Token: 0x04000132 RID: 306
	public GameObject designObj;

	// Token: 0x04000133 RID: 307
	public GameObject mediaObj;

	// Token: 0x04000134 RID: 308
	public GameObject effectObj;

	// Token: 0x04000135 RID: 309
	public GameObject particleObj;

	// Token: 0x04000136 RID: 310
	public GameObject textureObj;

	// Token: 0x04000137 RID: 311
	public VideoPlayer videoPlayer;

	// Token: 0x04000138 RID: 312
	public GameObject ledObj;

	// Token: 0x04000139 RID: 313
	public GameObject imageSprite;

	// Token: 0x0400013A RID: 314
	public GameObject audioReactorObj;

	// Token: 0x0400013B RID: 315
	public Dropdown reactorDropDown;

	// Token: 0x0400013C RID: 316
	public Dropdown reactorSizeDropDown;

	// Token: 0x0400013D RID: 317
	public Slider reactorRedSlider;

	// Token: 0x0400013E RID: 318
	public Slider reactorGreenSlider;

	// Token: 0x0400013F RID: 319
	public Slider reactorBlueSlider;

	// Token: 0x04000140 RID: 320
	public Slider reactorGlowSlider;

	// Token: 0x04000141 RID: 321
	private float reactorRed;

	// Token: 0x04000142 RID: 322
	private float reactorGreen;

	// Token: 0x04000143 RID: 323
	private float reactorBlue;

	// Token: 0x04000144 RID: 324
	private float reactorGlow;

	// Token: 0x04000145 RID: 325
	public GameObject audioReactorPrevObj;

	// Token: 0x04000146 RID: 326
	public GameObject lightObj;

	// Token: 0x04000147 RID: 327
	public bool useVirtualLights;

	// Token: 0x04000148 RID: 328
	public bool useHitEffect;

	// Token: 0x04000149 RID: 329
	public VisualEffect fog;

	// Token: 0x0400014A RID: 330
	public Color fogColor;

	// Token: 0x0400014B RID: 331
	public int currentProjectIndex;

	// Token: 0x0400014C RID: 332
	public ProjectSlots userProjects;

	// Token: 0x0400014D RID: 333
	public Dropdown userProjectsDropdown;

	// Token: 0x0400014E RID: 334
	public InputField userProjectNameInputField;

	// Token: 0x0400014F RID: 335
	public Text projectNameText;

	// Token: 0x04000150 RID: 336
	public GameObject resetOptionObj;

	// Token: 0x04000151 RID: 337
	public string lastProjectText;

	// Token: 0x04000152 RID: 338
	public Toggle reverseAnimationToggle;

	// Token: 0x04000153 RID: 339
	public GameObject particleEditor;

	// Token: 0x04000154 RID: 340
	public Slider noteLengthSlider;

	// Token: 0x04000155 RID: 341
	public Text noteLengthText;

	// Token: 0x04000156 RID: 342
	public GameObject pianoEditorObj;

	// Token: 0x04000157 RID: 343
	public Slider keyDarnkess;

	// Token: 0x04000158 RID: 344
	public GameObject whiteKeys;

	// Token: 0x04000159 RID: 345
	public GameObject whiteKeysPrev;

	// Token: 0x0400015A RID: 346
	public Slider tileRoundnessSlider;

	// Token: 0x0400015B RID: 347
	public Slider tileOutlineSlider;
}
