using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the type of character that was received on the serial port of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x020003F2 RID: 1010
	public enum SerialData
	{
		/// <summary>A character was received and placed in the input buffer.</summary>
		// Token: 0x04001AF8 RID: 6904
		Chars = 1,
		/// <summary>The end of file character was received and placed in the input buffer.</summary>
		// Token: 0x04001AF9 RID: 6905
		Eof
	}
}
