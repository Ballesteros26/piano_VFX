using System;
using System.IO;

namespace Mono.Audio
{
	// Token: 0x02000006 RID: 6
	internal class WavData : AudioData
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000020F4 File Offset: 0x000002F4
		public WavData(Stream data)
		{
			this.stream = data;
			byte[] array = new byte[44];
			int num = this.stream.Read(array, 0, 12);
			if (num != 12 || array[0] != 82 || array[1] != 73 || array[2] != 70 || array[3] != 70 || array[8] != 87 || array[9] != 65 || array[10] != 86 || array[11] != 69)
			{
				throw new Exception("incorrect format" + num);
			}
			num = this.stream.Read(array, 0, 8);
			if (num != 8 || array[0] != 102 || array[1] != 109 || array[2] != 116 || array[3] != 32)
			{
				throw new Exception("incorrect format (fmt)");
			}
			int num2 = (int)array[4];
			num2 |= (int)array[5] << 8;
			num2 |= (int)array[6] << 16;
			num2 |= (int)array[7] << 24;
			num = this.stream.Read(array, 0, num2);
			if (num2 != num)
			{
				throw new Exception(string.Concat(new object[] { "Error: Can't Read ", num2, " bytes from stream (", num, " bytes read" }));
			}
			int num3 = 0;
			if (((int)array[num3++] | ((int)array[num3++] << 8)) != 1)
			{
				throw new Exception("incorrect format (not PCM)");
			}
			this.channels = (short)((int)array[num3++] | ((int)array[num3++] << 8));
			this.sample_rate = (int)array[num3++];
			this.sample_rate |= (int)array[num3++] << 8;
			this.sample_rate |= (int)array[num3++] << 16;
			this.sample_rate |= (int)array[num3++] << 24;
			int num4 = (int)array[num3++] | ((int)array[num3++] << 8) | ((int)array[num3++] << 16);
			byte b = array[num3++];
			num3 += 2;
			int num5 = (int)array[num3++] | ((int)array[num3++] << 8);
			if (num5 != 8)
			{
				if (num5 != 16)
				{
					throw new Exception("bits per sample");
				}
				this.frame_divider = 2;
				this.format = AudioFormat.S16_LE;
			}
			else
			{
				this.frame_divider = 1;
				this.format = AudioFormat.U8;
			}
			num = this.stream.Read(array, 0, 8);
			if (num != 8)
			{
				return;
			}
			if (array[0] == 102 && array[1] == 97 && array[2] == 99 && array[3] == 116)
			{
				int num6 = (int)array[4];
				num6 |= (int)array[5] << 8;
				num6 |= (int)array[6] << 16;
				num6 |= (int)array[7] << 24;
				num = this.stream.Read(array, 0, num6);
				num = this.stream.Read(array, 0, 8);
			}
			if (array[0] == 100 && array[1] == 97 && array[2] == 116 && array[3] == 97)
			{
				int num7 = (int)array[4];
				num7 |= (int)array[5] << 8;
				num7 |= (int)array[6] << 16;
				num7 |= (int)array[7] << 24;
				this.data_len = num7;
				this.data_offset = this.stream.Position;
				return;
			}
			throw new Exception("incorrect format (data/fact chunck)");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
		public override void Play(AudioDevice dev)
		{
			int num = 0;
			int chunkSize = (int)dev.ChunkSize;
			int num2 = this.data_len;
			byte[] array = new byte[this.data_len];
			byte[] array2 = new byte[chunkSize];
			this.stream.Position = this.data_offset;
			this.stream.Read(array, 0, this.data_len);
			while (!this.IsStopped && num2 >= 0)
			{
				Buffer.BlockCopy(array, num, array2, 0, chunkSize);
				int num3 = dev.PlaySample(array2, chunkSize / (int)(this.frame_divider * (ushort)this.channels));
				if (num3 > 0)
				{
					num += num3 * (int)this.frame_divider * (int)this.channels;
					num2 -= num3 * (int)this.frame_divider * (int)this.channels;
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000024C4 File Offset: 0x000006C4
		public override int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024CC File Offset: 0x000006CC
		public override int Rate
		{
			get
			{
				return this.sample_rate;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000024D4 File Offset: 0x000006D4
		public override AudioFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x040006A0 RID: 1696
		private Stream stream;

		// Token: 0x040006A1 RID: 1697
		private short channels;

		// Token: 0x040006A2 RID: 1698
		private ushort frame_divider;

		// Token: 0x040006A3 RID: 1699
		private int sample_rate;

		// Token: 0x040006A4 RID: 1700
		private int data_len;

		// Token: 0x040006A5 RID: 1701
		private long data_offset;

		// Token: 0x040006A6 RID: 1702
		private AudioFormat format;
	}
}
