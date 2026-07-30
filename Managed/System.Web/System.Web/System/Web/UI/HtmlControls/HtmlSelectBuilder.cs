using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Interacts with the parser to build an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	// Token: 0x02000270 RID: 624
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlSelectBuilder : ControlBuilder
	{
		/// <summary>Determines whether the white space literals in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control are to be processed or ignored.</summary>
		/// <returns>This method always returns false, indicating that white space in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control is ignored.</returns>
		// Token: 0x060019B4 RID: 6580 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		/// <summary>Obtains the <see cref="T:System.Type" /> for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's child controls.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's specified child control.</returns>
		/// <param name="tagName">The tag name of the child control. </param>
		/// <param name="attribs">An array of attributes contained in the child control. </param>
		// Token: 0x060019B5 RID: 6581 RVA: 0x00044DBC File Offset: 0x00042FBC
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			if (string.Compare(tagName, "option", true, Helpers.InvariantCulture) != 0)
			{
				return null;
			}
			string text = attribs["selected"] as string;
			if (text != null && text.Length > 0 && string.Compare(text, "selected", true, Helpers.InvariantCulture) == 0)
			{
				attribs["selected"] = "true";
			}
			return typeof(ListItem);
		}
	}
}
