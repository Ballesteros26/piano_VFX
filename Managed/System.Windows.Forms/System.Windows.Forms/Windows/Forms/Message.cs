using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Implements a Windows message.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000256 RID: 598
	public struct Message
	{
		/// <summary>Gets or sets the window handle of the message.</summary>
		/// <returns>The window handle of the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x00095C98 File Offset: 0x00093E98
		// (set) Token: 0x06002749 RID: 10057 RVA: 0x00095CA0 File Offset: 0x00093EA0
		public IntPtr HWnd
		{
			get
			{
				return this.hwnd;
			}
			set
			{
				this.hwnd = value;
			}
		}

		/// <summary>Specifies the <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</summary>
		/// <returns>The <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x00095CAC File Offset: 0x00093EAC
		// (set) Token: 0x0600274B RID: 10059 RVA: 0x00095CB4 File Offset: 0x00093EB4
		public IntPtr LParam
		{
			get
			{
				return this.lParam;
			}
			set
			{
				this.lParam = value;
			}
		}

		/// <summary>Gets or sets the ID number for the message.</summary>
		/// <returns>The ID number for the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x00095CC0 File Offset: 0x00093EC0
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x00095CC8 File Offset: 0x00093EC8
		public int Msg
		{
			get
			{
				return this.msg;
			}
			set
			{
				this.msg = value;
			}
		}

		/// <summary>Specifies the value that is returned to Windows in response to handling the message.</summary>
		/// <returns>The return value of the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x00095CD4 File Offset: 0x00093ED4
		// (set) Token: 0x0600274F RID: 10063 RVA: 0x00095CDC File Offset: 0x00093EDC
		public IntPtr Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Windows.Forms.Message.WParam" /> field of the message.</summary>
		/// <returns>The <see cref="P:System.Windows.Forms.Message.WParam" /> field of the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x00095CE8 File Offset: 0x00093EE8
		// (set) Token: 0x06002751 RID: 10065 RVA: 0x00095CF0 File Offset: 0x00093EF0
		public IntPtr WParam
		{
			get
			{
				return this.wParam;
			}
			set
			{
				this.wParam = value;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.Message" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Message" /> that represents the message that was created.</returns>
		/// <param name="hWnd">The window handle that the message is for. </param>
		/// <param name="msg">The message ID. </param>
		/// <param name="wparam">The message <paramref name="wparam" /> field. </param>
		/// <param name="lparam">The message <paramref name="lparam" /> field. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002752 RID: 10066 RVA: 0x00095CFC File Offset: 0x00093EFC
		public static Message Create(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			return new Message
			{
				msg = msg,
				hwnd = hWnd,
				wParam = wparam,
				lParam = lparam
			};
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current object.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002753 RID: 10067 RVA: 0x00095D34 File Offset: 0x00093F34
		public override bool Equals(object o)
		{
			return o is Message && (this.msg == ((Message)o).msg && this.hwnd == ((Message)o).hwnd && this.lParam == ((Message)o).lParam && this.wParam == ((Message)o).wParam) && this.result == ((Message)o).result;
		}

		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002754 RID: 10068 RVA: 0x00095DE0 File Offset: 0x00093FE0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets the <see cref="P:System.Windows.Forms.Message.LParam" /> value and converts the value to an object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents an instance of the class specified by the <paramref name="cls" /> parameter, with the data from the <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</returns>
		/// <param name="cls">The type to use to create an instance. This type must be declared as a structure type. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002755 RID: 10069 RVA: 0x00095DF4 File Offset: 0x00093FF4
		public object GetLParam(Type cls)
		{
			return Marshal.PtrToStructure(this.lParam, cls);
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Message" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Message" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002756 RID: 10070 RVA: 0x00095E10 File Offset: 0x00094010
		public override string ToString()
		{
			return string.Format("msg=0x{0:x} ({1}) hwnd=0x{2:x} wparam=0x{3:x} lparam=0x{4:x} result=0x{5:x}", new object[]
			{
				this.msg,
				((Msg)this.msg).ToString(),
				this.hwnd.ToInt32(),
				this.wParam.ToInt32(),
				this.lParam.ToInt32(),
				this.result.ToInt32()
			});
		}

		/// <summary>Determines whether two instances of <see cref="T:System.Windows.Forms.Message" /> are equal. </summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> represent the same <see cref="T:System.Windows.Forms.Message" />; otherwise, false. </returns>
		/// <param name="a">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002757 RID: 10071 RVA: 0x00095E9C File Offset: 0x0009409C
		public static bool operator ==(Message a, Message b)
		{
			return a.hwnd == b.hwnd && a.lParam == b.lParam && a.msg == b.msg && a.result == b.result && a.wParam == b.wParam;
		}

		/// <summary>Determines whether two instances of <see cref="T:System.Windows.Forms.Message" /> are not equal. </summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> do not represent the same <see cref="T:System.Windows.Forms.Message" />; otherwise, false. </returns>
		/// <param name="a">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002758 RID: 10072 RVA: 0x00095F1C File Offset: 0x0009411C
		public static bool operator !=(Message a, Message b)
		{
			return !(a == b);
		}

		// Token: 0x0400138E RID: 5006
		private int msg;

		// Token: 0x0400138F RID: 5007
		private IntPtr hwnd;

		// Token: 0x04001390 RID: 5008
		private IntPtr lParam;

		// Token: 0x04001391 RID: 5009
		private IntPtr wParam;

		// Token: 0x04001392 RID: 5010
		private IntPtr result;
	}
}
