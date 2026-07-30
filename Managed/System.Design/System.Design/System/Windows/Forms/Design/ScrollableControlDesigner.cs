using System;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	/// <summary>Base designer class for extending the design mode behavior of a <see cref="T:System.Windows.Forms.Control" /> which should receive scroll messages.</summary>
	// Token: 0x02000034 RID: 52
	public class ScrollableControlDesigner : ParentControlDesigner
	{
		/// <summary>Indicates whether a mouse click at the specified point should be handled by the control.</summary>
		/// <returns>true if a click at the specified point is to be handled by the control; otherwise, false.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> indicating the position at which the mouse was clicked, in screen coordinates. </param>
		// Token: 0x060001CE RID: 462 RVA: 0x0000659C File Offset: 0x0000479C
		protected override bool GetHitTest(Point pt)
		{
			if (base.GetHitTest(pt))
			{
				return true;
			}
			if (this.Control is ScrollableControl && ((ScrollableControl)this.Control).AutoScroll)
			{
				int num = (int)Native.SendMessage(this.Control.Handle, Native.Msg.WM_NCHITTEST, IntPtr.Zero, Native.LParam(pt.X, pt.Y));
				if (num == 6 || num == 7)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Processes Windows messages and passes WM_HSCROLL and WM_VSCROLL messages to the control at design time.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x060001CF RID: 463 RVA: 0x00006611 File Offset: 0x00004811
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == 276 || m.Msg == 277)
			{
				base.DefWndProc(ref m);
			}
		}

		// Token: 0x040000C5 RID: 197
		private const int HTHSCROLL = 6;

		// Token: 0x040000C6 RID: 198
		private const int HTVSCROLL = 7;
	}
}
