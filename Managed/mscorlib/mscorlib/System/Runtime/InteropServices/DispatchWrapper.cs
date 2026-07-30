using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_DISPATCH.</summary>
	// Token: 0x020008DC RID: 2268
	[ComVisible(true)]
	[Serializable]
	public sealed class DispatchWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" /> class with the object being wrapped.</summary>
		/// <param name="obj">The object to be wrapped and converted to <see cref="F:System.Runtime.InteropServices.VarEnum.VT_DISPATCH" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a class or an array.-or- <paramref name="obj" /> does not support IDispatch. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="obj" /> parameter was marked with a <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" />  attribute that was passed a value of false.-or-The <paramref name="obj" /> parameter inherits from a type marked with a <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" />  attribute that was passed a value of false.</exception>
		// Token: 0x0600555C RID: 21852 RVA: 0x00128CBD File Offset: 0x00126EBD
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public DispatchWrapper(object obj)
		{
			if (obj != null)
			{
				Marshal.Release(Marshal.GetIDispatchForObject(obj));
			}
			this.m_WrappedObject = obj;
		}

		/// <summary>Gets the object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</summary>
		/// <returns>The object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</returns>
		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x0600555D RID: 21853 RVA: 0x00128CDB File Offset: 0x00126EDB
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002CCD RID: 11469
		private object m_WrappedObject;
	}
}
