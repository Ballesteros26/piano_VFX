using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TagMapInfo" /> objects. </summary>
	// Token: 0x020005DE RID: 1502
	[ConfigurationCollection(typeof(TagMapInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class TagMapCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.TagMapInfo" /> object to the collection.</summary>
		/// <param name="tagMapInformation">A <see cref="T:System.Web.Configuration.TagMapInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.TagMapInfo" /> object to add already exists in the collection.- or -The collection is read-only. </exception>
		// Token: 0x06004117 RID: 16663 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(TagMapInfo tagMapInformation)
		{
			this.BaseAdd(tagMapInformation);
		}

		/// <summary>Clears all object from the collection.</summary>
		// Token: 0x06004118 RID: 16664 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x000AAEC6 File Offset: 0x000A90C6
		protected override ConfigurationElement CreateNewElement()
		{
			return new TagMapInfo();
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x000AAECD File Offset: 0x000A90CD
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TagMapInfo)element).TagType;
		}

		/// <summary>Removes the specified object from the collection.</summary>
		/// <param name="tagMapInformation">A <see cref="T:System.Web.Configuration.TagMapInfo" /> object in the collection.</param>
		// Token: 0x0600411B RID: 16667 RVA: 0x000AAEDA File Offset: 0x000A90DA
		public void Remove(TagMapInfo tagMapInformation)
		{
			base.BaseRemove(tagMapInformation.TagType);
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x000AAEE8 File Offset: 0x000A90E8
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagMapCollection.properties;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TagMapInfo" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TagMapInfo" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TagMapInfo" /> object in the collection.</param>
		// Token: 0x170014B3 RID: 5299
		public TagMapInfo this[int index]
		{
			get
			{
				return (TagMapInfo)base.BaseGet(index);
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

		// Token: 0x0400231E RID: 8990
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
