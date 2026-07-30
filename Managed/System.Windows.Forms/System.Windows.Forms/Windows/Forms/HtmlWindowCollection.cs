using System;
using System.Collections;
using System.Collections.Generic;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms
{
	/// <summary>Represents the windows contained within another <see cref="T:System.Windows.Forms.HtmlWindow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BE RID: 446
	public class HtmlWindowCollection : ICollection, IEnumerable
	{
		// Token: 0x06001D69 RID: 7529 RVA: 0x0006F6A8 File Offset: 0x0006D8A8
		internal HtmlWindowCollection(WebBrowser owner, IWebBrowser webHost, IWindowCollection col)
		{
			this.windows = new List<HtmlWindow>();
			foreach (object obj in col)
			{
				IWindow window = (IWindow)obj;
				this.windows.Add(new HtmlWindow(owner, webHost, window));
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in Array at which copying begins.</param>
		// Token: 0x06001D6A RID: 7530 RVA: 0x0006F730 File Offset: 0x0006D930
		void ICollection.CopyTo(Array dest, int index)
		{
			this.windows.CopyTo(dest as HtmlWindow[], index);
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0006F744 File Offset: 0x0006D944
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0006F748 File Offset: 0x0006D948
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the number of elements in the collection. </summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.HtmlWindow" /> objects in the current <see cref="T:System.Windows.Forms.HtmlWindowCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0006F74C File Offset: 0x0006D94C
		public int Count
		{
			get
			{
				return this.windows.Count;
			}
		}

		/// <summary>Retrieves a frame window by supplying the frame's name.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> element corresponding to the supplied name. </returns>
		/// <param name="windowId">The name of the <see cref="T:System.Windows.Forms.HtmlWindow" /> to retrieve.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="windowId" /> is not the name of a Frame object in the current document or in any of its children.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000724 RID: 1828
		public HtmlWindow this[string windowId]
		{
			get
			{
				foreach (HtmlWindow htmlWindow in this.windows)
				{
					if (htmlWindow.Name.Equals(windowId))
					{
						return htmlWindow;
					}
				}
				return null;
			}
		}

		/// <summary>Retrieves a frame window by supplying the frame's position in the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> corresponding to the requested frame.</returns>
		/// <param name="index">The position of the <see cref="T:System.Windows.Forms.HtmlWindow" /> within the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than the number of items in the collection.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000725 RID: 1829
		public HtmlWindow this[int index]
		{
			get
			{
				if (index > this.windows.Count || index < 0)
				{
					return null;
				}
				return this.windows[index];
			}
		}

		/// <summary>Returns an enumerator that can iterate through all elements in the <see cref="T:System.Windows.Forms.HtmlWindowCollection" />.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> that enables enumeration of this collection's elements.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D70 RID: 7536 RVA: 0x0006F80C File Offset: 0x0006DA0C
		public IEnumerator GetEnumerator()
		{
			return this.windows.GetEnumerator();
		}

		// Token: 0x04000F93 RID: 3987
		private List<HtmlWindow> windows;
	}
}
