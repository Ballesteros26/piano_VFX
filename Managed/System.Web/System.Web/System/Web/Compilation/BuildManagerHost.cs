using System;
using System.Web.Hosting;

namespace System.Web.Compilation
{
	// Token: 0x0200063E RID: 1598
	internal class BuildManagerHost : MarshalByRefObject, IRegisteredObject
	{
		// Token: 0x060044CA RID: 17610 RVA: 0x000BCB6C File Offset: 0x000BAD6C
		protected void RegisterAssembly(string assemblyName, string assemblyLocation)
		{
			if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(assemblyLocation))
			{
				return;
			}
			HttpRuntime.RegisteredAssemblies.InsertOrUpdate((uint)assemblyName.GetHashCode(), assemblyName, assemblyLocation, assemblyLocation);
			HttpRuntime.EnableAssemblyMapping(true);
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Stop(bool immediate)
		{
		}
	}
}
