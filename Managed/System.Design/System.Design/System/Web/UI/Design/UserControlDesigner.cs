using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Provides designer functionality for user controls.</summary>
	// Token: 0x020000B1 RID: 177
	public class UserControlDesigner : ControlDesigner
	{
		/// <summary>Gets the HTML markup that is used to represent the user control at design time.</summary>
		/// <returns>The markup that is used to represent the control at design time.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Control.ID" /> property of a child control is empty or null.</exception>
		// Token: 0x0600053C RID: 1340 RVA: 0x000094BC File Offset: 0x000076BC
		public override string GetDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml();
		}

		/// <summary>Gets the action list collection for the user control designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> that contains the action list tags for the control designer.</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000234B File Offset: 0x0000054B
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the user control can be resized.</summary>
		/// <returns>false.</returns>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000241E File Offset: 0x0000061E
		public override bool AllowResize
		{
			get
			{
				return false;
			}
		}
	}
}
