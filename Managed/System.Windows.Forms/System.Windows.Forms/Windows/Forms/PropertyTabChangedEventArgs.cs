using System;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyTabChanged" /> event of a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AB RID: 683
	[ComVisible(true)]
	public class PropertyTabChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyTabChangedEventArgs" /> class.</summary>
		/// <param name="oldTab">The Previously selected property tab. </param>
		/// <param name="newTab">The newly selected property tab. </param>
		// Token: 0x06002DC6 RID: 11718 RVA: 0x000B10DC File Offset: 0x000AF2DC
		public PropertyTabChangedEventArgs(PropertyTab oldTab, PropertyTab newTab)
		{
			this.old_tab = oldTab;
			this.new_tab = newTab;
		}

		/// <summary>Gets the new <see cref="T:System.Windows.Forms.Design.PropertyTab" /> selected.</summary>
		/// <returns>The newly selected <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x000B10F4 File Offset: 0x000AF2F4
		public PropertyTab NewTab
		{
			get
			{
				return this.new_tab;
			}
		}

		/// <summary>Gets the old <see cref="T:System.Windows.Forms.Design.PropertyTab" /> selected.</summary>
		/// <returns>The old <see cref="T:System.Windows.Forms.Design.PropertyTab" /> that was selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000B10FC File Offset: 0x000AF2FC
		public PropertyTab OldTab
		{
			get
			{
				return this.old_tab;
			}
		}

		// Token: 0x0400160A RID: 5642
		private PropertyTab old_tab;

		// Token: 0x0400160B RID: 5643
		private PropertyTab new_tab;
	}
}
