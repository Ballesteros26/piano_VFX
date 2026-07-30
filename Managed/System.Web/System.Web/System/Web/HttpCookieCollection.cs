using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Provides a type-safe way to manipulate HTTP cookies.</summary>
	// Token: 0x02000091 RID: 145
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpCookieCollection : NameObjectCollectionBase
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x00010341 File Offset: 0x0000E541
		[Obsolete("Don't use this constructor, use the (bool, bool) one, as it's more clear what it does")]
		internal HttpCookieCollection(HttpResponse Response, bool ReadOnly)
			: base(StringComparer.OrdinalIgnoreCase)
		{
			this.auto_fill = Response != null;
			base.IsReadOnly = ReadOnly;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001035F File Offset: 0x0000E55F
		internal HttpCookieCollection(bool auto_fill, bool read_only)
			: base(StringComparer.OrdinalIgnoreCase)
		{
			this.auto_fill = auto_fill;
			base.IsReadOnly = read_only;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001037C File Offset: 0x0000E57C
		internal HttpCookieCollection(string cookies)
			: base(StringComparer.OrdinalIgnoreCase)
		{
			if (string.IsNullOrEmpty(cookies))
			{
				return;
			}
			foreach (string text in cookies.Split(new char[] { ';' }))
			{
				int num = text.IndexOf('=');
				if (num != -1)
				{
					string text2 = text.Substring(0, num);
					string text3 = text.Substring(num + 1);
					this.Add(new HttpCookie(text2.Trim(), text3.Trim()));
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCookieCollection" /> class.</summary>
		// Token: 0x06000708 RID: 1800 RVA: 0x000103FC File Offset: 0x0000E5FC
		public HttpCookieCollection()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		/// <summary>Adds the specified cookie to the cookie collection.</summary>
		/// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> to add to the collection. </param>
		// Token: 0x06000709 RID: 1801 RVA: 0x00010409 File Offset: 0x0000E609
		public void Add(HttpCookie cookie)
		{
			base.BaseAdd(cookie.Name, cookie);
		}

		/// <summary>Clears all cookies from the cookie collection.</summary>
		// Token: 0x0600070A RID: 1802 RVA: 0x00010418 File Offset: 0x0000E618
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Copies members of the cookie collection to an <see cref="T:System.Array" /> beginning at the specified index of the array.</summary>
		/// <param name="dest">The destination <see cref="T:System.Array" />. </param>
		/// <param name="index">The index of the destination array where copying starts. </param>
		// Token: 0x0600070B RID: 1803 RVA: 0x00010420 File Offset: 0x0000E620
		public void CopyTo(Array dest, int index)
		{
			base.BaseGetAllValues().CopyTo(dest, index);
		}

		/// <summary>Returns the key (name) of the cookie at the specified numerical index.</summary>
		/// <returns>The name of the cookie specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the key to retrieve from the collection. </param>
		// Token: 0x0600070C RID: 1804 RVA: 0x00010430 File Offset: 0x0000E630
		public string GetKey(int index)
		{
			HttpCookie httpCookie = (HttpCookie)base.BaseGet(index);
			if (httpCookie == null)
			{
				return null;
			}
			return httpCookie.Name;
		}

		/// <summary>Removes the cookie with the specified name from the collection.</summary>
		/// <param name="name">The name of the cookie to remove from the collection. </param>
		// Token: 0x0600070D RID: 1805 RVA: 0x00010455 File Offset: 0x0000E655
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Updates the value of an existing cookie in a cookie collection.</summary>
		/// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> object to update. </param>
		// Token: 0x0600070E RID: 1806 RVA: 0x0001045E File Offset: 0x0000E65E
		public void Set(HttpCookie cookie)
		{
			base.BaseSet(cookie.Name, cookie);
		}

		/// <summary>Returns the <see cref="T:System.Web.HttpCookie" /> item with the specified index from the cookie collection.</summary>
		/// <returns>The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the cookie to return from the collection. </param>
		// Token: 0x0600070F RID: 1807 RVA: 0x0001046D File Offset: 0x0000E66D
		public HttpCookie Get(int index)
		{
			return (HttpCookie)base.BaseGet(index);
		}

		/// <summary>Returns the cookie with the specified name from the cookie collection.</summary>
		/// <returns>The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="name" />.</returns>
		/// <param name="name">The name of the cookie to retrieve from the collection. </param>
		// Token: 0x06000710 RID: 1808 RVA: 0x0001047B File Offset: 0x0000E67B
		public HttpCookie Get(string name)
		{
			return this[name];
		}

		/// <summary>Gets the cookie with the specified numerical index from the cookie collection.</summary>
		/// <returns>The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the cookie to retrieve from the collection. </param>
		// Token: 0x170002B8 RID: 696
		public HttpCookie this[int index]
		{
			get
			{
				return (HttpCookie)base.BaseGet(index);
			}
		}

		/// <summary>Gets the cookie with the specified name from the cookie collection.</summary>
		/// <returns>The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="name." /></returns>
		/// <param name="name">Name of cookie to retrieve. </param>
		// Token: 0x170002B9 RID: 697
		public HttpCookie this[string name]
		{
			get
			{
				HttpCookie httpCookie = (HttpCookie)base.BaseGet(name);
				if (!base.IsReadOnly && this.auto_fill && httpCookie == null)
				{
					httpCookie = new HttpCookie(name);
					base.BaseAdd(name, httpCookie);
				}
				return httpCookie;
			}
		}

		/// <summary>Gets a string array containing all the keys (cookie names) in the cookie collection.</summary>
		/// <returns>An array of cookie names.</returns>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x000104C4 File Offset: 0x0000E6C4
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[this.Keys.Count];
				((ICollection)this.Keys).CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04000F58 RID: 3928
		private bool auto_fill;
	}
}
