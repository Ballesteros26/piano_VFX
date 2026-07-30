using System;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a design-time user interface for selecting an XML transform file.</summary>
	// Token: 0x020000C1 RID: 193
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XslTransformFileEditor : UrlEditor
	{
		/// <summary>Gets the caption to display on the selection dialog box.</summary>
		/// <returns>The caption text to display on the selection dialog box.</returns>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000234B File Offset: 0x0000054B
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
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000234B File Offset: 0x0000054B
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
