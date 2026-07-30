using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200024F RID: 591
	internal class BlockExpressionList : IList<Expression>, ICollection<Expression>, IEnumerable<Expression>, IEnumerable
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x00035A74 File Offset: 0x00033C74
		internal BlockExpressionList(BlockExpression provider, Expression arg0)
		{
			this._block = provider;
			this._arg0 = arg0;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00035A8C File Offset: 0x00033C8C
		public int IndexOf(Expression item)
		{
			if (this._arg0 == item)
			{
				return 0;
			}
			for (int i = 1; i < this._block.ExpressionCount; i++)
			{
				if (this._block.GetExpression(i) == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public void Insert(int index, Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public void RemoveAt(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170002BF RID: 703
		public Expression this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this._arg0;
				}
				return this._block.GetExpression(index);
			}
			[ExcludeFromCodeCoverage]
			set
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public void Add(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public void Clear()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00035AE4 File Offset: 0x00033CE4
		public bool Contains(Expression item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00035AF4 File Offset: 0x00033CF4
		public void CopyTo(Expression[] array, int index)
		{
			ContractUtils.RequiresNotNull(array, "array");
			if (index < 0)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			int expressionCount = this._block.ExpressionCount;
			if (index + expressionCount > array.Length)
			{
				throw new ArgumentException();
			}
			array[index++] = this._arg0;
			for (int i = 1; i < expressionCount; i++)
			{
				array[index++] = this._block.GetExpression(i);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x00035B63 File Offset: 0x00033D63
		public int Count
		{
			get
			{
				return this._block.ExpressionCount;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public bool IsReadOnly
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public bool Remove(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00035B70 File Offset: 0x00033D70
		public IEnumerator<Expression> GetEnumerator()
		{
			yield return this._arg0;
			int num;
			for (int i = 1; i < this._block.ExpressionCount; i = num + 1)
			{
				yield return this._block.GetExpression(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00035B7F File Offset: 0x00033D7F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040008C2 RID: 2242
		private readonly BlockExpression _block;

		// Token: 0x040008C3 RID: 2243
		private readonly Expression _arg0;
	}
}
