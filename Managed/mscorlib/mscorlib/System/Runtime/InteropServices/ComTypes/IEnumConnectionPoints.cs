using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Manages the definition of the IEnumConnectionPoints interface.</summary>
	// Token: 0x02000976 RID: 2422
	[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumConnectionPoints
	{
		/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of IConnectionPoint references to return in <paramref name="rgelt" />. </param>
		/// <param name="rgelt">When this method returns, contains a reference to the enumerated connections. This parameter is passed uninitialized.</param>
		/// <param name="pceltFetched">When this method returns, contains a reference to the actual number of connections enumerated in <paramref name="rgelt" />. </param>
		// Token: 0x060059B1 RID: 22961
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IConnectionPoint[] rgelt, IntPtr pceltFetched);

		/// <summary>Skips a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the number of elements skipped equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of elements to skip in the enumeration. </param>
		// Token: 0x060059B2 RID: 22962
		[PreserveSig]
		int Skip(int celt);

		/// <summary>Resets the enumeration sequence to the beginning.</summary>
		// Token: 0x060059B3 RID: 22963
		void Reset();

		/// <summary>Creates a new enumerator that contains the same enumeration state as the current one.</summary>
		/// <param name="ppenum">When this method returns, contains a reference to the newly created enumerator. This parameter is passed uninitialized.</param>
		// Token: 0x060059B4 RID: 22964
		void Clone(out IEnumConnectionPoints ppenum);
	}
}
