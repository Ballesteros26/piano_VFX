using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace System.Web.Services.Configuration
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> objects.</summary>
	// Token: 0x0200014D RID: 333
	[ConfigurationCollection(typeof(WsiProfilesElement))]
	public sealed class WsiProfilesElementCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> to add.</param>
		// Token: 0x06000A4B RID: 2635 RVA: 0x000436A1 File Offset: 0x000418A1
		public void Add(WsiProfilesElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			this.BaseAdd(element);
		}

		/// <summary>Removes all <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> objects from the collection.</summary>
		// Token: 0x06000A4C RID: 2636 RVA: 0x000436B8 File Offset: 0x000418B8
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether a <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> with the specified key exists in the collection.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> to find in the collection.</param>
		// Token: 0x06000A4D RID: 2637 RVA: 0x000436C0 File Offset: 0x000418C0
		public bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return base.BaseGet(key) != null;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0004549D File Offset: 0x0004369D
		protected override ConfigurationElement CreateNewElement()
		{
			return new WsiProfilesElement();
		}

		/// <summary>Copies the elements from the collection to an array, starting at a specified index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> to which to copy the contents of the collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000A4F RID: 2639 RVA: 0x000436E1 File Offset: 0x000418E1
		public void CopyTo(WsiProfilesElement[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000454A4 File Offset: 0x000436A4
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((WsiProfilesElement)element).Name.ToString();
		}

		/// <summary>Returns the zero-based index of a specified <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> in the collection.</summary>
		/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" />, or -1 if the element was not found in the collection.</returns>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> to find in the collection.</param>
		// Token: 0x06000A51 RID: 2641 RVA: 0x00043730 File Offset: 0x00041930
		public int IndexOf(WsiProfilesElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes a specified <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> to remove from the collection.</param>
		// Token: 0x06000A52 RID: 2642 RVA: 0x00043747 File Offset: 0x00041947
		public void Remove(WsiProfilesElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> with the specified key from the collection.</summary>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> to be removed from the collection.</param>
		// Token: 0x06000A53 RID: 2643 RVA: 0x00043764 File Offset: 0x00041964
		public void RemoveAt(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			base.BaseRemove(key);
		}

		/// <summary>Removes the element at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> to remove from the collection.</param>
		// Token: 0x06000A54 RID: 2644 RVA: 0x0004377B File Offset: 0x0004197B
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000454D8 File Offset: 0x000436D8
		internal void SetDefaults()
		{
			WsiProfilesElement wsiProfilesElement = new WsiProfilesElement(WsiProfiles.BasicProfile1_1);
			this.Add(wsiProfilesElement);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> that has the specified key in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> with the specified key.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> to get or set in the collection.</param>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> with the specified key was not found in the collection.</exception>
		// Token: 0x170002AE RID: 686
		public WsiProfilesElement this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				WsiProfilesElement wsiProfilesElement = (WsiProfilesElement)base.BaseGet(key);
				if (wsiProfilesElement == null)
				{
					throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Res.GetString("ConfigKeyNotFoundInElementCollection"), key.ToString()));
				}
				return wsiProfilesElement;
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
		/// <returns>The <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection.</param>
		// Token: 0x170002AF RID: 687
		public WsiProfilesElement this[int index]
		{
			get
			{
				return (WsiProfilesElement)base.BaseGet(index);
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
