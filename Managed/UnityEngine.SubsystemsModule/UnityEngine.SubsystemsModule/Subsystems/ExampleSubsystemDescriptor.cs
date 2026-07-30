using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Subsystems
{
	// Token: 0x02000011 RID: 17
	[UsedByNativeCode]
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystemDescriptor.h")]
	public class ExampleSubsystemDescriptor : IntegratedSubsystemDescriptor<ExampleSubsystem>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004B RID: 75
		public extern bool supportsEditorMode
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004C RID: 76
		public extern bool disableBackbufferMSAA
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004D RID: 77
		public extern bool stereoscopicBackbuffer
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004E RID: 78
		public extern bool usePBufferEGL
		{
			[MethodImpl(4096)]
			get;
		}
	}
}
