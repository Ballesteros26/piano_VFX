using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime
{
	/// <summary>Checks for sufficient memory resources before executing an operation. This class cannot be inherited.</summary>
	// Token: 0x020006B8 RID: 1720
	public sealed class MemoryFailPoint : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.MemoryFailPoint" /> class, specifying the amount of memory required for successful execution. </summary>
		/// <param name="sizeInMegabytes">The required memory size, in megabytes. This must be a positive value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified memory size is negative.</exception>
		/// <exception cref="T:System.InsufficientMemoryException">There is insufficient memory to begin execution of the code protected by the gate.</exception>
		// Token: 0x06004968 RID: 18792 RVA: 0x001079D9 File Offset: 0x00105BD9
		[MonoTODO]
		public MemoryFailPoint(int sizeInMegabytes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x001079E8 File Offset: 0x00105BE8
		~MemoryFailPoint()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.MemoryFailPoint" />. </summary>
		// Token: 0x0600496A RID: 18794 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecuritySafeCritical]
		[MonoTODO]
		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
