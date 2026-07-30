using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000259 RID: 601
	internal class TypedConstantExpression : ConstantExpression
	{
		// Token: 0x06001082 RID: 4226 RVA: 0x00035EF7 File Offset: 0x000340F7
		internal TypedConstantExpression(object value, Type type)
			: base(value)
		{
			this.Type = type;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00035F07 File Offset: 0x00034107
		public sealed override Type Type { get; }
	}
}
