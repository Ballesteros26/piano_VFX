using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a strongly typed lambda expression as a data structure in the form of an expression tree. This class cannot be inherited.</summary>
	/// <typeparam name="TDelegate">The type of the delegate that the <see cref="T:System.Linq.Expressions.Expression`1" /> represents.</typeparam>
	// Token: 0x02000283 RID: 643
	public class Expression<TDelegate> : LambdaExpression
	{
		// Token: 0x060012E0 RID: 4832 RVA: 0x0003B57A File Offset: 0x0003977A
		internal Expression(Expression body)
			: base(body)
		{
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0000C4DE File Offset: 0x0000A6DE
		internal sealed override Type TypeCore
		{
			get
			{
				return typeof(TDelegate);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0003B583 File Offset: 0x00039783
		internal override Type PublicType
		{
			get
			{
				return typeof(Expression<TDelegate>);
			}
		}

		/// <summary>Compiles the lambda expression described by the expression tree into executable code and produces a delegate that represents the lambda expression.</summary>
		/// <returns>A delegate of type <paramref name="TDelegate" /> that represents the compiled lambda expression described by the <see cref="T:System.Linq.Expressions.Expression`1" />.</returns>
		// Token: 0x060012E3 RID: 4835 RVA: 0x0003B58F File Offset: 0x0003978F
		public new TDelegate Compile()
		{
			return this.Compile(false);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0003B598 File Offset: 0x00039798
		public new TDelegate Compile(bool preferInterpretation)
		{
			return (TDelegate)((object)LambdaCompiler.Compile(this));
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property of the result.</param>
		/// <param name="parameters">The <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> property of the result. </param>
		// Token: 0x060012E5 RID: 4837 RVA: 0x0003B5A8 File Offset: 0x000397A8
		public Expression<TDelegate> Update(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			if (body == base.Body)
			{
				ICollection<ParameterExpression> collection;
				if (parameters == null)
				{
					collection = null;
				}
				else
				{
					collection = parameters as ICollection<ParameterExpression>;
					if (collection == null)
					{
						collection = (parameters = parameters.ToReadOnly<ParameterExpression>());
					}
				}
				if (this.SameParameters(collection))
				{
					return this;
				}
			}
			return Expression.Lambda<TDelegate>(body, base.Name, base.TailCall, parameters);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0003B5F7 File Offset: 0x000397F7
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLambda<TDelegate>(this);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0003B600 File Offset: 0x00039800
		internal override LambdaExpression Accept(StackSpiller spiller)
		{
			return spiller.Rewrite<TDelegate>(this);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0003B60C File Offset: 0x0003980C
		internal static Expression<TDelegate> Create(Expression body, string name, bool tailCall, IReadOnlyList<ParameterExpression> parameters)
		{
			if (name != null || tailCall)
			{
				return new FullExpression<TDelegate>(body, name, tailCall, parameters);
			}
			switch (parameters.Count)
			{
			case 0:
				return new Expression0<TDelegate>(body);
			case 1:
				return new Expression1<TDelegate>(body, parameters[0]);
			case 2:
				return new Expression2<TDelegate>(body, parameters[0], parameters[1]);
			case 3:
				return new Expression3<TDelegate>(body, parameters[0], parameters[1], parameters[2]);
			default:
				return new ExpressionN<TDelegate>(body, parameters);
			}
		}

		/// <summary>Produces a delegate that represents the lambda expression.</summary>
		/// <returns>A delegate containing the compiled version of the lambda.</returns>
		/// <param name="debugInfoGenerator">Debugging information generator used by the compiler to mark sequence points and annotate local variables.</param>
		// Token: 0x060012EB RID: 4843 RVA: 0x0003B695 File Offset: 0x00039895
		public new TDelegate Compile(DebugInfoGenerator debugInfoGenerator)
		{
			return this.Compile();
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0000220F File Offset: 0x0000040F
		internal Expression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
