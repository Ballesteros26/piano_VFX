using System;
using System.Collections;

namespace System.Windows.Forms
{
	/// <summary>Provides a low-level encapsulation of a window handle and a window procedure.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000271 RID: 625
	public class NativeWindow : MarshalByRefObject, IWin32Window
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.NativeWindow" /> class.</summary>
		// Token: 0x060028AF RID: 10415 RVA: 0x0009DAB8 File Offset: 0x0009BCB8
		public NativeWindow()
		{
			this.window_handle = IntPtr.Zero;
		}

		/// <summary>Gets the handle for this window. </summary>
		/// <returns>If successful, an <see cref="T:System.IntPtr" /> representing the handle to the associated native Win32 window; otherwise, 0 if no handle is associated with the window.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x0009DAE4 File Offset: 0x0009BCE4
		public IntPtr Handle
		{
			get
			{
				return this.window_handle;
			}
		}

		/// <summary>Retrieves the window associated with the specified handle. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.NativeWindow" /> associated with the specified handle. This method returns null when the handle does not have an associated window.</returns>
		/// <param name="handle">A handle to a window. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060028B2 RID: 10418 RVA: 0x0009DAEC File Offset: 0x0009BCEC
		public static NativeWindow FromHandle(IntPtr handle)
		{
			return NativeWindow.FindFirstInTable(handle);
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x0009DAF4 File Offset: 0x0009BCF4
		internal void InvalidateHandle()
		{
			NativeWindow.RemoveFromTable(this);
			this.window_handle = IntPtr.Zero;
		}

		/// <summary>Assigns a handle to this window. </summary>
		/// <param name="handle">The handle to assign to this window. </param>
		/// <exception cref="T:System.Exception">This window already has a handle. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The windows procedure for the associated native window could not be retrieved.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060028B4 RID: 10420 RVA: 0x0009DB08 File Offset: 0x0009BD08
		public void AssignHandle(IntPtr handle)
		{
			NativeWindow.RemoveFromTable(this);
			this.window_handle = handle;
			NativeWindow.AddToTable(this);
			this.OnHandleChange();
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x0009DB24 File Offset: 0x0009BD24
		private static void AddToTable(NativeWindow window)
		{
			IntPtr handle = window.Handle;
			if (handle == IntPtr.Zero)
			{
				return;
			}
			Hashtable hashtable = NativeWindow.window_collection;
			lock (hashtable)
			{
				object obj = NativeWindow.window_collection[handle];
				if (obj == null)
				{
					NativeWindow.window_collection.Add(handle, window);
				}
				else
				{
					NativeWindow nativeWindow = obj as NativeWindow;
					if (nativeWindow != null)
					{
						if (nativeWindow != window)
						{
							ArrayList arrayList = new ArrayList();
							arrayList.Add(nativeWindow);
							arrayList.Add(window);
							NativeWindow.window_collection[handle] = arrayList;
						}
					}
					else
					{
						ArrayList arrayList2 = (ArrayList)NativeWindow.window_collection[handle];
						if (!arrayList2.Contains(window))
						{
							arrayList2.Add(window);
						}
					}
				}
			}
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x0009DC1C File Offset: 0x0009BE1C
		private static void RemoveFromTable(NativeWindow window)
		{
			IntPtr handle = window.Handle;
			if (handle == IntPtr.Zero)
			{
				return;
			}
			Hashtable hashtable = NativeWindow.window_collection;
			lock (hashtable)
			{
				object obj = NativeWindow.window_collection[handle];
				if (obj != null)
				{
					NativeWindow nativeWindow = obj as NativeWindow;
					if (nativeWindow != null)
					{
						NativeWindow.window_collection.Remove(handle);
					}
					else
					{
						ArrayList arrayList = (ArrayList)NativeWindow.window_collection[handle];
						arrayList.Remove(window);
						if (arrayList.Count == 0)
						{
							NativeWindow.window_collection.Remove(handle);
						}
						else if (arrayList.Count == 1)
						{
							NativeWindow.window_collection[handle] = arrayList[0];
						}
					}
				}
			}
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x0009DD14 File Offset: 0x0009BF14
		private static NativeWindow FindFirstInTable(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			NativeWindow nativeWindow = null;
			Hashtable hashtable = NativeWindow.window_collection;
			lock (hashtable)
			{
				object obj = NativeWindow.window_collection[handle];
				if (obj != null)
				{
					nativeWindow = obj as NativeWindow;
					if (nativeWindow == null)
					{
						ArrayList arrayList = (ArrayList)obj;
						if (arrayList.Count > 0)
						{
							nativeWindow = (NativeWindow)arrayList[0];
						}
					}
				}
			}
			return nativeWindow;
		}

		/// <summary>Creates a window and its handle with the specified creation parameters. </summary>
		/// <param name="cp">A <see cref="T:System.Windows.Forms.CreateParams" /> that specifies the creation parameters for this window. </param>
		/// <exception cref="T:System.OutOfMemoryException">The operating system ran out of resources when trying to create the native window.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The native Win32 API could not create the specified window. </exception>
		/// <exception cref="T:System.InvalidOperationException">The handle of the current native window is already assigned; in explanation, the <see cref="P:System.Windows.Forms.NativeWindow.Handle" /> property is not equal to <see cref="F:System.IntPtr.Zero" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060028B8 RID: 10424 RVA: 0x0009DDB0 File Offset: 0x0009BFB0
		public virtual void CreateHandle(CreateParams cp)
		{
			if (cp != null)
			{
				NativeWindow.WindowCreating = this;
				this.window_handle = XplatUI.CreateWindow(cp);
				NativeWindow.WindowCreating = null;
				if (this.window_handle != IntPtr.Zero)
				{
					NativeWindow.AddToTable(this);
				}
			}
		}

		/// <summary>Invokes the default window procedure associated with this window. </summary>
		/// <param name="m">The message that is currently being processed. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060028B9 RID: 10425 RVA: 0x0009DDEC File Offset: 0x0009BFEC
		public void DefWndProc(ref Message m)
		{
			m.Result = XplatUI.DefWndProc(ref m);
		}

		/// <summary>Destroys the window and its handle. </summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060028BA RID: 10426 RVA: 0x0009DDFC File Offset: 0x0009BFFC
		public virtual void DestroyHandle()
		{
			if (this.window_handle != IntPtr.Zero)
			{
				XplatUI.DestroyWindow(this.window_handle);
			}
		}

		/// <summary>Releases the handle associated with this window. </summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060028BB RID: 10427 RVA: 0x0009DE2C File Offset: 0x0009C02C
		public virtual void ReleaseHandle()
		{
			NativeWindow.RemoveFromTable(this);
			this.window_handle = IntPtr.Zero;
			this.OnHandleChange();
		}

		/// <summary>Releases the resources associated with this window. </summary>
		// Token: 0x060028BC RID: 10428 RVA: 0x0009DE48 File Offset: 0x0009C048
		~NativeWindow()
		{
		}

		/// <summary>Specifies a notification method that is called when the handle for a window is changed. </summary>
		// Token: 0x060028BD RID: 10429 RVA: 0x0009DE80 File Offset: 0x0009C080
		protected virtual void OnHandleChange()
		{
		}

		/// <summary>When overridden in a derived class, manages an unhandled thread exception. </summary>
		/// <param name="e">An <see cref="T:System.Exception" /> that specifies the unhandled thread exception. </param>
		// Token: 0x060028BE RID: 10430 RVA: 0x0009DE84 File Offset: 0x0009C084
		protected virtual void OnThreadException(Exception e)
		{
			Application.OnThreadException(e);
		}

		/// <summary>Invokes the default window procedure associated with this window. </summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that is associated with the current Windows message. </param>
		// Token: 0x060028BF RID: 10431 RVA: 0x0009DE8C File Offset: 0x0009C08C
		protected virtual void WndProc(ref Message m)
		{
			this.DefWndProc(ref m);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x0009DE98 File Offset: 0x0009C098
		internal static IntPtr WndProc(IntPtr hWnd, Msg msg, IntPtr wParam, IntPtr lParam)
		{
			IntPtr intPtr = IntPtr.Zero;
			Message message = default(Message);
			message.HWnd = hWnd;
			message.Msg = (int)msg;
			message.WParam = wParam;
			message.LParam = lParam;
			message.Result = IntPtr.Zero;
			NativeWindow nativeWindow = null;
			try
			{
				object obj = null;
				Hashtable hashtable = NativeWindow.window_collection;
				lock (hashtable)
				{
					obj = NativeWindow.window_collection[hWnd];
				}
				nativeWindow = obj as NativeWindow;
				if (obj == null)
				{
					nativeWindow = NativeWindow.EnsureCreated(nativeWindow, hWnd);
				}
				if (nativeWindow != null)
				{
					nativeWindow.WndProc(ref message);
					intPtr = message.Result;
				}
				else if (obj is ArrayList)
				{
					ArrayList arrayList = (ArrayList)obj;
					ArrayList arrayList2 = arrayList;
					lock (arrayList2)
					{
						if (arrayList.Count > 0)
						{
							nativeWindow = NativeWindow.EnsureCreated((NativeWindow)arrayList[0], hWnd);
							nativeWindow.WndProc(ref message);
							intPtr = message.Result;
							for (int i = 1; i < arrayList.Count; i++)
							{
								((NativeWindow)arrayList[i]).WndProc(ref message);
							}
						}
					}
				}
				else
				{
					intPtr = XplatUI.DefWndProc(ref message);
				}
			}
			catch (Exception ex)
			{
				if (nativeWindow != null)
				{
					nativeWindow.OnThreadException(ex);
				}
			}
			return intPtr;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0009E040 File Offset: 0x0009C240
		private static NativeWindow EnsureCreated(NativeWindow window, IntPtr hWnd)
		{
			if (window == null && NativeWindow.WindowCreating != null)
			{
				window = NativeWindow.WindowCreating;
				NativeWindow.WindowCreating = null;
				if (window.Handle == IntPtr.Zero)
				{
					window.AssignHandle(hWnd);
				}
			}
			return window;
		}

		// Token: 0x04001462 RID: 5218
		private IntPtr window_handle = IntPtr.Zero;

		// Token: 0x04001463 RID: 5219
		private static Hashtable window_collection = new Hashtable();

		// Token: 0x04001464 RID: 5220
		[ThreadStatic]
		private static NativeWindow WindowCreating;
	}
}
