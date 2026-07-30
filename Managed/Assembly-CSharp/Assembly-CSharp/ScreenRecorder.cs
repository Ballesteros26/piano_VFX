using System;
using NatSuite.Devices;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Inputs;
using UnityEngine;

// Token: 0x02000022 RID: 34
internal class ScreenRecorder : MonoBehaviour
{
	// Token: 0x06000142 RID: 322 RVA: 0x0000FE6D File Offset: 0x0000E06D
	private void Start()
	{
		this.audioClock = new RealtimeClock();
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000FE7A File Offset: 0x0000E07A
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			this.StartRecording();
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			this.StopRecording();
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000FE9C File Offset: 0x0000E09C
	private void StartRecording()
	{
		this.recording = true;
		MediaDeviceQuery mediaDeviceQuery = new MediaDeviceQuery(new MediaDeviceQuery.Criterion[] { MediaDeviceQuery.Criteria.AudioDevice });
		this.audioDevice = mediaDeviceQuery.currentDevice as IAudioDevice;
		RealtimeClock realtimeClock = new RealtimeClock();
		this.recorder = new MP4Recorder(1920, 1080, 60f, 48000, 2, 30000000, 3);
		this.cameraInput = new CameraInput(this.recorder, realtimeClock, new Camera[] { Camera.main });
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000FF24 File Offset: 0x0000E124
	private async void StopRecording()
	{
		this.recording = false;
		this.cameraInput.Dispose();
		this.audioDevice.StopRunning();
		await this.recorder.FinishWriting();
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000FF5D File Offset: 0x0000E15D
	private void OnAudioFilterRead(float[] sampleBuffer, int channels)
	{
		if (this.recording)
		{
			MP4Recorder mp4Recorder = this.recorder;
			if (mp4Recorder == null)
			{
				return;
			}
			mp4Recorder.CommitSamples(sampleBuffer, this.audioClock.timestamp);
		}
	}

	// Token: 0x04000304 RID: 772
	private MP4Recorder recorder;

	// Token: 0x04000305 RID: 773
	private CameraInput cameraInput;

	// Token: 0x04000306 RID: 774
	private IAudioDevice audioDevice;

	// Token: 0x04000307 RID: 775
	private IClock audioClock;

	// Token: 0x04000308 RID: 776
	private bool recording;
}
