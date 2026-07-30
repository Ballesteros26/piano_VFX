using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a way for clients to access the actual object, rather than the adapter object handed out by a custom marshaler.</summary>
	// Token: 0x020008E0 RID: 2272
	[ComVisible(true)]
	public interface ICustomAdapter
	{
		/// <summary>Provides access to the underlying object wrapped by a custom marshaler.</summary>
		/// <returns>The object contained by the adapter object.</returns>
		// Token: 0x0600556E RID: 21870
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetUnderlyingObject();
	}
}
