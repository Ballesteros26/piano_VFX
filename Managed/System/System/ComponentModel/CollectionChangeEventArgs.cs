using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataColumnCollection.CollectionChanged" /> event.</summary>
	// Token: 0x02000240 RID: 576
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CollectionChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values that specifies how the collection changed. </param>
		/// <param name="element">An <see cref="T:System.Object" /> that specifies the instance of the collection where the change occurred. </param>
		// Token: 0x060012AD RID: 4781 RVA: 0x0004E584 File Offset: 0x0004C784
		public CollectionChangeEventArgs(CollectionChangeAction action, object element)
		{
			this.action = action;
			this.element = element;
		}

		/// <summary>Gets an action that specifies how the collection changed.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0004E59A File Offset: 0x0004C79A
		public virtual CollectionChangeAction Action
		{
			get
			{
				return this.action;
			}
		}

		/// <summary>Gets the instance of the collection with the change.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the instance of the collection with the change, or null if you refresh the collection.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0004E5A2 File Offset: 0x0004C7A2
		public virtual object Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x0400127C RID: 4732
		private CollectionChangeAction action;

		// Token: 0x0400127D RID: 4733
		private object element;
	}
}
