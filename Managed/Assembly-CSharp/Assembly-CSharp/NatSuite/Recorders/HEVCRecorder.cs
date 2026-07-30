using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NatSuite.Recorders.Internal;

namespace NatSuite.Recorders
{
	// Token: 0x02000042 RID: 66
	[Doc("HEVCRecorder")]
	public sealed class HEVCRecorder : IMediaRecorder
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00013E26 File Offset: 0x00012026
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

		// Token: 0x06000274 RID: 628 RVA: 0x00013E34 File Offset: 0x00012034
		[Doc("HEVCRecorderCtor")]
		public HEVCRecorder(int width, int height, float framerate, int sampleRate = 0, int channelCount = 0, int bitrate = 5909760, int keyframeInterval = 3)
		{
			this.recorder = new NativeRecorder((Bridge.CompletionHandler callback, IntPtr context) => Bridge.CreateHEVCRecorder(width, height, framerate, bitrate, keyframeInterval, sampleRate, channelCount, Utility.GetPath(".mp4"), callback, context));
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00013E99 File Offset: 0x00012099
		[Doc("CommitFrame")]
		[Code("RecordWebCam")]
		public void CommitFrame<T>(T[] pixelBuffer, long timestamp) where T : struct
		{
			this.recorder.CommitFrame<T>(pixelBuffer, timestamp);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00013EA8 File Offset: 0x000120A8
		[Doc("CommitFrame")]
		public void CommitFrame(IntPtr nativeBuffer, long timestamp)
		{
			this.recorder.CommitFrame(nativeBuffer, timestamp);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00013EB7 File Offset: 0x000120B7
		[Doc("CommitSamples", "CommitSamplesDiscussion")]
		[Code("RecordPCM")]
		public void CommitSamples(float[] sampleBuffer, long timestamp)
		{
			this.recorder.CommitSamples(sampleBuffer, timestamp);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00013EC6 File Offset: 0x000120C6
		[Doc("FinishWriting", "FinishWritingDiscussion")]
		public Task<string> FinishWriting()
		{
			return this.recorder.FinishWriting();
		}

		// Token: 0x040003D7 RID: 983
		private readonly IMediaRecorder recorder;
	}
}
