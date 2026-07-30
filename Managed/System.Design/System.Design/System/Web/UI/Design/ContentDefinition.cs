using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides a property structure that defines Web content at design time.</summary>
	// Token: 0x02000058 RID: 88
	public class ContentDefinition
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.ContentDefinition" /> class.</summary>
		/// <param name="id">A string identifier for the content.</param>
		/// <param name="content">The default HTML markup content.</param>
		/// <param name="designTimeHtml">The design-time HTML markup content.</param>
		// Token: 0x060002BE RID: 702 RVA: 0x00008E47 File Offset: 0x00007047
		public ContentDefinition(string id, string content, string designTimeHtml)
		{
			this.id = id;
			this.content = content;
			this.html = designTimeHtml;
		}

		/// <summary>Gets the ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> control that is associated with the current content.</summary>
		/// <returns>The ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> associated with the current content.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00008E64 File Offset: 0x00007064
		public string ContentPlaceHolderID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets the default HTML markup for the content.</summary>
		/// <returns>A string containing HTML markup.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00008E6C File Offset: 0x0000706C
		public string DefaultContent
		{
			get
			{
				return this.content;
			}
		}

		/// <summary>Gets the HTML markup to represent the content at design time.</summary>
		/// <returns>A string containing HTML markup.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00008E74 File Offset: 0x00007074
		public string DefaultDesignTimeHtml
		{
			get
			{
				return this.html;
			}
		}

		// Token: 0x0400011B RID: 283
		private string id;

		// Token: 0x0400011C RID: 284
		private string content;

		// Token: 0x0400011D RID: 285
		private string html;
	}
}
