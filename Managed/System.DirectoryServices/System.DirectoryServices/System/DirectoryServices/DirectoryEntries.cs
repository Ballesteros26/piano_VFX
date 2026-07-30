using System;
using System.Collections;
using Novell.Directory.Ldap;
using Unity;

namespace System.DirectoryServices
{
	/// <summary>Contains a collection of <see cref="T:System.DirectoryServices.DirectoryEntry" /> objects.</summary>
	// Token: 0x02000013 RID: 19
	public class DirectoryEntries : IEnumerable
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002280 File Offset: 0x00000480
		private void InitBlock()
		{
			try
			{
				LdapUrl ldapUrl = new LdapUrl(this._Bpath);
				this._Conn = new LdapConnection();
				this._Conn.Connect(ldapUrl.Host, ldapUrl.Port);
				this._Conn.Bind(this._Buser, this._Bpass);
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000022F4 File Offset: 0x000004F4
		internal string Basedn
		{
			get
			{
				if (this._Basedn == null)
				{
					string dn = new LdapUrl(this._Bpath).getDN();
					if (dn != null)
					{
						this._Basedn = dn;
					}
					else
					{
						this._Basedn = "";
					}
				}
				return this._Basedn;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002337 File Offset: 0x00000537
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000233F File Offset: 0x0000053F
		internal string Bpath
		{
			get
			{
				return this._Bpath;
			}
			set
			{
				this._Bpath = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002348 File Offset: 0x00000548
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000235E File Offset: 0x0000055E
		internal LdapConnection Conn
		{
			get
			{
				if (this._Conn == null)
				{
					this.InitBlock();
				}
				return this._Conn;
			}
			set
			{
				this._Conn = value;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002367 File Offset: 0x00000567
		internal DirectoryEntries(string path, string uname, string passwd)
		{
			this._Bpath = path;
			this._Buser = uname;
			this._Bpass = passwd;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002384 File Offset: 0x00000584
		internal DirectoryEntries(string path, LdapConnection lc)
		{
			this._Bpath = path;
			this._Conn = lc;
		}

		/// <summary>Gets the schemas that specify which child objects are contained in the collection.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.SchemaNameCollection" /> that specifies which child objects are contained in the <see cref="T:System.DirectoryServices.DirectoryEntries" /> instance.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000239A File Offset: 0x0000059A
		public SchemaNameCollection SchemaFilter
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException("System.DirectoryServices.DirectoryEntries.SchemaFilter");
			}
		}

		/// <summary> Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600004E RID: 78 RVA: 0x000023A8 File Offset: 0x000005A8
		public IEnumerator GetEnumerator()
		{
			this.m_oValues = new ArrayList();
			string[] array = new string[] { "objectClass" };
			LdapSearchResults ldapSearchResults = this.Conn.Search(this.Basedn, 1, "objectClass=*", array, false);
			LdapUrl ldapUrl = new LdapUrl(this._Bpath);
			string host = ldapUrl.Host;
			int port = ldapUrl.Port;
			while (ldapSearchResults.hasMore())
			{
				LdapEntry ldapEntry = null;
				try
				{
					ldapEntry = ldapSearchResults.next();
				}
				catch (LdapException)
				{
					continue;
				}
				DirectoryEntry directoryEntry = new DirectoryEntry(this.Conn);
				string dn = ldapEntry.DN;
				LdapUrl ldapUrl2 = new LdapUrl(host, port, dn);
				directoryEntry.Path = ldapUrl2.ToString();
				this.m_oValues.Add(directoryEntry);
			}
			return this.m_oValues.GetEnumerator();
		}

		/// <summary>Creates a new entry in the container.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the new entry.</returns>
		/// <param name="name"> The name of the new entry.</param>
		/// <param name="schemaClassName">The name of the schema that is used for the new entry.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		// Token: 0x0600004F RID: 79 RVA: 0x00002474 File Offset: 0x00000674
		public DirectoryEntry Add(string name, string schemaClassName)
		{
			DirectoryEntry directoryEntry = new DirectoryEntry(this.Conn);
			LdapUrl ldapUrl = new LdapUrl(this._Bpath);
			string dn = ldapUrl.getDN();
			string text = ((dn != null && dn.Length != 0) ? (name + "," + dn) : name);
			LdapUrl ldapUrl2 = new LdapUrl(ldapUrl.Host, ldapUrl.Port, text);
			directoryEntry.Path = ldapUrl2.ToString();
			directoryEntry.Nflag = true;
			return directoryEntry;
		}

		/// <summary>Deletes a member of this collection.</summary>
		/// <param name="entry">The name of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object to delete.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		// Token: 0x06000050 RID: 80 RVA: 0x000024E0 File Offset: 0x000006E0
		public void Remove(DirectoryEntry entry)
		{
			LdapUrl ldapUrl = new LdapUrl(this._Bpath);
			string text = entry.Name + "," + ldapUrl.getDN();
			this.Conn.Delete(text);
		}

		/// <summary>Returns the member of this collection with the specified name.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> that represents the child object that was found.</returns>
		/// <param name="name">Contains the name of the child object for which to search.</param>
		/// <exception cref="T:System.InvalidOperationException">The Active Directory Domain Services object is not a container.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		// Token: 0x06000051 RID: 81 RVA: 0x0000251C File Offset: 0x0000071C
		public DirectoryEntry Find(string name)
		{
			return this.CheckEntry(name);
		}

		/// <summary>Returns the member of this collection with the specified name and of the specified type.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the child object that was found.</returns>
		/// <param name="name">The name of the child directory object for which to search.</param>
		/// <param name="schemaClassName">The class name of the child directory object for which to search.</param>
		/// <exception cref="T:System.InvalidOperationException">The Active Directory Domain Services object is not a container.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		// Token: 0x06000052 RID: 82 RVA: 0x00002528 File Offset: 0x00000728
		public DirectoryEntry Find(string name, string schemaClassName)
		{
			DirectoryEntry directoryEntry = this.CheckEntry(name);
			if (directoryEntry == null)
			{
				return directoryEntry;
			}
			if (directoryEntry.Properties["objectclass"].ContainsCaselessStringValue(schemaClassName))
			{
				return directoryEntry;
			}
			throw new SystemException("An unknown directory object was requested");
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002568 File Offset: 0x00000768
		private DirectoryEntry CheckEntry(string rdn)
		{
			DirectoryEntry directoryEntry = null;
			string text = rdn + "," + this.Basedn;
			string[] array = new string[] { "objectClass" };
			try
			{
				LdapSearchResults ldapSearchResults = this.Conn.Search(text, 0, "objectClass=*", array, false);
				while (ldapSearchResults.hasMore())
				{
					try
					{
						ldapSearchResults.next();
						directoryEntry = new DirectoryEntry(this.Conn);
						LdapUrl ldapUrl = new LdapUrl(this._Bpath);
						LdapUrl ldapUrl2 = new LdapUrl(ldapUrl.Host, ldapUrl.Port, text);
						directoryEntry.Path = ldapUrl2.ToString();
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
					return null;
				}
				throw ex2;
			}
			catch (Exception ex3)
			{
				throw ex3;
			}
			return directoryEntry;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002644 File Offset: 0x00000844
		internal DirectoryEntries()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000057 RID: 87
		private LdapConnection _Conn;

		// Token: 0x04000058 RID: 88
		private string _Bpath;

		// Token: 0x04000059 RID: 89
		private string _Buser;

		// Token: 0x0400005A RID: 90
		private string _Bpass;

		// Token: 0x0400005B RID: 91
		private string _Basedn;

		// Token: 0x0400005C RID: 92
		private ArrayList m_oValues;
	}
}
