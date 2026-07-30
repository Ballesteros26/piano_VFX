using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a region for content in an ASP.NET master page.</summary>
	// Token: 0x0200035B RID: 859
	[ControlBuilder(typeof(ContentPlaceHolderBuilder))]
	[ToolboxData("<;{0}:ContentPlaceHolder runat=&quot;server&quot;></{0}:ContentPlaceHolder>")]
	[ToolboxItemFilter("System.Web.UI", ToolboxItemFilterType.Allow)]
	[ToolboxItemFilter("Microsoft.VisualStudio.Web.WebForms.MasterPageWebFormDesigner", ToolboxItemFilterType.Require)]
	[Designer("System.Web.UI.Design.WebControls.ContentPlaceHolderDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class ContentPlaceHolder : Control, INamingContainer, INonBindingContainer
	{
	}
}
