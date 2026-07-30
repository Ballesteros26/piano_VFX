using System;
using System.Runtime.InteropServices;
using System.Text;
using AOT;
using UnityEngine;

namespace NatSuite.Devices.Internal
{
	// Token: 0x0200003E RID: 62
	public class NativeAudioDevice : AudioDevice
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00013630 File Offset: 0x00011830
		public override string uniqueID
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				this.device.UniqueID(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0001365C File Offset: 0x0001185C
		public override string name
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				this.device.Name(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00013686 File Offset: 0x00011886
		public override bool echoCancellation
		{
			get
			{
				return this.device.EchoCancellation();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00013693 File Offset: 0x00011893
		// (set) Token: 0x06000236 RID: 566 RVA: 0x000136A0 File Offset: 0x000118A0
		public override int sampleRate
		{
			get
			{
				return this.device.SampleRate();
			}
			set
			{
				this.device.SampleRate(value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000237 RID: 567 RVA: 0x000136AE File Offset: 0x000118AE
		// (set) Token: 0x06000238 RID: 568 RVA: 0x000136BB File Offset: 0x000118BB
		public override int channelCount
		{
			get
			{
				return this.device.ChannelCount();
			}
			set
			{
				this.device.ChannelCount(value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000136C9 File Offset: 0x000118C9
		public override bool running
		{
			get
			{
				return this.device.Running();
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000136D8 File Offset: 0x000118D8
		public override void StartRunning(SampleBufferDelegate @delegate)
		{
			Action<float[], long> action = delegate(float[] sampleBuffer, long timestamp)
			{
				try
				{
					@delegate(sampleBuffer, timestamp);
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("NatDevice Error: Sample buffer delegate raised exception: {0}", ex));
				}
			};
			this.device.StartRunning(new Bridge.SampleBufferDelegate(NativeAudioDevice.OnSampleBuffer), (IntPtr)GCHandle.Alloc(action, GCHandleType.Normal));
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00013721 File Offset: 0x00011921
		public override void StopRunning()
		{
			this.device.StopRunning();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0001372E File Offset: 0x0001192E
		public NativeAudioDevice(IntPtr device)
		{
			this.device = device;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00013740 File Offset: 0x00011940
		~NativeAudioDevice()
		{
			this.device.Dispose();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00013774 File Offset: 0x00011974
		[MonoPInvokeCallback(typeof(Bridge.SampleBufferDelegate))]
		private static void OnSampleBuffer(IntPtr context, IntPtr sampleBuffer, int sampleCount, long timestamp)
		{
			float[] array = new float[sampleCount];
			Marshal.Copy(sampleBuffer, array, 0, sampleCount);
			(((GCHandle)context).Target as Action<float[], long>)(array, timestamp);
		}

		// Token: 0x040003CC RID: 972
		private readonly IntPtr device;
	}
}
