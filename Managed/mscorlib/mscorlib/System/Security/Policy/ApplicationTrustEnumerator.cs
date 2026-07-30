using System;
using System.Collections;
using System.Runtime.InteropServices;
using Unity;

namespace System.Security.Policy
{
	/// <summary>Represents the enumerator for <see cref="T:System.Security.Policy.ApplicationTrust" /> objects in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
	// Token: 0x0200055B RID: 1371
	[ComVisible(true)]
	public sealed class ApplicationTrustEnumerator : IEnumerator
	{
		// Token: 0x06003DAB RID: 15787 RVA: 0x000DD635 File Offset: 0x000DB835
		internal ApplicationTrustEnumerator(ApplicationTrustCollection collection)
		{
			this.e = collection.GetEnumerator();
		}

		/// <summary>Gets the current <see cref="T:System.Security.Policy.ApplicationTrust" /> object in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>The current <see cref="T:System.Security.Policy.ApplicationTrust" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.</returns>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x000DD649 File Offset: 0x000DB849
		public ApplicationTrust Current
		{
			get
			{
				return (ApplicationTrust)this.e.Current;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Object" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>The current <see cref="T:System.Object" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.</returns>
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003DAD RID: 15789 RVA: 0x000DD65B File Offset: 0x000DB85B
		object IEnumerator.Current
		{
			get
			{
				return this.e.Current;
			}
		}

		/// <summary>Moves to the next element in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003DAE RID: 15790 RVA: 0x000DD668 File Offset: 0x000DB868
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			return this.e.MoveNext();
		}

		/// <summary>Resets the enumerator to the beginning of the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		// Token: 0x06003DAF RID: 15791 RVA: 0x000DD675 File Offset: 0x000DB875
		public void Reset()
		{
			this.e.Reset();
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal ApplicationTrustEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001FA2 RID: 8098
		private IEnumerator e;
	}
}
