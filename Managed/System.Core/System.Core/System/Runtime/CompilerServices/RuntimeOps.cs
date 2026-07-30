using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Linq.Expressions.Compiler;

namespace System.Runtime.CompilerServices
{
	/// <summary>Contains helper methods called from dynamically generated methods.</summary>
	// Token: 0x020002EC RID: 748
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	public static class RuntimeOps
	{
		/// <summary>Gets the value of an item in an expando object.</summary>
		/// <returns>True if the member exists in the expando object, otherwise false.</returns>
		/// <param name="expando">The expando object.</param>
		/// <param name="indexClass">The class of the expando object.</param>
		/// <param name="index">The index of the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		/// <param name="value">The out parameter containing the value of the member.</param>
		// Token: 0x060016DB RID: 5851 RVA: 0x0004AF66 File Offset: 0x00049166
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool ExpandoTryGetValue(ExpandoObject expando, object indexClass, int index, string name, bool ignoreCase, out object value)
		{
			return expando.TryGetValue(indexClass, index, name, ignoreCase, out value);
		}

		/// <summary>Sets the value of an item in an expando object.</summary>
		/// <returns>Returns the index for the set member.</returns>
		/// <param name="expando">The expando object.</param>
		/// <param name="indexClass">The class of the expando object.</param>
		/// <param name="index">The index of the member.</param>
		/// <param name="value">The value of the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x060016DC RID: 5852 RVA: 0x0004AF75 File Offset: 0x00049175
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static object ExpandoTrySetValue(ExpandoObject expando, object indexClass, int index, object value, string name, bool ignoreCase)
		{
			expando.TrySetValue(indexClass, index, value, name, ignoreCase, false);
			return value;
		}

		/// <summary>Deletes the value of an item in an expando object.</summary>
		/// <returns>true if the item was successfully removed; otherwise, false.</returns>
		/// <param name="expando">The expando object.</param>
		/// <param name="indexClass">The class of the expando object.</param>
		/// <param name="index">The index of the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x060016DD RID: 5853 RVA: 0x0004AF86 File Offset: 0x00049186
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool ExpandoTryDeleteValue(ExpandoObject expando, object indexClass, int index, string name, bool ignoreCase)
		{
			return expando.TryDeleteValue(indexClass, index, name, ignoreCase, ExpandoObject.Uninitialized);
		}

		/// <summary>Checks the version of the Expando object.</summary>
		/// <returns>Returns true if the version is equal; otherwise, false.</returns>
		/// <param name="expando">The Expando object.</param>
		/// <param name="version">The version to check.</param>
		// Token: 0x060016DE RID: 5854 RVA: 0x0004AF98 File Offset: 0x00049198
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static bool ExpandoCheckVersion(ExpandoObject expando, object version)
		{
			return expando.Class == version;
		}

		/// <summary>Promotes an Expando object from one class to a new class.</summary>
		/// <param name="expando">The Expando object.</param>
		/// <param name="oldClass">The old class of the Expando object.</param>
		/// <param name="newClass">The new class of the Expando object.</param>
		// Token: 0x060016DF RID: 5855 RVA: 0x0004AFA3 File Offset: 0x000491A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static void ExpandoPromoteClass(ExpandoObject expando, object oldClass, object newClass)
		{
			expando.PromoteClass(oldClass, newClass);
		}

		/// <summary>Quotes the provided expression tree.</summary>
		/// <returns>The quoted expression.</returns>
		/// <param name="expression">The expression to quote.</param>
		/// <param name="hoistedLocals">The hoisted local state provided by the compiler.</param>
		/// <param name="locals">The actual hoisted local values.</param>
		// Token: 0x060016E0 RID: 5856 RVA: 0x0004AFAD File Offset: 0x000491AD
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Expression Quote(Expression expression, object hoistedLocals, object[] locals)
		{
			return new RuntimeOps.ExpressionQuoter((HoistedLocals)hoistedLocals, locals).Visit(expression);
		}

		/// <summary>Combines two runtime variable lists and returns a new list.</summary>
		/// <returns>The merged runtime variables.</returns>
		/// <param name="first">The first list.</param>
		/// <param name="second">The second list.</param>
		/// <param name="indexes">The index array indicating which list to get variables from.</param>
		// Token: 0x060016E1 RID: 5857 RVA: 0x0004AFC1 File Offset: 0x000491C1
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IRuntimeVariables MergeRuntimeVariables(IRuntimeVariables first, IRuntimeVariables second, int[] indexes)
		{
			return new RuntimeOps.MergedRuntimeVariables(first, second, indexes);
		}

		/// <summary>Creates an interface that can be used to modify closed over variables at runtime.</summary>
		/// <returns>An interface to access variables.</returns>
		/// <param name="data">The closure array.</param>
		/// <param name="indexes">An array of indicies into the closure array where variables are found.</param>
		// Token: 0x060016E2 RID: 5858 RVA: 0x0004AFCB File Offset: 0x000491CB
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IRuntimeVariables CreateRuntimeVariables(object[] data, long[] indexes)
		{
			return new RuntimeOps.RuntimeVariableList(data, indexes);
		}

		/// <summary>Creates an interface that can be used to modify closed over variables at runtime.</summary>
		/// <returns>An interface to access variables.</returns>
		// Token: 0x060016E3 RID: 5859 RVA: 0x0004AFD4 File Offset: 0x000491D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static IRuntimeVariables CreateRuntimeVariables()
		{
			return new RuntimeOps.EmptyRuntimeVariables();
		}

		// Token: 0x020002ED RID: 749
		private sealed class ExpressionQuoter : ExpressionVisitor
		{
			// Token: 0x060016E4 RID: 5860 RVA: 0x0004AFDB File Offset: 0x000491DB
			internal ExpressionQuoter(HoistedLocals scope, object[] locals)
			{
				this._scope = scope;
				this._locals = locals;
			}

			// Token: 0x060016E5 RID: 5861 RVA: 0x0004AFFC File Offset: 0x000491FC
			protected internal override Expression VisitLambda<T>(Expression<T> node)
			{
				if (node.ParameterCount > 0)
				{
					HashSet<ParameterExpression> hashSet = new HashSet<ParameterExpression>();
					int i = 0;
					int parameterCount = node.ParameterCount;
					while (i < parameterCount)
					{
						hashSet.Add(node.GetParameter(i));
						i++;
					}
					this._shadowedVars.Push(hashSet);
				}
				Expression expression = this.Visit(node.Body);
				if (node.ParameterCount > 0)
				{
					this._shadowedVars.Pop();
				}
				if (expression == node.Body)
				{
					return node;
				}
				return node.Rewrite(expression, null);
			}

			// Token: 0x060016E6 RID: 5862 RVA: 0x0004B07C File Offset: 0x0004927C
			protected internal override Expression VisitBlock(BlockExpression node)
			{
				if (node.Variables.Count > 0)
				{
					this._shadowedVars.Push(new HashSet<ParameterExpression>(node.Variables));
				}
				Expression[] array = ExpressionVisitorUtils.VisitBlockExpressions(this, node);
				if (node.Variables.Count > 0)
				{
					this._shadowedVars.Pop();
				}
				if (array == null)
				{
					return node;
				}
				return node.Rewrite(node.Variables, array);
			}

			// Token: 0x060016E7 RID: 5863 RVA: 0x0004B0E4 File Offset: 0x000492E4
			protected override CatchBlock VisitCatchBlock(CatchBlock node)
			{
				if (node.Variable != null)
				{
					this._shadowedVars.Push(new HashSet<ParameterExpression> { node.Variable });
				}
				Expression expression = this.Visit(node.Body);
				Expression expression2 = this.Visit(node.Filter);
				if (node.Variable != null)
				{
					this._shadowedVars.Pop();
				}
				if (expression == node.Body && expression2 == node.Filter)
				{
					return node;
				}
				return Expression.MakeCatchBlock(node.Test, node.Variable, expression, expression2);
			}

			// Token: 0x060016E8 RID: 5864 RVA: 0x0004B16C File Offset: 0x0004936C
			protected internal override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
			{
				int count = node.Variables.Count;
				List<IStrongBox> list = new List<IStrongBox>();
				List<ParameterExpression> list2 = new List<ParameterExpression>();
				int[] array = new int[count];
				for (int i = 0; i < array.Length; i++)
				{
					IStrongBox box = this.GetBox(node.Variables[i]);
					if (box == null)
					{
						array[i] = list2.Count;
						list2.Add(node.Variables[i]);
					}
					else
					{
						array[i] = -1 - list.Count;
						list.Add(box);
					}
				}
				if (list.Count == 0)
				{
					return node;
				}
				ConstantExpression constantExpression = Expression.Constant(new RuntimeOps.RuntimeVariables(list.ToArray()), typeof(IRuntimeVariables));
				if (list2.Count == 0)
				{
					return constantExpression;
				}
				return Expression.Call(CachedReflectionInfo.RuntimeOps_MergeRuntimeVariables, Expression.RuntimeVariables(new TrueReadOnlyCollection<ParameterExpression>(list2.ToArray())), constantExpression, Expression.Constant(array));
			}

			// Token: 0x060016E9 RID: 5865 RVA: 0x0004B244 File Offset: 0x00049444
			protected internal override Expression VisitParameter(ParameterExpression node)
			{
				IStrongBox box = this.GetBox(node);
				if (box == null)
				{
					return node;
				}
				return Expression.Field(Expression.Constant(box), "Value");
			}

			// Token: 0x060016EA RID: 5866 RVA: 0x0004B270 File Offset: 0x00049470
			private IStrongBox GetBox(ParameterExpression variable)
			{
				using (Stack<HashSet<ParameterExpression>>.Enumerator enumerator = this._shadowedVars.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Contains(variable))
						{
							return null;
						}
					}
				}
				HoistedLocals hoistedLocals = this._scope;
				object[] array = this._locals;
				int num;
				while (!hoistedLocals.Indexes.TryGetValue(variable, out num))
				{
					hoistedLocals = hoistedLocals.Parent;
					if (hoistedLocals == null)
					{
						throw ContractUtils.Unreachable;
					}
					array = HoistedLocals.GetParent(array);
				}
				return (IStrongBox)array[num];
			}

