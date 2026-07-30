using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A9A RID: 2714
	[SecurityCritical]
	internal struct DataCollector
	{
		// Token: 0x060062C8 RID: 25288 RVA: 0x001420B8 File Offset: 0x001402B8
		internal unsafe void Enable(byte* scratch, int scratchSize, EventSource.EventData* datas, int dataCount, GCHandle* pins, int pinCount)
		{
			this.datasStart = datas;
			this.scratchEnd = scratch + scratchSize;
			this.datasEnd = datas + dataCount;
			this.pinsEnd = pins + pinCount;
			this.scratch = scratch;
			this.datas = datas;
			this.pins = pins;
			this.writingScalars = false;
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x00142117 File Offset: 0x00140317
		internal void Disable()
		{
			this = default(DataCollector);
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x00142120 File Offset: 0x00140320
		internal unsafe EventSource.EventData* Finish()
		{
			this.ScalarsEnd();
			return this.datas;
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x00142130 File Offset: 0x00140330
		internal unsafe void AddScalar(void* value, int size)
		{
			if (this.bufferNesting != 0)
			{
				int num = this.bufferPos;
				int num2;
				checked
				{
					this.bufferPos += size;
					this.EnsureBuffer();
					num2 = 0;
				}
				while (num2 != size)
				{
					this.buffer[num] = ((byte*)value)[num2];
					num2++;
					num++;
				}
				return;
			}
			byte* ptr = this.scratch;
			byte* ptr2 = ptr + size;
			if (this.scratchEnd < ptr2)
			{
				throw new IndexOutOfRangeException(Environment.GetResourceString("Getting out of bounds during scalar addition."));
			}
			this.ScalarsBegin();
			this.scratch = ptr2;
			for (int num3 = 0; num3 != size; num3++)
			{
				ptr[num3] = ((byte*)value)[num3];
			}
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x001421D0 File Offset: 0x001403D0
		internal unsafe void AddBinary(string value, int size)
		{
			if (size > 65535)
			{
				size = 65534;
			}
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(size + 2);
			}
			this.AddScalar((void*)(&size), 2);
			if (size != 0)
			{
				if (this.bufferNesting == 0)
				{
					this.ScalarsEnd();
					this.PinArray(value, size);
					return;
				}
				int num = this.bufferPos;
				checked
				{
					this.bufferPos += size;
					this.EnsureBuffer();
				}
				fixed (string text = value)
				{
					void* ptr = text;
					if (ptr != null)
					{
						ptr = (void*)((byte*)ptr + RuntimeHelpers.OffsetToStringData);
					}
					Marshal.Copy((IntPtr)ptr, this.buffer, num, size);
				}
			}
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x00142261 File Offset: 0x00140461
		internal void AddBinary(Array value, int size)
		{
			this.AddArray(value, size, 1);
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x0014226C File Offset: 0x0014046C
		internal unsafe void AddArray(Array value, int length, int itemSize)
		{
			if (length > 65535)
			{
				length = 65535;
			}
			int num = length * itemSize;
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(num + 2);
			}
			this.AddScalar((void*)(&length), 2);
			checked
			{
				if (length != 0)
				{
					if (this.bufferNesting == 0)
					{
						this.ScalarsEnd();
						this.PinArray(value, num);
						return;
					}
					int num2 = this.bufferPos;
					this.bufferPos += num;
					this.EnsureBuffer();
					Buffer.BlockCopy(value, 0, this.buffer, num2, num);
				}
			}
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x001422EB File Offset: 0x001404EB
		internal int BeginBufferedArray()
		{
			this.BeginBuffered();
			this.bufferPos += 2;
			return this.bufferPos;
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x00142307 File Offset: 0x00140507
		internal void EndBufferedArray(int bookmark, int count)
		{
			this.EnsureBuffer();
			this.buffer[bookmark - 2] = (byte)count;
			this.buffer[bookmark - 1] = (byte)(count >> 8);
			this.EndBuffered();
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x0014232F File Offset: 0x0014052F
		internal void BeginBuffered()
		{
			this.ScalarsEnd();
			this.bufferNesting++;
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x00142345 File Offset: 0x00140545
		internal void EndBuffered()
		{
			this.bufferNesting--;
			if (this.bufferNesting == 0)
			{
				this.EnsureBuffer();
				this.PinArray(this.buffer, this.bufferPos);
				this.buffer = null;
				this.bufferPos = 0;
			}
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x00142384 File Offset: 0x00140584
		private void EnsureBuffer()
		{
			int num = this.bufferPos;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x001423B4 File Offset: 0x001405B4
		private void EnsureBuffer(int additionalSize)
		{
			int num = this.bufferPos + additionalSize;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x060062D5 RID: 25301 RVA: 0x001423E4 File Offset: 0x001405E4
		private void GrowBuffer(int required)
		{
			int num = ((this.buffer == null) ? 64 : this.buffer.Length);
			do
			{
				num *= 2;
			}
			while (num < required);
			Array.Resize<byte>(ref this.buffer, num);
		}

		// Token: 0x060062D6 RID: 25302 RVA: 0x0014241C File Offset: 0x0014061C
		private unsafe void PinArray(object value, int size)
		{
			GCHandle* ptr = this.pins;
			if (this.pinsEnd == ptr)
			{
				throw new IndexOutOfRangeException(Environment.GetResourceString("Pins are out of range."));
			}
			EventSource.EventData* ptr2 = this.datas;
			if (this.datasEnd == ptr2)
			{
				throw new IndexOutOfRangeException(Environment.GetResourceString("Data descriptors are out of range."));
			}
			this.pins = ptr + 1;
			this.datas = ptr2 + 1;
			*ptr = GCHandle.Alloc(value, GCHandleType.Pinned);
			ptr2->m_Ptr = (long)(ulong)((UIntPtr)((void*)ptr->AddrOfPinnedObject()));
			ptr2->m_Size = size;
		}

		// Token: 0x060062D7 RID: 25303 RVA: 0x001424B4 File Offset: 0x001406B4
		private unsafe void ScalarsBegin()
		{
			if (!this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				if (this.datasEnd == ptr)
				{
					throw new IndexOutOfRangeException(Environment.GetResourceString("Data descriptors are out of range."));
				}
				ptr->m_Ptr = (long)(ulong)((UIntPtr)((void*)this.scratch));
				this.writingScalars = true;
			}
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x00142508 File Offset: 0x00140708
		private unsafe void ScalarsEnd()
		{
			if (this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				ptr->m_Size = (this.scratch - checked((UIntPtr)ptr->m_Ptr)) / 1;
				this.datas = ptr + 1;
				this.writingScalars = false;
			}
		}

		// Token: 0x04003132 RID: 12594
		[ThreadStatic]
		internal static DataCollector ThreadInstance;

		// Token: 0x04003133 RID: 12595
		private unsafe byte* scratchEnd;

		// Token: 0x04003134 RID: 12596
		private unsafe EventSource.EventData* datasEnd;

		// Token: 0x04003135 RID: 12597
		private unsafe GCHandle* pinsEnd;

		// Token: 0x04003136 RID: 12598
		private unsafe EventSource.EventData* datasStart;

		// Token: 0x04003137 RID: 12599
		private unsafe byte* scratch;

		// Token: 0x04003138 RID: 12600
		private unsafe EventSource.EventData* datas;

		// Token: 0x04003139 RID: 12601
		private unsafe GCHandle* pins;

		// Token: 0x0400313A RID: 12602
		private byte[] buffer;

		// Token: 0x0400313B RID: 12603
		private int bufferPos;

		// Token: 0x0400313C RID: 12604
		private int bufferNesting;

		// Token: 0x0400313D RID: 12605
		private bool writingScalars;
	}
}
