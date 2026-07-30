using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a field that displays custom content in a data-bound control.</summary>
	// Token: 0x02000428 RID: 1064
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TemplateField : DataControlField
	{
		/// <summary>Gets or sets the template for displaying the alternating items in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the alternating items in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06002FF6 RID: 12278 RVA: 0x0007E6CA File Offset: 0x0007C8CA
		// (set) Token: 0x06002FF7 RID: 12279 RVA: 0x0007E6D2 File Offset: 0x0007C8D2
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this.alternatingItemTemplate;
			}
			set
			{
				this.alternatingItemTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the value that the <see cref="T:System.Web.UI.WebControls.TemplateField" /> object is bound to should be converted to null if it is <see cref="F:System.String.Empty" />.</summary>
		/// <returns>true if the value that the <see cref="T:System.Web.UI.WebControls.TemplateField" /> is bound to should be converted to null when it is <see cref="F:System.String.Empty" />; otherwise, false. The default value is false.</returns>
		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x0007E6E4 File Offset: 0x0007C8E4
		// (set) Token: 0x06002FF9 RID: 12281 RVA: 0x0004947E File Offset: 0x0004767E
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = base.ViewState["ConvertEmptyStringToNull"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ConvertEmptyStringToNull"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying an item in edit mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying an item in edit mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06002FFA RID: 12282 RVA: 0x0007E70D File Offset: 0x0007C90D
		// (set) Token: 0x06002FFB RID: 12283 RVA: 0x0007E715 File Offset: 0x0007C915
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying the footer section of a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the footer section of a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06002FFC RID: 12284 RVA: 0x0007E724 File Offset: 0x0007C924
		// (set) Token: 0x06002FFD RID: 12285 RVA: 0x0007E72C File Offset: 0x0007C92C
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.OneWay)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying the header section of a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the header section of a <see cref="T:System.Web.UI.WebControls.TemplateField" /> in a data-bound control. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x0007E73B File Offset: 0x0007C93B
		// (set) Token: 0x06002FFF RID: 12287 RVA: 0x0007E743 File Offset: 0x0007C943
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.OneWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying an item in insert mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying an item in insert mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x0007E752 File Offset: 0x0007C952
		// (set) Token: 0x06003001 RID: 12289 RVA: 0x0007E75A File Offset: 0x0007C95A
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate InsertItemTemplate
		{
			get
			{
				return this.insertItemTemplate;
			}
			set
			{
				this.insertItemTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying an item in a data-bound control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying an item in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003002 RID: 12290 RVA: 0x0007E769 File Offset: 0x0007C969
		// (set) Token: 0x06003003 RID: 12291 RVA: 0x0007E771 File Offset: 0x0007C971
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[Browsable(false)]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Adds text or controls to a cell's controls collection.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values, specifying the state of the row that contains the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" />.</param>
		/// <param name="rowIndex">The index of the row that the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> is contained in.</param>
		// Token: 0x06003004 RID: 12292 RVA: 0x0007E780 File Offset: 0x0007C980
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.Header)
			{
				if (this.headerTemplate != null && this.ShowHeader)
				{
					cell.Text = string.Empty;
					this.headerTemplate.InstantiateIn(cell);
					return;
				}
			}
			else if (cellType == DataControlCellType.Footer)
			{
				if (this.footerTemplate != null)
				{
					cell.Text = string.Empty;
					this.footerTemplate.InstantiateIn(cell);
					return;
				}
			}
			else
			{
				cell.Text = string.Empty;
				if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal && this.insertItemTemplate != null)
				{
					this.insertItemTemplate.InstantiateIn(cell);
					return;
				}
				if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal && this.editItemTemplate != null)
				{
					this.editItemTemplate.InstantiateIn(cell);
					return;
				}
				if ((rowState & DataControlRowState.Alternate) != DataControlRowState.Normal && this.alternatingItemTemplate != null)
				{
					this.alternatingItemTemplate.InstantiateIn(cell);
					return;
				}
				if (this.itemTemplate != null)
				{
					this.itemTemplate.InstantiateIn(cell);
					return;
				}
				cell.Text = "&nbsp;";
			}
		}

		/// <summary>Extracts the value of the data control fields as specified by one or more two-way binding statements (DataBind) from the current table cell and adds the values to the specified <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</summary>
		/// <param name="dictionary">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" />.</param>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.TemplateField" />.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="includeReadOnly">true to indicate that the values of read-only fields are included in the <paramref name="dictionary" /> collection; otherwise, false.</param>
		// Token: 0x06003005 RID: 12293 RVA: 0x0007E86C File Offset: 0x0007CA6C
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			IBindableTemplate bindableTemplate;
			if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
			{
				bindableTemplate = this.insertItemTemplate as IBindableTemplate;
			}
			else if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal)
			{
				bindableTemplate = this.editItemTemplate as IBindableTemplate;
			}
			else if (this.alternatingItemTemplate != null && (rowState & DataControlRowState.Alternate) != DataControlRowState.Normal)
			{
				bindableTemplate = this.alternatingItemTemplate as IBindableTemplate;
			}
			else
			{
				bindableTemplate = this.itemTemplate as IBindableTemplate;
			}
			if (bindableTemplate != null)
			{
				IOrderedDictionary orderedDictionary = bindableTemplate.ExtractValues(cell);
				if (orderedDictionary == null)
				{
					return;
				}
				foreach (object obj in orderedDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					dictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
		}

		/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object support page callbacks.</summary>
		/// <exception cref="T:System.NotSupportedException">The default implementation of this method is called. </exception>
		// Token: 0x06003006 RID: 12294 RVA: 0x0007E92C File Offset: 0x0007CB2C
		public override void ValidateSupportsCallback()
		{
			throw new NotSupportedException("Callback not supported on TemplateField. Turn disable callbacks on '" + base.Control.ID + "'.");
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>Always returns a new <see cref="T:System.Web.UI.WebControls.TemplateField" />.</returns>
		// Token: 0x06003007 RID: 12295 RVA: 0x0007E94D File Offset: 0x0007CB4D
		protected override DataControlField CreateField()
		{
			return new TemplateField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.TemplateField" />-derived object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.TemplateField" /> to.</param>
		// Token: 0x06003008 RID: 12296 RVA: 0x0007E954 File Offset: 0x0007CB54
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			TemplateField templateField = (TemplateField)newField;
			templateField.AlternatingItemTemplate = this.AlternatingItemTemplate;
			templateField.ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
			templateField.EditItemTemplate = this.EditItemTemplate;
			templateField.FooterTemplate = this.FooterTemplate;
			templateField.HeaderTemplate = this.HeaderTemplate;
			templateField.InsertItemTemplate = this.InsertItemTemplate;
			templateField.ItemTemplate = this.ItemTemplate;
		}

		/// <summary>Gets or sets a value that specifies whether the control validates client input.</summary>
		/// <returns>true if the control validates client input; otherwise, false.</returns>
		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x0007E9C4 File Offset: 0x0007CBC4
		// (set) Token: 0x0600300B RID: 12299 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public new ValidateRequestMode ValidateRequestMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ValidateRequestMode.Inherit;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001BFE RID: 7166
		private ITemplate alternatingItemTemplate;

		// Token: 0x04001BFF RID: 7167
		private ITemplate editItemTemplate;

		// Token: 0x04001C00 RID: 7168
		private ITemplate footerTemplate;

		// Token: 0x04001C01 RID: 7169
		private ITemplate headerTemplate;

		// Token: 0x04001C02 RID: 7170
		private ITemplate insertItemTemplate;

		// Token: 0x04001C03 RID: 7171
		private ITemplate itemTemplate;
	}
}
