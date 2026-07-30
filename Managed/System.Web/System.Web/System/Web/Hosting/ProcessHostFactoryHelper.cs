using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides a method to retrieve an <see cref="T:System.Web.Hosting.IProcessHost" /> interface.</summary>
	// Token: 0x02000770 RID: 1904
	public sealed class ProcessHostFactoryHelper : MarshalByRefObject, IProcessHostFactoryHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.ProcessHostFactoryHelper" /> class.</summary>
		// Token: 0x06004D56 RID: 19798 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ProcessHostFactoryHelper()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the process host.</summary>
		/// <returns>An object that contains the process host.</returns>
		/// <param name="functions">Functions that are declared by the <see cref="T:System.Web.Hosting.IProcessHostSupportFunctions" /> interface.</param>
		// Token: 0x06004D57 RID: 19799 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public object GetProcessHost(IProcessHostSupportFunctions functions)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
