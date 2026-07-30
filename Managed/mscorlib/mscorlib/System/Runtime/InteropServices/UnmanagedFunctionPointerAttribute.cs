using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls the marshaling behavior of a delegate signature passed as an unmanaged function pointer to or from unmanaged code. This class cannot be inherited. </summary>
	// Token: 0x020008A3 RID: 2211
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class UnmanagedFunctionPointerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute" /> class with the specified calling convention. </summary>
		/// <param name="callingConvention">The specified calling convention.</param>
		// Token: 0x060054D4 RID: 21716 RVA: 0x00128316 File Offset: 0x00126516
		public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.m_callingConvention = callingConvention;
		}

		/// <summary>Gets the value of the calling convention.</summary>
		/// <returns>The value of the calling convention specified by the <see cref="M:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute.#ctor(System.Runtime.InteropServices.CallingConvention)" /> constructor.</returns>
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x060054D5 RID: 21717 RVA: 0x00128325 File Offset: 0x00126525
		public CallingConvention CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x04002BEE RID: 11246
		private CallingConvention m_callingConvention;

		/// <summary>Indicates how to marshal string parameters to the method, and controls name mangling.</summary>
		// Token: 0x04002BEF RID: 11247
		public CharSet CharSet;

		/// <summary>Enables or disables best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		// Token: 0x04002BF0 RID: 11248
		public bool BestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI "?" character.</summary>
		// Token: 0x04002BF1 RID: 11249
		public bool ThrowOnUnmappableChar;

		/// <summary>Indicates whether the callee calls the SetLastError Win32 API function before returning from the attributed method.</summary>
		// Token: 0x04002BF2 RID: 11250
		public bool SetLastError;
	}
}
