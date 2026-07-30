using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.AuthorizationRule" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000585 RID: 1413
	[ConfigurationCollection(typeof(AuthorizationRule), AddItemName = "allow,deny", CollectionType = ConfigurationElementCollectionType.BasicMapAlternate)]
	public sealed class AuthorizationRuleCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to the collection.</summary>
		/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to add to the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003BB1 RID: 15281 RVA: 0x0009F555 File Offset: 0x0009D755
		public void Add(AuthorizationRule rule)
		{
			base.BaseAdd(rule, false);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.AuthorizationRule" /> objects from the collection.</summary>
		// Token: 0x06003BB2 RID: 15282 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x0009FD84 File Offset: 0x0009DF84
		protected override ConfigurationElement CreateNewElement(string elementName)
		{
			return new AuthorizationRule((elementName == "allow") ? AuthorizationRuleAction.Allow : AuthorizationRuleAction.Deny);
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x0009FD9C File Offset: 0x0009DF9C
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthorizationRule(AuthorizationRuleAction.Allow);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.AuthorizationRule" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AuthorizationRule" /> at the specified index.</returns>
		/// <param name="index">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> index. </param>
		// Token: 0x06003BB5 RID: 15285 RVA: 0x0009FDA4 File Offset: 0x0009DFA4
		public AuthorizationRule Get(int index)
		{
			return (AuthorizationRule)base.BaseGet(index);
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x0009FDB4 File Offset: 0x0009DFB4
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AuthorizationRule)element).Action.ToString();
		}

		/// <summary>Gets the collection index of the specified <see cref="T:System.Web.Configuration.AuthorizationRule" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Configuration.AuthorizationRule" /> object.</returns>
		/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object whose index is returned.</param>
		// Token: 0x06003BB7 RID: 15287 RVA: 0x0009FDDA File Offset: 0x0009DFDA
		public int IndexOf(AuthorizationRule rule)
		{
			return base.BaseIndexOf(rule);
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x0009FDE3 File Offset: 0x0009DFE3
		protected override bool IsElementName(string elementname)
		{
			return elementname == "allow" || elementname == "deny";
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.AuthorizationRule" /> object from the collection.</summary>
		/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" />  object to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The passed <see cref="T:System.Web.Configuration.AuthorizationRule" /> object does not exist in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003BB9 RID: 15289 RVA: 0x0009FE00 File Offset: 0x0009E000
		public void Remove(AuthorizationRule rule)
		{
			base.BaseRemove(rule.Action.ToString());
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.AuthorizationRule" /> object from the collection at the specified index.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Web.Configuration.AuthorizationRule" /> to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.AuthorizationRule" /> object with the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003BBA RID: 15290 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to the collection at the specified index.</summary>
		/// <param name="index">The index location at which to add the specified <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> object. </param>
		/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to be added.</param>
		// Token: 0x06003BBB RID: 15291 RVA: 0x0009F59B File Offset: 0x0009D79B
		public void Set(int index, AuthorizationRule rule)
		{
			if (base.BaseGet(index) != null)
			{
				base.BaseRemoveAt(index);
			}
			this.BaseAdd(index, rule);
		}

		/// <summary>Gets the type of this <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" />.</summary>
		/// <returns>A value from the <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> enumeration representing the type of this collection.</returns>
		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000363BE File Offset: 0x000345BE
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x0000EE9B File Offset: 0x0000D09B
		protected override string ElementName
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets an item in this collection.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AuthorizationRule" /> at the specified index.</returns>
		/// <param name="index">
		///   <see cref="T:System.Web.Configuration.AuthorizationRule" /> collection index</param>
		// Token: 0x17001251 RID: 4689
		public AuthorizationRule this[int index]
		{
			get
			{
				return this.Get(index);
			}
			set
			{
				this.Set(index, value);
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x0009FE3A File Offset: 0x0009E03A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthorizationRuleCollection.properties;
			}
		}

		// Token: 0x04002095 RID: 8341
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
