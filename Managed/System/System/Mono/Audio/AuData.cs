using System;
using System.IO;

namespace Mono.Audio
{
	// Token: 0x02000007 RID: 7
	internal class AuData : AudioData
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000024DC File Offset: 0x000006DC
		public AuData(Stream data)
		{
			this.stream = data;
			byte[] array = new byte[24];
			int num = this.stream.Read(array, 0, 24);
			if (num != 24 || array[0] != 46 || array[1] != 115 || array[2] != 110 || array[3] != 100)
			{
				throw new Exception("incorrect format" + num);
			}
			int num2 = (int)array[7];
			num2 |= (int)array[6] << 8;
			num2 |= (int)array[5] << 16;
			num2 |= (int)array[4] << 24;
			this.data_len = (int)array[11];
			this.data_len |= (int)array[10] << 8;
			this.data_len |= (int)array[9] << 16;
			this.data_len |= (int)array[8] << 24;
			int num3 = (int)array[15];
			num3 |= (int)array[14] << 8;
			num3 |= (int)array[13] << 16;
			num3 |= (int)array[12] << 24;
			this.sample_rate = (int)array[19];
			this.sample_rate |= (int)array[18] << 8;
			this.sample_rate |= (int)array[17] << 16;
			this.sample_rate |= (int)array[16] << 24;
			int num4 = (int)array[23];
			num4 |= (int)array[22] << 8;
			num4 |= (int)array[21] << 16;
			num4 |= (int)array[20] << 24;
			this.channels = (short)num4;
			if (num2 < 24 || (num4 != 1 && num4 != 2))
			{
				throw new Exception("incorrect format offset" + num2);
			}
			if (num2 != 24)
			{
				for (int i = 24; i < num2; i++)
				{
					this.stream.ReadByte();
				}
			}
			if (num3 == 1)
			{
				this.frame_divider = 1;
				this.format = AudioFormat.MU_LAW;
				if (this.data_len == -1)
				{
					this.data_len = (int)this.stream.Length - num2;
				}
				return;
			}
			throw new Exception("incorrect format encoding" + num3);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026D0 File Offset: 0x000008D0
		public override void Play(AudioDevice dev)
		{
			int num = 0;
			int chunkSize = (int)dev.ChunkSize;
			int num2 = this.data_len;
			byte[] array = new byte[this.data_len];
			byte[] array2 = new byte[chunkSize];
			this.stream.Position = 0L;
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002780 File Offset: 0x00000980
		public override int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002788 File Offset: 0x00000988
		public override int Rate
		{
			get
			{
				return this.sample_rate;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002790 File Offset: 0x00000990
		public override AudioFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x040006A7 RID: 1703
		private Stream stream;

		// Token: 0x040006A8 RID: 1704
		private short channels;

		// Token: 0x040006A9 RID: 1705
		private ushort frame_divider;

		// Token: 0x040006AA RID: 1706
		private int sample_rate;

		// Token: 0x040006AB RID: 1707
		private int data_len;

		// Token: 0x040006AC RID: 1708
		private AudioFormat format;
	}
}
