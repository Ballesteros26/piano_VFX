using System;
using System.Collections;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of successful matches found by iteratively applying a regular expression pattern to the input string.</summary>
	// Token: 0x0200014F RID: 335
	[Serializable]
	public class MatchCollection : ICollection, IEnumerable
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x0003364C File Offset: 0x0003184C
		internal MatchCollection(Regex regex, string input, int beginning, int length, int startat)
		{
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", global::SR.GetString("Start index cannot be less than 0 or greater than input length."));
			}
			this._regex = regex;
			this._input = input;
			this._beginning = beginning;
			this._length = length;
			this._startat = startat;
			this._prevlen = -1;
			this._matches = new ArrayList();
			this._done = false;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000336C4 File Offset: 0x000318C4
		internal Match GetMatch(int i)
		{
			if (i < 0)
			{
				return null;
			}
			if (this._matches.Count > i)
			{
				return (Match)this._matches[i];
			}
			if (this._done)
			{
				return null;
			}
			for (;;)
			{
				Match match = this._regex.Run(false, this._prevlen, this._input, this._beginning, this._length, this._startat);
				if (!match.Success)
				{
					break;
				}
				this._matches.Add(match);
				this._prevlen = match._length;
				this._startat = match._textpos;
				if (this._matches.Count > i)
				{
					return match;
				}
			}
			this._done = true;
			return null;
		}

		/// <summary>Gets the number of matches.</summary>
		/// <returns>The number of matches.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00033771 File Offset: 0x00031971
		public int Count
		{
			get
			{
				if (this._done)
				{
					return this._matches.Count;
				}
				this.GetMatch(MatchCollection.infinite);
				return this._matches.Count;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection. This property always returns the object itself.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00002068 File Offset: 0x00000268
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>true in all cases. </returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x000027E2 File Offset: 0x000009E2
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <param name="i">Index into the <see cref="T:System.Text.RegularExpressions.Match" /> collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than or equal to <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" />. </exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x170001A2 RID: 418
		public virtual Match this[int i]
		{
			get
			{
				Match match = this.GetMatch(i);
				if (match == null)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				return match;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array starting at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the array where copying is to begin. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is a multi-dimensional array.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.-or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x060009EC RID: 2540 RVA: 0x000337B8 File Offset: 0x000319B8
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(global::SR.GetString("Only single dimensional arrays are supported for the requested action."));
			}
			int count = this.Count;
			try
			{
				this._matches.CopyTo(array, arrayIndex);
			}
			catch (ArrayTypeMismatchException ex)
			{
				throw new ArgumentException(global::SR.GetString("Target array type is not compatible with the type of items in the collection."), ex);
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Match" /> objects within the <see cref="T:System.Text.RegularExpressions.MatchCollection" />.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x060009ED RID: 2541 RVA: 0x0003381C File Offset: 0x00031A1C
		public IEnumerator GetEnumerator()
		{
			return new MatchEnumerator(this);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal MatchCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000ED7 RID: 3799
		internal Regex _regex;

		// Token: 0x04000ED8 RID: 3800
		internal ArrayList _matches;

		// Token: 0x04000ED9 RID: 3801
		internal bool _done;

		// Token: 0x04000EDA RID: 3802
		internal string _input;

		// Token: 0x04000EDB RID: 3803
		internal int _beginning;

		// Token: 0x04000EDC RID: 3804
		internal int _length;

		// Token: 0x04000EDD RID: 3805
		internal int _startat;

		// Token: 0x04000EDE RID: 3806
		internal int _prevlen;

		// Token: 0x04000EDF RID: 3807
		private static int infinite = int.MaxValue;
	}
}
