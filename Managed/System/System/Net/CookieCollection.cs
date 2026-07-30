using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides a collection container for instances of the <see cref="T:System.Net.Cookie" /> class.</summary>
	// Token: 0x020004B6 RID: 1206
	[Serializable]
	public class CookieCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieCollection" /> class.</summary>
		// Token: 0x06002398 RID: 9112 RVA: 0x0008A69C File Offset: 0x0008889C
		public CookieCollection()
		{
			this.m_IsReadOnly = true;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0008A6C1 File Offset: 0x000888C1
		internal CookieCollection(bool IsReadOnly)
		{
			this.m_IsReadOnly = IsReadOnly;
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Net.CookieCollection" /> is read-only.</summary>
		/// <returns>true if this is a read-only <see cref="T:System.Net.CookieCollection" />; otherwise, false. The default is true.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x0008A6E6 File Offset: 0x000888E6
		public bool IsReadOnly
		{
			get
			{
				return this.m_IsReadOnly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>A <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Net.Cookie" /> to be found. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or <paramref name="index" /> is greater than or equal to <see cref="P:System.Net.CookieCollection.Count" />. </exception>
		// Token: 0x17000757 RID: 1879
		public Cookie this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (Cookie)this.m_list[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Cookie" /> with a specific name from a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>The <see cref="T:System.Net.Cookie" /> with a specific name from a <see cref="T:System.Net.CookieCollection" />.</returns>
		/// <param name="name">The name of the <see cref="T:System.Net.Cookie" /> to be found. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x17000758 RID: 1880
		public Cookie this[string name]
		{
			get
			{
				foreach (object obj in this.m_list)
				{
					Cookie cookie = (Cookie)obj;
					if (string.Compare(cookie.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return cookie;
					}
				}
				return null;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to be added to a <see cref="T:System.Net.CookieCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is null. </exception>
		// Token: 0x0600239D RID: 9117 RVA: 0x0008A788 File Offset: 0x00088988
		public void Add(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			this.m_version++;
			int num = this.IndexOf(cookie);
			if (num == -1)
			{
				this.m_list.Add(cookie);
				return;
			}
			this.m_list[num] = cookie;
		}

		/// <summary>Adds the contents of a <see cref="T:System.Net.CookieCollection" /> to the current instance.</summary>
		/// <param name="cookies">The <see cref="T:System.Net.CookieCollection" /> to be added. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookies" /> is null. </exception>
		// Token: 0x0600239E RID: 9118 RVA: 0x0008A7D8 File Offset: 0x000889D8
		public void Add(CookieCollection cookies)
		{
			if (cookies == null)
			{
				throw new ArgumentNullException("cookies");
			}
			foreach (object obj in cookies)
			{
				Cookie cookie = (Cookie)obj;
				this.Add(cookie);
			}
		}

		/// <summary>Gets the number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>The number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x0008A83C File Offset: 0x00088A3C
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to a <see cref="T:System.Net.CookieCollection" /> is thread safe.</summary>
		/// <returns>true if access to the <see cref="T:System.Net.CookieCollection" /> is thread safe; otherwise, false. The default is false.</returns>
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An object to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x00002068 File Offset: 0x00000268
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of a <see cref="T:System.Net.CookieCollection" /> to an instance of the <see cref="T:System.Array" /> class, starting at a particular index.</summary>
		/// <param name="array">The target <see cref="T:System.Array" /> to which the <see cref="T:System.Net.CookieCollection" /> will be copied. </param>
		/// <param name="index">The zero-based index in the target <see cref="T:System.Array" /> where copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.CookieCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.CookieCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x060023A2 RID: 9122 RVA: 0x0008A849 File Offset: 0x00088A49
		public void CopyTo(Array array, int index)
		{
			this.m_list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of this <see cref="T:System.Net.CookieCollection" /> to a <see cref="T:System.Net.Cookie" /> array starting at the specified index of the target array.</summary>
		/// <param name="array">The target <see cref="T:System.Net.Cookie" /> array to which the <see cref="T:System.Net.CookieCollection" /> will be copied.</param>
		/// <param name="index">The zero-based index in the target <see cref="T:System.Array" /> where copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.CookieCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.CookieCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x060023A3 RID: 9123 RVA: 0x0008A849 File Offset: 0x00088A49
		public void CopyTo(Cookie[] array, int index)
		{
			this.m_list.CopyTo(array, index);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0008A858 File Offset: 0x00088A58
		internal DateTime TimeStamp(CookieCollection.Stamp how)
		{
			switch (how)
			{
			case CookieCollection.Stamp.Set:
				this.m_TimeStamp = DateTime.Now;
				break;
			case CookieCollection.Stamp.SetToUnused:
				this.m_TimeStamp = DateTime.MinValue;
				break;
			case CookieCollection.Stamp.SetToMaxUsed:
				this.m_TimeStamp = DateTime.MaxValue;
				break;
			}
			return this.m_TimeStamp;
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x0008A8A8 File Offset: 0x00088AA8
		internal bool IsOtherVersionSeen
		{
			get
			{
				return this.m_has_other_versions;
			}
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x0008A8B0 File Offset: 0x00088AB0
		internal int InternalAdd(Cookie cookie, bool isStrict)
		{
			int num = 1;
			if (isStrict)
			{
				IComparer comparer = Cookie.GetComparer();
				int num2 = 0;
				foreach (object obj in this.m_list)
				{
					Cookie cookie2 = (Cookie)obj;
					if (comparer.Compare(cookie, cookie2) == 0)
					{
						num = 0;
						if (cookie2.Variant <= cookie.Variant)
						{
							this.m_list[num2] = cookie;
							break;
						}
						break;
					}
					else
					{
						num2++;
					}
				}
				if (num2 == this.m_list.Count)
				{
					this.m_list.Add(cookie);
				}
			}
			else
			{
				this.m_list.Add(cookie);
			}
			if (cookie.Version != 1)
			{
				this.m_has_other_versions = true;
			}
			return num;
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x0008A980 File Offset: 0x00088B80
		internal int IndexOf(Cookie cookie)
		{
			IComparer comparer = Cookie.GetComparer();
			int num = 0;
			foreach (object obj in this.m_list)
			{
				Cookie cookie2 = (Cookie)obj;
				if (comparer.Compare(cookie, cookie2) == 0)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0008A9F4 File Offset: 0x00088BF4
		internal void RemoveAt(int idx)
		{
			this.m_list.RemoveAt(idx);
		}

		/// <summary>Gets an enumerator that can iterate through a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An instance of an implementation of an <see cref="T:System.Collections.IEnumerator" /> interface that can iterate through a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x060023A9 RID: 9129 RVA: 0x0008AA02 File Offset: 0x00088C02
		public IEnumerator GetEnumerator()
		{
			return new CookieCollection.CookieCollectionEnumerator(this);
		}

		// Token: 0x04001FD1 RID: 8145
		internal int m_version;

		// Token: 0x04001FD2 RID: 8146
		private ArrayList m_list = new ArrayList();

		// Token: 0x04001FD3 RID: 8147
		private DateTime m_TimeStamp = DateTime.MinValue;

		// Token: 0x04001FD4 RID: 8148
		private bool m_has_other_versions;

		// Token: 0x04001FD5 RID: 8149
		[OptionalField]
		private bool m_IsReadOnly;

		// Token: 0x020004B7 RID: 1207
		internal enum Stamp
		{
			// Token: 0x04001FD7 RID: 8151
			Check,
			// Token: 0x04001FD8 RID: 8152
			Set,
			// Token: 0x04001FD9 RID: 8153
			SetToUnused,
			// Token: 0x04001FDA RID: 8154
			SetToMaxUsed
		}

		// Token: 0x020004B8 RID: 1208
		private class CookieCollectionEnumerator : IEnumerator
		{
			// Token: 0x060023AA RID: 9130 RVA: 0x0008AA0A File Offset: 0x00088C0A
			internal CookieCollectionEnumerator(CookieCollection cookies)
			{
				this.m_cookies = cookies;
				this.m_count = cookies.Count;
				this.m_version = cookies.m_version;
			}

			// Token: 0x1700075D RID: 1885
			// (get) Token: 0x060023AB RID: 9131 RVA: 0x0008AA38 File Offset: 0x00088C38
			object IEnumerator.Current
			{
				get
				{
					if (this.m_index < 0 || this.m_index >= this.m_count)
					{
						throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
					}
					if (this.m_version != this.m_cookies.m_version)
					{
						throw new InvalidOperationException(global::SR.GetString("Collection was modified; enumeration operation may not execute."));
					}
					return this.m_cookies[this.m_index];
				}
			}

			// Token: 0x060023AC RID: 9132 RVA: 0x0008AAA0 File Offset: 0x00088CA0
			bool IEnumerator.MoveNext()
			{
				if (this.m_version != this.m_cookies.m_version)
				{
					throw new InvalidOperationException(global::SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				int num = this.m_index + 1;
				this.m_index = num;
				if (num < this.m_count)
				{
					return true;
				}
				this.m_index = this.m_count;
				return false;
			}

			// Token: 0x060023AD RID: 9133 RVA: 0x0008AAF8 File Offset: 0x00088CF8
			void IEnumerator.Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x04001FDB RID: 8155
			private CookieCollection m_cookies;

			// Token: 0x04001FDC RID: 8156
			private int m_count;

			// Token: 0x04001FDD RID: 8157
			private int m_index = -1;

			// Token: 0x04001FDE RID: 8158
			private int m_version;
		}
	}
}
