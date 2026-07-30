using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides information to the parser during design time.</summary>
	// Token: 0x020001C9 RID: 457
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DesignTimeParseData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DesignTimeParseData" /> class without a specified filter. </summary>
		/// <param name="designerHost">The object for managing designer transactions and components.</param>
		/// <param name="parseText">The text to parse during design time.</param>
		// Token: 0x060012A6 RID: 4774 RVA: 0x00032FC3 File Offset: 0x000311C3
		public DesignTimeParseData(IDesignerHost designerHost, string parseText)
		{
			this.host = designerHost;
			this.text = parseText;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DesignTimeParseData" /> class with the specified filter. </summary>
		/// <param name="designerHost">The object for managing designer transactions and components.</param>
		/// <param name="parseText">The text to parse during design time.</param>
		/// <param name="filter">The filter to apply during design time.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parseText" /> is null.</exception>
		// Token: 0x060012A7 RID: 4775 RVA: 0x00032FD9 File Offset: 0x000311D9
		public DesignTimeParseData(IDesignerHost designerHost, string parseText, string filter)
			: this(designerHost, parseText)
		{
			this.filter = filter;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00032FEA File Offset: 0x000311EA
		internal void SetCollection(ICollection collection)
		{
			this.collection = collection;
		}

		/// <summary>Gets or sets the delegate for data binding at design time.</summary>
		/// <returns>An <see cref="T:System.EventHandler" /> for data binding at design time.</returns>
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00032FF3 File Offset: 0x000311F3
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x00032FFB File Offset: 0x000311FB
		public EventHandler DataBindingHandler
		{
			get
			{
				return this.db_handler;
			}
			set
			{
				this.db_handler = value;
			}
		}

		/// <summary>Gets the object for managing designer transactions and components.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> object for managing designer transactions and components.</returns>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00033004 File Offset: 0x00031204
		public IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
		}

		/// <summary>Gets or sets the URL at which the document is located.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the URL.</returns>
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0003300C File Offset: 0x0003120C
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x00033014 File Offset: 0x00031214
		public string DocumentUrl
		{
			get
			{
				return this.durl;
			}
			set
			{
				this.durl = value;
			}
		}

		/// <summary>Gets the text to parse.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the text to parse.</returns>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0003301D File Offset: 0x0003121D
		public string ParseText
		{
			get
			{
				return this.text;
			}
		}

		/// <summary>Gets the filter used at design time.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the filter.</returns>
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00033025 File Offset: 0x00031225
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a theme should be applied.</summary>
		/// <returns>true if a theme should be applied; otherwise, false.</returns>
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0003302D File Offset: 0x0003122D
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x00033035 File Offset: 0x00031235
		public bool ShouldApplyTheme
		{
			get
			{
				return this.theme;
			}
			set
			{
				this.theme = value;
			}
		}

		/// <summary>Gets a collection of information about user control registrations.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the tag prefix, tag name, and location of the user control. The collection is populated automatically by the .NET Framework at parse time.</returns>
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0003303E File Offset: 0x0003123E
		public ICollection UserControlRegisterEntries
		{
			get
			{
				return this.collection;
			}
		}

		// Token: 0x04001429 RID: 5161
		private EventHandler db_handler;

		// Token: 0x0400142A RID: 5162
		private string text;

		// Token: 0x0400142B RID: 5163
		private IDesignerHost host;

		// Token: 0x0400142C RID: 5164
		private string durl;

		// Token: 0x0400142D RID: 5165
		private string filter;

		// Token: 0x0400142E RID: 5166
		private bool theme;

		// Token: 0x0400142F RID: 5167
		private ICollection collection;
	}
}
