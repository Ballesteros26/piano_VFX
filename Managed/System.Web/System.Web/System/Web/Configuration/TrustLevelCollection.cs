using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TrustLevel" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005E6 RID: 1510
	[ConfigurationCollection(typeof(TrustLevel), AddItemName = "trustLevel")]
	public sealed class TrustLevelCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.TrustLevel" /> object to the collection.</summary>
		/// <param name="trustLevel">The <see cref="T:System.Web.Configuration.TrustLevel" /> to add to the collection.</param>
		// Token: 0x06004177 RID: 16759 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(TrustLevel trustLevel)
		{
			this.BaseAdd(trustLevel);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.TrustLevel" /> objects from the collection.</summary>
		// Token: 0x06004178 RID: 16760 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.TrustLevel" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TrustLevel" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		// Token: 0x06004179 RID: 16761 RVA: 0x000AB8A0 File Offset: 0x000A9AA0
		public TrustLevel Get(int index)
		{
			return (TrustLevel)base.BaseGet(index);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.TrustLevel" /> object from the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
		/// <param name="trustLevel">The <see cref="T:System.Web.Configuration.TrustLevel" /> to remove from the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		// Token: 0x0600417A RID: 16762 RVA: 0x000AB8AE File Offset: 0x000A9AAE
		public void Remove(TrustLevel trustLevel)
		{
			base.BaseRemove(trustLevel.Name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.TrustLevel" /> object at the specified index location from the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Web.Configuration.TrustLevel" /> to remove from the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		// Token: 0x0600417B RID: 16763 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.TrustLevel" /> object to the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object at the specified index.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Web.Configuration.TrustLevel" /> to be set within the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		/// <param name="trustLevel">The <see cref="T:System.Web.Configuration.TrustLevel" /> to be set within the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		// Token: 0x0600417C RID: 16764 RVA: 0x0009F59B File Offset: 0x0009D79B
		public void Set(int index, TrustLevel trustLevel)
		{
			if (base.BaseGet(index) != null)
			{
				base.BaseRemoveAt(index);
			}
			this.BaseAdd(index, trustLevel);
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000AB8BC File Offset: 0x000A9ABC
		protected override ConfigurationElement CreateNewElement()
		{
			return new TrustLevel();
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000AB8C3 File Offset: 0x000A9AC3
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TrustLevel)element).Name;
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000AB8D0 File Offset: 0x000A9AD0
		protected override bool IsElementName(string elementname)
		{
			return elementname == "trustlevel";
		}

		/// <summary>Gets the type of the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
		/// <returns>A value from the <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> enumeration representing the type of the collection.</returns>
		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06004180 RID: 16768 RVA: 0x00008A69 File Offset: 0x00006C69
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06004181 RID: 16769 RVA: 0x000AB8DD File Offset: 0x000A9ADD
		protected override string ElementName
		{
			get
			{
				return "trustLevel";
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.TrustLevel" /> object at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TrustLevel" /> that exists at the specified index of the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</returns>
		/// <param name="key">The index of the <see cref="T:System.Web.Configuration.TrustLevel" />.</param>
		// Token: 0x170014D5 RID: 5333
		public TrustLevel this[string key]
		{
			get
			{
				return (TrustLevel)base.BaseGet(key);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TrustLevel" /> item at the specified index within the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TrustLevel" /> at the specified index.</returns>
		/// <param name="index">The numeric index of the <see cref="T:System.Web.Configuration.TrustLevel" />.</param>
		// Token: 0x170014D6 RID: 5334
		public TrustLevel this[int index]
		{
			get
			{
				return (TrustLevel)base.BaseGet(index);
			}
			set
			{
				this.Set(index, value);
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x00008A69 File Offset: 0x00006C69
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x000AB8FC File Offset: 0x000A9AFC
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TrustLevelCollection.properties;
			}
		}

		// Token: 0x04002339 RID: 9017
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
