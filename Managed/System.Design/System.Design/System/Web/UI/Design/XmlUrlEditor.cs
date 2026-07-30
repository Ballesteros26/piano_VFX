using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides a user interface for selecting a URL that indicates the location of an XML file.</summary>
	// Token: 0x020000BF RID: 191
	public class XmlUrlEditor : UrlEditor
	{
		/// <summary>Gets the caption to display on the selection dialog window.</summary>
		/// <returns>The caption to display on the selection dialog window.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000969D File Offset: 0x0000789D
		protected override string Caption
		{
			get
			{
				return "Select XML File";
			}
		}

		/// <summary>Gets the file name filter string for the editor. This is used to determine the items that appear in the file list of the dialog box.</summary>
		/// <returns>A string that contains information about the file filtering options available in the dialog box.</returns>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000096A4 File Offset: 0x000078A4
		protected override string Filter
		{
			get
			{
				return "XML Files(*.xml)|*.xml|All Files(*.*)|*.*|";
			}
		}

		/// <summary>Gets the options for the URL builder to use.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> that indicates the options for the URL builder to use.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000023D8 File Offset: 0x000005D8
		protected override UrlBuilderOptions Options
		{
			get
			{
				return UrlBuilderOptions.NoAbsolute;
			}
		}
	}
}
