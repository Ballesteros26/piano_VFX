using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.ProviderBase
{
	// Token: 0x020002FF RID: 767
	internal abstract class DbBuffer : SafeHandle
	{
		// Token: 0x06002204 RID: 8708 RVA: 0x0009F57C File Offset: 0x0009D77C
		protected DbBuffer(int initialSize)
			: base(IntPtr.Zero, true)
		{
			if (0 < initialSize)
			{
				this._bufferLength = initialSize;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.handle = SafeNativeMethods.LocalAlloc((IntPtr)initialSize);
				}
				if (IntPtr.Zero == this.handle)
				{
					throw new OutOfMemoryException();
				}
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0000EB26 File Offset: 0x0000CD26
		protected DbBuffer(IntPtr invalidHandleValue, bool ownsHandle)
			: base(invalidHandleValue, ownsHandle)
		{
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x000061D5 File Offset: 0x000043D5
		private int BaseOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x0008DE8C File Offset: 0x0008C08C
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x0009F5E4 File Offset: 0x0009D7E4
		internal int Length
		{
			get
			{
				return this._bufferLength;
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0009F5EC File Offset: 0x0009D7EC
		internal string PtrToStringUni(int offset)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2);
			string text = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				text = Marshal.PtrToStringUni(ADP.IntPtrOffset(base.DangerousGetHandle(), offset));
				this.Validate(offset, 2 * (text.Length + 1));
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return text;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0009F65C File Offset: 0x0009D85C
		internal string PtrToStringUni(int offset, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			string text = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				text = Marshal.PtrToStringUni(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return text;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0009F6C0 File Offset: 0x0009D8C0
		internal byte ReadByte(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte b;
			try
			{
				base.DangerousAddRef(ref flag);
				b = Marshal.ReadByte(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return b;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0009F710 File Offset: 0x0009D910
		internal byte[] ReadBytes(int offset, int length)
		{
			byte[] array = new byte[length];
			return this.ReadBytes(offset, array, 0, length);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0009F730 File Offset: 0x0009D930
		internal byte[] ReadBytes(int offset, byte[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x0009F794 File Offset: 0x0009D994
		internal char ReadChar(int offset)
		{
			return (char)this.ReadInt16(offset);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0009F7A0 File Offset: 0x0009D9A0
		internal char[] ReadChars(int offset, char[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0009F804 File Offset: 0x0009DA04
		internal double ReadDouble(int offset)
		{
			return BitConverter.Int64BitsToDouble(this.ReadInt64(offset));
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0009F814 File Offset: 0x0009DA14
		internal short ReadInt16(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			short num;
			try
			{
				base.DangerousAddRef(ref flag);
				num = Marshal.ReadInt16(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0009F864 File Offset: 0x0009DA64
		internal void ReadInt16Array(int offset, short[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0009F8C8 File Offset: 0x0009DAC8
		internal int ReadInt32(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			int num;
			try
			{
				base.DangerousAddRef(ref flag);
				num = Marshal.ReadInt32(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0009F918 File Offset: 0x0009DB18
		internal void ReadInt32Array(int offset, int[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0009F97C File Offset: 0x0009DB7C
		internal long ReadInt64(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			long num;
			try
			{
				base.DangerousAddRef(ref flag);
				num = Marshal.ReadInt64(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0009F9CC File Offset: 0x0009DBCC
		internal IntPtr ReadIntPtr(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IntPtr intPtr;
			try
			{
				base.DangerousAddRef(ref flag);
				intPtr = Marshal.ReadIntPtr(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return intPtr;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0009FA1C File Offset: 0x0009DC1C
		internal unsafe float ReadSingle(int offset)
		{
			int num = this.ReadInt32(offset);
			return *(float*)(&num);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0009FA38 File Offset: 0x0009DC38
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SafeNativeMethods.LocalFree(handle);
			}
			return true;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0009FA6C File Offset: 0x0009DC6C
		private void StructureToPtr(int offset, object structure)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.StructureToPtr(structure, intPtr, false);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0009FAC4 File Offset: 0x0009DCC4
		internal void WriteByte(int offset, byte value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteByte(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0009FB14 File Offset: 0x0009DD14
		internal void WriteBytes(int offset, byte[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0009FB78 File Offset: 0x0009DD78
		internal void WriteCharArray(int offset, char[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x0009FBDC File Offset: 0x0009DDDC
		internal void WriteDouble(int offset, double value)
		{
			this.WriteInt64(offset, BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0009FBEC File Offset: 0x0009DDEC
		internal void WriteInt16(int offset, short value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt16(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0009FC3C File Offset: 0x0009DE3C
		internal void WriteInt16Array(int offset, short[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0009FCA0 File Offset: 0x0009DEA0
		internal void WriteInt32(int offset, int value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt32(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0009FCF0 File Offset: 0x0009DEF0
		internal void WriteInt32Array(int offset, int[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0009FD54 File Offset: 0x0009DF54
		internal void WriteInt64(int offset, long value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt64(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0009FDA4 File Offset: 0x0009DFA4
		internal void WriteIntPtr(int offset, IntPtr value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteIntPtr(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0009FDF4 File Offset: 0x0009DFF4
		internal unsafe void WriteSingle(int offset, float value)
		{
			this.WriteInt32(offset, *(int*)(&value));
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0009FE04 File Offset: 0x0009E004
		internal void ZeroMemory()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				SafeNativeMethods.ZeroMemory(base.DangerousGetHandle(), this.Length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0009FE50 File Offset: 0x0009E050
		internal Guid ReadGuid(int offset)
		{
			byte[] array = new byte[16];
			this.ReadBytes(offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0009FE77 File Offset: 0x0009E077
		internal void WriteGuid(int offset, Guid value)
		{
			this.StructureToPtr(offset, value);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0009FE88 File Offset: 0x0009E088
		internal DateTime ReadDate(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0009FEB8 File Offset: 0x0009E0B8
		internal void WriteDate(int offset, DateTime value)
		{
			short[] array = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day
			};
			this.WriteInt16Array(offset, array, 0, 3);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0009FEF8 File Offset: 0x0009E0F8
		internal TimeSpan ReadTime(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new TimeSpan((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0009FF28 File Offset: 0x0009E128
		internal void WriteTime(int offset, TimeSpan value)
		{
			short[] array = new short[]
			{
				(short)value.Hours,
				(short)value.Minutes,
				(short)value.Seconds
			};
			this.WriteInt16Array(offset, array, 0, 3);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0009FF68 File Offset: 0x0009E168
		internal DateTime ReadDateTime(int offset)
		{
			short[] array = new short[6];
			this.ReadInt16Array(offset, array, 0, 6);
			int num = this.ReadInt32(offset + 12);
			DateTime dateTime = new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]), (int)((ushort)array[3]), (int)((ushort)array[4]), (int)((ushort)array[5]));
			return dateTime.AddTicks((long)(num / 100));
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0009FFBC File Offset: 0x0009E1BC
		internal void WriteDateTime(int offset, DateTime value)
		{
			int num = (int)(value.Ticks % 10000000L) * 100;
			short[] array = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day,
				(short)value.Hour,
				(short)value.Minute,
				(short)value.Second
			};
			this.WriteInt16Array(offset, array, 0, 6);
			this.WriteInt32(offset + 12, num);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000A003C File Offset: 0x0009E23C
		internal decimal ReadNumeric(int offset)
		{
			byte[] array = new byte[20];
			this.ReadBytes(offset, array, 1, 19);
			int[] array2 = new int[]
			{
				0,
				0,
				0,
				(int)array[2] << 16
			};
			if (array[3] == 0)
			{
				array2[3] |= int.MinValue;
			}
			array2[0] = BitConverter.ToInt32(array, 4);
			array2[1] = BitConverter.ToInt32(array, 8);
			array2[2] = BitConverter.ToInt32(array, 12);
			if (BitConverter.ToInt32(array, 16) != 0)
			{
				throw ADP.NumericToDecimalOverflow();
			}
			return new decimal(array2);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000A00B8 File Offset: 0x0009E2B8
		internal void WriteNumeric(int offset, decimal value, byte precision)
		{
			int[] bits = decimal.GetBits(value);
			byte[] array = new byte[20];
			array[1] = precision;
			Buffer.BlockCopy(bits, 14, array, 2, 2);
			array[3] = ((array[3] == 0) ? 1 : 0);
			Buffer.BlockCopy(bits, 0, array, 4, 12);
			array[16] = 0;
			array[17] = 0;
			array[18] = 0;
			array[19] = 0;
			this.WriteBytes(offset, array, 1, 19);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000A0118 File Offset: 0x0009E318
		[Conditional("DEBUG")]
		protected void ValidateCheck(int offset, int count)
		{
			this.Validate(offset, count);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000A0122 File Offset: 0x0009E322
		protected void Validate(int offset, int count)
		{
			if (offset < 0 || count < 0 || this.Length < checked(offset + count))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
			}
		}

		// Token: 0x040016CC RID: 5836
		private readonly int _bufferLength;
	}
}
