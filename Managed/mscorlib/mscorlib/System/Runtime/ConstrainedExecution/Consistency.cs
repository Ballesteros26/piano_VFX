using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Specifies a reliability contract.</summary>
	// Token: 0x0200082E RID: 2094
	[Serializable]
	public enum Consistency
	{
		/// <summary>In the face of exceptional conditions, the CLR makes no guarantees regarding state consistency; that is, the condition might corrupt the process.</summary>
		// Token: 0x04002B6B RID: 11115
		MayCorruptProcess,
		/// <summary>In the face of exceptional conditions, the common language runtime (CLR) makes no guarantees regarding state consistency in the current application domain.</summary>
		// Token: 0x04002B6C RID: 11116
		MayCorruptAppDomain,
		/// <summary>In the face of exceptional conditions, the method is guaranteed to limit state corruption to the current instance.</summary>
		// Token: 0x04002B6D RID: 11117
		MayCorruptInstance,
		/// <summary>In the face of exceptional conditions, the method is guaranteed not to corrupt state. </summary>
		// Token: 0x04002B6E RID: 11118
		WillNotCorruptState
	}
}
