using System;
using System.Collections;
using System.Collections.Generic;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms
{
	/// <summary>Defines a collection of <see cref="T:System.Windows.Forms.HtmlElement" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B8 RID: 440
	public sealed class HtmlElementCollection : ICollection, IEnumerable
	{
		// Token: 0x06001CFF RID: 7423 RVA: 0x0006E954 File Offset: 0x0006CB54
		internal HtmlElementCollection(WebBrowser owner, IWebBrowser webHost, IElementCollection col)
		{
			this.elements = new List<HtmlElement>();
			foreach (object obj in col)
			{
				IElement element = (IElement)obj;
				this.elements.Add(new HtmlElement(owner, webHost, element));
			}
			this.webHost = webHost;
			this.owner = owner;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0006E9EC File Offset: 0x0006CBEC
		private HtmlElementCollection(WebBrowser owner, IWebBrowser webHost, List<HtmlElement> elems)
		{
			this.elements = elems;
			this.webHost = webHost;
			this.owner = owner;
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06001D01 RID: 7425 RVA: 0x0006EA0C File Offset: 0x0006CC0C
		void ICollection.CopyTo(Array dest, int index)
		{
			this.elements.CopyTo(dest as HtmlElement[], index);
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0006EA20 File Offset: 0x0006CC20
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.HtmlElementCollection" /> is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0006EA24 File Offset: 0x0006CC24
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the number of elements in the collection. </summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the number of elements in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0006EA28 File Offset: 0x0006CC28
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		/// <summary>Gets an item from the collection by specifying its name.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" />, if the named element is found. Otherwise, null.</returns>
		/// <param name="elementId">The <see cref="P:System.Windows.Forms.HtmlElement.Name" /> or <see cref="P:System.Windows.Forms.HtmlElement.Id" /> attribute of the element.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FE RID: 1790
		public HtmlElement this[string elementId]
		{
			get
			{
				foreach (HtmlElement htmlElement in this.elements)
				{
					if (htmlElement.Id.Equals(elementId))
					{
						return htmlElement;
					}
				}
				return null;
			}
		}

		/// <summary>Gets an item from the collection by specifying its numerical index.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" />.</returns>
		/// <param name="index">The position from which to retrieve an item from the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FF RID: 1791
		public HtmlElement this[int index]
		{
			get
			{
				if (index > this.elements.Count || index < 0)
				{
					return null;
				}
				return this.elements[index];
			}
		}

		/// <summary>Gets a collection of elements by their name.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> containing the elements whose <see cref="P:System.Windows.Forms.HtmlElement.Name" /> property match <paramref name="name" />. </returns>
		/// <param name="name">The name or ID of the element. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D07 RID: 7431 RVA: 0x0006EAE8 File Offset: 0x0006CCE8
		public HtmlElementCollection GetElementsByName(string name)
		{
			List<HtmlElement> list = new List<HtmlElement>();
			foreach (HtmlElement htmlElement in this.elements)
			{
				if (htmlElement.HasAttribute("name") && htmlElement.GetAttribute("name").Equals(name))
				{
					list.Add(new HtmlElement(this.owner, this.webHost, htmlElement.element));
				}
			}
			return new HtmlElementCollection(this.owner, this.webHost, list);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D08 RID: 7432 RVA: 0x0006EBA4 File Offset: 0x0006CDA4
		public IEnumerator GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x04000F6B RID: 3947
		private List<HtmlElement> elements;

		// Token: 0x04000F6C RID: 3948
		private IWebBrowser webHost;

		// Token: 0x04000F6D RID: 3949
		private WebBrowser owner;
	}
}
