using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a call to either static or an instance method.</summary>
	// Token: 0x02000295 RID: 661
	[DebuggerTypeProxy(typeof(Expression.MethodCallExpressionProxy))]
	public class MethodCallExpression : Expression, IArgumentProvider
	{
		// Token: 0x06001353 RID: 4947 RVA: 0x0003BEF6 File Offset: 0x0003A0F6
		internal MethodCallExpression(MethodInfo method)
		{
			this.Method = method;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00005E51 File Offset: 0x00004051
		internal virtual Expression GetInstance()
		{
			return null;
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0003BF05 File Offset: 0x0003A105
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Call;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.MethodCallExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0003BF08 File Offset: 0x0003A108
		public sealed override Type Type
		{
			get
			{
				return this.Method.ReturnType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodInfo" /> for the method to be called.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the called method.</returns>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x0003BF15 File Offset: 0x0003A115
		public MethodInfo Method { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> that represents the instance for instance method calls or null for static method calls.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the receiving object of the method.</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0003BF1D File Offset: 0x0003A11D
		public Expression Object
		{
			get
			{
				return this.GetInstance();
			}
		}

		/// <summary>Gets a collection of expressions that represent arguments of the called method.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects which represent the arguments to the called method.</returns>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0003BF25 File Offset: 0x0003A125
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.GetOrMakeArguments();
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="object">The <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> property of the result.</param>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> property of the result.</param>
		// Token: 0x0600135A RID: 4954 RVA: 0x0003BF30 File Offset: 0x0003A130
		public MethodCallExpression Update(Expression @object, IEnumerable<Expression> arguments)
		{
			if (@object == this.Object)
			{
				ICollection<Expression> collection;
				if (arguments == null)
				{
					collection = null;
				}
				else
				{
					collection = arguments as ICollection<Expression>;
					if (collection == null)
					{
						collection = (arguments = arguments.ToReadOnly<Expression>());
					}
				}
				if (this.SameArguments(collection))
				{
					return this;
				}
			}
			return Expression.Call(@object, this.Method, arguments);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual bool SameArguments(ICollection<Expression> arguments)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			throw ContractUtils.Unreachable;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x0600135D RID: 4957 RVA: 0x0003BF79 File Offset: 0x0003A179
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMethodCall(this);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public virtual Expression GetArgument(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public virtual int ArgumentCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0000220F File Offset: 0x0000040F
		internal MethodCallExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
