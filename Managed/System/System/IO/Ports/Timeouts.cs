using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x02000401 RID: 1025
	[StructLayout(LayoutKind.Sequential)]
	internal class Timeouts
	{
		// Token: 0x06001F4A RID: 8010 RVA: 0x0007ACB2 File Offset: 0x00078EB2
		public Timeouts(int read_timeout, int write_timeout)
		{
			this.SetValues(read_timeout, write_timeout);
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x0007ACC2 File Offset: 0x00078EC2
		public void SetValues(int read_timeout, int write_timeout)
		{
			this.ReadIntervalTimeout = uint.MaxValue;
			this.ReadTotalTimeoutMultiplier = uint.MaxValue;
			this.ReadTotalTimeoutConstant = (uint)((read_timeout == -1) ? (-2) : read_timeout);
			this.WriteTotalTimeoutMultiplier = 0U;
			this.WriteTotalTimeoutConstant = (uint)((write_timeout == -1) ? (-1) : write_timeout);
		}

		// Token: 0x04001B68 RID: 7016
		public uint ReadIntervalTimeout;

		// Token: 0x04001B69 RID: 7017
		public uint ReadTotalTimeoutMultiplier;

		// Token: 0x04001B6A RID: 7018
		public uint ReadTotalTimeoutConstant;

		// Token: 0x04001B6B RID: 7019
		public uint WriteTotalTimeoutMultiplier;

		// Token: 0x04001B6C RID: 7020
		public uint WriteTotalTimeoutConstant;

		// Token: 0x04001B6D RID: 7021
		public const uint MaxDWord = 4294967295U;
	}
}
