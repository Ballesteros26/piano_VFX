using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a try/catch/finally/fault block.</summary>
	// Token: 0x020002B7 RID: 695
	[DebuggerTypeProxy(typeof(Expression.TryExpressionProxy))]
	public sealed class TryExpression : Expression
	{
		// Token: 0x060014B4 RID: 5300 RVA: 0x0003D956 File Offset: 0x0003BB56
		internal TryExpression(Type type, Expression body, Expression @finally, Expression fault, ReadOnlyCollection<CatchBlock> handlers)
		{
			this.Type = type;
			this.Body = body;
			this.Handlers = handlers;
			this.Finally = @finally;
			this.Fault = fault;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.TryExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0003D983 File Offset: 0x0003BB83
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0003D98B File Offset: 0x0003BB8B
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Try;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> representing the body of the try block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the body of the try block.</returns>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0003D98F File Offset: 0x0003BB8F
		public Expression Body { get; }

		/// <summary>Gets the collection of <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions associated with the try block.</summary>
		/// <returns>The collection of <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions associated with the try block.</returns>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0003D997 File Offset: 0x0003BB97
		public ReadOnlyCollection<CatchBlock> Handlers { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> representing the finally block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the finally block.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0003D99F File Offset: 0x0003BB9F
		public Expression Finally { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> representing the fault block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the fault block.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0003D9A7 File Offset: 0x0003BBA7
		public Expression Fault { get; }

		// Token: 0x060014BB RID: 5307 RVA: 0x0003D9AF File Offset: 0x0003BBAF
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitTry(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.TryExpression.Body" /> property of the result.</param>
		/// <param name="handlers">The <see cref="P:System.Linq.Expressions.TryExpression.Handlers" /> property of the result.</param>
		/// <param name="finally">The <see cref="P:System.Linq.Expressions.TryExpression.Finally" /> property of the result.</param>
		/// <param name="fault">The <see cref="P:System.Linq.Expressions.TryExpression.Fault" /> property of the result.</param>
		// Token: 0x060014BC RID: 5308 RVA: 0x0003D9B8 File Offset: 0x0003BBB8
		public TryExpression Update(Expression body, IEnumerable<CatchBlock> handlers, Expression @finally, Expression fault)
		{
			if (((body == this.Body) & (@finally == this.Finally) & (fault == this.Fault)) && ExpressionUtils.SameElements<CatchBlock>(ref handlers, this.Handlers))
			{
				return this;
			}
			return Expression.MakeTry(this.Type, body, @finally, fault, handlers);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0000220F File Offset: 0x0000040F
		internal TryExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
