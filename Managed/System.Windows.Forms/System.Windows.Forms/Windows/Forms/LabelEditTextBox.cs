using System;

namespace System.Windows.Forms
{
	// Token: 0x020001FE RID: 510
	internal class LabelEditTextBox : FixedSizeTextBox
	{
		// Token: 0x06001F83 RID: 8067 RVA: 0x000761C8 File Offset: 0x000743C8
		public LabelEditTextBox()
			: base(true, true)
		{
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x000761D4 File Offset: 0x000743D4
		// Note: this type is marked as 'beforefieldinit'.
		static LabelEditTextBox()
		{
			LabelEditTextBox.EditingCancelledEvent = new object();
			LabelEditTextBox.EditingFinishedEvent = new object();
		}

		// Token: 0x140001F5 RID: 501
		// (add) Token: 0x06001F85 RID: 8069 RVA: 0x000761EC File Offset: 0x000743EC
		// (remove) Token: 0x06001F86 RID: 8070 RVA: 0x00076200 File Offset: 0x00074400
		public event EventHandler EditingCancelled
		{
			add
			{
				base.Events.AddHandler(LabelEditTextBox.EditingCancelledEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LabelEditTextBox.EditingCancelledEvent, value);
			}
		}

		// Token: 0x140001F6 RID: 502
		// (add) Token: 0x06001F87 RID: 8071 RVA: 0x00076214 File Offset: 0x00074414
		// (remove) Token: 0x06001F88 RID: 8072 RVA: 0x00076228 File Offset: 0x00074428
		public event EventHandler EditingFinished
		{
			add
			{
				base.Events.AddHandler(LabelEditTextBox.EditingFinishedEvent, value);
			}
			remove
			{
				base.Events.AddHandler(LabelEditTextBox.EditingFinishedEvent, value);
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0007623C File Offset: 0x0007443C
		protected override bool IsInputKey(Keys key_data)
		{
			if ((key_data & Keys.Alt) == Keys.None)
			{
				Keys keys = key_data & Keys.KeyCode;
				if (keys == Keys.Return)
				{
					return true;
				}
				if (keys == Keys.Escape)
				{
					return true;
				}
			}
			return base.IsInputKey(key_data);
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00076280 File Offset: 0x00074480
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (!base.Visible)
			{
				return;
			}
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				if (keyCode == Keys.Escape)
				{
					base.Visible = false;
					base.Parent.Focus();
					e.Handled = true;
					this.OnEditingCancelled(e);
				}
			}
			else
			{
				base.Visible = false;
				base.Parent.Focus();
				e.Handled = true;
				this.OnEditingFinished(e);
			}
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00076304 File Offset: 0x00074504
		protected override void OnLostFocus(EventArgs e)
		{
			if (base.Visible)
			{
				this.OnEditingFinished(e);
			}
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00076318 File Offset: 0x00074518
		protected void OnEditingCancelled(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LabelEditTextBox.EditingCancelledEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0007634C File Offset: 0x0007454C
		protected void OnEditingFinished(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LabelEditTextBox.EditingFinishedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}
	}
}
