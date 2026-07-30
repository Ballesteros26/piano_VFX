using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AED RID: 2797
	internal class TraceLoggingMetadataCollector
	{
		// Token: 0x060064C8 RID: 25800 RVA: 0x0014A63B File Offset: 0x0014883B
		internal TraceLoggingMetadataCollector()
		{
			this.impl = new TraceLoggingMetadataCollector.Impl();
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x0014A659 File Offset: 0x00148859
		private TraceLoggingMetadataCollector(TraceLoggingMetadataCollector other, FieldMetadata group)
		{
			this.impl = other.impl;
			this.currentGroup = group;
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060064CA RID: 25802 RVA: 0x0014A67F File Offset: 0x0014887F
		// (set) Token: 0x060064CB RID: 25803 RVA: 0x0014A687 File Offset: 0x00148887
		internal EventFieldTags Tags { get; set; }

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060064CC RID: 25804 RVA: 0x0014A690 File Offset: 0x00148890
		internal int ScratchSize
		{
			get
			{
				return (int)this.impl.scratchSize;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060064CD RID: 25805 RVA: 0x0014A69D File Offset: 0x0014889D
		internal int DataCount
		{
			get
			{
				return (int)this.impl.dataCount;
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x060064CE RID: 25806 RVA: 0x0014A6AA File Offset: 0x001488AA
		internal int PinCount
		{
			get
			{
				return (int)this.impl.pinCount;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060064CF RID: 25807 RVA: 0x0014A6B7 File Offset: 0x001488B7
		private bool BeginningBufferedArray
		{
			get
			{
				return this.bufferedArrayFieldCount == 0;
			}
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x0014A6C4 File Offset: 0x001488C4
		public TraceLoggingMetadataCollector AddGroup(string name)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = this;
			if (name != null || this.BeginningBufferedArray)
			{
				FieldMetadata fieldMetadata = new FieldMetadata(name, TraceLoggingDataType.Struct, this.Tags, this.BeginningBufferedArray);
				this.AddField(fieldMetadata);
				traceLoggingMetadataCollector = new TraceLoggingMetadataCollector(this, fieldMetadata);
			}
			return traceLoggingMetadataCollector;
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x0014A704 File Offset: 0x00148904
		public void AddScalar(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			int num;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
				break;
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
				goto IL_006F;
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.HexInt32:
				num = 4;
				goto IL_008B;
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt64:
				num = 8;
				goto IL_008B;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case (TraceLoggingDataType)19:
				goto IL_0080;
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.SystemTime:
				num = 16;
				goto IL_008B;
			default:
				if (traceLoggingDataType != TraceLoggingDataType.Char8)
				{
					if (traceLoggingDataType != TraceLoggingDataType.Char16)
					{
						goto IL_0080;
					}
					goto IL_006F;
				}
				break;
			}
			num = 1;
			goto IL_008B;
			IL_006F:
			num = 2;
			goto IL_008B;
			IL_0080:
			throw new ArgumentOutOfRangeException("type");
			IL_008B:
			this.impl.AddScalar(num);
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x0014A7C4 File Offset: 0x001489C4
		public void AddBinary(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			if (traceLoggingDataType != TraceLoggingDataType.Binary && traceLoggingDataType - TraceLoggingDataType.CountedUtf16String > 1)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x0014A820 File Offset: 0x00148A20
		public void AddArray(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Utf16String:
			case TraceLoggingDataType.MbcsString:
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt32:
			case TraceLoggingDataType.HexInt64:
				goto IL_007C;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case TraceLoggingDataType.SystemTime:
			case (TraceLoggingDataType)19:
				break;
			default:
				if (traceLoggingDataType == TraceLoggingDataType.Char8 || traceLoggingDataType == TraceLoggingDataType.Char16)
				{
					goto IL_007C;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
			IL_007C:
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("Nested arrays/enumerables are not supported."));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, true));
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x0014A8EC File Offset: 0x00148AEC
		public void BeginBufferedArray()
		{
			if (this.bufferedArrayFieldCount >= 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Nested arrays/enumerables are not supported."));
			}
			this.bufferedArrayFieldCount = 0;
			this.impl.BeginBuffered();
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x0014A919 File Offset: 0x00148B19
		public void EndBufferedArray()
		{
			if (this.bufferedArrayFieldCount != 1)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Incorrectly-authored TypeInfo - a type should be serialized as one field or as one group"));
			}
			this.bufferedArrayFieldCount = int.MinValue;
			this.impl.EndBuffered();
		}

		// Token: 0x060064D6 RID: 25814 RVA: 0x0014A94C File Offset: 0x00148B4C
		public void AddCustom(string name, TraceLoggingDataType type, byte[] metadata)
		{
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("Enumerables of custom-serialized data are not supported"));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, metadata));
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x0014A99C File Offset: 0x00148B9C
		internal byte[] GetMetadata()
		{
			byte[] array = new byte[this.impl.Encode(null)];
			this.impl.Encode(array);
			return array;
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x0014A9C9 File Offset: 0x00148BC9
		private void AddField(FieldMetadata fieldMetadata)
		{
			this.Tags = EventFieldTags.None;
			this.bufferedArrayFieldCount++;
			this.impl.fields.Add(fieldMetadata);
			if (this.currentGroup != null)
			{
				this.currentGroup.IncrementStructFieldCount();
			}
		}

		// Token: 0x040031FD RID: 12797
		private readonly TraceLoggingMetadataCollector.Impl impl;

		// Token: 0x040031FE RID: 12798
		private readonly FieldMetadata currentGroup;

		// Token: 0x040031FF RID: 12799
		private int bufferedArrayFieldCount = int.MinValue;

		// Token: 0x02000AEE RID: 2798
		private class Impl
		{
			// Token: 0x060064D9 RID: 25817 RVA: 0x0014AA04 File Offset: 0x00148C04
			public void AddScalar(int size)
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						if (!this.scalar)
						{
							this.dataCount += 1;
						}
						this.scalar = true;
						this.scratchSize = (short)((int)this.scratchSize + size);
					}
				}
			}

			// Token: 0x060064DA RID: 25818 RVA: 0x0014AA3B File Offset: 0x00148C3B
			public void AddNonscalar()
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						this.scalar = false;
						this.pinCount += 1;
						this.dataCount += 1;
					}
				}
			}

			// Token: 0x060064DB RID: 25819 RVA: 0x0014AA6A File Offset: 0x00148C6A
			public void BeginBuffered()
			{
				if (this.bufferNesting == 0)
				{
					this.AddNonscalar();
				}
				this.bufferNesting++;
			}

			// Token: 0x060064DC RID: 25820 RVA: 0x0014AA88 File Offset: 0x00148C88
			public void EndBuffered()
			{
				this.bufferNesting--;
			}

			// Token: 0x060064DD RID: 25821 RVA: 0x0014AA98 File Offset: 0x00148C98
			public int Encode(byte[] metadata)
			{
				int num = 0;
				foreach (FieldMetadata fieldMetadata in this.fields)
				{
					fieldMetadata.Encode(ref num, metadata);
				}
				return num;
			}

			// Token: 0x04003201 RID: 12801
			internal readonly List<FieldMetadata> fields = new List<FieldMetadata>();

			// Token: 0x04003202 RID: 12802
			internal short scratchSize;

			// Token: 0x04003203 RID: 12803
			internal sbyte dataCount;

			// Token: 0x04003204 RID: 12804
			internal sbyte pinCount;

			// Token: 0x04003205 RID: 12805
			private int bufferNesting;

			// Token: 0x04003206 RID: 12806
			private bool scalar;
		}
	}
}
