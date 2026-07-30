using System;
using System.Collections;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of captures made by a single capturing group. </summary>
	// Token: 0x0200013A RID: 314
	[Serializable]
	public class CaptureCollection : ICollection, IEnumerable
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x00029DED File Offset: 0x00027FED
		internal CaptureCollection(Group group)
		{
			this._group = group;
			this._capcount = this._group._capcount;
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00029E0D File Offset: 0x0002800D
		public object SyncRoot
		{
			get
			{
				return this._group;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000027E2 File Offset: 0x000009E2
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the number of substrings captured by the group.</summary>
		/// <returns>The number of items in the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00029E15 File Offset: 0x00028015
		public int Count
		{
			get
			{
				return this._capcount;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <param name="i">Index into the capture collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" />. </exception>
		// Token: 0x17000182 RID: 386
		public Capture this[int i]
		{
			get
			{
				return this.GetCapture(i);
			}
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the destination array where copying is to begin. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array " />is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />. -or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" /> is outside the bounds of <paramref name="array" />. </exception>
		// Token: 0x060008BF RID: 2239 RVA: 0x00029E28 File Offset: 0x00028028
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], num);
				num++;
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Capture" /> objects within the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x060008C0 RID: 2240 RVA: 0x00029E68 File Offset: 0x00028068
		public IEnumerator GetEnumerator()
		{
			return new CaptureEnumerator(this);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00029E70 File Offset: 0x00028070
		internal Capture GetCapture(int i)
		{
			if (i == this._capcount - 1 && i >= 0)
			{
				return this._group;
			}
			if (i >= this._capcount || i < 0)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (this._captures == null)
			{
				this._captures = new Capture[this._capcount];
				for (int j = 0; j < this._capcount - 1; j++)
				{
					this._captures[j] = new Capture(this._group._text, this._group._caps[j * 2], this._group._caps[j * 2 + 1]);
				}
			}
			return this._captures[i];
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal CaptureCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000DD9 RID: 3545
		internal Group _group;

		// Token: 0x04000DDA RID: 3546
		internal int _capcount;

		// Token: 0x04000DDB RID: 3547
		internal Capture[] _captures;
	}
}
