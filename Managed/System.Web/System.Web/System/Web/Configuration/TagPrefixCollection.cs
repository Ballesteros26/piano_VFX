using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TagPrefixInfo" /> objects.</summary>
	// Token: 0x020005E0 RID: 1504
	[ConfigurationCollection(typeof(TagPrefixInfo), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class TagPrefixCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object to the collection.</summary>
		/// <param name="tagPrefixInformation">The <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object to add already exists in the collection.- or -The collection is read-only. </exception>
		// Token: 0x0600412C RID: 16684 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(TagPrefixInfo tagPrefixInformation)
		{
			this.BaseAdd(tagPrefixInformation);
		}

		/// <summary>Clears all object from the collection.</summary>
		// Token: 0x0600412D RID: 16685 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x000AB065 File Offset: 0x000A9265
		protected override ConfigurationElement CreateNewElement()
		{
			return new TagPrefixInfo();
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x000AB06C File Offset: 0x000A926C
		protected override object GetElementKey(ConfigurationElement element)
		{
			TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)element;
			return string.Concat(new string[] { tagPrefixInfo.TagPrefix, "-", tagPrefixInfo.TagName, "-", tagPrefixInfo.Source, "-", tagPrefixInfo.Namespace, "-", tagPrefixInfo.Assembly });
		}

		/// <summary>Removes the specified object from the collection.</summary>
		/// <param name="tagPrefixInformation">A <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object in the collection.</param>
		// Token: 0x06004130 RID: 16688 RVA: 0x000AB0D9 File Offset: 0x000A92D9
		public void Remove(TagPrefixInfo tagPrefixInformation)
		{
			base.BaseRemove(this.GetElementKey(tagPrefixInformation));
		}

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06004131 RID: 16689 RVA: 0x00008A69 File Offset: 0x00006C69
		[global::System.MonoTODO("why override this?")]
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x000A0AC6 File Offset: 0x0009ECC6
		[global::System.MonoTODO("why override this?")]
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x000AB0E8 File Offset: 0x000A92E8
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagPrefixCollection.properties;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object in the collection.</param>
		// Token: 0x170014BA RID: 5306
		public TagPrefixInfo this[int index]
		{
			get
			{
				return (TagPrefixInfo)base.BaseGet(index);
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

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06004136 RID: 16694 RVA: 0x00008A69 File Offset: 0x00006C69
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04002322 RID: 8994
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
