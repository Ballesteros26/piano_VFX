using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of designers.</summary>
	// Token: 0x0200031E RID: 798
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class DesignerCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified designers.</summary>
		/// <param name="designers">An array of <see cref="T:System.ComponentModel.Design.IDesignerHost" /> objects to store. </param>
		// Token: 0x06001969 RID: 6505 RVA: 0x0006A27C File Offset: 0x0006847C
		public DesignerCollection(IDesignerHost[] designers)
		{
			if (designers != null)
			{
				this.designers = new ArrayList(designers);
				return;
			}
			this.designers = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified set of designers.</summary>
		/// <param name="designers">A list that contains the collection of designers to add. </param>
		// Token: 0x0600196A RID: 6506 RVA: 0x0006A29F File Offset: 0x0006849F
		public DesignerCollection(IList designers)
		{
			this.designers = designers;
		}

		/// <summary>Gets the number of designers in the collection.</summary>
		/// <returns>The number of designers in the collection.</returns>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x0006A2AE File Offset: 0x000684AE
		public int Count
		{
			get
			{
				return this.designers.Count;
			}
		}

		/// <summary>Gets the designer at the specified index.</summary>
		/// <returns>The designer at the specified index.</returns>
		/// <param name="index">The index of the designer to return. </param>
		// Token: 0x1700052C RID: 1324
		public virtual IDesignerHost this[int index]
		{
			get
			{
				return (IDesignerHost)this.designers[index];
			}
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x0600196D RID: 6509 RVA: 0x0006A2CE File Offset: 0x000684CE
		public IEnumerator GetEnumerator()
		{
			return this.designers.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0006A2DB File Offset: 0x000684DB
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00009E57 File Offset: 0x00008057
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x06001971 RID: 6513 RVA: 0x0006A2E3 File Offset: 0x000684E3
		void ICollection.CopyTo(Array array, int index)
		{
			this.designers.CopyTo(array, index);
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x06001972 RID: 6514 RVA: 0x0006A2F2 File Offset: 0x000684F2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001470 RID: 5232
		private IList designers;
	}
}
