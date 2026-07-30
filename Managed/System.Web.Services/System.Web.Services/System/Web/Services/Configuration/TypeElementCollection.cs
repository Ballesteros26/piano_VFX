using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace System.Web.Services.Configuration
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Web.Services.Configuration.TypeElement" /> objects.</summary>
	// Token: 0x02000149 RID: 329
	[ConfigurationCollection(typeof(TypeElement))]
	public sealed class TypeElementCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Services.Configuration.TypeElement" /> to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.TypeElement" /> to add.</param>
		// Token: 0x06000A01 RID: 2561 RVA: 0x000436A1 File Offset: 0x000418A1
		public void Add(TypeElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			this.BaseAdd(element);
		}

		/// <summary>Removes all <see cref="T:System.Web.Services.Configuration.TypeElement" /> objects from the collection.</summary>
		// Token: 0x06000A02 RID: 2562 RVA: 0x000436B8 File Offset: 0x000418B8
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether a <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key exists in the collection.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> to find in the collection.</param>
		// Token: 0x06000A03 RID: 2563 RVA: 0x000436C0 File Offset: 0x000418C0
		public bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return base.BaseGet(key) != null;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00043E71 File Offset: 0x00042071
		protected override ConfigurationElement CreateNewElement()
		{
			return new TypeElement();
		}

		/// <summary>Copies the elements from the collection to an array, starting at a specified index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Configuration.TypeElement" /> to which to copy the contents of the collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000A05 RID: 2565 RVA: 0x000436E1 File Offset: 0x000418E1
		public void CopyTo(TypeElement[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00043E78 File Offset: 0x00042078
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((TypeElement)element).Type;
		}

		/// <summary>Returns the zero-based index of a specified <see cref="T:System.Web.Services.Configuration.TypeElement" /> in the collection.</summary>
		/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Configuration.TypeElement" />, or -1 if the element was not found in the collection.</returns>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.TypeElement" /> to find in the collection.</param>
		// Token: 0x06000A07 RID: 2567 RVA: 0x00043730 File Offset: 0x00041930
		public int IndexOf(TypeElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes a specified <see cref="T:System.Web.Services.Configuration.TypeElement" /> from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.TypeElement" /> to remove from the collection.</param>
		// Token: 0x06000A08 RID: 2568 RVA: 0x00043747 File Offset: 0x00041947
		public void Remove(TypeElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes the <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key from the collection.</summary>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> to be removed from the collection.</param>
		// Token: 0x06000A09 RID: 2569 RVA: 0x00043764 File Offset: 0x00041964
		public void RemoveAt(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			base.BaseRemove(key);
		}

		/// <summary>Removes the element at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> to remove from the collection.</param>
		// Token: 0x06000A0A RID: 2570 RVA: 0x0004377B File Offset: 0x0004197B
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.TypeElement" /> that has the specified key in the collection. </summary>
		/// <returns>The <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> to get or set in the collection.</param>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key was not found in the collection.</exception>
		// Token: 0x1700028A RID: 650
		public TypeElement this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				TypeElement typeElement = (TypeElement)base.BaseGet(key);
				if (typeElement == null)
				{
					throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Res.GetString("ConfigKeyNotFoundInElementCollection"), key.ToString()));
				}
				return typeElement;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (this.GetElementKey(value).Equals(key))
				{
					if (base.BaseGet(key) != null)
					{
						base.BaseRemove(key);
					}
					this.Add(value);
					return;
				}
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Res.GetString("ConfigKeysDoNotMatch"), this.GetElementKey(value).ToString(), key.ToString()));
			}
		}

		/// <summary>Gets or sets the element at a specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Configuration.TypeElement" /> that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection.</param>
		// Token: 0x1700028B RID: 651
		public TypeElement this[int index]
		{
			get
			{
				return (TypeElement)base.BaseGet(index);
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
	}
}
