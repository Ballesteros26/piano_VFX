using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Represents the &lt;tr&gt; HTML element in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	// Token: 0x02000275 RID: 629
	[ParseChildren(true, "Cells")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTableRow : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
		// Token: 0x060019F6 RID: 6646 RVA: 0x00045476 File Offset: 0x00043676
		public HtmlTableRow()
			: base("tr")
		{
		}

		/// <summary>Gets or sets the horizontal alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The horizontal alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x00045484 File Offset: 0x00043684
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x00042AB8 File Offset: 0x00040CB8
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the background color of the row represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
		/// <returns>The background color of the row represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />.</returns>
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000454AC File Offset: 0x000436AC
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x00044E88 File Offset: 0x00043088
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the border color of the row represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
		/// <returns>The border color of the row represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />.</returns>
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x000454D4 File Offset: 0x000436D4
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x00044F04 File Offset: 0x00043104
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
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

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects that represent the cells contained in a row of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> that contains the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x000454FC File Offset: 0x000436FC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual HtmlTableCellCollection Cells
		{
			get
			{
				if (this._cells == null)
				{
					this._cells = new HtmlTableCellCollection(this);
				}
				return this._cells;
			}
		}

		/// <summary>Gets or sets the height (in pixels) of the row represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
		/// <returns>The height (in pixels) of the row represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00045518 File Offset: 0x00043718
		// (set) Token: 0x060019FF RID: 6655 RVA: 0x0004500C File Offset: 0x0004320C
		[DefaultValue("")]
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Gets or sets the content between the opening and closing tags of the control without automatically converting special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x00003A01 File Offset: 0x00001C01
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x00003A01 File Offset: 0x00001C01
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

		/// <summary>Gets or sets the content between the opening and closing tags of the control with automatic conversion of special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00003A01 File Offset: 0x00001C01
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x00003A01 File Offset: 0x00001C01
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

		/// <summary>Gets or sets the vertical alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The vertical alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00045540 File Offset: 0x00043740
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x00045374 File Offset: 0x00043574
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public string VAlign
		{
			get
			{
				string text = base.Attributes["valign"];
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
					base.Attributes.Remove("valign");
					return;
				}
				base.Attributes["valign"] = value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x00045568 File Offset: 0x00043768
		private int Count
		{
			get
			{
				if (this._cells != null)
				{
					return this._cells.Count;
				}
				return 0;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object for the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control's child server controls.</returns>
		// Token: 0x06001A07 RID: 6663 RVA: 0x0004557F File Offset: 0x0004377F
		protected override ControlCollection CreateControlCollection()
		{
			return new HtmlTableRow.HtmlTableCellControlCollection(this);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the rendered content.</param>
		// Token: 0x06001A08 RID: 6664 RVA: 0x00045588 File Offset: 0x00043788
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

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control's end tag.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the rendered content.</param>
		// Token: 0x06001A09 RID: 6665 RVA: 0x000455CA File Offset: 0x000437CA
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this.Count == 0)
			{
				writer.WriteLine();
			}
			writer.WriteEndTag(this.TagName);
			if (writer.Indent == 0)
			{
				writer.WriteLine();
			}
		}

		// Token: 0x04001649 RID: 5705
		private HtmlTableCellCollection _cells;

		/// <summary>Represents a collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects that are the cells of an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control. </summary>
		// Token: 0x02000276 RID: 630
		protected class HtmlTableCellControlCollection : ControlCollection
		{
			// Token: 0x06001A0A RID: 6666 RVA: 0x0002B24E File Offset: 0x0002944E
			internal HtmlTableCellControlCollection(HtmlTableRow owner)
				: base(owner)
			{
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</exception>
			// Token: 0x06001A0B RID: 6667 RVA: 0x000455F4 File Offset: 0x000437F4
			public override void Add(Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is HtmlTableCell))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an HtmlTableCell instance."));
				}
				base.Add(child);
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection at the specified index location.</summary>
			/// <param name="index">The location in the array at which to add the child control. </param>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
			/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</exception>
			// Token: 0x06001A0C RID: 6668 RVA: 0x00045628 File Offset: 0x00043828
			public override void AddAt(int index, Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is HtmlTableCell))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an HtmlTableCell instance."));
				}
				base.AddAt(index, child);
			}
		}
	}
}
