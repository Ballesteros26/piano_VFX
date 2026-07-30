using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumVARIANT" /> instead.</summary>
	// Token: 0x02000931 RID: 2353
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Obsolete]
	[Guid("00020404-0000-0000-c000-000000000046")]
	[ComImport]
	public interface UCOMIEnumVARIANT
	{
		/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of elements to return in <paramref name="rgelt" />. </param>
		/// <param name="rgvar">On successful return, a reference to the enumerated elements. </param>
		/// <param name="pceltFetched">On successful return, a reference to the actual number of elements enumerated in <paramref name="rgelt" />. </param>
		// Token: 0x06005712 RID: 22290
		[PreserveSig]
		int Next(int celt, int rgvar, int pceltFetched);

		/// <summary>Skips over a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the number of elements skipped equals <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of elements to skip in the enumeration. </param>
		// Token: 0x06005713 RID: 22291
		[PreserveSig]
		int Skip(int celt);

		/// <summary>Resets the enumeration sequence to the beginning.</summary>
		/// <returns>An HRESULT with the value S_OK.</returns>
		// Token: 0x06005714 RID: 22292
		[PreserveSig]
		int Reset();

		/// <summary>Creates another enumerator that contains the same enumeration state as the current one.</summary>
		/// <param name="ppenum">On successful return, a reference to the newly created enumerator. </param>
		// Token: 0x06005715 RID: 22293
		void Clone(int ppenum);
	}
}
