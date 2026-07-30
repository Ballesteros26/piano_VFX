using System;
using System.Runtime.InteropServices;

namespace NatSuite.Recorders.Internal
{
	// Token: 0x02000047 RID: 71
	public static class Bridge
	{
		// Token: 0x06000290 RID: 656
		[DllImport("NatCorder", EntryPoint = "NCCreateMP4Recorder")]
		public static extern IntPtr CreateMP4Recorder(int width, int height, float framerate, int bitrate, int keyframeInterval, int sampleRate, int channelCount, [MarshalAs(UnmanagedType.LPStr)] string recordingPath, Bridge.CompletionHandler callback, IntPtr context);

		// Token: 0x06000291 RID: 657
		[DllImport("NatCorder", EntryPoint = "NCCreateHEVCRecorder")]
		public static extern IntPtr CreateHEVCRecorder(int width, int height, float framerate, int bitrate, int keyframeInterval, int sampleRate, int channelCount, [MarshalAs(UnmanagedType.LPStr)] string recordingPath, Bridge.CompletionHandler callback, IntPtr context);

		// Token: 0x06000292 RID: 658
		[DllImport("NatCorder", EntryPoint = "NCCreateGIFRecorder")]
		public static extern IntPtr CreateGIFRecorder(int width, int height, float frameDuration, [MarshalAs(UnmanagedType.LPStr)] string recordingPath, Bridge.CompletionHandler callback, IntPtr context);

		// Token: 0x06000293 RID: 659
		[DllImport("NatCorder", EntryPoint = "NCFrameSize")]
		public static extern void FrameSize(this IntPtr recorder, out int width, out int height);

		// Token: 0x06000294 RID: 660
		[DllImport("NatCorder", EntryPoint = "NCCommitFrame")]
		public static extern void CommitFrame(this IntPtr recorder, IntPtr pixelBuffer, long timestamp);

		// Token: 0x06000295 RID: 661
		[DllImport("NatCorder", EntryPoint = "NCCommitSamples")]
		public static extern void CommitSamples(this IntPtr recorder, float[] sampleBuffer, int sampleCount, long timestamp);

		// Token: 0x06000296 RID: 662
		[DllImport("NatCorder", EntryPoint = "NCFinishWriting")]
		public static extern void FinishWriting(this IntPtr recorder);

		// Token: 0x040003E0 RID: 992
		private const string Assembly = "NatCorder";

		// Token: 0x02000088 RID: 136
		// (Invoke) Token: 0x06000387 RID: 903
		public delegate void CompletionHandler(IntPtr context, IntPtr path);
	}
}
