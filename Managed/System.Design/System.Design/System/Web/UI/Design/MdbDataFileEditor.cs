using System;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a design-time user interface for selecting a Microsoft Access database file.</summary>
	// Token: 0x0200009C RID: 156
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MdbDataFileEditor : UrlEditor
	{
		/// <summary>Gets the caption to display on the selection dialog box.</summary>
		/// <returns>The caption text to display on the selection dialog box.</returns>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000234B File Offset: 0x0000054B
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
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000234B File Offset: 0x0000054B
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
