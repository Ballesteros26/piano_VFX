using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides properties and methods for selecting a data connection expression that is associated with a control property at design time.</summary>
	// Token: 0x02000056 RID: 86
	public class ConnectionStringsExpressionEditor : ExpressionEditor
	{
		/// <summary>Evaluates a connection string expression and provides the design-time value for a control property.</summary>
		/// <returns>The object referenced by the evaluated expression string if the expression evaluation succeeded; otherwise, null.</returns>
		/// <param name="expression">A connection string expression to evaluate. The expression does not include the ConnectionStrings expression prefix.</param>
		/// <param name="parseTimeData">An object containing additional parsing information for evaluating the expression.</param>
		/// <param name="propertyType">The type of the control property.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		// Token: 0x060002B1 RID: 689 RVA: 0x0000234B File Offset: 0x0000054B
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an expression editor sheet for a connection string expression.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditorSheet" /> instance that defines the connection string expression properties.</returns>
		/// <param name="expression">The expression string set for a control property, used to initialize the expression editor sheet. The expression does not include the ConnectionStrings expression prefix.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		// Token: 0x060002B2 RID: 690 RVA: 0x0000234B File Offset: 0x0000054B
		public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}
	}
}
