using System;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AE4 RID: 2788
	[SecuritySafeCritical]
	internal class TraceLoggingDataCollector
	{
		// Token: 0x0600640C RID: 25612 RVA: 0x00002111 File Offset: 0x00000311
		private TraceLoggingDataCollector()
		{
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x001448F8 File Offset: 0x00142AF8
		public int BeginBufferedArray()
		{
			return DataCollector.ThreadInstance.BeginBufferedArray();
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x00144904 File Offset: 0x00142B04
		public void EndBufferedArray(int bookmark, int count)
		{
			DataCollector.ThreadInstance.EndBufferedArray(bookmark, count);
		}

		// Token: 0x0600640F RID: 25615 RVA: 0x00002119 File Offset: 0x00000319
		public TraceLoggingDataCollector AddGroup()
		{
			return this;
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x00144912 File Offset: 0x00142B12
		public unsafe void AddScalar(bool value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x00144912 File Offset: 0x00142B12
		public unsafe void AddScalar(sbyte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x00144912 File Offset: 0x00142B12
		public unsafe void AddScalar(byte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06006413 RID: 25619 RVA: 0x00144922 File Offset: 0x00142B22
		public unsafe void AddScalar(short value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x00144922 File Offset: 0x00142B22
		public unsafe void AddScalar(ushort value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x06006415 RID: 25621 RVA: 0x00144932 File Offset: 0x00142B32
		public unsafe void AddScalar(int value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x00144932 File Offset: 0x00142B32
		public unsafe void AddScalar(uint value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x00144942 File Offset: 0x00142B42
		public unsafe void AddScalar(long value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x00144942 File Offset: 0x00142B42
		public unsafe void AddScalar(ulong value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x00144952 File Offset: 0x00142B52
		public unsafe void AddScalar(IntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), IntPtr.Size);
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x00144966 File Offset: 0x00142B66
		public unsafe void AddScalar(UIntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), UIntPtr.Size);
		}

		// Token: 0x0600641B RID: 25627 RVA: 0x00144932 File Offset: 0x00142B32
		public unsafe void AddScalar(float value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x0600641C RID: 25628 RVA: 0x00144942 File Offset: 0x00142B42
		public unsafe void AddScalar(double value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x0600641D RID: 25629 RVA: 0x00144922 File Offset: 0x00142B22
		public unsafe void AddScalar(char value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x0014497A File Offset: 0x00142B7A
		public unsafe void AddScalar(Guid value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 16);
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x0014498B File Offset: 0x00142B8B
		public void AddBinary(string value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : (value.Length * 2));
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x001449A6 File Offset: 0x00142BA6
		public void AddBinary(byte[] value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : value.Length);
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x001449BC File Offset: 0x00142BBC
		public void AddArray(bool[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x001449BC File Offset: 0x00142BBC
		public void AddArray(sbyte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x06006423 RID: 25635 RVA: 0x001449D3 File Offset: 0x00142BD3
		public void AddArray(short[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x001449D3 File Offset: 0x00142BD3
		public void AddArray(ushort[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x001449EA File Offset: 0x00142BEA
		public void AddArray(int[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x001449EA File Offset: 0x00142BEA
		public void AddArray(uint[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x06006427 RID: 25639 RVA: 0x00144A01 File Offset: 0x00142C01
		public void AddArray(long[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x06006428 RID: 25640 RVA: 0x00144A01 File Offset: 0x00142C01
		public void AddArray(ulong[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x00144A18 File Offset: 0x00142C18
		public void AddArray(IntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, IntPtr.Size);
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x00144A33 File Offset: 0x00142C33
		public void AddArray(UIntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, UIntPtr.Size);
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x001449EA File Offset: 0x00142BEA
		public void AddArray(float[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x00144A01 File Offset: 0x00142C01
		public void AddArray(double[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x0600642D RID: 25645 RVA: 0x001449D3 File Offset: 0x00142BD3
		public void AddArray(char[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x00144A4E File Offset: 0x00142C4E
		public void AddArray(Guid[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 16);
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x001449BC File Offset: 0x00142BBC
		public void AddCustom(byte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x04003198 RID: 12696
		internal static readonly TraceLoggingDataCollector Instance = new TraceLoggingDataCollector();
	}
}
