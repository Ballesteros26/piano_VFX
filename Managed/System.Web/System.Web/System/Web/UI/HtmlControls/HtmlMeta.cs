using System;
using System.ComponentModel;
using System.Web.Configuration;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;meta&gt; tag on the server. </summary>
	// Token: 0x0200026D RID: 621
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlMeta : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> class. </summary>
		// Token: 0x0600196C RID: 6508 RVA: 0x000440AC File Offset: 0x000422AC
		public HtmlMeta()
			: base("meta")
		{
		}

		/// <summary>Gets or sets the metadata property value defined by the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control.</summary>
		/// <returns>The metadata property value. </returns>
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x000440BC File Offset: 0x000422BC
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x000440E4 File Offset: 0x000422E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public virtual string Content
		{
			get
			{
				string text = base.Attributes["content"];
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
					base.Attributes.Remove("content");
					return;
				}
				base.Attributes["content"] = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control property that is included in the HTTP response header.</summary>
		/// <returns>The name of the HTTP response header item. </returns>
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0004410C File Offset: 0x0004230C
		// (set) Token: 0x06001970 RID: 6512 RVA: 0x00044134 File Offset: 0x00042334
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string HttpEquiv
		{
			get
			{
				string text = base.Attributes["http-equiv"];
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
					base.Attributes.Remove("http-equiv");
					return;
				}
				base.Attributes["http-equiv"] = value;
			}
		}

		/// <summary>Gets or sets the metadata property name defined by the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control. </summary>
		/// <returns>The property name. </returns>
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0004415C File Offset: 0x0004235C
		// (set) Token: 0x06001972 RID: 6514 RVA: 0x00043BF0 File Offset: 0x00041DF0
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Name
		{
			get
			{
				string text = base.Attributes["name"];
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
					base.Attributes.Remove("name");
					return;
				}
				base.Attributes["name"] = value;
			}
		}

		/// <summary>Gets or sets a scheme attribute used to interpret the metadata property value defined by the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control.</summary>
		/// <returns>The scheme attribute. </returns>
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00044184 File Offset: 0x00042384
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x000441AC File Offset: 0x000423AC
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Scheme
		{
			get
			{
				string text = base.Attributes["scheme"];
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
					base.Attributes.Remove("scheme");
					return;
				}
				base.Attributes["scheme"] = value;
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> used to render the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control's content on the client's browser.</param>
		// Token: 0x06001975 RID: 6517 RVA: 0x000441D4 File Offset: 0x000423D4
		protected internal override void Render(HtmlTextWriter writer)
		{
			XhtmlConformanceSection xhtmlConformanceSection = WebConfigurationManager.GetSection("system.web/xhtmlConformance") as XhtmlConformanceSection;
			if (xhtmlConformanceSection != null && xhtmlConformanceSection.Mode == XhtmlConformanceMode.Legacy)
			{
				base.Render(writer);
				return;
			}
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write("/>");
		}
	}
}
