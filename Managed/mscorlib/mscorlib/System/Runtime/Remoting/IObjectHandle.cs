using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Defines the interface for unwrapping marshal-by-value objects from indirection.</summary>
	// Token: 0x0200074D RID: 1869
	[ComVisible(true)]
	[Guid("C460E2B4-E199-412a-8456-84DC3E4838C3")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IObjectHandle
	{
		/// <summary>Unwraps the object.</summary>
		/// <returns>The unwrapped object.</returns>
		// Token: 0x06004D3B RID: 19771
		object Unwrap();
	}
}
