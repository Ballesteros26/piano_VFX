using System;

namespace System.Web.UI
{
	/// <summary>Supports the page parser in building controls that are connected to a data provider. This class cannot be inherited.</summary>
	// Token: 0x0200015D RID: 349
	public sealed class DataSourceControlBuilder : ControlBuilder
	{
		/// <summary>Determines whether white-space literals are permitted in the content between a control's opening and closing tags.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x06000F34 RID: 3892 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
