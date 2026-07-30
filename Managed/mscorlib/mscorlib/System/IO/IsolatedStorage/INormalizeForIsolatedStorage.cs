using System;
using System.Runtime.InteropServices;

namespace System.IO.IsolatedStorage
{
	/// <summary>Enables comparisons between an isolated store and an application domain and assembly's evidence.</summary>
	// Token: 0x020003E8 RID: 1000
	[ComVisible(true)]
	public interface INormalizeForIsolatedStorage
	{
		/// <summary>When overridden in a derived class, returns a normalized copy of the object on which it is called.</summary>
		/// <returns>A normalized object that represents the instance on which this method was called. This instance can be a string, stream, or any serializable object.</returns>
		// Token: 0x06002F01 RID: 12033
		object Normalize();
	}
}
