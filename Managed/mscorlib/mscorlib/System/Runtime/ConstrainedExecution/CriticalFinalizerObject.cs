using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Ensures that all finalization code in derived classes is marked as critical.</summary>
	// Token: 0x0200082C RID: 2092
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalFinalizerObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ConstrainedExecution.CriticalFinalizerObject" /> class.</summary>
		// Token: 0x06005387 RID: 21383 RVA: 0x00002111 File Offset: 0x00000311
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalFinalizerObject()
		{
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x00125978 File Offset: 0x00123B78
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalFinalizerObject()
		{
		}
	}
}
