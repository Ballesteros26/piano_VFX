using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the parity bit for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x020003F1 RID: 1009
	public enum Parity
	{
		/// <summary>No parity check occurs.</summary>
		// Token: 0x04001AF2 RID: 6898
		None,
		/// <summary>Sets the parity bit so that the count of bits set is an odd number.</summary>
		// Token: 0x04001AF3 RID: 6899
		Odd,
		/// <summary>Sets the parity bit so that the count of bits set is an even number.</summary>
		// Token: 0x04001AF4 RID: 6900
		Even,
		/// <summary>Leaves the parity bit set to 1.</summary>
		// Token: 0x04001AF5 RID: 6901
		Mark,
		/// <summary>Leaves the parity bit set to 0.</summary>
		// Token: 0x04001AF6 RID: 6902
		Space
	}
}
