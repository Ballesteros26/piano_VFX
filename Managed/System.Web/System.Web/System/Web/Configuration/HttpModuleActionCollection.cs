using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.HttpModuleAction" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005B0 RID: 1456
	[ConfigurationCollection(typeof(HttpModuleAction), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class HttpModuleActionCollection : ConfigurationElementCollection
	{
		/// <summary>Adds an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object to the collection.</summary>
		/// <param name="httpModule">The <see cref="T:System.Web.Configuration.HttpModuleAction" /> module to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">An attempt was made to add an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object that already exists in the collection. </exception>
		// Token: 0x06003E4D RID: 15949 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(HttpModuleAction httpModule)
		{
			this.BaseAdd(httpModule);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.HttpModuleAction" /> objects from the collection.</summary>
		// Token: 0x06003E4E RID: 15950 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x000A5190 File Offset: 0x000A3390
		protected override ConfigurationElement CreateNewElement()
		{
			return new HttpModuleAction();
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x000A5197 File Offset: 0x000A3397
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((HttpModuleAction)element).Name;
		}

		/// <summary>Gets the collection index of the specified <see cref="T:System.Web.Configuration.HttpModuleAction" /> module.</summary>
		/// <returns>The collection index value for the specified module.</returns>
		/// <param name="action">The <see cref="T:System.Web.Configuration.HttpModuleAction" /> module for which to get the collection index.</param>
		// Token: 0x06003E51 RID: 15953 RVA: 0x0009FDDA File Offset: 0x0009DFDA
		public int IndexOf(HttpModuleAction action)
		{
			return base.BaseIndexOf(action);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object from the collection.</summary>
		/// <param name="name">The key that identifies the <see cref="T:System.Web.Configuration.HttpModuleAction" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpModuleAction" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003E52 RID: 15954 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object from the collection.</summary>
		/// <param name="action">The <see cref="T:System.Web.Configuration.HttpModuleAction" /> module to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The passed <see cref="T:System.Web.Configuration.HttpModuleAction" /> object does not exist in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003E53 RID: 15955 RVA: 0x000A51A4 File Offset: 0x000A33A4
		public void Remove(HttpModuleAction action)
		{
			base.BaseRemove(action.Name);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpModuleAction" /> module at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.HttpModuleAction" /> module to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpModuleAction" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003E54 RID: 15956 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000A51B2 File Offset: 0x000A33B2
		protected override bool IsElementRemovable(ConfigurationElement element)
		{
			return base.IsElementRemovable(element);
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06003E56 RID: 15958 RVA: 0x000A51BB File Offset: 0x000A33BB
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpModuleActionCollection.properties;
			}
		}

		/// <summary>Gets or sets an item in this collection.</summary>
		/// <returns>The item at the specified <paramref name="index" />.</returns>
		/// <param name="index">Module collection index.</param>
		// Token: 0x17001388 RID: 5000
		public HttpModuleAction this[int index]
		{
			get
			{
				return (HttpModuleAction)base.BaseGet(index);
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

		// Token: 0x04002219 RID: 8729
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
