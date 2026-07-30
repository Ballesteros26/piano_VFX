using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.BulletedList.Click" /> event of a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	// Token: 0x02000281 RID: 641
	public class BulletedListEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BulletedListEventArgs" /> class.</summary>
		/// <param name="index">The zero-based index of the list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> that raised the event. </param>
		// Token: 0x06001A68 RID: 6760 RVA: 0x00045D58 File Offset: 0x00043F58
		public BulletedListEventArgs(int index)
		{
			this._index = index;
		}

		/// <summary>Gets the index of the list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control that raised the event.</summary>
		/// <returns>The index of the list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control that raised the event.</returns>
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00045D67 File Offset: 0x00043F67
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x0400167D RID: 5757
		private int _index;
	}
}
