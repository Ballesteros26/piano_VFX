using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Represents the design-time version of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> control. This class cannot be inherited.</summary>
	// Token: 0x020001CB RID: 459
	[ToolboxItem(false)]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DesignerDataBoundLiteralControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DesignerDataBoundLiteralControl" /> class.</summary>
		// Token: 0x060012B8 RID: 4792 RVA: 0x000330FD File Offset: 0x000312FD
		public DesignerDataBoundLiteralControl()
		{
			base.AutoID = false;
		}

		/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the text in the &lt;%# … %&gt; data-binding expression.</returns>
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00033117 File Offset: 0x00031317
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x0003311F File Offset: 0x0003131F
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					this.text = string.Empty;
					return;
				}
				this.text = value;
			}
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00033137 File Offset: 0x00031337
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.text = (string)savedState;
			}
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00033148 File Offset: 0x00031348
		protected internal override void Render(HtmlTextWriter output)
		{
			output.Write(this.text);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00033117 File Offset: 0x00031317
		protected override object SaveViewState()
		{
			return this.text;
		}

		// Token: 0x04001430 RID: 5168
		private string text = string.Empty;
	}
}
