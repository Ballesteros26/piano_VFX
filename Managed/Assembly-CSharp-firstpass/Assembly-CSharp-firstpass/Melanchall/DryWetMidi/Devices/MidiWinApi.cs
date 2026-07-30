using System;
using System.Runtime.InteropServices;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000111 RID: 273
	internal static class MidiWinApi
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x0001C860 File Offset: 0x0001AA60
		public static byte[] UnpackSysExBytes(IntPtr headerPointer)
		{
			MidiWinApi.MIDIHDR midihdr = (MidiWinApi.MIDIHDR)Marshal.PtrToStructure(headerPointer, typeof(MidiWinApi.MIDIHDR));
			byte[] array = new byte[midihdr.dwBytesRecorded - 1];
			Marshal.Copy(IntPtr.Add(midihdr.lpData, 1), array, 0, array.Length);
			return array;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001C8A6 File Offset: 0x0001AAA6
		public static void UnpackShortEventBytes(int message, out byte statusByte, out byte firstDataByte, out byte secondDataByte)
		{
			statusByte = message.GetFourthByte();
			firstDataByte = message.GetThirdByte();
			secondDataByte = message.GetSecondByte();
		}

		// Token: 0x0400082D RID: 2093
		public const uint MaxErrorLength = 256U;

		// Token: 0x0400082E RID: 2094
		public const uint CallbackFunction = 196608U;

		// Token: 0x0400082F RID: 2095
		public static readonly int MidiHeaderSize = Marshal.SizeOf(typeof(MidiWinApi.MIDIHDR));

		// Token: 0x04000830 RID: 2096
		public const uint MMSYSERR_NOERROR = 0U;

		// Token: 0x04000831 RID: 2097
		public const uint MMSYSERR_ERROR = 1U;

		// Token: 0x04000832 RID: 2098
		public const uint MMSYSERR_INVALHANDLE = 5U;

		// Token: 0x04000833 RID: 2099
		public const uint MIDIERR_NOTREADY = 67U;

		// Token: 0x04000834 RID: 2100
		public const uint TIMERR_NOCANDO = 97U;

		// Token: 0x02000281 RID: 641
		internal struct MIDIHDR
		{
			// Token: 0x04000D85 RID: 3461
			public IntPtr lpData;

			// Token: 0x04000D86 RID: 3462
			public int dwBufferLength;

			// Token: 0x04000D87 RID: 3463
			public int dwBytesRecorded;

			// Token: 0x04000D88 RID: 3464
			public IntPtr dwUser;

			// Token: 0x04000D89 RID: 3465
			public int dwFlags;

			// Token: 0x04000D8A RID: 3466
			public IntPtr lpNext;

			// Token: 0x04000D8B RID: 3467
			public IntPtr reserved;

			// Token: 0x04000D8C RID: 3468
			public int dwOffset;

			// Token: 0x04000D8D RID: 3469
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public int[] dwReserved;
		}

		// Token: 0x02000282 RID: 642
		// (Invoke) Token: 0x06000EA9 RID: 3753
		public delegate void MidiMessageCallback(IntPtr hMidi, MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);
	}
}
