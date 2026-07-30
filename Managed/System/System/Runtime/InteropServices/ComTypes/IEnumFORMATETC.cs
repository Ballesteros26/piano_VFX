using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the IEnumFORMATETC interface.</summary>
	// Token: 0x02000367 RID: 871
	[Guid("00000103-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumFORMATETC
	{
		/// <summary>Creates a new enumerator that contains the same enumeration state as the current enumerator.</summary>
		/// <param name="newEnum">When this method returns, contains a reference to the newly created enumerator. This parameter is passed uninitialized.</param>
		// Token: 0x06001AD2 RID: 6866
		void Clone(out IEnumFORMATETC newEnum);

		/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> references to return in <paramref name="rgelt" />.</param>
		/// <param name="rgelt">When this method returns, contains a reference to the enumerated <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> references. This parameter is passed uninitialized.</param>
		/// <param name="pceltFetched">When this method returns, contains a reference to the actual number of references enumerated in <paramref name="rgelt" />. This parameter is passed uninitialized.</param>
		// Token: 0x06001AD3 RID: 6867
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray)] [Out] FORMATETC[] rgelt, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pceltFetched);

		/// <summary>Resets the enumeration sequence to the beginning.</summary>
		/// <returns>An HRESULT with the value S_OK.</returns>
		// Token: 0x06001AD4 RID: 6868
		[PreserveSig]
		int Reset();

		/// <summary>Skips a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the number of elements skipped equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of elements to skip in the enumeration.</param>
		// Token: 0x06001AD5 RID: 6869
		[PreserveSig]
		int Skip(int celt);
	}
}
