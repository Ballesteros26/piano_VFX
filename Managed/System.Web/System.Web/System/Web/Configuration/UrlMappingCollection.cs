using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.UrlMapping" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005E9 RID: 1513
	[ConfigurationCollection(typeof(UrlMapping), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class UrlMappingCollection : ConfigurationElementCollection
	{
		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.UrlMapping" /> to the collection.</summary>
		/// <param name="urlMapping">The <see cref="T:System.Web.Configuration.UrlMapping" /> object to add to the collection.</param>
		// Token: 0x060041A1 RID: 16801 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(UrlMapping urlMapping)
		{
			this.BaseAdd(urlMapping);
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.UrlMapping" /> objects from the collection.</summary>
		// Token: 0x060041A2 RID: 16802 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000ABB6E File Offset: 0x000A9D6E
		protected override ConfigurationElement CreateNewElement()
		{
			return new UrlMapping();
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x000ABB75 File Offset: 0x000A9D75
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((UrlMapping)element).Url;
		}

		/// <summary>Gets the collection key of the specified element.</summary>
		/// <returns>A string representing the value of the key. </returns>
		/// <param name="index">The collection index of the element to get.</param>
		// Token: 0x060041A5 RID: 16805 RVA: 0x000A09E6 File Offset: 0x0009EBE6
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.UrlMapping" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.UrlMapping" /> object to remove from the collection.</param>
		// Token: 0x060041A6 RID: 16806 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.UrlMapping" /> object from the collection.</summary>
		/// <param name="urlMapping">The <see cref="T:System.Web.Configuration.UrlMapping" /> object to remove from the collection.</param>
		// Token: 0x060041A7 RID: 16807 RVA: 0x000ABB82 File Offset: 0x000A9D82
		public void Remove(UrlMapping urlMapping)
		{
			base.BaseRemove(urlMapping.Url);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.UrlMapping" /> object with the specified index from the collection.</summary>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.UrlMapping" /> object to remove.</param>
		// Token: 0x060041A8 RID: 16808 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets an array of the keys for all of the configuration elements contained in the <see cref="T:System.Web.Configuration.UrlMappingCollection" />.</summary>
		/// <returns>An array of the keys for all of the <see cref="T:System.Web.Configuration.UrlMapping" /> objects contained in the <see cref="T:System.Web.Configuration.UrlMappingCollection" />.</returns>
		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x000ABB90 File Offset: 0x000A9D90
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Url;
				}
				return array;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.UrlMapping" /> object at the specified index.</summary>
		/// <returns>The object at the specified index.</returns>
		/// <param name="index">The index of the object to get.</param>
		// Token: 0x170014E4 RID: 5348
		public UrlMapping this[int index]
		{
			get
			{
				return (UrlMapping)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.UrlMapping" /> object with the specified name.</summary>
		/// <returns>The collection object that has the specified name.</returns>
		/// <param name="name">The name of the collection object.</param>
		// Token: 0x170014E5 RID: 5349
		public UrlMapping this[string name]
		{
			get
			{
				return (UrlMapping)base.BaseGet(name);
			}
		}

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x000ABBE6 File Offset: 0x000A9DE6
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UrlMappingCollection.properties;
			}
		}

		// Token: 0x04002341 RID: 9025
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
