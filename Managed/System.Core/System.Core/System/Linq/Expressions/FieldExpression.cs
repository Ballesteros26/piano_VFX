using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000290 RID: 656
	internal sealed class FieldExpression : MemberExpression
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x0003BC4B File Offset: 0x00039E4B
		public FieldExpression(Expression expression, FieldInfo member)
			: base(expression)
		{
			this._field = member;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0003BC5B File Offset: 0x00039E5B
		internal override MemberInfo GetMember()
		{
			return this._field;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0003BC63 File Offset: 0x00039E63
		public sealed override Type Type
		{
			get
			{
				return this._field.FieldType;
			}
		}

		// Token: 0x04000992 RID: 2450
		private readonly FieldInfo _field;
	}
}
