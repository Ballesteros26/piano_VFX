using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Interacts with the parser to build an <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control.</summary>
	// Token: 0x0200025F RID: 607
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlHeadBuilder : ControlBuilder
	{
		/// <summary>Determines whether the literal white space characters in the control must be processed or ignored.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x060018B9 RID: 6329 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		/// <summary>Obtains the <see cref="T:System.Type" /> for the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control's child controls. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the specified control's child control.</returns>
		/// <param name="tagName">The tag name of the child control.</param>
		/// <param name="attribs">An array of attributes contained in the child control.</param>
		// Token: 0x060018BA RID: 6330 RVA: 0x00042A28 File Offset: 0x00040C28
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			if (string.Compare(tagName, "title", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return typeof(HtmlTitle);
			}
			if (string.Compare(tagName, "link", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return typeof(HtmlLink);
			}
			if (string.Compare(tagName, "meta", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return typeof(HtmlMeta);
			}
			return null;
		}
	}
}
