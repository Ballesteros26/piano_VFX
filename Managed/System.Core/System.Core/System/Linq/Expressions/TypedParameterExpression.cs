using System;

namespace System.Linq.Expressions
{
	// Token: 0x020002AA RID: 682
	internal class TypedParameterExpression : ParameterExpression
	{
		// Token: 0x060013CF RID: 5071 RVA: 0x0003CD45 File Offset: 0x0003AF45
		internal TypedParameterExpression(Type type, string name)
			: base(name)
		{
			this.Type = type;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0003CD55 File Offset: 0x0003AF55
		public sealed override Type Type { get; }
	}
}
