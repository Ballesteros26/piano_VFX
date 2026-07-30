using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000024 RID: 36
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystemDescriptor.h")]
	[UsedByNativeCode]
	[NativeConditional("ENABLE_XR")]
	public class XRInputSubsystemDescriptor : IntegratedSubsystemDescriptor<XRInputSubsystem>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000124 RID: 292
		[NativeConditional("ENABLE_XR")]
		public extern bool disablesLegacyInput
		{
			[MethodImpl(4096)]
			get;
		}
	}
}
