using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a row in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	// Token: 0x02000420 RID: 1056
	[DefaultProperty("Cells")]
	[ParseChildren(true, "Cells")]
	[ToolboxItem("")]
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableRow : WebControl
	{
		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x0007DC4E File Offset: 0x0007BE4E
		// (set) Token: 0x06002FA8 RID: 12200 RVA: 0x0007DC56 File Offset: 0x0007BE56
		internal TableRowCollection Container { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableRow" /> class.</summary>
		// Token: 0x06002FA9 RID: 12201 RVA: 0x0007DC5F File Offset: 0x0007BE5F
		public TableRow()
			: base(HtmlTextWriterTag.Tr)
		{
			base.AutoID = false;
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x0007DC70 File Offset: 0x0007BE70
		internal bool TableRowSectionSet
		{
			get
			{
				return this.tableRowSectionSet;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects that represent the cells of a row in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> object that represents a collection of cells from a row of a <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x0007DC78 File Offset: 0x0007BE78
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual TableCellCollection Cells
		{
			get
			{
				if (this.cells == null)
				{
					this.cells = new TableCellCollection(this);
				}
				return this.cells;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the contents in the row.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default value is NotSet.</returns>
		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x0007DC94 File Offset: 0x0007BE94
		// (set) Token: 0x06002FAD RID: 12205 RVA: 0x0007DCAB File Offset: 0x0007BEAB
		[WebSysDescription("")]
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return this.TableItemStyle.HorizontalAlign;
			}
			set
			{
				this.TableItemStyle.HorizontalAlign = value;
			}
		}

		/// <summary>Gets or sets the vertical alignment of the contents in the row.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> values. The default value is NotSet.</returns>
		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x0007DCB9 File Offset: 0x0007BEB9
		// (set) Token: 0x06002FAF RID: 12207 RVA: 0x0007DCD0 File Offset: 0x0007BED0
		[DefaultValue(VerticalAlign.NotSet)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual VerticalAlign VerticalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return VerticalAlign.NotSet;
				}
				return this.TableItemStyle.VerticalAlign;
			}
			set
			{
				this.TableItemStyle.VerticalAlign = value;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x0007D401 File Offset: 0x0007B601
		private TableItemStyle TableItemStyle
		{
			get
			{
				return base.ControlStyle as TableItemStyle;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object for the <see cref="T:System.Web.UI.WebControls.TableRow" /> control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object that contains the <see cref="T:System.Web.UI.WebControls.TableRow" /> control's child server controls.</returns>
		// Token: 0x06002FB2 RID: 12210 RVA: 0x0007DCDE File Offset: 0x0007BEDE
		protected override ControlCollection CreateControlCollection()
		{
			return new TableRow.CellControlCollection(this);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object for the <see cref="T:System.Web.UI.WebControls.TableRow" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that specifies the style properties for the <see cref="T:System.Web.UI.WebControls.TableRow" /> control.The <see cref="M:System.Web.UI.WebControls.TableRow.CreateControlCollection" /> method is primarily of interest to control developers extending the functionality of the <see cref="T:System.Web.UI.WebControls.TableRow" /> control.</returns>
		// Token: 0x06002FB3 RID: 12211 RVA: 0x00059A1E File Offset: 0x00057C1E
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle(this.ViewState);
		}

		/// <summary>Gets or sets the location for a <see cref="T:System.Web.UI.WebControls.TableRow" /> object in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableRowSection" /> value. The default is <see cref="F:System.Web.UI.WebControls.TableRowSection.TableBody" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="T:System.Web.UI.WebControls.TableRowSection" /> is not valid.</exception>
		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x0007DCE8 File Offset: 0x0007BEE8
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x0007DD14 File Offset: 0x0007BF14
		[DefaultValue(TableRowSection.TableBody)]
		public virtual TableRowSection TableSection
		{
			get
			{
				object obj = this.ViewState["TableSection"];
				if (obj != null)
				{
					return (TableRowSection)obj;
				}
				return TableRowSection.TableBody;
			}
			set
			{
				if (value < TableRowSection.TableHeader || value > TableRowSection.TableFooter)
				{
					throw new ArgumentOutOfRangeException("TableSection");
				}
				this.ViewState["TableSection"] = (int)value;
				this.tableRowSectionSet = true;
				TableRowCollection container = this.Container;
				if (container != null)
				{
					container.RowTableSectionSet();
				}
			}
		}

		// Token: 0x04001BEF RID: 7151
		private TableCellCollection cells;

		// Token: 0x04001BF0 RID: 7152
		private bool tableRowSectionSet;

		/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects that are the cells of a <see cref="T:System.Web.UI.WebControls.TableRow" /> control. </summary>
		// Token: 0x02000421 RID: 1057
		protected class CellControlCollection : ControlCollection
		{
			// Token: 0x06002FB6 RID: 12214 RVA: 0x0002B24E File Offset: 0x0002944E
			internal CellControlCollection(TableRow owner)
				: base(owner)
			{
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" /> collection.</summary>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" />. </param>
			/// <exception cref="T:System.ArgumentException">The added <see cref="T:System.Web.UI.Control" /> must be of the type <see cref="T:System.Web.UI.WebControls.TableCell" />. </exception>
			// Token: 0x06002FB7 RID: 12215 RVA: 0x0007DD61 File Offset: 0x0007BF61
			public override void Add(Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is TableCell))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an TableCell instance."));
				}
				base.Add(child);
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" /> collection. The new control is added to the array at the specified index location.</summary>
			/// <param name="index">The location in the array to add the child control. </param>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" />. </param>
			/// <exception cref="T:System.ArgumentException">The added <see cref="T:System.Web.UI.Control" /> must be of the type <see cref="T:System.Web.UI.WebControls.TableCell" />.</exception>
			// Token: 0x06002FB8 RID: 12216 RVA: 0x0007DD95 File Offset: 0x0007BF95
			public override void AddAt(int index, Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is TableCell))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an TableCell instance."));
				}
				base.AddAt(index, child);
			}
		}
	}
}
