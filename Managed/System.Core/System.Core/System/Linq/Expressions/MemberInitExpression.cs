using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents calling a constructor and initializing one or more members of the new object.</summary>
	// Token: 0x02000292 RID: 658
	[DebuggerTypeProxy(typeof(Expression.MemberInitExpressionProxy))]
	public sealed class MemberInitExpression : Expression
	{
		// Token: 0x0600133C RID: 4924 RVA: 0x0003BC95 File Offset: 0x00039E95
		internal MemberInitExpression(NewExpression newExpression, ReadOnlyCollection<MemberBinding> bindings)
		{
			this.NewExpression = newExpression;
			this.Bindings = bindings;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.MemberInitExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0003BCAB File Offset: 0x00039EAB
		public sealed override Type Type
		{
			get
			{
				return this.NewExpression.Type;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if the node can be reduced, otherwise false.</returns>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0000AA13 File Offset: 0x00008C13
		public override bool CanReduce
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0003BCB8 File Offset: 0x00039EB8
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.MemberInit;
			}
		}

		/// <summary>Gets the expression that represents the constructor call.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that represents the constructor call.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0003BCBC File Offset: 0x00039EBC
		public NewExpression NewExpression { get; }

		/// <summary>Gets the bindings that describe how to initialize the members of the newly created object.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects which describe how to initialize the members.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0003BCC4 File Offset: 0x00039EC4
		public ReadOnlyCollection<MemberBinding> Bindings { get; }

		// Token: 0x06001342 RID: 4930 RVA: 0x0003BCCC File Offset: 0x00039ECC
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMemberInit(this);
		}

		/// <summary>Reduces the <see cref="T:System.Linq.Expressions.MemberInitExpression" /> to a simpler expression. </summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06001343 RID: 4931 RVA: 0x0003BCD5 File Offset: 0x00039ED5
		public override Expression Reduce()
		{
			return MemberInitExpression.ReduceMemberInit(this.NewExpression, this.Bindings, true);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0003BCEC File Offset: 0x00039EEC
		private static Expression ReduceMemberInit(Expression objExpression, ReadOnlyCollection<MemberBinding> bindings, bool keepOnStack)
		{
			ParameterExpression parameterExpression = Expression.Variable(objExpression.Type);
			int count = bindings.Count;
			Expression[] array = new Expression[count + 2];
			array[0] = Expression.Assign(parameterExpression, objExpression);
			for (int i = 0; i < count; i++)
			{
				array[i + 1] = MemberInitExpression.ReduceMemberBinding(parameterExpression, bindings[i]);
			}
			array[count + 1] = (keepOnStack ? parameterExpression : Utils.Empty);
			return Expression.Block(new ParameterExpression[] { parameterExpression }, array);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0003BD60 File Offset: 0x00039F60
		internal static Expression ReduceListInit(Expression listExpression, ReadOnlyCollection<ElementInit> initializers, bool keepOnStack)
		{
			ParameterExpression parameterExpression = Expression.Variable(listExpression.Type);
			int count = initializers.Count;
			Expression[] array = new Expression[count + 2];
			array[0] = Expression.Assign(parameterExpression, listExpression);
			for (int i = 0; i < count; i++)
			{
				ElementInit elementInit = initializers[i];
				array[i + 1] = Expression.Call(parameterExpression, elementInit.AddMethod, elementInit.Arguments);
			}
			array[count + 1] = (keepOnStack ? parameterExpression : Utils.Empty);
			return Expression.Block(new ParameterExpression[] { parameterExpression }, array);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0003BDE4 File Offset: 0x00039FE4
		internal static Expression ReduceMemberBinding(ParameterExpression objVar, MemberBinding binding)
		{
			MemberExpression memberExpression = Expression.MakeMemberAccess(objVar, binding.Member);
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				return Expression.Assign(memberExpression, ((MemberAssignment)binding).Expression);
			case MemberBindingType.MemberBinding:
				return MemberInitExpression.ReduceMemberInit(memberExpression, ((MemberMemberBinding)binding).Bindings, false);
			case MemberBindingType.ListBinding:
				return MemberInitExpression.ReduceListInit(memberExpression, ((MemberListBinding)binding).Initializers, false);
			default:
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="newExpression">The <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> property of the result.</param>
		/// <param name="bindings">The <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> property of the result.</param>
		// Token: 0x06001347 RID: 4935 RVA: 0x0003BE56 File Offset: 0x0003A056
		public MemberInitExpression Update(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
		{
			if (((newExpression == this.NewExpression) & (bindings != null)) && ExpressionUtils.SameElements<MemberBinding>(ref bindings, this.Bindings))
			{
				return this;
			}
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0000220F File Offset: 0x0000040F
		internal MemberInitExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
