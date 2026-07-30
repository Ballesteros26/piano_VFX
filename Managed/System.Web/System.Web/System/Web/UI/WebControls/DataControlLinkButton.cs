using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036E RID: 878
	internal class DataControlLinkButton : LinkButton, IDataControlButton, IButtonControl
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x00054ADE File Offset: 0x00052CDE
		// (set) Token: 0x0600210D RID: 8461 RVA: 0x00054AE6 File Offset: 0x00052CE6
		public Control Container
		{
			get
			{
				return this._container;
			}
			set
			{
				this._container = value;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x0000EE9B File Offset: 0x0000D09B
		// (set) Token: 0x0600210F RID: 8463 RVA: 0x0000393A File Offset: 0x00001B3A
		public string ImageUrl
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x00054A39 File Offset: 0x00052C39
		// (set) Token: 0x06002111 RID: 8465 RVA: 0x00054A4C File Offset: 0x00052C4C
		public bool AllowCallback
		{
			get
			{
				return this.ViewState.GetBool("AllowCallback", true);
			}
			set
			{
				this.ViewState["AllowCallback"] = value;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x000363BE File Offset: 0x000345BE
		public ButtonType ButtonType
		{
			get
			{
				return ButtonType.Link;
			}
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x00054AF0 File Offset: 0x00052CF0
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.EnsureForeColor();
			if (this.AllowCallback)
			{
				ICallbackContainer callbackContainer = this.Container as ICallbackContainer;
				if (callbackContainer != null)
				{
					this.OnClientClick = ClientScriptManager.EnsureEndsWithSemicolon(this.OnClientClick) + callbackContainer.GetCallbackScript(this, base.CommandName + "$" + base.CommandArgument);
				}
			}
			base.Render(writer);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00054B54 File Offset: 0x00052D54
		private void EnsureForeColor()
		{
			if (this.ForeColor != Color.Empty)
			{
				return;
			}
			for (Control control = this.Parent; control != null; control = control.Parent)
			{
				WebControl webControl = control as WebControl;
				if (webControl != null && webControl.ForeColor != Color.Empty)
				{
					this.ForeColor = webControl.ForeColor;
					return;
				}
				if (control == this.Container)
				{
					break;
				}
			}
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x00054BBC File Offset: 0x00052DBC
		protected override PostBackOptions GetPostBackOptions()
		{
			IPostBackContainer postBackContainer = this.Container as IPostBackContainer;
			if (postBackContainer != null)
			{
				return postBackContainer.GetPostBackOptions(this);
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x040018B2 RID: 6322
		private Control _container;
	}
}
