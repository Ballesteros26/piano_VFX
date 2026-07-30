using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.PropertyGrid.SelectedGridItemChanged" /> event of the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002D6 RID: 726
	public class SelectedGridItemChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectedGridItemChangedEventArgs" /> class.</summary>
		/// <param name="oldSel">The previously selected grid item. </param>
		/// <param name="newSel">The newly selected grid item. </param>
		// Token: 0x06002FF7 RID: 12279 RVA: 0x000B9924 File Offset: 0x000B7B24
		public SelectedGridItemChangedEventArgs(GridItem oldSel, GridItem newSel)
		{
			this.old_selection = oldSel;
			this.new_selection = newSel;
		}

		/// <summary>Gets the newly selected <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x000B993C File Offset: 0x000B7B3C
		public GridItem NewSelection
		{
			get
			{
				return this.new_selection;
			}
		}

		/// <summary>Gets the previously selected <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>The old <see cref="T:System.Windows.Forms.GridItem" />. This can be null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x000B9944 File Offset: 0x000B7B44
		public GridItem OldSelection
		{
			get
			{
				return this.old_selection;
			}
		}

		// Token: 0x040016FB RID: 5883
		private GridItem new_selection;

		// Token: 0x040016FC RID: 5884
		private GridItem old_selection;
	}
}
