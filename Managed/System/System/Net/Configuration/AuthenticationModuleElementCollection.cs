using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for authentication module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x02000692 RID: 1682
	[ConfigurationCollection(typeof(AuthenticationModuleElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class AuthenticationModuleElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> class. </summary>
		// Token: 0x060034C1 RID: 13505 RVA: 0x0003C14F File Offset: 0x0003A34F
		[MonoTODO]
		public AuthenticationModuleElementCollection()
		{
		}

		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000C95 RID: 3221
		[MonoTODO]
		public AuthenticationModuleElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> with the specified key or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection. </param>
		// Token: 0x17000C96 RID: 3222
		[MonoTODO]
		public AuthenticationModuleElement this[string name]
		{
			get
			{
				return (AuthenticationModuleElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> to add to the collection.</param>
		// Token: 0x060034C6 RID: 13510 RVA: 0x0003C157 File Offset: 0x0003A357
		public void Add(AuthenticationModuleElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x060034C7 RID: 13511 RVA: 0x0003C160 File Offset: 0x0003A360
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000C3B8C File Offset: 0x000C1D8C
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthenticationModuleElement();
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000C3B93 File Offset: 0x000C1D93
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is AuthenticationModuleElement))
			{
				throw new ArgumentException("element");
			}
			return ((AuthenticationModuleElement)element).Type;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.AuthenticationModuleElement" />.</param>
		// Token: 0x060034CA RID: 13514 RVA: 0x000C3BB3 File Offset: 0x000C1DB3
		public int IndexOf(AuthenticationModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> to remove.</param>
		// Token: 0x060034CB RID: 13515 RVA: 0x000C3BBC File Offset: 0x000C1DBC
		public void Remove(AuthenticationModuleElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x060034CC RID: 13516 RVA: 0x000C3BBC File Offset: 0x000C1DBC
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x060034CD RID: 13517 RVA: 0x000C3BC5 File Offset: 0x000C1DC5
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
