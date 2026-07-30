using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	/// <summary>Manages <see cref="T:System.Web.HttpWorkerRequest" /> objects in the .NET Framework. This class cannot be inherited.</summary>
	// Token: 0x02000555 RID: 1365
	public sealed class ISAPIRuntime : MarshalByRefObject, IISAPIRuntime, IRegisteredObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.ISAPIRuntime" /> class. </summary>
		// Token: 0x06003B05 RID: 15109 RVA: 0x00007A53 File Offset: 0x00005C53
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public ISAPIRuntime()
		{
		}

		/// <summary>Forces garbage collection.</summary>
		// Token: 0x06003B06 RID: 15110 RVA: 0x0000393A File Offset: 0x00001B3A
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public void DoGCCollect()
		{
		}

		/// <summary>Creates a new <see cref="T:System.Web.HttpWorkerRequest" /> object to process the current request.</summary>
		/// <returns>0 if <see cref="T:System.Web.HttpWorkerRequest" /> was created successfully; otherwise, 1.</returns>
		/// <param name="ecb">An ISAPI extension control block.</param>
		/// <param name="iWRType">0 to create an out-of-process request; otherwise, an in-process request is created.</param>
		// Token: 0x06003B07 RID: 15111 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public int ProcessRequest(IntPtr ecb, int iWRType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Starts processing all items in the worker process pipeline.</summary>
		// Token: 0x06003B08 RID: 15112 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public void StartProcessing()
		{
			throw new NotImplementedException();
		}

		/// <summary>Stops processing the items in the worker process pipeline.</summary>
		// Token: 0x06003B09 RID: 15113 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public void StopProcessing()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gives the <see cref="T:System.Web.Hosting.ISAPIRuntime" /> object an infinite lifetime by preventing a lease from being created. </summary>
		/// <returns>null to prevent a lease from being created.</returns>
		// Token: 0x06003B0A RID: 15114 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public override object InitializeLifetimeService()
		{
			throw new NotImplementedException();
		}

		/// <summary>Requests a registered object to unregister.</summary>
		/// <param name="immediate">true to indicate that the registered object should unregister from the hosting environment before returning; otherwise, false.</param>
		// Token: 0x06003B0B RID: 15115 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		void IRegisteredObject.Stop(bool immediate)
		{
			throw new NotImplementedException();
		}
	}
}
