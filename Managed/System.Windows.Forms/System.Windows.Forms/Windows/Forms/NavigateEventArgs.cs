using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGrid.Navigate" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000272 RID: 626
	[ComVisible(true)]
	public class NavigateEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NavigateEventArgs" /> class.</summary>
		/// <param name="isForward">true to navigate in a forward direction; otherwise, false. </param>
		// Token: 0x060028C2 RID: 10434 RVA: 0x0009E088 File Offset: 0x0009C288
		public NavigateEventArgs(bool isForward)
		{
			this.forward = isForward;
		}

		/// <summary>Gets a value indicating whether to navigate in a forward direction.</summary>
		/// <returns>true if the navigation is in a forward direction; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x0009E098 File Offset: 0x0009C298
		public bool Forward
		{
			get
			{
				return this.forward;
			}
		}

		// Token: 0x04001465 RID: 5221
		private bool forward;
	}
}
