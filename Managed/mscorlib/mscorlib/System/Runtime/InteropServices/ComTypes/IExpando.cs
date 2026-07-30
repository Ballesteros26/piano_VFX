using System;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200097E RID: 2430
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IExpando : IReflect
	{
		// Token: 0x060059C9 RID: 22985
		FieldInfo AddField(string name);

		// Token: 0x060059CA RID: 22986
		PropertyInfo AddProperty(string name);

		// Token: 0x060059CB RID: 22987
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x060059CC RID: 22988
		void RemoveMember(MemberInfo m);
	}
}
