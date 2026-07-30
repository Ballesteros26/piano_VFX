using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents a cache of runtime binding rules.</summary>
	/// <typeparam name="T">The delegate type.</typeparam>
	// Token: 0x020002FE RID: 766
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	public class RuleCache<T> where T : class
	{
		// Token: 0x06001762 RID: 5986 RVA: 0x0004CB78 File Offset: 0x0004AD78
		internal RuleCache()
		{
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0004CB96 File Offset: 0x0004AD96
		internal T[] GetRules()
		{
			return this._rules;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0004CBA0 File Offset: 0x0004ADA0
		internal void MoveRule(T rule, int i)
		{
			object cacheLock = this._cacheLock;
			lock (cacheLock)
			{
				int num = this._rules.Length - i;
				if (num > 8)
				{
					num = 8;
				}
				int num2 = -1;
				int num3 = Math.Min(this._rules.Length, i + num);
				for (int j = i; j < num3; j++)
				{
					if (this._rules[j] == rule)
					{
						num2 = j;
						break;
					}
				}
				if (num2 >= 2)
				{
					T t = this._rules[num2];
					this._rules[num2] = this._rules[num2 - 1];
					this._rules[num2 - 1] = this._rules[num2 - 2];
					this._rules[num2 - 2] = t;
				}
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0004CC8C File Offset: 0x0004AE8C
		internal void AddRule(T newRule)
		{
			object cacheLock = this._cacheLock;
			lock (cacheLock)
			{
				this._rules = RuleCache<T>.AddOrInsert(this._rules, newRule);
			}
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0004CCD8 File Offset: 0x0004AED8
		internal void ReplaceRule(T oldRule, T newRule)
		{
			object cacheLock = this._cacheLock;
			lock (cacheLock)
			{
				int num = Array.IndexOf<T>(this._rules, oldRule);
				if (num >= 0)
				{
					this._rules[num] = newRule;
				}
				else
				{
					this._rules = RuleCache<T>.AddOrInsert(this._rules, newRule);
				}
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0004CD44 File Offset: 0x0004AF44
		private static T[] AddOrInsert(T[] rules, T item)
		{
			if (rules.Length < 64)
			{
				return rules.AddLast(item);
			}
			int num = rules.Length + 1;
			T[] array;
			if (num > 128)
			{
				num = 128;
				array = rules;
			}
			else
			{
				array = new T[num];
			}
			Array.Copy(rules, 0, array, 0, 64);
			array[64] = item;
			Array.Copy(rules, 64, array, 65, num - 64 - 1);
			return array;
		}

		// Token: 0x04000ACB RID: 2763
		private T[] _rules = Array.Empty<T>();

		// Token: 0x04000ACC RID: 2764
		private readonly object _cacheLock = new object();

		// Token: 0x04000ACD RID: 2765
		private const int MaxRules = 128;

		// Token: 0x04000ACE RID: 2766
		private const int InsertPosition = 64;
	}
}
