using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NatSuite.Recorders.Internal;

namespace NatSuite.Recorders
{
	// Token: 0x02000041 RID: 65
	[Doc("GIFRecorder")]
	public sealed class GIFRecorder : IMediaRecorder
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00013DA4 File Offset: 0x00011FA4
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

		// Token: 0x0600026E RID: 622 RVA: 0x00013DB4 File Offset: 0x00011FB4
		[Doc("GIFRecorderCtor")]
		public GIFRecorder(int width, int height, float frameDuration)
		{
			this.recorder = new NativeRecorder((Bridge.CompletionHandler callback, IntPtr context) => Bridge.CreateGIFRecorder(width, height, frameDuration, Utility.GetPath(".gif"), callback, context));
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00013DF9 File Offset: 0x00011FF9
		[Doc("CommitFrame")]
		[Code("RecordWebCam")]
		public void CommitFrame<T>(T[] pixelBuffer, long timestamp) where T : struct
		{
			this.recorder.CommitFrame<T>(pixelBuffer, timestamp);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00013E08 File Offset: 0x00012008
		[Doc("CommitFrame")]
		public void CommitFrame(IntPtr nativeBuffer, long timestamp)
		{
			this.recorder.CommitFrame(nativeBuffer, timestamp);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00013E17 File Offset: 0x00012017
		[Doc("CommitSamplesNotSupported")]
		public void CommitSamples(float[] sampleBuffer, long timestamp)
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00013E19 File Offset: 0x00012019
		[Doc("FinishWriting", "FinishWritingDiscussion")]
		public Task<string> FinishWriting()
		{
			return this.recorder.FinishWriting();
		}

		// Token: 0x040003D6 RID: 982
		private readonly IMediaRecorder recorder;
	}
}
