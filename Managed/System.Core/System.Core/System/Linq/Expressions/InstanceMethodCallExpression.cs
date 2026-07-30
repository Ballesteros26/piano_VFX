using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000296 RID: 662
	internal class InstanceMethodCallExpression : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06001362 RID: 4962 RVA: 0x0003BF82 File Offset: 0x0003A182
		public InstanceMethodCallExpression(MethodInfo method, Expression instance)
			: base(method)
		{
			this._instance = instance;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0003BF92 File Offset: 0x0003A192
		internal override Expression GetInstance()
		{
			return this._instance;
		}

		// Token: 0x04000999 RID: 2457
		private readonly Expression _instance;
	}
}
