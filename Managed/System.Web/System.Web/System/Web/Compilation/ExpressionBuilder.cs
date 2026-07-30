using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation
{
	/// <summary>Evaluates expressions during page parsing.</summary>
	// Token: 0x02000653 RID: 1619
	public abstract class ExpressionBuilder
	{
		/// <summary>When overridden in a derived class, returns code that is used during page execution to obtain the evaluated expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that is used for property assignment.</returns>
		/// <param name="entry">The object that represents information about the property bound to by the expression.</param>
		/// <param name="parsedData">The object containing parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />. </param>
		/// <param name="context">Contextual information for the evaluation of the expression.</param>
		// Token: 0x0600457D RID: 17789
		public abstract CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context);

		/// <summary>When overridden in a derived class, returns an object that represents an evaluated expression.</summary>
		/// <returns>An object that represents the evaluated expression; otherwise, null if the inheritor does not implement <see cref="M:System.Web.Compilation.ExpressionBuilder.EvaluateExpression(System.Object,System.Web.UI.BoundPropertyEntry,System.Object,System.Web.Compilation.ExpressionBuilderContext)" />.</returns>
		/// <param name="target">The object containing the expression.</param>
		/// <param name="entry">The object that represents information about the property bound to by the expression.</param>
		/// <param name="parsedData">The object containing parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
		/// <param name="context">Contextual information for the evaluation of the expression.</param>
		// Token: 0x0600457E RID: 17790 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return null;
		}

		/// <summary>When overridden in a derived class, returns an object that represents the parsed expression.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the parsed representation of the expression; otherwise, null if <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" /> is not implemented.</returns>
		/// <param name="expression">The value of the declarative expression.</param>
		/// <param name="propertyType">The type of the property bound to by the expression.</param>
		/// <param name="context">Contextual information for the evaluation of the expression.</param>
		// Token: 0x0600457F RID: 17791 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
		{
			return null;
		}

		/// <summary>When overridden in a derived class, returns a value indicating whether the current <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object supports no-compile pages. </summary>
		/// <returns>true if the <see cref="T:System.Web.Compilation.ExpressionBuilder" /> supports expression evaluation; otherwise, false.</returns>
		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06004580 RID: 17792 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool SupportsEvaluate
		{
			get
			{
				return false;
			}
		}
	}
}
