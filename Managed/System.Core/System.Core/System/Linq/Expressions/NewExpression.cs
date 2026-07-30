using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a constructor call.</summary>
	// Token: 0x020002A6 RID: 678
	[DebuggerTypeProxy(typeof(Expression.NewExpressionProxy))]
	public class NewExpression : Expression, IArgumentProvider
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x0003CB10 File Offset: 0x0003AD10
		internal NewExpression(ConstructorInfo constructor, IReadOnlyList<Expression> arguments, ReadOnlyCollection<MemberInfo> members)
		{
			this.Constructor = constructor;
			this._arguments = arguments;
			this.Members = members;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.NewExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0003CB2D File Offset: 0x0003AD2D
		public override Type Type
		{
			get
			{
				return this.Constructor.DeclaringType;
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0003CB3A File Offset: 0x0003AD3A
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.New;
			}
		}

		/// <summary>Gets the called constructor.</summary>
		/// <returns>The <see cref="T:System.Reflection.ConstructorInfo" /> that represents the called constructor.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0003CB3E File Offset: 0x0003AD3E
		public ConstructorInfo Constructor { get; }

		/// <summary>Gets the arguments to the constructor.</summary>
		/// <returns>A collection of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments to the constructor.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0003CB46 File Offset: 0x0003AD46
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0003CB53 File Offset: 0x0003AD53
		public Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0003CB61 File Offset: 0x0003AD61
		public int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		/// <summary>Gets the members that can retrieve the values of the fields that were initialized with constructor arguments.</summary>
		/// <returns>A collection of <see cref="T:System.Reflection.MemberInfo" /> objects that represent the members that can retrieve the values of the fields that were initialized with constructor arguments.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0003CB6E File Offset: 0x0003AD6E
		public ReadOnlyCollection<MemberInfo> Members { get; }

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x060013BF RID: 5055 RVA: 0x0003CB76 File Offset: 0x0003AD76
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitNew(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> property of the result.</param>
		// Token: 0x060013C0 RID: 5056 RVA: 0x0003CB7F File Offset: 0x0003AD7F
		public NewExpression Update(IEnumerable<Expression> arguments)
		{
			if (ExpressionUtils.SameElements<Expression>(ref arguments, this.Arguments))
			{
				return this;
			}
			if (this.Members == null)
			{
				return Expression.New(this.Constructor, arguments);
			}
			return Expression.New(this.Constructor, arguments, this.Members);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0000220F File Offset: 0x0000040F
		internal NewExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040009B3 RID: 2483
		private IReadOnlyList<Expression> _arguments;
	}
}
