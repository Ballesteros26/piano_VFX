using System;
using Unity;

namespace System.Web.UI.Design
{
	/// <summary>Provides properties and methods for composing a RouteValue expression at design time.</summary>
	// Token: 0x0200017C RID: 380
	public class RouteValueExpressionEditor : ExpressionEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.RouteValueExpressionEditor" /> class.</summary>
		// Token: 0x06000B0A RID: 2826 RVA: 0x00009519 File Offset: 0x00007719
		public RouteValueExpressionEditor()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Evaluates a RouteValue expression and provides the design-time value.</summary>
		/// <returns>The value of the specified URL parameter if the expression evaluation succeeded; otherwise, null.</returns>
		/// <param name="expression">A RouteValue expression to evaluate. The expression does not include the RouteValue expression prefix.</param>
		/// <param name="parseTimeData">An object that contains additional parsing information that is used to evaluate the expression.</param>
		/// <param name="propertyType">The type of the control property.</param>
		/// <param name="serviceProvider">A service provider implementation that is supplied by the designer host and that is used to obtain additional design-time services.</param>
		// Token: 0x06000B0B RID: 2827 RVA: 0x0000970B File Offset: 0x0000790B
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
