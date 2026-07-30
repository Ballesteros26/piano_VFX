using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000271 RID: 625
	internal abstract class BaseStyleMatcher
	{
		// Token: 0x06001255 RID: 4693
		protected abstract bool MatchKeyword(string keyword);

		// Token: 0x06001256 RID: 4694
		protected abstract bool MatchNumber();

		// Token: 0x06001257 RID: 4695
		protected abstract bool MatchInteger();

		// Token: 0x06001258 RID: 4696
		protected abstract bool MatchLength();

		// Token: 0x06001259 RID: 4697
		protected abstract bool MatchPercentage();

		// Token: 0x0600125A RID: 4698
		protected abstract bool MatchColor();

		// Token: 0x0600125B RID: 4699
		protected abstract bool MatchResource();

		// Token: 0x0600125C RID: 4700
		protected abstract bool MatchUrl();

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600125D RID: 4701
		public abstract int valueCount { get; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600125E RID: 4702
		public abstract bool isVariable { get; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00052277 File Offset: 0x00050477
		public bool hasCurrent
		{
			get
			{
				return this.m_CurrentIndex < this.valueCount;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00052287 File Offset: 0x00050487
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x0005228F File Offset: 0x0005048F
		public int matchedVariableCount { get; set; }

		// Token: 0x06001262 RID: 4706 RVA: 0x00052298 File Offset: 0x00050498
		protected void Initialize()
		{
			this.m_CurrentIndex = 0;
			this.m_MarkStack.Clear();
			this.matchedVariableCount = 0;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000522B8 File Offset: 0x000504B8
		public void MoveNext()
		{
			bool flag = this.m_CurrentIndex + 1 <= this.valueCount;
			if (flag)
			{
				this.m_CurrentIndex++;
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000522ED File Offset: 0x000504ED
		public void SaveMark()
		{
			this.m_MarkStack.Push(this.m_CurrentIndex);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00052302 File Offset: 0x00050502
		public void RestoreMark()
		{
			this.m_CurrentIndex = this.m_MarkStack.Pop();
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00052316 File Offset: 0x00050516
		public void DropMark()
		{
			this.m_MarkStack.Pop();
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00052328 File Offset: 0x00050528
		protected bool Match(Expression exp)
		{
			bool flag = true;
			bool flag2 = exp.multiplier.type == ExpressionMultiplierType.None;
			if (flag2)
			{
				flag = this.MatchExpression(exp);
			}
			else
			{
				Debug.Assert(exp.multiplier.type != ExpressionMultiplierType.OneOrMoreComma, "'#' multiplier in syntax expression is not supported");
				Debug.Assert(exp.multiplier.type != ExpressionMultiplierType.GroupAtLeastOne, "'!' multiplier in syntax expression is not supported");
				int min = exp.multiplier.min;
				int max = exp.multiplier.max;
				int num = 0;
				int num2 = 0;
				while (flag && this.hasCurrent && num2 < max)
				{
					flag = this.MatchExpression(exp);
					bool flag3 = flag;
					if (flag3)
					{
						num++;
					}
					num2++;
				}
				flag = num >= min && num <= max;
			}
			return flag;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00052404 File Offset: 0x00050604
		private bool MatchExpression(Expression exp)
		{
			bool flag = false;
			bool flag2 = exp.type == ExpressionType.Combinator;
			if (flag2)
			{
				flag = this.MatchCombinator(exp);
			}
			else
			{
				bool isVariable = this.isVariable;
				if (isVariable)
				{
					flag = true;
					int matchedVariableCount = this.matchedVariableCount;
					this.matchedVariableCount = matchedVariableCount + 1;
				}
				else
				{
					bool flag3 = exp.type == ExpressionType.Data;
					if (flag3)
					{
						flag = this.MatchDataType(exp);
					}
					else
					{
						bool flag4 = exp.type == ExpressionType.Keyword;
						if (flag4)
						{
							flag = this.MatchKeyword(exp.keyword);
						}
					}
				}
				bool flag5 = flag;
				if (flag5)
				{
					this.MoveNext();
				}
			}
			bool flag6 = !flag && !this.hasCurrent && this.matchedVariableCount > 0;
			if (flag6)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000524C0 File Offset: 0x000506C0
		private bool MatchGroup(Expression exp)
		{
			Debug.Assert(exp.subExpressions.Length == 1, "Group has invalid number of sub expressions");
			Expression expression = exp.subExpressions[0];
			return this.Match(expression);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000524F8 File Offset: 0x000506F8
		private bool MatchCombinator(Expression exp)
		{
			this.SaveMark();
			bool flag = false;
			switch (exp.combinator)
			{
			case ExpressionCombinator.Or:
				flag = this.MatchOr(exp);
				break;
			case ExpressionCombinator.OrOr:
				flag = this.MatchOrOr(exp);
				break;
			case ExpressionCombinator.AndAnd:
				flag = this.MatchAndAnd(exp);
				break;
			case ExpressionCombinator.Juxtaposition:
				flag = this.MatchJuxtaposition(exp);
				break;
			case ExpressionCombinator.Group:
				flag = this.MatchGroup(exp);
				break;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.DropMark();
			}
			else
			{
				this.RestoreMark();
			}
			return flag;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00052584 File Offset: 0x00050784
		private bool MatchOr(Expression exp)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < exp.subExpressions.Length)
			{
				flag = this.Match(exp.subExpressions[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000525C8 File Offset: 0x000507C8
		private bool MatchOrOr(Expression exp)
		{
			int num = this.MatchMany(exp);
			return num > 0;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x000525E8 File Offset: 0x000507E8
		private bool MatchAndAnd(Expression exp)
		{
			int num = this.MatchMany(exp);
			int num2 = exp.subExpressions.Length;
			return num == num2;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00052610 File Offset: 0x00050810
		private unsafe int MatchMany(Expression exp)
		{
			int num = 0;
			int num2 = 0;
			int num3 = exp.subExpressions.Length;
			int* ptr;
			int num4;
			checked
			{
				ptr = stackalloc int[unchecked((UIntPtr)num3) * 4];
				num4 = 0;
			}
			while (num4 < num3 && num + num2 < num3)
			{
				bool flag = false;
				for (int i = 0; i < num; i++)
				{
					bool flag2 = ptr[i] == num4;
					if (flag2)
					{
						flag = true;
						break;
					}
				}
				bool flag3 = false;
				bool flag4 = !flag;
				if (flag4)
				{
					flag3 = this.Match(exp.subExpressions[num4]);
				}
				bool flag5 = flag3;
				if (flag5)
				{
					bool flag6 = num2 == this.matchedVariableCount;
					if (flag6)
					{
						ptr[num] = num4;
						num++;
					}
					else
					{
						num2 = this.matchedVariableCount;
					}
					num4 = 0;
				}
				else
				{
					num4++;
				}
			}
			return num + num2;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000526F0 File Offset: 0x000508F0
		private bool MatchJuxtaposition(Expression exp)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < exp.subExpressions.Length)
			{
				flag = this.Match(exp.subExpressions[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00052734 File Offset: 0x00050934
		private bool MatchDataType(Expression exp)
		{
			bool flag = false;
			bool hasCurrent = this.hasCurrent;
			if (hasCurrent)
			{
				switch (exp.dataType)
				{
				case DataType.Number:
					flag = this.MatchNumber();
					break;
				case DataType.Integer:
					flag = this.MatchInteger();
					break;
				case DataType.Length:
					flag = this.MatchLength();
					break;
				case DataType.Percentage:
					flag = this.MatchPercentage();
					break;
				case DataType.Color:
					flag = this.MatchColor();
					break;
				case DataType.Resource:
					flag = this.MatchResource();
					break;
				case DataType.Url:
					flag = this.MatchUrl();
					break;
				}
			}
			return flag;
		}

		// Token: 0x04000927 RID: 2343
		private Stack<int> m_MarkStack = new Stack<int>();

		// Token: 0x04000928 RID: 2344
		protected int m_CurrentIndex;
	}
}
