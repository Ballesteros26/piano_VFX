using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows horizontal scroll bar.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AD RID: 429
	[ClassInterface(1)]
	[ComVisible(true)]
	public class HScrollBar : ScrollBar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HScrollBar" /> class. </summary>
		// Token: 0x06001C00 RID: 7168 RVA: 0x0006C13C File Offset: 0x0006A33C
		public HScrollBar()
		{
			this.vert = false;
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0006C14C File Offset: 0x0006A34C
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.HScrollBarDefaultSize;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0006C158 File Offset: 0x0006A358
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}
	}
}
