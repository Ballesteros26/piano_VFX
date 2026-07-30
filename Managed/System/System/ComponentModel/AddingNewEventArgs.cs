using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
	// Token: 0x02000224 RID: 548
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class AddingNewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using no parameters.</summary>
		// Token: 0x060011C3 RID: 4547 RVA: 0x0000BE61 File Offset: 0x0000A061
		public AddingNewEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using the specified object as the new item.</summary>
		/// <param name="newObject">An <see cref="T:System.Object" /> to use as the new item value.</param>
		// Token: 0x060011C4 RID: 4548 RVA: 0x0004C832 File Offset: 0x0004AA32
		public AddingNewEventArgs(object newObject)
		{
			this.newObject = newObject;
		}

		/// <summary>Gets or sets the object to be added to the binding list. </summary>
		/// <returns>The <see cref="T:System.Object" /> to be added as a new item to the associated collection. </returns>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004C841 File Offset: 0x0004AA41
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x0004C849 File Offset: 0x0004AA49
		public object NewObject
		{
			get
			{
				return this.newObject;
			}
			set
			{
				this.newObject = value;
			}
		}

		// Token: 0x04001227 RID: 4647
		private object newObject;
	}
}
