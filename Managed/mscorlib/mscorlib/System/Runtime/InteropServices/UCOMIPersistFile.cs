using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IPersistFile" /> instead.</summary>
	// Token: 0x02000933 RID: 2355
	[Guid("0000010b-0000-0000-c000-000000000046")]
	[Obsolete]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIPersistFile
	{
		/// <summary>Retrieves the class identifier (CLSID) of an object.</summary>
		/// <param name="pClassID">On successful return, a reference to the CLSID. </param>
		// Token: 0x0600572A RID: 22314
		void GetClassID(out Guid pClassID);

		/// <summary>Checks an object for changes since it was last saved to its current file.</summary>
		/// <returns>S_OK if the file has changed since it was last saved; S_FALSE if the file has not changed since it was last saved.</returns>
		// Token: 0x0600572B RID: 22315
		[PreserveSig]
		int IsDirty();

		/// <summary>Opens the specified file and initializes an object from the file contents.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to open. </param>
		/// <param name="dwMode">A combination of values from the STGM enumeration to indicate the access mode in which to open <paramref name="pszFileName" />. </param>
		// Token: 0x0600572C RID: 22316
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		/// <summary>Saves a copy of the object into the specified file.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to which the object is saved. </param>
		/// <param name="fRemember">Indicates whether <paramref name="pszFileName" /> is to be used as the current working file. </param>
		// Token: 0x0600572D RID: 22317
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		/// <summary>Notifies the object that it can write to its file.</summary>
		/// <param name="pszFileName">The absolute path of the file where the object was previously saved. </param>
		// Token: 0x0600572E RID: 22318
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		/// <summary>Retrieves either the absolute path to current working file of the object, or if there is no current working file, the default filename prompt of the object.</summary>
		/// <param name="ppszFileName">The address of a pointer to a zero-terminated string containing the path for the current file, or the default filename prompt (such as *.txt). </param>
		// Token: 0x0600572F RID: 22319
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
