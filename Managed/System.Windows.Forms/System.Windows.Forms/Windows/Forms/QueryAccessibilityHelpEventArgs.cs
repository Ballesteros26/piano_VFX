using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.QueryAccessibilityHelp" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AD RID: 685
	[ComVisible(true)]
	public class QueryAccessibilityHelpEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryAccessibilityHelpEventArgs" /> class.</summary>
		// Token: 0x06002DCC RID: 11724 RVA: 0x000B112C File Offset: 0x000AF32C
		public QueryAccessibilityHelpEventArgs()
		{
			this.help_namespace = null;
			this.help_string = null;
			this.help_keyword = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryAccessibilityHelpEventArgs" /> class.</summary>
		/// <param name="helpNamespace">The file containing Help for the <see cref="T:System.Windows.Forms.AccessibleObject" />. </param>
		/// <param name="helpString">The string defining what Help to get for the <see cref="T:System.Windows.Forms.AccessibleObject" />. </param>
		/// <param name="helpKeyword">The keyword to associate with the Help request for the <see cref="T:System.Windows.Forms.AccessibleObject" />. </param>
		// Token: 0x06002DCD RID: 11725 RVA: 0x000B114C File Offset: 0x000AF34C
		public QueryAccessibilityHelpEventArgs(string helpNamespace, string helpString, string helpKeyword)
		{
			this.help_namespace = helpNamespace;
			this.help_string = helpString;
			this.help_keyword = helpKeyword;
		}

		/// <summary>Gets or sets the Help keyword for the specified control.</summary>
		/// <returns>The Help topic associated with the <see cref="T:System.Windows.Forms.AccessibleObject" /> that was queried.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000B116C File Offset: 0x000AF36C
		// (set) Token: 0x06002DCF RID: 11727 RVA: 0x000B1174 File Offset: 0x000AF374
		public string HelpKeyword
		{
			get
			{
				return this.help_keyword;
			}
			set
			{
				this.help_keyword = value;
			}
		}

		/// <summary>Gets or sets a value specifying the name of the Help file.</summary>
		/// <returns>The name of the Help file. This name can be in the form C:\path\sample.chm or /folder/file.htm.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000B1180 File Offset: 0x000AF380
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x000B1188 File Offset: 0x000AF388
		public string HelpNamespace
		{
			get
			{
				return this.help_namespace;
			}
			set
			{
				this.help_namespace = value;
			}
		}

		/// <summary>Gets or sets the string defining what Help to get for the <see cref="T:System.Windows.Forms.AccessibleObject" />.</summary>
		/// <returns>The Help to retrieve for the accessible object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000B1194 File Offset: 0x000AF394
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x000B119C File Offset: 0x000AF39C
		public string HelpString
		{
			get
			{
				return this.help_string;
			}
			set
			{
				this.help_string = value;
			}
		}

		// Token: 0x0400160E RID: 5646
		private string help_namespace;

		// Token: 0x0400160F RID: 5647
		private string help_string;

		// Token: 0x04001610 RID: 5648
		private string help_keyword;
	}
}
