using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Controls access to Simple Mail Transport Protocol (SMTP) servers.</summary>
	// Token: 0x02000592 RID: 1426
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SmtpPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermissionAttribute" /> class. </summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values that specifies the permission behavior.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x06002C67 RID: 11367 RVA: 0x0008208A File Offset: 0x0008028A
		public SmtpPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the level of access to SMTP servers controlled by the attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> value. Valid values are "Connect" and "None".</returns>
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002C68 RID: 11368 RVA: 0x000AF3A4 File Offset: 0x000AD5A4
		// (set) Token: 0x06002C69 RID: 11369 RVA: 0x000AF3AC File Offset: 0x000AD5AC
		public string Access
		{
			get
			{
				return this.access;
			}
			set
			{
				this.access = value;
			}
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000AF3B8 File Offset: 0x000AD5B8
		private SmtpAccess GetSmtpAccess()
		{
			if (this.access == null)
			{
				return SmtpAccess.None;
			}
			string text = this.access.ToLowerInvariant();
			if (text == "connecttounrestrictedport")
			{
				return SmtpAccess.ConnectToUnrestrictedPort;
			}
			if (text == "connect")
			{
				return SmtpAccess.Connect;
			}
			if (!(text == "none"))
			{
				string text2 = global::Locale.GetText("Invalid Access='{0}' value.", new object[] { this.access });
				throw new ArgumentException("Access", text2);
			}
			return SmtpAccess.None;
		}

		/// <summary>Creates a permission object that can be stored with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> instance.</returns>
		// Token: 0x06002C6B RID: 11371 RVA: 0x000AF42F File Offset: 0x000AD62F
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.GetSmtpAccess());
		}

		// Token: 0x040024C4 RID: 9412
		private string access;
	}
}
