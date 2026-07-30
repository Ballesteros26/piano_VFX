using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to commit or rollback changes to an object that is used as a data source.</summary>
	// Token: 0x0200027F RID: 639
	public interface IEditableObject
	{
		/// <summary>Begins an edit on an object.</summary>
		// Token: 0x06001464 RID: 5220
		void BeginEdit();

		/// <summary>Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> or <see cref="M:System.ComponentModel.IBindingList.AddNew" /> call into the underlying object.</summary>
		// Token: 0x06001465 RID: 5221
		void EndEdit();

		/// <summary>Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> call.</summary>
		// Token: 0x06001466 RID: 5222
		void CancelEdit();
	}
}
