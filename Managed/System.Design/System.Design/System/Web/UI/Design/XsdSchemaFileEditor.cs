using System;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a design-time user interface for selecting an XML schema definition file.</summary>
	// Token: 0x020000C0 RID: 192
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XsdSchemaFileEditor : UrlEditor
	{
		/// <summary>Gets the caption to display on the selection dialog box.</summary>
		/// <returns>The caption text to display on the selection dialog box.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override string Caption
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the URL filter options for the editor, which are used to filter the items that appear in the URL selection dialog box.</summary>
		/// <returns>A string that represents one or more URL filter options for the dialog box.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000234B File Offset: 0x0000054B
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
