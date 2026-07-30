using System;
using System.Collections;

namespace System.Xml.Serialization.Advanced
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> objects.</summary>
	// Token: 0x02000381 RID: 897
	public class SchemaImporterExtensionCollection : CollectionBase
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x000DD649 File Offset: 0x000DB849
		internal Hashtable Names
		{
			get
			{
				if (this.exNames == null)
				{
					this.exNames = new Hashtable();
				}
				return this.exNames;
			}
		}

		/// <summary>Adds the specified importer extension to the collection.</summary>
		/// <returns>The index of the added extension.</returns>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to add.</param>
		// Token: 0x06002458 RID: 9304 RVA: 0x000DD664 File Offset: 0x000DB864
		public int Add(SchemaImporterExtension extension)
		{
			return this.Add(extension.GetType().FullName, extension);
		}

		/// <summary>Adds the specified importer extension to the collection. The name parameter allows you to supply a custom name for the extension.</summary>
		/// <returns>The index of the newly added item.</returns>
		/// <param name="name">A custom name for the extension.</param>
		/// <param name="type">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to add.</param>
		/// <exception cref="T:System.ArgumentException">The value of type does not inherit from <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" />.</exception>
		// Token: 0x06002459 RID: 9305 RVA: 0x000DD678 File Offset: 0x000DB878
		public int Add(string name, Type type)
		{
			if (type.IsSubclassOf(typeof(SchemaImporterExtension)))
			{
				return this.Add(name, (SchemaImporterExtension)Activator.CreateInstance(type));
			}
			throw new ArgumentException(Res.GetString("'{0}' is not a valid SchemaExtensionType.", new object[] { type }));
		}

		/// <summary>Removes the <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" />, specified by name, from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to remove. The name is set using the <see cref="M:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection.Add(System.String,System.Type)" /> method.</param>
		// Token: 0x0600245A RID: 9306 RVA: 0x000DD6B8 File Offset: 0x000DB8B8
		public void Remove(string name)
		{
			if (this.Names[name] != null)
			{
				base.List.Remove(this.Names[name]);
				this.Names[name] = null;
			}
		}

		/// <summary>Clears the collection of importer extensions.</summary>
		// Token: 0x0600245B RID: 9307 RVA: 0x000DD6EC File Offset: 0x000DB8EC
		public new void Clear()
		{
			this.Names.Clear();
			base.List.Clear();
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000DD704 File Offset: 0x000DB904
		internal SchemaImporterExtensionCollection Clone()
		{
			SchemaImporterExtensionCollection schemaImporterExtensionCollection = new SchemaImporterExtensionCollection();
			schemaImporterExtensionCollection.exNames = (Hashtable)this.Names.Clone();
			foreach (object obj in base.List)
			{
				schemaImporterExtensionCollection.List.Add(obj);
			}
			return schemaImporterExtensionCollection;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> at the specified index.</returns>
		/// <param name="index">The index of the item to find.</param>
		// Token: 0x17000731 RID: 1841
		public SchemaImporterExtension this[int index]
		{
			get
			{
				return (SchemaImporterExtension)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000DD790 File Offset: 0x000DB990
		internal int Add(string name, SchemaImporterExtension extension)
		{
			if (this.Names[name] == null)
			{
				this.Names[name] = extension;
				return base.List.Add(extension);
			}
			if (this.Names[name].GetType() != extension.GetType())
			{
				throw new InvalidOperationException(Res.GetString("Duplicate extension name.  schemaImporterExtension with name '{0}' already been added.", new object[] { name }));
			}
			return -1;
		}

		/// <summary>Inserts the specified <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-base index at which the <paramref name="extension" /> should be inserted.</param>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to insert.</param>
		// Token: 0x06002460 RID: 9312 RVA: 0x000A6A98 File Offset: 0x000A4C98
		public void Insert(int index, SchemaImporterExtension extension)
		{
			base.List.Insert(index, extension);
		}

		/// <summary>Searches for the specified item and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>The index of the found item.</returns>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to search for.</param>
		// Token: 0x06002461 RID: 9313 RVA: 0x000A6AA7 File Offset: 0x000A4CA7
		public int IndexOf(SchemaImporterExtension extension)
		{
			return base.List.IndexOf(extension);
		}

		/// <summary>Gets a value that indicates whether the specified importer extension exists in the collection.</summary>
		/// <returns>true if the extension is found; otherwise, false.</returns>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to search for.</param>
		// Token: 0x06002462 RID: 9314 RVA: 0x000A6AB5 File Offset: 0x000A4CB5
		public bool Contains(SchemaImporterExtension extension)
		{
			return base.List.Contains(extension);
		}

		/// <summary>Removes the specified <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> from the collection.</summary>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to remove. </param>
		// Token: 0x06002463 RID: 9315 RVA: 0x000A6AC3 File Offset: 0x000A4CC3
		public void Remove(SchemaImporterExtension extension)
		{
			base.List.Remove(extension);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to the specified array of <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> objects at the specified index. </summary>
		/// <param name="array">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to copy the current collection to.</param>
		/// <param name="index">The zero-based index at which the collection is added.</param>
		// Token: 0x06002464 RID: 9316 RVA: 0x000A6AD1 File Offset: 0x000A4CD1
		public void CopyTo(SchemaImporterExtension[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x040018BD RID: 6333
		private Hashtable exNames;
	}
}
