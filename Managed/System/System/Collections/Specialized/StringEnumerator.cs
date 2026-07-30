using System;
using Unity;

namespace System.Collections.Specialized
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
	// Token: 0x0200070B RID: 1803
	public class StringEnumerator
	{
		// Token: 0x060038D3 RID: 14547 RVA: 0x000D05CF File Offset: 0x000CE7CF
		internal StringEnumerator(StringCollection mappings)
		{
			this.temp = mappings;
			this.baseEnumerator = this.temp.GetEnumerator();
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000D05EF File Offset: 0x000CE7EF
		public string Current
		{
			get
			{
				return (string)this.baseEnumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x060038D5 RID: 14549 RVA: 0x000D0601 File Offset: 0x000CE801
		public bool MoveNext()
		{
			return this.baseEnumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x060038D6 RID: 14550 RVA: 0x000D060E File Offset: 0x000CE80E
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal StringEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002C77 RID: 11383
		private IEnumerator baseEnumerator;

		// Token: 0x04002C78 RID: 11384
		private IEnumerable temp;
	}
}
