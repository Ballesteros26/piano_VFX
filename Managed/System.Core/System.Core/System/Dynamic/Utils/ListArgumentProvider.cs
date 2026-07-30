using System;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x02000343 RID: 835
	internal sealed class ListArgumentProvider : ListProvider<Expression>
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x000530BD File Offset: 0x000512BD
		internal ListArgumentProvider(IArgumentProvider provider, Expression arg0)
		{
			this._provider = provider;
			this._arg0 = arg0;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x000530D3 File Offset: 0x000512D3
		protected override Expression First
		{
			get
			{
				return this._arg0;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x000530DB File Offset: 0x000512DB
		protected override int ElementCount
		{
			get
			{
				return this._provider.ArgumentCount;
			}
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000530E8 File Offset: 0x000512E8
		protected override Expression GetElement(int index)
		{
			return this._provider.GetArgument(index);
		}

		// Token: 0x04000B50 RID: 2896
		private readonly IArgumentProvider _provider;

		// Token: 0x04000B51 RID: 2897
		private readonly Expression _arg0;
	}
}
