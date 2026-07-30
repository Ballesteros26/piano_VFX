using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000354 RID: 852
	internal class DynamicMethodTokenGenerator : TokenGenerator
	{
		// Token: 0x060025F8 RID: 9720 RVA: 0x000885BB File Offset: 0x000867BB
		public DynamicMethodTokenGenerator(DynamicMethod m)
		{
			this.m = m;
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000885CA File Offset: 0x000867CA
		public int GetToken(string str)
		{
			return this.m.AddRef(str);
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x0007EA26 File Offset: 0x0007CC26
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x000885CA File Offset: 0x000867CA
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.m.AddRef(member);
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000885CA File Offset: 0x000867CA
		public int GetToken(SignatureHelper helper)
		{
			return this.m.AddRef(helper);
		}

		// Token: 0x040013F4 RID: 5108
		private DynamicMethod m;
	}
}
