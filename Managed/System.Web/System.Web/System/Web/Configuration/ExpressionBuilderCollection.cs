using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.ExpressionBuilder" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200059D RID: 1437
	[ConfigurationCollection(typeof(ExpressionBuilder), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ExpressionBuilderCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> class.</summary>
		// Token: 0x06003CE8 RID: 15592 RVA: 0x000A044C File Offset: 0x0009E64C
		public ExpressionBuilderCollection()
			: base(CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> at the specified index in the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object at the specified index or null if there is no object at that index.</returns>
		/// <param name="index">An integer value specifying an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object within the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</param>
		// Token: 0x170012C3 RID: 4803
		public ExpressionBuilder this[int index]
		{
			get
			{
				return (ExpressionBuilder)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ExpressionBuilder" /> or null if there is no object with that name in the collection.</returns>
		/// <param name="name">The name of an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object in the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</param>
		// Token: 0x170012C4 RID: 4804
		public ExpressionBuilder this[string name]
		{
			get
			{
				return (ExpressionBuilder)base.BaseGet(name);
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x000A1C8E File Offset: 0x0009FE8E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ExpressionBuilderCollection.props;
			}
		}

		/// <summary>Adds an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object to the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" />.</summary>
		/// <param name="buildProvider">A string value specifying the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> reference.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object to add already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003CED RID: 15597 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(ExpressionBuilder buildProvider)
		{
			this.BaseAdd(buildProvider);
		}

		/// <summary>Clears all the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> objects from the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
		// Token: 0x06003CEE RID: 15598 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object from the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
		/// <param name="name">A string value specifying the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> reference.</param>
		// Token: 0x06003CEF RID: 15599 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object from the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
		/// <param name="index">An integer value specifying a specific <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object within the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</param>
		// Token: 0x06003CF0 RID: 15600 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x000A1C95 File Offset: 0x0009FE95
		protected override ConfigurationElement CreateNewElement()
		{
			return new ExpressionBuilder();
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000A1C9C File Offset: 0x0009FE9C
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ExpressionBuilder)element).ExpressionPrefix;
		}

		// Token: 0x040020E4 RID: 8420
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
