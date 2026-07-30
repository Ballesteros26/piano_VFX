using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Represents HTML elements, text, and any other strings in an ASP.NET page that do not require processing on the server.</summary>
	// Token: 0x020001E5 RID: 485
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LiteralControl : Control, ITextControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LiteralControl" /> class that contains a literal string to be rendered on the requested ASP.NET page.</summary>
		// Token: 0x060013A0 RID: 5024 RVA: 0x000353BF File Offset: 0x000335BF
		public LiteralControl()
		{
			this.EnableViewState = false;
			base.AutoID = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LiteralControl" /> class with the specified text.</summary>
		/// <param name="text">The text to be rendered on the requested Web page. </param>
		// Token: 0x060013A1 RID: 5025 RVA: 0x000353D5 File Offset: 0x000335D5
		public LiteralControl(string text)
			: this()
		{
			this.Text = text;
		}

		/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.LiteralControl" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the text content of the literal control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x000353E4 File Offset: 0x000335E4
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x000353EC File Offset: 0x000335EC
		public virtual string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Writes the content of the <see cref="T:System.Web.UI.LiteralControl" /> object to the ASP.NET page.</summary>
		/// <param name="output">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders the content of the <see cref="T:System.Web.UI.LiteralControl" /> to the requesting client. </param>
		// Token: 0x060013A4 RID: 5028 RVA: 0x000353FF File Offset: 0x000335FF
		protected internal override void Render(HtmlTextWriter output)
		{
			output.Write(this._text);
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object for the current instance of the <see cref="T:System.Web.UI.LiteralControl" /> class.</summary>
		/// <returns>The <see cref="T:System.Web.UI.EmptyControlCollection" /> for the current control.</returns>
		// Token: 0x060013A5 RID: 5029 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x04001474 RID: 5236
		private string _text;
	}
}
