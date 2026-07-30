using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for Web request module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x020006B5 RID: 1717
	[ConfigurationCollection(typeof(WebRequestModuleElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class WebRequestModuleElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000CFE RID: 3326
		[MonoTODO]
		public WebRequestModuleElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> with the specified key or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection.</param>
		// Token: 0x17000CFF RID: 3327
		[MonoTODO]
		public WebRequestModuleElement this[string name]
		{
			get
			{
				return (WebRequestModuleElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> to add to the collection.</param>
		// Token: 0x060035D4 RID: 13780 RVA: 0x0003C157 File Offset: 0x0003A357
		public void Add(WebRequestModuleElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x060035D5 RID: 13781 RVA: 0x0003C160 File Offset: 0x0003A360
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000C5B6A File Offset: 0x000C3D6A
		protected override ConfigurationElement CreateNewElement()
		{
			return new WebRequestModuleElement();
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000C5B71 File Offset: 0x000C3D71
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is WebRequestModuleElement))
			{
				throw new ArgumentException("element");
			}
			return ((WebRequestModuleElement)element).Prefix;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.WebRequestModuleElement" />.</param>
		// Token: 0x060035D8 RID: 13784 RVA: 0x000C3BB3 File Offset: 0x000C1DB3
		public int IndexOf(WebRequestModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> to remove.</param>
		// Token: 0x060035D9 RID: 13785 RVA: 0x000C5B91 File Offset: 0x000C3D91
		public void Remove(WebRequestModuleElement element)
		{
			base.BaseRemove(element.Prefix);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x060035DA RID: 13786 RVA: 0x000C3BBC File Offset: 0x000C1DBC
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x060035DB RID: 13787 RVA: 0x000C3BC5 File Offset: 0x000C1DC5
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
