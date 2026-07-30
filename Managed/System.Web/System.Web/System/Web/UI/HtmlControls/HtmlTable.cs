using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access on the server to the HTML &lt;table&gt; element.</summary>
	// Token: 0x02000271 RID: 625
	[ParseChildren(true, "Rows")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTable : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> class.</summary>
		// Token: 0x060019B7 RID: 6583 RVA: 0x00044E28 File Offset: 0x00043028
		public HtmlTable()
			: base("table")
		{
		}

		/// <summary>Gets or sets the alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control in relation to other elements on the Web page.</summary>
		/// <returns>The alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control in relation to other elements on the Web page. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00044E38 File Offset: 0x00043038
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x00042AB8 File Offset: 0x00040CB8
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public string Align
		{
			get
			{
				string text = base.Attributes["align"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("align");
					return;
				}
				base.Attributes["align"] = value;
			}
		}

		/// <summary>Gets or sets the background color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The background color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x00044E60 File Offset: 0x00043060
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x00044E88 File Offset: 0x00043088
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public string BgColor
		{
			get
			{
				string text = base.Attributes["bgcolor"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("bgcolor");
					return;
				}
				base.Attributes["bgcolor"] = value;
			}
		}

		/// <summary>Gets or sets the width (in pixels) of the border of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The width (in pixels) of the border of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default is -1, which indicates that the border width is not set.</returns>
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x00044EB0 File Offset: 0x000430B0
		// (set) Token: 0x060019BD RID: 6589 RVA: 0x00043862 File Offset: 0x00041A62
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int Border
		{
			get
			{
				string text = base.Attributes["border"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("border");
					return;
				}
				base.Attributes["border"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the border color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The border color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00044EDC File Offset: 0x000430DC
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x00044F04 File Offset: 0x00043104
		[WebSysDescription("")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		public string BorderColor
		{
			get
			{
				string text = base.Attributes["bordercolor"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("bordercolor");
					return;
				}
				base.Attributes["bordercolor"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space (in pixels) between the contents of a cell and the cell's border in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The amount of space (in pixels) between the contents of a cell and the cell's border in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x00044F2C File Offset: 0x0004312C
		// (set) Token: 0x060019C1 RID: 6593 RVA: 0x00044F55 File Offset: 0x00043155
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public int CellPadding
		{
			get
			{
				string text = base.Attributes["cellpadding"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("cellpadding");
					return;
				}
				base.Attributes["cellpadding"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the amount of space (in pixels) between adjacent cells in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The amount of space (in pixels) between adjacent cells in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x00044F88 File Offset: 0x00043188
		// (set) Token: 0x060019C3 RID: 6595 RVA: 0x00044FB1 File Offset: 0x000431B1
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int CellSpacing
		{
			get
			{
				string text = base.Attributes["cellspacing"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("cellspacing");
					return;
				}
				base.Attributes["cellspacing"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the height of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The height of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x00044FE4 File Offset: 0x000431E4
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x0004500C File Offset: 0x0004320C
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public string Height
		{
			get
			{
				string text = base.Attributes["height"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("height");
					return;
				}
				base.Attributes["height"] = value;
			}
		}

		/// <summary>Gets or sets the content between the opening and closing tags of the control, without automatically converting special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00003A01 File Offset: 0x00001C01
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x00003A01 File Offset: 0x00001C01
		public override string InnerHtml
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the content between the opening and closing tags of the control, with automatic conversion of special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00003A01 File Offset: 0x00001C01
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x00003A01 File Offset: 0x00001C01
		public override string InnerText
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection that contains all the rows in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> that contains all the rows in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x00045033 File Offset: 0x00043233
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual HtmlTableRowCollection Rows
		{
			get
			{
				if (this._rows == null)
				{
					this._rows = new HtmlTableRowCollection(this);
				}
				return this._rows;
			}
		}

		/// <summary>Gets or sets the width of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The width of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00045050 File Offset: 0x00043250
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x00045078 File Offset: 0x00043278
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public string Width
		{
			get
			{
				string text = base.Attributes["width"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("width");
					return;
				}
				base.Attributes["width"] = value;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object for the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control's child server controls.</returns>
		// Token: 0x060019CD RID: 6605 RVA: 0x0004509F File Offset: 0x0004329F
		protected override ControlCollection CreateControlCollection()
		{
			return new HtmlTable.HtmlTableRowControlCollection(this);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content. </param>
		// Token: 0x060019CE RID: 6606 RVA: 0x000450A8 File Offset: 0x000432A8
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			if (this.HasControls())
			{
				int num = writer.Indent;
				writer.Indent = num + 1;
				base.RenderChildren(writer);
				num = writer.Indent;
				writer.Indent = num - 1;
				writer.WriteLine();
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control's end tag.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content. </param>
		// Token: 0x060019CF RID: 6607 RVA: 0x000450EA File Offset: 0x000432EA
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteLine();
			writer.WriteEndTag(this.TagName);
			writer.WriteLine();
		}

		// Token: 0x04001647 RID: 5703
		private HtmlTableRowCollection _rows;

		/// <summary>Represents a collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects that are the rows of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. </summary>
		// Token: 0x02000272 RID: 626
		protected class HtmlTableRowControlCollection : ControlCollection
		{
			// Token: 0x060019D0 RID: 6608 RVA: 0x0002B24E File Offset: 0x0002944E
			internal HtmlTableRowControlCollection(HtmlTable owner)
				: base(owner)
			{
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />.</exception>
			// Token: 0x060019D1 RID: 6609 RVA: 0x00045104 File Offset: 0x00043304
			public override void Add(Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is HtmlTableRow))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an HtmlTableRow instance."));
				}
				base.Add(child);
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection. The new control is added to the array at the specified index location.</summary>
			/// <param name="index">The location in the array at which to add the child control. </param>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
			/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />. </exception>
			// Token: 0x060019D2 RID: 6610 RVA: 0x00045138 File Offset: 0x00043338
			public override void AddAt(int index, Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is HtmlTableRow))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an HtmlTableRow instance."));
				}
				base.AddAt(index, child);
			}
		}
	}
}
