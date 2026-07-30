using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NatSuite.Recorders.Internal;

namespace NatSuite.Recorders
{
	// Token: 0x02000045 RID: 69
	[Doc("MP4Recorder")]
	public sealed class MP4Recorder : IMediaRecorder
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00014064 File Offset: 0x00012264
		[TupleElementNames(new string[] { "width", "height" })]
		[Doc("FrameSize")]
		public ValueTuple<int, int> frameSize
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get
			{
				return this.recorder.frameSize;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00014074 File Offset: 0x00012274
		[Doc("MP4RecorderCtor")]
		public MP4Recorder(int width, int height, float framerate, int sampleRate = 0, int channelCount = 0, int bitrate = 5909760, int keyframeInterval = 3)
		{
			this.recorder = new NativeRecorder((Bridge.CompletionHandler callback, IntPtr context) => Bridge.CreateMP4Recorder(width, height, framerate, bitrate, keyframeInterval, sampleRate, channelCount, Utility.GetPath(".mp4"), callback, context));
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000140D9 File Offset: 0x000122D9
		[Doc("CommitFrame")]
		[Code("RecordWebCam")]
		public void CommitFrame<T>(T[] pixelBuffer, long timestamp) where T : struct
		{
			this.recorder.CommitFrame<T>(pixelBuffer, timestamp);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000140E8 File Offset: 0x000122E8
		[Doc("CommitFrame")]
		public void CommitFrame(IntPtr nativeBuffer, long timestamp)
		{
			this.recorder.CommitFrame(nativeBuffer, timestamp);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000140F7 File Offset: 0x000122F7
		[Doc("CommitSamples", "CommitSamplesDiscussion")]
		[Code("RecordPCM")]
		public void CommitSamples(float[] sampleBuffer, long timestamp)
		{
			this.recorder.CommitSamples(sampleBuffer, timestamp);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00014106 File Offset: 0x00012306
		[Doc("FinishWriting", "FinishWritingDiscussion")]
		public Task<string> FinishWriting()
		{
			return this.recorder.FinishWriting();
		}

		// Token: 0x040003DB RID: 987
		private readonly IMediaRecorder recorder;
	}
}
