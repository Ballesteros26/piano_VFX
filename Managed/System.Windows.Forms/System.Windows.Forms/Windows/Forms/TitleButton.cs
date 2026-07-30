using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020001EA RID: 490
	internal class TitleButton
	{
		// Token: 0x06001EE3 RID: 7907 RVA: 0x000745B8 File Offset: 0x000727B8
		public TitleButton(CaptionButton caption, EventHandler clicked)
		{
			this.Caption = caption;
			this.Clicked = clicked;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000745D0 File Offset: 0x000727D0
		public void OnClick()
		{
			if (this.Clicked != null)
			{
				this.Clicked.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x000745F0 File Offset: 0x000727F0
		// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x000745F8 File Offset: 0x000727F8
		public bool Entered
		{
			get
			{
				return this.entered;
			}
			set
			{
				this.entered = value;
			}
		}

		// Token: 0x04001027 RID: 4135
		public Rectangle Rectangle;

		// Token: 0x04001028 RID: 4136
		public ButtonState State;

		// Token: 0x04001029 RID: 4137
		public CaptionButton Caption;

		// Token: 0x0400102A RID: 4138
		private EventHandler Clicked;

		// Token: 0x0400102B RID: 4139
		public bool Visible;

		// Token: 0x0400102C RID: 4140
		private bool entered;
	}
}
