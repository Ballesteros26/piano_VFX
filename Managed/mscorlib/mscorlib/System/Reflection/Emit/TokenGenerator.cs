using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000361 RID: 865
	internal interface TokenGenerator
	{
		// Token: 0x060026F2 RID: 9970
		int GetToken(string str);

		// Token: 0x060026F3 RID: 9971
		int GetToken(MemberInfo member, bool create_open_instance);

		// Token: 0x060026F4 RID: 9972
		int GetToken(MethodBase method, Type[] opt_param_types);

		// Token: 0x060026F5 RID: 9973
		int GetToken(SignatureHelper helper);
	}
}
