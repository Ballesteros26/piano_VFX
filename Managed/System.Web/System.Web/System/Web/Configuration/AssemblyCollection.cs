using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.AssemblyInfo" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000581 RID: 1409
	[ConfigurationCollection(typeof(AssemblyInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class AssemblyCollection : ConfigurationElementCollection
	{
		/// <summary>Adds an <see cref="T:System.Web.Configuration.AssemblyInfo" /> object to the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
		/// <param name="assemblyInformation">A string value specifying the assembly reference.</param>
		// Token: 0x06003B81 RID: 15233 RVA: 0x0009F555 File Offset: 0x0009D755
		public void Add(AssemblyInfo assemblyInformation)
		{
			base.BaseAdd(assemblyInformation, false);
		}

		/// <summary>Clears all the <see cref="T:System.Web.Configuration.AssemblyInfo" /> objects from the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
		// Token: 0x06003B82 RID: 15234 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x0009F567 File Offset: 0x0009D767
		protected override ConfigurationElement CreateNewElement()
		{
			return new AssemblyInfo();
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x0009F56E File Offset: 0x0009D76E
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AssemblyInfo)element).Assembly;
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.AssemblyInfo" /> object from the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
		/// <param name="key">A string value specifying the assembly reference.</param>
		// Token: 0x06003B85 RID: 15237 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.AssemblyInfo" /> object from the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
		/// <param name="index">An integer value specifying an <see cref="T:System.Web.Configuration.AssemblyInfo" /> object within the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</param>
		// Token: 0x06003B86 RID: 15238 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.AssemblyInfo" /> at the specified index in the <see cref="T:System.Web.Configuration.AssemblyCollection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.AssemblyInfo" /> object.</returns>
		/// <param name="index">An integer value specifying a specific <see cref="T:System.Web.Configuration.AssemblyInfo" /> object within the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</param>
		// Token: 0x17001241 RID: 4673
		public AssemblyInfo this[int index]
		{
			get
			{
				return (AssemblyInfo)base.BaseGet(index);
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

		/// <summary>Gets the item identified by the specified assembly name.</summary>
		/// <returns>The item identified by the specified assembly name.</returns>
		/// <param name="assemblyName">The name identifying the assembly to retrieve.</param>
		// Token: 0x17001242 RID: 4674
		public AssemblyInfo this[string assemblyName]
		{
			get
			{
				return (AssemblyInfo)base.BaseGet(assemblyName);
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06003B8A RID: 15242 RVA: 0x0009F5C3 File Offset: 0x0009D7C3
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssemblyCollection.properties;
			}
		}

		// Token: 0x04002088 RID: 8328
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
