using System;
using System.IO;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Provides access to properties and values of the ticket used with forms authentication to identify users. This class cannot be inherited.</summary>
	// Token: 0x020004C0 RID: 1216
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class FormsAuthenticationTicket
	{
		// Token: 0x060036BA RID: 14010 RVA: 0x0008F78C File Offset: 0x0008D98C
		internal byte[] ToByteArray()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.version);
			binaryWriter.Write(this.persistent);
			binaryWriter.Write(this.issue_date.Ticks);
			binaryWriter.Write(this.expiration.Ticks);
			binaryWriter.Write(this.name != null);
			if (this.name != null)
			{
				binaryWriter.Write(this.name);
			}
			binaryWriter.Write(this.cookie_path != null);
			if (this.cookie_path != null)
			{
				binaryWriter.Write(this.cookie_path);
			}
			binaryWriter.Write(this.user_data != null);
			if (this.user_data != null)
			{
				binaryWriter.Write(this.user_data);
			}
			binaryWriter.Flush();
			return memoryStream.ToArray();
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x0008F854 File Offset: 0x0008DA54
		internal static FormsAuthenticationTicket FromByteArray(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(bytes));
			FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket();
			formsAuthenticationTicket.version = binaryReader.ReadInt32();
			formsAuthenticationTicket.persistent = binaryReader.ReadBoolean();
			formsAuthenticationTicket.issue_date = new DateTime(binaryReader.ReadInt64());
			formsAuthenticationTicket.expiration = new DateTime(binaryReader.ReadInt64());
			if (binaryReader.ReadBoolean())
			{
				formsAuthenticationTicket.name = binaryReader.ReadString();
			}
			if (binaryReader.ReadBoolean())
			{
				formsAuthenticationTicket.cookie_path = binaryReader.ReadString();
			}
			if (binaryReader.ReadBoolean())
			{
				formsAuthenticationTicket.user_data = binaryReader.ReadString();
			}
			return formsAuthenticationTicket;
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x00002050 File Offset: 0x00000250
		private FormsAuthenticationTicket()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> class with cookie name, version, expiration date, issue date, persistence, and user-specific data. The cookie path is set to the default value established in the application's configuration file.</summary>
		/// <param name="version">The version number of the ticket.</param>
		/// <param name="name">The user name associated with the ticket.</param>
		/// <param name="issueDate">The local date and time at which the ticket was issued.</param>
		/// <param name="expiration">The local date and time at which the ticket expires.</param>
		/// <param name="isPersistent">true if the ticket will be stored in a persistent cookie (saved across browser sessions); otherwise, false. If the ticket is stored in the URL, this value is ignored.</param>
		/// <param name="userData">The user-specific data to be stored with the ticket.</param>
		// Token: 0x060036BD RID: 14013 RVA: 0x0008F8F8 File Offset: 0x0008DAF8
		public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration, bool isPersistent, string userData)
		{
			this.version = version;
			this.name = name;
			this.issue_date = issueDate;
			this.expiration = expiration;
			this.persistent = isPersistent;
			this.user_data = userData;
			this.cookie_path = "/";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> class with cookie name, version, directory path, issue date, expiration date, persistence, and user-defined data.</summary>
		/// <param name="version">The version number of the ticket. </param>
		/// <param name="name">The user name associated with the ticket. </param>
		/// <param name="issueDate">The local date and time at which the ticket was issued. </param>
		/// <param name="expiration">The local date and time at which the ticket expires. </param>
		/// <param name="isPersistent">true if the ticket will be stored in a persistent cookie (saved across browser sessions); otherwise, false. If the ticket is stored in the URL, this value is ignored.</param>
		/// <param name="userData">The user-specific data to be stored with the ticket. </param>
		/// <param name="cookiePath">The path for the ticket when stored in a cookie. </param>
		// Token: 0x060036BE RID: 14014 RVA: 0x0008F938 File Offset: 0x0008DB38
		public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration, bool isPersistent, string userData, string cookiePath)
		{
			this.version = version;
			this.name = name;
			this.issue_date = issueDate;
			this.expiration = expiration;
			this.persistent = isPersistent;
			this.user_data = userData;
			this.cookie_path = cookiePath;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> class using a cookie name and expiration information.</summary>
		/// <param name="name">The user name associated with the ticket.</param>
		/// <param name="isPersistent">true if the ticket will be stored in a persistent cookie (saved across browser sessions); otherwise, false. If the ticket is stored in the URL, this value is ignored.</param>
		/// <param name="timeout">The time, in minutes, for which the authentication ticket is valid.</param>
		// Token: 0x060036BF RID: 14015 RVA: 0x0008F978 File Offset: 0x0008DB78
		public FormsAuthenticationTicket(string name, bool isPersistent, int timeout)
		{
			this.version = 1;
			this.name = name;
			this.issue_date = DateTime.Now;
			this.persistent = isPersistent;
			if (this.persistent)
			{
				this.expiration = this.issue_date.AddYears(50);
			}
			else
			{
				this.expiration = this.issue_date.AddMinutes((double)timeout);
			}
			this.user_data = "";
			this.cookie_path = "/";
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x0008F9F1 File Offset: 0x0008DBF1
		internal void SetDates(DateTime issue_date, DateTime expiration)
		{
			this.issue_date = issue_date;
			this.expiration = expiration;
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x0008FA01 File Offset: 0x0008DC01
		internal FormsAuthenticationTicket Clone()
		{
			return new FormsAuthenticationTicket(this.version, this.name, this.issue_date, this.expiration, this.persistent, this.user_data, this.cookie_path);
		}

		/// <summary>Gets the cookie path for the forms-authentication ticket.</summary>
		/// <returns>The cookie path for the forms-authentication ticket.</returns>
		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x0008FA32 File Offset: 0x0008DC32
		public string CookiePath
		{
			get
			{
				return this.cookie_path;
			}
		}

		/// <summary>Gets the local date and time at which the forms-authentication ticket expires.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> at which the forms-authentication ticket expires.</returns>
		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x0008FA3A File Offset: 0x0008DC3A
		public DateTime Expiration
		{
			get
			{
				return this.expiration;
			}
		}

		/// <summary>Gets a value indicating whether the forms-authentication ticket has expired.</summary>
		/// <returns>true if the forms-authentication ticket has expired; otherwise, false.</returns>
		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x0008FA42 File Offset: 0x0008DC42
		public bool Expired
		{
			get
			{
				return DateTime.Now > this.expiration;
			}
		}

		/// <summary>Gets a value indicating whether the cookie that contains the forms-authentication ticket information is persistent.</summary>
		/// <returns>true if a durable cookie (a cookie that is saved across browser sessions) was issued; otherwise, false.</returns>
		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x0008FA54 File Offset: 0x0008DC54
		public bool IsPersistent
		{
			get
			{
				return this.persistent;
			}
		}

		/// <summary>Gets the local date and time at which the forms-authentication ticket was originally issued.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> when the forms-authentication ticket was originally issued.</returns>
		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x0008FA5C File Offset: 0x0008DC5C
		public DateTime IssueDate
		{
			get
			{
				return this.issue_date;
			}
		}

		/// <summary>Gets the user name associated with the forms-authentication ticket.</summary>
		/// <returns>The user name associated with the forms-authentication ticket.</returns>
		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x0008FA64 File Offset: 0x0008DC64
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a user-specific string stored with the ticket.</summary>
		/// <returns>A user-specific string stored with the ticket. The default is an empty string ("").</returns>
		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060036C8 RID: 14024 RVA: 0x0008FA6C File Offset: 0x0008DC6C
		public string UserData
		{
			get
			{
				return this.user_data;
			}
		}

		/// <summary>Gets the version number of the ticket.</summary>
		/// <returns>The version number of the ticket. The default is 2.</returns>
		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x0008FA74 File Offset: 0x0008DC74
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x04001DD4 RID: 7636
		private int version;

		// Token: 0x04001DD5 RID: 7637
		private bool persistent;

		// Token: 0x04001DD6 RID: 7638
		private DateTime issue_date;

		// Token: 0x04001DD7 RID: 7639
		private DateTime expiration;

		// Token: 0x04001DD8 RID: 7640
		private string name;

		// Token: 0x04001DD9 RID: 7641
		private string cookie_path;

		// Token: 0x04001DDA RID: 7642
		private string user_data;
	}
}
