using System;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a design-time user interface for selecting an XML data file.</summary>
	// Token: 0x020000BC RID: 188
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDataFileEditor : UrlEditor
	{
		/// <summary>Gets the caption to display on the selection dialog box.</summary>
		/// <returns>The caption text to display on the selection dialog box.</returns>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override string Caption
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the URL filter options for the editor, which is used to filter the items that appear in the URL selection dialog box.</summary>
		/// <returns>A string that represents one or more URL filter options for the dialog box.</returns>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override string Filter
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
