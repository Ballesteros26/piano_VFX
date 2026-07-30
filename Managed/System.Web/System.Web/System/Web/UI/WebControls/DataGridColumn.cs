using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for the different column types of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	// Token: 0x02000378 RID: 888
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class DataGridColumn : IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class.</summary>
		// Token: 0x060021DC RID: 8668 RVA: 0x0005734C File Offset: 0x0005554C
		protected DataGridColumn()
		{
			this.viewstate = new StateBag();
		}

		/// <summary>Gets the style properties for the footer section of the column.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the footer section of the column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x0005735F File Offset: 0x0005555F
		[WebCategory("Misc")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[DefaultValue(null)]
		public virtual TableItemStyle FooterStyle
		{
			get
			{
				if (this.footer_style == null)
				{
					this.footer_style = new TableItemStyle();
					if (this.tracking_viewstate)
					{
						this.footer_style.TrackViewState();
					}
				}
				return this.footer_style;
			}
		}

		/// <summary>Gets or sets the text displayed in the footer section of the column.</summary>
		/// <returns>The text displayed in the footer section of the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x0005738D File Offset: 0x0005558D
		// (set) Token: 0x060021DF RID: 8671 RVA: 0x000573A4 File Offset: 0x000555A4
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DefaultValue("")]
		public virtual string FooterText
		{
			get
			{
				return this.viewstate.GetString("FooterText", string.Empty);
			}
			set
			{
				this.viewstate["FooterText"] = value;
			}
		}

		/// <summary>Gets or sets the location of an image to display in the header section of the column.</summary>
		/// <returns>The location of an image to display in the header section of the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000573B7 File Offset: 0x000555B7
		// (set) Token: 0x060021E1 RID: 8673 RVA: 0x000573CE File Offset: 0x000555CE
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[UrlProperty]
		public virtual string HeaderImageUrl
		{
			get
			{
				return this.viewstate.GetString("HeaderImageUrl", string.Empty);
			}
			set
			{
				this.viewstate["HeaderImageUrl"] = value;
			}
		}

		/// <summary>Gets the style properties for the header section of the column.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the header section of the column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000573E1 File Offset: 0x000555E1
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this.header_style == null)
				{
					this.header_style = new TableItemStyle();
					if (this.tracking_viewstate)
					{
						this.header_style.TrackViewState();
					}
				}
				return this.header_style;
			}
		}

		/// <summary>Gets or sets the text displayed in the header section of the column.</summary>
		/// <returns>The text displayed in the header section of the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x0005740F File Offset: 0x0005560F
		// (set) Token: 0x060021E4 RID: 8676 RVA: 0x00057426 File Offset: 0x00055626
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public virtual string HeaderText
		{
			get
			{
				return this.viewstate.GetString("HeaderText", string.Empty);
			}
			set
			{
				this.viewstate["HeaderText"] = value;
			}
		}

		/// <summary>Gets the style properties for the item cells of the column.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the item cells of the column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x00057439 File Offset: 0x00055639
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (this.item_style == null)
				{
					this.item_style = new TableItemStyle();
					if (this.tracking_viewstate)
					{
						this.item_style.TrackViewState();
					}
				}
				return this.item_style;
			}
		}

		/// <summary>Gets or sets the name of the field or expression to pass to the <see cref="M:System.Web.UI.WebControls.DataGrid.OnSortCommand(System.Web.UI.WebControls.DataGridSortCommandEventArgs)" /> method when a column is selected for sorting.</summary>
		/// <returns>The name of the field to pass to <see cref="M:System.Web.UI.WebControls.DataGrid.OnSortCommand(System.Web.UI.WebControls.DataGridSortCommandEventArgs)" /> when a column is selected for sorting. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x00057467 File Offset: 0x00055667
		// (set) Token: 0x060021E7 RID: 8679 RVA: 0x0005747E File Offset: 0x0005567E
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public virtual string SortExpression
		{
			get
			{
				return this.viewstate.GetString("SortExpression", string.Empty);
			}
			set
			{
				this.viewstate["SortExpression"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the column is visible in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>true if the column is visible in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control; otherwise, false. The default value is true.</returns>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x00057491 File Offset: 0x00055691
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x000574A4 File Offset: 0x000556A4
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return this.viewstate.GetBool("Visible", true);
			}
			set
			{
				this.viewstate["Visible"] = value;
			}
		}

		/// <summary>Provides the base implementation to reset a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to its initial state.</summary>
		// Token: 0x060021EA RID: 8682 RVA: 0x000574BC File Offset: 0x000556BC
		public virtual void Initialize()
		{
			if (this.owner != null && this.owner.Site != null)
			{
				this.design = this.owner.Site.DesignMode;
			}
		}

		/// <summary>Provides the base implementation to reset the specified cell from a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to its initial state.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents the cell to reset. </param>
		/// <param name="columnIndex">The column number where the cell is located. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x060021EB RID: 8683 RVA: 0x000574EC File Offset: 0x000556EC
		public virtual void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			if (itemType != ListItemType.Header)
			{
				if (itemType != ListItemType.Footer)
				{
					return;
				}
				string footerText = this.FooterText;
				if (footerText.Length > 0)
				{
					cell.Text = footerText;
					return;
				}
				cell.Text = "&nbsp;";
				return;
			}
			else
			{
				bool flag = false;
				string sortExpression = this.SortExpression;
				if (this.owner != null && sortExpression.Length > 0)
				{
					flag = this.owner.AllowSorting;
				}
				string headerImageUrl = this.HeaderImageUrl;
				if (headerImageUrl.Length > 0)
				{
					if (flag)
					{
						ImageButton imageButton = new ImageButton();
						imageButton.ImageUrl = headerImageUrl;
						imageButton.CommandName = "Sort";
						imageButton.CommandArgument = sortExpression;
						cell.Controls.Add(imageButton);
						return;
					}
					Image image = new Image();
					image.ImageUrl = headerImageUrl;
					cell.Controls.Add(image);
					return;
				}
				else
				{
					if (flag)
					{
						LinkButton linkButton = new DataGridColumn.ForeColorLinkButton();
						linkButton.Text = this.HeaderText;
						linkButton.CommandName = "Sort";
						linkButton.CommandArgument = sortExpression;
						cell.Controls.Add(linkButton);
						return;
					}
					string headerText = this.HeaderText;
					if (headerText.Length > 0)
					{
						cell.Text = headerText;
						return;
					}
					cell.Text = "&nbsp;";
					return;
				}
			}
		}

		/// <summary>Returns the string representation of the column.</summary>
		/// <returns>Returns <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x060021EC RID: 8684 RVA: 0x0000EE9B File Offset: 0x0000D09B
		public override string ToString()
		{
			return string.Empty;
		}

		/// <summary>Gets a value that indicates whether the column is in design mode.</summary>
		/// <returns>true if the column is in design mode; otherwise, false.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x0005760C File Offset: 0x0005580C
		protected bool DesignMode
		{
			get
			{
				return this.design;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that the column is a member of.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that the column is a member of.</returns>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x00057614 File Offset: 0x00055814
		protected DataGrid Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0005761C File Offset: 0x0005581C
		internal TableItemStyle GetStyle(ListItemType type)
		{
			if (type == ListItemType.Header)
			{
				return this.header_style;
			}
			if (type == ListItemType.Footer)
			{
				return this.footer_style;
			}
			return this.item_style;
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00057639 File Offset: 0x00055839
		internal void Set_Owner(DataGrid value)
		{
			this.owner = value;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.StateBag" /> object that allows a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to store its properties.</summary>
		/// <returns>The <see cref="T:System.Web.UI.StateBag" /> for the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />.</returns>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x00057642 File Offset: 0x00055842
		protected StateBag ViewState
		{
			get
			{
				return this.viewstate;
			}
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.Design.WebControls.DataGridDesigner.OnColumnsChanged" /> method.</summary>
		// Token: 0x060021F2 RID: 8690 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void OnColumnChanged()
		{
		}

		/// <summary>Loads previously saved state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object to restore.</param>
		// Token: 0x060021F3 RID: 8691 RVA: 0x0005764A File Offset: 0x0005584A
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Returns an object containing state changes.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x060021F4 RID: 8692 RVA: 0x00057653 File Offset: 0x00055853
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Starts tracking state changes.</summary>
		// Token: 0x060021F5 RID: 8693 RVA: 0x0005765B File Offset: 0x0005585B
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether the column is tracking view state changes.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x00057663 File Offset: 0x00055863
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Loads the state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />. </param>
		// Token: 0x060021F7 RID: 8695 RVA: 0x0005766C File Offset: 0x0005586C
		protected virtual void LoadViewState(object savedState)
		{
			object[] array = savedState as object[];
			if (array == null)
			{
				return;
			}
			if (array[0] != null)
			{
				this.viewstate.LoadViewState(array[0]);
			}
			if (array[1] != null)
			{
				this.FooterStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.HeaderStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.ItemStyle.LoadViewState(array[3]);
			}
		}

		/// <summary>Saves the current state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />.</returns>
		// Token: 0x060021F8 RID: 8696 RVA: 0x000576D0 File Offset: 0x000558D0
		protected virtual object SaveViewState()
		{
			object[] array = new object[4];
			array[0] = this.viewstate.SaveViewState();
			if (this.footer_style != null)
			{
				array[1] = this.footer_style.SaveViewState();
			}
			if (this.header_style != null)
			{
				array[2] = this.header_style.SaveViewState();
			}
			if (this.item_style != null)
			{
				array[3] = this.item_style.SaveViewState();
			}
			return array;
		}

		/// <summary>Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x060021F9 RID: 8697 RVA: 0x00057738 File Offset: 0x00055938
		protected virtual void TrackViewState()
		{
			this.tracking_viewstate = true;
			this.viewstate.TrackViewState();
			if (this.footer_style != null)
			{
				this.footer_style.TrackViewState();
			}
			if (this.header_style != null)
			{
				this.header_style.TrackViewState();
			}
			if (this.item_style != null)
			{
				this.item_style.TrackViewState();
			}
		}

		/// <summary>Gets a value that determines whether the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object is marked to save its state.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> is marked; otherwise, false.</returns>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x00057790 File Offset: 0x00055990
		protected bool IsTrackingViewState
		{
			get
			{
				return this.tracking_viewstate;
			}
		}

		// Token: 0x040018F9 RID: 6393
		private DataGrid owner;

		// Token: 0x040018FA RID: 6394
		private StateBag viewstate;

		// Token: 0x040018FB RID: 6395
		private bool tracking_viewstate;

		// Token: 0x040018FC RID: 6396
		private bool design;

		// Token: 0x040018FD RID: 6397
		private TableItemStyle footer_style;

		// Token: 0x040018FE RID: 6398
		private TableItemStyle header_style;

		// Token: 0x040018FF RID: 6399
		private TableItemStyle item_style;

		// Token: 0x02000379 RID: 889
		internal class ForeColorLinkButton : LinkButton
		{
			// Token: 0x060021FB RID: 8699 RVA: 0x00057798 File Offset: 0x00055998
			private Color GetForeColor(WebControl control)
			{
				if (control == null)
				{
					return Color.Empty;
				}
				if (control is Table)
				{
					return control.ControlStyle.ForeColor;
				}
				Color foreColor = control.ControlStyle.ForeColor;
				if (foreColor != Color.Empty)
				{
					return foreColor;
				}
				return this.GetForeColor((WebControl)control.Parent);
			}

			// Token: 0x060021FC RID: 8700 RVA: 0x000577F0 File Offset: 0x000559F0
			protected internal override void Render(HtmlTextWriter writer)
			{
				Color foreColor = this.GetForeColor(this);
				if (foreColor != Color.Empty)
				{
					this.ForeColor = foreColor;
				}
				base.Render(writer);
			}
		}
	}
}
