using System;
using Unity;

namespace System.Web.UI.Design
{
	/// <summary>Provides properties and methods for composing a RouteURL expression at design time.</summary>
	// Token: 0x0200017A RID: 378
	public class RouteUrlExpressionEditor : ExpressionEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.RouteUrlExpressionEditor" /> class.</summary>
		// Token: 0x06000B02 RID: 2818 RVA: 0x00009519 File Offset: 0x00007719
		public RouteUrlExpressionEditor()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Evaluates a RouteUrl expression and provides the design-time value.</summary>
		/// <returns>The URL for the specified route and route parameter values if the expression evaluation succeeded; otherwise, null.</returns>
		/// <param name="expression">A RouteUrl expression to evaluate. The expression does not include the RouteUrl expression prefix.</param>
		/// <param name="parseTimeData">An object that contains additional parsing information for evaluating the expression.</param>
		/// <param name="propertyType">The type of the control property.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host that is used to obtain additional design-time services.</param>
		// Token: 0x06000B03 RID: 2819 RVA: 0x0000970B File Offset: 0x0000790B
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
