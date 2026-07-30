using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>The data source control used by data-bound controls to perform CRUD (create, read, update, delete) operations when model binding is in use. </summary>
	// Token: 0x020006B1 RID: 1713
	public class ModelDataSource : IDataSource, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> class.</summary>
		/// <param name="dataControl">The data-bound control.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataControl" /> parameter is null.</exception>
		// Token: 0x0600483C RID: 18492 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelDataSource(Control dataControl)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the data-bound control that is using this control as a data source when model binding is in use.</summary>
		/// <returns>The data-bound control.</returns>
		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x0600483D RID: 18493 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Control DataControl
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x000C9ECC File Offset: 0x000C80CC
		bool IStateManager.get_IsTrackingViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Gets the default (and only) view object for this data source control.</summary>
		/// <returns>The view object.</returns>
		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x0600483F RID: 18495 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ModelDataSourceView View
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Occurs when model binding is in use and data methods are being called.</summary>
		// Token: 0x14000116 RID: 278
		// (add) Token: 0x06004840 RID: 18496 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x06004841 RID: 18497 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event CallingDataMethodsEventHandler CallingDataMethods
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the underlying data source has changed. The change may be due to a change in the control's properties, or a change in the data due to an edit action performed by the data source control.</summary>
		// Token: 0x14000117 RID: 279
		// (add) Token: 0x06004842 RID: 18498 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x06004843 RID: 18499 RVA: 0x0000B3E4 File Offset: 0x000095E4
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns a value that indicates whether the control is tracking view state.</summary>
		/// <returns>true if the control is tracking view state; otherwise, false.</returns>
		// Token: 0x06004844 RID: 18500 RVA: 0x000C9EE8 File Offset: 0x000C80E8
		protected virtual bool IsTrackingViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Loads the previously saved view state.</summary>
		/// <param name="savedState">The saved view state.</param>
		// Token: 0x06004845 RID: 18501 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void LoadViewState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the state of the control.</summary>
		/// <returns>Returns the control's current view state; otherwise, returns null, if there is no view state associated with the control.</returns>
		// Token: 0x06004846 RID: 18502 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual object SaveViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the view.</summary>
		/// <returns>The view.</returns>
		/// <param name="viewName">The name of the view.</param>
		// Token: 0x06004847 RID: 18503 RVA: 0x0000E80B File Offset: 0x0000CA0B
		DataSourceView IDataSource.GetView(string viewName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the view names.</summary>
		/// <returns>The view names.</returns>
		// Token: 0x06004848 RID: 18504 RVA: 0x0000E80B File Offset: 0x0000CA0B
		ICollection IDataSource.GetViewNames()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Loads the previously saved view state.</summary>
		/// <param name="savedState">The saved view state.</param>
		// Token: 0x06004849 RID: 18505 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IStateManager.LoadViewState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the state of the control.</summary>
		/// <returns>Returns the control's current view state; otherwise, returns null, if there is no view state associated with the control.</returns>
		// Token: 0x0600484A RID: 18506 RVA: 0x0000E80B File Offset: 0x0000CA0B
		object IStateManager.SaveViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Tracks view-state changes to the control so that they can be stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x0600484B RID: 18507 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IStateManager.TrackViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Tracks view-state changes to the control so that they can be stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x0600484C RID: 18508 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void TrackViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the required properties for one-way data binding.</summary>
		/// <param name="modelTypeName">The name of the model type.</param>
		/// <param name="selectMethod">The name of the select method.</param>
		// Token: 0x0600484D RID: 18509 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void UpdateProperties(string modelTypeName, string selectMethod)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the required properties for two-way data binding.</summary>
		/// <param name="modelTypeName">The name of the model type.</param>
		/// <param name="selectMethod">The name of the select method.</param>
		/// <param name="updateMethod">The name of the update method.</param>
		/// <param name="insertMethod">The name of the insert method.</param>
		/// <param name="deleteMethod">The name of the delete method.</param>
		/// <param name="dataKeyName">The data key name.</param>
		// Token: 0x0600484E RID: 18510 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void UpdateProperties(string modelTypeName, string selectMethod, string updateMethod, string insertMethod, string deleteMethod, string dataKeyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
