using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows vertical scroll bar.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A4 RID: 932
	[ClassInterface(1)]
	[ComVisible(true)]
	public class VScrollBar : ScrollBar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VScrollBar" /> class. </summary>
		// Token: 0x06004417 RID: 17431 RVA: 0x0010C190 File Offset: 0x0010A390
		public VScrollBar()
		{
			this.vert = true;
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.VScrollBar.RightToLeft" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000439 RID: 1081
		// (add) Token: 0x06004418 RID: 17432 RVA: 0x0010C1A0 File Offset: 0x0010A3A0
		// (remove) Token: 0x06004419 RID: 17433 RVA: 0x0010C1AC File Offset: 0x0010A3AC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		/// <summary>Gets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.RightToLeft.No" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x0600441A RID: 17434 RVA: 0x0010C1B8 File Offset: 0x0010A3B8
		// (set) Token: 0x0600441B RID: 17435 RVA: 0x0010C1C0 File Offset: 0x0010A3C0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				if (this.RightToLeft == value)
				{
					return;
				}
				base.RightToLeft = value;
				this.OnRightToLeftChanged(EventArgs.Empty);
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x0600441C RID: 17436 RVA: 0x0010C1E4 File Offset: 0x0010A3E4
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.VScrollBarDefaultSize;
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x0010C1F0 File Offset: 0x0010A3F0
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}
	}
}
