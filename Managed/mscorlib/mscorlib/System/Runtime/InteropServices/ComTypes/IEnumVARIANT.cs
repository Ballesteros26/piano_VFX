using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Manages the definition of the IEnumVARIANT interface.</summary>
	// Token: 0x0200097D RID: 2429
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00020404-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IEnumVARIANT
	{
		/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of elements to return in <paramref name="rgelt" />. </param>
		/// <param name="rgVar">When this method returns, contains a reference to the enumerated elements. This parameter is passed uninitialized.</param>
		/// <param name="pceltFetched">When this method returns, contains a reference to the actual number of elements enumerated in <paramref name="rgelt" />. </param>
		// Token: 0x060059C5 RID: 22981
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] object[] rgVar, IntPtr pceltFetched);

		/// <summary>Skips a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the number of elements skipped equals <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of elements to skip in the enumeration. </param>
		// Token: 0x060059C6 RID: 22982
		[PreserveSig]
		int Skip(int celt);

		/// <summary>Resets the enumeration sequence to the beginning.</summary>
		/// <returns>An HRESULT with the value S_OK.</returns>
		// Token: 0x060059C7 RID: 22983
		[PreserveSig]
		int Reset();

		/// <summary>Creates a new enumerator that contains the same enumeration state as the current one.</summary>
		/// <returns>An <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumVARIANT" />  reference to the newly created enumerator.</returns>
		// Token: 0x060059C8 RID: 22984
		IEnumVARIANT Clone();
	}
}
