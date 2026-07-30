using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.HttpHandlerAction" /> elements. This class cannot be inherited.</summary>
	// Token: 0x020005AD RID: 1453
	[ConfigurationCollection(typeof(HttpHandlerAction), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMapAlternate)]
	public sealed class HttpHandlerActionCollection : ConfigurationElementCollection
	{
		/// <summary>Adds an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to the collection.</summary>
		/// <param name="httpHandlerAction">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to add to the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to add already exists in the collection or the collection is read-only.</exception>
		// Token: 0x06003E2F RID: 15919 RVA: 0x000A4ED3 File Offset: 0x000A30D3
		public void Add(HttpHandlerAction httpHandlerAction)
		{
			HttpApplication.ClearHandlerCache();
			this.BaseAdd(httpHandlerAction);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.HttpHandlerAction" /> objects from the collection.</summary>
		// Token: 0x06003E30 RID: 15920 RVA: 0x000A4EE1 File Offset: 0x000A30E1
		public void Clear()
		{
			HttpApplication.ClearHandlerCache();
			base.BaseClear();
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x000A4EEE File Offset: 0x000A30EE
		protected override ConfigurationElement CreateNewElement()
		{
			return new HttpHandlerAction();
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x000A4EF5 File Offset: 0x000A30F5
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((HttpHandlerAction)element).Path + "-" + ((HttpHandlerAction)element).Verb;
		}

		/// <summary>Gets the collection index of the specified <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object.</summary>
		/// <returns>The collection index value.</returns>
		/// <param name="action">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object for which to get the collection index. </param>
		// Token: 0x06003E33 RID: 15923 RVA: 0x0009FDDA File Offset: 0x0009DFDA
		public int IndexOf(HttpHandlerAction action)
		{
			return base.BaseIndexOf(action);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object with the specified <see cref="P:System.Web.Configuration.HttpHandlerAction.Verb" /> and <see cref="P:System.Web.Configuration.HttpHandlerAction.Path" /> properties from the collection.</summary>
		/// <param name="verb">The <see cref="P:System.Web.Configuration.HttpHandlerAction.Verb" /> property value that belongs to the handler to remove.</param>
		/// <param name="path">The <see cref="P:System.Web.Configuration.HttpHandlerAction.Path" /> property value that belongs to the handler to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003E34 RID: 15924 RVA: 0x000A4F17 File Offset: 0x000A3117
		public void Remove(string verb, string path)
		{
			HttpApplication.ClearHandlerCache();
			base.BaseRemove(path + "-" + verb);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object from the collection.</summary>
		/// <param name="action">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to remove. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The passed <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object does not exist in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003E35 RID: 15925 RVA: 0x000A4F30 File Offset: 0x000A3130
		public void Remove(HttpHandlerAction action)
		{
			HttpApplication.ClearHandlerCache();
			base.BaseRemove(action.Path + "-" + action.Verb);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The collection index of the object to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003E36 RID: 15926 RVA: 0x000A4F53 File Offset: 0x000A3153
		public void RemoveAt(int index)
		{
			HttpApplication.ClearHandlerCache();
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets the type of <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" />.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x000A4F61 File Offset: 0x000A3161
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMapAlternate;
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x000A4F64 File Offset: 0x000A3164
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpHandlerActionCollection.properties;
			}
		}

		/// <summary>Gets or sets an item in this collection.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object selected.</returns>
		/// <param name="index">The item index.</param>
		// Token: 0x1700137F RID: 4991
		public HttpHandlerAction this[int index]
		{
			get
			{
				return (HttpHandlerAction)base.BaseGet(index);
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

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x00008A69 File Offset: 0x00006C69
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04002212 RID: 8722
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
