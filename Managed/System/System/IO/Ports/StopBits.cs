using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the number of stop bits used on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x020003FE RID: 1022
	public enum StopBits
	{
		/// <summary>No stop bits are used. This value is not supported by the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> property. </summary>
		// Token: 0x04001B30 RID: 6960
		None,
		/// <summary>One stop bit is used.</summary>
		// Token: 0x04001B31 RID: 6961
		One,
		/// <summary>Two stop bits are used.</summary>
		// Token: 0x04001B32 RID: 6962
		Two,
		/// <summary>1.5 stop bits are used.</summary>
		// Token: 0x04001B33 RID: 6963
		OnePointFive
	}
}
