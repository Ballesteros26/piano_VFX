using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002E9 RID: 745
	internal sealed class VariableBinder : ExpressionVisitor
	{
		// Token: 0x060016BD RID: 5821 RVA: 0x0004A8D9 File Offset: 0x00048AD9
		internal static AnalyzedTree Bind(LambdaExpression lambda)
		{
			VariableBinder variableBinder = new VariableBinder();
			variableBinder.Visit(lambda);
			return variableBinder._tree;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0004A8ED File Offset: 0x00048AED
		private VariableBinder()
		{
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0004A924 File Offset: 0x00048B24
		public override Expression Visit(Expression node)
		{
			if (!this._guard.TryEnterOnCurrentStack())
			{
				return this._guard.RunOnEmptyStack<VariableBinder, Expression, Expression>((VariableBinder @this, Expression e) => @this.Visit(e), this, node);
			}
			return base.Visit(node);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0004A972 File Offset: 0x00048B72
		protected internal override Expression VisitConstant(ConstantExpression node)
		{
			if (this._inQuote)
			{
				return node;
			}
			if (ILGen.CanEmitConstant(node.Value, node.Type))
			{
				return node;
			}
			this._constants.Peek().AddReference(node.Value, node.Type);
			return node;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0004A9B0 File Offset: 0x00048BB0
		protected internal override Expression VisitUnary(UnaryExpression node)
		{
			if (node.NodeType == ExpressionType.Quote)
			{
				bool inQuote = this._inQuote;
				this._inQuote = true;
				this.Visit(node.Operand);
				this._inQuote = inQuote;
			}
			else
			{
				this.Visit(node.Operand);
			}
			return node;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0004A9FC File Offset: 0x00048BFC
		protected internal override Expression VisitLambda<T>(Expression<T> node)
		{
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, true));
			this._constants.Push(this._tree.Constants[node] = new BoundConstants());
			base.Visit(this.MergeScopes(node));
			this._constants.Pop();
			this._scopes.Pop();
			return node;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0004AA7C File Offset: 0x00048C7C
		protected internal override Expression VisitInvocation(InvocationExpression node)
		{
			LambdaExpression lambdaOperand = node.LambdaOperand;
			if (lambdaOperand != null)
			{
				this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(lambdaOperand, false));
				base.Visit(this.MergeScopes(lambdaOperand));
				this._scopes.Pop();
				int i = 0;
				int argumentCount = node.ArgumentCount;
				while (i < argumentCount)
				{
					this.Visit(node.GetArgument(i));
					i++;
				}
				return node;
			}
			return base.VisitInvocation(node);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0004AB00 File Offset: 0x00048D00
		protected internal override Expression VisitBlock(BlockExpression node)
		{
			if (node.Variables.Count == 0)
			{
				base.Visit(node.Expressions);
				return node;
			}
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, false));
			base.Visit(this.MergeScopes(node));
			this._scopes.Pop();
			return node;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0004AB6C File Offset: 0x00048D6C
		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			if (node.Variable == null)
			{
				this.Visit(node.Filter);
				this.Visit(node.Body);
				return node;
			}
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, false));
			this.Visit(node.Filter);
			this.Visit(node.Body);
			this._scopes.Pop();
			return node;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0004ABEC File Offset: 0x00048DEC
		private ReadOnlyCollection<Expression> MergeScopes(Expression node)
		{
			LambdaExpression lambdaExpression = node as LambdaExpression;
			ReadOnlyCollection<Expression> readOnlyCollection;
			if (lambdaExpression != null)
			{
				readOnlyCollection = new ReadOnlyCollection<Expression>(new Expression[] { lambdaExpression.Body });
			}
			else
			{
				readOnlyCollection = ((BlockExpression)node).Expressions;
			}
			CompilerScope compilerScope = this._scopes.Peek();
			while (readOnlyCollection.Count == 1 && readOnlyCollection[0].NodeType == ExpressionType.Block)
			{
				BlockExpression blockExpression = (BlockExpression)readOnlyCollection[0];
				if (blockExpression.Variables.Count > 0)
				{
					foreach (ParameterExpression parameterExpression in blockExpression.Variables)
					{
						if (compilerScope.Definitions.ContainsKey(parameterExpression))
						{
							return readOnlyCollection;
						}
					}
					if (compilerScope.MergedScopes == null)
					{
						compilerScope.MergedScopes = new HashSet<BlockExpression>(global::System.Collections.Generic.ReferenceEqualityComparer<object>.Instance);
					}
					compilerScope.MergedScopes.Add(blockExpression);
					foreach (ParameterExpression parameterExpression2 in blockExpression.Variables)
					{
						compilerScope.Definitions.Add(parameterExpression2, VariableStorageKind.Local);
					}
				}
				readOnlyCollection = blockExpression.Expressions;
			}
			return readOnlyCollection;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0004AD40 File Offset: 0x00048F40
		protected internal override Expression VisitParameter(ParameterExpression node)
		{
			this.Reference(node, VariableStorageKind.Local);
			CompilerScope compilerScope = null;
			foreach (CompilerScope compilerScope2 in this._scopes)
			{
				if (compilerScope2.IsMethod || compilerScope2.Definitions.ContainsKey(node))
				{
					compilerScope = compilerScope2;
					break;
				}
			}
			if (compilerScope.ReferenceCount == null)
			{
				compilerScope.ReferenceCount = new Dictionary<ParameterExpression, int>();
			}
			Helpers.IncrementCount<ParameterExpression>(node, compilerScope.ReferenceCount);
			return node;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0004ADD0 File Offset: 0x00048FD0
		protected internal override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			foreach (ParameterExpression parameterExpression in node.Variables)
			{
				this.Reference(parameterExpression, VariableStorageKind.Hoisted);
			}
			return node;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0004AE20 File Offset: 0x00049020
		private void Reference(ParameterExpression node, VariableStorageKind storage)
		{
			CompilerScope compilerScope = null;
			foreach (CompilerScope compilerScope2 in this._scopes)
			{
				if (compilerScope2.Definitions.ContainsKey(node))
				{
					compilerScope = compilerScope2;
					break;
				}
				compilerScope2.NeedsClosure = true;
				if (compilerScope2.IsMethod)
				{
					storage = VariableStorageKind.Hoisted;
				}
			}
			if (compilerScope == null)
			{
				throw Error.UndefinedVariable(node.Name, node.Type, this.CurrentLambdaName);
			}
			if (storage == VariableStorageKind.Hoisted)
			{
				if (node.IsByRef)
				{
					throw Error.CannotCloseOverByRef(node.Name, this.CurrentLambdaName);
				}
				compilerScope.Definitions[node] = VariableStorageKind.Hoisted;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0004AED8 File Offset: 0x000490D8
		private string CurrentLambdaName
		{
			get
			{
				foreach (CompilerScope compilerScope in this._scopes)
				{
					LambdaExpression lambdaExpression = compilerScope.Node as LambdaExpression;
					if (lambdaExpression != null)
					{
						return lambdaExpression.Name;
					}
				}
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x04000A9E RID: 2718
		private readonly AnalyzedTree _tree = new AnalyzedTree();

		// Token: 0x04000A9F RID: 2719
		private readonly Stack<CompilerScope> _scopes = new Stack<CompilerScope>();

		// Token: 0x04000AA0 RID: 2720
		private readonly Stack<BoundConstants> _constants = new Stack<BoundConstants>();

		// Token: 0x04000AA1 RID: 2721
		private readonly StackGuard _guard = new StackGuard();

		// Token: 0x04000AA2 RID: 2722
		private bool _inQuote;
	}
}
