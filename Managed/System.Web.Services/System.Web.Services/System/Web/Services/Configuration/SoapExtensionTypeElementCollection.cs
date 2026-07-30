using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace System.Web.Services.Configuration
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> objects.</summary>
	// Token: 0x02000145 RID: 325
	[ConfigurationCollection(typeof(SoapExtensionTypeElement))]
	public sealed class SoapExtensionTypeElementCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to add.</param>
		// Token: 0x060009E4 RID: 2532 RVA: 0x000436A1 File Offset: 0x000418A1
		public void Add(SoapExtensionTypeElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			this.BaseAdd(element);
		}

		/// <summary>Removes all <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> objects from the collection.</summary>
		// Token: 0x060009E5 RID: 2533 RVA: 0x000436B8 File Offset: 0x000418B8
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether a <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key exists in the collection.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to find in the collection.</param>
		// Token: 0x060009E6 RID: 2534 RVA: 0x000436C0 File Offset: 0x000418C0
		public bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return base.BaseGet(key) != null;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00043BE7 File Offset: 0x00041DE7
		protected override ConfigurationElement CreateNewElement()
		{
			return new SoapExtensionTypeElement();
		}

		/// <summary>Copies the elements from the collection to an array, starting at a specified index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to which to copy the contents of the collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060009E8 RID: 2536 RVA: 0x000436E1 File Offset: 0x000418E1
		public void CopyTo(SoapExtensionTypeElement[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00043BEE File Offset: 0x00041DEE
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return element;
		}

		/// <summary>Returns the zero-based index of a specified <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> in the collection.</summary>
		/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" />, or -1 if the element was not found in the collection.</returns>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to find in the collection.</param>
		// Token: 0x060009EA RID: 2538 RVA: 0x00043730 File Offset: 0x00041930
		public int IndexOf(SoapExtensionTypeElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes a specified <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to remove from the collection.</param>
		// Token: 0x060009EB RID: 2539 RVA: 0x00043747 File Offset: 0x00041947
		public void Remove(SoapExtensionTypeElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key from the collection.</summary>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to be removed from the collection.</param>
		// Token: 0x060009EC RID: 2540 RVA: 0x00043764 File Offset: 0x00041964
		public void RemoveAt(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			base.BaseRemove(key);
		}

		/// <summary>Removes the element at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to remove from the collection.</param>
		// Token: 0x060009ED RID: 2541 RVA: 0x0004377B File Offset: 0x0004197B
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> having the specified key in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to get or set in the collection.</param>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key was not found in the collection.</exception>
		// Token: 0x17000286 RID: 646
		public SoapExtensionTypeElement this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				SoapExtensionTypeElement soapExtensionTypeElement = (SoapExtensionTypeElement)base.BaseGet(key);
				if (soapExtensionTypeElement == null)
				{
					throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Res.GetString("ConfigKeyNotFoundInElementCollection"), key.ToString()));
				}
				return soapExtensionTypeElement;
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
		/// <returns>The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection.</param>
		// Token: 0x17000287 RID: 647
		public SoapExtensionTypeElement this[int index]
		{
			get
			{
				return (SoapExtensionTypeElement)base.BaseGet(index);
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
