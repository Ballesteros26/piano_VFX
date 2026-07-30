using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides custom wrappers for handling method calls.</summary>
	// Token: 0x020008E2 RID: 2274
	[ComVisible(true)]
	public interface ICustomMarshaler
	{
		/// <summary>Converts the unmanaged data to managed data.</summary>
		/// <returns>An object that represents the managed view of the COM data.</returns>
		/// <param name="pNativeData">A pointer to the unmanaged data to be wrapped. </param>
		// Token: 0x06005570 RID: 21872
		object MarshalNativeToManaged(IntPtr pNativeData);

		/// <summary>Converts the managed data to unmanaged data.</summary>
		/// <returns>A pointer to the COM view of the managed object.</returns>
		/// <param name="ManagedObj">The managed object to be converted. </param>
		// Token: 0x06005571 RID: 21873
		IntPtr MarshalManagedToNative(object ManagedObj);

		/// <summary>Performs necessary cleanup of the unmanaged data when it is no longer needed.</summary>
		/// <param name="pNativeData">A pointer to the unmanaged data to be destroyed. </param>
		// Token: 0x06005572 RID: 21874
		void CleanUpNativeData(IntPtr pNativeData);

		/// <summary>Performs necessary cleanup of the managed data when it is no longer needed.</summary>
		/// <param name="ManagedObj">The managed object to be destroyed. </param>
		// Token: 0x06005573 RID: 21875
		void CleanUpManagedData(object ManagedObj);

		/// <summary>Returns the size of the native data to be marshaled.</summary>
		/// <returns>The size, in bytes, of the native data.</returns>
		// Token: 0x06005574 RID: 21876
		int GetNativeDataSize();
	}
}
