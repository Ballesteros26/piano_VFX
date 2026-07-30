using System;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a dialog box for selecting files to edit at design time.</summary>
	// Token: 0x020000B2 RID: 178
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class UserControlFileEditor : UrlEditor
	{
		/// <summary>Gets the caption for the dialog box.</summary>
		/// <returns>The caption for the editor window.</returns>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override string Caption
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the file name filter string used to determine the items that appear in the file list of the dialog box.</summary>
		/// <returns>A string that filters the list of files available in the dialog box, such as "*.txt".</returns>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000234B File Offset: 0x0000054B
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
