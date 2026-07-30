using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for the addresses of resources that bypass the proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000695 RID: 1685
	[ConfigurationCollection(typeof(BypassElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class BypassElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000C9B RID: 3227
		[MonoTODO]
		public BypassElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> with the specified key, or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection. </param>
		// Token: 0x17000C9C RID: 3228
		public BypassElement this[string name]
		{
			get
			{
				return (BypassElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x00004240 File Offset: 0x00002440
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to add to the collection.</param>
		// Token: 0x060034E0 RID: 13536 RVA: 0x0003C157 File Offset: 0x0003A357
		public void Add(BypassElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x060034E1 RID: 13537 RVA: 0x0003C160 File Offset: 0x0003A360
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000C3C97 File Offset: 0x000C1E97
		protected override ConfigurationElement CreateNewElement()
		{
			return new BypassElement();
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000C3C9E File Offset: 0x000C1E9E
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is BypassElement))
			{
				throw new ArgumentException("element");
			}
			return ((BypassElement)element).Address;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.BypassElement" />.</param>
		// Token: 0x060034E4 RID: 13540 RVA: 0x000C3BB3 File Offset: 0x000C1DB3
		public int IndexOf(BypassElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to remove.</param>
		// Token: 0x060034E5 RID: 13541 RVA: 0x000C3BBC File Offset: 0x000C1DBC
		public void Remove(BypassElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x060034E6 RID: 13542 RVA: 0x000C3BBC File Offset: 0x000C1DBC
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x060034E7 RID: 13543 RVA: 0x000C3BC5 File Offset: 0x000C1DC5
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
