using System;
using System.Collections;
using System.Configuration;
using System.Web.Compilation;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.BuildProvider" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200058A RID: 1418
	[ConfigurationCollection(typeof(BuildProvider), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class BuildProviderCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BuildProviderCollection" /> class.</summary>
		// Token: 0x06003BF2 RID: 15346 RVA: 0x000A044C File Offset: 0x0009E64C
		public BuildProviderCollection()
			: base(CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.BuildProvider" /> object at the specified index of the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.BuildProvider" /> object.</returns>
		/// <param name="index">An integer value specifying a particular <see cref="T:System.Web.Configuration.BuildProvider" /> object within the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</param>
		// Token: 0x17001264 RID: 4708
		public BuildProvider this[int index]
		{
			get
			{
				return (BuildProvider)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.BuildProvider" /> collection element based on the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.BuildProvider" /> object.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.BuildProvider" /> object to return from the collection.</param>
		// Token: 0x17001265 RID: 4709
		public BuildProvider this[string name]
		{
			get
			{
				string text;
				if (!string.IsNullOrEmpty(name))
				{
					text = name.ToLowerInvariant();
				}
				else
				{
					text = name;
				}
				return (BuildProvider)base.BaseGet(text);
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000A0494 File Offset: 0x0009E694
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BuildProviderCollection.props;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Configuration.BuildProvider" /> object to the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
		/// <param name="buildProvider">A <see cref="T:System.Web.Configuration.BuildProvider" /> object.</param>
		// Token: 0x06003BF7 RID: 15351 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(BuildProvider buildProvider)
		{
			this.BaseAdd(buildProvider);
		}

		/// <summary>Clears all <see cref="T:System.Web.Configuration.BuildProvider" /> objects from the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
		// Token: 0x06003BF8 RID: 15352 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.BuildProvider" /> object from the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
		/// <param name="name">A string value specifying the <see cref="T:System.Web.Configuration.BuildProvider" /> reference.</param>
		// Token: 0x06003BF9 RID: 15353 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.BuildProvider" /> object at the specified index from the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
		/// <param name="index">An integer value specifying the location of a specific <see cref="T:System.Web.Configuration.BuildProvider" /> object within the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</param>
		// Token: 0x06003BFA RID: 15354 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x000A049B File Offset: 0x0009E69B
		protected override ConfigurationElement CreateNewElement()
		{
			return new BuildProvider();
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x000A04A2 File Offset: 0x0009E6A2
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((BuildProvider)element).Extension;
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x000A04AF File Offset: 0x0009E6AF
		internal Type GetProviderTypeForExtension(string extension)
		{
			return BuildProvider.GetProviderTypeForExtension(extension);
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000A04B7 File Offset: 0x0009E6B7
		internal BuildProvider GetProviderInstanceForExtension(string extension)
		{
			return BuildProvider.GetProviderInstanceForExtension(extension);
		}

		// Token: 0x040020A5 RID: 8357
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
