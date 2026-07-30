using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates which IDispatch implementation to use for a particular class.</summary>
	// Token: 0x020008B3 RID: 2227
	[ComVisible(true)]
	[Obsolete("The IDispatchImplAttribute is deprecated.", false)]
	[Serializable]
	public enum IDispatchImplType
	{
		/// <summary>Specifies that the common language runtime decides which IDispatch implementation to use.</summary>
		// Token: 0x04002C08 RID: 11272
		SystemDefinedImpl,
		/// <summary>Specifies that the IDispatch implemenation is supplied by the runtime.</summary>
		// Token: 0x04002C09 RID: 11273
		InternalImpl,
		/// <summary>Specifies that the IDispatch implementation is supplied by passing the type information for the object to the COM CreateStdDispatch API method.</summary>
		// Token: 0x04002C0A RID: 11274
		CompatibleImpl
	}
}
