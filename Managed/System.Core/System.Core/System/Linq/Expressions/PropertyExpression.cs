using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000291 RID: 657
	internal sealed class PropertyExpression : MemberExpression
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x0003BC70 File Offset: 0x00039E70
		public PropertyExpression(Expression expression, PropertyInfo member)
			: base(expression)
		{
			this._property = member;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0003BC80 File Offset: 0x00039E80
		internal override MemberInfo GetMember()
		{
			return this._property;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0003BC88 File Offset: 0x00039E88
		public sealed override Type Type
		{
			get
			{
				return this._property.PropertyType;
			}
		}

		// Token: 0x04000993 RID: 2451
		private readonly PropertyInfo _property;
	}
}
