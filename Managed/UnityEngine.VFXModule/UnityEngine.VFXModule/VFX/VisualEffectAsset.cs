using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000013 RID: 19
	[NativeHeader("VFXScriptingClasses.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/VFX/Public/VisualEffectAsset.h")]
	public class VisualEffectAsset : VisualEffectObject
	{
		// Token: 0x06000094 RID: 148
		[FreeFunction(Name = "VisualEffectAssetBindings::GetTextureDimension", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern TextureDimension GetTextureDimension(int nameID);

		// Token: 0x06000095 RID: 149
		[FreeFunction(Name = "VisualEffectAssetBindings::GetExposedProperties", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetExposedProperties([NotNull] List<VFXExposedProperty> exposedProperties);

		// Token: 0x06000096 RID: 150
		[FreeFunction(Name = "VisualEffectAssetBindings::GetEvents", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetEvents([NotNull] List<string> names);

		// Token: 0x06000097 RID: 151 RVA: 0x00002844 File Offset: 0x00000A44
		public TextureDimension GetTextureDimension(string name)
		{
			return this.GetTextureDimension(Shader.PropertyToID(name));
		}

		// Token: 0x040000D8 RID: 216
		public const string PlayEventName = "OnPlay";

		// Token: 0x040000D9 RID: 217
		public const string StopEventName = "OnStop";

		// Token: 0x040000DA RID: 218
		public static readonly int PlayEventID = Shader.PropertyToID("OnPlay");

		// Token: 0x040000DB RID: 219
		public static readonly int StopEventID = Shader.PropertyToID("OnStop");
	}
}
