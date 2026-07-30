using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides a user interface for selecting an URL that indicates the location of an XSL file.</summary>
	// Token: 0x020000C2 RID: 194
	public class XslUrlEditor : UrlEditor
	{
		/// <summary>Gets the caption to display on the selection dialog window.</summary>
		/// <returns>The caption to display on the selection dialog window.</returns>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000096AB File Offset: 0x000078AB
		protected override string Caption
		{
			get
			{
				return "Select XSL Transform File";
			}
		}

		/// <summary>Gets the file name filter string for the editor. This is used to determine the items that appear in the file list of the dialog box.</summary>
		/// <returns>A string that contains information about the file filtering options available in the dialog box.</returns>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000096B2 File Offset: 0x000078B2
		protected override string Filter
		{
			get
			{
				return "XSL Files(*.xsl;*.xslt)|*.xsl;*.xslt|All Files(*.*)|*.*|";
			}
		}

		/// <summary>Gets the options for the URL builder to use.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> that indicates the options for the URL builder to use.</returns>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x000023D8 File Offset: 0x000005D8
		protected override UrlBuilderOptions Options
		{
			get
			{
				return UrlBuilderOptions.NoAbsolute;
			}
		}
	}
}
