using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.OutputCacheProfile" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005C0 RID: 1472
	[ConfigurationCollection(typeof(OutputCacheProfile), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class OutputCacheProfileCollection : ConfigurationElementCollection, ICollection, IEnumerable
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object to the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object already exists in the collection or the collection is read only.</exception>
		// Token: 0x06003F22 RID: 16162 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(OutputCacheProfile name)
		{
			this.BaseAdd(name);
		}

		/// <summary>Removes all the  <see cref="T:System.Web.Configuration.OutputCacheProfile" /> objects from the collection.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationException">The collection is read only.</exception>
		// Token: 0x06003F23 RID: 16163 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x000A7191 File Offset: 0x000A5391
		protected override ConfigurationElement CreateNewElement()
		{
			return new OutputCacheProfile();
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x000A7198 File Offset: 0x000A5398
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((OutputCacheProfile)element).Name;
		}

		/// <summary>Gets the key at the specified <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> index.</summary>
		/// <returns>The key with the specified <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> index.</returns>
		/// <param name="index">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> index of the key. </param>
		// Token: 0x06003F26 RID: 16166 RVA: 0x000A09E6 File Offset: 0x0009EBE6
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> element.</param>
		// Token: 0x06003F27 RID: 16167 RVA: 0x000A71A5 File Offset: 0x000A53A5
		public OutputCacheProfile Get(string name)
		{
			return (OutputCacheProfile)base.BaseGet(name);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> element. </param>
		// Token: 0x06003F28 RID: 16168 RVA: 0x000A71B3 File Offset: 0x000A53B3
		public OutputCacheProfile Get(int index)
		{
			return (OutputCacheProfile)base.BaseGet(index);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element to remove from the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003F29 RID: 16169 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003F2A RID: 16170 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Sets the specified <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object. </summary>
		/// <param name="user">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> element to set.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> is read-only.</exception>
		// Token: 0x06003F2B RID: 16171 RVA: 0x000A71C4 File Offset: 0x000A53C4
		public void Set(OutputCacheProfile user)
		{
			OutputCacheProfile outputCacheProfile = this.Get(user.Name);
			if (outputCacheProfile == null)
			{
				this.Add(user);
				return;
			}
			int num = base.BaseIndexOf(outputCacheProfile);
			this.RemoveAt(num);
			this.BaseAdd(num, user);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> keys.</summary>
		/// <returns>The string array containing the collection keys.</returns>
		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x000A7200 File Offset: 0x000A5400
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

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> at the specified index.</returns>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object </param>
		// Token: 0x170013D6 RID: 5078
		public OutputCacheProfile this[int index]
		{
			get
			{
				return (OutputCacheProfile)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object.</param>
		// Token: 0x170013D7 RID: 5079
		public OutputCacheProfile this[string name]
		{
			get
			{
				return (OutputCacheProfile)base.BaseGet(name);
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x000A723A File Offset: 0x000A543A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheProfileCollection.properties;
			}
		}

		// Token: 0x0400226E RID: 8814
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
