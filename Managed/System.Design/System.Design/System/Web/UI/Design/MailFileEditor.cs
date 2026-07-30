using System;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a user interface for selecting and editing a mail file name for a property at design time.</summary>
	// Token: 0x0200009B RID: 155
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MailFileEditor : UrlEditor
	{
		/// <summary>Gets the caption for the editor dialog.</summary>
		/// <returns>The caption for the design-time dialog box.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override string Caption
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the file filter string for the dialog (such as "*.txt").</summary>
		/// <returns>The filter for selecting files in the design-time dialog box.</returns>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000234B File Offset: 0x0000054B
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
