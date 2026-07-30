using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the base class for a tabular data-bound control that is composed of other server controls.</summary>
	// Token: 0x02000357 RID: 855
	public abstract class CompositeDataBoundControl : DataBoundControl, INamingContainer
	{
		/// <summary>Gets a collection of the child controls within the composite data-bound control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that represents the child controls within the composite data-bound control.</returns>
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x00047ACE File Offset: 0x00045CCE
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		/// <summary>Creates the control hierarchy that is used to render a composite data-bound control based on the values that are stored in view state.</summary>
		// Token: 0x06001FBB RID: 8123 RVA: 0x000503C4 File Offset: 0x0004E5C4
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			object obj = this.ViewState["_!ItemCount"];
			if (obj != null)
			{
				object[] array = new object[(int)obj];
				this.CreateChildControls(array, false);
				return;
			}
			if (base.RequiresDataBinding)
			{
				this.EnsureDataBound();
			}
		}

		/// <summary>Binds the data from the data source to the composite data-bound control.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> that contains the values to bind to the composite data-bound control.</param>
		// Token: 0x06001FBC RID: 8124 RVA: 0x00050414 File Offset: 0x0004E614
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			this.Controls.Clear();
			this.ViewState["_!ItemCount"] = this.CreateChildControls(data, true);
		}

		/// <summary>When overridden in an abstract class, creates the control hierarchy that is used to render the composite data-bound control based on the values from the specified data source.</summary>
		/// <returns>The number of items created by the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)" />.</returns>
		/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that contains the values to bind to the control.</param>
		/// <param name="dataBinding">true to indicate that the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)" /> is called during data binding; otherwise, false.</param>
		// Token: 0x06001FBD RID: 8125
		protected abstract int CreateChildControls(IEnumerable dataSource, bool dataBinding);

		/// <summary>Gets or sets the name of the method to call in order to delete data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06001FBF RID: 8127 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal string DeleteMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the method to call in order to insert data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06001FC1 RID: 8129 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal string InsertMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether model binding is in use.</summary>
		/// <returns>true if model binding is in use; otherwise, false.</returns>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x00050448 File Offset: 0x0004E648
		protected override bool IsUsingModelBinders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the name of the method to call in order to update data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal string UpdateMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
