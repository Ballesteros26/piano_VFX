using System;
using System.Collections;
using Unity;

namespace System.ComponentModel.Design
{
	/// <summary>Contains a collection of design surfaces. This class cannot be inherited.</summary>
	// Token: 0x02000106 RID: 262
	public sealed class DesignSurfaceCollection : ICollection, IEnumerable
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x0000CE93 File Offset: 0x0000B093
		internal DesignSurfaceCollection(DesignerCollection designers)
		{
			if (designers == null)
			{
				designers = new DesignerCollection(null);
			}
			this._designers = designers;
		}

		/// <summary>Gets the total number of design surfaces in the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</summary>
		/// <returns>The total number of elements in the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0000CEAD File Offset: 0x0000B0AD
		public int Count
		{
			get
			{
				return this._designers.Count;
			}
		}

		/// <summary>Gets the design surface at the specified index.</summary>
		/// <returns>The design surface at the specified index.</returns>
		/// <param name="index">The index of the design surface to return.</param>
		/// <exception cref="T:System.NotSupportedException">The design surface specified by <paramref name="index" /> is not supported.</exception>
		// Token: 0x170001C4 RID: 452
		public DesignSurface this[int index]
		{
			get
			{
				DesignSurface designSurface = this._designers[index].GetService(typeof(DesignSurface)) as DesignSurface;
				if (designSurface == null)
				{
					throw new NotSupportedException();
				}
				return designSurface;
			}
		}

		/// <summary>Copies the collection members to the specified <see cref="T:System.ComponentModel.Design.DesignSurface" /> array beginning at the specified destination index.</summary>
		/// <param name="array">The array to copy collection members to.</param>
		/// <param name="index">The destination index to begin copying to.</param>
		// Token: 0x060007AA RID: 1962 RVA: 0x0000CEE5 File Offset: 0x0000B0E5
		public void CopyTo(DesignSurface[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" /> method.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</param>
		/// <param name="index">The index in <paramref name="array" /> where copying begins.</param>
		// Token: 0x060007AB RID: 1963 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object obj in this)
			{
				DesignSurface designSurface = (DesignSurface)obj;
				array.SetValue(designSurface, index);
				index++;
			}
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" /> instance.</returns>
		// Token: 0x060007AC RID: 1964 RVA: 0x0000CF4C File Offset: 0x0000B14C
		public IEnumerator GetEnumerator()
		{
			return new DesignSurfaceCollection.DesignSurfaceEnumerator(this._designers.GetEnumerator());
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" /> instance.</returns>
		// Token: 0x060007AD RID: 1965 RVA: 0x0000CF5E File Offset: 0x0000B15E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.Count" /> property.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0000CF66 File Offset: 0x0000B166
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.IsSynchronized" /> property.</summary>
		/// <returns>true if access to the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000241E File Offset: 0x0000061E
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.SyncRoot" /> property.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0000256A File Offset: 0x0000076A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00009519 File Offset: 0x00007719
		internal DesignSurfaceCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040001A0 RID: 416
		private DesignerCollection _designers;

		// Token: 0x02000107 RID: 263
		private class DesignSurfaceEnumerator : IEnumerator
		{
			// Token: 0x060007B2 RID: 1970 RVA: 0x0000CF6E File Offset: 0x0000B16E
			public DesignSurfaceEnumerator(IEnumerator designerCollectionEnumerator)
			{
				this._designerCollectionEnumerator = designerCollectionEnumerator;
			}

			// Token: 0x060007B3 RID: 1971 RVA: 0x0000CF7D File Offset: 0x0000B17D
			public bool MoveNext()
			{
				return this._designerCollectionEnumerator.MoveNext();
			}

			// Token: 0x060007B4 RID: 1972 RVA: 0x0000CF8A File Offset: 0x0000B18A
			public void Reset()
			{
				this._designerCollectionEnumerator.Reset();
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000CF97 File Offset: 0x0000B197
			public object Current
			{
				get
				{
					DesignSurface designSurface = ((IDesignerHost)this._designerCollectionEnumerator.Current).GetService(typeof(DesignSurface)) as DesignSurface;
					if (designSurface == null)
					{
						throw new NotSupportedException();
					}
					return designSurface;
				}
			}

			// Token: 0x040001A1 RID: 417
			private IEnumerator _designerCollectionEnumerator;
		}
	}
}
