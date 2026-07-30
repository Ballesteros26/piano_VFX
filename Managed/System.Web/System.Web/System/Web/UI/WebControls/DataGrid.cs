using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>A data bound list control that displays the items from data source in a table. The <see cref="T:System.Web.UI.WebControls.DataGrid" /> control allows you to select, sort, and edit these items.</summary>
	// Token: 0x02000375 RID: 885
	[Editor("System.Web.UI.Design.WebControls.DataGridComponentEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(ComponentEditor))]
	[Designer("System.Web.UI.Design.WebControls.DataGridDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataGrid : BaseDataList, INamingContainer
	{
		/// <summary>Gets or sets a value that indicates whether custom paging is enabled.</summary>
		/// <returns>true if custom paging is enabled; otherwise, false. The default value is false.</returns>
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x00055825 File Offset: 0x00053A25
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x00055838 File Offset: 0x00053A38
		[WebSysDescription("")]
		[DefaultValue(false)]
		[WebCategory("Paging")]
		public virtual bool AllowCustomPaging
		{
			get
			{
				return this.ViewState.GetBool("AllowCustomPaging", false);
			}
			set
			{
				this.ViewState["AllowCustomPaging"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether paging is enabled.</summary>
		/// <returns>true if paging is enabled; otherwise, false. The default value is false.</returns>
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x00055850 File Offset: 0x00053A50
		// (set) Token: 0x0600217B RID: 8571 RVA: 0x00055863 File Offset: 0x00053A63
		[WebCategory("Paging")]
		[WebSysDescription("")]
		[DefaultValue(false)]
		public virtual bool AllowPaging
		{
			get
			{
				return this.ViewState.GetBool("AllowPaging", false);
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether sorting is enabled.</summary>
		/// <returns>true if sorting is enabled; otherwise, false. The default value is false.</returns>
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x0005587B File Offset: 0x00053A7B
		// (set) Token: 0x0600217D RID: 8573 RVA: 0x0005588E File Offset: 0x00053A8E
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool AllowSorting
		{
			get
			{
				return this.ViewState.GetBool("AllowSorting", false);
			}
			set
			{
				this.ViewState["AllowSorting"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether <see cref="T:System.Web.UI.WebControls.BoundColumn" /> objects are automatically created and displayed in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control for each field in the data source.</summary>
		/// <returns>true if <see cref="T:System.Web.UI.WebControls.BoundColumn" /> objects are automatically created and displayed; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x000558A6 File Offset: 0x00053AA6
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x000558B9 File Offset: 0x00053AB9
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AutoGenerateColumns
		{
			get
			{
				return this.ViewState.GetBool("AutoGenerateColumns", true);
			}
			set
			{
				this.ViewState["AutoGenerateColumns"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to display in the background of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The URL of an image to display in the background of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x000558D1 File Offset: 0x00053AD1
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x000558DE File Offset: 0x00053ADE
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string BackImageUrl
		{
			get
			{
				return this.TableStyle.BackImageUrl;
			}
			set
			{
				this.TableStyle.BackImageUrl = value;
			}
		}

		/// <summary>Gets or sets the index of the currently displayed page.</summary>
		/// <returns>The zero-based index of the page currently displayed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified page index is a negative value. </exception>
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x000558EC File Offset: 0x00053AEC
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x000558FF File Offset: 0x00053AFF
		[Browsable(false)]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Behavior")]
		public int CurrentPageIndex
		{
			get
			{
				return this.ViewState.GetInt("CurrentPageIndex", 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["CurrentPageIndex"] = value;
			}
		}

		/// <summary>Gets or sets the index of an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control to edit.</summary>
		/// <returns>The index of an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control to edit. The default value is -1, which indicates that no item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control is being edited.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1. </exception>
		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x00055926 File Offset: 0x00053B26
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x00055939 File Offset: 0x00053B39
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual int EditItemIndex
		{
			get
			{
				return this.ViewState.GetInt("EditItemIndex", -1);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["EditItemIndex"] = value;
			}
		}

		/// <summary>Gets the total number of pages required to display the items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The total number of pages required to display the items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x00055960 File Offset: 0x00053B60
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Style")]
		[WebSysDescription("")]
		public int PageCount
		{
			get
			{
				if (this.paged_data_source != null)
				{
					return this.paged_data_source.PageCount;
				}
				return this.ViewState.GetInt("PageCount", 0);
			}
		}

		/// <summary>Gets or sets the number of items to display on a single page of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The number of items to display on a single page of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified page size less than 1. </exception>
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x00055987 File Offset: 0x00053B87
		// (set) Token: 0x06002188 RID: 8584 RVA: 0x0005599B File Offset: 0x00053B9B
		[WebCategory("Paging")]
		[DefaultValue(10)]
		[WebSysDescription("")]
		public virtual int PageSize
		{
			get
			{
				return this.ViewState.GetInt("PageSize", 10);
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["PageSize"] = value;
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000559C4 File Offset: 0x00053BC4
		private void AdjustItemTypes(int prev_select, int new_select)
		{
			if (this.items_list == null)
			{
				return;
			}
			int count = this.items_list.Count;
			if (count == 0)
			{
				return;
			}
			DataGridItem dataGridItem;
			if (prev_select >= 0 && prev_select < count)
			{
				dataGridItem = (DataGridItem)this.items_list[prev_select];
				if (dataGridItem.ItemType != ListItemType.EditItem)
				{
					if (dataGridItem.ItemIndex % 2 != 0)
					{
						dataGridItem.SetItemType(ListItemType.AlternatingItem);
					}
					else
					{
						dataGridItem.SetItemType(ListItemType.Item);
					}
				}
			}
			if (new_select == -1 || new_select >= count)
			{
				return;
			}
			dataGridItem = (DataGridItem)this.items_list[new_select];
			if (dataGridItem.ItemType != ListItemType.EditItem)
			{
				dataGridItem.SetItemType(ListItemType.SelectedItem);
			}
		}

		/// <summary>Gets or sets the index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1. </exception>
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x00055A52 File Offset: 0x00053C52
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x00055A68 File Offset: 0x00053C68
		[Bindable(true)]
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Paging")]
		public virtual int SelectedIndex
		{
			get
			{
				return this.ViewState.GetInt("SelectedIndex", -1);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				int @int = this.ViewState.GetInt("SelectedIndex", -1);
				this.AdjustItemTypes(@int, value);
				this.ViewState["SelectedIndex"] = value;
			}
		}

		/// <summary>Gets the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that represents the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataGrid" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x00055AB4 File Offset: 0x00053CB4
		[WebSysDescription("")]
		[WebCategory("Style")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle AlternatingItemStyle
		{
			get
			{
				if (this.alt_item_style == null)
				{
					this.alt_item_style = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.alt_item_style.TrackViewState();
					}
				}
				return this.alt_item_style;
			}
		}

		/// <summary>Gets the style properties of the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x00055AE2 File Offset: 0x00053CE2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public virtual TableItemStyle EditItemStyle
		{
			get
			{
				if (this.edit_item_style == null)
				{
					this.edit_item_style = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.edit_item_style.TrackViewState();
					}
				}
				return this.edit_item_style;
			}
		}

		/// <summary>Gets the style properties of the footer section in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the footer section of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x00055B10 File Offset: 0x00053D10
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public virtual TableItemStyle FooterStyle
		{
			get
			{
				if (this.footer_style == null)
				{
					this.footer_style = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.footer_style.TrackViewState();
					}
				}
				return this.footer_style;
			}
		}

		/// <summary>Gets the style properties of the heading section in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the heading section in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x00055B3E File Offset: 0x00053D3E
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this.header_style == null)
				{
					this.header_style = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.header_style.TrackViewState();
					}
				}
				return this.header_style;
			}
		}

		/// <summary>Gets the style properties of the items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x00055B6C File Offset: 0x00053D6C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (this.item_style == null)
				{
					this.item_style = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.item_style.TrackViewState();
					}
				}
				return this.item_style;
			}
		}

		/// <summary>Gets the style properties of the currently selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the currently selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x00055B9A File Offset: 0x00053D9A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public virtual TableItemStyle SelectedItemStyle
		{
			get
			{
				if (this.selected_style == null)
				{
					this.selected_style = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.selected_style.TrackViewState();
					}
				}
				return this.selected_style;
			}
		}

		/// <summary>Gets the style properties of the paging section of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> object that contains the style properties of the paging section of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> object.</returns>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x00055BC8 File Offset: 0x00053DC8
		[WebCategory("Style")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual DataGridPagerStyle PagerStyle
		{
			get
			{
				if (this.pager_style == null)
				{
					this.pager_style = new DataGridPagerStyle();
					if (base.IsTrackingViewState)
					{
						this.pager_style.TrackViewState();
					}
				}
				return this.pager_style;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects that represent the individual items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> that contains a collection of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects representing the individual items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x00055BF6 File Offset: 0x00053DF6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[Browsable(false)]
		public virtual DataGridItemCollection Items
		{
			get
			{
				this.EnsureChildControls();
				if (this.items == null)
				{
					if (this.items_list == null)
					{
						this.items_list = new ArrayList();
					}
					this.items = new DataGridItemCollection(this.items_list);
				}
				return this.items;
			}
		}

		/// <summary>Gets a collection of objects that represent the columns of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> object that contains a collection of objects that represent the columns of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x00055C30 File Offset: 0x00053E30
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Behavior")]
		[Editor("System.Web.UI.Design.WebControls.DataGridColumnCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[WebSysDescription("")]
		public virtual DataGridColumnCollection Columns
		{
			get
			{
				if (this.columns == null)
				{
					this.columns_list = new ArrayList();
					this.columns = new DataGridColumnCollection(this, this.columns_list);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.columns).TrackViewState();
					}
				}
				return this.columns;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x00055C70 File Offset: 0x00053E70
		private DataGridColumnCollection DataSourceColumns
		{
			get
			{
				if (this.data_source_columns == null)
				{
					this.data_source_columns_list = new ArrayList();
					this.data_source_columns = new DataGridColumnCollection(this, this.data_source_columns_list);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.data_source_columns).TrackViewState();
					}
				}
				return this.data_source_columns;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x00055CB0 File Offset: 0x00053EB0
		private Table RenderTable
		{
			get
			{
				if (this.render_table == null)
				{
					this.render_table = new ChildTable(this);
					this.render_table.AutoID = false;
				}
				return this.render_table;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x00055CD8 File Offset: 0x00053ED8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Paging")]
		public virtual DataGridItem SelectedItem
		{
			get
			{
				if (this.SelectedIndex == -1)
				{
					return null;
				}
				return this.Items[this.SelectedIndex];
			}
		}

		/// <summary>Gets or sets a value that indicates whether the footer is displayed in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>true to display the footer; otherwise, false. The default value is false.</returns>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x00055CF6 File Offset: 0x00053EF6
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x00055D09 File Offset: 0x00053F09
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual bool ShowFooter
		{
			get
			{
				return this.ViewState.GetBool("ShowFooter", false);
			}
			set
			{
				this.ViewState["ShowFooter"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the header is displayed in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>true to display the header; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x00055D21 File Offset: 0x00053F21
		// (set) Token: 0x0600219B RID: 8603 RVA: 0x00055D34 File Offset: 0x00053F34
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		public virtual bool ShowHeader
		{
			get
			{
				return this.ViewState.GetBool("ShowHeader", true);
			}
			set
			{
				this.ViewState["ShowHeader"] = value;
			}
		}

		/// <summary>Gets or sets the virtual number of items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when custom paging is used.</summary>
		/// <returns>The virtual number of items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when custom paging is used.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is a negative number. </exception>
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x00055D4C File Offset: 0x00053F4C
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x00055D5F File Offset: 0x00053F5F
		[WebCategory("Appearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public virtual int VirtualItemCount
		{
			get
			{
				return this.ViewState.GetInt("VirtualItemCount", 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["VirtualItemCount"] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>Always returns HtmlTextWriterTag.Table.</returns>
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x0004D090 File Offset: 0x0004B290
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x00047D06 File Offset: 0x00045F06
		private TableStyle TableStyle
		{
			get
			{
				return (TableStyle)base.ControlStyle;
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00055D88 File Offset: 0x00053F88
		private void AddColumnsFromSource(PagedDataSource data_source)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			Type type = null;
			bool flag = false;
			PropertyInfo property = data_source.GetType().GetProperty("Item", DataGrid.item_args);
			if (property == null)
			{
				IEnumerator enumerator = ((data_source.DataSource != null) ? data_source.GetEnumerator() : null);
				if (enumerator != null && enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (obj is ICustomTypeDescriptor || !BaseDataList.IsBindableType(obj.GetType()))
					{
						propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
					}
					else if (obj != null)
					{
						type = obj.GetType();
					}
					this.data_enumerator = enumerator;
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				type = property.PropertyType;
			}
			if (type != null)
			{
				this.AddPropertyToColumns();
				return;
			}
			if (propertyDescriptorCollection != null)
			{
				using (IEnumerator enumerator2 = propertyDescriptorCollection.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
						this.AddPropertyToColumns(propertyDescriptor, false);
					}
					return;
				}
			}
			if (!flag)
			{
				throw new HttpException(string.Format("DataGrid '{0}' cannot autogenerate columns from the given datasource. {1}", this.ID, type));
			}
		}

		/// <summary>Creates the set of columns to be used to build up the control hierarchy. When <see cref="P:System.Web.UI.WebControls.DataGrid.AutoGenerateColumns" /> is true, the columns are created to match the data source and are appended to the set of columns defined in the <see cref="P:System.Web.UI.WebControls.DataGrid.Columns" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the effective set of columns in the right order.</returns>
		/// <param name="dataSource">The data source being used to create the control hierarchy </param>
		/// <param name="useDataSource">Whether to use the data source to generate columns automatically or to use saved state. </param>
		// Token: 0x060021A1 RID: 8609 RVA: 0x00055EA4 File Offset: 0x000540A4
		protected virtual ArrayList CreateColumnSet(PagedDataSource dataSource, bool useDataSource)
		{
			ArrayList arrayList = new ArrayList();
			if (this.columns_list != null)
			{
				arrayList.AddRange(this.columns_list);
			}
			if (this.AutoGenerateColumns)
			{
				if (useDataSource)
				{
					this.data_enumerator = null;
					PropertyDescriptorCollection itemProperties = dataSource.GetItemProperties(null);
					this.DataSourceColumns.Clear();
					if (itemProperties != null)
					{
						using (IEnumerator enumerator = itemProperties.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
								this.AddPropertyToColumns(propertyDescriptor, false);
							}
							goto IL_0087;
						}
					}
					this.AddColumnsFromSource(dataSource);
				}
				IL_0087:
				if (this.data_source_columns != null && this.data_source_columns.Count > 0)
				{
					arrayList.AddRange(this.data_source_columns);
				}
			}
			return arrayList;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00055F6C File Offset: 0x0005416C
		private void AddPropertyToColumns()
		{
			BoundColumn boundColumn = new BoundColumn();
			if (base.IsTrackingViewState)
			{
				((IStateManager)boundColumn).TrackViewState();
			}
			boundColumn.Set_Owner(this);
			boundColumn.HeaderText = "Item";
			boundColumn.SortExpression = "Item";
			boundColumn.DataField = BoundColumn.thisExpr;
			this.DataSourceColumns.Add(boundColumn);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00055FC4 File Offset: 0x000541C4
		private void AddPropertyToColumns(PropertyDescriptor prop, bool tothis)
		{
			BoundColumn boundColumn = new BoundColumn();
			boundColumn.Set_Owner(this);
			if (base.IsTrackingViewState)
			{
				((IStateManager)boundColumn).TrackViewState();
			}
			boundColumn.HeaderText = prop.Name;
			boundColumn.DataField = (tothis ? BoundColumn.thisExpr : prop.Name);
			boundColumn.SortExpression = prop.Name;
			if (string.Compare(this.DataKeyField, boundColumn.DataField, StringComparison.OrdinalIgnoreCase) == 0)
			{
				boundColumn.ReadOnly = true;
			}
			this.DataSourceColumns.Add(boundColumn);
		}

		/// <summary>Marks the starting point to begin tracking and saving changes to the control as part of the control view state.</summary>
		// Token: 0x060021A4 RID: 8612 RVA: 0x00056044 File Offset: 0x00054244
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.pager_style != null)
			{
				this.pager_style.TrackViewState();
			}
			if (this.header_style != null)
			{
				this.header_style.TrackViewState();
			}
			if (this.footer_style != null)
			{
				this.footer_style.TrackViewState();
			}
			if (this.item_style != null)
			{
				this.item_style.TrackViewState();
			}
			if (this.alt_item_style != null)
			{
				this.alt_item_style.TrackViewState();
			}
			if (this.selected_style != null)
			{
				this.selected_style.TrackViewState();
			}
			if (this.edit_item_style != null)
			{
				this.edit_item_style.TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				base.ControlStyle.TrackViewState();
			}
			IStateManager stateManager = this.columns;
			if (stateManager != null)
			{
				stateManager.TrackViewState();
			}
		}

		/// <summary>Saves the current state of the <see cref="T:System.Web.UI.WebControls.DataGrid" />.</summary>
		/// <returns>The saved state of the <see cref="T:System.Web.UI.WebControls.DataGrid" />.</returns>
		// Token: 0x060021A5 RID: 8613 RVA: 0x00056100 File Offset: 0x00054300
		protected override object SaveViewState()
		{
			object[] array = new object[11];
			array[0] = base.SaveViewState();
			if (this.columns != null)
			{
				IStateManager stateManager = this.columns;
				array[1] = stateManager.SaveViewState();
			}
			if (this.pager_style != null)
			{
				array[2] = this.pager_style.SaveViewState();
			}
			if (this.header_style != null)
			{
				array[3] = this.header_style.SaveViewState();
			}
			if (this.footer_style != null)
			{
				array[4] = this.footer_style.SaveViewState();
			}
			if (this.item_style != null)
			{
				array[5] = this.item_style.SaveViewState();
			}
			if (this.alt_item_style != null)
			{
				array[6] = this.alt_item_style.SaveViewState();
			}
			if (this.selected_style != null)
			{
				array[7] = this.selected_style.SaveViewState();
			}
			if (this.edit_item_style != null)
			{
				array[8] = this.edit_item_style.SaveViewState();
			}
			if (base.ControlStyleCreated)
			{
				array[9] = base.ControlStyle.SaveViewState();
			}
			if (this.data_source_columns != null)
			{
				IStateManager stateManager2 = this.data_source_columns;
				array[10] = stateManager2.SaveViewState();
			}
			return array;
		}

		/// <summary>Loads a saved state of the <see cref="T:System.Web.UI.WebControls.DataGrid" />.</summary>
		/// <param name="savedState">A saved state of the <see cref="T:System.Web.UI.WebControls.DataGrid" />. </param>
		// Token: 0x060021A6 RID: 8614 RVA: 0x00056204 File Offset: 0x00054404
		protected override void LoadViewState(object savedState)
		{
			object[] array = savedState as object[];
			if (array == null)
			{
				return;
			}
			base.LoadViewState(array[0]);
			if (this.columns != null)
			{
				((IStateManager)this.columns).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.PagerStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.HeaderStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				this.FooterStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				this.ItemStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				this.AlternatingItemStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				this.SelectedItemStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				this.EditItemStyle.LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				base.ControlStyle.LoadViewState(array[8]);
			}
			if (array[10] != null)
			{
				foreach (object obj in (object[])array[10])
				{
					BoundColumn boundColumn = new BoundColumn();
					((IStateManager)boundColumn).TrackViewState();
					boundColumn.Set_Owner(this);
					((IStateManager)boundColumn).LoadViewState(obj);
					this.DataSourceColumns.Add(boundColumn);
				}
			}
			if (array[9] != null)
			{
				foreach (object obj2 in (object[])array[9])
				{
					BoundColumn boundColumn2 = new BoundColumn();
					boundColumn2.Set_Owner(this);
					((IStateManager)boundColumn2).LoadViewState(obj2);
					this.DataSourceColumns.Add(boundColumn2);
				}
			}
		}

		/// <summary>Creates new control style.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> the represents the new style.</returns>
		// Token: 0x060021A7 RID: 8615 RVA: 0x00056369 File Offset: 0x00054569
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				GridLines = GridLines.Both,
				CellSpacing = 0
			};
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object.</summary>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.DataGridItem" /> to initialize.</param>
		/// <param name="columns">An array of <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> objects that contains the columns in this <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</param>
		// Token: 0x060021A8 RID: 8616 RVA: 0x00056380 File Offset: 0x00054580
		protected virtual void InitializeItem(DataGridItem item, DataGridColumn[] columns)
		{
			bool flag = this.UseAccessibleHeader && item.ItemType == ListItemType.Header;
			for (int i = 0; i < columns.Length; i++)
			{
				TableCell tableCell;
				if (flag)
				{
					tableCell = new TableHeaderCell();
					tableCell.Attributes["scope"] = "col";
				}
				else
				{
					tableCell = new TableCell();
				}
				columns[i].InitializeCell(tableCell, i, item.ItemType);
				item.Cells.Add(tableCell);
			}
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object that contains the paging UI.</summary>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that contains the pager.</param>
		/// <param name="columnSpan">The number of columns to span the pager.</param>
		/// <param name="pagedDataSource">A <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> that contains the properties for the pager.</param>
		// Token: 0x060021A9 RID: 8617 RVA: 0x000563F8 File Offset: 0x000545F8
		protected virtual void InitializePager(DataGridItem item, int columnSpan, PagedDataSource pagedDataSource)
		{
			TableCell tableCell;
			if (this.PagerStyle.Mode == PagerMode.NextPrev)
			{
				tableCell = this.InitializeNextPrevPager(item, columnSpan, pagedDataSource);
			}
			else
			{
				tableCell = this.InitializeNumericPager(item, columnSpan, pagedDataSource);
			}
			item.Controls.Add(tableCell);
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x00056434 File Offset: 0x00054634
		private TableCell InitializeNumericPager(DataGridItem item, int columnSpan, PagedDataSource paged)
		{
			TableCell tableCell = new TableCell();
			tableCell.ColumnSpan = columnSpan;
			int pageButtonCount = this.PagerStyle.PageButtonCount;
			int currentPageIndex = paged.CurrentPageIndex;
			int num = currentPageIndex - currentPageIndex % pageButtonCount;
			int num2 = num + pageButtonCount;
			if (num2 > paged.PageCount)
			{
				num2 = paged.PageCount;
			}
			if (num > 0)
			{
				LinkButton linkButton = new LinkButton();
				linkButton.Text = "...";
				linkButton.CommandName = "Page";
				linkButton.CommandArgument = num.ToString(Helpers.InvariantCulture);
				linkButton.CausesValidation = false;
				tableCell.Controls.Add(linkButton);
				tableCell.Controls.Add(new LiteralControl("&nbsp;"));
			}
			for (int i = num; i < num2; i++)
			{
				string text = (i + 1).ToString(Helpers.InvariantCulture);
				Control control;
				if (i != paged.CurrentPageIndex)
				{
					control = new LinkButton
					{
						Text = text,
						CommandName = "Page",
						CommandArgument = text,
						CausesValidation = false
					};
				}
				else
				{
					control = new Label
					{
						Text = text
					};
				}
				tableCell.Controls.Add(control);
				if (i < num2 - 1)
				{
					tableCell.Controls.Add(new LiteralControl("&nbsp;"));
				}
			}
			if (num2 < paged.PageCount)
			{
				tableCell.Controls.Add(new LiteralControl("&nbsp;"));
				LinkButton linkButton2 = new LinkButton();
				linkButton2.Text = "...";
				linkButton2.CommandName = "Page";
				linkButton2.CommandArgument = (num2 + 1).ToString(Helpers.InvariantCulture);
				linkButton2.CausesValidation = false;
				tableCell.Controls.Add(linkButton2);
			}
			return tableCell;
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000565E0 File Offset: 0x000547E0
		private TableCell InitializeNextPrevPager(DataGridItem item, int columnSpan, PagedDataSource paged)
		{
			TableCell tableCell = new TableCell();
			tableCell.ColumnSpan = columnSpan;
			Control control;
			if (paged.IsFirstPage)
			{
				control = new Label
				{
					Text = this.PagerStyle.PrevPageText
				};
			}
			else
			{
				control = new DataControlLinkButton
				{
					Text = this.PagerStyle.PrevPageText,
					CommandName = "Page",
					CommandArgument = "Prev",
					CausesValidation = false
				};
			}
			Control control2;
			if (paged.Count > 0 && !paged.IsLastPage)
			{
				control2 = new DataControlLinkButton
				{
					Text = this.PagerStyle.NextPageText,
					CommandName = "Page",
					CommandArgument = "Next",
					CausesValidation = false
				};
			}
			else
			{
				control2 = new Label
				{
					Text = this.PagerStyle.NextPageText
				};
			}
			tableCell.Controls.Add(control);
			tableCell.Controls.Add(new LiteralControl("&nbsp;"));
			tableCell.Controls.Add(control2);
			return tableCell;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object.</returns>
		/// <param name="itemIndex">The index for the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object.</param>
		/// <param name="dataSourceIndex">The index of the data item from the data source.</param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</param>
		// Token: 0x060021AC RID: 8620 RVA: 0x000566D9 File Offset: 0x000548D9
		protected virtual DataGridItem CreateItem(int itemIndex, int dataSourceIndex, ListItemType itemType)
		{
			return new DataGridItem(itemIndex, dataSourceIndex, itemType);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000566E4 File Offset: 0x000548E4
		private DataGridItem CreateItem(int item_index, int data_source_index, ListItemType type, bool data_bind, object data_item, PagedDataSource paged)
		{
			DataGridItem dataGridItem = this.CreateItem(item_index, data_source_index, type);
			DataGridItemEventArgs dataGridItemEventArgs = new DataGridItemEventArgs(dataGridItem);
			bool flag = type != ListItemType.Pager;
			if (flag)
			{
				this.InitializeItem(dataGridItem, this.render_columns);
				if (data_bind)
				{
					dataGridItem.DataItem = data_item;
				}
				this.OnItemCreated(dataGridItemEventArgs);
			}
			else
			{
				this.InitializePager(dataGridItem, this.render_columns.Length, paged);
				if (this.pager_style != null)
				{
					dataGridItem.ApplyStyle(this.pager_style);
				}
				this.OnItemCreated(dataGridItemEventArgs);
			}
			this.RenderTable.Controls.Add(dataGridItem);
			if (flag && data_bind)
			{
				dataGridItem.DataBind();
				this.OnItemDataBound(dataGridItemEventArgs);
				dataGridItem.DataItem = null;
			}
			return dataGridItem;
		}

		/// <summary>Creates the control hierarchy that is used to render the <see cref="T:System.Web.UI.WebControls.DataGrid" />.</summary>
		/// <param name="useDataSource">Whether to use the data source to generate columns automatically or to use saved state. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="useDataSource" /> is true, the value of <see cref="P:System.Web.UI.WebControls.DataGrid.VirtualItemCount" /> is not set, and the selected data source does not implement the <see cref="T:System.Collections.ICollection" /> interface.- or -<paramref name="useDataSource" /> is true and the data source has an invalid <see cref="P:System.Web.UI.WebControls.PagedDataSource.CurrentPageIndex" /> property.</exception>
		// Token: 0x060021AE RID: 8622 RVA: 0x00056788 File Offset: 0x00054988
		protected override void CreateControlHierarchy(bool useDataSource)
		{
			this.Controls.Clear();
			this.RenderTable.Controls.Clear();
			this.Controls.Add(this.RenderTable);
			ArrayList arrayList = null;
			IEnumerable enumerable;
			if (useDataSource)
			{
				if (base.IsBoundUsingDataSourceID)
				{
					enumerable = this.GetData();
				}
				else
				{
					enumerable = DataSourceResolver.ResolveDataSource(this.DataSource, base.DataMember);
				}
				if (enumerable == null)
				{
					this.Controls.Clear();
					return;
				}
				arrayList = base.DataKeysArray;
				arrayList.Clear();
			}
			else
			{
				enumerable = new DataGrid.NCollection(this.ViewState.GetInt("Items", 0));
			}
			this.paged_data_source = new PagedDataSource();
			PagedDataSource pagedDataSource = this.paged_data_source;
			pagedDataSource.AllowPaging = this.AllowPaging;
			pagedDataSource.AllowCustomPaging = this.AllowCustomPaging;
			pagedDataSource.DataSource = enumerable;
			pagedDataSource.CurrentPageIndex = this.CurrentPageIndex;
			pagedDataSource.PageSize = this.PageSize;
			pagedDataSource.VirtualCount = this.VirtualItemCount;
			if (pagedDataSource.IsPagingEnabled && pagedDataSource.PageCount < pagedDataSource.CurrentPageIndex)
			{
				this.Controls.Clear();
				throw new HttpException("Invalid DataGrid PageIndex");
			}
			ArrayList arrayList2 = this.CreateColumnSet(this.paged_data_source, useDataSource);
			if (arrayList2.Count == 0)
			{
				this.Controls.Clear();
				return;
			}
			Page page = this.Page;
			if (page != null)
			{
				page.RequiresPostBackScript();
			}
			this.render_columns = new DataGridColumn[arrayList2.Count];
			for (int i = 0; i < arrayList2.Count; i++)
			{
				DataGridColumn dataGridColumn = (DataGridColumn)arrayList2[i];
				dataGridColumn.Set_Owner(this);
				dataGridColumn.Initialize();
				this.render_columns[i] = dataGridColumn;
			}
			if (pagedDataSource.IsPagingEnabled)
			{
				this.CreateItem(-1, -1, ListItemType.Pager, false, null, pagedDataSource);
			}
			this.CreateItem(-1, -1, ListItemType.Header, useDataSource, null, pagedDataSource);
			if (this.items_list == null)
			{
				this.items_list = new ArrayList();
			}
			else
			{
				this.items_list.Clear();
			}
			bool flag = false;
			IEnumerator enumerator;
			if (this.data_enumerator != null)
			{
				enumerator = this.data_enumerator;
				flag = true;
			}
			else if (pagedDataSource.DataSource != null)
			{
				enumerator = pagedDataSource.GetEnumerator();
			}
			else
			{
				enumerator = null;
			}
			int num = 0;
			bool flag2 = true;
			string text = null;
			int num2 = pagedDataSource.FirstIndexInPage;
			int selectedIndex = this.SelectedIndex;
			int editItemIndex = this.EditItemIndex;
			while (enumerator != null && (flag || enumerator.MoveNext()))
			{
				if (flag2)
				{
					flag2 = false;
					text = this.DataKeyField;
					flag = false;
				}
				object obj = enumerator.Current;
				if (useDataSource && text != "")
				{
					arrayList.Add(DataBinder.GetPropertyValue(obj, text));
				}
				ListItemType listItemType = ListItemType.Item;
				if (num == editItemIndex)
				{
					listItemType = ListItemType.EditItem;
				}
				else if (num == selectedIndex)
				{
					listItemType = ListItemType.SelectedItem;
				}
				else if (num % 2 != 0)
				{
					listItemType = ListItemType.AlternatingItem;
				}
				this.items_list.Add(this.CreateItem(num, num2, listItemType, useDataSource, obj, pagedDataSource));
				num++;
				num2++;
			}
			this.CreateItem(-1, -1, ListItemType.Footer, useDataSource, null, this.paged_data_source);
			if (pagedDataSource.IsPagingEnabled)
			{
				this.CreateItem(-1, -1, ListItemType.Pager, false, null, this.paged_data_source);
				if (useDataSource)
				{
					this.ViewState["Items"] = (pagedDataSource.IsCustomPagingEnabled ? num : pagedDataSource.DataSourceCount);
					return;
				}
			}
			else if (useDataSource)
			{
				this.ViewState["Items"] = num;
			}
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x00056ACC File Offset: 0x00054CCC
		private void ApplyColumnStyle(TableCellCollection cells, ListItemType type)
		{
			int num = Math.Min(cells.Count, this.render_columns.Length);
			if (num <= 0)
			{
				return;
			}
			for (int i = 0; i < num; i++)
			{
				TableCell tableCell = cells[i];
				DataGridColumn dataGridColumn = this.render_columns[i];
				if (!dataGridColumn.Visible)
				{
					tableCell.Visible = false;
				}
				else
				{
					Style style = dataGridColumn.GetStyle(type);
					if (style != null)
					{
						tableCell.MergeStyle(style);
					}
				}
			}
		}

		/// <summary>Sets up the control hierarchy for this <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		// Token: 0x060021B0 RID: 8624 RVA: 0x00056B38 File Offset: 0x00054D38
		protected override void PrepareControlHierarchy()
		{
			if (!this.HasControls() || this.Controls.Count == 0)
			{
				return;
			}
			Table table = this.render_table;
			table.CopyBaseAttributes(this);
			table.ApplyStyle(base.ControlStyle);
			table.Caption = this.Caption;
			table.CaptionAlign = this.CaptionAlign;
			table.Enabled = base.IsEnabled;
			bool flag = true;
			foreach (object obj in table.Rows)
			{
				DataGridItem dataGridItem = (DataGridItem)obj;
				switch (dataGridItem.ItemType)
				{
				case ListItemType.Header:
					if (!this.ShowHeader)
					{
						dataGridItem.Visible = false;
					}
					else
					{
						if (this.header_style != null)
						{
							dataGridItem.MergeStyle(this.header_style);
						}
						this.ApplyColumnStyle(dataGridItem.Cells, ListItemType.Header);
					}
					break;
				case ListItemType.Footer:
					if (!this.ShowFooter)
					{
						dataGridItem.Visible = false;
					}
					else
					{
						if (this.footer_style != null)
						{
							dataGridItem.MergeStyle(this.footer_style);
						}
						this.ApplyColumnStyle(dataGridItem.Cells, ListItemType.Footer);
					}
					break;
				case ListItemType.Item:
					this.ApplyItemStyle(dataGridItem);
					break;
				case ListItemType.AlternatingItem:
					this.ApplyItemStyle(dataGridItem);
					break;
				case ListItemType.SelectedItem:
					dataGridItem.MergeStyle(this.selected_style);
					this.ApplyItemStyle(dataGridItem);
					this.ApplyColumnStyle(dataGridItem.Cells, ListItemType.SelectedItem);
					break;
				case ListItemType.EditItem:
					dataGridItem.MergeStyle(this.edit_item_style);
					this.ApplyItemStyle(dataGridItem);
					this.ApplyColumnStyle(dataGridItem.Cells, ListItemType.EditItem);
					break;
				case ListItemType.Separator:
					this.ApplyColumnStyle(dataGridItem.Cells, ListItemType.Separator);
					break;
				case ListItemType.Pager:
				{
					DataGridPagerStyle pagerStyle = this.PagerStyle;
					if (!pagerStyle.Visible || !this.paged_data_source.IsPagingEnabled)
					{
						dataGridItem.Visible = false;
					}
					else
					{
						if (flag)
						{
							dataGridItem.Visible = pagerStyle.Position > PagerPosition.Bottom;
						}
						else
						{
							dataGridItem.Visible = pagerStyle.Position != PagerPosition.Top;
						}
						flag = false;
					}
					if (dataGridItem.Visible)
					{
						dataGridItem.MergeStyle(this.pager_style);
					}
					break;
				}
				}
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00056D6C File Offset: 0x00054F6C
		private void ApplyItemStyle(DataGridItem item)
		{
			if (item.ItemIndex % 2 != 0)
			{
				item.MergeStyle(this.alt_item_style);
			}
			item.MergeStyle(this.item_style);
			this.ApplyColumnStyle(item.Cells, ListItemType.Item);
		}

		/// <summary>Passes the event raised by a control within the container up the page's UI server control hierarchy.</summary>
		/// <returns>true to indicate that this method is passing an event raised by a control within the container up the page's UI server control hierarchy; otherwise, false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x060021B2 RID: 8626 RVA: 0x00056DA0 File Offset: 0x00054FA0
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			DataGridCommandEventArgs dataGridCommandEventArgs = e as DataGridCommandEventArgs;
			if (dataGridCommandEventArgs == null)
			{
				return false;
			}
			string commandName = dataGridCommandEventArgs.CommandName;
			CultureInfo invariantCulture = Helpers.InvariantCulture;
			this.OnItemCommand(dataGridCommandEventArgs);
			if (string.Compare(commandName, "Cancel", true, invariantCulture) == 0)
			{
				this.OnCancelCommand(dataGridCommandEventArgs);
			}
			else if (string.Compare(commandName, "Delete", true, invariantCulture) == 0)
			{
				this.OnDeleteCommand(dataGridCommandEventArgs);
			}
			else if (string.Compare(commandName, "Edit", true, invariantCulture) == 0)
			{
				this.OnEditCommand(dataGridCommandEventArgs);
			}
			else if (string.Compare(commandName, "Select", true, invariantCulture) == 0)
			{
				this.SelectedIndex = dataGridCommandEventArgs.Item.ItemIndex;
				this.OnSelectedIndexChanged(dataGridCommandEventArgs);
			}
			else if (string.Compare(commandName, "Sort", true, invariantCulture) == 0)
			{
				DataGridSortCommandEventArgs dataGridSortCommandEventArgs = new DataGridSortCommandEventArgs(dataGridCommandEventArgs.CommandSource, dataGridCommandEventArgs);
				this.OnSortCommand(dataGridSortCommandEventArgs);
			}
			else if (string.Compare(commandName, "Update", true, invariantCulture) == 0)
			{
				this.OnUpdateCommand(dataGridCommandEventArgs);
			}
			else if (string.Compare(commandName, "Page", true, invariantCulture) == 0)
			{
				int num;
				if (string.Compare((string)dataGridCommandEventArgs.CommandArgument, "Next", true, invariantCulture) == 0)
				{
					num = this.CurrentPageIndex + 1;
				}
				else if (string.Compare((string)dataGridCommandEventArgs.CommandArgument, "Prev", true, invariantCulture) == 0)
				{
					num = this.CurrentPageIndex - 1;
				}
				else
				{
					num = int.Parse((string)dataGridCommandEventArgs.CommandArgument, invariantCulture) - 1;
				}
				DataGridPageChangedEventArgs dataGridPageChangedEventArgs = new DataGridPageChangedEventArgs(dataGridCommandEventArgs.CommandSource, num);
				this.OnPageIndexChanged(dataGridPageChangedEventArgs);
			}
			return true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.CancelCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
		// Token: 0x060021B3 RID: 8627 RVA: 0x00056F14 File Offset: 0x00055114
		protected virtual void OnCancelCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.CancelCommandEvent];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.DeleteCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
		// Token: 0x060021B4 RID: 8628 RVA: 0x00056F44 File Offset: 0x00055144
		protected virtual void OnDeleteCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.DeleteCommandEvent];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.EditCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
		// Token: 0x060021B5 RID: 8629 RVA: 0x00056F74 File Offset: 0x00055174
		protected virtual void OnEditCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.EditCommandEvent];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
		// Token: 0x060021B6 RID: 8630 RVA: 0x00056FA4 File Offset: 0x000551A4
		protected virtual void OnItemCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.ItemCommandEvent];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCreated" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridItemEventArgs" /> that contains event data. </param>
		// Token: 0x060021B7 RID: 8631 RVA: 0x00056FD4 File Offset: 0x000551D4
		protected virtual void OnItemCreated(DataGridItemEventArgs e)
		{
			DataGridItemEventHandler dataGridItemEventHandler = (DataGridItemEventHandler)base.Events[DataGrid.ItemCreatedEvent];
			if (dataGridItemEventHandler != null)
			{
				dataGridItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.ItemDataBound" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridItemEventArgs" /> that contains event data. </param>
		// Token: 0x060021B8 RID: 8632 RVA: 0x00057004 File Offset: 0x00055204
		protected virtual void OnItemDataBound(DataGridItemEventArgs e)
		{
			DataGridItemEventHandler dataGridItemEventHandler = (DataGridItemEventHandler)base.Events[DataGrid.ItemDataBoundEvent];
			if (dataGridItemEventHandler != null)
			{
				dataGridItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.PageIndexChanged" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridPageChangedEventArgs" /> that contains event data. </param>
		// Token: 0x060021B9 RID: 8633 RVA: 0x00057034 File Offset: 0x00055234
		protected virtual void OnPageIndexChanged(DataGridPageChangedEventArgs e)
		{
			DataGridPageChangedEventHandler dataGridPageChangedEventHandler = (DataGridPageChangedEventHandler)base.Events[DataGrid.PageIndexChangedEvent];
			if (dataGridPageChangedEventHandler != null)
			{
				dataGridPageChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.SortCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridSortCommandEventArgs" /> that contains event data. </param>
		// Token: 0x060021BA RID: 8634 RVA: 0x00057064 File Offset: 0x00055264
		protected virtual void OnSortCommand(DataGridSortCommandEventArgs e)
		{
			DataGridSortCommandEventHandler dataGridSortCommandEventHandler = (DataGridSortCommandEventHandler)base.Events[DataGrid.SortCommandEvent];
			if (dataGridSortCommandEventHandler != null)
			{
				dataGridSortCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.UpdateCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
		// Token: 0x060021BB RID: 8635 RVA: 0x00057094 File Offset: 0x00055294
		protected virtual void OnUpdateCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.UpdateCommandEvent];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		/// <summary>Occurs when the Cancel button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060021BC RID: 8636 RVA: 0x000570C2 File Offset: 0x000552C2
		// (remove) Token: 0x060021BD RID: 8637 RVA: 0x000570D5 File Offset: 0x000552D5
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataGridCommandEventHandler CancelCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.CancelCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.CancelCommandEvent, value);
			}
		}

		/// <summary>Occurs when the Delete button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		// Token: 0x14000069 RID: 105
		// (add) Token: 0x060021BE RID: 8638 RVA: 0x000570E8 File Offset: 0x000552E8
		// (remove) Token: 0x060021BF RID: 8639 RVA: 0x000570FB File Offset: 0x000552FB
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataGridCommandEventHandler DeleteCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.DeleteCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.DeleteCommandEvent, value);
			}
		}

		/// <summary>Occurs when the Edit button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x060021C0 RID: 8640 RVA: 0x0005710E File Offset: 0x0005530E
		// (remove) Token: 0x060021C1 RID: 8641 RVA: 0x00057121 File Offset: 0x00055321
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataGridCommandEventHandler EditCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.EditCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EditCommandEvent, value);
			}
		}

		/// <summary>Occurs when any button is clicked in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060021C2 RID: 8642 RVA: 0x00057134 File Offset: 0x00055334
		// (remove) Token: 0x060021C3 RID: 8643 RVA: 0x00057147 File Offset: 0x00055347
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataGridCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.ItemCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.ItemCommandEvent, value);
			}
		}

		/// <summary>Occurs on the server when an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control is created.</summary>
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060021C4 RID: 8644 RVA: 0x0005715A File Offset: 0x0005535A
		// (remove) Token: 0x060021C5 RID: 8645 RVA: 0x0005716D File Offset: 0x0005536D
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataGridItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(DataGrid.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.ItemCreatedEvent, value);
			}
		}

		/// <summary>Occurs after an item is data bound to the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x060021C6 RID: 8646 RVA: 0x00057180 File Offset: 0x00055380
		// (remove) Token: 0x060021C7 RID: 8647 RVA: 0x00057193 File Offset: 0x00055393
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataGridItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(DataGrid.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.ItemDataBoundEvent, value);
			}
		}

		/// <summary>Occurs when one of the page selection elements is clicked.</summary>
		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060021C8 RID: 8648 RVA: 0x000571A6 File Offset: 0x000553A6
		// (remove) Token: 0x060021C9 RID: 8649 RVA: 0x000571B9 File Offset: 0x000553B9
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataGridPageChangedEventHandler PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.PageIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.PageIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when a column is sorted.</summary>
		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060021CA RID: 8650 RVA: 0x000571CC File Offset: 0x000553CC
		// (remove) Token: 0x060021CB RID: 8651 RVA: 0x000571DF File Offset: 0x000553DF
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataGridSortCommandEventHandler SortCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.SortCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.SortCommandEvent, value);
			}
		}

		/// <summary>Occurs when the Update button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x060021CC RID: 8652 RVA: 0x000571F2 File Offset: 0x000553F2
		// (remove) Token: 0x060021CD RID: 8653 RVA: 0x000571F2 File Offset: 0x000553F2
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataGridCommandEventHandler UpdateCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.UpdateCommandEvent, value);
			}
			remove
			{
				base.Events.AddHandler(DataGrid.UpdateCommandEvent, value);
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x00057210 File Offset: 0x00055410
		// Note: this type is marked as 'beforefieldinit'.
		static DataGrid()
		{
			DataGrid.CancelCommandEvent = new object();
			DataGrid.DeleteCommandEvent = new object();
			DataGrid.EditCommandEvent = new object();
			DataGrid.ItemCommandEvent = new object();
			DataGrid.ItemCreatedEvent = new object();
			DataGrid.ItemDataBoundEvent = new object();
			DataGrid.PageIndexChangedEvent = new object();
			DataGrid.SortCommandEvent = new object();
			DataGrid.UpdateCommandEvent = new object();
			DataGrid.item_args = new Type[] { typeof(int) };
		}

		/// <summary>Represents the Cancel command name. This field is read-only.</summary>
		// Token: 0x040018D0 RID: 6352
		public const string CancelCommandName = "Cancel";

		/// <summary>Represents the Delete command name. This field is read-only.</summary>
		// Token: 0x040018D1 RID: 6353
		public const string DeleteCommandName = "Delete";

		/// <summary>Represents the Edit command name. This field is read-only.</summary>
		// Token: 0x040018D2 RID: 6354
		public const string EditCommandName = "Edit";

		/// <summary>Represents the Select command name. This field is read-only.</summary>
		// Token: 0x040018D3 RID: 6355
		public const string SelectCommandName = "Select";

		/// <summary>Represents the Sort command name. This field is read-only.</summary>
		// Token: 0x040018D4 RID: 6356
		public const string SortCommandName = "Sort";

		/// <summary>Represents the Update command name. This field is read-only.</summary>
		// Token: 0x040018D5 RID: 6357
		public const string UpdateCommandName = "Update";

		/// <summary>Represents the Page command name. This field is read-only.</summary>
		// Token: 0x040018D6 RID: 6358
		public const string PageCommandName = "Page";

		/// <summary>Represents the Next command argument. This field is read-only.</summary>
		// Token: 0x040018D7 RID: 6359
		public const string NextPageCommandArgument = "Next";

		/// <summary>Represents the Prev command argument. This field is read-only.</summary>
		// Token: 0x040018D8 RID: 6360
		public const string PrevPageCommandArgument = "Prev";

		// Token: 0x040018E2 RID: 6370
		private TableItemStyle alt_item_style;

		// Token: 0x040018E3 RID: 6371
		private TableItemStyle edit_item_style;

		// Token: 0x040018E4 RID: 6372
		private TableItemStyle footer_style;

		// Token: 0x040018E5 RID: 6373
		private TableItemStyle header_style;

		// Token: 0x040018E6 RID: 6374
		private TableItemStyle item_style;

		// Token: 0x040018E7 RID: 6375
		private TableItemStyle selected_style;

		// Token: 0x040018E8 RID: 6376
		private DataGridPagerStyle pager_style;

		// Token: 0x040018E9 RID: 6377
		private ArrayList items_list;

		// Token: 0x040018EA RID: 6378
		private DataGridItemCollection items;

		// Token: 0x040018EB RID: 6379
		private ArrayList columns_list;

		// Token: 0x040018EC RID: 6380
		private DataGridColumnCollection columns;

		// Token: 0x040018ED RID: 6381
		private ArrayList data_source_columns_list;

		// Token: 0x040018EE RID: 6382
		private DataGridColumnCollection data_source_columns;

		// Token: 0x040018EF RID: 6383
		private Table render_table;

		// Token: 0x040018F0 RID: 6384
		private DataGridColumn[] render_columns;

		// Token: 0x040018F1 RID: 6385
		private PagedDataSource paged_data_source;

		// Token: 0x040018F2 RID: 6386
		private IEnumerator data_enumerator;

		// Token: 0x040018F3 RID: 6387
		private static Type[] item_args;

		// Token: 0x02000376 RID: 886
		private sealed class NCollection : ICollection, IEnumerable
		{
			// Token: 0x060021D0 RID: 8656 RVA: 0x0005728F File Offset: 0x0005548F
			public NCollection(int n)
			{
				this.n = n;
			}

			// Token: 0x060021D1 RID: 8657 RVA: 0x0005729E File Offset: 0x0005549E
			public IEnumerator GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.n; i = num + 1)
				{
					yield return i;
					num = i;
				}
				yield break;
			}

			// Token: 0x17000A99 RID: 2713
			// (get) Token: 0x060021D2 RID: 8658 RVA: 0x000572AD File Offset: 0x000554AD
			public int Count
			{
				get
				{
					return this.n;
				}
			}

			// Token: 0x17000A9A RID: 2714
			// (get) Token: 0x060021D3 RID: 8659 RVA: 0x00008A69 File Offset: 0x00006C69
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000A9B RID: 2715
			// (get) Token: 0x060021D4 RID: 8660 RVA: 0x00002058 File Offset: 0x00000258
			public object SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060021D5 RID: 8661 RVA: 0x000572B5 File Offset: 0x000554B5
			public void CopyTo(Array array, int index)
			{
				throw new NotImplementedException("This should never be called");
			}

			// Token: 0x040018F4 RID: 6388
			private int n;
		}
	}
}
