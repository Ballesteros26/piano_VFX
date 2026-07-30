using System;
using System.ComponentModel;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for all ASP.NET version 2.0 data-bound controls that display their data in hierarchical form.</summary>
	// Token: 0x020003AD RID: 941
	[Designer("System.Web.UI.Design.WebControls.HierarchicalDataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class HierarchicalDataBoundControl : BaseDataBoundControl
	{
		/// <returns>The ID of a control that represents the data source from which the data-bound control retrieves its data. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x00064A00 File Offset: 0x00062C00
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x00064A2D File Offset: 0x00062C2D
		[IDReferenceProperty(typeof(HierarchicalDataSourceControl))]
		public override string DataSourceID
		{
			get
			{
				object obj = this.ViewState["DataSourceID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
				this.ViewState["DataSourceID"] = value;
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> object that the data-bound control uses to perform data operations.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> object that the data-bound control uses to perform data operations. </returns>
		/// <param name="viewPath">The hierarchical path of the view to retrieve.</param>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> could not be retrieved for the specified <paramref name="viewPath" />.</exception>
		// Token: 0x0600266A RID: 9834 RVA: 0x00064A50 File Offset: 0x00062C50
		protected virtual HierarchicalDataSourceView GetData(string viewPath)
		{
			if (this.DataSource != null && !string.IsNullOrEmpty(this.DataSourceID))
			{
				throw new HttpException();
			}
			IHierarchicalDataSource dataSource = this.GetDataSource();
			if (dataSource != null)
			{
				return dataSource.GetHierarchicalView(viewPath);
			}
			if (this.DataSource is IHierarchicalEnumerable)
			{
				return new ReadOnlyDataSourceView((IHierarchicalEnumerable)this.DataSource);
			}
			return null;
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> that the data-bound control is associated with, if any.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalDataSource" /> instance that represents the data source identified by the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property. </returns>
		/// <exception cref="T:System.Web.HttpException">The data source control identified by the <see cref="P:System.Web.UI.WebControls.HierarchicalDataBoundControl.DataSourceID" /> property does not exist in the current container.- or -The data source control identified by the <see cref="P:System.Web.UI.WebControls.HierarchicalDataBoundControl.DataSourceID" /> property does not implement the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface.</exception>
		// Token: 0x0600266B RID: 9835 RVA: 0x00064AAC File Offset: 0x00062CAC
		protected virtual IHierarchicalDataSource GetDataSource()
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				return this.DataSource as IHierarchicalDataSource;
			}
			Control control = base.FindDataSource();
			if (control == null)
			{
				throw new HttpException(string.Format("A control with ID '{0}' could not be found.", this.DataSourceID));
			}
			if (!(control is IHierarchicalDataSource))
			{
				throw new HttpException(string.Format("The control with ID '{0}' is not a control of type IHierarchicalDataSource.", this.DataSourceID));
			}
			return (IHierarchicalDataSource)control;
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x00064B0F File Offset: 0x00062D0F
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x00054989 File Offset: 0x00052B89
		private bool IsDataBound
		{
			get
			{
				return this.ViewState.GetBool("DataBound", false);
			}
			set
			{
				this.ViewState["DataBound"] = value;
			}
		}

		/// <summary>Sets the state of the control in view state as successfully bound to data.</summary>
		// Token: 0x0600266E RID: 9838 RVA: 0x00064B22 File Offset: 0x00062D22
		protected void MarkAsDataBound()
		{
			this.IsDataBound = true;
		}

		/// <summary>Called when one of the base data source identification properties is changed, to re-bind the data-bound control to its data.</summary>
		// Token: 0x0600266F RID: 9839 RVA: 0x00054752 File Offset: 0x00052952
		protected override void OnDataPropertyChanged()
		{
			base.RequiresDataBinding = true;
		}

		/// <summary>Called when the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> instance that the data-bound control works with raises the <see cref="E:System.Web.UI.IDataSource.DataSourceChanged" /> event.</summary>
		/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> object. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
		// Token: 0x06002670 RID: 9840 RVA: 0x00054752 File Offset: 0x00052952
		protected virtual void OnDataSourceChanged(object sender, EventArgs e)
		{
			base.RequiresDataBinding = true;
		}

		/// <summary>Handles the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
		// Token: 0x06002671 RID: 9841 RVA: 0x00064B2B File Offset: 0x00062D2B
		protected internal override void OnLoad(EventArgs e)
		{
			if (!base.Initialized)
			{
				this.Initialize();
				base.ConfirmInitState();
			}
			base.OnLoad(e);
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00064B48 File Offset: 0x00062D48
		private void Initialize()
		{
			if (!this.Page.IsPostBack || (base.IsViewStateEnabled && !this.IsDataBound))
			{
				base.RequiresDataBinding = true;
			}
			IHierarchicalDataSource dataSource = this.GetDataSource();
			if (dataSource != null && this.DataSourceID != "")
			{
				dataSource.DataSourceChanged += this.OnDataSourceChanged;
			}
		}

		/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
		/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002673 RID: 9843 RVA: 0x00064BA8 File Offset: 0x00062DA8
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			base.OnPagePreLoad(sender, e);
			this.Initialize();
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00064BB8 File Offset: 0x00062DB8
		protected void InternalPerformDataBinding()
		{
			HierarchicalDataBoundControlAdapter hierarchicalDataBoundControlAdapter = base.Adapter as HierarchicalDataBoundControlAdapter;
			if (hierarchicalDataBoundControlAdapter != null)
			{
				hierarchicalDataBoundControlAdapter.PerformDataBinding();
				return;
			}
			this.PerformDataBinding();
		}

		/// <summary>When overridden in a derived class, binds data from the data source to the control.</summary>
		// Token: 0x06002675 RID: 9845 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void PerformDataBinding()
		{
		}

		/// <summary>Retrieves data from the associated data source.</summary>
		// Token: 0x06002676 RID: 9846 RVA: 0x00064BE1 File Offset: 0x00062DE1
		protected override void PerformSelect()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.InternalPerformDataBinding();
			base.RequiresDataBinding = false;
			this.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		/// <summary>Verifies that the object a data-bound control binds to is one it can work with.</summary>
		/// <param name="dataSource">An object set to the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="dataSource" /> is not null and implements neither the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> nor the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface.</exception>
		// Token: 0x06002677 RID: 9847 RVA: 0x00064C0C File Offset: 0x00062E0C
		protected override void ValidateDataSource(object dataSource)
		{
			if (dataSource == null || dataSource is IHierarchicalDataSource || dataSource is IHierarchicalEnumerable)
			{
				return;
			}
			throw new InvalidOperationException("Invalid data source");
		}
	}
}
