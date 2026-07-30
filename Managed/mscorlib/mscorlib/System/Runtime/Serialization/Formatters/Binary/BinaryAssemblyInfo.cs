using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000708 RID: 1800
	internal sealed class BinaryAssemblyInfo
	{
		// Token: 0x06004B65 RID: 19301 RVA: 0x0010CF2A File Offset: 0x0010B12A
		internal BinaryAssemblyInfo(string assemblyString)
		{
			this.assemblyString = assemblyString;
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x0010CF39 File Offset: 0x0010B139
		internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
		{
			this.assemblyString = assemblyString;
			this.assembly = assembly;
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x0010CF50 File Offset: 0x0010B150
		internal Assembly GetAssembly()
		{
			if (this.assembly == null)
			{
				this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
				if (this.assembly == null)
				{
					throw new SerializationException(Environment.GetResourceString("Unable to find assembly '{0}'.", new object[] { this.assemblyString }));
				}
			}
			return this.assembly;
		}

		// Token: 0x0400274E RID: 10062
		internal string assemblyString;

		// Token: 0x0400274F RID: 10063
		private Assembly assembly;
	}
}
