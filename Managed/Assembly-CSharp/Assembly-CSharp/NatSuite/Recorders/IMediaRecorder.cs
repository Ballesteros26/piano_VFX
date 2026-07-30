using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NatSuite.Recorders
{
	// Token: 0x02000043 RID: 67
	public interface IMediaRecorder
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000279 RID: 633
		[TupleElementNames(new string[] { "width", "height" })]
		ValueTuple<int, int> frameSize
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get;
		}

		// Token: 0x0600027A RID: 634
		void CommitFrame<T>(T[] pixelBuffer, long timestamp) where T : struct;

		// Token: 0x0600027B RID: 635
		void CommitFrame(IntPtr nativeBuffer, long timestamp);

		// Token: 0x0600027C RID: 636
		void CommitSamples(float[] sampleBuffer, long timestamp);

		// Token: 0x0600027D RID: 637
		Task<string> FinishWriting();
	}
}
