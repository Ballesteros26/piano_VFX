using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036F RID: 879
	internal class DataControlImageButton : ImageButton, IDataControlButton, IButtonControl
	{
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x00054BEE File Offset: 0x00052DEE
		// (set) Token: 0x06002118 RID: 8472 RVA: 0x00054BF6 File Offset: 0x00052DF6
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

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x00054A39 File Offset: 0x00052C39
		// (set) Token: 0x0600211A RID: 8474 RVA: 0x00054A4C File Offset: 0x00052C4C
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

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x00008B66 File Offset: 0x00006D66
		public ButtonType ButtonType
		{
			get
			{
				return ButtonType.Image;
			}
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00054C00 File Offset: 0x00052E00
		internal override string GetClientScriptEventReference()
		{
			if (this.AllowCallback)
			{
				ICallbackContainer callbackContainer = this.Container as ICallbackContainer;
				if (callbackContainer != null)
				{
					return callbackContainer.GetCallbackScript(this, base.CommandName + "$" + base.CommandArgument);
				}
			}
			return base.GetClientScriptEventReference();
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00054C48 File Offset: 0x00052E48
		protected override PostBackOptions GetPostBackOptions()
		{
			IPostBackContainer postBackContainer = this.Container as IPostBackContainer;
			if (postBackContainer != null)
			{
				return postBackContainer.GetPostBackOptions(this);
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x040018B3 RID: 6323
		private Control _container;
	}
}
