using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides properties and methods for evaluating and editing a resource expression at design time.</summary>
	// Token: 0x0200009E RID: 158
	public class ResourceExpressionEditor : ExpressionEditor
	{
		/// <summary>Evaluates a resource expression and provides the design-time value for a control property.</summary>
		/// <returns>The object referenced by the evaluated expression string, if the expression evaluation succeeded; otherwise, null.</returns>
		/// <param name="expression">A resource expression to evaluate. <paramref name="expression" /> does not include the Resources expression prefix.</param>
		/// <param name="parseTimeData">An object supplying additional parse data, in the form of a <see cref="T:System.Web.Compilation.ResourceExpressionFields" /> value.</param>
		/// <param name="propertyType">The type of the control property.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		// Token: 0x060004AF RID: 1199 RVA: 0x0000234B File Offset: 0x0000054B
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a resource expression editor sheet that is initialized with the input expression string and service provider implementation.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.ResourceExpressionEditorSheet" /> that defines the resource expression properties.</returns>
		/// <param name="expression">A resource expression, used to initialize the expression editor sheet.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to initialize the expression editor sheet.</param>
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000234B File Offset: 0x0000054B
		public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}
	}
}
