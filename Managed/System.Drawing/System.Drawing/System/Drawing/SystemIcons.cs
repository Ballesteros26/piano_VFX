using System;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemIcons" /> class is an <see cref="T:System.Drawing.Icon" /> object for Windows system-wide icons. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200008B RID: 139
	public sealed class SystemIcons
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00014A08 File Offset: 0x00012C08
		static SystemIcons()
		{
			SystemIcons.icons[0] = new Icon("Mono.ico", true);
			SystemIcons.icons[1] = new Icon("Information.ico", true);
			SystemIcons.icons[2] = new Icon("Error.ico", true);
			SystemIcons.icons[3] = new Icon("Warning.ico", true);
			SystemIcons.icons[4] = new Icon("Question.ico", true);
			SystemIcons.icons[5] = new Icon("Shield.ico", true);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00002050 File Offset: 0x00000250
		private SystemIcons()
		{
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the default application icon (WIN32: IDI_APPLICATION).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the default application icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00014A8C File Offset: 0x00012C8C
		public static Icon Application
		{
			get
			{
				return SystemIcons.icons[0];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system asterisk icon (WIN32: IDI_ASTERISK).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system asterisk icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00014A95 File Offset: 0x00012C95
		public static Icon Asterisk
		{
			get
			{
				return SystemIcons.icons[1];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system error icon (WIN32: IDI_ERROR).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system error icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00014A9E File Offset: 0x00012C9E
		public static Icon Error
		{
			get
			{
				return SystemIcons.icons[2];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system exclamation icon (WIN32: IDI_EXCLAMATION).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system exclamation icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00014AA7 File Offset: 0x00012CA7
		public static Icon Exclamation
		{
			get
			{
				return SystemIcons.icons[3];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system hand icon (WIN32: IDI_HAND).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system hand icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00014A9E File Offset: 0x00012C9E
		public static Icon Hand
		{
			get
			{
				return SystemIcons.icons[2];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system information icon (WIN32: IDI_INFORMATION).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system information icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00014A95 File Offset: 0x00012C95
		public static Icon Information
		{
			get
			{
				return SystemIcons.icons[1];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system question icon (WIN32: IDI_QUESTION).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system question icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00014AB0 File Offset: 0x00012CB0
		public static Icon Question
		{
			get
			{
				return SystemIcons.icons[4];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system warning icon (WIN32: IDI_WARNING).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system warning icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00014AA7 File Offset: 0x00012CA7
		public static Icon Warning
		{
			get
			{
				return SystemIcons.icons[3];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the Windows logo icon (WIN32: IDI_WINLOGO).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the Windows logo icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00014A8C File Offset: 0x00012C8C
		public static Icon WinLogo
		{
			get
			{
				return SystemIcons.icons[0];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the shield icon.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the shield icon.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00014AB9 File Offset: 0x00012CB9
		public static Icon Shield
		{
			get
			{
				return SystemIcons.icons[5];
			}
		}

		// Token: 0x04000556 RID: 1366
		private static Icon[] icons = new Icon[6];

		// Token: 0x04000557 RID: 1367
		private const int Application_Winlogo = 0;

		// Token: 0x04000558 RID: 1368
		private const int Asterisk_Information = 1;

		// Token: 0x04000559 RID: 1369
		private const int Error_Hand = 2;

		// Token: 0x0400055A RID: 1370
		private const int Exclamation_Warning = 3;

		// Token: 0x0400055B RID: 1371
		private const int Question_ = 4;

		// Token: 0x0400055C RID: 1372
		private const int Shield_ = 5;
	}
}
