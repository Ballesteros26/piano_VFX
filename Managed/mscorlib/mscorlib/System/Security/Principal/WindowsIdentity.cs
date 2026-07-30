using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System.Security.Principal
{
	/// <summary>Represents a Windows user.</summary>
	// Token: 0x0200062E RID: 1582
	[ComVisible(true)]
	[Serializable]
	public class WindowsIdentity : ClaimsIdentity, IIdentity, IDeserializationCallback, ISerializable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x06004495 RID: 17557 RVA: 0x000F1811 File Offset: 0x000EFA11
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(IntPtr userToken)
			: this(userToken, null, WindowsAccountType.Normal, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token and the specified authentication type.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running. </param>
		/// <param name="type">(Informational use only.) The type of authentication used to identify the user. For more information, see Remarks.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x06004496 RID: 17558 RVA: 0x000F181D File Offset: 0x000EFA1D
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(IntPtr userToken, string type)
			: this(userToken, type, WindowsAccountType.Normal, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token, the specified authentication type, and the specified Windows account type.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running. </param>
		/// <param name="type">(Informational use only.) The type of authentication used to identify the user. For more information, see Remarks.</param>
		/// <param name="acctType">One of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x06004497 RID: 17559 RVA: 0x000F1829 File Offset: 0x000EFA29
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType)
			: this(userToken, type, acctType, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token, the specified authentication type, the specified Windows account type, and the specified authentication status.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running. </param>
		/// <param name="type">(Informational use only.) The type of authentication used to identify the user. For more information, see Remarks.</param>
		/// <param name="acctType">One of the enumeration values. </param>
		/// <param name="isAuthenticated">true to indicate that the user is authenticated; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x06004498 RID: 17560 RVA: 0x000F1835 File Offset: 0x000EFA35
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
		{
			this._type = type;
			this._account = acctType;
			this._authenticated = isAuthenticated;
			this._name = null;
			this.SetToken(userToken);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified User Principal Name (UPN).</summary>
		/// <param name="sUserPrincipalName">The UPN for the user on whose behalf the code is running. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-The computer is not attached to a Windows 2003 or later domain.-or-The computer is not running Windows 2003 or later.-or-The user is not a member of the domain the computer is attached to.</exception>
		// Token: 0x06004499 RID: 17561 RVA: 0x000F1861 File Offset: 0x000EFA61
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(string sUserPrincipalName)
			: this(sUserPrincipalName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified User Principal Name (UPN) and the specified authentication type.</summary>
		/// <param name="sUserPrincipalName">The UPN for the user on whose behalf the code is running. </param>
		/// <param name="type">(Informational use only.) The type of authentication used to identify the user. For more information, see Remarks.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-The computer is not attached to a Windows 2003 or later domain.-or-The computer is not running Windows 2003 or later.-or-The user is not a member of the domain the computer is attached to.</exception>
		// Token: 0x0600449A RID: 17562 RVA: 0x000F186C File Offset: 0x000EFA6C
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(string sUserPrincipalName, string type)
		{
			if (sUserPrincipalName == null)
			{
				throw new NullReferenceException("sUserPrincipalName");
			}
			IntPtr userToken = WindowsIdentity.GetUserToken(sUserPrincipalName);
			if (!Environment.IsUnix && userToken == IntPtr.Zero)
			{
				throw new ArgumentException("only for Windows Server 2003 +");
			}
			this._authenticated = true;
			this._account = WindowsAccountType.Normal;
			this._type = type;
			this.SetToken(userToken);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by information in a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> stream.</summary>
		/// <param name="info">The object containing the account information for the user. </param>
		/// <param name="context">An object that indicates the stream characteristics. </param>
		/// <exception cref="T:System.NotSupportedException">A <see cref="T:System.Security.Principal.WindowsIdentity" /> cannot be serialized across processes. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x0600449B RID: 17563 RVA: 0x000F18CF File Offset: 0x000EFACF
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(SerializationInfo info, StreamingContext context)
		{
			this._info = info;
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x000F18DE File Offset: 0x000EFADE
		internal WindowsIdentity(ClaimsIdentity claimsIdentity, IntPtr userToken)
			: base(claimsIdentity)
		{
			if (userToken != IntPtr.Zero && userToken.ToInt64() > 0L)
			{
				this.SetToken(userToken);
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Principal.WindowsIdentity" />. </summary>
		// Token: 0x0600449D RID: 17565 RVA: 0x000F1906 File Offset: 0x000EFB06
		[ComVisible(false)]
		public void Dispose()
		{
			this._token = IntPtr.Zero;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Principal.WindowsIdentity" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600449E RID: 17566 RVA: 0x000F1906 File Offset: 0x000EFB06
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			this._token = IntPtr.Zero;
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that you can use as a sentinel value in your code to represent an anonymous user. The property value does not represent the built-in anonymous identity used by the Windows operating system.</summary>
		/// <returns>An object that represents an anonymous user.</returns>
		// Token: 0x0600449F RID: 17567 RVA: 0x000F1914 File Offset: 0x000EFB14
		public static WindowsIdentity GetAnonymous()
		{
			WindowsIdentity windowsIdentity;
			if (Environment.IsUnix)
			{
				windowsIdentity = new WindowsIdentity("nobody");
				windowsIdentity._account = WindowsAccountType.Anonymous;
				windowsIdentity._authenticated = false;
				windowsIdentity._type = string.Empty;
			}
			else
			{
				windowsIdentity = new WindowsIdentity(IntPtr.Zero, string.Empty, WindowsAccountType.Anonymous, false);
				windowsIdentity._name = string.Empty;
			}
			return windowsIdentity;
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the current Windows user.</summary>
		/// <returns>An object that represents the current user.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060044A0 RID: 17568 RVA: 0x000F196E File Offset: 0x000EFB6E
		public static WindowsIdentity GetCurrent()
		{
			return new WindowsIdentity(WindowsIdentity.GetCurrentToken(), null, WindowsAccountType.Normal, true);
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the Windows identity for either the thread or the process, depending on the value of the <paramref name="ifImpersonating" /> parameter.</summary>
		/// <returns>An object that represents a Windows user.</returns>
		/// <param name="ifImpersonating">true to return the <see cref="T:System.Security.Principal.WindowsIdentity" /> only if the thread is currently impersonating; false to return the <see cref="T:System.Security.Principal.WindowsIdentity" />   of the thread if it is impersonating or the <see cref="T:System.Security.Principal.WindowsIdentity" /> of the process if the thread is not currently impersonating.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060044A1 RID: 17569 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("need icall changes")]
		public static WindowsIdentity GetCurrent(bool ifImpersonating)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the current Windows user, using the specified desired token access level.</summary>
		/// <returns>An object that represents the current user.</returns>
		/// <param name="desiredAccess">A bitwise combination of the enumeration values. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060044A2 RID: 17570 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("need icall changes")]
		public static WindowsIdentity GetCurrent(TokenAccessLevels desiredAccess)
		{
			throw new NotImplementedException();
		}

		/// <summary>Impersonates the user represented by the <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <returns>An object that represents the Windows user prior to impersonation; this can be used to revert to the original user's context.</returns>
		/// <exception cref="T:System.InvalidOperationException">An anonymous identity attempted to perform an impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">A Win32 error occurred.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060044A3 RID: 17571 RVA: 0x000F197D File Offset: 0x000EFB7D
		public virtual WindowsImpersonationContext Impersonate()
		{
			return new WindowsImpersonationContext(this._token);
		}

		/// <summary>Impersonates the user represented by the specified user token.</summary>
		/// <returns>An object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
		/// <param name="userToken">The handle of a Windows account token. This token is usually retrieved through a call to unmanaged code, such as a call to the Win32 API LogonUser function. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060044A4 RID: 17572 RVA: 0x000F198A File Offset: 0x000EFB8A
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public static WindowsImpersonationContext Impersonate(IntPtr userToken)
		{
			return new WindowsImpersonationContext(userToken);
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecuritySafeCritical]
		public static void RunImpersonated(SafeAccessTokenHandle safeAccessTokenHandle, Action action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecuritySafeCritical]
		public static T RunImpersonated<T>(SafeAccessTokenHandle safeAccessTokenHandle, Func<T> func)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type of authentication used to identify the user.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-The computer is not attached to a Windows 2003 or later domain.-or-The computer is not running Windows 2003 or later.-or-The user is not a member of the domain the computer is attached to.</exception>
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060044A7 RID: 17575 RVA: 0x000F1992 File Offset: 0x000EFB92
		public sealed override string AuthenticationType
		{
			[SecuritySafeCritical]
			get
			{
				return this._type;
			}
		}

		/// <summary>Gets a value that indicates whether the user account is identified as an anonymous account by the system.</summary>
		/// <returns>true if the user account is an anonymous account; otherwise, false.</returns>
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x000F199A File Offset: 0x000EFB9A
		public virtual bool IsAnonymous
		{
			get
			{
				return this._account == WindowsAccountType.Anonymous;
			}
		}

		/// <summary>Gets a value indicating whether the user has been authenticated by Windows.</summary>
		/// <returns>true if the user was authenticated; otherwise, false.</returns>
		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x000F19A5 File Offset: 0x000EFBA5
		public override bool IsAuthenticated
		{
			get
			{
				return this._authenticated;
			}
		}

		/// <summary>Gets a value indicating whether the user account is identified as a <see cref="F:System.Security.Principal.WindowsAccountType.Guest" /> account by the system.</summary>
		/// <returns>true if the user account is a <see cref="F:System.Security.Principal.WindowsAccountType.Guest" /> account; otherwise, false.</returns>
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x000F19AD File Offset: 0x000EFBAD
		public virtual bool IsGuest
		{
			get
			{
				return this._account == WindowsAccountType.Guest;
			}
		}

		/// <summary>Gets a value indicating whether the user account is identified as a <see cref="F:System.Security.Principal.WindowsAccountType.System" /> account by the system.</summary>
		/// <returns>true if the user account is a <see cref="F:System.Security.Principal.WindowsAccountType.System" /> account; otherwise, false.</returns>
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x000F19B8 File Offset: 0x000EFBB8
		public virtual bool IsSystem
		{
			get
			{
				return this._account == WindowsAccountType.System;
			}
		}

		/// <summary>Gets the user's Windows logon name.</summary>
		/// <returns>The Windows logon name of the user on whose behalf the code is being run.</returns>
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x000F19C3 File Offset: 0x000EFBC3
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this._name == null)
				{
					this._name = WindowsIdentity.GetTokenName(this._token);
				}
				return this._name;
			}
		}

		/// <summary>Gets the Windows account token for the user.</summary>
		/// <returns>The handle of the access token associated with the current execution thread.</returns>
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x000F19E4 File Offset: 0x000EFBE4
		public virtual IntPtr Token
		{
			get
			{
				return this._token;
			}
		}

		/// <summary>Gets the groups the current Windows user belongs to.</summary>
		/// <returns>An object representing the groups the current Windows user belongs to.</returns>
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("not implemented")]
		public IdentityReferenceCollection Groups
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the impersonation level for the user.</summary>
		/// <returns>One of the enumeration values that specifies the impersonation level. </returns>
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x0002126B File Offset: 0x0001F46B
		[ComVisible(false)]
		[MonoTODO("not implemented")]
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the security identifier (SID) for the token owner.</summary>
		/// <returns>An object for the token owner.</returns>
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x0002126B File Offset: 0x0001F46B
		[ComVisible(false)]
		[MonoTODO("not implemented")]
		public SecurityIdentifier Owner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the security identifier (SID) for the user.</summary>
		/// <returns>An object for the user.</returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public SecurityIdentifier User
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		// Token: 0x060044B2 RID: 17586 RVA: 0x000F19EC File Offset: 0x000EFBEC
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this._token = (IntPtr)this._info.GetValue("m_userToken", typeof(IntPtr));
			this._name = this._info.GetString("m_name");
			if (this._name != null)
			{
				if (WindowsIdentity.GetTokenName(this._token) != this._name)
				{
					throw new SerializationException("Token-Name mismatch.");
				}
			}
			else
			{
				this._name = WindowsIdentity.GetTokenName(this._token);
				if (this._name == null)
				{
					throw new SerializationException("Token doesn't match a user.");
				}
			}
			this._type = this._info.GetString("m_type");
			this._account = (WindowsAccountType)this._info.GetValue("m_acctType", typeof(WindowsAccountType));
			this._authenticated = this._info.GetBoolean("m_isAuthenticated");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of this execution context.</summary>
		/// <param name="info">An object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <param name="context">An object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />. </param>
		// Token: 0x060044B3 RID: 17587 RVA: 0x000F1AD4 File Offset: 0x000EFCD4
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_userToken", this._token);
			info.AddValue("m_name", this._name);
			info.AddValue("m_type", this._type);
			info.AddValue("m_acctType", this._account);
			info.AddValue("m_isAuthenticated", this._authenticated);
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x000F1B40 File Offset: 0x000EFD40
		internal ClaimsIdentity CloneAsBase()
		{
			return base.Clone();
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x000F19E4 File Offset: 0x000EFBE4
		internal IntPtr GetTokenInternal()
		{
			return this._token;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x000F1B48 File Offset: 0x000EFD48
		private void SetToken(IntPtr token)
		{
			if (Environment.IsUnix)
			{
				this._token = token;
				if (this._type == null)
				{
					this._type = "POSIX";
				}
				if (this._token == IntPtr.Zero)
				{
					this._account = WindowsAccountType.System;
					return;
				}
			}
			else
			{
				if (token == WindowsIdentity.invalidWindows && this._account != WindowsAccountType.Anonymous)
				{
					throw new ArgumentException("Invalid token");
				}
				this._token = token;
				if (this._type == null)
				{
					this._type = "NTLM";
				}
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060044B7 RID: 17591 RVA: 0x0002126B File Offset: 0x0001F46B
		public SafeAccessTokenHandle AccessToken
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060044B8 RID: 17592
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] _GetRoles(IntPtr token);

		// Token: 0x060044B9 RID: 17593
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCurrentToken();

		// Token: 0x060044BA RID: 17594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetTokenName(IntPtr token);

		// Token: 0x060044BB RID: 17595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUserToken(string username);

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class by using the specified <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <param name="identity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.WindowsIdentity" />.</param>
		// Token: 0x060044BD RID: 17597 RVA: 0x0001FB35 File Offset: 0x0001DD35
		[SecuritySafeCritical]
		protected WindowsIdentity(WindowsIdentity identity)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets claims that have the <see cref="F:System.Security.Claims.ClaimTypes.WindowsDeviceClaim" /> property key.</summary>
		/// <returns>A collection of claims that have the <see cref="F:System.Security.Claims.ClaimTypes.WindowsDeviceClaim" /> property key.</returns>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x000F1BD7 File Offset: 0x000EFDD7
		public virtual IEnumerable<Claim> DeviceClaims
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets claims that have the <see cref="F:System.Security.Claims.ClaimTypes.WindowsUserClaim" /> property key.</summary>
		/// <returns>A collection of claims that have the <see cref="F:System.Security.Claims.ClaimTypes.WindowsUserClaim" /> property key.</returns>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x000F1BD7 File Offset: 0x000EFDD7
		public virtual IEnumerable<Claim> UserClaims
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x040022FE RID: 8958
		private IntPtr _token;

		// Token: 0x040022FF RID: 8959
		private string _type;

		// Token: 0x04002300 RID: 8960
		private WindowsAccountType _account;

		// Token: 0x04002301 RID: 8961
		private bool _authenticated;

		// Token: 0x04002302 RID: 8962
		private string _name;

		// Token: 0x04002303 RID: 8963
		private SerializationInfo _info;

		// Token: 0x04002304 RID: 8964
		private static IntPtr invalidWindows = IntPtr.Zero;

		/// <summary>Identifies the name of the default <see cref="T:System.Security.Claims.ClaimsIdentity" /> issuer.</summary>
		/// <returns>NT_AUTHORITY.</returns>
		// Token: 0x04002305 RID: 8965
		[NonSerialized]
		public new const string DefaultIssuer = "AD AUTHORITY";
	}
}
