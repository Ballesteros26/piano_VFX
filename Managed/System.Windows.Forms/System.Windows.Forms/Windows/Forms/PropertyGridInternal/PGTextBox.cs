using System;
using System.Drawing;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020002A5 RID: 677
	internal class PGTextBox : TextBox
	{
		// Token: 0x06002D4B RID: 11595 RVA: 0x000AE49C File Offset: 0x000AC69C
		public void FocusAt(Point location)
		{
			this._focusing = true;
			Point point = base.PointToClient(location);
			XplatUI.SendMessage(this.Handle, Msg.WM_LBUTTONDOWN, new IntPtr(1), Control.MakeParam(point.X, point.Y));
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000AE4E4 File Offset: 0x000AC6E4
		protected override bool IsInputKey(Keys keyData)
		{
			return ((keyData & Keys.Alt) != Keys.None && (keyData & Keys.KeyCode) == Keys.Down) || base.IsInputKey(keyData);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000AE50C File Offset: 0x000AC70C
		protected override void WndProc(ref Message m)
		{
			if (this._focusing && m.Msg == 512)
			{
				this._focusing = false;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x040015DE RID: 5598
		private bool _focusing;
	}
}
