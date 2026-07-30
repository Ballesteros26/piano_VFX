using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.CodeSubDirectory" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000591 RID: 1425
	[ConfigurationCollection(typeof(CodeSubDirectory), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class CodeSubDirectoriesCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> class.</summary>
		// Token: 0x06003C35 RID: 15413 RVA: 0x000A044C File Offset: 0x0009E64C
		public CodeSubDirectoriesCollection()
			: base(CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> at the specified index in the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.CodeSubDirectory" /> object.</returns>
		/// <param name="index">An integer value specifying a specific <see cref="T:System.Web.Configuration.CodeSubDirectory" /> object within the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> collection.</param>
		// Token: 0x1700127A RID: 4730
		public CodeSubDirectory this[int index]
		{
			get
			{
				return (CodeSubDirectory)base.BaseGet(index);
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

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x00008A69 File Offset: 0x00006C69
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x000A0AC6 File Offset: 0x0009ECC6
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x000A0ACD File Offset: 0x0009ECCD
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeSubDirectoriesCollection.props;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Configuration.CodeSubDirectory" /> object to the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" />.</summary>
		/// <param name="codeSubDirectory">A string value specifying the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> reference.</param>
		// Token: 0x06003C3B RID: 15419 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(CodeSubDirectory codeSubDirectory)
		{
			this.BaseAdd(codeSubDirectory);
		}

		/// <summary>Removes all items from the collection</summary>
		// Token: 0x06003C3C RID: 15420 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x000A0AD4 File Offset: 0x0009ECD4
		protected override ConfigurationElement CreateNewElement()
		{
			return new CodeSubDirectory(null);
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x000A0ADC File Offset: 0x0009ECDC
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CodeSubDirectory)element).DirectoryName;
		}

		/// <summary>Removes the specified item from the collection.</summary>
		/// <param name="directoryName">The name of the directory to remove.</param>
		// Token: 0x06003C3F RID: 15423 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string directoryName)
		{
			base.BaseRemove(directoryName);
		}

		/// <summary>Removes the item at the specified index in the collection.</summary>
		/// <param name="index">The index position of the item to be removed.</param>
		// Token: 0x06003C40 RID: 15424 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x040020B2 RID: 8370
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
