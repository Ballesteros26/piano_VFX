using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a single view of a <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> control.</summary>
	// Token: 0x020006B2 RID: 1714
	public class ModelDataSourceView : DataSourceView, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> class.</summary>
		/// <param name="owner">The model data source that owns this view.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="owner" /> parameter is null.</exception>
		// Token: 0x0600484F RID: 18511 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelDataSourceView(ModelDataSource owner)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the first item in the <see cref="P:System.Web.UI.WebControls.FormView.DataKeyNames" /> array of the data-bound control if the data-bound control is a <see cref="T:System.Web.UI.WebControls.FormView" />, <see cref="T:System.Web.UI.WebControls.ListView" />, <see cref="T:System.Web.UI.WebControls.GridView" />, or <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The first item in the <see cref="P:System.Web.UI.WebControls.FormView.DataKeyNames" /> array of a <see cref="T:System.Web.UI.WebControls.FormView" />, <see cref="T:System.Web.UI.WebControls.ListView" />, <see cref="T:System.Web.UI.WebControls.GridView" />, or <see cref="T:System.Web.UI.WebControls.DetailsView" /> control, or an empty string for other data-bound controls.</returns>
		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string DataKeyName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the name of the method that the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> object invokes to delete data.</summary>
		/// <returns>A string that represents the name of the method or that the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> uses to delete data.</returns>
		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06004851 RID: 18513 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string DeleteMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the name of the method that the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> object invokes to insert data.</summary>
		/// <returns>A string that represents the name of the method that the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> uses to insert data. </returns>
		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string InsertMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the data type name for the data bound control.</summary>
		/// <returns>The data type name.</returns>
		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string ModelTypeName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the name of the method that the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> control invokes to retrieve data.</summary>
		/// <returns>A string that represents the name of the method that the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> uses to retrieve data. </returns>
		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06004854 RID: 18516 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string SelectMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x000C9F04 File Offset: 0x000C8104
		bool IStateManager.get_IsTrackingViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Gets or sets the name of the method that the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> object invokes to update data.</summary>
		/// <returns>A string that represents the name of the method that the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> uses to update data. </returns>
		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string UpdateMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Occurs when data methods are being called.</summary>
		// Token: 0x14000118 RID: 280
		// (add) Token: 0x06004857 RID: 18519 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x06004858 RID: 18520 RVA: 0x0000B3E4 File Offset: 0x000095E4
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

		/// <summary>Returns the result of a Select operation after converting it to an <see cref="T:System.Collections.IEnumerable" /> collection if it is not already one.</summary>
		/// <returns>The result of a Select operation, converted to an <see cref="T:System.Collections.IEnumerable" /> collection if it was not originally one.</returns>
		/// <param name="result">The result of a Select operation.</param>
		/// <exception cref="T:System.InvalidOperationException">The return value is not the correct type.</exception>
		// Token: 0x06004859 RID: 18521 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual IEnumerable CreateSelectResult(object result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Invokes the Delete method and gets the result.</summary>
		/// <returns>Returns the result of the delete method.</returns>
		/// <param name="keys">The keys.</param>
		/// <param name="oldValues">The old values.</param>
		// Token: 0x0600485A RID: 18522 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual ModelDataSourceMethod EvaluateDeleteMethodParameters(IDictionary keys, IDictionary oldValues)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Invokes the insert method.</summary>
		/// <returns>Returns the insert method.</returns>
		/// <param name="values">The values.</param>
		// Token: 0x0600485B RID: 18523 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual ModelDataSourceMethod EvaluateInsertMethodParameters(IDictionary values)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Evaluates the method parameters for model binding, using the data source operation, the data source method object, and the control values.</summary>
		/// <param name="dataSourceOperation">The data source operation for which the parameters are being evaluated.</param>
		/// <param name="modelDataSourceMethod">The method object for which the parameters are being evaluated.</param>
		/// <param name="controlValues">The values from the data-bound control.</param>
		// Token: 0x0600485C RID: 18524 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void EvaluateMethodParameters(DataSourceOperation dataSourceOperation, ModelDataSourceMethod modelDataSourceMethod, IDictionary controlValues)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Evaluates the method parameters for model binding, using the data source operation, the data source method object, the control values, and a value that indicates whether this method is called in the page's <see cref="E:System.Web.UI.Page.LoadComplete" /> event handler.</summary>
		/// <param name="dataSourceOperation">The data source operation for which the parameters are being evaluated.</param>
		/// <param name="modelDataSourceMethod">The method object for which the parameters are being evaluated.</param>
		/// <param name="controlValues">The values from the data-bound control.</param>
		/// <param name="isPageLoadComplete">Set to true if this method is called in the page's <see cref="E:System.Web.UI.Page.LoadComplete" /> event handler, and if it is called to evaluate the select method parameters, and if custom value providers are being used. This makes it possible to identify changes in the custom value providers in order to mark the data-bound control for data binding if necessary. </param>
		// Token: 0x0600485D RID: 18525 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void EvaluateMethodParameters(DataSourceOperation dataSourceOperation, ModelDataSourceMethod modelDataSourceMethod, IDictionary controlValues, bool isPageLoadComplete)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Evaluates the select method parameters and also determines the options for processing the select result, such as auto paging and sorting behavior.</summary>
		/// <returns>Returns the information required to invoke the select method.</returns>
		/// <param name="arguments">The <see cref="T:System.Web.UI.DataSourceSelectArguments" /> for the select operation.</param>
		/// <param name="selectResultProcessingOptions">The <see cref="T:System.Web.UI.WebControls.DataSourceSelectResultProcessingOptions" /> to use for processing the select result once the select operation is complete. These options are determined in this method and later used by the <see cref="M:System.Web.UI.WebControls.ModelDataSourceView.ProcessSelectMethodResult(System.Web.UI.DataSourceSelectArguments,System.Web.UI.WebControls.DataSourceSelectResultProcessingOptions,System.Web.UI.WebControls.ModelDataMethodResult)" /> method.</param>
		// Token: 0x0600485E RID: 18526 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual ModelDataSourceMethod EvaluateSelectMethodParameters(DataSourceSelectArguments arguments, out DataSourceSelectResultProcessingOptions selectResultProcessingOptions)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Invokes the Update method and gets the result.</summary>
		/// <returns>Returns the update method.</returns>
		/// <param name="keys">The keys.</param>
		/// <param name="values">The values.</param>
		/// <param name="oldValues">The old values.</param>
		// Token: 0x0600485F RID: 18527 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual ModelDataSourceMethod EvaluateUpdateMethodParameters(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Performs a select operation using the <see cref="P:System.Web.UI.WebControls.ModelDataSourceView.SelectMethod" /> method.</summary>
		/// <returns>The result of the select operation.</returns>
		/// <param name="arguments">The select operation arguments.</param>
		// Token: 0x06004860 RID: 18528 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Finds the method to be executed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ModelDataSourceMethod" /> object with the <see cref="P:System.Web.UI.WebControls.ModelDataSourceMethod.Instance" /> and <see cref="P:System.Web.UI.WebControls.ModelDataSourceMethod.MethodInfo" /> properties set. The <see cref="P:System.Web.UI.WebControls.ModelDataSourceMethod.Parameters" /> collection of the <see cref="T:System.Web.UI.WebControls.ModelDataSourceMethod" /> object is still empty when this method returns.</returns>
		/// <param name="methodName">The name of the method to be executed.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.CallingDataMethodsEventArgs.DataMethodsObject" /> property and the <see cref="P:System.Web.UI.WebControls.CallingDataMethodsEventArgs.DataMethodsType" /> property both have values, or the specified method was not found.</exception>
		// Token: 0x06004861 RID: 18529 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual ModelDataSourceMethod FindMethod(string methodName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Invokes the delete method and gets the result.</summary>
		/// <returns>The result of the delete method.</returns>
		/// <param name="keys">The parameters to be used with the <see cref="P:System.Web.UI.WebControls.ModelDataSourceView.DeleteMethod" /> method. If there are no parameters associated with the method, pass null.</param>
		/// <param name="oldValues">The values of the row to be deleted.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ModelDataSourceView.CanDelete" /> property is false.</exception>
		// Token: 0x06004862 RID: 18530 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual object GetDeleteMethodResult(IDictionary keys, IDictionary oldValues)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Invokes the insert method and gets the result.</summary>
		/// <returns>The result of the insert method.</returns>
		/// <param name="values">The values to be inserted.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ModelDataSourceView.CanInsert" /> property is false.</exception>
		// Token: 0x06004863 RID: 18531 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual object GetInsertMethodResult(IDictionary values)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Invokes the select method and gets the result.</summary>
		/// <returns>The result of the select method.</returns>
		/// <param name="arguments">The select method arguments.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.ModelDataSourceView.SelectMethod" /> property is an empty string.</exception>
		// Token: 0x06004864 RID: 18532 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual object GetSelectMethodResult(DataSourceSelectArguments arguments)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Invokes the update method and gets the result.</summary>
		/// <returns>The result of the update method.</returns>
		/// <param name="keys">The parameters to be used with the <see cref="P:System.Web.UI.WebControls.ModelDataSourceView.UpdateMethod" /> method. If there are no parameters associated with the method, pass null.</param>
		/// <param name="values">The new values of the row to be updated.</param>
		/// <param name="oldValues">The old values of the row to be updated.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ModelDataSourceView.CanUpdate" /> property is false.</exception>
		// Token: 0x06004865 RID: 18533 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual object GetUpdateMethodResult(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Invokes a data method.</summary>
		/// <returns>The result of the data method.</returns>
		/// <param name="method">An object that provides information about the method to be invoked.</param>
		// Token: 0x06004866 RID: 18534 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual ModelDataMethodResult InvokeMethod(ModelDataSourceMethod method)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a value that indicates whether the control stores state in view state.</summary>
		/// <returns>true if the control stores state in view state; otherwise, false.</returns>
		// Token: 0x06004867 RID: 18535 RVA: 0x000C9F20 File Offset: 0x000C8120
		protected virtual bool IsTrackingViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Restores previously saved view state for the data source view.</summary>
		/// <param name="savedState">The saved view state.</param>
		// Token: 0x06004868 RID: 18536 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void LoadViewState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ModelDataSourceView.CallingDataMethods" /> event.</summary>
		/// <param name="e">The data for the <see cref="E:System.Web.UI.WebControls.ModelDataSourceView.CallingDataMethods" /> event.</param>
		// Token: 0x06004869 RID: 18537 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnCallingDataMethods(CallingDataMethodsEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs operations on the select method result like auto paging and sorting if applicable.</summary>
		/// <returns>Returns the select method result.</returns>
		/// <param name="arguments">The <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object for the select operation.</param>
		/// <param name="selectResultProcessingOptions">The <see cref="T:System.Web.UI.WebControls.DataSourceSelectResultProcessingOptions" /> object to use for processing the select result.These options are determined in an earlier call to <see cref="M:System.Web.UI.WebControls.ModelDataSourceView.EvaluateSelectMethodParameters(System.Web.UI.DataSourceSelectArguments,System.Web.UI.WebControls.DataSourceSelectResultProcessingOptions@)" />.</param>
		/// <param name="result">The result after operations like auto paging/sorting are done.</param>
		// Token: 0x0600486A RID: 18538 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual object ProcessSelectMethodResult(DataSourceSelectArguments arguments, DataSourceSelectResultProcessingOptions selectResultProcessingOptions, ModelDataMethodResult result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Saves the changes to the view state for the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> object since the time when the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> view state; otherwise null, if there is no view state associated with the object.</returns>
		// Token: 0x0600486B RID: 18539 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual object SaveViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" />.</summary>
		/// <param name="savedState">The saved state to restore.</param>
		// Token: 0x0600486C RID: 18540 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IStateManager.LoadViewState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.SaveViewState" />.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> view state; otherwise, null.</returns>
		// Token: 0x0600486D RID: 18541 RVA: 0x0000E80B File Offset: 0x0000CA0B
		object IStateManager.SaveViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.TrackViewState" />.</summary>
		// Token: 0x0600486E RID: 18542 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IStateManager.TrackViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> object to track changes to its view state so that the changes can be stored in the <see cref="P:System.Web.UI.Control.ViewState" /> object for the control and persisted across requests for the same page.</summary>
		// Token: 0x0600486F RID: 18543 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void TrackViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the specified properties using the values provided in the parameters.</summary>
		/// <param name="modelTypeName">The model type name.</param>
		/// <param name="selectMethod">The select method name.</param>
		/// <param name="updateMethod">The update method name.</param>
		/// <param name="insertMethod">The update method name.</param>
		/// <param name="deleteMethod">The delete method name.</param>
		/// <param name="dataKeyName">The data key name.</param>
		// Token: 0x06004870 RID: 18544 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void UpdateProperties(string modelTypeName, string selectMethod, string updateMethod, string insertMethod, string deleteMethod, string dataKeyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
