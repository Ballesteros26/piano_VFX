using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.DirectoryServices.Design;
using System.Runtime.InteropServices;
using Novell.Directory.Ldap;
using Novell.Directory.Ldap.Utilclass;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> class encapsulates a node or object in the Active Directory Domain Services hierarchy.</summary>
	// Token: 0x02000014 RID: 20
	[TypeConverter(typeof(DirectoryEntryConverter))]
	public class DirectoryEntry : Component
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000264C File Offset: 0x0000084C
		internal string Fdn
		{
			get
			{
				if (this._Fdn == null)
				{
					string dn = new LdapUrl(this.ADsPath).getDN();
					if (dn != null)
					{
						this._Fdn = dn;
					}
					else
					{
						this._Fdn = string.Empty;
					}
				}
				return this._Fdn;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000268F File Offset: 0x0000088F
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000026A5 File Offset: 0x000008A5
		internal LdapConnection conn
		{
			get
			{
				if (this._conn == null)
				{
					this.InitBlock();
				}
				return this._conn;
			}
			set
			{
				this._conn = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000026AE File Offset: 0x000008AE
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000026B6 File Offset: 0x000008B6
		internal bool Nflag
		{
			get
			{
				return this._Nflag;
			}
			set
			{
				this._Nflag = value;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000026C0 File Offset: 0x000008C0
		private void InitBlock()
		{
			try
			{
				this._conn = new LdapConnection();
				LdapUrl ldapUrl = new LdapUrl(this.ADsPath);
				this._conn.Connect(ldapUrl.Host, ldapUrl.Port);
				this._conn.Bind(this.Username, this.Password, (AuthenticationTypes)this.AuthenticationType);
			}
			catch (LdapException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002738 File Offset: 0x00000938
		private void InitEntry()
		{
			LdapUrl ldapUrl = new LdapUrl(this.ADsPath);
			string dn = ldapUrl.getDN();
			if (dn == null)
			{
				this._Name = ldapUrl.Host + ":" + ldapUrl.Port;
				this._Parent = new DirectoryEntry(this.conn);
				this._Parent.Path = "Ldap:";
				return;
			}
			if (string.Compare(dn, "rootDSE", true) == 0)
			{
				this.InitToRootDse(ldapUrl.Host, ldapUrl.Port);
				return;
			}
			DN dn2 = new DN(dn);
			string[] array = dn2.explodeDN(false);
			this._Name = array[0];
			this._Parent = new DirectoryEntry(this.conn);
			this._Parent.Path = DirectoryEntry.GetLdapUrlString(ldapUrl.Host, ldapUrl.Port, dn2.Parent.ToString());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class.</summary>
		// Token: 0x0600005C RID: 92 RVA: 0x0000280F File Offset: 0x00000A0F
		public DirectoryEntry()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class that binds to the specified native Active Directory Domain Services object.</summary>
		/// <param name="adsObject">The name of the native Active Directory Domain Services object to bind to.</param>
		// Token: 0x0600005D RID: 93 RVA: 0x00002829 File Offset: 0x00000A29
		public DirectoryEntry(object adsObject)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class that binds this instance to the node in Active Directory Domain Services located at the specified path.</summary>
		/// <param name="path">The path at which to bind the <see cref="M:System.DirectoryServices.DirectoryEntry.#ctor(System.String)" /> to the directory. The <see cref="P:System.DirectoryServices.DirectoryEntry.Path" /> property is initialized to this value.</param>
		// Token: 0x0600005E RID: 94 RVA: 0x00002848 File Offset: 0x00000A48
		public DirectoryEntry(string path)
		{
			this._Path = path;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class.</summary>
		/// <param name="username">The user name to use when authenticating the client. The <see cref="P:System.DirectoryServices.DirectoryEntry.Username" /> property is initialized to this value.</param>
		// Token: 0x0600005F RID: 95 RVA: 0x00002869 File Offset: 0x00000A69
		public DirectoryEntry(string path, string username, string password)
		{
			this._Path = path;
			this._Username = username;
			this._Password = password;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class.</summary>
		/// <param name="username">The user name to use when authenticating the client. The <see cref="P:System.DirectoryServices.DirectoryEntry.Username" /> property is initialized to this value.</param>
		/// <param name="authenticationType">One of the <see cref="T:System.DirectoryServices.AuthenticationTypes" /> values. The <see cref="P:System.DirectoryServices.DirectoryEntry.AuthenticationType" /> property is initialized to this value.</param>
		// Token: 0x06000060 RID: 96 RVA: 0x00002898 File Offset: 0x00000A98
		public DirectoryEntry(string path, string username, string password, AuthenticationTypes authenticationType)
		{
			this._Path = path;
			this._Username = username;
			this._Password = password;
			this._AuthenticationType = authenticationType;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000028CF File Offset: 0x00000ACF
		internal DirectoryEntry(LdapConnection lconn)
		{
			this.conn = lconn;
		}

		/// <summary>Gets or sets the type of authentication to use.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.AuthenticationTypes" /> values.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000028F0 File Offset: 0x00000AF0
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000028F8 File Offset: 0x00000AF8
		[DSDescription("Type of authentication to use while Binding to Ldap server")]
		[DefaultValue(AuthenticationTypes.None)]
		public AuthenticationTypes AuthenticationType
		{
			get
			{
				return this._AuthenticationType;
			}
			set
			{
				this._AuthenticationType = value;
			}
		}

		/// <summary>Gets the child entries of this node in the Active Directory Domain Services hierarchy.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntries" /> object containing the child entries of this node in the Active Directory Domain Services hierarchy.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002901 File Offset: 0x00000B01
		[DSDescription("Child entries of this node")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DirectoryEntries Children
		{
			get
			{
				this._Children = new DirectoryEntries(this.ADsPath, this.conn);
				return this._Children;
			}
		}

		/// <summary>Gets the GUID of the <see cref="T:System.DirectoryServices.DirectoryEntry" />.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that represents the GUID of the <see cref="T:System.DirectoryServices.DirectoryEntry" />.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000208C File Offset: 0x0000028C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DSDescription("A globally unique identifier for this DirectoryEntry")]
		[MonoTODO]
		public Guid Guid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the object as named with the underlying directory service.</summary>
		/// <returns>The name of the object as named with the underlying directory service.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002920 File Offset: 0x00000B20
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DSDescription("The name of the object as named with the underlying directory")]
		[Browsable(false)]
		public string Name
		{
			get
			{
				if (this._Name == null)
				{
					if (!DirectoryEntry.CheckEntry(this.conn, this.ADsPath))
					{
						throw new SystemException("There is no such object on the server");
					}
					this.InitEntry();
				}
				return this._Name;
			}
		}

		/// <summary>Gets this entry's parent in the Active Directory Domain Services hierarchy.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the parent of this entry.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002956 File Offset: 0x00000B56
		[DSDescription("This entry's parent in the Ldap Directory hierarchy.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DirectoryEntry Parent
		{
			get
			{
				if (this._Parent == null)
				{
					if (!DirectoryEntry.CheckEntry(this.conn, this.ADsPath))
					{
						throw new SystemException("There is no such object on the server");
					}
					this.InitEntry();
				}
				return this._Parent;
			}
		}

		/// <summary>Gets the GUID of the <see cref="T:System.DirectoryServices.DirectoryEntry" />, as returned from the provider.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that represents the GUID of the <see cref="T:System.DirectoryServices.DirectoryEntry" />, as returned from the provider.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000208C File Offset: 0x0000028C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DSDescription("The globally unique identifier of the DirectoryEntry, as returned from the provider")]
		[MonoTODO]
		public string NativeGuid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the native Active Directory Service Interfaces (ADSI) object.</summary>
		/// <returns>The native ADSI object.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000208C File Offset: 0x0000028C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DSDescription("The native Active Directory Service Interfaces (ADSI) object.")]
		[Browsable(false)]
		public object NativeObject
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the security descriptor for this entry.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object that represents the security descriptor for this directory entry.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000208C File Offset: 0x0000028C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DSDescription("An ActiveDirectorySecurity object that represents the security descriptor for this directory entry.")]
		[Browsable(false)]
		public ActiveDirectorySecurity ObjectSecurity
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
			[MonoTODO]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the cache should be committed after each operation.</summary>
		/// <returns>true if the cache should not be committed after each operation; otherwise, false. The default is true.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000298C File Offset: 0x00000B8C
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002994 File Offset: 0x00000B94
		[DefaultValue(true)]
		[DSDescription("Determines if a cache should be used.")]
		public bool UsePropertyCache
		{
			get
			{
				return this._usePropertyCache;
			}
			set
			{
				this._usePropertyCache = value;
			}
		}

		/// <summary>Gets the provider-specific options for this entry.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntryConfiguration" /> object that contains the provider-specific options for this entry.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000208C File Offset: 0x0000028C
		[DSDescription("The provider-specific options for this entry.")]
		[Browsable(false)]
		[MonoTODO]
		public DirectoryEntryConfiguration Options
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Sets the password to use when authenticating the client.</summary>
		/// <returns>The password to use when authenticating the client.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000299D File Offset: 0x00000B9D
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000029A5 File Offset: 0x00000BA5
		[DSDescription("The password to use when authenticating the client.")]
		[DefaultValue(null)]
		[Browsable(false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
			}
		}

		/// <summary>Gets or sets the user name to use when authenticating the client.</summary>
		/// <returns>The user name to use when authenticating the client.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000029AE File Offset: 0x00000BAE
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000029B6 File Offset: 0x00000BB6
		[Browsable(false)]
		[DSDescription("The user name to use when authenticating the client.")]
		[DefaultValue(null)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				this._Username = value;
			}
		}

		/// <summary>Gets or sets the path for this <see cref="T:System.DirectoryServices.DirectoryEntry" />.</summary>
		/// <returns>The path of this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object. The default is an empty string ("").</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000029BF File Offset: 0x00000BBF
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000029C7 File Offset: 0x00000BC7
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RecommendedAsConfigurable(true)]
		[DefaultValue("")]
		[DSDescription("The path for this DirectoryEntry.")]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				if (value == null)
				{
					this._Path = string.Empty;
					return;
				}
				this._Path = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000029E0 File Offset: 0x00000BE0
		internal string ADsPath
		{
			get
			{
				if (this.Path == null || this.Path == string.Empty)
				{
					DirectoryEntry directoryEntry = new DirectoryEntry();
					directoryEntry.InitToRootDse(null, -1);
					string text = (string)directoryEntry.Properties["defaultNamingContext"].Value;
					if (text == null)
					{
						text = (string)directoryEntry.Properties["namingContexts"].Value;
					}
					return new LdapUrl(this.DefaultHost, this.DefaultPort, text).ToString();
				}
				return this.Path;
			}
		}

		/// <summary>Gets the Active Directory Domain Services properties for this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.PropertyCollection" /> object that contains the properties that are set on this entry.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002A6C File Offset: 0x00000C6C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DSDescription("Properties set on this object.")]
		public PropertyCollection Properties
		{
			get
			{
				return this.GetProperties(true);
			}
		}

		/// <summary>Gets the name of the schema class for this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object.</summary>
		/// <returns>The name of the schema class for this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002A75 File Offset: 0x00000C75
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DSDescription("The name of the schema used for this DirectoryEntry.")]
		public string SchemaClassName
		{
			get
			{
				if (this._SchemaClassName == null)
				{
					this._SchemaClassName = this.FindAttrValue("structuralObjectClass");
				}
				return this._SchemaClassName;
			}
		}

		/// <summary>Gets the schema object for this entry.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the schema class for this entry.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000208C File Offset: 0x0000028C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DSDescription("The current schema directory entry.")]
		public DirectoryEntry SchemaEntry
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002A98 File Offset: 0x00000C98
		private string DefaultHost
		{
			get
			{
				string text = (string)AppDomain.CurrentDomain.GetData(DirectoryEntry.DEFAULT_LDAP_HOST);
				if (text == null)
				{
					NameValueCollection nameValueCollection = (NameValueCollection)ConfigurationSettings.GetConfig("mainsoft.directoryservices/settings");
					if (nameValueCollection != null)
					{
						text = nameValueCollection["servername"];
					}
					if (text == null)
					{
						text = "localhost";
					}
					AppDomain.CurrentDomain.SetData(DirectoryEntry.DEFAULT_LDAP_HOST, text);
				}
				return text;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private int DefaultPort
		{
			get
			{
				string text = (string)AppDomain.CurrentDomain.GetData(DirectoryEntry.DEFAULT_LDAP_PORT);
				if (text == null)
				{
					NameValueCollection nameValueCollection = (NameValueCollection)ConfigurationSettings.GetConfig("mainsoft.directoryservices/settings");
					if (nameValueCollection != null)
					{
						text = nameValueCollection["port"];
					}
					if (text == null)
					{
						text = "389";
					}
					AppDomain.CurrentDomain.SetData(DirectoryEntry.DEFAULT_LDAP_PORT, text);
				}
				return int.Parse(text);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002B5C File Offset: 0x00000D5C
		private void InitToRootDse(string host, int port)
		{
			if (host == null)
			{
				host = this.DefaultHost;
			}
			if (port < 0)
			{
				port = this.DefaultPort;
			}
			LdapUrl ldapUrl = new LdapUrl(host, port, string.Empty);
			string[] array = new string[] { "+", "*" };
			SearchResult searchResult = new DirectorySearcher(new DirectoryEntry(ldapUrl.ToString(), this.Username, this.Password, this.AuthenticationType), null, array, SearchScope.Base).FindOne();
			PropertyCollection propertyCollection = new PropertyCollection();
			foreach (object obj in searchResult.Properties.PropertyNames)
			{
				string text = (string)obj;
				IEnumerator enumerator2 = searchResult.Properties[text].GetEnumerator();
				if (enumerator2 != null)
				{
					while (enumerator2.MoveNext())
					{
						if (string.Compare(text, "ADsPath", true) != 0)
						{
							propertyCollection[text].Add(enumerator2.Current);
						}
					}
				}
			}
			this.SetProperties(propertyCollection);
			this._Name = "rootDSE";
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002C80 File Offset: 0x00000E80
		private void SetProperties(PropertyCollection pcoll)
		{
			this._Properties = pcoll;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002C8C File Offset: 0x00000E8C
		private PropertyCollection GetProperties(bool forceLoad)
		{
			if (this._Properties == null)
			{
				PropertyCollection propertyCollection = new PropertyCollection(this);
				if (forceLoad && !this.Nflag)
				{
					this.LoadProperties(propertyCollection, null);
				}
				this._Properties = propertyCollection;
			}
			return this._Properties;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002CC8 File Offset: 0x00000EC8
		private void LoadProperties(PropertyCollection properties, string[] propertyNames)
		{
			this._inPropertiesLoading = true;
			try
			{
				LdapSearchResults ldapSearchResults = this.conn.Search(this.Fdn, 0, "objectClass=*", propertyNames, false);
				if (ldapSearchResults.hasMore())
				{
					LdapEntry ldapEntry = ldapSearchResults.next();
					string[] array = null;
					if (propertyNames != null)
					{
						int num = propertyNames.Length;
						array = new string[num];
						for (int i = 0; i < num; i++)
						{
							array[i] = propertyNames[i].ToLower();
						}
					}
					foreach (object obj in ldapEntry.getAttributeSet())
					{
						LdapAttribute ldapAttribute = (LdapAttribute)obj;
						string name = ldapAttribute.Name;
						if (propertyNames == null || Array.IndexOf<string>(array, name.ToLower()) != -1)
						{
							properties[name].Value = null;
							properties[name].AddRange(ldapAttribute.StringValueArray);
							properties[name].Mbit = false;
						}
					}
				}
			}
			finally
			{
				this._inPropertiesLoading = false;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002DE8 File Offset: 0x00000FE8
		private string FindAttrValue(string attrName)
		{
			string text = null;
			string[] array = new string[] { attrName };
			LdapSearchResults ldapSearchResults = this.conn.Search(this.Fdn, 0, "objectClass=*", array, false);
			if (ldapSearchResults.hasMore())
			{
				LdapEntry ldapEntry = null;
				try
				{
					ldapEntry = ldapSearchResults.next();
				}
				catch (LdapException ex)
				{
					throw ex;
				}
				text = ldapEntry.getAttribute(attrName).StringValue;
			}
			return text;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002E54 File Offset: 0x00001054
		private void ModEntry(LdapModification[] mods)
		{
			try
			{
				this.conn.Modify(this.Fdn, mods);
			}
			catch (LdapException ex)
			{
				throw ex;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002E88 File Offset: 0x00001088
		private static bool CheckEntry(LdapConnection lconn, string epath)
		{
			string text = new LdapUrl(epath).getDN();
			if (text == null)
			{
				text = string.Empty;
			}
			else if (string.Compare(text, "rootDSE", true) == 0)
			{
				return true;
			}
			string[] array = new string[] { "objectClass" };
			try
			{
				LdapSearchResults ldapSearchResults = lconn.Search(text, 0, "objectClass=*", array, false);
				while (ldapSearchResults.hasMore())
				{
					try
					{
						ldapSearchResults.next();
						break;
					}
					catch (LdapException ex)
					{
						throw ex;
					}
				}
			}
			catch (LdapException ex2)
			{
				if (ex2.ResultCode == 32)
				{
					return false;
				}
				throw ex2;
			}
			catch (Exception ex3)
			{
				throw ex3;
			}
			return true;
		}

		/// <summary>Closes the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object and releases any system resources that are associated with this component.</summary>
		// Token: 0x06000082 RID: 130 RVA: 0x00002F34 File Offset: 0x00001134
		public void Close()
		{
			if (this._conn != null && this._conn.Connected)
			{
				this._conn.Disconnect();
			}
		}

		/// <summary>Creates a copy of this entry as a child of the specified parent.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the copy of this entry as a child of the new parent.</returns>
		/// <param name="newParent">The distinguished name of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that will be the parent for the copy that is being created.</param>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
		// Token: 0x06000083 RID: 131 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public DirectoryEntry CopyTo(DirectoryEntry newParent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes this entry and its entire subtree from the Active Directory Domain Services hierarchy.</summary>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
		// Token: 0x06000084 RID: 132 RVA: 0x00002F58 File Offset: 0x00001158
		public void DeleteTree()
		{
			foreach (object obj in this.Children)
			{
				DirectoryEntry directoryEntry = (DirectoryEntry)obj;
				this.conn.Delete(directoryEntry.Fdn);
			}
			this.conn.Delete(this.Fdn);
		}

		/// <summary>Determines if the specified path represents an actual entry in the directory service.</summary>
		/// <returns>true if the specified path represents a valid entry in the directory service; otherwise, false.</returns>
		/// <param name="path">The path of the entry to verify.</param>
		// Token: 0x06000085 RID: 133 RVA: 0x00002FAC File Offset: 0x000011AC
		public static bool Exists(string path)
		{
			LdapConnection ldapConnection = new LdapConnection();
			LdapUrl ldapUrl = new LdapUrl(path);
			ldapConnection.Connect(ldapUrl.Host, ldapUrl.Port);
			ldapConnection.Bind("", "");
			return DirectoryEntry.CheckEntry(ldapConnection, path);
		}

		/// <summary>Moves this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object to the specified parent.</summary>
		/// <param name="newParent">The parent to which you want to move this entry.</param>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
		// Token: 0x06000086 RID: 134 RVA: 0x00002FF4 File Offset: 0x000011F4
		public void MoveTo(DirectoryEntry newParent)
		{
			string fdn = this.Parent.Fdn;
			this.conn.Rename(this.Fdn, this.Name, newParent.Fdn, true);
			this.Path = this.Path.Replace(fdn, newParent.Fdn);
			this.RefreshEntry();
		}

		/// <summary>Moves this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object to the specified parent and changes its name to the specified value.</summary>
		/// <param name="newParent">The parent to which you want to move this entry.</param>
		/// <param name="newName">The new name of this entry.</param>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
		// Token: 0x06000087 RID: 135 RVA: 0x0000304C File Offset: 0x0000124C
		public void MoveTo(DirectoryEntry newParent, string newName)
		{
			string fdn = this.Parent.Fdn;
			this.conn.Rename(this.Fdn, newName, newParent.Fdn, true);
			this.Path = this.Path.Replace(fdn, newParent.Fdn).Replace(this.Name, newName);
			this.RefreshEntry();
		}

		/// <summary>Changes the name of this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object.</summary>
		/// <param name="newName">The new name of the entry.</param>
		// Token: 0x06000088 RID: 136 RVA: 0x000030A8 File Offset: 0x000012A8
		public void Rename(string newName)
		{
			string name = this.Name;
			this.conn.Rename(this.Fdn, newName, true);
			this.Path = this.Path.Replace(name, newName);
			this.RefreshEntry();
		}

		/// <summary>Calls a method on the native Active Directory Domain Services object.</summary>
		/// <returns>The return value of the invoked method.</returns>
		/// <param name="methodName">The name of the method to invoke.</param>
		/// <param name="args">An array of type <see cref="T:System.Object" /> objects that contains the arguments of the method to invoke.</param>
		/// <exception cref="T:System.DirectoryServices.DirectoryServicesCOMException">The native method threw a <see cref="T:System.Runtime.InteropServices.COMException" /> exception.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The native method threw a <see cref="T:System.Reflection.TargetInvocationException" /> exception. The <see cref="P:System.Exception.InnerException" /> property contains a <see cref="T:System.Runtime.InteropServices.COMException" /> exception that contains information about the actual error that occurred.</exception>
		// Token: 0x06000089 RID: 137 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public object Invoke(string methodName, params object[] args)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a property from the native Active Directory Domain Services object.</summary>
		/// <returns>An object that represents the requested property.</returns>
		/// <param name="propertyName">The name of the property to get.</param>
		// Token: 0x0600008A RID: 138 RVA: 0x0000208C File Offset: 0x0000028C
		[ComVisible(false)]
		[MonoNotSupported("")]
		public object InvokeGet(string propertyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets a property on the native Active Directory Domain Services object.</summary>
		/// <param name="propertyName">The name of the property to set.</param>
		/// <param name="args">The Active Directory Domain Services object to set.</param>
		// Token: 0x0600008B RID: 139 RVA: 0x0000208C File Offset: 0x0000028C
		[ComVisible(false)]
		[MonoNotSupported("")]
		public void InvokeSet(string propertyName, params object[] args)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a copy of this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object, as a child of the specified parent <see cref="T:System.DirectoryServices.DirectoryEntry" /> object, with the specified new name.</summary>
		/// <returns>A renamed copy of this entry as a child of the specified parent.</returns>
		/// <param name="newParent">The DN of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that will be the parent for the copy that is being created.</param>
		/// <param name="newName">The name of the copy of this entry.</param>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> object is not a container.</exception>
		// Token: 0x0600008C RID: 140 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public DirectoryEntry CopyTo(DirectoryEntry newParent, string newName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves changes that are made to a directory entry to the underlying directory store.</summary>
		// Token: 0x0600008D RID: 141 RVA: 0x000030E8 File Offset: 0x000012E8
		public void CommitChanges()
		{
			if (this.UsePropertyCache)
			{
				this.CommitEntry();
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000030F8 File Offset: 0x000012F8
		private void CommitEntry()
		{
			PropertyCollection properties = this.GetProperties(false);
			if (!this.Nflag)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in properties.PropertyNames)
				{
					string text = (string)obj;
					if (properties[text].Mbit)
					{
						int count = properties[text].Count;
						if (count != 0)
						{
							if (count != 1)
							{
								Array array = (object[])properties[text].Value;
								string[] array2 = new string[properties[text].Count];
								Array.Copy(array, 0, array2, 0, properties[text].Count);
								LdapAttribute ldapAttribute = new LdapAttribute(text, array2);
								arrayList.Add(new LdapModification(2, ldapAttribute));
							}
							else
							{
								string text2 = (string)properties[text].Value;
								LdapAttribute ldapAttribute = new LdapAttribute(text, text2);
								arrayList.Add(new LdapModification(2, ldapAttribute));
							}
						}
						else
						{
							LdapAttribute ldapAttribute = new LdapAttribute(text, new string[0]);
							arrayList.Add(new LdapModification(1, ldapAttribute));
						}
						properties[text].Mbit = false;
					}
				}
				if (arrayList.Count > 0)
				{
					LdapModification[] array3 = new LdapModification[arrayList.Count];
					Type typeFromHandle = typeof(LdapModification);
					array3 = (LdapModification[])arrayList.ToArray(typeFromHandle);
					this.ModEntry(array3);
					return;
				}
			}
			else
			{
				LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
				foreach (object obj2 in properties.PropertyNames)
				{
					string text3 = (string)obj2;
					if (properties[text3].Count == 1)
					{
						string text4 = (string)properties[text3].Value;
						ldapAttributeSet.Add(new LdapAttribute(text3, text4));
					}
					else
					{
						Array array4 = (object[])properties[text3].Value;
						string[] array5 = new string[properties[text3].Count];
						Array.Copy(array4, 0, array5, 0, properties[text3].Count);
						ldapAttributeSet.Add(new LdapAttribute(text3, array5));
					}
				}
				LdapEntry ldapEntry = new LdapEntry(this.Fdn, ldapAttributeSet);
				this.conn.Add(ldapEntry);
				this.Nflag = false;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003380 File Offset: 0x00001580
		internal void CommitDeferred()
		{
			if (!this._inPropertiesLoading && !this.UsePropertyCache && !this.Nflag)
			{
				this.CommitEntry();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000033A0 File Offset: 0x000015A0
		private void RefreshEntry()
		{
			this._Properties = null;
			this._Fdn = null;
			this._Name = null;
			this._Parent = null;
			this._SchemaClassName = null;
			this.InitEntry();
		}

		/// <summary>Loads the property values for this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object into the property cache.</summary>
		// Token: 0x06000091 RID: 145 RVA: 0x000033CC File Offset: 0x000015CC
		public void RefreshCache()
		{
			PropertyCollection propertyCollection = new PropertyCollection();
			this.LoadProperties(propertyCollection, null);
			this.SetProperties(propertyCollection);
		}

		/// <summary>Loads the values of the specified properties into the property cache.</summary>
		/// <param name="propertyNames">An array of the specified properties.</param>
		// Token: 0x06000092 RID: 146 RVA: 0x000033EE File Offset: 0x000015EE
		public void RefreshCache(string[] propertyNames)
		{
			this.LoadProperties(this.GetProperties(false), propertyNames);
		}

		/// <summary>Disposes of the resources (other than memory) that are used by the <see cref="T:System.DirectoryServices.DirectoryEntry" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000093 RID: 147 RVA: 0x000033FE File Offset: 0x000015FE
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003410 File Offset: 0x00001610
		internal static string GetLdapUrlString(string host, int port, string dn)
		{
			LdapUrl ldapUrl;
			if (port == 389)
			{
				ldapUrl = new LdapUrl(host, 0, dn);
			}
			else
			{
				ldapUrl = new LdapUrl(host, port, dn);
			}
			return ldapUrl.ToString();
		}

		// Token: 0x0400005D RID: 93
		private static readonly string DEFAULT_LDAP_HOST = "System.DirectoryServices.DefaultLdapHost";

		// Token: 0x0400005E RID: 94
		private static readonly string DEFAULT_LDAP_PORT = "System.DirectoryServices.DefaultLdapPort";

		// Token: 0x0400005F RID: 95
		private LdapConnection _conn;

		// Token: 0x04000060 RID: 96
		private AuthenticationTypes _AuthenticationType;

		// Token: 0x04000061 RID: 97
		private DirectoryEntries _Children;

		// Token: 0x04000062 RID: 98
		private string _Fdn;

		// Token: 0x04000063 RID: 99
		private string _Path = "";

		// Token: 0x04000064 RID: 100
		private string _Name;

		// Token: 0x04000065 RID: 101
		private DirectoryEntry _Parent;

		// Token: 0x04000066 RID: 102
		private string _Username;

		// Token: 0x04000067 RID: 103
		private string _Password;

		// Token: 0x04000068 RID: 104
		private PropertyCollection _Properties;

		// Token: 0x04000069 RID: 105
		private string _SchemaClassName;

		// Token: 0x0400006A RID: 106
		private bool _Nflag;

		// Token: 0x0400006B RID: 107
		private bool _usePropertyCache = true;

		// Token: 0x0400006C RID: 108
		private bool _inPropertiesLoading;
	}
}
