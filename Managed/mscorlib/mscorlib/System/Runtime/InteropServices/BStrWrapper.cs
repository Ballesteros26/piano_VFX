using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Marshals data of type VT_BSTR from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x020008D5 RID: 2261
	[ComVisible(true)]
	[Serializable]
	public sealed class BStrWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> class with the specified <see cref="T:System.String" /> object.</summary>
		/// <param name="value">The object to wrap and marshal as VT_BSTR.</param>
		// Token: 0x06005542 RID: 21826 RVA: 0x00128A78 File Offset: 0x00126C78
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public BStrWrapper(string value)
		{
			this.m_WrappedObject = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> class with the specified <see cref="T:System.Object" /> object.</summary>
		/// <param name="value">The object to wrap and marshal as VT_BSTR.</param>
		// Token: 0x06005543 RID: 21827 RVA: 0x00128A87 File Offset: 0x00126C87
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public BStrWrapper(object value)
		{
			this.m_WrappedObject = (string)value;
		}

		/// <summary>Gets the wrapped <see cref="T:System.String" /> object to marshal as type VT_BSTR.</summary>
		/// <returns>The object that is wrapped by <see cref="T:System.Runtime.InteropServices.BStrWrapper" />.</returns>
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06005544 RID: 21828 RVA: 0x00128A9B File Offset: 0x00126C9B
		public string WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002CBA RID: 11450
		private string m_WrappedObject;
	}
}
