using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for connection management configuration elements. This class cannot be inherited.</summary>
	// Token: 0x02000697 RID: 1687
	[ConfigurationCollection(typeof(ConnectionManagementElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ConnectionManagementElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000CA1 RID: 3233
		[MonoTODO]
		public ConnectionManagementElement this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> with the specified key or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection. </param>
		// Token: 0x17000CA2 RID: 3234
		public ConnectionManagementElement this[string name]
		{
			get
			{
				return (ConnectionManagementElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to add to the collection.</param>
		// Token: 0x060034F5 RID: 13557 RVA: 0x0003C157 File Offset: 0x0003A357
		public void Add(ConnectionManagementElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x060034F6 RID: 13558 RVA: 0x0003C160 File Offset: 0x0003A360
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000C3DA0 File Offset: 0x000C1FA0
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionManagementElement();
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000C3DA7 File Offset: 0x000C1FA7
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is ConnectionManagementElement))
			{
				throw new ArgumentException("element");
			}
			return ((ConnectionManagementElement)element).Address;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.ConnectionManagementElement" />.</param>
		// Token: 0x060034F9 RID: 13561 RVA: 0x000C3BB3 File Offset: 0x000C1DB3
		public int IndexOf(ConnectionManagementElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to remove.</param>
		// Token: 0x060034FA RID: 13562 RVA: 0x000C3BBC File Offset: 0x000C1DBC
		public void Remove(ConnectionManagementElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x060034FB RID: 13563 RVA: 0x000C3BBC File Offset: 0x000C1DBC
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x060034FC RID: 13564 RVA: 0x000C3BC5 File Offset: 0x000C1DC5
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
