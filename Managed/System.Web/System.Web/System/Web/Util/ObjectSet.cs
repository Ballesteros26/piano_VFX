using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Util
{
	// Token: 0x02000121 RID: 289
	internal class ObjectSet : ICollection, IEnumerable
	{
		// Token: 0x06000E1E RID: 3614 RVA: 0x00002050 File Offset: 0x00000250
		internal ObjectSet()
		{
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool CaseInsensitive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0002649B File Offset: 0x0002469B
		public void Add(object o)
		{
			if (this._objects == null)
			{
				this._objects = new HybridDictionary(this.CaseInsensitive);
			}
			this._objects[o] = null;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000264C4 File Offset: 0x000246C4
		public void AddCollection(ICollection c)
		{
			foreach (object obj in c)
			{
				this.Add(obj);
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00026514 File Offset: 0x00024714
		public void Remove(object o)
		{
			if (this._objects == null)
			{
				return;
			}
			this._objects.Remove(o);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0002652B File Offset: 0x0002472B
		public bool Contains(object o)
		{
			return this._objects != null && this._objects.Contains(o);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00026543 File Offset: 0x00024743
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._objects == null)
			{
				return ObjectSet._emptyEnumerator;
			}
			return this._objects.Keys.GetEnumerator();
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00026563 File Offset: 0x00024763
		public int Count
		{
			get
			{
				if (this._objects == null)
				{
					return 0;
				}
				return this._objects.Keys.Count;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0002657F File Offset: 0x0002477F
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._objects == null || this._objects.Keys.IsSynchronized;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0002659B File Offset: 0x0002479B
		object ICollection.SyncRoot
		{
			get
			{
				if (this._objects == null)
				{
					return this;
				}
				return this._objects.Keys.SyncRoot;
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000265B7 File Offset: 0x000247B7
		public void CopyTo(Array array, int index)
		{
			if (this._objects != null)
			{
				this._objects.Keys.CopyTo(array, index);
			}
		}

		// Token: 0x040011BB RID: 4539
		private static ObjectSet.EmptyEnumerator _emptyEnumerator = new ObjectSet.EmptyEnumerator();

		// Token: 0x040011BC RID: 4540
		private IDictionary _objects;

		// Token: 0x02000122 RID: 290
		private class EmptyEnumerator : IEnumerator
		{
			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00003BEA File Offset: 0x00001DEA
			public object Current
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000E2B RID: 3627 RVA: 0x00008A69 File Offset: 0x00006C69
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x06000E2C RID: 3628 RVA: 0x0000393A File Offset: 0x00001B3A
			public void Reset()
			{
			}
		}
	}
}
