using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure the operation of the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" />. This class cannot be inherited.</summary>
	// Token: 0x0200037B RID: 891
	[ConfigurationCollection(typeof(SchemaImporterExtensionElement))]
	public sealed class SchemaImporterExtensionElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the object that represents the XML element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the XML element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than Count.</exception>
		// Token: 0x17000724 RID: 1828
		public SchemaImporterExtensionElement this[int index]
		{
			get
			{
				return (SchemaImporterExtensionElement)base.BaseGet(index);
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

		/// <summary>Gets or sets the item with the specified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> with the specified name.</returns>
		/// <param name="name">The name of the item to get or set.</param>
		// Token: 0x17000725 RID: 1829
		public SchemaImporterExtensionElement this[string name]
		{
			get
			{
				return (SchemaImporterExtensionElement)base.BaseGet(name);
			}
			set
			{
				if (base.BaseGet(name) != null)
				{
					base.BaseRemove(name);
				}
				this.BaseAdd(value);
			}
		}

		/// <summary>Adds an item to the end of the collection.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> to add to the collection.</param>
		// Token: 0x06002435 RID: 9269 RVA: 0x000DD05C File Offset: 0x000DB25C
		public void Add(SchemaImporterExtensionElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x06002436 RID: 9270 RVA: 0x000DD065 File Offset: 0x000DB265
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000DD06D File Offset: 0x000DB26D
		protected override ConfigurationElement CreateNewElement()
		{
			return new SchemaImporterExtensionElement();
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000DD074 File Offset: 0x000DB274
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SchemaImporterExtensionElement)element).Key;
		}

		/// <summary>Returns the zero-based index of the first element in the collection with the specified value.</summary>
		/// <returns>The index of the found element.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> to find.</param>
		// Token: 0x06002439 RID: 9273 RVA: 0x000DD081 File Offset: 0x000DB281
		public int IndexOf(SchemaImporterExtensionElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the first occurrence of a specific item from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> to remove.</param>
		// Token: 0x0600243A RID: 9274 RVA: 0x000DD08A File Offset: 0x000DB28A
		public void Remove(SchemaImporterExtensionElement element)
		{
			base.BaseRemove(element.Key);
		}

		/// <summary>Removes the item with the specified name from the collection.</summary>
		/// <param name="name">The name of the item to remove.</param>
		// Token: 0x0600243B RID: 9275 RVA: 0x000DD098 File Offset: 0x000DB298
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the item at the specified index from the collection.</summary>
		/// <param name="index">The index of the object to remove.</param>
		// Token: 0x0600243C RID: 9276 RVA: 0x000DD0A1 File Offset: 0x000DB2A1
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
