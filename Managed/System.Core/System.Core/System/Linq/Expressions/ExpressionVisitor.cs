using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	/// <summary>Represents a visitor or rewriter for expression trees.</summary>
	// Token: 0x02000271 RID: 625
	public abstract class ExpressionVisitor
	{
		/// <summary>Dispatches the expression to one of the more specialized visit methods in this class.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001242 RID: 4674 RVA: 0x0003A621 File Offset: 0x00038821
		public virtual Expression Visit(Expression node)
		{
			if (node == null)
			{
				return null;
			}
			return node.Accept(this);
		}

		/// <summary>Dispatches the list of expressions to one of the more specialized visit methods in this class.</summary>
		/// <returns>The modified expression list, if any one of the elements were modified; otherwise, returns the original expression list.</returns>
		/// <param name="nodes">The expressions to visit.</param>
		// Token: 0x06001243 RID: 4675 RVA: 0x0003A630 File Offset: 0x00038830
		public ReadOnlyCollection<Expression> Visit(ReadOnlyCollection<Expression> nodes)
		{
			ContractUtils.RequiresNotNull(nodes, "nodes");
			Expression[] array = null;
			int i = 0;
			int count = nodes.Count;
			while (i < count)
			{
				Expression expression = this.Visit(nodes[i]);
				if (array != null)
				{
					array[i] = expression;
				}
				else if (expression != nodes[i])
				{
					array = new Expression[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes[j];
					}
					array[i] = expression;
				}
				i++;
			}
			if (array == null)
			{
				return nodes;
			}
			return new TrueReadOnlyCollection<Expression>(array);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0003A6B0 File Offset: 0x000388B0
		private Expression[] VisitArguments(IArgumentProvider nodes)
		{
			return ExpressionVisitorUtils.VisitArguments(this, nodes);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0003A6B9 File Offset: 0x000388B9
		private ParameterExpression[] VisitParameters(IParameterProvider nodes, string callerName)
		{
			return ExpressionVisitorUtils.VisitParameters(this, nodes, callerName);
		}

		/// <summary>Visits all nodes in the collection using a specified element visitor.</summary>
		/// <returns>The modified node list, if any of the elements were modified; otherwise, returns the original node list.</returns>
		/// <param name="nodes">The nodes to visit.</param>
		/// <param name="elementVisitor">A delegate that visits a single element, optionally replacing it with a new element.</param>
		/// <typeparam name="T">The type of the nodes.</typeparam>
		// Token: 0x06001246 RID: 4678 RVA: 0x0003A6C4 File Offset: 0x000388C4
		public static ReadOnlyCollection<T> Visit<T>(ReadOnlyCollection<T> nodes, Func<T, T> elementVisitor)
		{
			ContractUtils.RequiresNotNull(nodes, "nodes");
			ContractUtils.RequiresNotNull(elementVisitor, "elementVisitor");
			T[] array = null;
			int i = 0;
			int count = nodes.Count;
			while (i < count)
			{
				T t = elementVisitor(nodes[i]);
				if (array != null)
				{
					array[i] = t;
				}
				else if (t != nodes[i])
				{
					array = new T[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes[j];
					}
					array[i] = t;
				}
				i++;
			}
			if (array == null)
			{
				return nodes;
			}
			return new TrueReadOnlyCollection<T>(array);
		}

		/// <summary>Visits an expression, casting the result back to the original expression type.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		/// <param name="callerName">The name of the calling method; used to report to report a better error message.</param>
		/// <typeparam name="T">The type of the expression.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The visit method for this node returned a different type.</exception>
		// Token: 0x06001247 RID: 4679 RVA: 0x0003A768 File Offset: 0x00038968
		public T VisitAndConvert<T>(T node, string callerName) where T : Expression
		{
			if (node == null)
			{
				return default(T);
			}
			node = this.Visit(node) as T;
			if (node == null)
			{
				throw Error.MustRewriteToSameNode(callerName, typeof(T), callerName);
			}
			return node;
		}

		/// <summary>Visits an expression, casting the result back to the original expression type.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="nodes">The expression to visit.</param>
		/// <param name="callerName">The name of the calling method; used to report to report a better error message.</param>
		/// <typeparam name="T">The type of the expression.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The visit method for this node returned a different type.</exception>
		// Token: 0x06001248 RID: 4680 RVA: 0x0003A7BC File Offset: 0x000389BC
		public ReadOnlyCollection<T> VisitAndConvert<T>(ReadOnlyCollection<T> nodes, string callerName) where T : Expression
		{
			ContractUtils.RequiresNotNull(nodes, "nodes");
			T[] array = null;
			int i = 0;
			int count = nodes.Count;
			while (i < count)
			{
				T t = this.Visit(nodes[i]) as T;
				if (t == null)
				{
					throw Error.MustRewriteToSameNode(callerName, typeof(T), callerName);
				}
				if (array != null)
				{
					array[i] = t;
				}
				else if (t != nodes[i])
				{
					array = new T[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes[j];
					}
					array[i] = t;
				}
				i++;
			}
			if (array == null)
			{
				return nodes;
			}
			return new TrueReadOnlyCollection<T>(array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.BinaryExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001249 RID: 4681 RVA: 0x0003A881 File Offset: 0x00038A81
		protected internal virtual Expression VisitBinary(BinaryExpression node)
		{
			return ExpressionVisitor.ValidateBinary(node, node.Update(this.Visit(node.Left), this.VisitAndConvert<LambdaExpression>(node.Conversion, "VisitBinary"), this.Visit(node.Right)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.BlockExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600124A RID: 4682 RVA: 0x0003A8B8 File Offset: 0x00038AB8
		protected internal virtual Expression VisitBlock(BlockExpression node)
		{
			Expression[] array = ExpressionVisitorUtils.VisitBlockExpressions(this, node);
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = this.VisitAndConvert<ParameterExpression>(node.Variables, "VisitBlock");
			if (readOnlyCollection == node.Variables && array == null)
			{
				return node;
			}
			return node.Rewrite(readOnlyCollection, array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.ConditionalExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600124B RID: 4683 RVA: 0x0003A8F5 File Offset: 0x00038AF5
		protected internal virtual Expression VisitConditional(ConditionalExpression node)
		{
			return node.Update(this.Visit(node.Test), this.Visit(node.IfTrue), this.Visit(node.IfFalse));
		}

		/// <summary>Visits the <see cref="T:System.Linq.Expressions.ConstantExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600124C RID: 4684 RVA: 0x000021A3 File Offset: 0x000003A3
		protected internal virtual Expression VisitConstant(ConstantExpression node)
		{
			return node;
		}

		/// <summary>Visits the <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600124D RID: 4685 RVA: 0x000021A3 File Offset: 0x000003A3
		protected internal virtual Expression VisitDebugInfo(DebugInfoExpression node)
		{
			return node;
		}

		/// <summary>Visits the <see cref="T:System.Linq.Expressions.DefaultExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600124E RID: 4686 RVA: 0x000021A3 File Offset: 0x000003A3
		protected internal virtual Expression VisitDefault(DefaultExpression node)
		{
			return node;
		}

		/// <summary>Visits the children of the extension expression.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600124F RID: 4687 RVA: 0x0003A921 File Offset: 0x00038B21
		protected internal virtual Expression VisitExtension(Expression node)
		{
			return node.VisitChildren(this);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.GotoExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001250 RID: 4688 RVA: 0x0003A92A File Offset: 0x00038B2A
		protected internal virtual Expression VisitGoto(GotoExpression node)
		{
			return node.Update(this.VisitLabelTarget(node.Target), this.Visit(node.Value));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.InvocationExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001251 RID: 4689 RVA: 0x0003A94C File Offset: 0x00038B4C
		protected internal virtual Expression VisitInvocation(InvocationExpression node)
		{
			Expression expression = this.Visit(node.Expression);
			Expression[] array = this.VisitArguments(node);
			if (expression == node.Expression && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		/// <summary>Visits the <see cref="T:System.Linq.Expressions.LabelTarget" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001252 RID: 4690 RVA: 0x000021A3 File Offset: 0x000003A3
		protected virtual LabelTarget VisitLabelTarget(LabelTarget node)
		{
			return node;
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.LabelExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001253 RID: 4691 RVA: 0x0003A984 File Offset: 0x00038B84
		protected internal virtual Expression VisitLabel(LabelExpression node)
		{
			return node.Update(this.VisitLabelTarget(node.Target), this.Visit(node.DefaultValue));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.Expression`1" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		/// <typeparam name="T">The type of the delegate.</typeparam>
		// Token: 0x06001254 RID: 4692 RVA: 0x0003A9A4 File Offset: 0x00038BA4
		protected internal virtual Expression VisitLambda<T>(Expression<T> node)
		{
			Expression expression = this.Visit(node.Body);
			ParameterExpression[] array = this.VisitParameters(node, "VisitLambda");
			if (expression == node.Body && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.LoopExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001255 RID: 4693 RVA: 0x0003A9E1 File Offset: 0x00038BE1
		protected internal virtual Expression VisitLoop(LoopExpression node)
		{
			return node.Update(this.VisitLabelTarget(node.BreakLabel), this.VisitLabelTarget(node.ContinueLabel), this.Visit(node.Body));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001256 RID: 4694 RVA: 0x0003AA0D File Offset: 0x00038C0D
		protected internal virtual Expression VisitMember(MemberExpression node)
		{
			return node.Update(this.Visit(node.Expression));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.IndexExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001257 RID: 4695 RVA: 0x0003AA24 File Offset: 0x00038C24
		protected internal virtual Expression VisitIndex(IndexExpression node)
		{
			Expression expression = this.Visit(node.Object);
			Expression[] array = this.VisitArguments(node);
			if (expression == node.Object && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MethodCallExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001258 RID: 4696 RVA: 0x0003AA5C File Offset: 0x00038C5C
		protected internal virtual Expression VisitMethodCall(MethodCallExpression node)
		{
			Expression expression = this.Visit(node.Object);
			Expression[] array = this.VisitArguments(node);
			if (expression == node.Object && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.NewArrayExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001259 RID: 4697 RVA: 0x0003AA94 File Offset: 0x00038C94
		protected internal virtual Expression VisitNewArray(NewArrayExpression node)
		{
			return node.Update(this.Visit(node.Expressions));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.NewExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600125A RID: 4698 RVA: 0x0003AAA8 File Offset: 0x00038CA8
		protected internal virtual Expression VisitNew(NewExpression node)
		{
			Expression[] array = this.VisitArguments(node);
			if (array == null)
			{
				return node;
			}
			return node.Update(array);
		}

		/// <summary>Visits the <see cref="T:System.Linq.Expressions.ParameterExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600125B RID: 4699 RVA: 0x000021A3 File Offset: 0x000003A3
		protected internal virtual Expression VisitParameter(ParameterExpression node)
		{
			return node;
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600125C RID: 4700 RVA: 0x0003AAC9 File Offset: 0x00038CC9
		protected internal virtual Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			return node.Update(this.VisitAndConvert<ParameterExpression>(node.Variables, "VisitRuntimeVariables"));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.SwitchCase" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600125D RID: 4701 RVA: 0x0003AAE2 File Offset: 0x00038CE2
		protected virtual SwitchCase VisitSwitchCase(SwitchCase node)
		{
			return node.Update(this.Visit(node.TestValues), this.Visit(node.Body));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.SwitchExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600125E RID: 4702 RVA: 0x0003AB02 File Offset: 0x00038D02
		protected internal virtual Expression VisitSwitch(SwitchExpression node)
		{
			return ExpressionVisitor.ValidateSwitch(node, node.Update(this.Visit(node.SwitchValue), ExpressionVisitor.Visit<SwitchCase>(node.Cases, new Func<SwitchCase, SwitchCase>(this.VisitSwitchCase)), this.Visit(node.DefaultBody)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.CatchBlock" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600125F RID: 4703 RVA: 0x0003AB40 File Offset: 0x00038D40
		protected virtual CatchBlock VisitCatchBlock(CatchBlock node)
		{
			return node.Update(this.VisitAndConvert<ParameterExpression>(node.Variable, "VisitCatchBlock"), this.Visit(node.Filter), this.Visit(node.Body));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.TryExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001260 RID: 4704 RVA: 0x0003AB74 File Offset: 0x00038D74
		protected internal virtual Expression VisitTry(TryExpression node)
		{
			return node.Update(this.Visit(node.Body), ExpressionVisitor.Visit<CatchBlock>(node.Handlers, new Func<CatchBlock, CatchBlock>(this.VisitCatchBlock)), this.Visit(node.Finally), this.Visit(node.Fault));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.TypeBinaryExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001261 RID: 4705 RVA: 0x0003ABC3 File Offset: 0x00038DC3
		protected internal virtual Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			return node.Update(this.Visit(node.Expression));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.UnaryExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001262 RID: 4706 RVA: 0x0003ABD7 File Offset: 0x00038DD7
		protected internal virtual Expression VisitUnary(UnaryExpression node)
		{
			return ExpressionVisitor.ValidateUnary(node, node.Update(this.Visit(node.Operand)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberInitExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001263 RID: 4707 RVA: 0x0003ABF1 File Offset: 0x00038DF1
		protected internal virtual Expression VisitMemberInit(MemberInitExpression node)
		{
			return node.Update(this.VisitAndConvert<NewExpression>(node.NewExpression, "VisitMemberInit"), ExpressionVisitor.Visit<MemberBinding>(node.Bindings, new Func<MemberBinding, MemberBinding>(this.VisitMemberBinding)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.ListInitExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001264 RID: 4708 RVA: 0x0003AC22 File Offset: 0x00038E22
		protected internal virtual Expression VisitListInit(ListInitExpression node)
		{
			return node.Update(this.VisitAndConvert<NewExpression>(node.NewExpression, "VisitListInit"), ExpressionVisitor.Visit<ElementInit>(node.Initializers, new Func<ElementInit, ElementInit>(this.VisitElementInit)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.ElementInit" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001265 RID: 4709 RVA: 0x0003AC53 File Offset: 0x00038E53
		protected virtual ElementInit VisitElementInit(ElementInit node)
		{
			return node.Update(this.Visit(node.Arguments));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001266 RID: 4710 RVA: 0x0003AC68 File Offset: 0x00038E68
		protected virtual MemberBinding VisitMemberBinding(MemberBinding node)
		{
			switch (node.BindingType)
			{
			case MemberBindingType.Assignment:
				return this.VisitMemberAssignment((MemberAssignment)node);
			case MemberBindingType.MemberBinding:
				return this.VisitMemberMemberBinding((MemberMemberBinding)node);
			case MemberBindingType.ListBinding:
				return this.VisitMemberListBinding((MemberListBinding)node);
			default:
				throw Error.UnhandledBindingType(node.BindingType);
			}
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberAssignment" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001267 RID: 4711 RVA: 0x0003ACC7 File Offset: 0x00038EC7
		protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment node)
		{
			return node.Update(this.Visit(node.Expression));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberMemberBinding" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001268 RID: 4712 RVA: 0x0003ACDB File Offset: 0x00038EDB
		protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
		{
			return node.Update(ExpressionVisitor.Visit<MemberBinding>(node.Bindings, new Func<MemberBinding, MemberBinding>(this.VisitMemberBinding)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberListBinding" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06001269 RID: 4713 RVA: 0x0003ACFB File Offset: 0x00038EFB
		protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding node)
		{
			return node.Update(ExpressionVisitor.Visit<ElementInit>(node.Initializers, new Func<ElementInit, ElementInit>(this.VisitElementInit)));
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0003AD1C File Offset: 0x00038F1C
		private static UnaryExpression ValidateUnary(UnaryExpression before, UnaryExpression after)
		{
			if (before != after && before.Method == null)
			{
				if (after.Method != null)
				{
					throw Error.MustRewriteWithoutMethod(after.Method, "VisitUnary");
				}
				if (before.Operand != null && after.Operand != null)
				{
					ExpressionVisitor.ValidateChildType(before.Operand.Type, after.Operand.Type, "VisitUnary");
				}
			}
			return after;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0003AD8C File Offset: 0x00038F8C
		private static BinaryExpression ValidateBinary(BinaryExpression before, BinaryExpression after)
		{
			if (before != after && before.Method == null)
			{
				if (after.Method != null)
				{
					throw Error.MustRewriteWithoutMethod(after.Method, "VisitBinary");
				}
				ExpressionVisitor.ValidateChildType(before.Left.Type, after.Left.Type, "VisitBinary");
				ExpressionVisitor.ValidateChildType(before.Right.Type, after.Right.Type, "VisitBinary");
			}
			return after;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0003AE0B File Offset: 0x0003900B
		private static SwitchExpression ValidateSwitch(SwitchExpression before, SwitchExpression after)
		{
			if (before.Comparison == null && after.Comparison != null)
			{
				throw Error.MustRewriteWithoutMethod(after.Comparison, "VisitSwitch");
			}
			return after;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0003AE3B File Offset: 0x0003903B
		private static void ValidateChildType(Type before, Type after, string methodName)
		{
			if (before.IsValueType)
			{
				if (TypeUtils.AreEquivalent(before, after))
				{
					return;
				}
			}
			else if (!after.IsValueType)
			{
				return;
			}
			throw Error.MustRewriteChildToSameType(before, after, methodName);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.DynamicExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600126E RID: 4718 RVA: 0x0003AE60 File Offset: 0x00039060
		protected internal virtual Expression VisitDynamic(DynamicExpression node)
		{
			Expression[] array = this.VisitArguments(node);
			if (array == null)
			{
				return node;
			}
			return node.Rewrite(array);
		}
	}
}
