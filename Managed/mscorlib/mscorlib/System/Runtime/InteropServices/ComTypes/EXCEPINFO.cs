using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes the exceptions that occur during IDispatch::Invoke.</summary>
	// Token: 0x02000999 RID: 2457
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		/// <summary>Represents an error code identifying the error.</summary>
		// Token: 0x04002EAE RID: 11950
		public short wCode;

		/// <summary>This field is reserved; it must be set to 0.</summary>
		// Token: 0x04002EAF RID: 11951
		public short wReserved;

		/// <summary>Indicates the name of the source of the exception. Typically, this is an application name.</summary>
		// Token: 0x04002EB0 RID: 11952
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		/// <summary>Describes the error intended for the customer.</summary>
		// Token: 0x04002EB1 RID: 11953
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		/// <summary>Contains the fully-qualified drive, path, and file name of a Help file that contains more information about the error.</summary>
		// Token: 0x04002EB2 RID: 11954
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		/// <summary>Indicates the Help context ID of the topic within the Help file.</summary>
		// Token: 0x04002EB3 RID: 11955
		public int dwHelpContext;

		/// <summary>This field is reserved; it must be set to null.</summary>
		// Token: 0x04002EB4 RID: 11956
		public IntPtr pvReserved;

		/// <summary>Represents a pointer to a function that takes an <see cref="T:System.Runtime.InteropServices.EXCEPINFO" /> structure as an argument and returns an HRESULT value. If deferred fill-in is not desired, this field is set to null.</summary>
		// Token: 0x04002EB5 RID: 11957
		public IntPtr pfnDeferredFillIn;

		/// <summary>A return value describing the error.</summary>
		// Token: 0x04002EB6 RID: 11958
		public int scode;
	}
}
