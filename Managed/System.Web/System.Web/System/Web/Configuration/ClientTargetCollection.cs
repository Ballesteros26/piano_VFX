using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.ClientTarget" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200058F RID: 1423
	[ConfigurationCollection(typeof(ClientTarget), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ClientTargetCollection : ConfigurationElementCollection
	{
		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.ClientTarget" /> object to the collection.</summary>
		/// <param name="clientTarget">The <see cref="T:System.Web.Configuration.ClientTarget" /> to add to the collection.</param>
		// Token: 0x06003C22 RID: 15394 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(ClientTarget clientTarget)
		{
			this.BaseAdd(clientTarget);
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.ClientTarget" /> objects from the collection.</summary>
		// Token: 0x06003C23 RID: 15395 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x000A09D0 File Offset: 0x0009EBD0
		protected override ConfigurationElement CreateNewElement()
		{
			return new ClientTarget(null, null);
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x000A09D9 File Offset: 0x0009EBD9
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ClientTarget)element).Alias;
		}

		/// <summary>Gets the collection key for the specified element.</summary>
		/// <returns>A string containing the value of the key.</returns>
		/// <param name="index">The collection index of the element to get.</param>
		// Token: 0x06003C26 RID: 15398 RVA: 0x000A09E6 File Offset: 0x0009EBE6
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified alias from the collection.</summary>
		/// <param name="name">The alias of the <see cref="T:System.Web.Configuration.ClientTarget" /> to remove.</param>
		// Token: 0x06003C27 RID: 15399 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.ClientTarget" /> object from the collection.</summary>
		/// <param name="clientTarget">The <see cref="T:System.Web.Configuration.ClientTarget" /> to remove.</param>
		// Token: 0x06003C28 RID: 15400 RVA: 0x000A09F4 File Offset: 0x0009EBF4
		public void Remove(ClientTarget clientTarget)
		{
			base.BaseRemove(clientTarget.Alias);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified collection index.</summary>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.ClientTarget" /> to remove.</param>
		// Token: 0x06003C29 RID: 15401 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Returns an array of the keys for all the configuration elements contained in the <see cref="T:System.Web.Configuration.ClientTargetCollection" /> collection.</summary>
		/// <returns>The array of the keys for all of the <see cref="T:System.Web.Configuration.ClientTarget" /> objects contained in the <see cref="T:System.Web.Configuration.ClientTargetCollection" />.</returns>
		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000A0A04 File Offset: 0x0009EC04
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Alias;
				}
				return array;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ClientTarget" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ClientTarget" /> object at the specified index.</returns>
		/// <param name="index">The collection index of the object.</param>
		// Token: 0x17001275 RID: 4725
		public ClientTarget this[int index]
		{
			get
			{
				return (ClientTarget)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified name.</returns>
		/// <param name="name">The user agent's name.</param>
		// Token: 0x17001276 RID: 4726
		public ClientTarget this[string name]
		{
			get
			{
				return (ClientTarget)base.BaseGet(name);
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000A0A5A File Offset: 0x0009EC5A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientTargetCollection.properties;
			}
		}

		// Token: 0x040020AF RID: 8367
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
