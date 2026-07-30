using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a table on a Web page.</summary>
	// Token: 0x02000418 RID: 1048
	[DefaultProperty("Rows")]
	[Designer("System.Web.UI.Design.WebControls.TableDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(true, "Rows")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Table : WebControl, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Table" /> class.</summary>
		// Token: 0x06002F43 RID: 12099 RVA: 0x0007CED4 File Offset: 0x0007B0D4
		public Table()
			: base(HtmlTextWriterTag.Table)
		{
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x0007CEDE File Offset: 0x0007B0DE
		// (set) Token: 0x06002F45 RID: 12101 RVA: 0x0007CEE6 File Offset: 0x0007B0E6
		internal bool GenerateTableSections
		{
			get
			{
				return this.generateTableSections;
			}
			set
			{
				this.generateTableSections = value;
			}
		}

		/// <summary>Gets or sets the URL of the background image to display behind the <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>The URL of the background image for the <see cref="T:System.Web.UI.WebControls.Table" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x0007CEEF File Offset: 0x0007B0EF
		// (set) Token: 0x06002F47 RID: 12103 RVA: 0x0007CF0A File Offset: 0x0007B10A
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return string.Empty;
				}
				return this.TableStyle.BackImageUrl;
			}
			set
			{
				this.TableStyle.BackImageUrl = value;
			}
		}

		/// <summary>Gets or sets the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This property is provided to make the control more accessible to users of Assistive Technology devices.</summary>
		/// <returns>A string that represents the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.Table" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x0007CF18 File Offset: 0x0007B118
		// (set) Token: 0x06002F49 RID: 12105 RVA: 0x00047A1A File Offset: 0x00045C1A
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Accessibility")]
		public virtual string Caption
		{
			get
			{
				object obj = this.ViewState["Caption"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("Caption");
					return;
				}
				this.ViewState["Caption"] = value;
			}
		}

		/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This property is provided to make the control more accessible to users of Assistive Technology devices.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. The default value is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. </exception>
		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x0007CF48 File Offset: 0x0007B148
		// (set) Token: 0x06002F4B RID: 12107 RVA: 0x00047A54 File Offset: 0x00045C54
		[DefaultValue(TableCaptionAlign.NotSet)]
		[WebCategory("Accessibility")]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				object obj = this.ViewState["CaptionAlign"];
				if (obj != null)
				{
					return (TableCaptionAlign)obj;
				}
				return TableCaptionAlign.NotSet;
			}
			set
			{
				if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid TableCaptionAlign value."));
				}
				this.ViewState["CaptionAlign"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border. </summary>
		/// <returns>The amount of space, in pixels, between the contents of a cell and the cell's border. The default value is -1, which indicates that the property has not been set.</returns>
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x0007CF71 File Offset: 0x0007B171
		// (set) Token: 0x06002F4D RID: 12109 RVA: 0x0007CF88 File Offset: 0x0007B188
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return this.TableStyle.CellPadding;
			}
			set
			{
				this.TableStyle.CellPadding = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells. </summary>
		/// <returns>The amount of space, in pixels, between cells. The default value is -1, which indicates that the property has not been set.</returns>
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x0007CF96 File Offset: 0x0007B196
		// (set) Token: 0x06002F4F RID: 12111 RVA: 0x0007CFAD File Offset: 0x0007B1AD
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(-1)]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return this.TableStyle.CellSpacing;
			}
			set
			{
				this.TableStyle.CellSpacing = value;
			}
		}

		/// <summary>Gets or sets the grid line style to display in the <see cref="T:System.Web.UI.WebControls.Table" /> control. </summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. The default value is None.</returns>
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x0007CFBB File Offset: 0x0007B1BB
		// (set) Token: 0x06002F51 RID: 12113 RVA: 0x0007CFD2 File Offset: 0x0007B1D2
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(GridLines.None)]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.None;
				}
				return this.TableStyle.GridLines;
			}
			set
			{
				this.TableStyle.GridLines = value;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the <see cref="T:System.Web.UI.WebControls.Table" /> control on the page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default value is NotSet.</returns>
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x0007CFE0 File Offset: 0x0007B1E0
		// (set) Token: 0x06002F53 RID: 12115 RVA: 0x0007CFF7 File Offset: 0x0007B1F7
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return this.TableStyle.HorizontalAlign;
			}
			set
			{
				this.TableStyle.HorizontalAlign = value;
			}
		}

		/// <summary>Gets the collection of rows in the <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> that contains the <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in the <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x0007D005 File Offset: 0x0007B205
		[WebSysDescription("")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual TableRowCollection Rows
		{
			get
			{
				if (this.rows == null)
				{
					this.rows = new TableRowCollection(this);
				}
				return this.rows;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06002F55 RID: 12117 RVA: 0x0007D021 File Offset: 0x0007B221
		private TableStyle TableStyle
		{
			get
			{
				return base.ControlStyle as TableStyle;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x06002F57 RID: 12119 RVA: 0x00067521 File Offset: 0x00065721
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the <see cref="T:System.Web.UI.WebControls.TableRow" /> controls of the current <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the <see cref="T:System.Web.UI.WebControls.TableRow" /> controls of the current <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
		// Token: 0x06002F58 RID: 12120 RVA: 0x0007D02E File Offset: 0x0007B22E
		protected override ControlCollection CreateControlCollection()
		{
			return new Table.RowControlCollection(this);
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that contains the properties that define the appearance of the <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
		// Token: 0x06002F59 RID: 12121 RVA: 0x0004F3FE File Offset: 0x0004D5FE
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		/// <summary>Renders the rows in the table control to the specified writer.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		/// <exception cref="T:System.Web.HttpException">The table sections are not in order.</exception>
		// Token: 0x06002F5A RID: 12122 RVA: 0x0007D038 File Offset: 0x0007B238
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			TableRowSection tableRowSection = TableRowSection.TableHeader;
			bool flag = false;
			if (this.Rows.Count > 0)
			{
				foreach (object obj in this.Rows)
				{
					TableRow tableRow = (TableRow)obj;
					if (this.generateTableSections)
					{
						TableRowSection tableSection = tableRow.TableSection;
						if (tableSection < tableRowSection)
						{
							throw new HttpException("The table " + this.ID + " must contain row sections in order of header, body, then footer.");
						}
						if (tableRowSection != tableSection)
						{
							if (flag)
							{
								writer.RenderEndTag();
								flag = false;
							}
							tableRowSection = tableSection;
						}
						if (!flag)
						{
							switch (tableSection)
							{
							case TableRowSection.TableHeader:
								writer.RenderBeginTag(HtmlTextWriterTag.Thead);
								break;
							case TableRowSection.TableBody:
								writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
								break;
							case TableRowSection.TableFooter:
								writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);
								break;
							}
							flag = true;
						}
					}
					if (tableRow != null)
					{
						tableRow.RenderControl(writer);
					}
				}
				if (flag)
				{
					writer.RenderEndTag();
				}
			}
		}

		/// <summary>Renders the HTML opening tag of the <see cref="T:System.Web.UI.WebControls.Table" /> control to the specified writer. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002F5B RID: 12123 RVA: 0x0007D134 File Offset: 0x0007B334
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			string caption = this.Caption;
			if (caption.Length > 0)
			{
				TableCaptionAlign captionAlign = this.CaptionAlign;
				if (captionAlign != TableCaptionAlign.NotSet)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Align, captionAlign.ToString());
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Caption);
				writer.Write(caption);
				writer.RenderEndTag();
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06002F5C RID: 12124 RVA: 0x0007D18B File Offset: 0x0007B38B
		void IPostBackEventHandler.RaisePostBackEvent(string argument)
		{
			this.RaisePostBackEvent(argument);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.Table" /> control when a form is posted back to the server.</summary>
		/// <param name="argument">A <see cref="T:System.String" /> that represents the argument for the event. </param>
		// Token: 0x06002F5D RID: 12125 RVA: 0x0007D194 File Offset: 0x0007B394
		protected virtual void RaisePostBackEvent(string argument)
		{
			base.ValidateEvent(this.UniqueID, argument);
		}

		// Token: 0x04001BE8 RID: 7144
		private TableRowCollection rows;

		// Token: 0x04001BE9 RID: 7145
		private bool generateTableSections;

		/// <summary>Represents the collection of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		// Token: 0x02000419 RID: 1049
		protected class RowControlCollection : ControlCollection
		{
			// Token: 0x06002F5E RID: 12126 RVA: 0x0002B24E File Offset: 0x0002944E
			internal RowControlCollection(Table owner)
				: base(owner)
			{
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" /> collection.</summary>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" />. </param>
			/// <exception cref="T:System.ArgumentException">The object specified by <paramref name="child" /> is not a <see cref="T:System.Web.UI.WebControls.TableRow" />. </exception>
			// Token: 0x06002F5F RID: 12127 RVA: 0x0007D1A3 File Offset: 0x0007B3A3
			public override void Add(Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is TableRow))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an TableRow instance."));
				}
				base.Add(child);
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" /> collection. The new control is added to the array at the specified index location.</summary>
			/// <param name="index">The location in the array at which to add the child control. </param>
			/// <param name="child">The Control object to add to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" />. </param>
			/// <exception cref="T:System.Web.HttpException">The control does not allow child controls. </exception>
			/// <exception cref="T:System.ArgumentException">The child value is null. -or- The object is not a <see cref="T:System.Web.UI.WebControls.TableRow" />.</exception>
			// Token: 0x06002F60 RID: 12128 RVA: 0x0007D1D7 File Offset: 0x0007B3D7
			public override void AddAt(int index, Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is TableRow))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an TableRow instance."));
				}
				base.AddAt(index, child);
			}
		}
	}
}
