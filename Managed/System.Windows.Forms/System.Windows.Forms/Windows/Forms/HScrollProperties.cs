using System;

namespace System.Windows.Forms
{
	/// <summary>Provides basic properties for the <see cref="T:System.Windows.Forms.HScrollBar" /></summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B5 RID: 437
	public class HScrollProperties : ScrollProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HScrollProperties" /> class. </summary>
		/// <param name="container">A <see cref="T:System.Windows.Forms.ScrollableControl" /> that contains the scroll bar.</param>
		// Token: 0x06001C3A RID: 7226 RVA: 0x0006C81C File Offset: 0x0006AA1C
		public HScrollProperties(ScrollableControl container)
			: base(container)
		{
			this.scroll_bar = container.hscrollbar;
		}
	}
}
