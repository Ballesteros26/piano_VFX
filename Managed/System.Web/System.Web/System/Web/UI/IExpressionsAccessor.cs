using System;

namespace System.Web.UI
{
	/// <summary>Defines the properties a class must implement to support collections of expressions.</summary>
	// Token: 0x02000173 RID: 371
	public interface IExpressionsAccessor
	{
		/// <summary>Gets a value indicating whether the instance of the class that implements this interface has any properties bound by an expression.</summary>
		/// <returns>true if the control has properties set through expressions; otherwise, false. </returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000F6E RID: 3950
		bool HasExpressions { get; }

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.ExpressionBinding" /> objects.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ExpressionBindingCollection" /> containing <see cref="T:System.Web.UI.ExpressionBinding" /> objects that represent the properties and expressions for a control.</returns>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000F6F RID: 3951
		ExpressionBindingCollection Expressions { get; }
	}
}
