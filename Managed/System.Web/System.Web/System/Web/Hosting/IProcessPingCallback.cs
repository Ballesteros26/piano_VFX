using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides functionality to respond to a ping request.</summary>
	// Token: 0x0200053F RID: 1343
	[Guid("f11dc4c9-ddd1-4566-ad53-cf6f3a28fefe")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IProcessPingCallback
	{
		/// <summary>Provides a callback routine that responds to a ping request.</summary>
		// Token: 0x06003A7E RID: 14974
		void Respond();
	}
}
