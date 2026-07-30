using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a constructor call that has a collection initializer.</summary>
	// Token: 0x0200028A RID: 650
	[DebuggerTypeProxy(typeof(Expression.ListInitExpressionProxy))]
	public sealed class ListInitExpression : Expression
	{
		// Token: 0x0600130E RID: 4878 RVA: 0x0003BA51 File Offset: 0x00039C51
		internal ListInitExpression(NewExpression newExpression, ReadOnlyCollection<ElementInit> initializers)
		{
			this.NewExpression = newExpression;
			this.Initializers = initializers;
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x0003BA67 File Offset: 0x00039C67
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.ListInit;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ListInitExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0003BA6B File Offset: 0x00039C6B
		public sealed override Type Type
		{
			get
			{
				return this.NewExpression.Type;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if the node can be reduced, otherwise false.</returns>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x0000AA13 File Offset: 0x00008C13
		public override bool CanReduce
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the expression that contains a call to the constructor of a collection type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that represents the call to the constructor of a collection type.</returns>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0003BA78 File Offset: 0x00039C78
		public NewExpression NewExpression { get; }

		/// <summary>Gets the element initializers that are used to initialize a collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ElementInit" /> objects which represent the elements that are used to initialize the collection.</returns>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0003BA80 File Offset: 0x00039C80
		public ReadOnlyCollection<ElementInit> Initializers { get; }

		// Token: 0x06001314 RID: 4884 RVA: 0x0003BA88 File Offset: 0x00039C88
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitListInit(this);
		}

		/// <summary>Reduces the binary expression node to a simpler expression.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06001315 RID: 4885 RVA: 0x0003BA91 File Offset: 0x00039C91
		public override Expression Reduce()
		{
			return MemberInitExpression.ReduceListInit(this.NewExpression, this.Initializers, true);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="newExpression">The <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property of the result.</param>
		/// <param name="initializers">The <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> property of the result.</param>
		// Token: 0x06001316 RID: 4886 RVA: 0x0003BAA5 File Offset: 0x00039CA5
		public ListInitExpression Update(NewExpression newExpression, IEnumerable<ElementInit> initializers)
		{
			if (((newExpression == this.NewExpression) & (initializers != null)) && ExpressionUtils.SameElements<ElementInit>(ref initializers, this.Initializers))
			{
				return this;
			}
			return Expression.ListInit(newExpression, initializers);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0000220F File Offset: 0x0000040F
		internal ListInitExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
