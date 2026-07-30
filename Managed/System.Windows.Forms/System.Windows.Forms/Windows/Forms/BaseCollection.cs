using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides the base functionality for creating data-related collections in the <see cref="N:System.Windows.Forms" /> namespace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000056 RID: 86
	public class BaseCollection : MarshalByRefObject, ICollection, IEnumerable
	{
		/// <summary>Gets the total number of elements in the collection.</summary>
		/// <returns>The total number of elements in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00011E24 File Offset: 0x00010024
		[EditorBrowsable(2)]
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>This property is always false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00011E34 File Offset: 0x00010034
		[EditorBrowsable(2)]
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized.</summary>
		/// <returns>This property always returns false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00011E38 File Offset: 0x00010038
		[Browsable(false)]
		[EditorBrowsable(2)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.BaseCollection" />.</summary>
		/// <returns>An object that can be used to synchronize the <see cref="T:System.Windows.Forms.BaseCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00011E3C File Offset: 0x0001003C
		[EditorBrowsable(2)]
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Windows.Forms.BaseCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns null unless overridden in a derived class.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00011E40 File Offset: 0x00010040
		protected virtual ArrayList List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		/// <summary>Copies all the elements of the current one-dimensional <see cref="T:System.Array" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current Array. </param>
		/// <param name="index">The zero-based relative index in <paramref name="ar" /> at which copying begins. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600033D RID: 829 RVA: 0x00011E60 File Offset: 0x00010060
		public void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		/// <summary>Gets the object that enables iterating through the members of the collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600033E RID: 830 RVA: 0x00011E70 File Offset: 0x00010070
		public IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		// Token: 0x04000605 RID: 1541
		internal ArrayList list;
	}
}
