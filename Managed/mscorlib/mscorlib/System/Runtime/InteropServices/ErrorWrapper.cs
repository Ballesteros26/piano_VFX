using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_ERROR.</summary>
	// Token: 0x020008DD RID: 2269
	[ComVisible(true)]
	[Serializable]
	public sealed class ErrorWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> class with the HRESULT of the error.</summary>
		/// <param name="errorCode">The HRESULT of the error. </param>
		// Token: 0x0600555E RID: 21854 RVA: 0x00128CE3 File Offset: 0x00126EE3
		public ErrorWrapper(int errorCode)
		{
			this.m_ErrorCode = errorCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> class with an object containing the HRESULT of the error.</summary>
		/// <param name="errorCode">The object containing the HRESULT of the error. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="errorCode" /> parameter is not an <see cref="T:System.Int32" /> type.</exception>
		// Token: 0x0600555F RID: 21855 RVA: 0x00128CF2 File Offset: 0x00126EF2
		public ErrorWrapper(object errorCode)
		{
			if (!(errorCode is int))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Int32."), "errorCode");
			}
			this.m_ErrorCode = (int)errorCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> class with the HRESULT that corresponds to the exception supplied.</summary>
		/// <param name="e">The exception to be converted to an error code. </param>
		// Token: 0x06005560 RID: 21856 RVA: 0x00128D23 File Offset: 0x00126F23
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public ErrorWrapper(Exception e)
		{
			this.m_ErrorCode = Marshal.GetHRForException(e);
		}

		/// <summary>Gets the error code of the wrapper.</summary>
		/// <returns>The HRESULT of the error.</returns>
		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06005561 RID: 21857 RVA: 0x00128D37 File Offset: 0x00126F37
		public int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
		}

		// Token: 0x04002CCE RID: 11470
		private int m_ErrorCode;
	}
}
