using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using Novell.Directory.Ldap;
using Unity;

namespace System.DirectoryServices
{
	/// <summary>Performs queries against Active Directory Domain Services.</summary>
	// Token: 0x02000015 RID: 21
	public class DirectorySearcher : Component
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003455 File Offset: 0x00001655
		internal SearchResultCollection SrchColl
		{
			get
			{
				if (this._SrchColl == null)
				{
					this._SrchColl = new SearchResultCollection();
					this.DoSearch();
				}
				return this._SrchColl;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003478 File Offset: 0x00001678
		private void InitBlock()
		{
			this._conn = new LdapConnection();
			LdapUrl ldapUrl = new LdapUrl(this.SearchRoot.ADsPath);
			this._Host = ldapUrl.Host;
			this._Port = ldapUrl.Port;
			this._conn.Connect(this._Host, this._Port);
			this._conn.Bind(this.SearchRoot.Username, this.SearchRoot.Password, (AuthenticationTypes)this.SearchRoot.AuthenticationType);
		}

		/// <summary>Gets or sets a value indicating whether the result is cached on the client computer.</summary>
		/// <returns>true if the result is cached on the client computer; otherwise, false. The default is true.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000034FC File Offset: 0x000016FC
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003504 File Offset: 0x00001704
		[DefaultValue(true)]
		[DSDescription("The cacheability of results.")]
		public bool CacheResults
		{
			get
			{
				return this._CacheResults;
			}
			set
			{
				this._CacheResults = value;
			}
		}

		/// <summary>Gets or sets the maximum amount of time that the client waits for the server to return results. If the server does not respond within this time, the search is aborted and no results are returned.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> structure that contains the maximum amount of time for the client to wait for the server to return results.The default value is -1 second, which means to wait indefinitely.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000350D File Offset: 0x0000170D
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003515 File Offset: 0x00001715
		[DSDescription("The maximum amount of time that the client waits for the server to return results.")]
		public TimeSpan ClientTimeout
		{
			get
			{
				return this._ClientTimeout;
			}
			set
			{
				this._ClientTimeout = value;
			}
		}

		/// <summary>Gets or sets a value indicating the Lightweight Directory Access Protocol (LDAP) format filter string.</summary>
		/// <returns>The search filter string in LDAP format, such as "(objectClass=user)". The default is "(objectClass=*)", which retrieves all objects.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000351E File Offset: 0x0000171E
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003526 File Offset: 0x00001726
		[DefaultValue("(objectClass=*)")]
		[DSDescription("The Lightweight Directory Access Protocol (Ldap) format filter string.")]
		[RecommendedAsConfigurable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Filter
		{
			get
			{
				return this._Filter;
			}
			set
			{
				this._Filter = value;
				this.ClearCachedResults();
			}
		}

		/// <summary>Gets or sets a value indicating the page size in a paged search.</summary>
		/// <returns>The maximum number of objects the server can return in a paged search. The default is zero, which means do not do a paged search.</returns>
		/// <exception cref="T:System.ArgumentException">The new value is less than zero.</exception>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003535 File Offset: 0x00001735
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000353D File Offset: 0x0000173D
		[DefaultValue(0)]
		[DSDescription("The page size in a paged search.")]
		public int PageSize
		{
			get
			{
				return this._PageSize;
			}
			set
			{
				this._PageSize = value;
			}
		}

		/// <summary>Gets a value indicating the list of properties to retrieve during the search.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> object that contains the set of properties to retrieve during the search.The default is an empty <see cref="T:System.Collections.Specialized.StringCollection" />, which retrieves all properties.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003546 File Offset: 0x00001746
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DSDescription("The set of properties retrieved during the search.")]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public StringCollection PropertiesToLoad
		{
			get
			{
				return this._PropertiesToLoad;
			}
		}

		/// <summary>Gets or sets a value indicating whether the search retrieves only the names of attributes to which values have been assigned.</summary>
		/// <returns>true if the search obtains only the names of attributes to which values have been assigned; false if the search obtains the names and values for all the requested attributes. The default value is false.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000354E File Offset: 0x0000174E
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003556 File Offset: 0x00001756
		[DefaultValue(false)]
		[DSDescription("A value indicating whether the search retrieves only the names of attributes to which values have been assigned.")]
		public bool PropertyNamesOnly
		{
			get
			{
				return this._PropertyNamesOnly;
			}
			set
			{
				this._PropertyNamesOnly = value;
			}
		}

		/// <summary>Gets or sets a value indicating how referrals are chased.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.ReferralChasingOption" /> values. The default is <see cref="F:System.DirectoryServices.ReferralChasingOption.External" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.DirectoryServices.ReferralChasingOption" /> values.</exception>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000355F File Offset: 0x0000175F
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003567 File Offset: 0x00001767
		[DSDescription("How referrals are chased.")]
		[DefaultValue(ReferralChasingOption.External)]
		public ReferralChasingOption ReferralChasing
		{
			get
			{
				return this._ReferralChasing;
			}
			set
			{
				this._ReferralChasing = value;
			}
		}

		/// <summary>Gets or sets a value indicating the node in the Active Directory Domain Services hierarchy where the search starts.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> object in the Active Directory Domain Services hierarchy where the search starts. The default is a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003570 File Offset: 0x00001770
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003578 File Offset: 0x00001778
		[DSDescription("The node in the Ldap Directory hierarchy where the search starts.")]
		[DefaultValue(null)]
		public DirectoryEntry SearchRoot
		{
			get
			{
				return this._SearchRoot;
			}
			set
			{
				this._SearchRoot = value;
				this.ClearCachedResults();
			}
		}

		/// <summary>Gets or sets a value indicating the scope of the search that is observed by the server.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.SearchScope" /> values. The default is <see cref="F:System.DirectoryServices.SearchScope.Subtree" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.DirectoryServices.ReferralChasingOption" /> values.</exception>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003587 File Offset: 0x00001787
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000358F File Offset: 0x0000178F
		[DSDescription("The scope of the search that is observed by the server.")]
		[RecommendedAsConfigurable(true)]
		[DefaultValue(SearchScope.Subtree)]
		public SearchScope SearchScope
		{
			get
			{
				return this._SearchScope;
			}
			set
			{
				this._SearchScope = value;
				this.ClearCachedResults();
			}
		}

		/// <summary>Gets or sets a value indicating the maximum amount of time the server should search for an individual page of results. This is not the same as the time limit for the entire search.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that represents the amount of time the server should search for a page of results.The default value is -1 seconds, which means to search indefinitely.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000359E File Offset: 0x0000179E
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000035A6 File Offset: 0x000017A6
		[DSDescription("The time limit the server should observe to search an individual page of results.")]
		public TimeSpan ServerPageTimeLimit
		{
			get
			{
				return this._ServerPageTimeLimit;
			}
			set
			{
				this._ServerPageTimeLimit = value;
			}
		}

		/// <summary>The <see cref="P:System.DirectoryServices.DirectorySearcher.ServerTimeLimit" /> property gets or sets a value indicating the maximum amount of time the server spends searching. If the time limit is reached, only entries that are found up to that point are returned.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that represents the amount of time that the server should search.The default value is -1 seconds, which means to use the server-determined default of 120 seconds.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000035AF File Offset: 0x000017AF
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000035B7 File Offset: 0x000017B7
		[DSDescription("The time limit the server should observe to search.")]
		public TimeSpan ServerTimeLimit
		{
			[MonoTODO]
			get
			{
				return this._serverTimeLimit;
			}
			[MonoTODO]
			set
			{
				this._serverTimeLimit = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum number of objects that the server returns in a search.</summary>
		/// <returns>The maximum number of objects that the server returns in a search. The default value is zero, which means to use the server-determined default size limit of 1000 entries.</returns>
		/// <exception cref="T:System.ArgumentException">The new value is less than zero.</exception>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000035C8 File Offset: 0x000017C8
		[DSDescription("The maximum number of objects the server returns in a search.")]
		[DefaultValue(0)]
		public int SizeLimit
		{
			get
			{
				return this._SizeLimit;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException();
				}
				this._SizeLimit = value;
			}
		}

		/// <summary>Gets or sets a value indicating the property on which the results are sorted.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.SortOption" /> object that specifies the property and direction that the search results should be sorted on.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is null (Nothing in Visual Basic).</exception>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000208C File Offset: 0x0000028C
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DSDescription("An object that defines how the data should be sorted.")]
		public SortOption Sort
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

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with default values.</summary>
		// Token: 0x060000B1 RID: 177 RVA: 0x000035DC File Offset: 0x000017DC
		public DirectorySearcher()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class using the specified search root.</summary>
		/// <param name="searchRoot">The node in the Active Directory Domain Services hierarchy where the search starts. The <see cref="P:System.DirectoryServices.DirectorySearcher.SearchRoot" /> property is initialized to this value.</param>
		// Token: 0x060000B2 RID: 178 RVA: 0x00003648 File Offset: 0x00001848
		public DirectorySearcher(DirectoryEntry searchRoot)
		{
			this._SearchRoot = searchRoot;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search filter.  </summary>
		/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
		// Token: 0x060000B3 RID: 179 RVA: 0x000036BC File Offset: 0x000018BC
		public DirectorySearcher(string filter)
		{
			this._Filter = filter;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search root and search filter.  </summary>
		/// <param name="searchRoot">The node in the Active Directory Domain Services hierarchy where the search starts. The <see cref="P:System.DirectoryServices.DirectorySearcher.SearchRoot" /> property is initialized to this value.</param>
		/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
		// Token: 0x060000B4 RID: 180 RVA: 0x00003730 File Offset: 0x00001930
		public DirectorySearcher(DirectoryEntry searchRoot, string filter)
		{
			this._SearchRoot = searchRoot;
			this._Filter = filter;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search filter and properties to retrieve. </summary>
		/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The  <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
		/// <param name="propertiesToLoad">The set of properties to retrieve during the search. The <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property is initialized to this value.</param>
		// Token: 0x060000B5 RID: 181 RVA: 0x000037AC File Offset: 0x000019AC
		public DirectorySearcher(string filter, string[] propertiesToLoad)
		{
			this._Filter = filter;
			this.PropertiesToLoad.AddRange(propertiesToLoad);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search root, search filter, and properties to retrieve..</summary>
		/// <param name="searchRoot">The node in the Active Directory Domain Services hierarchy where the search starts. The <see cref="P:System.DirectoryServices.DirectorySearcher.SearchRoot" /> property is initialized to this value.</param>
		/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
		/// <param name="propertiesToLoad">The set of properties that are retrieved during the search. The <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property is initialized to this value.</param>
		// Token: 0x060000B6 RID: 182 RVA: 0x0000382C File Offset: 0x00001A2C
		public DirectorySearcher(DirectoryEntry searchRoot, string filter, string[] propertiesToLoad)
		{
			this._SearchRoot = searchRoot;
			this._Filter = filter;
			this.PropertiesToLoad.AddRange(propertiesToLoad);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search filter, properties to retrieve, and search scope. </summary>
		/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
		/// <param name="propertiesToLoad">The set of properties to retrieve during the search. The <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property is initialized to this value.</param>
		/// <param name="scope">The scope of the search that is observed by the server. The <see cref="T:System.DirectoryServices.SearchScope" /> property is initialized to this value.</param>
		// Token: 0x060000B7 RID: 183 RVA: 0x000038B4 File Offset: 0x00001AB4
		public DirectorySearcher(string filter, string[] propertiesToLoad, SearchScope scope)
		{
			this._SearchScope = scope;
			this._Filter = filter;
			this.PropertiesToLoad.AddRange(propertiesToLoad);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search root, search filter, properties to retrieve, and search scope. </summary>
		/// <param name="searchRoot">The node in the Active Directory Domain Services hierarchy where the search starts. The <see cref="P:System.DirectoryServices.DirectorySearcher.SearchRoot" /> property is initialized to this value.</param>
		/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
		/// <param name="propertiesToLoad">The set of properties to retrieve during the search. The <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property is initialized to this value.</param>
		/// <param name="scope">The scope of the search that is observed by the server. The <see cref="T:System.DirectoryServices.SearchScope" /> property is initialized to this value.</param>
		// Token: 0x060000B8 RID: 184 RVA: 0x0000393C File Offset: 0x00001B3C
		public DirectorySearcher(DirectoryEntry searchRoot, string filter, string[] propertiesToLoad, SearchScope scope)
		{
			this._SearchRoot = searchRoot;
			this._SearchScope = scope;
			this._Filter = filter;
			this.PropertiesToLoad.AddRange(propertiesToLoad);
		}

		/// <summary>Executes the search and returns only the first entry that is found.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.SearchResult" /> object that contains the first entry that is found during the search.</returns>
		// Token: 0x060000B9 RID: 185 RVA: 0x000039C9 File Offset: 0x00001BC9
		public SearchResult FindOne()
		{
			if (this.SrchColl.Count == 0)
			{
				return null;
			}
			return this.SrchColl[0];
		}

		/// <summary>Executes the search and returns a collection of the entries that are found.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.SearchResultCollection" /> object that contains the results of the search.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
		/// <exception cref="T:System.NotSupportedException">Searching is not supported by the provider that is being used.</exception>
		// Token: 0x060000BA RID: 186 RVA: 0x000039E6 File Offset: 0x00001BE6
		public SearchResultCollection FindAll()
		{
			return this.SrchColl;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000039F0 File Offset: 0x00001BF0
		private void DoSearch()
		{
			this.InitBlock();
			string[] array = new string[this.PropertiesToLoad.Count];
			this.PropertiesToLoad.CopyTo(array, 0);
			LdapSearchConstraints searchConstraints = this._conn.SearchConstraints;
			if (this.SizeLimit > 0)
			{
				searchConstraints.MaxResults = this.SizeLimit;
			}
			if (this.ServerTimeLimit != DirectorySearcher.DefaultTimeSpan)
			{
				searchConstraints.ServerTimeLimit = (int)this.ServerTimeLimit.TotalSeconds;
			}
			int num;
			switch (this._SearchScope)
			{
			case SearchScope.Base:
				num = 0;
				break;
			case SearchScope.OneLevel:
				num = 1;
				break;
			case SearchScope.Subtree:
				num = 2;
				break;
			default:
				num = 2;
				break;
			}
			LdapSearchResults ldapSearchResults = this._conn.Search(this.SearchRoot.Fdn, num, this.Filter, array, this.PropertyNamesOnly, searchConstraints);
			while (ldapSearchResults.hasMore())
			{
				LdapEntry ldapEntry = null;
				try
				{
					ldapEntry = ldapSearchResults.next();
				}
				catch (LdapException ex)
				{
					int resultCode = ex.ResultCode;
					if (resultCode - 3 <= 1 || resultCode == 10)
					{
						continue;
					}
					throw ex;
				}
				DirectoryEntry directoryEntry = new DirectoryEntry(this._conn);
				PropertyCollection propertyCollection = new PropertyCollection();
				directoryEntry.Path = DirectoryEntry.GetLdapUrlString(this._Host, this._Port, ldapEntry.DN);
				IEnumerator enumerator = ldapEntry.getAttributeSet().GetEnumerator();
				if (enumerator != null)
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						LdapAttribute ldapAttribute = (LdapAttribute)obj;
						string name = ldapAttribute.Name;
						propertyCollection[name].AddRange(ldapAttribute.StringValueArray);
					}
				}
				if (!propertyCollection.Contains("ADsPath"))
				{
					propertyCollection["ADsPath"].Add(directoryEntry.Path);
				}
				this._SrchColl.Add(new SearchResult(directoryEntry, propertyCollection));
			}
		}

		/// <summary>Releases the managed resources that are used by the <see cref="T:System.DirectoryServices.DirectorySearcher" /> object and, optionally, releases unmanaged resources.</summary>
		/// <param name="disposing">true if this method releases both managed and unmanaged resources; false if it releases only unmanaged resources.</param>
		// Token: 0x060000BC RID: 188 RVA: 0x00003BC4 File Offset: 0x00001DC4
		[MonoTODO]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._conn != null && this._conn.Connected)
			{
				this._conn.Disconnect();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003BF0 File Offset: 0x00001DF0
		private void ClearCachedResults()
		{
			this._SrchColl = null;
		}

		/// <summary>Gets or sets a value that indicates if the search is performed asynchronously.</summary>
		/// <returns>true if the search is asynchronous; false otherwise.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003C0C File Offset: 0x00001E0C
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002644 File Offset: 0x00000844
		public bool Asynchronous
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the LDAP display name of the distinguished name attribute to search in. Only one attribute can be used for this type of search.</summary>
		/// <returns>The LDAP display name of the attribute to perform the search against, or an empty string of no attribute scope query is set.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectorySearcher.SearchScope" /> property is set to a value other than <see cref="F:System.DirectoryServices.SearchScope.Base" />.</exception>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003C27 File Offset: 0x00001E27
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00002644 File Offset: 0x00000844
		public string AttributeScopeQuery
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value indicating how the aliases of objects that are found during a search should be resolved.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DereferenceAlias" /> value that specifies the behavior in which aliases are dereferenced. The default setting for this property is <see cref="F:System.DirectoryServices.DereferenceAlias.Never" />.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003C30 File Offset: 0x00001E30
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002644 File Offset: 0x00000844
		public DereferenceAlias DerefAlias
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return DereferenceAlias.Never;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets an object that represents the directory synchronization control to use with the search.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object for the search. null if the directory synchronization control should not be used.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003C27 File Offset: 0x00001E27
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002644 File Offset: 0x00000844
		public DirectorySynchronization DirectorySynchronization
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates the format of the distinguished names.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.ExtendedDN" /> values.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003C4C File Offset: 0x00001E4C
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002644 File Offset: 0x00000844
		public ExtendedDN ExtendedDN
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return ExtendedDN.HexString;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value indicating which security access information for the specified attributes should be returned by the search.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.SecurityMasks" /> values.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003C68 File Offset: 0x00001E68
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00002644 File Offset: 0x00000844
		public SecurityMasks SecurityMasks
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return SecurityMasks.None;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the search should also return deleted objects that match the search filter.</summary>
		/// <returns>true if deleted objects should be included in the search; false otherwise. The default value is false.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003C84 File Offset: 0x00001E84
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00002644 File Offset: 0x00000844
		public bool Tombstone
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value indicating the virtual list view options for the search.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> object that contains the virtual list view search information. The default value for this property is null, which means do not use the virtual list view search option.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003C27 File Offset: 0x00001E27
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00002644 File Offset: 0x00000844
		public DirectoryVirtualListView VirtualListView
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0400006D RID: 109
		private static readonly TimeSpan DefaultTimeSpan = new TimeSpan(-10000000L);

		// Token: 0x0400006E RID: 110
		private DirectoryEntry _SearchRoot;

		// Token: 0x0400006F RID: 111
		private bool _CacheResults = true;

		// Token: 0x04000070 RID: 112
		private TimeSpan _ClientTimeout = DirectorySearcher.DefaultTimeSpan;

		// Token: 0x04000071 RID: 113
		private string _Filter = "(objectClass=*)";

		// Token: 0x04000072 RID: 114
		private int _PageSize;

		// Token: 0x04000073 RID: 115
		private StringCollection _PropertiesToLoad = new StringCollection();

		// Token: 0x04000074 RID: 116
		private bool _PropertyNamesOnly;

		// Token: 0x04000075 RID: 117
		private ReferralChasingOption _ReferralChasing = ReferralChasingOption.External;

		// Token: 0x04000076 RID: 118
		private SearchScope _SearchScope = SearchScope.Subtree;

		// Token: 0x04000077 RID: 119
		private TimeSpan _ServerPageTimeLimit = DirectorySearcher.DefaultTimeSpan;

		// Token: 0x04000078 RID: 120
		private TimeSpan _serverTimeLimit = DirectorySearcher.DefaultTimeSpan;

		// Token: 0x04000079 RID: 121
		private int _SizeLimit;

		// Token: 0x0400007A RID: 122
		private LdapConnection _conn;

		// Token: 0x0400007B RID: 123
		private string _Host;

		// Token: 0x0400007C RID: 124
		private int _Port = 389;

		// Token: 0x0400007D RID: 125
		private SearchResultCollection _SrchColl;
	}
}
