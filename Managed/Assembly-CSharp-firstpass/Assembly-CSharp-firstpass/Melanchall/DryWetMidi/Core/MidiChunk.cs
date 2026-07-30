using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000116 RID: 278
	public abstract class MidiChunk
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x0001CA90 File Offset: 0x0001AC90
		public MidiChunk(string id)
		{
			ThrowIfArgument.IsNull("id", id);
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("ID is empty string.", "id");
			}
			if (id.Length != 4)
			{
				throw new ArgumentException(string.Format("ID length doesn't equal {0}.", 4), "id");
			}
			this.ChunkId = id;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001CAF1 File Offset: 0x0001ACF1
		public string ChunkId { get; }

		// Token: 0x06000751 RID: 1873
		public abstract MidiChunk Clone();

		// Token: 0x06000752 RID: 1874 RVA: 0x0001CAF9 File Offset: 0x0001ACF9
		public static string[] GetStandardChunkIds()
		{
			return StandardChunkIds.GetIds();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001CB00 File Offset: 0x0001AD00
		public static bool Equals(MidiChunk chunk1, MidiChunk chunk2)
		{
			string text;
			return MidiChunk.Equals(chunk1, chunk2, out text);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001CB16 File Offset: 0x0001AD16
		public static bool Equals(MidiChunk chunk1, MidiChunk chunk2, out string message)
		{
			return MidiChunk.Equals(chunk1, chunk2, null, out message);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001CB21 File Offset: 0x0001AD21
		public static bool Equals(MidiChunk chunk1, MidiChunk chunk2, MidiChunkEqualityCheckSettings settings, out string message)
		{
			return MidiChunkEquality.Equals(chunk1, chunk2, settings ?? new MidiChunkEqualityCheckSettings(), out message);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001CB38 File Offset: 0x0001AD38
		internal void Read(MidiReader reader, ReadingSettings settings)
		{
			uint num = reader.ReadDword();
			long position = reader.Position;
			this.ReadContent(reader, settings, num);
			long num2 = reader.Position - position;
			if (settings.InvalidChunkSizePolicy == InvalidChunkSizePolicy.Abort && num2 != (long)((ulong)num))
			{
				throw new InvalidChunkSizeException(base.GetType(), (long)((ulong)num), num2);
			}
			long num3 = (long)((ulong)num - (ulong)num2);
			if (num3 > 0L)
			{
				reader.Position += Math.Min(num3, reader.Length);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
		internal void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteString(this.ChunkId);
			uint contentSize = this.GetContentSize(settings);
			writer.WriteDword(contentSize);
			this.WriteContent(writer, settings);
		}

		// Token: 0x06000758 RID: 1880
		protected abstract void ReadContent(MidiReader reader, ReadingSettings settings, uint size);

		// Token: 0x06000759 RID: 1881
		protected abstract void WriteContent(MidiWriter writer, WritingSettings settings);

		// Token: 0x0600075A RID: 1882
		protected abstract uint GetContentSize(WritingSettings settings);

		// Token: 0x0400083E RID: 2110
		public const int IdLength = 4;
	}
}
