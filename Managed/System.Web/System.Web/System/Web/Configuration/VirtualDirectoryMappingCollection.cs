using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005ED RID: 1517
	[Serializable]
	public sealed class VirtualDirectoryMappingCollection : NameObjectCollectionBase
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object to the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		/// <param name="virtualDirectory">A <see cref="T:System.String" /> that contains the virtual directory path.</param>
		/// <param name="mapping">A <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mapping" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="virtualDirectory" /> already exists in the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> is read-only.</exception>
		// Token: 0x060041C3 RID: 16835 RVA: 0x000ABE1A File Offset: 0x000AA01A
		public void Add(string virtualDirectory, VirtualDirectoryMapping mapping)
		{
			mapping.SetVirtualDirectory(virtualDirectory);
			base.BaseAdd(virtualDirectory, mapping);
		}

		/// <summary>Clears all <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> objects from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		// Token: 0x060041C4 RID: 16836 RVA: 0x00010418 File Offset: 0x0000E618
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Array.Length" /> property of <paramref name="array" /> is less than the value of <see cref="P:System.Collections.Specialized.NameObjectCollectionBase.Count" /> plus <paramref name="index" />.</exception>
		// Token: 0x060041C5 RID: 16837 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(VirtualDirectoryMapping[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets the specified <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> collection element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> element at the specified index.</returns>
		/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
		// Token: 0x060041C6 RID: 16838 RVA: 0x000ABE2B File Offset: 0x000AA02B
		public VirtualDirectoryMapping Get(int index)
		{
			return (VirtualDirectoryMapping)base.BaseGet(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> collection element based on the specified virtual-directory name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> element based on the specified virtual-directory name.</returns>
		/// <param name="virtualDirectory">A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
		// Token: 0x060041C7 RID: 16839 RVA: 0x000ABE39 File Offset: 0x000AA039
		public VirtualDirectoryMapping Get(string virtualDirectory)
		{
			return (VirtualDirectoryMapping)base.BaseGet(virtualDirectory);
		}

		/// <summary>Gets the key of the entry at the specified index of the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		/// <returns>A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</returns>
		/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
		// Token: 0x060041C8 RID: 16840 RVA: 0x00011087 File Offset: 0x0000F287
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		/// <param name="virtualDirectory">A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
		// Token: 0x060041C9 RID: 16841 RVA: 0x00010455 File Offset: 0x0000E655
		public void Remove(string virtualDirectory)
		{
			base.BaseRemove(virtualDirectory);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object at the specified index from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</summary>
		/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
		// Token: 0x060041CA RID: 16842 RVA: 0x000ABE47 File Offset: 0x000AA047
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Returns a string array that contains all the keys in the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		/// <returns>A string array that contains all the keys in the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</returns>
		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x060041CB RID: 16843 RVA: 0x000110BE File Offset: 0x0000F2BE
		public ICollection AllKeys
		{
			get
			{
				return base.BaseGetAllKeys();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object at the specified index location.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</returns>
		/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
		// Token: 0x170014EF RID: 5359
		public VirtualDirectoryMapping this[int index]
		{
			get
			{
				return (VirtualDirectoryMapping)base.BaseGet(index);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object based on the specified virtual directory name.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</returns>
		/// <param name="virtualDirectory">A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
		// Token: 0x170014F0 RID: 5360
		public VirtualDirectoryMapping this[string virtualDirectory]
		{
			get
			{
				return (VirtualDirectoryMapping)base.BaseGet(virtualDirectory);
			}
		}
	}
}
