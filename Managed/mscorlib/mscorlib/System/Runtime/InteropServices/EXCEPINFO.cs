using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.EXCEPINFO" /> instead.</summary>
	// Token: 0x02000903 RID: 2307
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.EXCEPINFO instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		/// <summary>Represents an error code identifying the error.</summary>
		// Token: 0x04002D49 RID: 11593
		public short wCode;

		/// <summary>This field is reserved; must be set to 0.</summary>
		// Token: 0x04002D4A RID: 11594
		public short wReserved;

		/// <summary>Indicates the name of the source of the exception. Typically, this is an application name.</summary>
		// Token: 0x04002D4B RID: 11595
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		/// <summary>Describes the error intended for the customer.</summary>
		// Token: 0x04002D4C RID: 11596
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		/// <summary>Contains the fully-qualified drive, path, and file name of a Help file with more information about the error.</summary>
		// Token: 0x04002D4D RID: 11597
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		/// <summary>Indicates the Help context ID of the topic within the Help file.</summary>
		// Token: 0x04002D4E RID: 11598
		public int dwHelpContext;

		/// <summary>This field is reserved; must be set to null.</summary>
		// Token: 0x04002D4F RID: 11599
		public IntPtr pvReserved;

		/// <summary>Represents a pointer to a function that takes an <see cref="T:System.Runtime.InteropServices.EXCEPINFO" /> structure as an argument and returns an HRESULT value. If deferred fill-in is not desired, this field is set to null.</summary>
		// Token: 0x04002D50 RID: 11600
		public IntPtr pfnDeferredFillIn;
	}
}
