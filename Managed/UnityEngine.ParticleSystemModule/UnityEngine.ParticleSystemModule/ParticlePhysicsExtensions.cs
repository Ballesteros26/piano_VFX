using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	public static class ParticlePhysicsExtensions
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x0000599C File Offset: 0x00003B9C
		[Obsolete("GetCollisionEvents function using ParticleCollisionEvent[] is deprecated. Use List<ParticleCollisionEvent> instead.", false)]
		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, ParticleCollisionEvent[] collisionEvents)
		{
			bool flag = go == null;
			if (flag)
			{
				throw new ArgumentNullException("go");
			}
			bool flag2 = collisionEvents == null;
			if (flag2)
			{
				throw new ArgumentNullException("collisionEvents");
			}
			return ParticleSystemExtensionsImpl.GetCollisionEventsDeprecated(ps, go, collisionEvents);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000059E0 File Offset: 0x00003BE0
		public static int GetSafeCollisionEventSize(this ParticleSystem ps)
		{
			return ParticleSystemExtensionsImpl.GetSafeCollisionEventSize(ps);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000059F8 File Offset: 0x00003BF8
		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, List<ParticleCollisionEvent> collisionEvents)
		{
			return ParticleSystemExtensionsImpl.GetCollisionEvents(ps, go, collisionEvents);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00005A14 File Offset: 0x00003C14
		public static int GetSafeTriggerParticlesSize(this ParticleSystem ps, ParticleSystemTriggerEventType type)
		{
			return ParticleSystemExtensionsImpl.GetSafeTriggerParticlesSize(ps, (int)type);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00005A30 File Offset: 0x00003C30
		public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			return ParticleSystemExtensionsImpl.GetTriggerParticles(ps, (int)type, particles);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00005A4C File Offset: 0x00003C4C
		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, int offset, int count)
		{
			bool flag = particles == null;
			if (flag)
			{
				throw new ArgumentNullException("particles");
			}
			bool flag2 = offset >= particles.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("offset", "offset should be smaller than the size of the particles list.");
			}
			bool flag3 = offset + count >= particles.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("count", "offset+count should be smaller than the size of the particles list.");
			}
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, offset, count);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00005ABC File Offset: 0x00003CBC
		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, 0, particles.Count);
		}
	}
}
