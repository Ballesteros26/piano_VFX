using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002CC RID: 716
	internal sealed class HoistedLocals
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x0003FFEC File Offset: 0x0003E1EC
		internal HoistedLocals(HoistedLocals parent, ReadOnlyCollection<ParameterExpression> vars)
		{
			if (parent != null)
			{
				vars = vars.AddFirst(parent.SelfVariable);
			}
			Dictionary<Expression, int> dictionary = new Dictionary<Expression, int>(vars.Count);
			for (int i = 0; i < vars.Count; i++)
			{
				dictionary.Add(vars[i], i);
			}
			this.SelfVariable = Expression.Variable(typeof(object[]), null);
			this.Parent = parent;
			this.Variables = vars;
			this.Indexes = new ReadOnlyDictionary<Expression, int>(dictionary);
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0004006B File Offset: 0x0003E26B
		internal ParameterExpression ParentVariable
		{
			get
			{
				HoistedLocals parent = this.Parent;
				if (parent == null)
				{
					return null;
				}
				return parent.SelfVariable;
			}
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0004007E File Offset: 0x0003E27E
		internal static object[] GetParent(object[] locals)
		{
			return ((StrongBox<object[]>)locals[0]).Value;
		}

		// Token: 0x04000A2E RID: 2606
		internal readonly HoistedLocals Parent;

		// Token: 0x04000A2F RID: 2607
		internal readonly ReadOnlyDictionary<Expression, int> Indexes;

		// Token: 0x04000A30 RID: 2608
		internal readonly ReadOnlyCollection<ParameterExpression> Variables;

		// Token: 0x04000A31 RID: 2609
		internal readonly ParameterExpression SelfVariable;
	}
}
