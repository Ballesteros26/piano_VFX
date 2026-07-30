using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000055 RID: 85
	internal class ParticleSystemExtensionsImpl
	{
		// Token: 0x060006C4 RID: 1732
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeCollisionEventSize")]
		[MethodImpl(4096)]
		internal static extern int GetSafeCollisionEventSize([NotNull] ParticleSystem ps);

		// Token: 0x060006C5 RID: 1733
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEventsDeprecated")]
		[MethodImpl(4096)]
		internal static extern int GetCollisionEventsDeprecated([NotNull] ParticleSystem ps, GameObject go, [Out] ParticleCollisionEvent[] collisionEvents);

		// Token: 0x060006C6 RID: 1734
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeTriggerParticlesSize")]
		[MethodImpl(4096)]
		internal static extern int GetSafeTriggerParticlesSize([NotNull] ParticleSystem ps, int type);

		// Token: 0x060006C7 RID: 1735
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEvents")]
		[MethodImpl(4096)]
		internal static extern int GetCollisionEvents([NotNull] ParticleSystem ps, [NotNull] GameObject go, [NotNull] List<ParticleCollisionEvent> collisionEvents);

		// Token: 0x060006C8 RID: 1736
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticles")]
		[MethodImpl(4096)]
		internal static extern int GetTriggerParticles([NotNull] ParticleSystem ps, int type, [NotNull] List<ParticleSystem.Particle> particles);

		// Token: 0x060006C9 RID: 1737
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTriggerParticles")]
		[MethodImpl(4096)]
		internal static extern void SetTriggerParticles([NotNull] ParticleSystem ps, int type, [NotNull] List<ParticleSystem.Particle> particles, int offset, int count);
	}
}
