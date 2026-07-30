using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides return values for the <see cref="M:System.Runtime.InteropServices.ICustomQueryInterface.GetInterface(System.Guid@,System.IntPtr@)" /> method.</summary>
	// Token: 0x020008E3 RID: 2275
	[ComVisible(false)]
	[Serializable]
	public enum CustomQueryInterfaceResult
	{
		/// <summary>The interface pointer that is returned from the <see cref="M:System.Runtime.InteropServices.ICustomQueryInterface.GetInterface(System.Guid@,System.IntPtr@)" /> method can be used as the result of IUnknown::QueryInterface.</summary>
		// Token: 0x04002CD2 RID: 11474
		Handled,
		/// <summary>The custom QueryInterface was not used. Instead, the default implementation of IUnknown::QueryInterface should be used.</summary>
		// Token: 0x04002CD3 RID: 11475
		NotHandled,
		/// <summary>The interface for a specific interface ID is not available. In this case, the returned interface is null. E_NOINTERFACE is returned to the caller of IUnknown::QueryInterface.</summary>
		// Token: 0x04002CD4 RID: 11476
		Failed
	}
}
