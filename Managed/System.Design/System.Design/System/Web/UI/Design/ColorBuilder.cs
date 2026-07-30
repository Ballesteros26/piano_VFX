using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System.Web.UI.Design
{
	/// <summary>Provides an HTML color string builder at design time that allows a user to select a color.</summary>
	// Token: 0x02000054 RID: 84
	public sealed class ColorBuilder
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00002352 File Offset: 0x00000552
		private ColorBuilder()
		{
		}

		/// <summary>Starts a color editor to build an HTML color property value.</summary>
		/// <returns>The color value, represented as a string in an HTML color format, or null if the builder service could not be retrieved.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> whose site is to be used to access design-time services. </param>
		/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> used to parent the picker window. </param>
		/// <param name="initialColor">The initial color to be shown in the picker window, in a valid HTML color format. </param>
		// Token: 0x060002AA RID: 682 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string BuildColor(IComponent component, Control owner, string initialColor)
		{
			throw new NotImplementedException();
		}
	}
}
