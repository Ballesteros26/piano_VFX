using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of namespace objects. This class cannot be inherited.</summary>
	// Token: 0x020005BD RID: 1469
	[ConfigurationCollection(typeof(NamespaceInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class NamespaceCollection : ConfigurationElementCollection
	{
		// Token: 0x06003EF3 RID: 16115 RVA: 0x000A6CA6 File Offset: 0x000A4EA6
		static NamespaceCollection()
		{
			NamespaceCollection.properties.Add(NamespaceCollection.autoImportVBNamespaceProp);
		}

		/// <summary>Adds a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to the collection.</summary>
		/// <param name="namespaceInformation">A <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to add already exists in the collection or the collection is read-only.</exception>
		// Token: 0x06003EF5 RID: 16117 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(NamespaceInfo namespaceInformation)
		{
			this.BaseAdd(namespaceInformation);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.NamespaceInfo" /> objects from the collection.</summary>
		// Token: 0x06003EF6 RID: 16118 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000A6CE0 File Offset: 0x000A4EE0
		protected override ConfigurationElement CreateNewElement()
		{
			return new NamespaceInfo(null);
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000A6CE8 File Offset: 0x000A4EE8
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((NamespaceInfo)element).Namespace;
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.NamespaceInfo" /> object with the specified key from the collection.</summary>
		/// <param name="s">The namespace of a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.NamespaceInfo" /> object with the specified key in the collection.- or -The element has already been removed.- or -The collection is read-only.</exception>
		// Token: 0x06003EF9 RID: 16121 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string s)
		{
			base.BaseRemove(s);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object from the specified index in the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.NamespaceInfo" /> object at the specified index in the collection.- or -The element has already been removed.- or -The collection is read-only.</exception>
		// Token: 0x06003EFA RID: 16122 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets a value that determines whether the Visual Basic namespace is imported without having to specify it.</summary>
		/// <returns>true if the Visual Basic namespace is imported automatically; otherwise, false. The default is true.</returns>
		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06003EFB RID: 16123 RVA: 0x000A6CF5 File Offset: 0x000A4EF5
		// (set) Token: 0x06003EFC RID: 16124 RVA: 0x000A6D07 File Offset: 0x000A4F07
		[ConfigurationProperty("autoImportVBNamespace", DefaultValue = true)]
		public bool AutoImportVBNamespace
		{
			get
			{
				return (bool)base[NamespaceCollection.autoImportVBNamespaceProp];
			}
			set
			{
				base[NamespaceCollection.autoImportVBNamespaceProp] = value;
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06003EFD RID: 16125 RVA: 0x000A6D1A File Offset: 0x000A4F1A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NamespaceCollection.properties;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.NamespaceInfo" /> object at the specified index in the collection.</summary>
		/// <returns>
		///   <see cref="T:System.Web.Configuration.NamespaceInfo" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object in the collection.</param>
		// Token: 0x170013C6 RID: 5062
		public NamespaceInfo this[int index]
		{
			get
			{
				return (NamespaceInfo)base.BaseGet(index);
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

		// Token: 0x0400225E RID: 8798
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400225F RID: 8799
		private static ConfigurationProperty autoImportVBNamespaceProp = new ConfigurationProperty("autoImportVBNamespace", typeof(bool), true);
	}
}
