using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020002A7 RID: 679
	internal sealed class NewValueTypeExpression : NewExpression
	{
		// Token: 0x060013C2 RID: 5058 RVA: 0x0003CBB9 File Offset: 0x0003ADB9
		internal NewValueTypeExpression(Type type, ReadOnlyCollection<Expression> arguments, ReadOnlyCollection<MemberInfo> members)
			: base(null, arguments, members)
		{
			this.Type = type;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0003CBCB File Offset: 0x0003ADCB
		public sealed override Type Type { get; }
	}
}
