using System;

namespace System.Windows.Forms
{
	// Token: 0x02000277 RID: 631
	internal class NumericTextBox : TextBox
	{
		// Token: 0x06002923 RID: 10531 RVA: 0x0009F164 File Offset: 0x0009D364
		// Note: this type is marked as 'beforefieldinit'.
		static NumericTextBox()
		{
			NumericTextBox.ValueChangedEvent = new object();
		}

		// Token: 0x14000264 RID: 612
		// (add) Token: 0x06002924 RID: 10532 RVA: 0x0009F170 File Offset: 0x0009D370
		// (remove) Token: 0x06002925 RID: 10533 RVA: 0x0009F184 File Offset: 0x0009D384
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(NumericTextBox.ValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NumericTextBox.ValueChangedEvent, value);
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x0009F198 File Offset: 0x0009D398
		// (set) Token: 0x06002927 RID: 10535 RVA: 0x0009F1A0 File Offset: 0x0009D3A0
		public double Value
		{
			get
			{
				return this.val;
			}
			set
			{
				if (value == this.val)
				{
					return;
				}
				if (value < this.min)
				{
					value = this.min;
				}
				this.val = value;
				this.OnValueChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x0009F1D8 File Offset: 0x0009D3D8
		// (set) Token: 0x06002929 RID: 10537 RVA: 0x0009F1E0 File Offset: 0x0009D3E0
		public double Min
		{
			get
			{
				return this.min;
			}
			set
			{
				this.min = value;
			}
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x0009F1EC File Offset: 0x0009D3EC
		protected override void OnLostFocus(EventArgs args)
		{
			string text = this.Value.ToString();
			if (this.Text != text)
			{
				this.Text = text;
			}
			base.OnLostFocus(args);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x0009F228 File Offset: 0x0009D428
		protected override void OnTextChanged(EventArgs args)
		{
			try
			{
				string text = ((this.Text.Length != 0) ? this.Text : "0");
				double num = double.Parse(text);
				this.Value = num;
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			base.OnTextChanged(args);
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x0009F2B0 File Offset: 0x0009D4B0
		protected override void OnKeyPress(KeyPressEventArgs args)
		{
			string text = "\b.01234567890";
			if (text.IndexOf(args.KeyChar) < 0)
			{
				args.Handled = true;
			}
			base.OnKeyPress(args);
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x0009F2E4 File Offset: 0x0009D4E4
		protected virtual void OnValueChanged(EventArgs args)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NumericTextBox.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, args);
			}
		}

		// Token: 0x04001488 RID: 5256
		private double val;

		// Token: 0x04001489 RID: 5257
		private double min;
	}
}
