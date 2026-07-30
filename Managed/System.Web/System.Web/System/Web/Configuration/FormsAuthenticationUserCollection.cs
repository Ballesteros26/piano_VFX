using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005A1 RID: 1441
	[ConfigurationCollection(typeof(FormsAuthenticationUser), AddItemName = "user", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class FormsAuthenticationUserCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to the collection.</summary>
		/// <param name="user">The <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to add to the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003D20 RID: 15648 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(FormsAuthenticationUser user)
		{
			this.BaseAdd(user);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> objects from the collection.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationException">The collection is read-only.</exception>
		// Token: 0x06003D21 RID: 15649 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x000A231A File Offset: 0x000A051A
		protected override ConfigurationElement CreateNewElement()
		{
			return new FormsAuthenticationUser("", "");
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> collection element at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> that contains the user name and password.</returns>
		/// <param name="index">The collection user's index. </param>
		// Token: 0x06003D23 RID: 15651 RVA: 0x000A232B File Offset: 0x000A052B
		public FormsAuthenticationUser Get(int index)
		{
			return (FormsAuthenticationUser)base.BaseGet(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> collection element with the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object that contains the user name and password.</returns>
		/// <param name="name">The user's name. </param>
		// Token: 0x06003D24 RID: 15652 RVA: 0x000A2339 File Offset: 0x000A0539
		public FormsAuthenticationUser Get(string name)
		{
			return (FormsAuthenticationUser)base.BaseGet(name);
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x000A2347 File Offset: 0x000A0547
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FormsAuthenticationUser)element).Name;
		}

		/// <summary>Gets the key at the specified <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> collection index.</summary>
		/// <returns>The key at the specified index of the <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" />.</returns>
		/// <param name="index">The index in the collection.</param>
		// Token: 0x06003D26 RID: 15654 RVA: 0x000A2354 File Offset: 0x000A0554
		public string GetKey(int index)
		{
			return this.Get(index).Name;
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to remove from the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003D27 RID: 15655 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to remove from the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object at the specified index in the collection, the element has already been removed, or the collection is read only.</exception>
		// Token: 0x06003D28 RID: 15656 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Modifies the specified <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object contained in the collection.</summary>
		/// <param name="user">The <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> object that must be changed. </param>
		// Token: 0x06003D29 RID: 15657 RVA: 0x000A2364 File Offset: 0x000A0564
		public void Set(FormsAuthenticationUser user)
		{
			FormsAuthenticationUser formsAuthenticationUser = this.Get(user.Name);
			if (formsAuthenticationUser == null)
			{
				this.Add(user);
				return;
			}
			int num = base.BaseIndexOf(formsAuthenticationUser);
			this.RemoveAt(num);
			this.BaseAdd(num, user);
		}

		/// <summary>Gets all the collection's keys.</summary>
		/// <returns>The string array containing the collection keys.</returns>
		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000A23A0 File Offset: 0x000A05A0
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

		/// <summary>Gets the type of the <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" />.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06003D2B RID: 15659 RVA: 0x00008A69 File Offset: 0x00006C69
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x000A23DA File Offset: 0x000A05DA
		protected override string ElementName
		{
			get
			{
				return "user";
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x000A23E1 File Offset: 0x000A05E1
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationUserCollection.properties;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> that contains the user name and password.</returns>
		/// <param name="index">The collection user's index. </param>
		// Token: 0x170012DF RID: 4831
		public FormsAuthenticationUser this[int index]
		{
			get
			{
				return this.Get(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> with the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object that contains the user name and password.</returns>
		/// <param name="name">The user's name. </param>
		// Token: 0x170012E0 RID: 4832
		public FormsAuthenticationUser this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x00008A69 File Offset: 0x00006C69
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040020F9 RID: 8441
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
