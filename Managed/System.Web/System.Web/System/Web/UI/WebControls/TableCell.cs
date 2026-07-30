using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a cell in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	// Token: 0x0200041A RID: 1050
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Bindable(false)]
	[ParseChildren(false)]
	[ToolboxItem("")]
	[DefaultProperty("Text")]
	[ControlBuilder(typeof(TableCellControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableCell : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableCell" /> class.</summary>
		// Token: 0x06002F61 RID: 12129 RVA: 0x0007D20C File Offset: 0x0007B40C
		public TableCell()
			: base(HtmlTextWriterTag.Td)
		{
			base.AutoID = false;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0007D21D File Offset: 0x0007B41D
		internal TableCell(HtmlTextWriterTag tag)
			: base(tag)
		{
			base.AutoID = false;
		}

		/// <summary>Gets or sets a space-separated list of table header cells associated with the <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
		/// <returns>An array of strings containing the identifiers of the associated table header cells.</returns>
		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06002F63 RID: 12131 RVA: 0x0007D230 File Offset: 0x0007B430
		// (set) Token: 0x06002F64 RID: 12132 RVA: 0x0007D25E File Offset: 0x0007B45E
		[TypeConverter(typeof(StringArrayConverter))]
		[DefaultValue(null)]
		public virtual string[] AssociatedHeaderCellID
		{
			get
			{
				object obj = this.ViewState["AssociatedHeaderCellID"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("AssociatedHeaderCellID");
					return;
				}
				this.ViewState["AssociatedHeaderCellID"] = value;
			}
		}

		/// <summary>Gets or sets the number of columns in the <see cref="T:System.Web.UI.WebControls.Table" /> control that the cell spans.</summary>
		/// <returns>The number of columns in the rendered table that the cell spans. The default value is 0, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 0.</exception>
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x0007D288 File Offset: 0x0007B488
		// (set) Token: 0x06002F66 RID: 12134 RVA: 0x0007D2B1 File Offset: 0x0007B4B1
		[DefaultValue(0)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual int ColumnSpan
		{
			get
			{
				object obj = this.ViewState["ColumnSpan"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("< 0");
				}
				this.ViewState["ColumnSpan"] = value;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the contents in the cell.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default is NotSet.</returns>
		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x0007D2D8 File Offset: 0x0007B4D8
		// (set) Token: 0x06002F68 RID: 12136 RVA: 0x0007D2EF File Offset: 0x0007B4EF
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
				return this.TableItemStyle.HorizontalAlign;
			}
			set
			{
				this.TableItemStyle.HorizontalAlign = value;
			}
		}

		/// <summary>Gets or sets the number of rows in the <see cref="T:System.Web.UI.WebControls.Table" /> control that the cell spans.</summary>
		/// <returns>The number of rows in the rendered table that the cell spans. The default value is 0, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 0.</exception>
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x0007D300 File Offset: 0x0007B500
		// (set) Token: 0x06002F6A RID: 12138 RVA: 0x0007D329 File Offset: 0x0007B529
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(0)]
		public virtual int RowSpan
		{
			get
			{
				object obj = this.ViewState["RowSpan"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("< 0");
				}
				this.ViewState["RowSpan"] = value;
			}
		}

		/// <summary>Gets or sets the text contents of the cell.</summary>
		/// <returns>The text contents of the cell. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06002F6B RID: 12139 RVA: 0x0007D350 File Offset: 0x0007B550
		// (set) Token: 0x06002F6C RID: 12140 RVA: 0x0007D37D File Offset: 0x0007B57D
		[WebCategory("Appearance")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[WebSysDescription("")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
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
					this.ViewState.Remove("Text");
					return;
				}
				this.ViewState["Text"] = value;
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
			}
		}

		/// <summary>Gets or sets the vertical alignment of the contents in the cell.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> enumeration values. The default is NotSet.</returns>
		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06002F6D RID: 12141 RVA: 0x0007D3B7 File Offset: 0x0007B5B7
		// (set) Token: 0x06002F6E RID: 12142 RVA: 0x0007D3CE File Offset: 0x0007B5CE
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

		/// <summary>Gets or sets a value that indicating whether the contents of the cell wrap.</summary>
		/// <returns>true if the contents of the cell wrap in the cell; otherwise, false. The default is true.</returns>
		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x0007D3DC File Offset: 0x0007B5DC
		// (set) Token: 0x06002F70 RID: 12144 RVA: 0x0007D3F3 File Offset: 0x0007B5F3
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual bool Wrap
		{
			get
			{
				return !base.ControlStyleCreated || this.TableItemStyle.Wrap;
			}
			set
			{
				this.TableItemStyle.Wrap = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x0007D401 File Offset: 0x0007B601
		private TableItemStyle TableItemStyle
		{
			get
			{
				return base.ControlStyle as TableItemStyle;
			}
		}

		/// <summary>Adds properties specific to the <see cref="T:System.Web.UI.WebControls.TableCell" /> control to the list of attributes to render.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		/// <exception cref="T:System.Web.HttpException">A cell listed as an associated header cell was not found.</exception>
		// Token: 0x06002F73 RID: 12147 RVA: 0x0007D410 File Offset: 0x0007B610
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (writer == null)
			{
				return;
			}
			int i = this.ColumnSpan;
			if (i > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Colspan, i.ToString(Helpers.InvariantCulture), false);
			}
			i = this.RowSpan;
			if (i > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, i.ToString(Helpers.InvariantCulture), false);
			}
			string[] associatedHeaderCellID = this.AssociatedHeaderCellID;
			if (associatedHeaderCellID.Length > 1)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (i = 0; i < associatedHeaderCellID.Length - 1; i++)
				{
					stringBuilder.Append(associatedHeaderCellID[i]);
					stringBuilder.Append(",");
				}
				stringBuilder.Append(associatedHeaderCellID.Length - 1);
				writer.AddAttribute(HtmlTextWriterAttribute.Headers, stringBuilder.ToString());
				return;
			}
			if (associatedHeaderCellID.Length == 1)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Headers, associatedHeaderCellID[0]);
			}
		}

		/// <summary>Adds a parsed child control to the <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element</param>
		// Token: 0x06002F74 RID: 12148 RVA: 0x0007D4D0 File Offset: 0x0007B6D0
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl == null)
			{
				string text = this.Text;
				if (text.Length > 0)
				{
					this.Controls.Add(new LiteralControl(text));
					this.Text = null;
				}
				base.AddParsedSubObject(obj);
				return;
			}
			this.Text = literalControl.Text;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> for the <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> for the <see cref="T:System.Web.UI.WebControls.TableCell" /> control. </returns>
		// Token: 0x06002F75 RID: 12149 RVA: 0x00059A1E File Offset: 0x00057C1E
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle(this.ViewState);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.TableCell" /> contents to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x06002F76 RID: 12150 RVA: 0x0007D533 File Offset: 0x0007B733
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}
	}
}
