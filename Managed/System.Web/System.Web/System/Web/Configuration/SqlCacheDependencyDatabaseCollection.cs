using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005DB RID: 1499
	[ConfigurationCollection(typeof(SqlCacheDependencyDatabase), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class SqlCacheDependencyDatabaseCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object already exists in the collection or the collection is read only.</exception>
		// Token: 0x060040D5 RID: 16597 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(SqlCacheDependencyDatabase name)
		{
			this.BaseAdd(name);
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects from the collection.</summary>
		// Token: 0x060040D6 RID: 16598 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element to retrieve.</param>
		// Token: 0x060040D7 RID: 16599 RVA: 0x000AA9A0 File Offset: 0x000A8BA0
		public SqlCacheDependencyDatabase Get(string name)
		{
			return (SqlCacheDependencyDatabase)base.BaseGet(name);
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element to retrieve.</param>
		// Token: 0x060040D8 RID: 16600 RVA: 0x000AA9AE File Offset: 0x000A8BAE
		public SqlCacheDependencyDatabase Get(int index)
		{
			return (SqlCacheDependencyDatabase)base.BaseGet(index);
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x000AA9BC File Offset: 0x000A8BBC
		protected override ConfigurationElement CreateNewElement()
		{
			return new SqlCacheDependencyDatabase();
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x000AA9C3 File Offset: 0x000A8BC3
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SqlCacheDependencyDatabase)element).Name;
		}

		/// <summary>Returns the key for the element located at the specified index in the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" />.</summary>
		/// <returns>The key at the specified index.</returns>
		/// <param name="index">The index of the key to return.</param>
		// Token: 0x060040DB RID: 16603 RVA: 0x000AA9D0 File Offset: 0x000A8BD0
		public string GetKey(int index)
		{
			SqlCacheDependencyDatabase sqlCacheDependencyDatabase = this.Get(index);
			if (sqlCacheDependencyDatabase == null)
			{
				return null;
			}
			return sqlCacheDependencyDatabase.Name;
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to remove from the collection.</param>
		// Token: 0x060040DC RID: 16604 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to remove from the collection.</param>
		// Token: 0x060040DD RID: 16605 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Resets a specified <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object that exists within the collection. </summary>
		/// <param name="user">The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> element to reset. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> is read-only or already exists.</exception>
		// Token: 0x060040DE RID: 16606 RVA: 0x000AA9F0 File Offset: 0x000A8BF0
		public void Set(SqlCacheDependencyDatabase user)
		{
			SqlCacheDependencyDatabase sqlCacheDependencyDatabase = this.Get(user.Name);
			if (sqlCacheDependencyDatabase == null)
			{
				this.Add(user);
				return;
			}
			int num = base.BaseIndexOf(sqlCacheDependencyDatabase);
			this.RemoveAt(num);
			this.BaseAdd(num, user);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> keys.</summary>
		/// <returns>The string array containing the collection keys.</returns>
		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x000AAA2C File Offset: 0x000A8C2C
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Name;
				}
				return array;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object at the specified index.</returns>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object.</param>
		// Token: 0x17001486 RID: 5254
		public SqlCacheDependencyDatabase this[int index]
		{
			get
			{
				return (SqlCacheDependencyDatabase)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object.</param>
		// Token: 0x17001487 RID: 5255
		public SqlCacheDependencyDatabase this[string name]
		{
			get
			{
				return (SqlCacheDependencyDatabase)base.BaseGet(name);
			}
		}
	}
}
