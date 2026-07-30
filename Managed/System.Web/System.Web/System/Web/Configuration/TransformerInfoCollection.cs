using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TransformerInfo" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005E4 RID: 1508
	[ConfigurationCollection(typeof(TransformerInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class TransformerInfoCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.TransformerInfo" /> object to the collection.</summary>
		/// <param name="transformerInfo">A <see cref="T:System.Web.Configuration.TransformerInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.TransformerInfo" /> object to add already exists in the collection.- or -The collection is read-only. </exception>
		// Token: 0x06004164 RID: 16740 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(TransformerInfo transformerInfo)
		{
			this.BaseAdd(transformerInfo);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.TransformerInfo" /> objects from the collection.</summary>
		// Token: 0x06004165 RID: 16741 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x000AB77A File Offset: 0x000A997A
		protected override ConfigurationElement CreateNewElement()
		{
			return new TransformerInfo("", "");
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000AB78B File Offset: 0x000A998B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TransformerInfo)element).Name;
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.TransformerInfo" /> object with the passed <see cref="P:System.Web.Configuration.TransformerInfo.Name" /> property value from the collection.</summary>
		/// <param name="s">The name of a <see cref="T:System.Web.Configuration.TransformerInfo" /> object to remove from the collection.</param>
		// Token: 0x06004168 RID: 16744 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string s)
		{
			base.BaseRemove(s);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.TransformerInfo" /> object from the collection at the passed index.</summary>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TransformerInfo" /> object to remove from the collection.</param>
		// Token: 0x06004169 RID: 16745 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TagMapInfo" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TransformerInfo" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TransformerInfo" /> object in the collection.</param>
		// Token: 0x170014CE RID: 5326
		public TransformerInfo this[int index]
		{
			get
			{
				return (TransformerInfo)base.BaseGet(index);
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

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x0600416C RID: 16748 RVA: 0x000AB7A6 File Offset: 0x000A99A6
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TransformerInfoCollection.properties;
			}
		}

		// Token: 0x04002335 RID: 9013
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
