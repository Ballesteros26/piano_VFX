using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps a managed object holding a handle to a resource that is passed to unmanaged code using platform invoke.</summary>
	// Token: 0x020008DF RID: 2271
	[ComVisible(true)]
	public struct HandleRef
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleRef" /> class with the object to wrap and a handle to the resource used by unmanaged code.</summary>
		/// <param name="wrapper">A managed object that should not be finalized until the platform invoke call returns. </param>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that indicates a handle to a resource. </param>
		// Token: 0x06005569 RID: 21865 RVA: 0x00128E23 File Offset: 0x00127023
		public HandleRef(object wrapper, IntPtr handle)
		{
			this.m_wrapper = wrapper;
			this.m_handle = handle;
		}

		/// <summary>Gets the object holding the handle to a resource.</summary>
		/// <returns>The object holding the handle to a resource.</returns>
		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x0600556A RID: 21866 RVA: 0x00128E33 File Offset: 0x00127033
		public object Wrapper
		{
			get
			{
				return this.m_wrapper;
			}
		}

		/// <summary>Gets the handle to a resource.</summary>
		/// <returns>The handle to a resource.</returns>
		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x0600556B RID: 21867 RVA: 0x00128E3B File Offset: 0x0012703B
		public IntPtr Handle
		{
			get
			{
				return this.m_handle;
			}
		}

		/// <summary>Returns the handle to a resource of the specified <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</summary>
		/// <returns>The handle to a resource of the specified <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</returns>
		/// <param name="value">The object that needs a handle. </param>
		// Token: 0x0600556C RID: 21868 RVA: 0x00128E3B File Offset: 0x0012703B
		public static explicit operator IntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		/// <summary>Returns the internal integer representation of a <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> object that represents a <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</returns>
		/// <param name="value">A <see cref="T:System.Runtime.InteropServices.HandleRef" /> object to retrieve an internal integer representation from.</param>
		// Token: 0x0600556D RID: 21869 RVA: 0x00128E3B File Offset: 0x0012703B
		public static IntPtr ToIntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		// Token: 0x04002CCF RID: 11471
		internal object m_wrapper;

		// Token: 0x04002CD0 RID: 11472
		internal IntPtr m_handle;
	}
}
