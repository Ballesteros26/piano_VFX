using System;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Internal;
using UnityEngine;

namespace NatSuite.Recorders.Inputs
{
	// Token: 0x0200004D RID: 77
	[Doc("AudioInput")]
	public sealed class AudioInput : IDisposable
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x00014500 File Offset: 0x00012700
		[Doc("AudioInputCtorListener")]
		public AudioInput(IMediaRecorder recorder, IClock clock, AudioListener audioListener)
			: this(recorder, clock, audioListener.gameObject, false)
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00014511 File Offset: 0x00012711
		[Doc("AudioInputCtorSource")]
		public AudioInput(IMediaRecorder recorder, IClock clock, AudioSource audioSource, bool mute = false)
			: this(recorder, clock, audioSource.gameObject, mute)
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00014523 File Offset: 0x00012723
		[Doc("AudioInputDispose")]
		public void Dispose()
		{
			global::UnityEngine.Object.Destroy(this.attachment);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00014530 File Offset: 0x00012730
		private AudioInput(IMediaRecorder recorder, IClock clock, GameObject gameObject, bool mute = false)
		{
			this.recorder = recorder;
			this.clock = clock;
			this.attachment = gameObject.AddComponent<AudioInput.AudioInputAttachment>();
			this.attachment.sampleBufferDelegate = new Action<float[]>(this.OnSampleBuffer);
			this.mute = mute;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001457C File Offset: 0x0001277C
		private void OnSampleBuffer(float[] data)
		{
			AndroidJNI.AttachCurrentThread();
			this.recorder.CommitSamples(data, this.clock.timestamp);
			if (this.mute)
			{
				Array.Clear(data, 0, data.Length);
			}
		}

		// Token: 0x040003E4 RID: 996
		private readonly IMediaRecorder recorder;

		// Token: 0x040003E5 RID: 997
		private readonly IClock clock;

		// Token: 0x040003E6 RID: 998
		private readonly AudioInput.AudioInputAttachment attachment;

		// Token: 0x040003E7 RID: 999
		private readonly bool mute;

		// Token: 0x02000089 RID: 137
		private class AudioInputAttachment : MonoBehaviour
		{
			// Token: 0x0600038A RID: 906 RVA: 0x0001883C File Offset: 0x00016A3C
			private void OnAudioFilterRead(float[] data, int channels)
			{
				Action<float[]> action = this.sampleBufferDelegate;
				if (action == null)
				{
					return;
				}
				action(data);
			}

			// Token: 0x040004C4 RID: 1220
			public Action<float[]> sampleBufferDelegate;
		}
	}
}
