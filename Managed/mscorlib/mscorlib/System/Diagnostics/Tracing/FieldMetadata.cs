using System;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AAA RID: 2730
	internal class FieldMetadata
	{
		// Token: 0x06006330 RID: 25392 RVA: 0x00142D8E File Offset: 0x00140F8E
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, bool variableCount)
			: this(name, type, tags, variableCount ? 64 : 0, 0, null)
		{
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x00142DA4 File Offset: 0x00140FA4
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, ushort fixedCount)
			: this(name, type, tags, 32, fixedCount, null)
		{
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x00142DB4 File Offset: 0x00140FB4
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, byte[] custom)
			: this(name, type, tags, 96, checked((ushort)((custom == null) ? 0 : custom.Length)), custom)
		{
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x00142DD0 File Offset: 0x00140FD0
		private FieldMetadata(string name, TraceLoggingDataType dataType, EventFieldTags tags, byte countFlags, ushort fixedCount = 0, byte[] custom = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "This usually means that the object passed to Write is of a type that does not support being used as the top-level object in an event, e.g. a primitive or built-in type.");
			}
			Statics.CheckName(name);
			int num = (int)(dataType & (TraceLoggingDataType)31);
			this.name = name;
			this.nameSize = Encoding.UTF8.GetByteCount(this.name) + 1;
			this.inType = (byte)(num | (int)countFlags);
			this.outType = (byte)((dataType >> 8) & (TraceLoggingDataType)127);
			this.tags = tags;
			this.fixedCount = fixedCount;
			this.custom = custom;
			if (countFlags != 0)
			{
				if (num == 0)
				{
					throw new NotSupportedException(Environment.GetResourceString("Arrays of Nil are not supported."));
				}
				if (num == 14)
				{
					throw new NotSupportedException(Environment.GetResourceString("Arrays of Binary are not supported."));
				}
				if (num == 1 || num == 2)
				{
					throw new NotSupportedException(Environment.GetResourceString("Arrays of null-terminated string are not supported."));
				}
			}
			if ((this.tags & (EventFieldTags)268435455) != EventFieldTags.None)
			{
				this.outType |= 128;
			}
			if (this.outType != 0)
			{
				this.inType |= 128;
			}
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x00142ECF File Offset: 0x001410CF
		public void IncrementStructFieldCount()
		{
			this.inType |= 128;
			this.outType += 1;
			if ((this.outType & 127) == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Too many fields in structure."));
			}
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x00142F10 File Offset: 0x00141110
		public void Encode(ref int pos, byte[] metadata)
		{
			if (metadata != null)
			{
				Encoding.UTF8.GetBytes(this.name, 0, this.name.Length, metadata, pos);
			}
			pos += this.nameSize;
			if (metadata != null)
			{
				metadata[pos] = this.inType;
			}
			pos++;
			if ((this.inType & 128) != 0)
			{
				if (metadata != null)
				{
					metadata[pos] = this.outType;
				}
				pos++;
				if ((this.outType & 128) != 0)
				{
					Statics.EncodeTags((int)this.tags, ref pos, metadata);
				}
			}
			if ((this.inType & 32) != 0)
			{
				if (metadata != null)
				{
					metadata[pos] = (byte)this.fixedCount;
					metadata[pos + 1] = (byte)(this.fixedCount >> 8);
				}
				pos += 2;
				if (96 == (this.inType & 96) && this.fixedCount != 0)
				{
					if (metadata != null)
					{
						Buffer.BlockCopy(this.custom, 0, metadata, pos, (int)this.fixedCount);
					}
					pos += (int)this.fixedCount;
				}
			}
		}

		// Token: 0x0400316D RID: 12653
		private readonly string name;

		// Token: 0x0400316E RID: 12654
		private readonly int nameSize;

		// Token: 0x0400316F RID: 12655
		private readonly EventFieldTags tags;

		// Token: 0x04003170 RID: 12656
		private readonly byte[] custom;

		// Token: 0x04003171 RID: 12657
		private readonly ushort fixedCount;

		// Token: 0x04003172 RID: 12658
		private byte inType;

		// Token: 0x04003173 RID: 12659
		private byte outType;
	}
}
