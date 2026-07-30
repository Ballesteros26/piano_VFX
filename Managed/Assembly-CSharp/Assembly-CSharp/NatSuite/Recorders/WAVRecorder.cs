using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NatSuite.Recorders.Internal;

namespace NatSuite.Recorders
{
	// Token: 0x02000046 RID: 70
	[Doc("WAVRecorder")]
	public sealed class WAVRecorder : IMediaRecorder
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00014114 File Offset: 0x00012314
		[TupleElementNames(new string[] { "width", "height" })]
		[Doc("FrameSizeNotSupported")]
		public ValueTuple<int, int> frameSize
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get
			{
				return default(ValueTuple<int, int>);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001412C File Offset: 0x0001232C
		[Doc("WAVRecorderCtor")]
		public WAVRecorder(int sampleRate, int channelCount)
		{
			this.sampleRate = this.sampleCount;
			this.channelCount = channelCount;
			this.stream = new FileStream(Utility.GetPath(".wav"), FileMode.Create);
			this.stream.Write(new byte[44], 0, 44);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00013E17 File Offset: 0x00012017
		[Doc("CommitFrameNotSupported")]
		public void CommitFrame<T>(T[] pixelBuffer, long timestamp) where T : struct
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00013E17 File Offset: 0x00012017
		[Doc("CommitFrameNotSupported")]
		public void CommitFrame(IntPtr nativeBuffer, long timestamp)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00014180 File Offset: 0x00012380
		[Doc("CommitSamples")]
		public void CommitSamples(float[] sampleBuffer, long timestamp)
		{
			short[] array = new short[sampleBuffer.Length];
			byte[] array2 = new byte[Buffer.ByteLength(array)];
			for (int i = 0; i < sampleBuffer.Length; i++)
			{
				array[i] = (short)(sampleBuffer[i] * 32767f);
			}
			Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
			this.stream.Write(array2, 0, array2.Length);
			this.sampleCount += sampleBuffer.Length;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000141EC File Offset: 0x000123EC
		[Doc("FinishWriting")]
		public Task<string> FinishWriting()
		{
			this.stream.Seek(0L, SeekOrigin.Begin);
			this.stream.Write(Encoding.UTF8.GetBytes("RIFF"), 0, 4);
			this.stream.Write(BitConverter.GetBytes(this.stream.Length - 8L), 0, 4);
			this.stream.Write(Encoding.UTF8.GetBytes("WAVE"), 0, 4);
			this.stream.Write(Encoding.UTF8.GetBytes("fmt "), 0, 4);
			this.stream.Write(BitConverter.GetBytes(16), 0, 4);
			this.stream.Write(BitConverter.GetBytes(1), 0, 2);
			this.stream.Write(BitConverter.GetBytes(this.channelCount), 0, 2);
			this.stream.Write(BitConverter.GetBytes(this.sampleRate), 0, 4);
			this.stream.Write(BitConverter.GetBytes(this.sampleRate * this.channelCount * 2), 0, 4);
			this.stream.Write(BitConverter.GetBytes((ushort)(this.channelCount * 2)), 0, 2);
			this.stream.Write(BitConverter.GetBytes(16), 0, 2);
			this.stream.Write(Encoding.UTF8.GetBytes("data"), 0, 4);
			this.stream.Write(BitConverter.GetBytes(this.sampleCount * 2), 0, 4);
			this.stream.Dispose();
			return Task.FromResult<string>(this.stream.Name);
		}

		// Token: 0x040003DC RID: 988
		private readonly int sampleRate;

		// Token: 0x040003DD RID: 989
		private readonly int channelCount;

		// Token: 0x040003DE RID: 990
		private readonly FileStream stream;

		// Token: 0x040003DF RID: 991
		private int sampleCount;
	}
}
