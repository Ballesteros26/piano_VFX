using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the IPersistFile interface, with functionality from IPersist.</summary>
	// Token: 0x02000981 RID: 2433
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IPersistFile
	{
		/// <summary>Retrieves the class identifier (CLSID) of an object.</summary>
		/// <param name="pClassID">When this method returns, contains a reference to the CLSID. This parameter is passed uninitialized.</param>
		// Token: 0x060059E1 RID: 23009
		void GetClassID(out Guid pClassID);

		/// <summary>Checks an object for changes since it was last saved to its current file.</summary>
		/// <returns>S_OK if the file has changed since it was last saved; S_FALSE if the file has not changed since it was last saved.</returns>
		// Token: 0x060059E2 RID: 23010
		[PreserveSig]
		int IsDirty();

		/// <summary>Opens the specified file and initializes an object from the file contents.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to open. </param>
		/// <param name="dwMode">A combination of values from the STGM enumeration to indicate the access mode in which to open <paramref name="pszFileName" />. </param>
		// Token: 0x060059E3 RID: 23011
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		/// <summary>Saves a copy of the object into the specified file.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to which the object is saved. </param>
		/// <param name="fRemember">true to used the <paramref name="pszFileName" /> parameter as the current working file; otherwise false. </param>
		// Token: 0x060059E4 RID: 23012
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		/// <summary>Notifies the object that it can write to its file.</summary>
		/// <param name="pszFileName">The absolute path of the file where the object was previously saved. </param>
		// Token: 0x060059E5 RID: 23013
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		/// <summary>Retrieves either the absolute path to the current working file of the object or, if there is no current working file, the default file name prompt of the object.</summary>
		/// <param name="ppszFileName">When this method returns, contains the address of a pointer to a zero-terminated string containing the path for the current file, or the default file name prompt (such as *.txt). This parameter is passed uninitialized.</param>
		// Token: 0x060059E6 RID: 23014
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
