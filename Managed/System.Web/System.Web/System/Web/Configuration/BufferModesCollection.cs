using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>A collection of <see cref="T:System.Web.Configuration.BufferModeSettings" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000588 RID: 1416
	[ConfigurationCollection(typeof(BufferModeSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class BufferModesCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object to the collection.</summary>
		/// <param name="bufferModeSettings">A <see cref="T:System.Web.Configuration.BufferModeSettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.BufferModeSettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003BDD RID: 15325 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(BufferModeSettings bufferModeSettings)
		{
			this.BaseAdd(bufferModeSettings);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.BufferModeSettings" /> objects from the collection.</summary>
		// Token: 0x06003BDE RID: 15326 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x000A0298 File Offset: 0x0009E498
		protected override ConfigurationElement CreateNewElement()
		{
			return new BufferModeSettings();
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000A029F File Offset: 0x0009E49F
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((BufferModeSettings)element).Name;
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object from the collection.</summary>
		/// <param name="s">The name of a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.BufferModeSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003BE1 RID: 15329 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string s)
		{
			base.BaseRemove(s);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object with the specified numeric index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.BufferModeSettings" /> object at the specified index.</returns>
		/// <param name="index">A valid index of a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object in the collection.</param>
		// Token: 0x1700125E RID: 4702
		public BufferModeSettings this[int index]
		{
			get
			{
				return (BufferModeSettings)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object based on the specified key in the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.BufferModeSettings" /> object.</returns>
		/// <param name="key">The name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object contained in the collection.</param>
		// Token: 0x1700125F RID: 4703
		public BufferModeSettings this[string key]
		{
			get
			{
				return (BufferModeSettings)base.BaseGet(key);
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000A02C8 File Offset: 0x0009E4C8
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BufferModesCollection.properties;
			}
		}

		// Token: 0x040020A1 RID: 8353
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
