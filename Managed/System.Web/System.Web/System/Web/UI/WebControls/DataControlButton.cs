using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036D RID: 877
	[SupportsEventValidation]
	internal class DataControlButton : Button, IDataControlButton, IButtonControl
	{
		// Token: 0x060020FF RID: 8447 RVA: 0x000549C8 File Offset: 0x00052BC8
		public static IDataControlButton CreateButton(ButtonType type, Control container, string text, string image, string command, string commandArg, bool allowCallback)
		{
			IDataControlButton dataControlButton;
			if (type != ButtonType.Image)
			{
				if (type == ButtonType.Link)
				{
					dataControlButton = new DataControlLinkButton();
				}
				else
				{
					dataControlButton = new DataControlButton();
				}
			}
			else
			{
				dataControlButton = new DataControlImageButton();
				dataControlButton.ImageUrl = image;
			}
			dataControlButton.Container = container;
			dataControlButton.CommandName = command;
			dataControlButton.CommandArgument = commandArg;
			dataControlButton.Text = text;
			dataControlButton.CausesValidation = false;
			dataControlButton.AllowCallback = allowCallback;
			return dataControlButton;
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x00054A28 File Offset: 0x00052C28
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x00054A30 File Offset: 0x00052C30
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

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x0000EE9B File Offset: 0x0000D09B
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x0000393A File Offset: 0x00001B3A
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

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x00054A39 File Offset: 0x00052C39
		// (set) Token: 0x06002105 RID: 8453 RVA: 0x00054A4C File Offset: 0x00052C4C
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

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x00008A69 File Offset: 0x00006C69
		public ButtonType ButtonType
		{
			get
			{
				return ButtonType.Button;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x06002108 RID: 8456 RVA: 0x00003A01 File Offset: 0x00001C01
		public override bool UseSubmitBehavior
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x00054A64 File Offset: 0x00052C64
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

		// Token: 0x0600210A RID: 8458 RVA: 0x00054AAC File Offset: 0x00052CAC
		protected override PostBackOptions GetPostBackOptions()
		{
			IPostBackContainer postBackContainer = this.Container as IPostBackContainer;
			if (postBackContainer != null)
			{
				return postBackContainer.GetPostBackOptions(this);
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x040018B1 RID: 6321
		private Control _container;
	}
}
