using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200036E RID: 878
	internal class ModuleBuilderTokenGenerator : TokenGenerator
	{
		// Token: 0x0600283D RID: 10301 RVA: 0x0008E863 File Offset: 0x0008CA63
		public ModuleBuilderTokenGenerator(ModuleBuilder mb)
		{
			this.mb = mb;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0008E872 File Offset: 0x0008CA72
		public int GetToken(string str)
		{
			return this.mb.GetToken(str);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0008E880 File Offset: 0x0008CA80
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.mb.GetToken(member, create_open_instance);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0008E88F File Offset: 0x0008CA8F
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			return this.mb.GetToken(method, opt_param_types);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0008E89E File Offset: 0x0008CA9E
		public int GetToken(SignatureHelper helper)
		{
			return this.mb.GetToken(helper);
		}

		// Token: 0x040014A2 RID: 5282
		private ModuleBuilder mb;
	}
}
