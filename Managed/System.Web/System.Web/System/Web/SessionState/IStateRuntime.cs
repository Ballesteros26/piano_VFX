using System;
using System.Runtime.InteropServices;

namespace System.Web.SessionState
{
	/// <summary>Defines the interface used by the ASP.NET state service to manage session data.</summary>
	// Token: 0x02000498 RID: 1176
	[Guid("7297744b-e188-40bf-b7e9-56698d25cf44")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IStateRuntime
	{
		/// <summary>Used by the ASP.NET state server to process session data.</summary>
		/// <param name="tracker">An <see cref="T:System.IntPtr" /> pointer to an object stored in the unmanaged ASP.NET state server.</param>
		/// <param name="verb">The action to take on the object.</param>
		/// <param name="uri">An identifier for the session.</param>
		/// <param name="exclusive">The type of access to objects in the store.</param>
		/// <param name="timeout">The number of minutes the session data is stored.</param>
		/// <param name="lockCookieExists">A value that indicates whether the lock cookie exists in the original request from the ASP.NET Web server to the ASP.NET state server.</param>
		/// <param name="lockCookie">The owner of the lock on the session state.</param>
		/// <param name="contentLength">The length, in bytes, of the data stored for the session.</param>
		/// <param name="content">An <see cref="T:System.IntPtr" /> pointer to the content stored for the session in the unmanaged ASP.NET state server.</param>
		// Token: 0x06003579 RID: 13689
		void ProcessRequest([MarshalAs(UnmanagedType.SysInt)] [In] IntPtr tracker, [MarshalAs(UnmanagedType.I4)] [In] int verb, [MarshalAs(UnmanagedType.LPWStr)] [In] string uri, [MarshalAs(UnmanagedType.I4)] [In] int exclusive, [MarshalAs(UnmanagedType.I4)] [In] int timeout, [MarshalAs(UnmanagedType.I4)] [In] int lockCookieExists, [MarshalAs(UnmanagedType.I4)] [In] int lockCookie, [MarshalAs(UnmanagedType.I4)] [In] int contentLength, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr content);

		/// <summary>Used by the ASP.NET state server to process session data.</summary>
		/// <param name="tracker">An <see cref="T:System.IntPtr" /> pointer to an object stored in the unmanaged ASP.NET state server.</param>
		/// <param name="verb">The action to take on the object.</param>
		/// <param name="uri">An identifier for the session.</param>
		/// <param name="exclusive">The type of access to objects in the store.</param>
		/// <param name="extraFlags">A value that indicates whether the current session is an uninitialized, cookieless session.</param>
		/// <param name="timeout">The number of minutes the session data is stored.</param>
		/// <param name="lockCookieExists">A value that indicates whether the lock cookie exists in the original request from the ASP.NET Web server to the ASP.NET state server.</param>
		/// <param name="lockCookie">The owner of the lock on the session state.</param>
		/// <param name="contentLength">The length, in bytes, of the data stored for the session.</param>
		/// <param name="content">An <see cref="T:System.IntPtr" /> pointer to the content stored for the session in the unmanaged ASP.NET state server.</param>
		// Token: 0x0600357A RID: 13690
		void ProcessRequest([MarshalAs(UnmanagedType.SysInt)] [In] IntPtr tracker, [MarshalAs(UnmanagedType.I4)] [In] int verb, [MarshalAs(UnmanagedType.LPWStr)] [In] string uri, [MarshalAs(UnmanagedType.I4)] [In] int exclusive, [MarshalAs(UnmanagedType.I4)] [In] int extraFlags, [MarshalAs(UnmanagedType.I4)] [In] int timeout, [MarshalAs(UnmanagedType.I4)] [In] int lockCookieExists, [MarshalAs(UnmanagedType.I4)] [In] int lockCookie, [MarshalAs(UnmanagedType.I4)] [In] int contentLength, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr content);

		/// <summary>Stops the processing of session data stored in ASP.NET state server.</summary>
		// Token: 0x0600357B RID: 13691
		void StopProcessing();
	}
}
