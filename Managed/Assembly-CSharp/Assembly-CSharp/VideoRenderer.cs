using System;
using System.Collections;
using System.IO;
using System.Linq;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using SFB;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x02000027 RID: 39
public class VideoRenderer : MonoBehaviour
{
	// Token: 0x0600015F RID: 351 RVA: 0x000117E0 File Offset: 0x0000F9E0
	private void Start()
	{
		this.pms = this.soundControllerObj.GetComponent<PlayMidiSound>();
		float time = Time.time;
		this.audioSource = this.fileManager.GetComponent<FileManager>().audioSource;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0001180F File Offset: 0x0000FA0F
	public void StartRenderingButton()
	{
		base.StartCoroutine(this.StartRendering());
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0001181E File Offset: 0x0000FA1E
	public IEnumerator StartRendering()
	{
		if (this.videoPath != null && this.videoPath != "" && !this.renderingInProgress)
		{
			if (this.pms.videoOn)
			{
				this.videoFrameRate = this.pms.pe.fm.videoPlayer.frameRate;
				this.pms.pe.fm.videoPlayer.playbackSpeed = 1E-05f;
				this.pms.pe.fm.videoPlayer.Play();
				if (this.pms.videoOffset > 0f)
				{
					this.pms.pe.fm.videoPlayer.time = (double)this.pms.videoOffset;
				}
			}
			this.UpdateConsole("Rendering...");
			yield return new WaitForSeconds(3f);
			if (this.pms.audioOn)
			{
				this.audioReactorObj.GetComponent<AudioVisualizer>().isRendering = true;
			}
			this.renderStartTime = (float)(DateTime.UtcNow - new DateTime(2020, 1, 1)).TotalSeconds;
			this.playerObj.GetComponent<PlayMidiSound>().RenderPlay();
			this.renderingInProgress = true;
			this.captureVideo = true;
			Time.captureDeltaTime = 1f / (float)this.frameRate;
			this.clock = new FixedIntervalClock(this.frameRate, true);
			if (this.pms.audioOn)
			{
				this.recorder = new MP4Recorder(this.captureWidth, this.captureHeight, (float)this.frameRate, this.audioSource.clip.frequency, this.audioSource.clip.channels, this.quality, 3);
				this.PrepareAudio();
			}
			else
			{
				this.recorder = new MP4Recorder(this.captureWidth, this.captureHeight, (float)this.frameRate, 0, 0, this.quality, 3);
			}
		}
		else if (this.videoPath == null || this.videoPath == "")
		{
			this.UpdateConsole("No file path selected.");
		}
		yield break;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00011830 File Offset: 0x0000FA30
	private void PrepareAudio()
	{
		float[] array = new float[this.audioSource.clip.samples * this.audioSource.clip.channels];
		this.audioSource.clip.GetData(array, 0);
		if (this.pms.trimStart > 0f)
		{
			int num = (int)(this.pms.trimStart * (float)this.audioSource.clip.frequency * (float)this.audioSource.clip.channels);
			for (int i = 0; i < num; i++)
			{
				array[i] = 0f;
			}
		}
		if (this.pms.trimEnd > 0f)
		{
			int num2 = (int)((this.audioSource.clip.length - this.pms.trimEnd) * (float)this.audioSource.clip.frequency * (float)this.audioSource.clip.channels);
			for (int j = array.Length - num2; j < array.Length; j++)
			{
				array[j] = 0f;
			}
		}
		this.pms.audioOffset -= 0.1f;
		if (this.pms.audioOffset < 0f)
		{
			int num3 = (int)(this.pms.audioOffset * -1f * (float)this.audioSource.clip.frequency * (float)this.audioSource.clip.channels);
			float[] array2 = new float[num3];
			for (int k = 0; k < num3; k++)
			{
				array2[k] = 0f;
			}
			this.editedAudioSamples = new float[num3 + array.Length];
			Array.Copy(array2, this.editedAudioSamples, num3);
			Array.Copy(array, 0, this.editedAudioSamples, num3, array.Length);
		}
		else if (this.pms.audioOffset > 0f)
		{
			int num4 = (int)(this.pms.audioOffset * (float)this.audioSource.clip.frequency * (float)this.audioSource.clip.channels);
			this.editedAudioSamples = new float[array.Length - num4];
			for (int l = num4; l < array.Length; l++)
			{
				this.editedAudioSamples[l - num4] = array[l];
			}
		}
		else
		{
			this.editedAudioSamples = array;
		}
		this.pms.audioOffset += 0.1f;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00011A98 File Offset: 0x0000FC98
	public void CaptureScreenshot()
	{
		this.captureScreenshot = true;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00011AA4 File Offset: 0x0000FCA4
	private void Update()
	{
		if (!this.renderingInProgress)
		{
			return;
		}
		if (this.progressObj.value == 1f && this.notRendering && this.midiBasedRendering)
		{
			this.notRendering = false;
			base.StartCoroutine(this.WaitAndEncode());
		}
		double length = this.pms.pe.fm.videoPlayer.length;
		float videoOffset = this.pms.videoOffset;
		if (!this.captureVideo)
		{
			return;
		}
		if (this.recorder != null)
		{
			if (this.hideGameObject != null)
			{
				this.hideGameObject.transform.position = new Vector2(this.hideGameObject.transform.position.x + 50f, this.hideGameObject.transform.position.y);
			}
			if (this.renderTexture == null)
			{
				this.rect = new Rect(0f, 0f, (float)this.captureWidth, (float)this.captureHeight);
				this.renderTexture = new RenderTexture(this.captureWidth, this.captureHeight, 24);
				this.screenShot = new Texture2D(this.captureWidth, this.captureHeight, TextureFormat.RGB24, false);
			}
			Camera component = this.mainCam.GetComponent<Camera>();
			component.targetTexture = this.renderTexture;
			component.Render();
			RenderTexture.active = this.renderTexture;
			this.screenShot.ReadPixels(this.rect, 0, 0);
			component.targetTexture = null;
			RenderTexture.active = null;
			this.currentTimestamp = this.clock.timestamp;
			if (this.pms.videoOn)
			{
				if ((float)this.frameCounter / (float)this.frameRate + this.pms.videoOffset > 0f)
				{
					float num = this.videoFrameRate / (float)this.frameRate * (float)this.loopRunFrameCounter - (float)this.videoFrameCounter;
					int num2 = 0;
					while ((float)num2 < num)
					{
						this.pms.pe.fm.videoPlayer.StepForward();
						this.videoFrameCounter++;
						num2++;
					}
					this.loopRunFrameCounter++;
					this.recorder.CommitFrame<Color32>(this.screenShot.GetPixels32(), this.currentTimestamp);
				}
			}
			else
			{
				this.recorder.CommitFrame<Color32>(this.screenShot.GetPixels32(), this.currentTimestamp);
			}
			if (this.pms.audioOn)
			{
				if (this.pms.pe.useAudioReactorToggle.isOn)
				{
					if (this.pms.trimEnd - this.pms.audioOffset < (float)this.frameCounter / (float)this.frameRate)
					{
						if (this.audioSource.isPlaying)
						{
							this.audioSource.Stop();
						}
						this.audioSource.time = 0f;
					}
					else if ((float)this.frameCounter / (float)this.frameRate + (-this.pms.trimStart + this.pms.audioOffset) < 0f)
					{
						if (this.audioSource.isPlaying)
						{
							this.audioSource.Stop();
						}
						this.audioSource.time = 0f;
					}
					else
					{
						this.audioSource.time = (float)this.frameCounter / (float)this.frameRate + this.pms.audioOffset;
						if (!this.audioSource.isPlaying)
						{
							this.audioSource.Play();
						}
					}
				}
				if (this.editedAudioSamples.Length < (this.frameCounter + 1) * (this.audioSource.clip.frequency * this.audioSource.clip.channels / this.frameRate))
				{
					if (!this.midiBasedRendering && this.notRendering)
					{
						this.notRendering = false;
						base.StartCoroutine(this.WaitAndEncode());
					}
					this.editedAudioSamples = new float[(this.frameCounter + 1) * (this.audioSource.clip.frequency * this.audioSource.clip.channels / this.frameRate)];
					for (int i = (this.frameCounter - 60) * (this.audioSource.clip.frequency / this.frameRate); i < this.editedAudioSamples.Length; i++)
					{
						this.editedAudioSamples[i] = 0f;
					}
				}
				float[] array = this.editedAudioSamples.Skip(this.frameCounter * (this.audioSource.clip.frequency * this.audioSource.clip.channels / this.frameRate)).Take(this.audioSource.clip.frequency * this.audioSource.clip.channels / this.frameRate).ToArray<float>();
				IMediaRecorder mediaRecorder = this.recorder;
				if (mediaRecorder != null)
				{
					mediaRecorder.CommitSamples(array, this.currentTimestamp);
				}
			}
			if (this.frameCounter % (this.frameRate * 5) == 0 && this.frameCounter != 0)
			{
				this.timeSpan = DateTime.UtcNow - new DateTime(2020, 1, 1);
				float num3 = (float)this.timeSpan.TotalSeconds - this.renderStartTime;
				if (this.midiBasedRendering)
				{
					this.UpdateConsole(string.Concat(new object[]
					{
						this.frameCounter,
						" frames rendered / Time elapsed: ",
						Math.Round((double)(num3 / 60f), 2),
						"m  / Time remaining: ",
						Math.Round((double)(num3 * (1f - this.progressObj.value) / this.progressObj.value / 60f), 2),
						"m"
					}));
				}
				else
				{
					this.UpdateConsole(string.Concat(new object[]
					{
						this.frameCounter,
						" frames rendered / Time elapsed: ",
						Math.Round((double)(num3 / 60f), 2),
						"m  / Time remaining: ",
						Math.Round((double)(num3 * (this.pms.trimEnd - this.pms.audioOffset - (float)(this.frameCounter / this.frameRate)) / (float)(this.frameCounter / this.frameRate) / 60f), 2),
						"m"
					}));
				}
			}
			this.frameCounter++;
			if (this.hideGameObject != null)
			{
				this.hideGameObject.transform.position = new Vector2(this.hideGameObject.transform.position.x - 50f, this.hideGameObject.transform.position.y);
			}
			if (!this.optimizeForManyScreenshots)
			{
				global::UnityEngine.Object.Destroy(this.renderTexture);
				this.renderTexture = null;
				this.screenShot = null;
			}
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0001218D File Offset: 0x0001038D
	private void OnFrameReady(VideoPlayer source, long frameIndex)
	{
		Debug.Log("frame ready");
		source.StepForward();
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0001219F File Offset: 0x0001039F
	private void OnDestroy()
	{
		if (this.recorder != null)
		{
			this.ExitRenderer();
		}
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0001219F File Offset: 0x0001039F
	private void OnExit()
	{
		if (this.recorder != null)
		{
			this.ExitRenderer();
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000121B0 File Offset: 0x000103B0
	public void ExitRenderer()
	{
		this.playerObj.GetComponent<PlayMidiSound>().RenderStop();
		this.exitMessage = true;
		this.captureVideo = false;
		this.writingAudio = true;
		this.UpdateConsole("Rendering interrupted.");
		this.UpdateConsole("Moving your file to selected location. Don't close this application!");
		this.FinishRendering();
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000121FE File Offset: 0x000103FE
	private IEnumerator WaitAndEncode()
	{
		yield return new WaitForSeconds(7f);
		this.exitMessage = true;
		this.captureVideo = false;
		this.writingAudio = true;
		this.UpdateConsole("Rendering finished.");
		this.UpdateConsole("Moving your file to selected location. Don't close this application!");
		this.FinishRendering();
		yield break;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00012210 File Offset: 0x00010410
	public async void FinishRendering()
	{
		this.audioSource.Stop();
		string text = await this.recorder.FinishWriting();
		try
		{
			File.Move(text, this.videoPath + "/Piano-VFX_" + DateTime.Now.ToString("MM_dd_yyyy_h_mm_ss") + ".mp4");
			this.UpdateConsole("File moved to selected location, you can press ESC to exit.");
		}
		catch
		{
			this.UpdateConsole("Moving failed. File location: C:\\Users\\your_username\\AppData\\LocalLow\\WollyGames\\Piano VFX ");
		}
		this.recorder = null;
		Time.captureDeltaTime = 0f;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00012249 File Offset: 0x00010449
	public void SelectPath()
	{
		if (!this.renderingInProgress)
		{
			StandaloneFileBrowser.OpenFolderPanelAsync("Choose Path", "", false, delegate(string[] paths)
			{
				if (paths.Length == 0)
				{
					return;
				}
				this.videoPath = paths[0];
				this.videoPathText.text = paths[0];
				this.UpdateConsole("Path selected.");
			});
		}
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00012270 File Offset: 0x00010470
	public void PrepearRendering()
	{
		this.playerObj.GetComponent<PlayMidiSound>().RenderStop();
		if (this.pms.audioOn)
		{
			this.audioSource.Stop();
		}
		this.playerObj.GetComponent<PlayMidiSound>().rendering = true;
		this.UpdateConsole("Resolution: " + this.captureWidth.ToString() + "x" + this.captureHeight.ToString());
		this.UpdateConsole("Frame rate: " + this.frameRate.ToString() + " fps");
		this.UpdateConsole("Quality: Low");
		this.UpdateConsole("You can cancel rendering with ESC key.");
		this.UpdateConsole("You can toggle between window mode and fullscreen mode by pressing S.");
		this.UpdateConsole("Rendering will end when there is no MIDI events left.");
		if (this.pms.videoOn)
		{
			this.pms.pe.fm.videoPlayer.Play();
			this.pms.pe.fm.videoPlayer.StepForward();
			this.pms.pe.fm.videoPlayer.Pause();
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00012388 File Offset: 0x00010588
	public void UpdateConsole(string line)
	{
		this.line14.text = this.line13.text;
		this.line13.text = this.line12.text;
		this.line12.text = this.line11.text;
		this.line11.text = this.line10.text;
		this.line10.text = this.line9.text;
		this.line9.text = this.line8.text;
		this.line8.text = this.line7.text;
		this.line7.text = this.line6.text;
		this.line6.text = this.line5.text;
		this.line5.text = this.line4.text;
		this.line4.text = this.line3.text;
		this.line3.text = this.line2.text;
		this.line2.text = this.line1.text;
		this.line1.text = line;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x000124C0 File Offset: 0x000106C0
	public void ChnageResolution()
	{
		if (this.renderingInProgress)
		{
			return;
		}
		switch (this.resolution.value)
		{
		case 0:
			this.captureWidth = 1280;
			this.captureHeight = 720;
			break;
		case 1:
			this.captureWidth = 1920;
			this.captureHeight = 1080;
			break;
		case 2:
			this.captureWidth = 2560;
			this.captureHeight = 1440;
			break;
		case 3:
			this.captureWidth = 3840;
			this.captureHeight = 2160;
			break;
		}
		this.UpdateConsole("Resolution changed: " + this.captureWidth.ToString() + "x" + this.captureHeight.ToString());
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00012584 File Offset: 0x00010784
	public void ChnageFrameRate()
	{
		if (this.renderingInProgress)
		{
			return;
		}
		switch (this.framerate.value)
		{
		case 0:
			this.frameRate = 24;
			break;
		case 1:
			this.frameRate = 30;
			break;
		case 2:
			this.frameRate = 60;
			break;
		}
		this.UpdateConsole("Frame rate changed: " + this.frameRate.ToString() + " fps");
	}

	// Token: 0x06000170 RID: 368 RVA: 0x000125F8 File Offset: 0x000107F8
	public void ChnageQuality()
	{
		if (this.renderingInProgress)
		{
			return;
		}
		switch (this.qualityD.value)
		{
		case 0:
			this.quality = 5000000;
			this.UpdateConsole("Quality changed: Low");
			return;
		case 1:
			this.quality = 15000000;
			this.UpdateConsole("Quality changed: Medium");
			return;
		case 2:
			this.quality = 30000000;
			this.UpdateConsole("Quality changed: High");
			return;
		case 3:
			this.quality = 50000000;
			this.UpdateConsole("Quality changed: Very High");
			return;
		default:
			return;
		}
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0001268C File Offset: 0x0001088C
	public void ChangeRenderingMode()
	{
		if (this.renderingInProgress)
		{
			return;
		}
		int value = this.renderingMode.value;
		if (value == 0)
		{
			this.midiBasedRendering = true;
			this.UpdateConsole("Rendering will end when there is no MIDI events left.");
			return;
		}
		if (value != 1)
		{
			return;
		}
		if (this.pms.audioOn)
		{
			this.midiBasedRendering = false;
			this.UpdateConsole("Rendering will end when there is no audio samples left.");
			return;
		}
		this.UpdateConsole("No audio file selected.");
		this.renderingMode.value = 0;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00012700 File Offset: 0x00010900
	public static void VerifyDir(string path)
	{
		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00012738 File Offset: 0x00010938
	public static void Logger(string lines)
	{
		string text = "C:/Log/";
		VideoRenderer.VerifyDir(text);
		string text2 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
		try
		{
			StreamWriter streamWriter = new StreamWriter(text + text2, true);
			streamWriter.WriteLine(DateTime.Now.ToString() + ": " + lines);
			streamWriter.Close();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0400036B RID: 875
	private int frameCounter;

	// Token: 0x0400036C RID: 876
	private int loopRunFrameCounter;

	// Token: 0x0400036D RID: 877
	private int videoFrameCounter;

	// Token: 0x0400036E RID: 878
	private string videoPath;

	// Token: 0x0400036F RID: 879
	public Text videoPathText;

	// Token: 0x04000370 RID: 880
	public bool renderingInProgress;

	// Token: 0x04000371 RID: 881
	public bool exitMessage;

	// Token: 0x04000372 RID: 882
	public GameObject playerObj;

	// Token: 0x04000373 RID: 883
	public Dropdown resolution;

	// Token: 0x04000374 RID: 884
	public Dropdown framerate;

	// Token: 0x04000375 RID: 885
	public Dropdown qualityD;

	// Token: 0x04000376 RID: 886
	public Dropdown renderingMode;

	// Token: 0x04000377 RID: 887
	public GameObject fileManager;

	// Token: 0x04000378 RID: 888
	private AudioSource audioSource;

	// Token: 0x04000379 RID: 889
	private float[] samples;

	// Token: 0x0400037A RID: 890
	public bool writingAudio;

	// Token: 0x0400037B RID: 891
	public GameObject soundControllerObj;

	// Token: 0x0400037C RID: 892
	private PlayMidiSound pms;

	// Token: 0x0400037D RID: 893
	private long audioTimestamp;

	// Token: 0x0400037E RID: 894
	private bool audioTimestampSet;

	// Token: 0x0400037F RID: 895
	private float renderStartTime;

	// Token: 0x04000380 RID: 896
	private TimeSpan timeSpan;

	// Token: 0x04000381 RID: 897
	public Text line1;

	// Token: 0x04000382 RID: 898
	public Text line2;

	// Token: 0x04000383 RID: 899
	public Text line3;

	// Token: 0x04000384 RID: 900
	public Text line4;

	// Token: 0x04000385 RID: 901
	public Text line5;

	// Token: 0x04000386 RID: 902
	public Text line6;

	// Token: 0x04000387 RID: 903
	public Text line7;

	// Token: 0x04000388 RID: 904
	public Text line8;

	// Token: 0x04000389 RID: 905
	public Text line9;

	// Token: 0x0400038A RID: 906
	public Text line10;

	// Token: 0x0400038B RID: 907
	public Text line11;

	// Token: 0x0400038C RID: 908
	public Text line12;

	// Token: 0x0400038D RID: 909
	public Text line13;

	// Token: 0x0400038E RID: 910
	public Text line14;

	// Token: 0x0400038F RID: 911
	public int captureWidth = 1920;

	// Token: 0x04000390 RID: 912
	public int captureHeight = 1080;

	// Token: 0x04000391 RID: 913
	public int frameRate = 30;

	// Token: 0x04000392 RID: 914
	public int quality = 5000000;

	// Token: 0x04000393 RID: 915
	private long currentTimestamp;

	// Token: 0x04000394 RID: 916
	public GameObject mainCam;

	// Token: 0x04000395 RID: 917
	public GameObject hideGameObject;

	// Token: 0x04000396 RID: 918
	public Slider progressObj;

	// Token: 0x04000397 RID: 919
	public bool optimizeForManyScreenshots = true;

	// Token: 0x04000398 RID: 920
	public VideoRenderer.Format format = VideoRenderer.Format.PPM;

	// Token: 0x04000399 RID: 921
	private string folder;

	// Token: 0x0400039A RID: 922
	private Rect rect;

	// Token: 0x0400039B RID: 923
	private RenderTexture renderTexture;

	// Token: 0x0400039C RID: 924
	private Texture2D screenShot;

	// Token: 0x0400039D RID: 925
	private int counter;

	// Token: 0x0400039E RID: 926
	private bool captureScreenshot;

	// Token: 0x0400039F RID: 927
	private bool captureVideo;

	// Token: 0x040003A0 RID: 928
	private bool notRendering = true;

	// Token: 0x040003A1 RID: 929
	private IMediaRecorder recorder;

	// Token: 0x040003A2 RID: 930
	private IClock clock;

	// Token: 0x040003A3 RID: 931
	private float[] editedAudioSamples;

	// Token: 0x040003A4 RID: 932
	private bool midiBasedRendering = true;

	// Token: 0x040003A5 RID: 933
	private float videoFrameRate;

	// Token: 0x040003A6 RID: 934
	public GameObject audioReactorObj;

	// Token: 0x0200006B RID: 107
	public enum Format
	{
		// Token: 0x04000470 RID: 1136
		RAW,
		// Token: 0x04000471 RID: 1137
		JPG,
		// Token: 0x04000472 RID: 1138
		PNG,
		// Token: 0x04000473 RID: 1139
		PPM
	}
}
