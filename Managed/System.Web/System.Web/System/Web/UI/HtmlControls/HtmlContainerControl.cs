using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Serves as the abstract base class for HTML server controls that map to HTML elements that are required to have an opening and a closing tag.</summary>
	// Token: 0x02000256 RID: 598
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HtmlContainerControl : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> class using default values.</summary>
		// Token: 0x06001867 RID: 6247 RVA: 0x00041D59 File Offset: 0x0003FF59
		protected HtmlContainerControl()
			: this("span")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> class using the specified tag name.</summary>
		/// <param name="tag">A string that specifies the tag name of the control. </param>
		// Token: 0x06001868 RID: 6248 RVA: 0x00041D66 File Offset: 0x0003FF66
		public HtmlContainerControl(string tag)
			: base(tag)
		{
		}

		/// <summary>Gets or sets the content found between the opening and closing tags of the specified HTML server control.</summary>
		/// <returns>The HTML content between opening and closing tags of an HTML server control.</returns>
		/// <exception cref="T:System.Web.HttpException">There is more than one HTML server control.- or -The HTML server control is not a <see cref="T:System.Web.UI.LiteralControl" /> or a <see cref="T:System.Web.UI.DataBoundLiteralControl" />. </exception>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x00041D70 File Offset: 0x0003FF70
		// (set) Token: 0x0600186A RID: 6250 RVA: 0x00041DD8 File Offset: 0x0003FFD8
		[HtmlControlPersistable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string InnerHtml
		{
			get
			{
				if (this.Controls.Count == 0)
				{
					return string.Empty;
				}
				if (this.Controls.Count == 1)
				{
					Control control = this.Controls[0];
					LiteralControl literalControl = control as LiteralControl;
					if (literalControl != null)
					{
						return literalControl.Text;
					}
					DataBoundLiteralControl dataBoundLiteralControl = control as DataBoundLiteralControl;
					if (dataBoundLiteralControl != null)
					{
						return dataBoundLiteralControl.Text;
					}
				}
				throw new HttpException("There is no literal content!");
			}
			set
			{
				this.Controls.Clear();
				this.Controls.Add(new LiteralControl(value));
				if (value == null)
				{
					this.ViewState.Remove("innerhtml");
					return;
				}
				this.ViewState["innerhtml"] = value;
			}
		}

		/// <summary>Gets or sets the text between the opening and closing tags of the specified HTML server control.</summary>
		/// <returns>The text between the opening and closing tags of an HTML server control.</returns>
		/// <exception cref="T:System.Web.HttpException">There is more than one HTML server control.- or - The HTML server control is not a <see cref="T:System.Web.UI.LiteralControl" /> or a <see cref="T:System.Web.UI.DataBoundLiteralControl" />. </exception>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x00041E26 File Offset: 0x00040026
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x00041E33 File Offset: 0x00040033
		[HtmlControlPersistable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string InnerText
		{
			get
			{
				return HttpUtility.HtmlDecode(this.InnerHtml);
			}
			set
			{
				this.InnerHtml = HttpUtility.HtmlEncode(value);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> content.</param>
		// Token: 0x0600186D RID: 6253 RVA: 0x00041E41 File Offset: 0x00040041
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderChildren(writer);
			this.RenderEndTag(writer);
		}

		/// <summary>Renders the closing tag for the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600186E RID: 6254 RVA: 0x00041E58 File Offset: 0x00040058
		protected virtual void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteEndTag(this.TagName);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that receives the rendered content.</param>
		// Token: 0x0600186F RID: 6255 RVA: 0x00041E66 File Offset: 0x00040066
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			this.ViewState.Remove("innerhtml");
			base.RenderAttributes(writer);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0002F1E0 File Offset: 0x0002D3E0
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		/// <summary>Restores the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control's view state from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
		// Token: 0x06001871 RID: 6257 RVA: 0x00041E80 File Offset: 0x00040080
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				base.LoadViewState(savedState);
				string text = this.ViewState["innerhtml"] as string;
				if (text != null)
				{
					this.InnerHtml = text;
				}
			}
		}
	}
}
