using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents a set of binding restrictions on the <see cref="T:System.Dynamic.DynamicMetaObject" /> under which the dynamic binding is valid.</summary>
	// Token: 0x02000306 RID: 774
	[DebuggerTypeProxy(typeof(BindingRestrictions.BindingRestrictionsProxy))]
	[DebuggerDisplay("{DebugView}")]
	public abstract class BindingRestrictions
	{
		// Token: 0x0600177F RID: 6015 RVA: 0x00002320 File Offset: 0x00000520
		private BindingRestrictions()
		{
		}

		// Token: 0x06001780 RID: 6016
		internal abstract Expression GetExpression();

		/// <summary>Merges the set of binding restrictions with the current binding restrictions.</summary>
		/// <returns>The new set of binding restrictions.</returns>
		/// <param name="restrictions">The set of restrictions with which to merge the current binding restrictions.</param>
		// Token: 0x06001781 RID: 6017 RVA: 0x0004CF54 File Offset: 0x0004B154
		public BindingRestrictions Merge(BindingRestrictions restrictions)
		{
			ContractUtils.RequiresNotNull(restrictions, "restrictions");
			if (this == BindingRestrictions.Empty)
			{
				return restrictions;
			}
			if (restrictions == BindingRestrictions.Empty)
			{
				return this;
			}
			return new BindingRestrictions.MergedRestriction(this, restrictions);
		}

		/// <summary>Creates the binding restriction that check the expression for runtime type identity.</summary>
		/// <returns>The new binding restrictions.</returns>
		/// <param name="expression">The expression to test.</param>
		/// <param name="type">The exact type to test.</param>
		// Token: 0x06001782 RID: 6018 RVA: 0x0004CF7C File Offset: 0x0004B17C
		public static BindingRestrictions GetTypeRestriction(Expression expression, Type type)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			return new BindingRestrictions.TypeRestriction(expression, type);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0004CF9B File Offset: 0x0004B19B
		internal static BindingRestrictions GetTypeRestriction(DynamicMetaObject obj)
		{
			if (obj.Value == null && obj.HasValue)
			{
				return BindingRestrictions.GetInstanceRestriction(obj.Expression, null);
			}
			return BindingRestrictions.GetTypeRestriction(obj.Expression, obj.LimitType);
		}

		/// <summary>Creates the binding restriction that checks the expression for object instance identity.</summary>
		/// <returns>The new binding restrictions.</returns>
		/// <param name="expression">The expression to test.</param>
		/// <param name="instance">The exact object instance to test.</param>
		// Token: 0x06001784 RID: 6020 RVA: 0x0004CFCB File Offset: 0x0004B1CB
		public static BindingRestrictions GetInstanceRestriction(Expression expression, object instance)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			return new BindingRestrictions.InstanceRestriction(expression, instance);
		}

		/// <summary>Creates the binding restriction that checks the expression for arbitrary immutable properties.</summary>
		/// <returns>The new binding restrictions.</returns>
		/// <param name="expression">The expression representing the restrictions.</param>
		// Token: 0x06001785 RID: 6021 RVA: 0x0004CFDF File Offset: 0x0004B1DF
		public static BindingRestrictions GetExpressionRestriction(Expression expression)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.Requires(expression.Type == typeof(bool), "expression");
			return new BindingRestrictions.CustomRestriction(expression);
		}

		/// <summary>Combines binding restrictions from the list of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances into one set of restrictions.</summary>
		/// <returns>The new set of binding restrictions.</returns>
		/// <param name="contributingObjects">The list of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances from which to combine restrictions.</param>
		// Token: 0x06001786 RID: 6022 RVA: 0x0004D014 File Offset: 0x0004B214
		public static BindingRestrictions Combine(IList<DynamicMetaObject> contributingObjects)
		{
			BindingRestrictions bindingRestrictions = BindingRestrictions.Empty;
			if (contributingObjects != null)
			{
				foreach (DynamicMetaObject dynamicMetaObject in contributingObjects)
				{
					if (dynamicMetaObject != null)
					{
						bindingRestrictions = bindingRestrictions.Merge(dynamicMetaObject.Restrictions);
					}
				}
			}
			return bindingRestrictions;
		}

		/// <summary>Creates the <see cref="T:System.Linq.Expressions.Expression" /> representing the binding restrictions.</summary>
		/// <returns>The expression tree representing the restrictions.</returns>
		// Token: 0x06001787 RID: 6023 RVA: 0x0004D070 File Offset: 0x0004B270
		public Expression ToExpression()
		{
			return this.GetExpression();
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x0004D078 File Offset: 0x0004B278
		private string DebugView
		{
			get
			{
				return this.ToExpression().ToString();
			}
		}

		/// <summary>Represents an empty set of binding restrictions. This field is read only.</summary>
		// Token: 0x04000AD5 RID: 2773
		public static readonly BindingRestrictions Empty = new BindingRestrictions.CustomRestriction(Utils.Constant(true));

		// Token: 0x04000AD6 RID: 2774
		private const int TypeRestrictionHash = 1227133513;

		// Token: 0x04000AD7 RID: 2775
		private const int InstanceRestrictionHash = -1840700270;

		// Token: 0x04000AD8 RID: 2776
		private const int CustomRestrictionHash = 613566756;

		// Token: 0x02000307 RID: 775
		private sealed class TestBuilder
		{
			// Token: 0x0600178A RID: 6026 RVA: 0x0004D097 File Offset: 0x0004B297
			internal void Append(BindingRestrictions restrictions)
			{
				if (this._unique.Add(restrictions))
				{
					this.Push(restrictions.GetExpression(), 0);
				}
			}

			// Token: 0x0600178B RID: 6027 RVA: 0x0004D0B4 File Offset: 0x0004B2B4
			internal Expression ToExpression()
			{
				Expression expression = this._tests.Pop().Node;
				while (this._tests.Count > 0)
				{
					expression = Expression.AndAlso(this._tests.Pop().Node, expression);
				}
				return expression;
			}

			// Token: 0x0600178C RID: 6028 RVA: 0x0004D0FC File Offset: 0x0004B2FC
			private void Push(Expression node, int depth)
			{
				while (this._tests.Count > 0 && this._tests.Peek().Depth == depth)
				{
					node = Expression.AndAlso(this._tests.Pop().Node, node);
					depth++;
				}
				this._tests.Push(new BindingRestrictions.TestBuilder.AndNode
				{
					Node = node,
					Depth = depth
				});
			}

			// Token: 0x04000AD9 RID: 2777
			private readonly HashSet<BindingRestrictions> _unique = new HashSet<BindingRestrictions>();

			// Token: 0x04000ADA RID: 2778
			private readonly Stack<BindingRestrictions.TestBuilder.AndNode> _tests = new Stack<BindingRestrictions.TestBuilder.AndNode>();

			// Token: 0x02000308 RID: 776
			private struct AndNode
			{
				// Token: 0x04000ADB RID: 2779
				internal int Depth;

				// Token: 0x04000ADC RID: 2780
				internal Expression Node;
			}
		}

		// Token: 0x02000309 RID: 777
		private sealed class MergedRestriction : BindingRestrictions
		{
			// Token: 0x0600178E RID: 6030 RVA: 0x0004D18B File Offset: 0x0004B38B
			internal MergedRestriction(BindingRestrictions left, BindingRestrictions right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x0600178F RID: 6031 RVA: 0x0004D1A4 File Offset: 0x0004B3A4
			internal override Expression GetExpression()
			{
				BindingRestrictions.TestBuilder testBuilder = new BindingRestrictions.TestBuilder();
				Stack<BindingRestrictions> stack = new Stack<BindingRestrictions>();
				BindingRestrictions bindingRestrictions = this;
				for (;;)
				{
					BindingRestrictions.MergedRestriction mergedRestriction = bindingRestrictions as BindingRestrictions.MergedRestriction;
					if (mergedRestriction != null)
					{
						stack.Push(mergedRestriction.Right);
						bindingRestrictions = mergedRestriction.Left;
					}
					else
					{
						testBuilder.Append(bindingRestrictions);
						if (stack.Count == 0)
						{
							break;
						}
						bindingRestrictions = stack.Pop();
					}
				}
				return testBuilder.ToExpression();
			}

			// Token: 0x04000ADD RID: 2781
			internal readonly BindingRestrictions Left;

			// Token: 0x04000ADE RID: 2782
			internal readonly BindingRestrictions Right;
		}

		// Token: 0x0200030A RID: 778
		private sealed class CustomRestriction : BindingRestrictions
		{
			// Token: 0x06001790 RID: 6032 RVA: 0x0004D1FC File Offset: 0x0004B3FC
			internal CustomRestriction(Expression expression)
			{
				this._expression = expression;
			}

			// Token: 0x06001791 RID: 6033 RVA: 0x0004D20B File Offset: 0x0004B40B
			public override bool Equals(object obj)
			{
				BindingRestrictions.CustomRestriction customRestriction = obj as BindingRestrictions.CustomRestriction;
				return ((customRestriction != null) ? customRestriction._expression : null) == this._expression;
			}

			// Token: 0x06001792 RID: 6034 RVA: 0x0004D227 File Offset: 0x0004B427
			public override int GetHashCode()
			{
				return 613566756 ^ this._expression.GetHashCode();
			}

			// Token: 0x06001793 RID: 6035 RVA: 0x0004D23A File Offset: 0x0004B43A
			internal override Expression GetExpression()
			{
				return this._expression;
			}

			// Token: 0x04000ADF RID: 2783
			private readonly Expression _expression;
		}

		// Token: 0x0200030B RID: 779
		private sealed class TypeRestriction : BindingRestrictions
		{
			// Token: 0x06001794 RID: 6036 RVA: 0x0004D242 File Offset: 0x0004B442
			internal TypeRestriction(Expression parameter, Type type)
			{
				this._expression = parameter;
				this._type = type;
			}

			// Token: 0x06001795 RID: 6037 RVA: 0x0004D258 File Offset: 0x0004B458
			public override bool Equals(object obj)
			{
				BindingRestrictions.TypeRestriction typeRestriction = obj as BindingRestrictions.TypeRestriction;
				return ((typeRestriction != null) ? typeRestriction._expression : null) == this._expression && TypeUtils.AreEquivalent(typeRestriction._type, this._type);
			}

			// Token: 0x06001796 RID: 6038 RVA: 0x0004D293 File Offset: 0x0004B493
			public override int GetHashCode()
			{
				return 1227133513 ^ this._expression.GetHashCode() ^ this._type.GetHashCode();
			}

			// Token: 0x06001797 RID: 6039 RVA: 0x0004D2B2 File Offset: 0x0004B4B2
			internal override Expression GetExpression()
			{
				return Expression.TypeEqual(this._expression, this._type);
			}

			// Token: 0x04000AE0 RID: 2784
			private readonly Expression _expression;

			// Token: 0x04000AE1 RID: 2785
			private readonly Type _type;
		}

		// Token: 0x0200030C RID: 780
		private sealed class InstanceRestriction : BindingRestrictions
		{
			// Token: 0x06001798 RID: 6040 RVA: 0x0004D2C5 File Offset: 0x0004B4C5
			internal InstanceRestriction(Expression parameter, object instance)
			{
				this._expression = parameter;
				this._instance = instance;
			}

			// Token: 0x06001799 RID: 6041 RVA: 0x0004D2DC File Offset: 0x0004B4DC
			public override bool Equals(object obj)
			{
				BindingRestrictions.InstanceRestriction instanceRestriction = obj as BindingRestrictions.InstanceRestriction;
				return ((instanceRestriction != null) ? instanceRestriction._expression : null) == this._expression && instanceRestriction._instance == this._instance;
			}

			// Token: 0x0600179A RID: 6042 RVA: 0x0004D314 File Offset: 0x0004B514
			public override int GetHashCode()
			{
				return -1840700270 ^ RuntimeHelpers.GetHashCode(this._instance) ^ this._expression.GetHashCode();
			}

			// Token: 0x0600179B RID: 6043 RVA: 0x0004D334 File Offset: 0x0004B534
			internal override Expression GetExpression()
			{
				if (this._instance == null)
				{
					return Expression.Equal(Expression.Convert(this._expression, typeof(object)), Utils.Null);
				}
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, Expression.Constant(this._instance, typeof(object))),
					Expression.AndAlso(Expression.NotEqual(parameterExpression, Utils.Null), Expression.Equal(Expression.Convert(this._expression, typeof(object)), parameterExpression))
				}));
			}

			// Token: 0x04000AE2 RID: 2786
			private readonly Expression _expression;

			// Token: 0x04000AE3 RID: 2787
			private readonly object _instance;
		}

		// Token: 0x0200030D RID: 781
		private sealed class BindingRestrictionsProxy
		{
			// Token: 0x0600179C RID: 6044 RVA: 0x0004D3E5 File Offset: 0x0004B5E5
			public BindingRestrictionsProxy(BindingRestrictions node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000424 RID: 1060
			// (get) Token: 0x0600179D RID: 6045 RVA: 0x0004D3FF File Offset: 0x0004B5FF
			public bool IsEmpty
			{
				get
				{
					return this._node == BindingRestrictions.Empty;
				}
			}

			// Token: 0x17000425 RID: 1061
			// (get) Token: 0x0600179E RID: 6046 RVA: 0x0004D40E File Offset: 0x0004B60E
			public Expression Test
			{
				get
				{
					return this._node.ToExpression();
				}
			}

			// Token: 0x17000426 RID: 1062
			// (get) Token: 0x0600179F RID: 6047 RVA: 0x0004D41C File Offset: 0x0004B61C
			public BindingRestrictions[] Restrictions
			{
				get
				{
					List<BindingRestrictions> list = new List<BindingRestrictions>();
					Stack<BindingRestrictions> stack = new Stack<BindingRestrictions>();
					BindingRestrictions bindingRestrictions = this._node;
					for (;;)
					{
						BindingRestrictions.MergedRestriction mergedRestriction = bindingRestrictions as BindingRestrictions.MergedRestriction;
						if (mergedRestriction != null)
						{
							stack.Push(mergedRestriction.Right);
							bindingRestrictions = mergedRestriction.Left;
						}
						else
						{
							list.Add(bindingRestrictions);
							if (stack.Count == 0)
							{
								break;
							}
							bindingRestrictions = stack.Pop();
						}
					}
					return list.ToArray();
				}
			}

			// Token: 0x060017A0 RID: 6048 RVA: 0x0004D479 File Offset: 0x0004B679
			public override string ToString()
			{
				return this._node.DebugView;
			}

			// Token: 0x04000AE4 RID: 2788
			private readonly BindingRestrictions _node;
		}
	}
}
