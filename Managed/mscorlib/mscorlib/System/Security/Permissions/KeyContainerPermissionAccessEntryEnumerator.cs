using System;
using System.Collections;
using System.Runtime.InteropServices;
using Unity;

namespace System.Security.Permissions
{
	/// <summary>Represents the enumerator for <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects in a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" />.</summary>
	// Token: 0x0200059F RID: 1439
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x0600402A RID: 16426 RVA: 0x000E4BFD File Offset: 0x000E2DFD
		internal KeyContainerPermissionAccessEntryEnumerator(ArrayList list)
		{
			this.e = list.GetEnumerator();
		}

		/// <summary>Gets the current entry in the collection.</summary>
		/// <returns>The current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed before first calling the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method. The cursor is located before the first object in the collection.-or- The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed after a call to the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method returns false, which indicates that the cursor is located after the last object in the collection. </exception>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600402B RID: 16427 RVA: 0x000E4C11 File Offset: 0x000E2E11
		public KeyContainerPermissionAccessEntry Current
		{
			get
			{
				return (KeyContainerPermissionAccessEntry)this.e.Current;
			}
		}

		/// <summary>Gets the current object in the collection.</summary>
		/// <returns>The current object in the collection.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x000E4C23 File Offset: 0x000E2E23
		object IEnumerator.Current
		{
			get
			{
				return this.e.Current;
			}
		}

		/// <summary>Moves to the next element in the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		// Token: 0x0600402D RID: 16429 RVA: 0x000E4C30 File Offset: 0x000E2E30
		public bool MoveNext()
		{
			return this.e.MoveNext();
		}

		/// <summary>Resets the enumerator to the beginning of the collection.</summary>
		// Token: 0x0600402E RID: 16430 RVA: 0x000E4C3D File Offset: 0x000E2E3D
		public void Reset()
		{
			this.e.Reset();
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal KeyContainerPermissionAccessEntryEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002085 RID: 8325
		private IEnumerator e;
	}
}
