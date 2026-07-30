using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Provides programmatic access for the HTML &lt;img&gt; element on the server.</summary>
	// Token: 0x02000260 RID: 608
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlImage : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlImage" /> class.</summary>
		// Token: 0x060018BC RID: 6332 RVA: 0x00042A81 File Offset: 0x00040C81
		public HtmlImage()
			: base("img")
		{
		}

		/// <summary>Gets or sets the alignment of the image relative to other Web page elements.</summary>
		/// <returns>A string that specifies the alignment of the image relative to other Web page elements.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x00042A90 File Offset: 0x00040C90
		// (set) Token: 0x060018BE RID: 6334 RVA: 0x00042AB8 File Offset: 0x00040CB8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Layout")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public string Align
		{
			get
			{
				string text = base.Attributes["align"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
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

		/// <summary>Gets or sets the alternative caption the browser displays if an image is unavailable or currently downloading and not yet finished.</summary>
		/// <returns>A string that contains the alternative caption for the browser to use when the image is unavailable.</returns>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00042AE0 File Offset: 0x00040CE0
		// (set) Token: 0x060018C0 RID: 6336 RVA: 0x00042B08 File Offset: 0x00040D08
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		public string Alt
		{
			get
			{
				string text = base.Attributes["alt"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("alt");
					return;
				}
				base.Attributes["alt"] = value;
			}
		}

		/// <summary>Gets or sets the width of a frame for an image.</summary>
		/// <returns>The width (in pixels) of a frame for an image.</returns>
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00042B30 File Offset: 0x00040D30
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x00042B5E File Offset: 0x00040D5E
		[DefaultValue(0)]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public int Border
		{
			get
			{
				string text = base.Attributes["border"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, Helpers.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("border");
					return;
				}
				base.Attributes["border"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the height of the image.</summary>
		/// <returns>The height of the image.</returns>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00042B8C File Offset: 0x00040D8C
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x00042BBA File Offset: 0x00040DBA
		[DefaultValue(100)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				string text = base.Attributes["height"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, Helpers.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("height");
					return;
				}
				base.Attributes["height"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the source of the image file to display.</summary>
		/// <returns>A string that contains the path to an image file to display.</returns>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x00042BE8 File Offset: 0x00040DE8
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x00042C10 File Offset: 0x00040E10
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		[DefaultValue("")]
		public string Src
		{
			get
			{
				string text = base.Attributes["src"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("src");
					return;
				}
				base.Attributes["src"] = value;
			}
		}

		/// <summary>Gets or sets the width of the image.</summary>
		/// <returns>The width of the image.</returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00042C38 File Offset: 0x00040E38
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x00042C66 File Offset: 0x00040E66
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(100)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public int Width
		{
			get
			{
				string text = base.Attributes["width"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, Helpers.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("width");
					return;
				}
				base.Attributes["width"] = value.ToString();
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlImage" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlImage.Src" /> property contains a malformed URL.</exception>
		// Token: 0x060018C9 RID: 6345 RVA: 0x00042C94 File Offset: 0x00040E94
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReference(writer, "src");
			string text = base.Attributes["src"];
			if (text == null || text.Length == 0)
			{
				base.Attributes.Remove("src");
			}
			base.RenderAttributes(writer);
			writer.Write(" /");
		}
	}
}
