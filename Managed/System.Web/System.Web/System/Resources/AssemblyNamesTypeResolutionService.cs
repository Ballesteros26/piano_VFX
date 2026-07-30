using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x0200001C RID: 28
	internal sealed class AssemblyNamesTypeResolutionService : ITypeResolutionService
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002050 File Offset: 0x00000250
		public AssemblyNamesTypeResolutionService(AssemblyName[] names)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003A15 File Offset: 0x00001C15
		public Assembly GetAssembly(AssemblyName name)
		{
			return this.GetAssembly(name, true);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A1F File Offset: 0x00001C1F
		public Assembly GetAssembly(AssemblyName name, bool throwOnError)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A26 File Offset: 0x00001C26
		public Type GetType(string name)
		{
			return this.GetType(name, true);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003A30 File Offset: 0x00001C30
		public Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A3B File Offset: 0x00001C3B
		public Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			Type type = Type.GetType(name, false, ignoreCase);
			if (type == null && throwOnError)
			{
				throw new ArgumentException(string.Format("Could not find a type for a name. The type name was `{0}'", name));
			}
			return type;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003A1F File Offset: 0x00001C1F
		public void ReferenceAssembly(AssemblyName name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string GetPathOfAssembly(AssemblyName name)
		{
			throw new NotImplementedException();
		}
	}
}
