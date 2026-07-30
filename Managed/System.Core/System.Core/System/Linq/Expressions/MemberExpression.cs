using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents accessing a field or property.</summary>
	// Token: 0x0200028F RID: 655
	[DebuggerTypeProxy(typeof(Expression.MemberExpressionProxy))]
	public class MemberExpression : Expression
	{
		/// <summary>Gets the field or property to be accessed.</summary>
		/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> that represents the field or property to be accessed.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0003BBC0 File Offset: 0x00039DC0
		public MemberInfo Member
		{
			get
			{
				return this.GetMember();
			}
		}

		/// <summary>Gets the containing object of the field or property.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the containing object of the field or property.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0003BBC8 File Offset: 0x00039DC8
		public Expression Expression { get; }

		// Token: 0x0600132D RID: 4909 RVA: 0x0003BBD0 File Offset: 0x00039DD0
		internal MemberExpression(Expression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0003BBDF File Offset: 0x00039DDF
		internal static PropertyExpression Make(Expression expression, PropertyInfo property)
		{
			return new PropertyExpression(expression, property);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0003BBE8 File Offset: 0x00039DE8
		internal static FieldExpression Make(Expression expression, FieldInfo field)
		{
			return new FieldExpression(expression, field);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0003BBF4 File Offset: 0x00039DF4
		internal static MemberExpression Make(Expression expression, MemberInfo member)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (!(fieldInfo == null))
			{
				return MemberExpression.Make(expression, fieldInfo);
			}
			return MemberExpression.Make(expression, (PropertyInfo)member);
		}

		/// <summary>Returns the node type of this <see cref="P:System.Linq.Expressions.MemberExpression.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0003BC25 File Offset: 0x00039E25
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.MemberAccess;
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual MemberInfo GetMember()
		{
			throw ContractUtils.Unreachable;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x06001333 RID: 4915 RVA: 0x0003BC29 File Offset: 0x00039E29
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMember(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property of the result.</param>
		// Token: 0x06001334 RID: 4916 RVA: 0x0003BC32 File Offset: 0x00039E32
		public MemberExpression Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			return Expression.MakeMemberAccess(expression, this.Member);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0000220F File Offset: 0x0000040F
		internal MemberExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
