using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Subsystems
{
	// Token: 0x02000010 RID: 16
	[UsedByNativeCode]
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystem.h")]
	public class ExampleSubsystem : IntegratedSubsystem<ExampleSubsystemDescriptor>
	{
		// Token: 0x06000048 RID: 72
		[MethodImpl(4096)]
		public extern void PrintExample();

		// Token: 0x06000049 RID: 73
		[MethodImpl(4096)]
		public extern bool GetBool();
	}
}
