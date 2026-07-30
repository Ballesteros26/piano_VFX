using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000267 RID: 615
	internal class DynamicExpression3 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x00037E6C File Offset: 0x0003606C
		internal DynamicExpression3(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
			: base(delegateType, binder)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00037E8D File Offset: 0x0003608D
		Expression IArgumentProvider.GetArgument(int index)
		{
			switch (index)
			{
			case 0:
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			case 1:
				return this._arg1;
			case 2:
				return this._arg2;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0003554A File Offset: 0x0003374A
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00037EC8 File Offset: 0x000360C8
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 3)
			{
				ReadOnlyCollection<Expression> readOnlyCollection = this._arg0 as ReadOnlyCollection<Expression>;
				if (readOnlyCollection != null)
				{
					return ExpressionUtils.SameElements<Expression>(arguments, readOnlyCollection);
				}
				using (IEnumerator<Expression> enumerator = arguments.GetEnumerator())
				{
					enumerator.MoveNext();
					if (enumerator.Current == this._arg0)
					{
						enumerator.MoveNext();
						if (enumerator.Current == this._arg1)
						{
							enumerator.MoveNext();
							return enumerator.Current == this._arg2;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00037F60 File Offset: 0x00036160
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00037F6E File Offset: 0x0003616E
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args[0], args[1], args[2]);
		}

		// Token: 0x040008F6 RID: 2294
		private object _arg0;

		// Token: 0x040008F7 RID: 2295
		private readonly Expression _arg1;

		// Token: 0x040008F8 RID: 2296
		private readonly Expression _arg2;
	}
}
