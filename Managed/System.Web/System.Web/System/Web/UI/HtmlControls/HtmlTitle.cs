using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;title&gt; element on the server.</summary>
	// Token: 0x02000279 RID: 633
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTitle : HtmlControl
	{
		/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> control that an XML or HTML element was parsed and adds that element to the <see cref="T:System.Web.UI.ControlCollection" /> collection of the control.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
		// Token: 0x06001A30 RID: 6704 RVA: 0x00045948 File Offset: 0x00043B48
		protected override void AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl != null)
			{
				this.text = literalControl.Text;
				return;
			}
			base.AddParsedSubObject(obj);
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> collection for the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
		// Token: 0x06001A31 RID: 6705 RVA: 0x0002F1E0 File Offset: 0x0002D3E0
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		/// <summary>Gets or sets the text of the HTML &lt;title&gt; element.</summary>
		/// <returns>The text of the HTML &lt;title&gt; element. The default value is an empty string ("").</returns>
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x00045973 File Offset: 0x00043B73
		// (set) Token: 0x06001A33 RID: 6707 RVA: 0x0004597B File Offset: 0x00043B7B
		[Localizable(true)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x06001A34 RID: 6708 RVA: 0x00045984 File Offset: 0x00043B84
		protected internal override void Render(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Title);
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				this.RenderChildren(writer);
			}
			else
			{
				writer.Write(this.text);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0400164C RID: 5708
		private string text;
	}
}
