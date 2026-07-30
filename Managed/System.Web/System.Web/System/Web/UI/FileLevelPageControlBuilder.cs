using System;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	/// <summary>Parses page files and is the default <see cref="T:System.Web.UI.ControlBuilder" /> class for parsing page files.</summary>
	// Token: 0x020001D0 RID: 464
	public class FileLevelPageControlBuilder : RootBuilder
	{
		/// <summary>Adds the specified literal content to a control. </summary>
		/// <param name="text">The content to add to the control.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="M:System.Web.UI.FileLevelPageControlBuilder.AppendLiteralString(System.String)" /> method cannot append the literal string to a content page.</exception>
		// Token: 0x060012E4 RID: 4836 RVA: 0x000334BC File Offset: 0x000316BC
		public override void AppendLiteralString(string text)
		{
			bool flag = text == null || text.Trim().Length == 0;
			if (this.hasContentControls && !flag)
			{
				throw new HttpException("Literal strings cannot be appended to Content pages.");
			}
			if (!flag)
			{
				this.hasLiteralControls = true;
			}
			base.AppendLiteralString(text);
		}

		/// <summary>Adds a <see cref="T:System.Web.UI.ControlBuilder" /> object to the <see cref="T:System.Web.UI.FileLevelPageControlBuilder" /> object for any child controls that belong to the container control.</summary>
		/// <param name="subBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> assigned to the child control. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.ControlBuilder" /> that was added is associated with a <see cref="T:System.Web.UI.WebControls.Content" /> control and is only allowed on pages that contain <see cref="T:System.Web.UI.WebControls.Content" /> controls.</exception>
		/// <exception cref="T:System.Web.HttpParseException">The content page contained a literal other than a <see cref="T:System.Web.UI.WebControls.Content" /> control.</exception>
		// Token: 0x060012E5 RID: 4837 RVA: 0x00033508 File Offset: 0x00031708
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			if (subBuilder == null)
			{
				base.AppendSubBuilder(subBuilder);
				return;
			}
			if (typeof(ContentBuilderInternal).IsAssignableFrom(subBuilder.GetType()))
			{
				if (this.hasOtherControls)
				{
					throw new HttpException("Only Content controls are supported on content pages.");
				}
				this.hasContentControls = true;
				if (this.hasLiteralControls)
				{
					throw new HttpParseException("Only Content controls are supported on content pages.");
				}
			}
			else
			{
				this.hasOtherControls = true;
			}
			base.AppendSubBuilder(subBuilder);
		}

		// Token: 0x0400143A RID: 5178
		private bool hasContentControls;

		// Token: 0x0400143B RID: 5179
		private bool hasLiteralControls;

		// Token: 0x0400143C RID: 5180
		private bool hasOtherControls;
	}
}