			// Token: 0x04000AA5 RID: 2725
			private readonly HoistedLocals _scope;

			// Token: 0x04000AA6 RID: 2726
			private readonly object[] _locals;

			// Token: 0x04000AA7 RID: 2727
			private readonly Stack<HashSet<ParameterExpression>> _shadowedVars = new Stack<HashSet<ParameterExpression>>();
		}

		// Token: 0x020002EE RID: 750
		internal sealed class MergedRuntimeVariables : IRuntimeVariables
		{
			// Token: 0x060016EB RID: 5867 RVA: 0x0004B30C File Offset: 0x0004950C
			internal MergedRuntimeVariables(IRuntimeVariables first, IRuntimeVariables second, int[] indexes)
			{
				this._first = first;
				this._second = second;
				this._indexes = indexes;
			}

			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x060016EC RID: 5868 RVA: 0x0004B329 File Offset: 0x00049529
			public int Count
			{
				get
				{
					return this._indexes.Length;
				}
			}

			// Token: 0x17000405 RID: 1029
			public object this[int index]
			{
				get
				{
					index = this._indexes[index];
					if (index < 0)
					{
						return this._second[-1 - index];
					}
					return this._first[index];
				}
				set
				{
					index = this._indexes[index];
					if (index >= 0)
					{
						this._first[index] = value;
						return;
					}
					this._second[-1 - index] = value;
				}
			}

			// Token: 0x04000AA8 RID: 2728
			private readonly IRuntimeVariables _first;

			// Token: 0x04000AA9 RID: 2729
			private readonly IRuntimeVariables _second;

			// Token: 0x04000AAA RID: 2730
			private readonly int[] _indexes;
		}

		// Token: 0x020002EF RID: 751
		private sealed class EmptyRuntimeVariables : IRuntimeVariables
		{
			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x060016EF RID: 5871 RVA: 0x00002285 File Offset: 0x00000485
			int IRuntimeVariables.Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000407 RID: 1031
			object IRuntimeVariables.this[int index]
			{
				get
				{
					throw new IndexOutOfRangeException();
				}
				set
				{
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x020002F0 RID: 752
		private sealed class RuntimeVariableList : IRuntimeVariables
		{
			// Token: 0x060016F3 RID: 5875 RVA: 0x0004B392 File Offset: 0x00049592
			internal RuntimeVariableList(object[] data, long[] indexes)
			{
				this._data = data;
				this._indexes = indexes;
			}

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0004B3A8 File Offset: 0x000495A8
			public int Count
			{
				get
				{
					return this._indexes.Length;
				}
			}

			// Token: 0x17000409 RID: 1033
			public object this[int index]
			{
				get
				{
					return this.GetStrongBox(index).Value;
				}
				set
				{
					this.GetStrongBox(index).Value = value;
				}
			}

			// Token: 0x060016F7 RID: 5879 RVA: 0x0004B3D0 File Offset: 0x000495D0
			private IStrongBox GetStrongBox(int index)
			{
				long num = this._indexes[index];
				object[] array = this._data;
				for (int i = (int)(num >> 32); i > 0; i--)
				{
					array = HoistedLocals.GetParent(array);
				}
				return (IStrongBox)array[(int)num];
			}

			// Token: 0x04000AAB RID: 2731
			private readonly object[] _data;

			// Token: 0x04000AAC RID: 2732
			private readonly long[] _indexes;
		}

		// Token: 0x020002F1 RID: 753
		internal sealed class RuntimeVariables : IRuntimeVariables
		{
			// Token: 0x060016F8 RID: 5880 RVA: 0x0004B40D File Offset: 0x0004960D
			internal RuntimeVariables(IStrongBox[] boxes)
			{
				this._boxes = boxes;
			}

			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0004B41C File Offset: 0x0004961C
			int IRuntimeVariables.Count
			{
				get
				{
					return this._boxes.Length;
				}
			}

			// Token: 0x1700040B RID: 1035
			object IRuntimeVariables.this[int index]
			{
				get
				{
					return this._boxes[index].Value;
				}
				set
				{
					this._boxes[index].Value = value;
				}
			}

			// Token: 0x04000AAD RID: 2733
			private readonly IStrongBox[] _boxes;
		}
	}
}
