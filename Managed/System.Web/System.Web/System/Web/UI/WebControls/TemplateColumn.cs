using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that allows you to customize the layout of controls in the column.</summary>
	// Token: 0x02000427 RID: 1063
	public class TemplateColumn : DataGridColumn
	{
		/// <summary>Calls a <see cref="T:System.Web.UI.WebControls.TableCell" /> object's base class to initialize the instance and then applies a <see cref="T:System.Web.UI.WebControls.ListItemType" /> to the cell.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> object that represents the cell to reset.</param>
		/// <param name="columnIndex">The column number where the cell is located.</param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06002FEC RID: 12268 RVA: 0x0007E5FC File Offset: 0x0007C7FC
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			ITemplate template = null;
			switch (itemType)
			{
			case ListItemType.Header:
				template = this.HeaderTemplate;
				break;
			case ListItemType.Footer:
				template = this.FooterTemplate;
				break;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
				template = this.ItemTemplate;
				if (template == null)
				{
					cell.Text = "&nbsp;";
				}
				break;
			case ListItemType.EditItem:
				template = this.EditItemTemplate;
				if (template == null)
				{
					template = this.ItemTemplate;
				}
				if (template == null)
				{
					cell.Text = "&nbsp;";
				}
				break;
			}
			if (template != null)
			{
				template.InstantiateIn(cell);
			}
		}

		/// <summary>Gets or sets the template for displaying the item selected for editing in a <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the item being edited in the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x0007E686 File Offset: 0x0007C886
		// (set) Token: 0x06002FEE RID: 12270 RVA: 0x0007E68E File Offset: 0x0007C88E
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		[WebSysDescription("")]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
			}
		}

		/// <summary>Gets or sets the template for displaying the footer section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the footer section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x0007E697 File Offset: 0x0007C897
		// (set) Token: 0x06002FF0 RID: 12272 RVA: 0x0007E69F File Offset: 0x0007C89F
		[WebSysDescription("")]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		[DefaultValue(null)]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
			}
		}

		/// <summary>Gets or sets the template for displaying the heading section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the heading section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x0007E6A8 File Offset: 0x0007C8A8
		// (set) Token: 0x06002FF2 RID: 12274 RVA: 0x0007E6B0 File Offset: 0x0007C8B0
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		[WebSysDescription("")]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
			}
		}

		/// <summary>Gets or sets the template for displaying a data item in a <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying a data item in the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x0007E6B9 File Offset: 0x0007C8B9
		// (set) Token: 0x06002FF4 RID: 12276 RVA: 0x0007E6C1 File Offset: 0x0007C8C1
		[WebSysDescription("")]
		[TemplateContainer(typeof(DataGridItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
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
			}
		}

		// Token: 0x04001BFA RID: 7162
		private ITemplate editItemTemplate;

		// Token: 0x04001BFB RID: 7163
		private ITemplate footerTemplate;

		// Token: 0x04001BFC RID: 7164
		private ITemplate headerTemplate;

		// Token: 0x04001BFD RID: 7165
		private ITemplate itemTemplate;
	}
}
