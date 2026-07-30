using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Represents the &lt;td&gt; and &lt;th&gt; HTML elements in an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object.</summary>
	// Token: 0x02000273 RID: 627
	[ConstructorNeedsTag(true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTableCell : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class using default values.</summary>
		// Token: 0x060019D3 RID: 6611 RVA: 0x0004516D File Offset: 0x0004336D
		public HtmlTableCell()
			: base("td")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class, using the specified tag name.</summary>
		/// <param name="tagName">The element name of the tag. </param>
		// Token: 0x060019D4 RID: 6612 RVA: 0x0004517A File Offset: 0x0004337A
		public HtmlTableCell(string tagName)
			: base(tagName)
		{
		}

		/// <summary>Gets or sets the horizontal alignment of the content in the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The horizontal alignment of the content in the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x00045184 File Offset: 0x00043384
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x00042AB8 File Offset: 0x00040CB8
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

		/// <summary>Gets or sets the background color of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The background color of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</returns>
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x000451AC File Offset: 0x000433AC
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x00044E88 File Offset: 0x00043088
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Gets or sets the border color of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The border color of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</returns>
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x000451D4 File Offset: 0x000433D4
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x00044F04 File Offset: 0x00043104
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

		/// <summary>Gets or sets the number of columns occupied by a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The number of columns occupied by the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x000451FC File Offset: 0x000433FC
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x00045225 File Offset: 0x00043425
		[WebCategory("Layout")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public int ColSpan
		{
			get
			{
				string text = base.Attributes["colspan"];
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
					base.Attributes.Remove("colspan");
					return;
				}
				base.Attributes["colspan"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the height (in pixels) of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The height (in pixels) of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x00045258 File Offset: 0x00043458
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x00045280 File Offset: 0x00043480
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
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
					base.Attributes.Remove("align");
					return;
				}
				base.Attributes["height"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class automatically continues on the next line when it reaches the end of the cell.</summary>
		/// <returns>true if the text does not automatically wrap in the cell; otherwise, false. The default value is false.</returns>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x000452A7 File Offset: 0x000434A7
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x000452C3 File Offset: 0x000434C3
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		[WebSysDescription("")]
		public bool NoWrap
		{
			get
			{
				return base.Attributes["nowrap"] == "nowrap";
			}
			set
			{
				if (value)
				{
					base.Attributes["nowrap"] = "nowrap";
					return;
				}
				base.Attributes.Remove("nowrap");
			}
		}

		/// <summary>Gets or sets the number of rows occupied by a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The number of rows occupied by a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x000452F0 File Offset: 0x000434F0
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x00045319 File Offset: 0x00043519
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Layout")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public int RowSpan
		{
			get
			{
				string text = base.Attributes["rowspan"];
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
					base.Attributes.Remove("rowspan");
					return;
				}
				base.Attributes["rowspan"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the vertical alignment for the content of a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The vertical alignment for the content of a cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0004534C File Offset: 0x0004354C
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x00045374 File Offset: 0x00043574
		[WebCategory("Appearance")]
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

		/// <summary>Gets or sets the width (in pixels) of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The width (in pixels) of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0004539C File Offset: 0x0004359C
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x00045078 File Offset: 0x00043278
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
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

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> control's end tag.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x060019E7 RID: 6631 RVA: 0x000453C4 File Offset: 0x000435C4
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteEndTag(this.TagName);
			if (writer.Indent == 0)
			{
				writer.WriteLine();
			}
		}
	}
}
