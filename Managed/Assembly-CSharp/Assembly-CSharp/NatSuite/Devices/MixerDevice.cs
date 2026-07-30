using System;
using System.Collections;
using System.Collections.Generic;
using NatSuite.Devices.Internal;
using UnityEngine;

namespace NatSuite.Devices
{
	// Token: 0x02000038 RID: 56
	[Doc("MixerDevice")]
	public sealed class MixerDevice : IAudioDevice, IMediaDevice, IEquatable<IMediaDevice>, IDisposable
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000132D0 File Offset: 0x000114D0
		[Doc("UniqueID")]
		public string uniqueID
		{
			get
			{
				return string.Concat(new string[]
				{
					"(",
					this.audioDevice.uniqueID,
					":",
					this.attachment.gameObject.name,
					")"
				});
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00013321 File Offset: 0x00011521
		[Doc("SampleRate")]
		public int sampleRate
		{
			get
			{
				return AudioSettings.outputSampleRate;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00013328 File Offset: 0x00011528
		[Doc("ChannelCount")]
		public int channelCount
		{
			get
			{
				return (int)AudioSettings.speakerMode;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0001332F File Offset: 0x0001152F
		[Doc("Running")]
		public bool running
		{
			get
			{
				return this.audioDevice.running;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0001333C File Offset: 0x0001153C
		[Doc("MixerDeviceCtorAudioSource")]
		public MixerDevice(IAudioDevice audioDevice, AudioSource audioSource)
			: this(audioDevice, audioSource.gameObject)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0001333C File Offset: 0x0001153C
		[Doc("MixerDeviceCtorAudioListener")]
		public MixerDevice(IAudioDevice audioDevice, AudioListener audioListener)
			: this(audioDevice, audioListener.gameObject)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0001334C File Offset: 0x0001154C
		[Doc("StartRecording")]
		public void StartRunning(SampleBufferDelegate @delegate)
		{
			this.attachment.@delegate = delegate(float[] sampleBuffer, long timestamp)
			{
				object syncRoot = ((ICollection)this.stagingBuffer).SyncRoot;
				lock (syncRoot)
				{
					this.stagingBuffer.AddRange(sampleBuffer);
				}
			};
			float[] copyBuffer = new float[4096];
			this.audioDevice.StartRunning(delegate(float[] sampleBuffer, long timestamp)
			{
				object syncRoot2 = ((ICollection)this.stagingBuffer).SyncRoot;
				lock (syncRoot2)
				{
					this.stagingBuffer.CopyTo(0, copyBuffer, 0, sampleBuffer.Length);
					this.stagingBuffer.RemoveRange(0, sampleBuffer.Length);
				}
				for (int i = 0; i < sampleBuffer.Length; i++)
				{
					sampleBuffer[i] = (float)Math.Tanh((double)(sampleBuffer[i] + copyBuffer[i]));
				}
				@delegate(sampleBuffer, timestamp);
			});
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000133AB File Offset: 0x000115AB
		[Doc("StopRunning")]
		public void StopRunning()
		{
			this.attachment.@delegate = null;
			this.audioDevice.StopRunning();
			this.stagingBuffer.Clear();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000133CF File Offset: 0x000115CF
		[Doc("MixerDeviceDispose")]
		public void Dispose()
		{
			global::UnityEngine.Object.Destroy(this.attachment);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000133DC File Offset: 0x000115DC
		private MixerDevice(IAudioDevice audioDevice, GameObject gameObject)
		{
			this.audioDevice = audioDevice;
			this.attachment = gameObject.AddComponent<MixerDevice.MixerDeviceAttachment>();
			this.stagingBuffer = new List<float>();
			AudioDevice audioDevice2;
			if ((audioDevice2 = audioDevice as AudioDevice) != null)
			{
				audioDevice2.sampleRate = AudioSettings.outputSampleRate;
				audioDevice2.channelCount = (int)AudioSettings.speakerMode;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0001342D File Offset: 0x0001162D
		public bool Equals(IMediaDevice other)
		{
			return other != null && other is MixerDevice && other.uniqueID == this.uniqueID;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0001344D File Offset: 0x0001164D
		public override string ToString()
		{
			return "MixerDevice (" + this.uniqueID + ")";
		}

		// Token: 0x040003C1 RID: 961
		private readonly IAudioDevice audioDevice;

		// Token: 0x040003C2 RID: 962
		private readonly MixerDevice.MixerDeviceAttachment attachment;

		// Token: 0x040003C3 RID: 963
		private readonly List<float> stagingBuffer;

		// Token: 0x02000078 RID: 120
		private class MixerDeviceAttachment : MonoBehaviour
		{
			// Token: 0x06000360 RID: 864 RVA: 0x00017E6B File Offset: 0x0001606B
			private void OnAudioFilterRead(float[] data, int channels)
			{
				SampleBufferDelegate sampleBufferDelegate = this.@delegate;
				if (sampleBufferDelegate == null)
				{
					return;
				}
				sampleBufferDelegate(data, 0L);
			}

			// Token: 0x04000495 RID: 1173
			public SampleBufferDelegate @delegate;
		}
	}
}
