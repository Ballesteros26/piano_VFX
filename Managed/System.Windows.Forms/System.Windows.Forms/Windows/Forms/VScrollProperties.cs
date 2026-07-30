using System;

namespace System.Windows.Forms
{
	/// <summary>Provides basic properties for the <see cref="T:System.Windows.Forms.VScrollBar" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A7 RID: 935
	public class VScrollProperties : ScrollProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VScrollProperties" /> class. </summary>
		/// <param name="container">A <see cref="T:System.Windows.Forms.ScrollableControl" /> that contains the scroll bar.</param>
		// Token: 0x0600441E RID: 17438 RVA: 0x0010C1F8 File Offset: 0x0010A3F8
		public VScrollProperties(ScrollableControl container)
			: base(container)
		{
			this.scroll_bar = container.vscrollbar;
		}
	}
}
