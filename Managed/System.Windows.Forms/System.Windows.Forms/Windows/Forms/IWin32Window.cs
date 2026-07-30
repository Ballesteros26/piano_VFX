using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides an interface to expose Win32 HWND handles.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D3 RID: 467
	[InterfaceType(1)]
	[Guid("458AB8A2-A1EA-4d7b-8EBE-DEE5D3D9442C")]
	[ComVisible(true)]
	public interface IWin32Window
	{
		/// <summary>Gets the handle to the window represented by the implementer.</summary>
		/// <returns>A handle to the window represented by the implementer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001E06 RID: 7686
		IntPtr Handle { get; }
	}
}
