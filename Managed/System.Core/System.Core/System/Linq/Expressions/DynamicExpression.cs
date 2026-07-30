using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a dynamic operation.</summary>
	// Token: 0x02000260 RID: 608
	public class DynamicExpression : Expression, IDynamicExpression, IArgumentProvider
	{
		// Token: 0x060010EC RID: 4332 RVA: 0x000379B9 File Offset: 0x00035BB9
		internal DynamicExpression(Type delegateType, CallSiteBinder binder)
		{
			this.DelegateType = delegateType;
			this.Binder = binder;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0000AA13 File Offset: 0x00008C13
		public override bool CanReduce
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000379D0 File Offset: 0x00035BD0
		public override Expression Reduce()
		{
			ConstantExpression constantExpression = Expression.Constant(CallSite.Create(this.DelegateType, this.Binder));
			return Expression.Invoke(Expression.Field(constantExpression, "Target"), this.Arguments.AddFirst(constantExpression));
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00037A10 File Offset: 0x00035C10
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, ReadOnlyCollection<Expression> arguments)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpressionN(delegateType, binder, arguments);
			}
			return new TypedDynamicExpressionN(returnType, delegateType, binder, arguments);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00037A36 File Offset: 0x00035C36
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, Expression arg0)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpression1(delegateType, binder, arg0);
			}
			return new TypedDynamicExpression1(returnType, delegateType, binder, arg0);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00037A5C File Offset: 0x00035C5C
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpression2(delegateType, binder, arg0, arg1);
			}
			return new TypedDynamicExpression2(returnType, delegateType, binder, arg0, arg1);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00037A86 File Offset: 0x00035C86
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpression3(delegateType, binder, arg0, arg1, arg2);
			}
			return new TypedDynamicExpression3(returnType, delegateType, binder, arg0, arg1, arg2);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00037AB4 File Offset: 0x00035CB4
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpression4(delegateType, binder, arg0, arg1, arg2, arg3);
			}
			return new TypedDynamicExpression4(returnType, delegateType, binder, arg0, arg1, arg2, arg3);
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.DynamicExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public override Type Type
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00037AF2 File Offset: 0x00035CF2
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Dynamic;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />, which determines the run-time behavior of the dynamic site.</summary>
		/// <returns>The <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />, which determines the run-time behavior of the dynamic site.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x00037AF6 File Offset: 0x00035CF6
		public CallSiteBinder Binder { get; }

		/// <summary>Gets the type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00037AFE File Offset: 0x00035CFE
		public Type DelegateType { get; }

		/// <summary>Gets the arguments to the dynamic operation.</summary>
		/// <returns>The read-only collections containing the arguments to the dynamic operation.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00037B06 File Offset: 0x00035D06
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.GetOrMakeArguments();
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			throw ContractUtils.Unreachable;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x060010FA RID: 4346 RVA: 0x00037B10 File Offset: 0x00035D10
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			DynamicExpressionVisitor dynamicExpressionVisitor = visitor as DynamicExpressionVisitor;
			if (dynamicExpressionVisitor != null)
			{
				return dynamicExpressionVisitor.VisitDynamic(this);
			}
			return base.Accept(visitor);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual DynamicExpression Rewrite(Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}

		/// <summary>Compares the value sent to the parameter, arguments, to the Arguments property of the current instance of DynamicExpression. If the values of the parameter and the property are equal, the current instance is returned. If they are not equal, a new DynamicExpression instance is returned that is identical to the current instance except that the Arguments property is set to the value of parameter arguments. </summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> property of the result.</param>
		// Token: 0x060010FC RID: 4348 RVA: 0x00037B38 File Offset: 0x00035D38
		public DynamicExpression Update(IEnumerable<Expression> arguments)
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
			return ExpressionExtension.MakeDynamic(this.DelegateType, this.Binder, arguments);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual bool SameArguments(ICollection<Expression> arguments)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		Expression IArgumentProvider.GetArgument(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		// Token: 0x06001100 RID: 4352 RVA: 0x00037B7D File Offset: 0x00035D7D
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, params Expression[] arguments)
		{
			return ExpressionExtension.Dynamic(binder, returnType, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />,  and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		// Token: 0x06001101 RID: 4353 RVA: 0x00037B87 File Offset: 0x00035D87
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, IEnumerable<Expression> arguments)
		{
			return ExpressionExtension.Dynamic(binder, returnType, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />,  and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		// Token: 0x06001102 RID: 4354 RVA: 0x00037B91 File Offset: 0x00035D91
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0)
		{
			return ExpressionExtension.Dynamic(binder, returnType, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		// Token: 0x06001103 RID: 4355 RVA: 0x00037B9B File Offset: 0x00035D9B
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1)
		{
			return ExpressionExtension.Dynamic(binder, returnType, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		// Token: 0x06001104 RID: 4356 RVA: 0x00037BA6 File Offset: 0x00035DA6
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2)
		{
			return ExpressionExtension.Dynamic(binder, returnType, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		/// <param name="arg3">The fourth argument to the dynamic operation.</param>
		// Token: 0x06001105 RID: 4357 RVA: 0x00037BB3 File Offset: 0x00035DB3
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			return ExpressionExtension.Dynamic(binder, returnType, arg0, arg1, arg2, arg3);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		// Token: 0x06001106 RID: 4358 RVA: 0x00037BC2 File Offset: 0x00035DC2
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, IEnumerable<Expression> arguments)
		{
			return ExpressionExtension.MakeDynamic(delegateType, binder, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		// Token: 0x06001107 RID: 4359 RVA: 0x00037BCC File Offset: 0x00035DCC
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, params Expression[] arguments)
		{
			return ExpressionExtension.MakeDynamic(delegateType, binder, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and one argument.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The argument to the dynamic operation.</param>
		// Token: 0x06001108 RID: 4360 RVA: 0x00037BD6 File Offset: 0x00035DD6
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0)
		{
			return ExpressionExtension.MakeDynamic(delegateType, binder, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and two arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		// Token: 0x06001109 RID: 4361 RVA: 0x00037BE0 File Offset: 0x00035DE0
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
		{
			return ExpressionExtension.MakeDynamic(delegateType, binder, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and three arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		// Token: 0x0600110A RID: 4362 RVA: 0x00037BEB File Offset: 0x00035DEB
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
		{
			return ExpressionExtension.MakeDynamic(delegateType, binder, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and four arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.DynamicExpression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" />, and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		/// <param name="arg3">The fourth argument to the dynamic operation.</param>
		// Token: 0x0600110B RID: 4363 RVA: 0x00037BF8 File Offset: 0x00035DF8
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			return ExpressionExtension.MakeDynamic(delegateType, binder, arg0, arg1, arg2, arg3);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00037C07 File Offset: 0x00035E07
		Expression IDynamicExpression.Rewrite(Expression[] args)
		{
			return this.Rewrite(args);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00037C10 File Offset: 0x00035E10
		object IDynamicExpression.CreateCallSite()
		{
			return CallSite.Create(this.DelegateType, this.Binder);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0000220F File Offset: 0x0000040F
		internal DynamicExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
