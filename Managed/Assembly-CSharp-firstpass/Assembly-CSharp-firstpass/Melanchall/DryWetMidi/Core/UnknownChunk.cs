using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000118 RID: 280
	public sealed class UnknownChunk : MidiChunk
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x0001D146 File Offset: 0x0001B346
		internal UnknownChunk(string id)
			: base(id)
		{
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001D14F File Offset: 0x0001B34F
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0001D157 File Offset: 0x0001B357
		public byte[] Data { get; private set; }

		// Token: 0x0600076B RID: 1899 RVA: 0x0001D160 File Offset: 0x0001B360
		public override MidiChunk Clone()
		{
			return new UnknownChunk(base.ChunkId)
			{
				Data = (byte[])this.Data.Clone()
			};
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001D184 File Offset: 0x0001B384
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, uint size)
		{
			long num = reader.Length - reader.Position;
			long num2 = ((num < (long)((ulong)size)) ? num : ((long)((ulong)size)));
			byte[] array = reader.ReadBytes((int)Math.Min(num2, 2147483647L));
			if ((long)array.Length < (long)((ulong)size) && settings.NotEnoughBytesPolicy == NotEnoughBytesPolicy.Abort)
			{
				throw new NotEnoughBytesException("Unknown chunk's data cannot be read since the reader's underlying stream doesn't have enough bytes.", (long)((ulong)size), (long)array.Length);
			}
			this.Data = array;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			byte[] data = this.Data;
			if (data != null)
			{
				writer.WriteBytes(data);
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001D206 File Offset: 0x0001B406
		protected override uint GetContentSize(WritingSettings settings)
		{
			byte[] data = this.Data;
			if (data == null)
			{
				return 0U;
			}
			return (uint)data.Length;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001D216 File Offset: 0x0001B416
		public override string ToString()
		{
			return "Unknown Chunk";
		}
	}
}
