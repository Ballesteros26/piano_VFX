using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AOT;

namespace NatSuite.Recorders.Internal
{
	// Token: 0x02000048 RID: 72
	public sealed class NativeRecorder : IMediaRecorder
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00014374 File Offset: 0x00012574
		[TupleElementNames(new string[] { "width", "height" })]
		public ValueTuple<int, int> frameSize
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get
			{
				int num;
				int num2;
				this.recorder.FrameSize(out num, out num2);
				return new ValueTuple<int, int>(num, num2);
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00014398 File Offset: 0x00012598
		public NativeRecorder(Func<Bridge.CompletionHandler, IntPtr, IntPtr> recorderCreator)
		{
			this.recordingTask = new TaskCompletionSource<string>();
			GCHandle gchandle = GCHandle.Alloc(this.recordingTask, GCHandleType.Normal);
			this.recorder = recorderCreator(new Bridge.CompletionHandler(NativeRecorder.OnRecording), (IntPtr)gchandle);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000143E4 File Offset: 0x000125E4
		public void CommitFrame<T>(T[] pixelBuffer, long timestamp) where T : struct
		{
			GCHandle gchandle = GCHandle.Alloc(pixelBuffer, GCHandleType.Pinned);
			this.CommitFrame(gchandle.AddrOfPinnedObject(), timestamp);
			gchandle.Free();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0001440E File Offset: 0x0001260E
		public void CommitFrame(IntPtr nativeBuffer, long timestamp)
		{
			this.recorder.CommitFrame(nativeBuffer, timestamp);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0001441D File Offset: 0x0001261D
		public void CommitSamples(float[] sampleBuffer, long timestamp)
		{
			this.recorder.CommitSamples(sampleBuffer, sampleBuffer.Length, timestamp);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0001442F File Offset: 0x0001262F
		public Task<string> FinishWriting()
		{
			this.recorder.FinishWriting();
			return this.recordingTask.Task;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00014448 File Offset: 0x00012648
		[MonoPInvokeCallback(typeof(Bridge.CompletionHandler))]
		private static void OnRecording(IntPtr context, IntPtr path)
		{
			GCHandle gchandle = (GCHandle)context;
			TaskCompletionSource<string> taskCompletionSource = gchandle.Target as TaskCompletionSource<string>;
			gchandle.Free();
			if (path != IntPtr.Zero)
			{
				taskCompletionSource.SetResult(Marshal.PtrToStringAnsi(path));
				return;
			}
			taskCompletionSource.SetException(new Exception("Recorder failed to finish writing"));
		}

		// Token: 0x040003E1 RID: 993
		private readonly IntPtr recorder;

		// Token: 0x040003E2 RID: 994
		private readonly TaskCompletionSource<string> recordingTask;
	}
}
