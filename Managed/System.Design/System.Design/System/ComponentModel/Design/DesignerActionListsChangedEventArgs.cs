using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignerActionService.DesignerActionListsChanged" /> event.</summary>
	// Token: 0x02000112 RID: 274
	public class DesignerActionListsChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionListsChangedEventArgs" /> class.</summary>
		/// <param name="relatedObject">The object that is associated with the collection.</param>
		/// <param name="changeType">A value that specifies whether a <see cref="T:System.ComponentModel.Design.DesignerActionList" /> has been added or removed from the collection.</param>
		/// <param name="actionLists">The collection of list elements after the action has been applied.</param>
		// Token: 0x06000805 RID: 2053 RVA: 0x0000D804 File Offset: 0x0000BA04
		public DesignerActionListsChangedEventArgs(object relatedObject, DesignerActionListsChangedType changeType, DesignerActionListCollection actionLists)
		{
			this.related_object = relatedObject;
			this.change_type = changeType;
			this.action_lists = actionLists;
		}

		/// <summary>Gets the collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects associated with this event.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> that represents the current state of the collection.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0000D821 File Offset: 0x0000BA21
		public DesignerActionListCollection ActionLists
		{
			get
			{
				return this.action_lists;
			}
		}

		/// <summary>Gets a flag indicating whether an element has been added or removed from the collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListsChangedType" /> that indicates the type of change.</returns>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0000D829 File Offset: 0x0000BA29
		public DesignerActionListsChangedType ChangeType
		{
			get
			{
				return this.change_type;
			}
		}

		/// <summary>Gets the object that that is associated with the collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects.</summary>
		/// <returns>The <see cref="T:System.Object" /> associated with the managed <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" />.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0000D831 File Offset: 0x0000BA31
		public object RelatedObject
		{
			get
			{
				return this.related_object;
			}
		}

		// Token: 0x040001B4 RID: 436
		private object related_object;

		// Token: 0x040001B5 RID: 437
		private DesignerActionListsChangedType change_type;

		// Token: 0x040001B6 RID: 438
		private DesignerActionListCollection action_lists;
	}
}
