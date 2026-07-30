using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.Util;
using Unity;

namespace System.Web.Security
{
	/// <summary>Represents security information for the current HTTP request, including role membership. This class cannot be inherited.</summary>
	// Token: 0x020004CA RID: 1226
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public class RolePrincipal : IPrincipal, ISerializable
	{
		/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" />.</summary>
		/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		// Token: 0x06003761 RID: 14177 RVA: 0x00090B50 File Offset: 0x0008ED50
		public RolePrincipal(IIdentity identity)
		{
			this._version = 1;
			base..ctor();
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this._identity = identity;
			this._cookiePath = this.RoleManagerConfig.CookiePath;
			this._issueDate = DateTime.Now;
			this._expireDate = this._issueDate.Add(this.RoleManagerConfig.CookieTimeout);
		}

		/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" /> with role information from the specified <paramref name="encryptedTicket" />.</summary>
		/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
		/// <param name="encryptedTicket">A string that contains encrypted role information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.-or-<paramref name="encryptedTicket" /> is null.</exception>
		// Token: 0x06003762 RID: 14178 RVA: 0x00090BB7 File Offset: 0x0008EDB7
		public RolePrincipal(IIdentity identity, string encryptedTicket)
			: this(identity)
		{
			this.DecryptTicket(encryptedTicket);
		}

		/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" /> using the specified <paramref name="providerName" />.</summary>
		/// <param name="providerName">The name of the role provider for the user.</param>
		/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providerName" /> is null.-or-<paramref name="providerName" /> refers to a role provider that does not exist in the configuration for the application.</exception>
		// Token: 0x06003763 RID: 14179 RVA: 0x00090BC7 File Offset: 0x0008EDC7
		public RolePrincipal(string providerName, IIdentity identity)
			: this(identity)
		{
			if (providerName == null)
			{
				throw new ArgumentNullException("providerName");
			}
			this._providerName = providerName;
		}

		/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" /> using the specified <paramref name="providerName" /> and role information from the specified <paramref name="encryptedTicket" />.</summary>
		/// <param name="providerName">The name of the role provider for the user.</param>
		/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
		/// <param name="encryptedTicket">A string that contains encrypted role information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.-or-<paramref name="encryptedTicket" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providerName" /> is null.-or-<paramref name="providerName" /> refers to a role provider that does not exist in the configuration for the application.</exception>
		// Token: 0x06003764 RID: 14180 RVA: 0x00090BE5 File Offset: 0x0008EDE5
		public RolePrincipal(string providerName, IIdentity identity, string encryptedTicket)
			: this(providerName, identity)
		{
			this.DecryptTicket(encryptedTicket);
		}

		/// <summary>Gets a list of roles that the <see cref="T:System.Web.Security.RolePrincipal" /> is a member of.</summary>
		/// <returns>The list of roles that the <see cref="T:System.Web.Security.RolePrincipal" /> is a member of.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Security.RolePrincipal.Identity" /> property is null.</exception>
		// Token: 0x06003765 RID: 14181 RVA: 0x00090BF8 File Offset: 0x0008EDF8
		public string[] GetRoles()
		{
			if (!this._identity.IsAuthenticated)
			{
				return new string[0];
			}
			if (!this.IsRoleListCached || this.Expired)
			{
				this._cachedArray = this.Provider.GetRolesForUser(this._identity.Name);
				this._cachedRoles = new HybridDictionary(true);
				foreach (string text in this._cachedArray)
				{
					this._cachedRoles.Add(text, text);
				}
				this._listChanged = true;
			}
			return this._cachedArray;
		}

		/// <summary>Gets a value indicating whether the user represented by the <see cref="T:System.Web.Security.RolePrincipal" /> is in the specified role.</summary>
		/// <returns>true if user represented by the <see cref="T:System.Web.Security.RolePrincipal" /> is in the specified role; otherwise, false.</returns>
		/// <param name="role">The role to search for.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Security.RolePrincipal.Identity" /> property is null.</exception>
		// Token: 0x06003766 RID: 14182 RVA: 0x00090C84 File Offset: 0x0008EE84
		public bool IsInRole(string role)
		{
			if (!this._identity.IsAuthenticated)
			{
				return false;
			}
			this.GetRoles();
			return this._cachedRoles[role] != null;
		}

		/// <summary>Returns the role information cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object encrypted based on the <see cref="P:System.Web.Security.Roles.CookieProtectionValue" />.</summary>
		/// <returns>The role information cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object encrypted based on the <see cref="P:System.Web.Security.Roles.CookieProtectionValue" />.</returns>
		// Token: 0x06003767 RID: 14183 RVA: 0x00090CAC File Offset: 0x0008EEAC
		public string ToEncryptedTicket()
		{
			string text = string.Join(",", this.GetRoles());
			string cookiePath = this.RoleManagerConfig.CookiePath;
			int num = text.Length + cookiePath.Length + 64;
			if (this._cachedArray.Length > Roles.MaxCachedResults)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream(num);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.Version);
			binaryWriter.Write(DateTime.Now.Ticks);
			binaryWriter.Write(this._expireDate.Ticks);
			binaryWriter.Write(cookiePath);
			binaryWriter.Write(text);
			CookieProtection cookieProtection = this.RoleManagerConfig.CookieProtection;
			byte[] array = memoryStream.GetBuffer();
			if (cookieProtection == CookieProtection.All)
			{
				array = MachineKeySectionUtils.EncryptSign(this.MachineConfig, array);
			}
			else if (cookieProtection == CookieProtection.Encryption)
			{
				array = MachineKeySectionUtils.Encrypt(this.MachineConfig, array);
			}
			else if (cookieProtection == CookieProtection.Validation)
			{
				array = MachineKeySectionUtils.Sign(this.MachineConfig, array);
			}
			return RolePrincipal.GetBase64FromBytes(array, 0, array.Length);
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x00090DA4 File Offset: 0x0008EFA4
		private void DecryptTicket(string encryptedTicket)
		{
			if (encryptedTicket == null || encryptedTicket == string.Empty)
			{
				throw new ArgumentException("Invalid encrypted ticket", "encryptedTicket");
			}
			byte[] bytesFromBase = RolePrincipal.GetBytesFromBase64(encryptedTicket);
			byte[] array = null;
			CookieProtection cookieProtection = this.RoleManagerConfig.CookieProtection;
			if (cookieProtection == CookieProtection.All)
			{
				array = MachineKeySectionUtils.VerifyDecrypt(this.MachineConfig, bytesFromBase);
			}
			else if (cookieProtection == CookieProtection.Encryption)
			{
				array = MachineKeySectionUtils.Decrypt(this.MachineConfig, bytesFromBase);
			}
			else if (cookieProtection == CookieProtection.Validation)
			{
				array = MachineKeySectionUtils.Verify(this.MachineConfig, bytesFromBase);
			}
			if (array == null)
			{
				throw new HttpException("ticket validation failed");
			}
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(array));
			this._version = binaryReader.ReadInt32();
			this._issueDate = new DateTime(binaryReader.ReadInt64());
			this._expireDate = new DateTime(binaryReader.ReadInt64());
			this._cookiePath = binaryReader.ReadString();
			string text = binaryReader.ReadString();
			if (!this.Expired)
			{
				this.InitializeRoles(text);
				if (Roles.CookieSlidingExpiration && this._expireDate - DateTime.Now < TimeSpan.FromTicks(this.RoleManagerConfig.CookieTimeout.Ticks / 2L))
				{
					this._issueDate = DateTime.Now;
					this._expireDate = DateTime.Now.Add(this.RoleManagerConfig.CookieTimeout);
					this.SetDirty();
					return;
				}
			}
			else
			{
				this._issueDate = DateTime.Now;
				this._expireDate = this._issueDate.Add(this.RoleManagerConfig.CookieTimeout);
			}
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x00090F20 File Offset: 0x0008F120
		private void InitializeRoles(string decryptedRoles)
		{
			this._cachedArray = decryptedRoles.Split(new char[] { ',' });
			this._cachedRoles = new HybridDictionary(true);
			foreach (string text in this._cachedArray)
			{
				this._cachedRoles.Add(text, text);
			}
		}

		/// <summary>Gets a value indicating whether the list of role names cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object has been modified.</summary>
		/// <returns>true if the list of role names cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object has been modified; otherwise, false.</returns>
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x00090F76 File Offset: 0x0008F176
		public bool CachedListChanged
		{
			get
			{
				return this._listChanged;
			}
		}

		/// <summary>Gets the path for the cached role names cookie.</summary>
		/// <returns>The path of the cookie where role names are cached. The default is /.</returns>
		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x00090F7E File Offset: 0x0008F17E
		public string CookiePath
		{
			get
			{
				return this._cookiePath;
			}
		}

		/// <summary>Gets a value indicating whether the roles cookie has expired.</summary>
		/// <returns>true if the roles cookie has expired; otherwise, false.</returns>
		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x00090F86 File Offset: 0x0008F186
		public bool Expired
		{
			get
			{
				return this.ExpireDate < DateTime.Now;
			}
		}

		/// <summary>Gets the date and time when the roles cookie will expire.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> value when the roles cookie will expire.</returns>
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x00090F98 File Offset: 0x0008F198
		public DateTime ExpireDate
		{
			get
			{
				return this._expireDate;
			}
		}

		/// <summary>Gets the security identity for the current HTTP request.</summary>
		/// <returns>The security identity for the current HTTP request.</returns>
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x00090FA0 File Offset: 0x0008F1A0
		public IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		/// <summary>Gets a value indicating whether the list of roles for the user has been cached in a cookie.</summary>
		/// <returns>true if role names are cached in a cookie; otherwise, false.</returns>
		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x00090FA8 File Offset: 0x0008F1A8
		public bool IsRoleListCached
		{
			get
			{
				return this._cachedRoles != null && this.RoleManagerConfig.CacheRolesInCookie;
			}
		}

		/// <summary>Gets the date and time that the roles cookie was issued.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that the roles cookie was issued.</returns>
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x00090FBF File Offset: 0x0008F1BF
		public DateTime IssueDate
		{
			get
			{
				return this._issueDate;
			}
		}

		/// <summary>Gets the name of the role provider that stores and retrieves role information for the user.</summary>
		/// <returns>The name of the role provider that stores and retrieves role information for the user.</returns>
		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x00090FC7 File Offset: 0x0008F1C7
		public string ProviderName
		{
			get
			{
				if (!string.IsNullOrEmpty(this._providerName))
				{
					return this._providerName;
				}
				return this.Provider.Name;
			}
		}

		/// <summary>Gets the version number of the roles cookie.</summary>
		/// <returns>The version number of the roles cookie.</returns>
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x00090FE8 File Offset: 0x0008F1E8
		public int Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x00090FF0 File Offset: 0x0008F1F0
		private RoleProvider Provider
		{
			get
			{
				if (string.IsNullOrEmpty(this._providerName))
				{
					return Roles.Provider;
				}
				return Roles.Providers[this._providerName];
			}
		}

		/// <summary>Marks the cached role list as having been changed.</summary>
		// Token: 0x06003774 RID: 14196 RVA: 0x00091015 File Offset: 0x0008F215
		public void SetDirty()
		{
			this._listChanged = true;
			this._cachedRoles = null;
			this._cachedArray = null;
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x0009102C File Offset: 0x0008F22C
		private static string GetBase64FromBytes(byte[] bytes, int offset, int len)
		{
			return Convert.ToBase64String(bytes, offset, len);
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x00091036 File Offset: 0x0008F236
		private static byte[] GetBytesFromBase64(string base64String)
		{
			return Convert.FromBase64String(base64String);
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x0009103E File Offset: 0x0008F23E
		private RoleManagerSection RoleManagerConfig
		{
			get
			{
				return (RoleManagerSection)WebConfigurationManager.GetSection("system.web/roleManager");
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x0009104F File Offset: 0x0008F24F
		private MachineKeySection MachineConfig
		{
			get
			{
				return (MachineKeySection)WebConfigurationManager.GetSection("system.web/machineKey");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.RolePrincipal" /> class using information that is contained in the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object and using the specified streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x06003779 RID: 14201 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected RolePrincipal(SerializationInfo info, StreamingContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required in order to serialize the target object using the specified streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x0600377A RID: 14202 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.Serialization.ISerializationSurrogate.GetObjectData(System.Object,System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x0600377B RID: 14203 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001DF1 RID: 7665
		private IIdentity _identity;

		// Token: 0x04001DF2 RID: 7666
		private bool _listChanged;

		// Token: 0x04001DF3 RID: 7667
		private string[] _cachedArray;

		// Token: 0x04001DF4 RID: 7668
		private HybridDictionary _cachedRoles;

		// Token: 0x04001DF5 RID: 7669
		private readonly string _providerName;

		// Token: 0x04001DF6 RID: 7670
		private int _version;

		// Token: 0x04001DF7 RID: 7671
		private string _cookiePath;

		// Token: 0x04001DF8 RID: 7672
		private DateTime _issueDate;

		// Token: 0x04001DF9 RID: 7673
		private DateTime _expireDate;
	}
}
