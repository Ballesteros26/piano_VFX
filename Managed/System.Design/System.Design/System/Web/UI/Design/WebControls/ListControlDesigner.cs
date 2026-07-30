using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Serves as the base class for designers that provide design-time support in the Visual Web Designer for controls that are derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> abstract class.</summary>
	// Token: 0x020000D6 RID: 214
	public class ListControlDesigner : DataBoundControlDesigner
	{
		/// <summary>Gets the designer action list collection for the designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> associated with the designer.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000234B File Offset: 0x0000054B
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the associated control should render its default action lists.</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000234B File Offset: 0x0000054B
		protected override bool UseDataSourcePickerActionList
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00009856 File Offset: 0x00007A56
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000985E File Offset: 0x00007A5E
		public string DataKeyField
		{
			get
			{
				return this.data_key_field;
			}
			set
			{
				this.data_key_field = value;
			}
		}

		/// <summary>Gets or sets the data text field of the control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.DataTextField" /> of the list control.</returns>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00009867 File Offset: 0x00007A67
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0000986F File Offset: 0x00007A6F
		public string DataTextField
		{
			get
			{
				return this.data_text_field;
			}
			set
			{
				this.data_text_field = value;
			}
		}

		/// <summary>Gets or sets the data value field of the control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.DataValueField" /> of the list control.</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00009878 File Offset: 0x00007A78
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00009880 File Offset: 0x00007A80
		public string DataValueField
		{
			get
			{
				return this.data_value_field;
			}
			set
			{
				this.data_value_field = value;
			}
		}

		/// <summary>Binds the specified control to the design-time data source. </summary>
		/// <param name="dataBoundControl">The associated control derived from the <see cref="T:System.Web.UI.WebControls.ListControl" />, or a copy of that control.</param>
		// Token: 0x06000634 RID: 1588 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void DataBind(BaseDataBoundControl dataBoundControl)
		{
			throw new NotImplementedException();
		}

		/// <summary>Prepares the designer to view, edit, and design the associated control.</summary>
		/// <param name="component">A control derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> that implements an <see cref="T:System.ComponentModel.IComponent" />.</param>
		// Token: 0x06000635 RID: 1589 RVA: 0x0000234B File Offset: 0x0000054B
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the HTML that is used to represent the control at design time.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the markup used to render the control derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> at design time.</returns>
		// Token: 0x06000636 RID: 1590 RVA: 0x0000234B File Offset: 0x0000054B
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the data source component from the associated control container, resolved to a specific data member.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> containing the design-time <see cref="P:System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner.DataSource" />, resolved to the <see cref="P:System.Web.UI.Design.WebControls.DataBoundControlDesigner.DataMember" /> of the associated control.</returns>
		// Token: 0x06000637 RID: 1591 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual IEnumerable GetResolvedSelectedDataSource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the data source component from the associated control container.</summary>
		/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface and containing the design-time <see cref="P:System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner.DataSource" /> of the associated control.</returns>
		// Token: 0x06000638 RID: 1592 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual object GetSelectedDataSource()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000234B File Offset: 0x0000054B
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the data source for the associated control has changed.</summary>
		// Token: 0x0600063A RID: 1594 RVA: 0x0000234B File Offset: 0x0000054B
		protected internal virtual void OnDataSourceChanged()
		{
			throw new NotImplementedException();
		}

		/// <summary>Used by the designer to remove additional properties from or add properties to the display in the Properties grid or to shadow properties of the associated control.</summary>
		/// <param name="properties">A collection implementing the <see cref="T:System.Collections.IDictionary" /> of the added or shadowed properties to expose for the associated control at design time. </param>
		// Token: 0x0600063B RID: 1595 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400014F RID: 335
		private string data_key_field;

		// Token: 0x04000150 RID: 336
		private string data_text_field;

		// Token: 0x04000151 RID: 337
		private string data_value_field;
	}
}
