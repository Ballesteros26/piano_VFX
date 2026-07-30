using System;

namespace System
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	internal class ReflectionOnlyType : RuntimeType
	{
		// Token: 0x060012D7 RID: 4823 RVA: 0x0004D442 File Offset: 0x0004B642
		private ReflectionOnlyType()
		{
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0004D44A File Offset: 0x0004B64A
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("The requested operation is invalid in the ReflectionOnly context."));
			}
		}
	}
}
