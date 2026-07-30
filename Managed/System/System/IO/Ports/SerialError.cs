using System;

namespace System.IO.Ports
{
	/// <summary>Specifies errors that occur on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x020003F3 RID: 1011
	public enum SerialError
	{
		/// <summary>An input buffer overflow has occurred. There is either no room in the input buffer, or a character was received after the end-of-file (EOF) character.</summary>
		// Token: 0x04001AFB RID: 6907
		RXOver = 1,
		/// <summary>A character-buffer overrun has occurred. The next character is lost.</summary>
		// Token: 0x04001AFC RID: 6908
		Overrun,
		/// <summary>The hardware detected a parity error.</summary>
		// Token: 0x04001AFD RID: 6909
		RXParity = 4,
		/// <summary>The hardware detected a framing error.</summary>
		// Token: 0x04001AFE RID: 6910
		Frame = 8,
		/// <summary>The application tried to transmit a character, but the output buffer was full.</summary>
		// Token: 0x04001AFF RID: 6911
		TXFull = 256
	}
}
