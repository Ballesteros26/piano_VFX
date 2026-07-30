using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
	// Token: 0x020003C5 RID: 965
	public class LiteralControlBuilder : ControlBuilder
	{
		/// <summary>Determines whether the control builder should process the white space literals that are represented by the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
		/// <returns>false.</returns>
		// Token: 0x06002823 RID: 10275 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		/// <summary>Throws <see cref="T:System.Web.HttpException" />, because adding child control builders does not apply to the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
		/// <param name="subBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> object to add the child control builders to. </param>
		/// <exception cref="T:System.Web.HttpException">An attempt is made to use this method. </exception>
		// Token: 0x06002824 RID: 10276 RVA: 0x000683F7 File Offset: 0x000665F7
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			throw new HttpException("LiteralControlBuilder should never be called");
		}

		/// <summary>Adds the specified literal content to a control. The <see cref="M:System.Web.UI.WebControls.LiteralControlBuilder.AppendLiteralString(System.String)" /> method is called by the ASP.NET page framework.</summary>
		/// <param name="s">The content to add to the control.</param>
		/// <exception cref="T:System.Web.HttpException">The string literal is not well formed. </exception>
		// Token: 0x06002825 RID: 10277 RVA: 0x00068403 File Offset: 0x00066603
		public override void AppendLiteralString(string s)
		{
			base.AppendLiteralString(s);
		}
	}
}
