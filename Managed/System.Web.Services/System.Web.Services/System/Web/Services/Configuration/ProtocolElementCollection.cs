using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace System.Web.Services.Configuration
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> objects.</summary>
	// Token: 0x02000140 RID: 320
	[ConfigurationCollection(typeof(ProtocolElement))]
	public sealed class ProtocolElementCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to add.</param>
		// Token: 0x060009BE RID: 2494 RVA: 0x000436A1 File Offset: 0x000418A1
		public void Add(ProtocolElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			this.BaseAdd(element);
		}

		/// <summary>Removes all <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> objects from the collection.</summary>
		// Token: 0x060009BF RID: 2495 RVA: 0x000436B8 File Offset: 0x000418B8
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether a <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key exists in the collection.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to find in the collection.</param>
		// Token: 0x060009C0 RID: 2496 RVA: 0x000436C0 File Offset: 0x000418C0
		public bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return base.BaseGet(key) != null;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000436DA File Offset: 0x000418DA
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProtocolElement();
		}

		/// <summary>Copies the elements from the collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to which to copy the contents of the collection.</param>
		/// <param name="index">The zero-based index in <paramref name="Array" /> at which copying begins.</param>
		// Token: 0x060009C2 RID: 2498 RVA: 0x000436E1 File Offset: 0x000418E1
		public void CopyTo(ProtocolElement[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000436FC File Offset: 0x000418FC
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((ProtocolElement)element).Name.ToString();
		}

		/// <summary>Returns the zero-based index of a specified <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> in the collection.</summary>
		/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Configuration.ProtocolElement" />, or -1 if the element was not found in the collection.</returns>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to find in the collection.</param>
		// Token: 0x060009C4 RID: 2500 RVA: 0x00043730 File Offset: 0x00041930
		public int IndexOf(ProtocolElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes a specified <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to remove from the collection.</param>
		// Token: 0x060009C5 RID: 2501 RVA: 0x00043747 File Offset: 0x00041947
		public void Remove(ProtocolElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key from the collection.</summary>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to be removed from the collection.</param>
		// Token: 0x060009C6 RID: 2502 RVA: 0x00043764 File Offset: 0x00041964
		public void RemoveAt(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			base.BaseRemove(key);
		}

		/// <summary>Removes the element at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to remove from the collection.</param>
		// Token: 0x060009C7 RID: 2503 RVA: 0x0004377B File Offset: 0x0004197B
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00043784 File Offset: 0x00041984
		internal void SetDefaults()
		{
			ProtocolElement protocolElement = new ProtocolElement(WebServiceProtocols.HttpSoap12);
			ProtocolElement protocolElement2 = new ProtocolElement(WebServiceProtocols.HttpSoap);
			ProtocolElement protocolElement3 = new ProtocolElement(WebServiceProtocols.HttpPostLocalhost);
			ProtocolElement protocolElement4 = new ProtocolElement(WebServiceProtocols.Documentation);
			this.Add(protocolElement);
			this.Add(protocolElement2);
			this.Add(protocolElement3);
			this.Add(protocolElement4);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> that has the specified key in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to get or set in the collection.</param>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key was not found in the collection.</exception>
		// Token: 0x1700027D RID: 637
		public ProtocolElement this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				ProtocolElement protocolElement = (ProtocolElement)base.BaseGet(key);
				if (protocolElement == null)
				{
					throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Res.GetString("ConfigKeyNotFoundInElementCollection"), key.ToString()));
				}
				return protocolElement;
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
		/// <returns>The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection.</param>
		// Token: 0x1700027E RID: 638
		public ProtocolElement this[int index]
		{
			get
			{
				return (ProtocolElement)base.BaseGet(index);
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
