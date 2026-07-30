using System;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x02000344 RID: 836
	internal sealed class ListParameterProvider : ListProvider<ParameterExpression>
	{
		// Token: 0x0600194D RID: 6477 RVA: 0x000530F6 File Offset: 0x000512F6
		internal ListParameterProvider(IParameterProvider provider, ParameterExpression arg0)
		{
			this._provider = provider;
			this._arg0 = arg0;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0005310C File Offset: 0x0005130C
		protected override ParameterExpression First
		{
			get
			{
				return this._arg0;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x00053114 File Offset: 0x00051314
		protected override int ElementCount
		{
			get
			{
				return this._provider.ParameterCount;
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00053121 File Offset: 0x00051321
		protected override ParameterExpression GetElement(int index)
		{
			return this._provider.GetParameter(index);
		}

		// Token: 0x04000B52 RID: 2898
		private readonly IParameterProvider _provider;

		// Token: 0x04000B53 RID: 2899
		private readonly ParameterExpression _arg0;
	}
}
