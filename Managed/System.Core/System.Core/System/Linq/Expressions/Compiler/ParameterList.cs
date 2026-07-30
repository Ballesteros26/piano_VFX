using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002C8 RID: 712
	internal sealed class ParameterList : IReadOnlyList<ParameterExpression>, IReadOnlyCollection<ParameterExpression>, IEnumerable<ParameterExpression>, IEnumerable
	{
		// Token: 0x06001531 RID: 5425 RVA: 0x0003F7BC File Offset: 0x0003D9BC
		public ParameterList(IParameterProvider provider)
		{
			this._provider = provider;
		}

		// Token: 0x170003ED RID: 1005
		public ParameterExpression this[int index]
		{
			get
			{
				return this._provider.GetParameter(index);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0003F7D9 File Offset: 0x0003D9D9
		public int Count
		{
			get
			{
				return this._provider.ParameterCount;
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0003F7E6 File Offset: 0x0003D9E6
		public IEnumerator<ParameterExpression> GetEnumerator()
		{
			int i = 0;
			int j = this._provider.ParameterCount;
			while (i < j)
			{
				yield return this._provider.GetParameter(i);
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0003F7F5 File Offset: 0x0003D9F5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000A20 RID: 2592
		private readonly IParameterProvider _provider;
	}
}
