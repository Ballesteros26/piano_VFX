using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>The <see cref="T:System.Web.UI.WebControls.DataControlCommands" /> class contains public fields that all ASP.NET data-bound controls use, to promote a consistent user interface (UI). This class cannot be inherited.</summary>
	// Token: 0x02000370 RID: 880
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataControlCommands
	{
		// Token: 0x0600211F RID: 8479 RVA: 0x00002050 File Offset: 0x00000250
		private DataControlCommands()
		{
		}

		/// <summary>Represents the string "Cancel".</summary>
		// Token: 0x040018B4 RID: 6324
		public const string CancelCommandName = "Cancel";

		/// <summary>Represents the string "Delete".</summary>
		// Token: 0x040018B5 RID: 6325
		public const string DeleteCommandName = "Delete";

		/// <summary>Represents the string "Edit".</summary>
		// Token: 0x040018B6 RID: 6326
		public const string EditCommandName = "Edit";

		/// <summary>Represents the string "First".</summary>
		// Token: 0x040018B7 RID: 6327
		public const string FirstPageCommandArgument = "First";

		/// <summary>Represents the string "Insert".</summary>
		// Token: 0x040018B8 RID: 6328
		public const string InsertCommandName = "Insert";

		/// <summary>Represents the string "Last".</summary>
		// Token: 0x040018B9 RID: 6329
		public const string LastPageCommandArgument = "Last";

		/// <summary>Represents the string "Next".</summary>
		// Token: 0x040018BA RID: 6330
		public const string NextPageCommandArgument = "Next";

		/// <summary>Represents the string "New".</summary>
		// Token: 0x040018BB RID: 6331
		public const string NewCommandName = "New";

		/// <summary>Represents the string "Page".</summary>
		// Token: 0x040018BC RID: 6332
		public const string PageCommandName = "Page";

		/// <summary>Represents the string "Prev".</summary>
		// Token: 0x040018BD RID: 6333
		public const string PreviousPageCommandArgument = "Prev";

		/// <summary>Represents the string "Select".</summary>
		// Token: 0x040018BE RID: 6334
		public const string SelectCommandName = "Select";

		/// <summary>Represents the string "Sort".</summary>
		// Token: 0x040018BF RID: 6335
		public const string SortCommandName = "Sort";

		/// <summary>Represents the string "Update".</summary>
		// Token: 0x040018C0 RID: 6336
		public const string UpdateCommandName = "Update";
	}
}
