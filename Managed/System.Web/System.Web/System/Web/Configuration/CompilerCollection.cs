using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.Compiler" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000595 RID: 1429
	[ConfigurationCollection(typeof(Compiler), AddItemName = "compiler", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class CompilerCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CompilerCollection" /> class.</summary>
		// Token: 0x06003C86 RID: 15494 RVA: 0x000A044C File Offset: 0x0009E64C
		public CompilerCollection()
			: base(CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x000A13CF File Offset: 0x0009F5CF
		protected override ConfigurationElement CreateNewElement()
		{
			return new Compiler();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> collection element at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
		/// <param name="index">An integer value specifying a <see cref="T:System.Web.Configuration.Compiler" /> within the <see cref="T:System.Web.Configuration.CompilerCollection" />.</param>
		// Token: 0x06003C88 RID: 15496 RVA: 0x000A13D6 File Offset: 0x0009F5D6
		public Compiler Get(int index)
		{
			return (Compiler)base.BaseGet(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> collection element for the specified language.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
		/// <param name="language">The language for the <see cref="T:System.Web.Configuration.Compiler" /> object within the collection.</param>
		// Token: 0x06003C89 RID: 15497 RVA: 0x000A13E4 File Offset: 0x0009F5E4
		public Compiler Get(string language)
		{
			return this[language];
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x000A13ED File Offset: 0x0009F5ED
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Compiler)element).Language;
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CompilerCollection" /> key name at the specified index.</summary>
		/// <returns>The key name at the specified index of the <see cref="T:System.Web.Configuration.CompilerCollection" />.</returns>
		/// <param name="index">An integer value specifying a <see cref="T:System.Web.Configuration.Compiler" /> within the <see cref="T:System.Web.Configuration.CompilerCollection" />.</param>
		// Token: 0x06003C8B RID: 15499 RVA: 0x000A09E6 File Offset: 0x0009EBE6
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Gets all the keys of the <see cref="T:System.Web.Configuration.CompilerCollection" />.</summary>
		/// <returns>The string array containing the collection keys.</returns>
		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x000A13FC File Offset: 0x0009F5FC
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Language;
				}
				return array;
			}
		}

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06003C8D RID: 15501 RVA: 0x00008A69 File Offset: 0x00006C69
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06003C8E RID: 15502 RVA: 0x000A1436 File Offset: 0x0009F636
		protected override string ElementName
		{
			get
			{
				return "compiler";
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06003C8F RID: 15503 RVA: 0x000A143D File Offset: 0x0009F63D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerCollection.properties;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> at the specified index of the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
		/// <param name="index">An integer value specifying a <see cref="T:System.Web.Configuration.Compiler" /> within the <see cref="T:System.Web.Configuration.CompilerCollection" />.</param>
		// Token: 0x170012A4 RID: 4772
		public Compiler this[int index]
		{
			get
			{
				return (Compiler)base.BaseGet(index);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> collection element for the specified language.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
		/// <param name="language">The language for the <see cref="T:System.Web.Configuration.Compiler" /> object within the collection.</param>
		// Token: 0x170012A5 RID: 4773
		public Compiler this[string language]
		{
			get
			{
				foreach (object obj in this)
				{
					Compiler compiler = (Compiler)obj;
					if (compiler.Language.IndexOf(language, StringComparison.InvariantCultureIgnoreCase) != -1)
					{
						return compiler;
					}
				}
				return null;
			}
		}

		// Token: 0x040020CF RID: 8399
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
