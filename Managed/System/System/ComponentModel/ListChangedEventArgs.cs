using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event.</summary>
	// Token: 0x020002A6 RID: 678
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ListChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the index of the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added, changed, or removed.</param>
		// Token: 0x060014FF RID: 5375 RVA: 0x00053AA2 File Offset: 0x00051CA2
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex)
			: this(listChangedType, newIndex, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change, the index of the affected item, and a <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added or changed.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the item.</param>
		// Token: 0x06001500 RID: 5376 RVA: 0x00053AAD File Offset: 0x00051CAD
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, PropertyDescriptor propDesc)
			: this(listChangedType, newIndex)
		{
			this.propDesc = propDesc;
			this.oldIndex = newIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, removed, or changed.</param>
		// Token: 0x06001501 RID: 5377 RVA: 0x00053AC5 File Offset: 0x00051CC5
		public ListChangedEventArgs(ListChangedType listChangedType, PropertyDescriptor propDesc)
		{
			this.listChangedType = listChangedType;
			this.propDesc = propDesc;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the old and new index of the item that was moved.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The new index of the item that was moved.</param>
		/// <param name="oldIndex">The old index of the item that was moved.</param>
		// Token: 0x06001502 RID: 5378 RVA: 0x00053ADB File Offset: 0x00051CDB
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex)
		{
			this.listChangedType = listChangedType;
			this.newIndex = newIndex;
			this.oldIndex = oldIndex;
		}

		/// <summary>Gets the type of change.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</returns>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00053AF8 File Offset: 0x00051CF8
		public ListChangedType ListChangedType
		{
			get
			{
				return this.listChangedType;
			}
		}

		/// <summary>Gets the index of the item affected by the change.</summary>
		/// <returns>The index of the affected by the change.</returns>
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00053B00 File Offset: 0x00051D00
		public int NewIndex
		{
			get
			{
				return this.newIndex;
			}
		}

		/// <summary>Gets the old index of an item that has been moved.</summary>
		/// <returns>The old index of the moved item.</returns>
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00053B08 File Offset: 0x00051D08
		public int OldIndex
		{
			get
			{
				return this.oldIndex;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, changed, or deleted.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected by the change.</returns>
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00053B10 File Offset: 0x00051D10
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propDesc;
			}
		}

		// Token: 0x0400130F RID: 4879
		private ListChangedType listChangedType;

		// Token: 0x04001310 RID: 4880
		private int newIndex;

		// Token: 0x04001311 RID: 4881
		private int oldIndex;

		// Token: 0x04001312 RID: 4882
		private PropertyDescriptor propDesc;
	}
}
