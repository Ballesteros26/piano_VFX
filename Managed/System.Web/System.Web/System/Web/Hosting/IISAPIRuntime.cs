using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Defines the methods that are used to create <see cref="T:System.Web.HttpWorkerRequest" /> objects in the .NET Framework.</summary>
	// Token: 0x02000554 RID: 1364
	[Guid("08A2C56F-7C16-41C1-A8BE-432917A1A2D1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IISAPIRuntime
	{
		/// <summary>Forces garbage collection.</summary>
		// Token: 0x06003B01 RID: 15105
		void DoGCCollect();

		/// <summary>Creates a new <see cref="T:System.Web.HttpWorkerRequest" /> object to process the current request.</summary>
		/// <returns>0 if <see cref="T:System.Web.HttpWorkerRequest" /> was created successfully; otherwise, 1.</returns>
		/// <param name="ecb">An ISAPI extension control block.</param>
		/// <param name="useProcessModel">0 to create an out-of-process request; otherwise, an in-process request is created.</param>
		// Token: 0x06003B02 RID: 15106
		[return: MarshalAs(UnmanagedType.I4)]
		int ProcessRequest([In] IntPtr ecb, [MarshalAs(UnmanagedType.I4)] [In] int useProcessModel);

		/// <summary>Starts processing all items in the worker process pipeline.</summary>
		// Token: 0x06003B03 RID: 15107
		void StartProcessing();

		/// <summary>Stops processing the items in the worker process pipeline.</summary>
		// Token: 0x06003B04 RID: 15108
		void StopProcessing();
	}
}
