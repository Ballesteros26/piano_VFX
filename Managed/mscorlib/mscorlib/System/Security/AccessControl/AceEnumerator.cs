using System;
using System.Collections;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to iterate through the access control entries (ACEs) in an access control list (ACL). </summary>
	// Token: 0x020005C8 RID: 1480
	public sealed class AceEnumerator : IEnumerator
	{
		// Token: 0x06004175 RID: 16757 RVA: 0x000E8C6F File Offset: 0x000E6E6F
		internal AceEnumerator(GenericAcl owner)
		{
			this.current = -1;
			base..ctor();
			this.owner = owner;
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection. This property gets the type-friendly version of the object. </summary>
		/// <returns>The current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</returns>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x000E8C85 File Offset: 0x000E6E85
		public GenericAce Current
		{
			get
			{
				if (this.current >= 0)
				{
					return this.owner[this.current];
				}
				return null;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000E8CA3 File Offset: 0x000E6EA3
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06004178 RID: 16760 RVA: 0x000E8CAB File Offset: 0x000E6EAB
		public bool MoveNext()
		{
			if (this.current + 1 == this.owner.Count)
			{
				return false;
			}
			this.current++;
			return true;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06004179 RID: 16761 RVA: 0x000E8CD3 File Offset: 0x000E6ED3
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal AceEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002123 RID: 8483
		private GenericAcl owner;

		// Token: 0x04002124 RID: 8484
		private int current;
	}
}
