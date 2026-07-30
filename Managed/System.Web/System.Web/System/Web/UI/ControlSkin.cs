using System;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Represents a control skin, which is a means to define stylistic properties that are applied to an ASP.NET Web server control. </summary>
	// Token: 0x020001BA RID: 442
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class ControlSkin
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.ControlSkin" /> class using the passed <see cref="T:System.Web.UI.Control" /> type and delegate.</summary>
		/// <param name="controlType">The <see cref="T:System.Type" /> of <see cref="T:System.Web.UI.Control" /> to which the skin is applied, used to enforce type consistency among named skins. </param>
		/// <param name="themeDelegate">The <see cref="T:System.Web.UI.ControlSkinDelegate" /> that applies the style elements defined in a control skin file to the type identified by the <paramref name="controlType" /> parameter. </param>
		// Token: 0x060011F9 RID: 4601 RVA: 0x00031BDB File Offset: 0x0002FDDB
		public ControlSkin(Type controlType, ControlSkinDelegate themeDelegate)
		{
			this.controlType = controlType;
			this.themeDelegate = themeDelegate;
		}

		/// <summary>Applies the skin to the <see cref="T:System.Web.UI.Control" /> control contained by the <see cref="T:System.Web.UI.ControlSkin" /> object.</summary>
		/// <param name="control">The control to which to apply the skin. </param>
		// Token: 0x060011FA RID: 4602 RVA: 0x00031BF1 File Offset: 0x0002FDF1
		public void ApplySkin(Control control)
		{
			this.themeDelegate(control);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the control that the <see cref="T:System.Web.UI.ControlSkin" /> object is associated with.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Web.UI.Control" /> used in this instance.</returns>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00031C00 File Offset: 0x0002FE00
		public Type ControlType
		{
			get
			{
				return this.controlType;
			}
		}

		// Token: 0x0400140B RID: 5131
		private Type controlType;

		// Token: 0x0400140C RID: 5132
		private ControlSkinDelegate themeDelegate;
	}
}
