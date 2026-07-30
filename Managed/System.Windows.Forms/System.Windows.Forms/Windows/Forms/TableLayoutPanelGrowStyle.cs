using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a <see cref="T:System.Windows.Forms.TableLayoutPanel" /> will gain additional rows or columns after its existing cells are full.</summary>
	// Token: 0x02000308 RID: 776
	public enum TableLayoutPanelGrowStyle
	{
		/// <summary>The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> does not allow additional rows or columns after it is full.</summary>
		// Token: 0x04001874 RID: 6260
		FixedSize,
		/// <summary>The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> gains additional rows after it is full.</summary>
		// Token: 0x04001875 RID: 6261
		AddRows,
		/// <summary>The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> gains additional columns after it is full.</summary>
		// Token: 0x04001876 RID: 6262
		AddColumns
	}
}
