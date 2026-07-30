using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/ParticleSystem/ParticleSystemGeometryJob.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemModulesScriptBindings.h")]
	[UsedByNativeCode]
	public sealed class ParticleSystem : Component
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("Emit with specific parameters is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color)
		{
			ParticleSystem.Particle particle = default(ParticleSystem.Particle);
			particle.position = position;
			particle.velocity = velocity;
			particle.lifetime = lifetime;
			particle.startLifetime = lifetime;
			particle.startSize = size;
			particle.rotation3D = Vector3.zero;
			particle.angularVelocity3D = Vector3.zero;
			particle.startColor = color;
			particle.randomSeed = 5U;
			this.EmitOld_Internal(ref particle);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020CB File Offset: 0x000002CB
		[Obsolete("Emit with a single particle structure is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(ParticleSystem.Particle particle)
		{
			this.EmitOld_Internal(ref particle);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		[Obsolete("startDelay property is deprecated. Use main.startDelay or main.startDelayMultiplier instead.", false)]
		public float startDelay
		{
			get
			{
				return this.main.startDelayMultiplier;
			}
			set
			{
				this.main.startDelayMultiplier = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002118 File Offset: 0x00000318
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002138 File Offset: 0x00000338
		[Obsolete("loop property is deprecated. Use main.loop instead.", false)]
		public bool loop
		{
			get
			{
				return this.main.loop;
			}
			set
			{
				this.main.loop = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002158 File Offset: 0x00000358
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		[Obsolete("playOnAwake property is deprecated. Use main.playOnAwake instead.", false)]
		public bool playOnAwake
		{
			get
			{
				return this.main.playOnAwake;
			}
			set
			{
				this.main.playOnAwake = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002198 File Offset: 0x00000398
		[Obsolete("duration property is deprecated. Use main.duration instead.", false)]
		public float duration
		{
			get
			{
				return this.main.duration;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021B8 File Offset: 0x000003B8
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021D8 File Offset: 0x000003D8
		[Obsolete("playbackSpeed property is deprecated. Use main.simulationSpeed instead.", false)]
		public float playbackSpeed
		{
			get
			{
				return this.main.simulationSpeed;
			}
			set
			{
				this.main.simulationSpeed = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002218 File Offset: 0x00000418
		[Obsolete("enableEmission property is deprecated. Use emission.enabled instead.", false)]
		public bool enableEmission
		{
			get
			{
				return this.emission.enabled;
			}
			set
			{
				this.emission.enabled = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002238 File Offset: 0x00000438
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002258 File Offset: 0x00000458
		[Obsolete("emissionRate property is deprecated. Use emission.rateOverTime, emission.rateOverDistance, emission.rateOverTimeMultiplier or emission.rateOverDistanceMultiplier instead.", false)]
		public float emissionRate
		{
			get
			{
				return this.emission.rateOverTimeMultiplier;
			}
			set
			{
				this.emission.rateOverTime = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000227C File Offset: 0x0000047C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000229C File Offset: 0x0000049C
		[Obsolete("startSpeed property is deprecated. Use main.startSpeed or main.startSpeedMultiplier instead.", false)]
		public float startSpeed
		{
			get
			{
				return this.main.startSpeedMultiplier;
			}
			set
			{
				this.main.startSpeedMultiplier = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022BC File Offset: 0x000004BC
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022DC File Offset: 0x000004DC
		[Obsolete("startSize property is deprecated. Use main.startSize or main.startSizeMultiplier instead.", false)]
		public float startSize
		{
			get
			{
				return this.main.startSizeMultiplier;
			}
			set
			{
				this.main.startSizeMultiplier = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002324 File Offset: 0x00000524
		[Obsolete("startColor property is deprecated. Use main.startColor instead.", false)]
		public Color startColor
		{
			get
			{
				return this.main.startColor.color;
			}
			set
			{
				this.main.startColor = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002348 File Offset: 0x00000548
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002368 File Offset: 0x00000568
		[Obsolete("startRotation property is deprecated. Use main.startRotation or main.startRotationMultiplier instead.", false)]
		public float startRotation
		{
			get
			{
				return this.main.startRotationMultiplier;
			}
			set
			{
				this.main.startRotationMultiplier = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000023CC File Offset: 0x000005CC
		[Obsolete("startRotation3D property is deprecated. Use main.startRotationX, main.startRotationY and main.startRotationZ instead. (Or main.startRotationXMultiplier, main.startRotationYMultiplier and main.startRotationZMultiplier).", false)]
		public Vector3 startRotation3D
		{
			get
			{
				return new Vector3(this.main.startRotationXMultiplier, this.main.startRotationYMultiplier, this.main.startRotationZMultiplier);
			}
			set
			{
				ParticleSystem.MainModule main = this.main;
				main.startRotationXMultiplier = value.x;
				main.startRotationYMultiplier = value.y;
				main.startRotationZMultiplier = value.z;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000242C File Offset: 0x0000062C
		[Obsolete("startLifetime property is deprecated. Use main.startLifetime or main.startLifetimeMultiplier instead.", false)]
		public float startLifetime
		{
			get
			{
				return this.main.startLifetimeMultiplier;
			}
			set
			{
				this.main.startLifetimeMultiplier = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000244C File Offset: 0x0000064C
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000246C File Offset: 0x0000066C
		[Obsolete("gravityModifier property is deprecated. Use main.gravityModifier or main.gravityModifierMultiplier instead.", false)]
		public float gravityModifier
		{
			get
			{
				return this.main.gravityModifierMultiplier;
			}
			set
			{
				this.main.gravityModifierMultiplier = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000024AC File Offset: 0x000006AC
		[Obsolete("maxParticles property is deprecated. Use main.maxParticles instead.", false)]
		public int maxParticles
		{
			get
			{
				return this.main.maxParticles;
			}
			set
			{
				this.main.maxParticles = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024CC File Offset: 0x000006CC
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000024EC File Offset: 0x000006EC
		[Obsolete("simulationSpace property is deprecated. Use main.simulationSpace instead.", false)]
		public ParticleSystemSimulationSpace simulationSpace
		{
			get
			{
				return this.main.simulationSpace;
			}
			set
			{
				this.main.simulationSpace = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000250C File Offset: 0x0000070C
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000252C File Offset: 0x0000072C
		[Obsolete("scalingMode property is deprecated. Use main.scalingMode instead.", false)]
		public ParticleSystemScalingMode scalingMode
		{
			get
			{
				return this.main.scalingMode;
			}
			set
			{
				this.main.scalingMode = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000254C File Offset: 0x0000074C
		[Obsolete("automaticCullingEnabled property is deprecated. Use proceduralSimulationSupported instead (UnityUpgradable) -> proceduralSimulationSupported", true)]
		public bool automaticCullingEnabled
		{
			get
			{
				return this.proceduralSimulationSupported;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37
		public extern bool isPlaying
		{
			[NativeName("SyncJobs(false)->IsPlaying")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000026 RID: 38
		public extern bool isEmitting
		{
			[NativeName("SyncJobs(false)->IsEmitting")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000027 RID: 39
		public extern bool isStopped
		{
			[NativeName("SyncJobs(false)->IsStopped")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000028 RID: 40
		public extern bool isPaused
		{
			[NativeName("SyncJobs(false)->IsPaused")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000029 RID: 41
		public extern int particleCount
		{
			[NativeName("SyncJobs(false)->GetParticleCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002A RID: 42
		// (set) Token: 0x0600002B RID: 43
		public extern float time
		{
			[NativeName("SyncJobs(false)->GetSecPosition")]
			[MethodImpl(4096)]
			get;
			[NativeName("SyncJobs(false)->SetSecPosition")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002C RID: 44
		// (set) Token: 0x0600002D RID: 45
		public extern uint randomSeed
		{
			[NativeName("GetRandomSeed")]
			[MethodImpl(4096)]
			get;
			[NativeName("SyncJobs(false)->SetRandomSeed")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002E RID: 46
		// (set) Token: 0x0600002F RID: 47
		public extern bool useAutoRandomSeed
		{
			[NativeName("GetAutoRandomSeed")]
			[MethodImpl(4096)]
			get;
			[NativeName("SyncJobs(false)->SetAutoRandomSeed")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000030 RID: 48
		public extern bool proceduralSimulationSupported
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000031 RID: 49
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern float GetParticleCurrentSize(ref ParticleSystem.Particle particle);

		// Token: 0x06000032 RID: 50 RVA: 0x00002564 File Offset: 0x00000764
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize3D", HasExplicitThis = true)]
		internal Vector3 GetParticleCurrentSize3D(ref ParticleSystem.Particle particle)
		{
			Vector3 vector;
			this.GetParticleCurrentSize3D_Injected(ref particle, out vector);
			return vector;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000257C File Offset: 0x0000077C
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentColor", HasExplicitThis = true)]
		internal Color32 GetParticleCurrentColor(ref ParticleSystem.Particle particle)
		{
			Color32 color;
			this.GetParticleCurrentColor_Injected(ref particle, out color);
			return color;
		}

		// Token: 0x06000034 RID: 52
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleMeshIndex", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern int GetParticleMeshIndex(ref ParticleSystem.Particle particle);

		// Token: 0x06000035 RID: 53
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticles", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetParticles([Out] ParticleSystem.Particle[] particles, int size, int offset);

		// Token: 0x06000036 RID: 54 RVA: 0x00002593 File Offset: 0x00000793
		public void SetParticles([Out] ParticleSystem.Particle[] particles, int size)
		{
			this.SetParticles(particles, size, 0);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025A0 File Offset: 0x000007A0
		public void SetParticles([Out] ParticleSystem.Particle[] particles)
		{
			this.SetParticles(particles, -1);
		}

		// Token: 0x06000038 RID: 56
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset);

		// Token: 0x06000039 RID: 57 RVA: 0x000025AC File Offset: 0x000007AC
		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size, int offset)
		{
			this.SetParticlesWithNativeArray((IntPtr)particles.GetUnsafeReadOnlyPtr<ParticleSystem.Particle>(), particles.Length, size, 0);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000025CA File Offset: 0x000007CA
		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size)
		{
			this.SetParticles(particles, size, 0);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000025D7 File Offset: 0x000007D7
		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles)
		{
			this.SetParticles(particles, -1);
		}

		// Token: 0x0600003C RID: 60
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticles", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern int GetParticles([NotNull] [Out] ParticleSystem.Particle[] particles, int size, int offset);

		// Token: 0x0600003D RID: 61 RVA: 0x000025E4 File Offset: 0x000007E4
		public int GetParticles([Out] ParticleSystem.Particle[] particles, int size)
		{
			return this.GetParticles(particles, size, 0);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002600 File Offset: 0x00000800
		public int GetParticles([Out] ParticleSystem.Particle[] particles)
		{
			return this.GetParticles(particles, -1);
		}

		// Token: 0x0600003F RID: 63
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern int GetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset);

		// Token: 0x06000040 RID: 64 RVA: 0x0000261C File Offset: 0x0000081C
		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size, int offset)
		{
			return this.GetParticlesWithNativeArray((IntPtr)particles.GetUnsafeReadOnlyPtr<ParticleSystem.Particle>(), particles.Length, size, 0);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002648 File Offset: 0x00000848
		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size)
		{
			return this.GetParticles(particles, size, 0);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002664 File Offset: 0x00000864
		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles)
		{
			return this.GetParticles(particles, -1);
		}

		// Token: 0x06000043 RID: 67
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetCustomParticleData([NotNull] List<Vector4> customData, ParticleSystemCustomData streamIndex);

		// Token: 0x06000044 RID: 68
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern int GetCustomParticleData([NotNull] List<Vector4> customData, ParticleSystemCustomData streamIndex);

		// Token: 0x06000045 RID: 69 RVA: 0x00002680 File Offset: 0x00000880
		public ParticleSystem.PlaybackState GetPlaybackState()
		{
			ParticleSystem.PlaybackState playbackState;
			this.GetPlaybackState_Injected(out playbackState);
			return playbackState;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002696 File Offset: 0x00000896
		public void SetPlaybackState(ParticleSystem.PlaybackState playbackState)
		{
			this.SetPlaybackState_Injected(ref playbackState);
		}

		// Token: 0x06000047 RID: 71
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTrailData", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetTrailDataInternal(ref ParticleSystem.Trails trailData);

		// Token: 0x06000048 RID: 72 RVA: 0x000026A0 File Offset: 0x000008A0
		public ParticleSystem.Trails GetTrails()
		{
			ParticleSystem.Trails trails = new ParticleSystem.Trails
			{
				positions = new List<Vector4>(),
				frontPositions = new List<int>(),
				backPositions = new List<int>(),
				positionCounts = new List<int>()
			};
			this.GetTrailDataInternal(ref trails);
			return trails;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000026F6 File Offset: 0x000008F6
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTrailData", HasExplicitThis = true)]
		public void SetTrails(ParticleSystem.Trails trailData)
		{
			this.SetTrails_Injected(ref trailData);
		}

		// Token: 0x0600004A RID: 74
		[FreeFunction(Name = "ParticleSystemScriptBindings::Simulate", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Simulate(float t, bool withChildren, bool restart, bool fixedTimeStep);

		// Token: 0x0600004B RID: 75 RVA: 0x00002700 File Offset: 0x00000900
		public void Simulate(float t, bool withChildren, bool restart)
		{
			this.Simulate(t, withChildren, restart, true);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000270E File Offset: 0x0000090E
		public void Simulate(float t, bool withChildren)
		{
			this.Simulate(t, withChildren, true);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000271B File Offset: 0x0000091B
		public void Simulate(float t)
		{
			this.Simulate(t, true);
		}

		// Token: 0x0600004E RID: 78
		[FreeFunction(Name = "ParticleSystemScriptBindings::Play", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Play(bool withChildren);

		// Token: 0x0600004F RID: 79 RVA: 0x00002727 File Offset: 0x00000927
		public void Play()
		{
			this.Play(true);
		}

		// Token: 0x06000050 RID: 80
		[FreeFunction(Name = "ParticleSystemScriptBindings::Pause", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Pause(bool withChildren);

		// Token: 0x06000051 RID: 81 RVA: 0x00002732 File Offset: 0x00000932
		public void Pause()
		{
			this.Pause(true);
		}

		// Token: 0x06000052 RID: 82
		[FreeFunction(Name = "ParticleSystemScriptBindings::Stop", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Stop(bool withChildren, ParticleSystemStopBehavior stopBehavior);

		// Token: 0x06000053 RID: 83 RVA: 0x0000273D File Offset: 0x0000093D
		public void Stop(bool withChildren)
		{
			this.Stop(withChildren, ParticleSystemStopBehavior.StopEmitting);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002749 File Offset: 0x00000949
		public void Stop()
		{
			this.Stop(true);
		}

		// Token: 0x06000055 RID: 85
		[FreeFunction(Name = "ParticleSystemScriptBindings::Clear", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Clear(bool withChildren);

		// Token: 0x06000056 RID: 86 RVA: 0x00002754 File Offset: 0x00000954
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x06000057 RID: 87
		[FreeFunction(Name = "ParticleSystemScriptBindings::IsAlive", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsAlive(bool withChildren);

		// Token: 0x06000058 RID: 88 RVA: 0x00002760 File Offset: 0x00000960
		public bool IsAlive()
		{
			return this.IsAlive(true);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002779 File Offset: 0x00000979
		[RequiredByNativeCode]
		public void Emit(int count)
		{
			this.Emit_Internal(count);
		}

		// Token: 0x0600005A RID: 90
		[NativeName("SyncJobs()->Emit")]
		[MethodImpl(4096)]
		private extern void Emit_Internal(int count);

		// Token: 0x0600005B RID: 91 RVA: 0x00002784 File Offset: 0x00000984
		[NativeName("SyncJobs()->EmitParticlesExternal")]
		public void Emit(ParticleSystem.EmitParams emitParams, int count)
		{
			this.Emit_Injected(ref emitParams, count);
		}

		// Token: 0x0600005C RID: 92
		[NativeName("SyncJobs()->EmitParticleExternal")]
		[MethodImpl(4096)]
		private extern void EmitOld_Internal(ref ParticleSystem.Particle particle);

		// Token: 0x0600005D RID: 93 RVA: 0x0000278F File Offset: 0x0000098F
		public void TriggerSubEmitter(int subEmitterIndex)
		{
			this.TriggerSubEmitter(subEmitterIndex, null);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000279B File Offset: 0x0000099B
		public void TriggerSubEmitter(int subEmitterIndex, ref ParticleSystem.Particle particle)
		{
			this.TriggerSubEmitterForParticle(subEmitterIndex, particle);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000027AC File Offset: 0x000009AC
		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitterForParticle", HasExplicitThis = true)]
		internal void TriggerSubEmitterForParticle(int subEmitterIndex, ParticleSystem.Particle particle)
		{
			this.TriggerSubEmitterForParticle_Injected(subEmitterIndex, ref particle);
		}

		// Token: 0x06000060 RID: 96
		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitter", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void TriggerSubEmitter(int subEmitterIndex, List<ParticleSystem.Particle> particles);

		// Token: 0x06000061 RID: 97
		[FreeFunction(Name = "ParticleSystemGeometryJob::ResetPreMappedBufferMemory")]
		[MethodImpl(4096)]
		public static extern void ResetPreMappedBufferMemory();

		// Token: 0x06000062 RID: 98
		[MethodImpl(4096)]
		internal unsafe extern void* GetManagedJobData();

		// Token: 0x06000063 RID: 99 RVA: 0x000027B8 File Offset: 0x000009B8
		internal JobHandle GetManagedJobHandle()
		{
			JobHandle jobHandle;
			this.GetManagedJobHandle_Injected(out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000027CE File Offset: 0x000009CE
		internal void SetManagedJobHandle(JobHandle handle)
		{
			this.SetManagedJobHandle_Injected(ref handle);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000027D8 File Offset: 0x000009D8
		[FreeFunction("ScheduleManagedJob", ThrowsException = true)]
		internal unsafe static JobHandle ScheduleManagedJob(ref JobsUtility.JobScheduleParameters parameters, void* additionalData)
		{
			JobHandle jobHandle;
			ParticleSystem.ScheduleManagedJob_Injected(ref parameters, additionalData, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000066 RID: 102
		[ThreadSafe]
		[MethodImpl(4096)]
		internal unsafe static extern void CopyManagedJobData(void* systemPtr, out NativeParticleData particleData);

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000027F0 File Offset: 0x000009F0
		public ParticleSystem.MainModule main
		{
			get
			{
				return new ParticleSystem.MainModule(this);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002808 File Offset: 0x00000A08
		public ParticleSystem.EmissionModule emission
		{
			get
			{
				return new ParticleSystem.EmissionModule(this);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002820 File Offset: 0x00000A20
		public ParticleSystem.ShapeModule shape
		{
			get
			{
				return new ParticleSystem.ShapeModule(this);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002838 File Offset: 0x00000A38
		public ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime
		{
			get
			{
				return new ParticleSystem.VelocityOverLifetimeModule(this);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002850 File Offset: 0x00000A50
		public ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime
		{
			get
			{
				return new ParticleSystem.LimitVelocityOverLifetimeModule(this);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002868 File Offset: 0x00000A68
		public ParticleSystem.InheritVelocityModule inheritVelocity
		{
			get
			{
				return new ParticleSystem.InheritVelocityModule(this);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002880 File Offset: 0x00000A80
		public ParticleSystem.LifetimeByEmitterSpeedModule lifetimeByEmitterSpeed
		{
			get
			{
				return new ParticleSystem.LifetimeByEmitterSpeedModule(this);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002898 File Offset: 0x00000A98
		public ParticleSystem.ForceOverLifetimeModule forceOverLifetime
		{
			get
			{
				return new ParticleSystem.ForceOverLifetimeModule(this);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000028B0 File Offset: 0x00000AB0
		public ParticleSystem.ColorOverLifetimeModule colorOverLifetime
		{
			get
			{
				return new ParticleSystem.ColorOverLifetimeModule(this);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000028C8 File Offset: 0x00000AC8
		public ParticleSystem.ColorBySpeedModule colorBySpeed
		{
			get
			{
				return new ParticleSystem.ColorBySpeedModule(this);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000028E0 File Offset: 0x00000AE0
		public ParticleSystem.SizeOverLifetimeModule sizeOverLifetime
		{
			get
			{
				return new ParticleSystem.SizeOverLifetimeModule(this);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000028F8 File Offset: 0x00000AF8
		public ParticleSystem.SizeBySpeedModule sizeBySpeed
		{
			get
			{
				return new ParticleSystem.SizeBySpeedModule(this);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002910 File Offset: 0x00000B10
		public ParticleSystem.RotationOverLifetimeModule rotationOverLifetime
		{
			get
			{
				return new ParticleSystem.RotationOverLifetimeModule(this);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002928 File Offset: 0x00000B28
		public ParticleSystem.RotationBySpeedModule rotationBySpeed
		{
			get
			{
				return new ParticleSystem.RotationBySpeedModule(this);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002940 File Offset: 0x00000B40
		public ParticleSystem.ExternalForcesModule externalForces
		{
			get
			{
				return new ParticleSystem.ExternalForcesModule(this);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002958 File Offset: 0x00000B58
		public ParticleSystem.NoiseModule noise
		{
			get
			{
				return new ParticleSystem.NoiseModule(this);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002970 File Offset: 0x00000B70
		public ParticleSystem.CollisionModule collision
		{
			get
			{
				return new ParticleSystem.CollisionModule(this);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002988 File Offset: 0x00000B88
		public ParticleSystem.TriggerModule trigger
		{
			get
			{
				return new ParticleSystem.TriggerModule(this);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000029A0 File Offset: 0x00000BA0
		public ParticleSystem.SubEmittersModule subEmitters
		{
			get
			{
				return new ParticleSystem.SubEmittersModule(this);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000029B8 File Offset: 0x00000BB8
		public ParticleSystem.TextureSheetAnimationModule textureSheetAnimation
		{
			get
			{
				return new ParticleSystem.TextureSheetAnimationModule(this);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000029D0 File Offset: 0x00000BD0
		public ParticleSystem.LightsModule lights
		{
			get
			{
				return new ParticleSystem.LightsModule(this);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000029E8 File Offset: 0x00000BE8
		public ParticleSystem.TrailModule trails
		{
			get
			{
				return new ParticleSystem.TrailModule(this);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002A00 File Offset: 0x00000C00
		public ParticleSystem.CustomDataModule customData
		{
			get
			{
				return new ParticleSystem.CustomDataModule(this);
			}
		}

		// Token: 0x0600007F RID: 127
		[MethodImpl(4096)]
		private extern void GetParticleCurrentSize3D_Injected(ref ParticleSystem.Particle particle, out Vector3 ret);

		// Token: 0x06000080 RID: 128
		[MethodImpl(4096)]
		private extern void GetParticleCurrentColor_Injected(ref ParticleSystem.Particle particle, out Color32 ret);

		// Token: 0x06000081 RID: 129
		[MethodImpl(4096)]
		private extern void GetPlaybackState_Injected(out ParticleSystem.PlaybackState ret);

		// Token: 0x06000082 RID: 130
		[MethodImpl(4096)]
		private extern void SetPlaybackState_Injected(ref ParticleSystem.PlaybackState playbackState);

		// Token: 0x06000083 RID: 131
		[MethodImpl(4096)]
		private extern void SetTrails_Injected(ref ParticleSystem.Trails trailData);

		// Token: 0x06000084 RID: 132
		[MethodImpl(4096)]
		private extern void Emit_Injected(ref ParticleSystem.EmitParams emitParams, int count);

		// Token: 0x06000085 RID: 133
		[MethodImpl(4096)]
		private extern void TriggerSubEmitterForParticle_Injected(int subEmitterIndex, ref ParticleSystem.Particle particle);

		// Token: 0x06000086 RID: 134
		[MethodImpl(4096)]
		private extern void GetManagedJobHandle_Injected(out JobHandle ret);

		// Token: 0x06000087 RID: 135
		[MethodImpl(4096)]
		private extern void SetManagedJobHandle_Injected(ref JobHandle handle);

		// Token: 0x06000088 RID: 136
		[MethodImpl(4096)]
		private unsafe static extern void ScheduleManagedJob_Injected(ref JobsUtility.JobScheduleParameters parameters, void* additionalData, out JobHandle ret);

		// Token: 0x02000004 RID: 4
		public struct MainModule
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000089 RID: 137 RVA: 0x00002A24 File Offset: 0x00000C24
			// (set) Token: 0x0600008A RID: 138 RVA: 0x00002A3C File Offset: 0x00000C3C
			[Obsolete("Please use flipRotation instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/MainModule.flipRotation", false)]
			public float randomizeRotationDirection
			{
				get
				{
					return this.flipRotation;
				}
				set
				{
					this.flipRotation = value;
				}
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00002A47 File Offset: 0x00000C47
			internal MainModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x0600008C RID: 140 RVA: 0x00002A51 File Offset: 0x00000C51
			// (set) Token: 0x0600008D RID: 141 RVA: 0x00002A59 File Offset: 0x00000C59
			public float duration
			{
				get
				{
					return ParticleSystem.MainModule.get_duration_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_duration_Injected(ref this, value);
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x0600008E RID: 142 RVA: 0x00002A62 File Offset: 0x00000C62
			// (set) Token: 0x0600008F RID: 143 RVA: 0x00002A6A File Offset: 0x00000C6A
			public bool loop
			{
				get
				{
					return ParticleSystem.MainModule.get_loop_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_loop_Injected(ref this, value);
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00002A73 File Offset: 0x00000C73
			// (set) Token: 0x06000091 RID: 145 RVA: 0x00002A7B File Offset: 0x00000C7B
			public bool prewarm
			{
				get
				{
					return ParticleSystem.MainModule.get_prewarm_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_prewarm_Injected(ref this, value);
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000092 RID: 146 RVA: 0x00002A84 File Offset: 0x00000C84
			// (set) Token: 0x06000093 RID: 147 RVA: 0x00002A9A File Offset: 0x00000C9A
			public ParticleSystem.MinMaxCurve startDelay
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startDelay_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startDelay_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000094 RID: 148 RVA: 0x00002AA4 File Offset: 0x00000CA4
			// (set) Token: 0x06000095 RID: 149 RVA: 0x00002AAC File Offset: 0x00000CAC
			public float startDelayMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startDelayMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startDelayMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000096 RID: 150 RVA: 0x00002AB8 File Offset: 0x00000CB8
			// (set) Token: 0x06000097 RID: 151 RVA: 0x00002ACE File Offset: 0x00000CCE
			public ParticleSystem.MinMaxCurve startLifetime
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startLifetime_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startLifetime_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000098 RID: 152 RVA: 0x00002AD8 File Offset: 0x00000CD8
			// (set) Token: 0x06000099 RID: 153 RVA: 0x00002AE0 File Offset: 0x00000CE0
			public float startLifetimeMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startLifetimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startLifetimeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x0600009A RID: 154 RVA: 0x00002AEC File Offset: 0x00000CEC
			// (set) Token: 0x0600009B RID: 155 RVA: 0x00002B02 File Offset: 0x00000D02
			public ParticleSystem.MinMaxCurve startSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startSpeed_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x0600009C RID: 156 RVA: 0x00002B0C File Offset: 0x00000D0C
			// (set) Token: 0x0600009D RID: 157 RVA: 0x00002B14 File Offset: 0x00000D14
			public float startSpeedMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x0600009E RID: 158 RVA: 0x00002B1D File Offset: 0x00000D1D
			// (set) Token: 0x0600009F RID: 159 RVA: 0x00002B25 File Offset: 0x00000D25
			public bool startSize3D
			{
				get
				{
					return ParticleSystem.MainModule.get_startSize3D_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSize3D_Injected(ref this, value);
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002B30 File Offset: 0x00000D30
			// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002B46 File Offset: 0x00000D46
			[NativeName("StartSizeX")]
			public ParticleSystem.MinMaxCurve startSize
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startSize_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSize_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002B50 File Offset: 0x00000D50
			// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002B58 File Offset: 0x00000D58
			[NativeName("StartSizeXMultiplier")]
			public float startSizeMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002B64 File Offset: 0x00000D64
			// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002B7A File Offset: 0x00000D7A
			public ParticleSystem.MinMaxCurve startSizeX
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startSizeX_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeX_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002B84 File Offset: 0x00000D84
			// (set) Token: 0x060000A7 RID: 167 RVA: 0x00002B8C File Offset: 0x00000D8C
			public float startSizeXMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002B98 File Offset: 0x00000D98
			// (set) Token: 0x060000A9 RID: 169 RVA: 0x00002BAE File Offset: 0x00000DAE
			public ParticleSystem.MinMaxCurve startSizeY
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startSizeY_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeY_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000AA RID: 170 RVA: 0x00002BB8 File Offset: 0x00000DB8
			// (set) Token: 0x060000AB RID: 171 RVA: 0x00002BC0 File Offset: 0x00000DC0
			public float startSizeYMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000AC RID: 172 RVA: 0x00002BCC File Offset: 0x00000DCC
			// (set) Token: 0x060000AD RID: 173 RVA: 0x00002BE2 File Offset: 0x00000DE2
			public ParticleSystem.MinMaxCurve startSizeZ
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startSizeZ_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000AE RID: 174 RVA: 0x00002BEC File Offset: 0x00000DEC
			// (set) Token: 0x060000AF RID: 175 RVA: 0x00002BF4 File Offset: 0x00000DF4
			public float startSizeZMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002BFD File Offset: 0x00000DFD
			// (set) Token: 0x060000B1 RID: 177 RVA: 0x00002C05 File Offset: 0x00000E05
			public bool startRotation3D
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotation3D_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotation3D_Injected(ref this, value);
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002C10 File Offset: 0x00000E10
			// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002C26 File Offset: 0x00000E26
			[NativeName("StartRotationZ")]
			public ParticleSystem.MinMaxCurve startRotation
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startRotation_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotation_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002C30 File Offset: 0x00000E30
			// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002C38 File Offset: 0x00000E38
			[NativeName("StartRotationZMultiplier")]
			public float startRotationMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002C44 File Offset: 0x00000E44
			// (set) Token: 0x060000B7 RID: 183 RVA: 0x00002C5A File Offset: 0x00000E5A
			public ParticleSystem.MinMaxCurve startRotationX
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startRotationX_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationX_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002C64 File Offset: 0x00000E64
			// (set) Token: 0x060000B9 RID: 185 RVA: 0x00002C6C File Offset: 0x00000E6C
			public float startRotationXMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060000BA RID: 186 RVA: 0x00002C78 File Offset: 0x00000E78
			// (set) Token: 0x060000BB RID: 187 RVA: 0x00002C8E File Offset: 0x00000E8E
			public ParticleSystem.MinMaxCurve startRotationY
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startRotationY_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationY_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000BC RID: 188 RVA: 0x00002C98 File Offset: 0x00000E98
			// (set) Token: 0x060000BD RID: 189 RVA: 0x00002CA0 File Offset: 0x00000EA0
			public float startRotationYMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000BE RID: 190 RVA: 0x00002CAC File Offset: 0x00000EAC
			// (set) Token: 0x060000BF RID: 191 RVA: 0x00002CC2 File Offset: 0x00000EC2
			public ParticleSystem.MinMaxCurve startRotationZ
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_startRotationZ_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002CCC File Offset: 0x00000ECC
			// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public float startRotationZMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002CDD File Offset: 0x00000EDD
			// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002CE5 File Offset: 0x00000EE5
			public float flipRotation
			{
				get
				{
					return ParticleSystem.MainModule.get_flipRotation_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_flipRotation_Injected(ref this, value);
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002CF0 File Offset: 0x00000EF0
			// (set) Token: 0x060000C5 RID: 197 RVA: 0x00002D06 File Offset: 0x00000F06
			public ParticleSystem.MinMaxGradient startColor
			{
				get
				{
					ParticleSystem.MinMaxGradient minMaxGradient;
					ParticleSystem.MainModule.get_startColor_Injected(ref this, out minMaxGradient);
					return minMaxGradient;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startColor_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002D10 File Offset: 0x00000F10
			// (set) Token: 0x060000C7 RID: 199 RVA: 0x00002D26 File Offset: 0x00000F26
			public ParticleSystem.MinMaxCurve gravityModifier
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.MainModule.get_gravityModifier_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_gravityModifier_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002D30 File Offset: 0x00000F30
			// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002D38 File Offset: 0x00000F38
			public float gravityModifierMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_gravityModifierMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_gravityModifierMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060000CA RID: 202 RVA: 0x00002D41 File Offset: 0x00000F41
			// (set) Token: 0x060000CB RID: 203 RVA: 0x00002D49 File Offset: 0x00000F49
			public ParticleSystemSimulationSpace simulationSpace
			{
				get
				{
					return ParticleSystem.MainModule.get_simulationSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_simulationSpace_Injected(ref this, value);
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060000CC RID: 204 RVA: 0x00002D52 File Offset: 0x00000F52
			// (set) Token: 0x060000CD RID: 205 RVA: 0x00002D5A File Offset: 0x00000F5A
			public Transform customSimulationSpace
			{
				get
				{
					return ParticleSystem.MainModule.get_customSimulationSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_customSimulationSpace_Injected(ref this, value);
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060000CE RID: 206 RVA: 0x00002D63 File Offset: 0x00000F63
			// (set) Token: 0x060000CF RID: 207 RVA: 0x00002D6B File Offset: 0x00000F6B
			public float simulationSpeed
			{
				get
				{
					return ParticleSystem.MainModule.get_simulationSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_simulationSpeed_Injected(ref this, value);
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002D74 File Offset: 0x00000F74
			// (set) Token: 0x060000D1 RID: 209 RVA: 0x00002D7C File Offset: 0x00000F7C
			public bool useUnscaledTime
			{
				get
				{
					return ParticleSystem.MainModule.get_useUnscaledTime_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_useUnscaledTime_Injected(ref this, value);
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002D85 File Offset: 0x00000F85
			// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002D8D File Offset: 0x00000F8D
			public ParticleSystemScalingMode scalingMode
			{
				get
				{
					return ParticleSystem.MainModule.get_scalingMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_scalingMode_Injected(ref this, value);
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002D96 File Offset: 0x00000F96
			// (set) Token: 0x060000D5 RID: 213 RVA: 0x00002D9E File Offset: 0x00000F9E
			public bool playOnAwake
			{
				get
				{
					return ParticleSystem.MainModule.get_playOnAwake_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_playOnAwake_Injected(ref this, value);
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002DA7 File Offset: 0x00000FA7
			// (set) Token: 0x060000D7 RID: 215 RVA: 0x00002DAF File Offset: 0x00000FAF
			public int maxParticles
			{
				get
				{
					return ParticleSystem.MainModule.get_maxParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_maxParticles_Injected(ref this, value);
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002DB8 File Offset: 0x00000FB8
			// (set) Token: 0x060000D9 RID: 217 RVA: 0x00002DC0 File Offset: 0x00000FC0
			public ParticleSystemEmitterVelocityMode emitterVelocityMode
			{
				get
				{
					return ParticleSystem.MainModule.get_emitterVelocityMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_emitterVelocityMode_Injected(ref this, value);
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x060000DA RID: 218 RVA: 0x00002DC9 File Offset: 0x00000FC9
			// (set) Token: 0x060000DB RID: 219 RVA: 0x00002DD1 File Offset: 0x00000FD1
			public ParticleSystemStopAction stopAction
			{
				get
				{
					return ParticleSystem.MainModule.get_stopAction_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_stopAction_Injected(ref this, value);
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x060000DC RID: 220 RVA: 0x00002DDA File Offset: 0x00000FDA
			// (set) Token: 0x060000DD RID: 221 RVA: 0x00002DE2 File Offset: 0x00000FE2
			public ParticleSystemRingBufferMode ringBufferMode
			{
				get
				{
					return ParticleSystem.MainModule.get_ringBufferMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_ringBufferMode_Injected(ref this, value);
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060000DE RID: 222 RVA: 0x00002DEC File Offset: 0x00000FEC
			// (set) Token: 0x060000DF RID: 223 RVA: 0x00002E02 File Offset: 0x00001002
			public Vector2 ringBufferLoopRange
			{
				get
				{
					Vector2 vector;
					ParticleSystem.MainModule.get_ringBufferLoopRange_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_ringBufferLoopRange_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002E0C File Offset: 0x0000100C
			// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002E14 File Offset: 0x00001014
			public ParticleSystemCullingMode cullingMode
			{
				get
				{
					return ParticleSystem.MainModule.get_cullingMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_cullingMode_Injected(ref this, value);
				}
			}

			// Token: 0x060000E2 RID: 226
			[MethodImpl(4096)]
			private static extern float get_duration_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000E3 RID: 227
			[MethodImpl(4096)]
			private static extern void set_duration_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000E4 RID: 228
			[MethodImpl(4096)]
			private static extern bool get_loop_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000E5 RID: 229
			[MethodImpl(4096)]
			private static extern void set_loop_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x060000E6 RID: 230
			[MethodImpl(4096)]
			private static extern bool get_prewarm_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000E7 RID: 231
			[MethodImpl(4096)]
			private static extern void set_prewarm_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x060000E8 RID: 232
			[MethodImpl(4096)]
			private static extern void get_startDelay_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060000E9 RID: 233
			[MethodImpl(4096)]
			private static extern void set_startDelay_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060000EA RID: 234
			[MethodImpl(4096)]
			private static extern float get_startDelayMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000EB RID: 235
			[MethodImpl(4096)]
			private static extern void set_startDelayMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000EC RID: 236
			[MethodImpl(4096)]
			private static extern void get_startLifetime_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060000ED RID: 237
			[MethodImpl(4096)]
			private static extern void set_startLifetime_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060000EE RID: 238
			[MethodImpl(4096)]
			private static extern float get_startLifetimeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000EF RID: 239
			[MethodImpl(4096)]
			private static extern void set_startLifetimeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000F0 RID: 240
			[MethodImpl(4096)]
			private static extern void get_startSpeed_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060000F1 RID: 241
			[MethodImpl(4096)]
			private static extern void set_startSpeed_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060000F2 RID: 242
			[MethodImpl(4096)]
			private static extern float get_startSpeedMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000F3 RID: 243
			[MethodImpl(4096)]
			private static extern void set_startSpeedMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000F4 RID: 244
			[MethodImpl(4096)]
			private static extern bool get_startSize3D_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000F5 RID: 245
			[MethodImpl(4096)]
			private static extern void set_startSize3D_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x060000F6 RID: 246
			[MethodImpl(4096)]
			private static extern void get_startSize_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060000F7 RID: 247
			[MethodImpl(4096)]
			private static extern void set_startSize_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060000F8 RID: 248
			[MethodImpl(4096)]
			private static extern float get_startSizeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000F9 RID: 249
			[MethodImpl(4096)]
			private static extern void set_startSizeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000FA RID: 250
			[MethodImpl(4096)]
			private static extern void get_startSizeX_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060000FB RID: 251
			[MethodImpl(4096)]
			private static extern void set_startSizeX_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060000FC RID: 252
			[MethodImpl(4096)]
			private static extern float get_startSizeXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000FD RID: 253
			[MethodImpl(4096)]
			private static extern void set_startSizeXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000FE RID: 254
			[MethodImpl(4096)]
			private static extern void get_startSizeY_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060000FF RID: 255
			[MethodImpl(4096)]
			private static extern void set_startSizeY_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000100 RID: 256
			[MethodImpl(4096)]
			private static extern float get_startSizeYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000101 RID: 257
			[MethodImpl(4096)]
			private static extern void set_startSizeYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000102 RID: 258
			[MethodImpl(4096)]
			private static extern void get_startSizeZ_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000103 RID: 259
			[MethodImpl(4096)]
			private static extern void set_startSizeZ_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000104 RID: 260
			[MethodImpl(4096)]
			private static extern float get_startSizeZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000105 RID: 261
			[MethodImpl(4096)]
			private static extern void set_startSizeZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000106 RID: 262
			[MethodImpl(4096)]
			private static extern bool get_startRotation3D_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000107 RID: 263
			[MethodImpl(4096)]
			private static extern void set_startRotation3D_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x06000108 RID: 264
			[MethodImpl(4096)]
			private static extern void get_startRotation_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000109 RID: 265
			[MethodImpl(4096)]
			private static extern void set_startRotation_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600010A RID: 266
			[MethodImpl(4096)]
			private static extern float get_startRotationMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600010B RID: 267
			[MethodImpl(4096)]
			private static extern void set_startRotationMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x0600010C RID: 268
			[MethodImpl(4096)]
			private static extern void get_startRotationX_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600010D RID: 269
			[MethodImpl(4096)]
			private static extern void set_startRotationX_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600010E RID: 270
			[MethodImpl(4096)]
			private static extern float get_startRotationXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600010F RID: 271
			[MethodImpl(4096)]
			private static extern void set_startRotationXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000110 RID: 272
			[MethodImpl(4096)]
			private static extern void get_startRotationY_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000111 RID: 273
			[MethodImpl(4096)]
			private static extern void set_startRotationY_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000112 RID: 274
			[MethodImpl(4096)]
			private static extern float get_startRotationYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000113 RID: 275
			[MethodImpl(4096)]
			private static extern void set_startRotationYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000114 RID: 276
			[MethodImpl(4096)]
			private static extern void get_startRotationZ_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000115 RID: 277
			[MethodImpl(4096)]
			private static extern void set_startRotationZ_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000116 RID: 278
			[MethodImpl(4096)]
			private static extern float get_startRotationZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000117 RID: 279
			[MethodImpl(4096)]
			private static extern void set_startRotationZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000118 RID: 280
			[MethodImpl(4096)]
			private static extern float get_flipRotation_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000119 RID: 281
			[MethodImpl(4096)]
			private static extern void set_flipRotation_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x0600011A RID: 282
			[MethodImpl(4096)]
			private static extern void get_startColor_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x0600011B RID: 283
			[MethodImpl(4096)]
			private static extern void set_startColor_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x0600011C RID: 284
			[MethodImpl(4096)]
			private static extern void get_gravityModifier_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600011D RID: 285
			[MethodImpl(4096)]
			private static extern void set_gravityModifier_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600011E RID: 286
			[MethodImpl(4096)]
			private static extern float get_gravityModifierMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600011F RID: 287
			[MethodImpl(4096)]
			private static extern void set_gravityModifierMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000120 RID: 288
			[MethodImpl(4096)]
			private static extern ParticleSystemSimulationSpace get_simulationSpace_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000121 RID: 289
			[MethodImpl(4096)]
			private static extern void set_simulationSpace_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemSimulationSpace value);

			// Token: 0x06000122 RID: 290
			[MethodImpl(4096)]
			private static extern Transform get_customSimulationSpace_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000123 RID: 291
			[MethodImpl(4096)]
			private static extern void set_customSimulationSpace_Injected(ref ParticleSystem.MainModule _unity_self, Transform value);

			// Token: 0x06000124 RID: 292
			[MethodImpl(4096)]
			private static extern float get_simulationSpeed_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000125 RID: 293
			[MethodImpl(4096)]
			private static extern void set_simulationSpeed_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000126 RID: 294
			[MethodImpl(4096)]
			private static extern bool get_useUnscaledTime_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000127 RID: 295
			[MethodImpl(4096)]
			private static extern void set_useUnscaledTime_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x06000128 RID: 296
			[MethodImpl(4096)]
			private static extern ParticleSystemScalingMode get_scalingMode_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000129 RID: 297
			[MethodImpl(4096)]
			private static extern void set_scalingMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemScalingMode value);

			// Token: 0x0600012A RID: 298
			[MethodImpl(4096)]
			private static extern bool get_playOnAwake_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600012B RID: 299
			[MethodImpl(4096)]
			private static extern void set_playOnAwake_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x0600012C RID: 300
			[MethodImpl(4096)]
			private static extern int get_maxParticles_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600012D RID: 301
			[MethodImpl(4096)]
			private static extern void set_maxParticles_Injected(ref ParticleSystem.MainModule _unity_self, int value);

			// Token: 0x0600012E RID: 302
			[MethodImpl(4096)]
			private static extern ParticleSystemEmitterVelocityMode get_emitterVelocityMode_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600012F RID: 303
			[MethodImpl(4096)]
			private static extern void set_emitterVelocityMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemEmitterVelocityMode value);

			// Token: 0x06000130 RID: 304
			[MethodImpl(4096)]
			private static extern ParticleSystemStopAction get_stopAction_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000131 RID: 305
			[MethodImpl(4096)]
			private static extern void set_stopAction_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemStopAction value);

			// Token: 0x06000132 RID: 306
			[MethodImpl(4096)]
			private static extern ParticleSystemRingBufferMode get_ringBufferMode_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000133 RID: 307
			[MethodImpl(4096)]
			private static extern void set_ringBufferMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemRingBufferMode value);

			// Token: 0x06000134 RID: 308
			[MethodImpl(4096)]
			private static extern void get_ringBufferLoopRange_Injected(ref ParticleSystem.MainModule _unity_self, out Vector2 ret);

			// Token: 0x06000135 RID: 309
			[MethodImpl(4096)]
			private static extern void set_ringBufferLoopRange_Injected(ref ParticleSystem.MainModule _unity_self, ref Vector2 value);

			// Token: 0x06000136 RID: 310
			[MethodImpl(4096)]
			private static extern ParticleSystemCullingMode get_cullingMode_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000137 RID: 311
			[MethodImpl(4096)]
			private static extern void set_cullingMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemCullingMode value);

			// Token: 0x04000004 RID: 4
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000005 RID: 5
		public struct EmissionModule
		{
			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000138 RID: 312 RVA: 0x00002E20 File Offset: 0x00001020
			// (set) Token: 0x06000139 RID: 313 RVA: 0x00002E33 File Offset: 0x00001033
			[Obsolete("ParticleSystemEmissionType no longer does anything. Time and Distance based emission are now both always active.", false)]
			public ParticleSystemEmissionType type
			{
				get
				{
					return ParticleSystemEmissionType.Time;
				}
				set
				{
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x0600013A RID: 314 RVA: 0x00002E38 File Offset: 0x00001038
			// (set) Token: 0x0600013B RID: 315 RVA: 0x00002E50 File Offset: 0x00001050
			[Obsolete("rate property is deprecated. Use rateOverTime or rateOverDistance instead.", false)]
			public ParticleSystem.MinMaxCurve rate
			{
				get
				{
					return this.rateOverTime;
				}
				set
				{
					this.rateOverTime = value;
				}
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x0600013C RID: 316 RVA: 0x00002E5C File Offset: 0x0000105C
			// (set) Token: 0x0600013D RID: 317 RVA: 0x00002E74 File Offset: 0x00001074
			[Obsolete("rateMultiplier property is deprecated. Use rateOverTimeMultiplier or rateOverDistanceMultiplier instead.", false)]
			public float rateMultiplier
			{
				get
				{
					return this.rateOverTimeMultiplier;
				}
				set
				{
					this.rateOverTimeMultiplier = value;
				}
			}

			// Token: 0x0600013E RID: 318 RVA: 0x00002E7F File Offset: 0x0000107F
			internal EmissionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x0600013F RID: 319 RVA: 0x00002E89 File Offset: 0x00001089
			// (set) Token: 0x06000140 RID: 320 RVA: 0x00002E91 File Offset: 0x00001091
			public bool enabled
			{
				get
				{
					return ParticleSystem.EmissionModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000141 RID: 321 RVA: 0x00002E9C File Offset: 0x0000109C
			// (set) Token: 0x06000142 RID: 322 RVA: 0x00002EB2 File Offset: 0x000010B2
			public ParticleSystem.MinMaxCurve rateOverTime
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.EmissionModule.get_rateOverTime_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverTime_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000143 RID: 323 RVA: 0x00002EBC File Offset: 0x000010BC
			// (set) Token: 0x06000144 RID: 324 RVA: 0x00002EC4 File Offset: 0x000010C4
			public float rateOverTimeMultiplier
			{
				get
				{
					return ParticleSystem.EmissionModule.get_rateOverTimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverTimeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000145 RID: 325 RVA: 0x00002ED0 File Offset: 0x000010D0
			// (set) Token: 0x06000146 RID: 326 RVA: 0x00002EE6 File Offset: 0x000010E6
			public ParticleSystem.MinMaxCurve rateOverDistance
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.EmissionModule.get_rateOverDistance_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverDistance_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000147 RID: 327 RVA: 0x00002EF0 File Offset: 0x000010F0
			// (set) Token: 0x06000148 RID: 328 RVA: 0x00002EF8 File Offset: 0x000010F8
			public float rateOverDistanceMultiplier
			{
				get
				{
					return ParticleSystem.EmissionModule.get_rateOverDistanceMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverDistanceMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x06000149 RID: 329 RVA: 0x00002F01 File Offset: 0x00001101
			public void SetBursts(ParticleSystem.Burst[] bursts)
			{
				this.SetBursts(bursts, bursts.Length);
			}

			// Token: 0x0600014A RID: 330 RVA: 0x00002F10 File Offset: 0x00001110
			public void SetBursts(ParticleSystem.Burst[] bursts, int size)
			{
				this.burstCount = size;
				for (int i = 0; i < size; i++)
				{
					this.SetBurst(i, bursts[i]);
				}
			}

			// Token: 0x0600014B RID: 331 RVA: 0x00002F48 File Offset: 0x00001148
			public int GetBursts(ParticleSystem.Burst[] bursts)
			{
				int burstCount = this.burstCount;
				for (int i = 0; i < burstCount; i++)
				{
					bursts[i] = this.GetBurst(i);
				}
				return burstCount;
			}

			// Token: 0x0600014C RID: 332 RVA: 0x00002F80 File Offset: 0x00001180
			[NativeThrows]
			public void SetBurst(int index, ParticleSystem.Burst burst)
			{
				ParticleSystem.EmissionModule.SetBurst_Injected(ref this, index, ref burst);
			}

			// Token: 0x0600014D RID: 333 RVA: 0x00002F8C File Offset: 0x0000118C
			[NativeThrows]
			public ParticleSystem.Burst GetBurst(int index)
			{
				ParticleSystem.Burst burst;
				ParticleSystem.EmissionModule.GetBurst_Injected(ref this, index, out burst);
				return burst;
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600014E RID: 334 RVA: 0x00002FA3 File Offset: 0x000011A3
			// (set) Token: 0x0600014F RID: 335 RVA: 0x00002FAB File Offset: 0x000011AB
			public int burstCount
			{
				get
				{
					return ParticleSystem.EmissionModule.get_burstCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_burstCount_Injected(ref this, value);
				}
			}

			// Token: 0x06000150 RID: 336
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.EmissionModule _unity_self);

			// Token: 0x06000151 RID: 337
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.EmissionModule _unity_self, bool value);

			// Token: 0x06000152 RID: 338
			[MethodImpl(4096)]
			private static extern void get_rateOverTime_Injected(ref ParticleSystem.EmissionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000153 RID: 339
			[MethodImpl(4096)]
			private static extern void set_rateOverTime_Injected(ref ParticleSystem.EmissionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000154 RID: 340
			[MethodImpl(4096)]
			private static extern float get_rateOverTimeMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self);

			// Token: 0x06000155 RID: 341
			[MethodImpl(4096)]
			private static extern void set_rateOverTimeMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self, float value);

			// Token: 0x06000156 RID: 342
			[MethodImpl(4096)]
			private static extern void get_rateOverDistance_Injected(ref ParticleSystem.EmissionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000157 RID: 343
			[MethodImpl(4096)]
			private static extern void set_rateOverDistance_Injected(ref ParticleSystem.EmissionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000158 RID: 344
			[MethodImpl(4096)]
			private static extern float get_rateOverDistanceMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self);

			// Token: 0x06000159 RID: 345
			[MethodImpl(4096)]
			private static extern void set_rateOverDistanceMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self, float value);

			// Token: 0x0600015A RID: 346
			[MethodImpl(4096)]
			private static extern void SetBurst_Injected(ref ParticleSystem.EmissionModule _unity_self, int index, ref ParticleSystem.Burst burst);

			// Token: 0x0600015B RID: 347
			[MethodImpl(4096)]
			private static extern void GetBurst_Injected(ref ParticleSystem.EmissionModule _unity_self, int index, out ParticleSystem.Burst ret);

			// Token: 0x0600015C RID: 348
			[MethodImpl(4096)]
			private static extern int get_burstCount_Injected(ref ParticleSystem.EmissionModule _unity_self);

			// Token: 0x0600015D RID: 349
			[MethodImpl(4096)]
			private static extern void set_burstCount_Injected(ref ParticleSystem.EmissionModule _unity_self, int value);

			// Token: 0x04000005 RID: 5
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000006 RID: 6
		public struct ShapeModule
		{
			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600015E RID: 350 RVA: 0x00002FB4 File Offset: 0x000011B4
			// (set) Token: 0x0600015F RID: 351 RVA: 0x00002FCC File Offset: 0x000011CC
			[Obsolete("Please use scale instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/ShapeModule.scale", false)]
			public Vector3 box
			{
				get
				{
					return this.scale;
				}
				set
				{
					this.scale = value;
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000160 RID: 352 RVA: 0x00002FD8 File Offset: 0x000011D8
			// (set) Token: 0x06000161 RID: 353 RVA: 0x00002FF5 File Offset: 0x000011F5
			[Obsolete("meshScale property is deprecated.Please use scale instead.", false)]
			public float meshScale
			{
				get
				{
					return this.scale.x;
				}
				set
				{
					this.scale = new Vector3(value, value, value);
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000162 RID: 354 RVA: 0x00003008 File Offset: 0x00001208
			// (set) Token: 0x06000163 RID: 355 RVA: 0x0000302A File Offset: 0x0000122A
			[Obsolete("randomDirection property is deprecated. Use randomDirectionAmount instead.", false)]
			public bool randomDirection
			{
				get
				{
					return this.randomDirectionAmount >= 0.5f;
				}
				set
				{
					this.randomDirectionAmount = (value ? 1f : 0f);
				}
			}

			// Token: 0x06000164 RID: 356 RVA: 0x00003043 File Offset: 0x00001243
			internal ShapeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000165 RID: 357 RVA: 0x0000304D File Offset: 0x0000124D
			// (set) Token: 0x06000166 RID: 358 RVA: 0x00003055 File Offset: 0x00001255
			public bool enabled
			{
				get
				{
					return ParticleSystem.ShapeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000167 RID: 359 RVA: 0x0000305E File Offset: 0x0000125E
			// (set) Token: 0x06000168 RID: 360 RVA: 0x00003066 File Offset: 0x00001266
			public ParticleSystemShapeType shapeType
			{
				get
				{
					return ParticleSystem.ShapeModule.get_shapeType_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_shapeType_Injected(ref this, value);
				}
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000169 RID: 361 RVA: 0x0000306F File Offset: 0x0000126F
			// (set) Token: 0x0600016A RID: 362 RVA: 0x00003077 File Offset: 0x00001277
			public float randomDirectionAmount
			{
				get
				{
					return ParticleSystem.ShapeModule.get_randomDirectionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_randomDirectionAmount_Injected(ref this, value);
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600016B RID: 363 RVA: 0x00003080 File Offset: 0x00001280
			// (set) Token: 0x0600016C RID: 364 RVA: 0x00003088 File Offset: 0x00001288
			public float sphericalDirectionAmount
			{
				get
				{
					return ParticleSystem.ShapeModule.get_sphericalDirectionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_sphericalDirectionAmount_Injected(ref this, value);
				}
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x0600016D RID: 365 RVA: 0x00003091 File Offset: 0x00001291
			// (set) Token: 0x0600016E RID: 366 RVA: 0x00003099 File Offset: 0x00001299
			public float randomPositionAmount
			{
				get
				{
					return ParticleSystem.ShapeModule.get_randomPositionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_randomPositionAmount_Injected(ref this, value);
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x0600016F RID: 367 RVA: 0x000030A2 File Offset: 0x000012A2
			// (set) Token: 0x06000170 RID: 368 RVA: 0x000030AA File Offset: 0x000012AA
			public bool alignToDirection
			{
				get
				{
					return ParticleSystem.ShapeModule.get_alignToDirection_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_alignToDirection_Injected(ref this, value);
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000171 RID: 369 RVA: 0x000030B3 File Offset: 0x000012B3
			// (set) Token: 0x06000172 RID: 370 RVA: 0x000030BB File Offset: 0x000012BB
			public float radius
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radius_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radius_Injected(ref this, value);
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000173 RID: 371 RVA: 0x000030C4 File Offset: 0x000012C4
			// (set) Token: 0x06000174 RID: 372 RVA: 0x000030CC File Offset: 0x000012CC
			public ParticleSystemShapeMultiModeValue radiusMode
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusMode_Injected(ref this, value);
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x06000175 RID: 373 RVA: 0x000030D5 File Offset: 0x000012D5
			// (set) Token: 0x06000176 RID: 374 RVA: 0x000030DD File Offset: 0x000012DD
			public float radiusSpread
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpread_Injected(ref this, value);
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000177 RID: 375 RVA: 0x000030E8 File Offset: 0x000012E8
			// (set) Token: 0x06000178 RID: 376 RVA: 0x000030FE File Offset: 0x000012FE
			public ParticleSystem.MinMaxCurve radiusSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.ShapeModule.get_radiusSpeed_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000179 RID: 377 RVA: 0x00003108 File Offset: 0x00001308
			// (set) Token: 0x0600017A RID: 378 RVA: 0x00003110 File Offset: 0x00001310
			public float radiusSpeedMultiplier
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600017B RID: 379 RVA: 0x00003119 File Offset: 0x00001319
			// (set) Token: 0x0600017C RID: 380 RVA: 0x00003121 File Offset: 0x00001321
			public float radiusThickness
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusThickness_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusThickness_Injected(ref this, value);
				}
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x0600017D RID: 381 RVA: 0x0000312A File Offset: 0x0000132A
			// (set) Token: 0x0600017E RID: 382 RVA: 0x00003132 File Offset: 0x00001332
			public float angle
			{
				get
				{
					return ParticleSystem.ShapeModule.get_angle_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_angle_Injected(ref this, value);
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x0600017F RID: 383 RVA: 0x0000313B File Offset: 0x0000133B
			// (set) Token: 0x06000180 RID: 384 RVA: 0x00003143 File Offset: 0x00001343
			public float length
			{
				get
				{
					return ParticleSystem.ShapeModule.get_length_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_length_Injected(ref this, value);
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000181 RID: 385 RVA: 0x0000314C File Offset: 0x0000134C
			// (set) Token: 0x06000182 RID: 386 RVA: 0x00003162 File Offset: 0x00001362
			public Vector3 boxThickness
			{
				get
				{
					Vector3 vector;
					ParticleSystem.ShapeModule.get_boxThickness_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_boxThickness_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000183 RID: 387 RVA: 0x0000316C File Offset: 0x0000136C
			// (set) Token: 0x06000184 RID: 388 RVA: 0x00003174 File Offset: 0x00001374
			public ParticleSystemMeshShapeType meshShapeType
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshShapeType_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshShapeType_Injected(ref this, value);
				}
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000185 RID: 389 RVA: 0x0000317D File Offset: 0x0000137D
			// (set) Token: 0x06000186 RID: 390 RVA: 0x00003185 File Offset: 0x00001385
			public Mesh mesh
			{
				get
				{
					return ParticleSystem.ShapeModule.get_mesh_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_mesh_Injected(ref this, value);
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000187 RID: 391 RVA: 0x0000318E File Offset: 0x0000138E
			// (set) Token: 0x06000188 RID: 392 RVA: 0x00003196 File Offset: 0x00001396
			public MeshRenderer meshRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshRenderer_Injected(ref this, value);
				}
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000189 RID: 393 RVA: 0x0000319F File Offset: 0x0000139F
			// (set) Token: 0x0600018A RID: 394 RVA: 0x000031A7 File Offset: 0x000013A7
			public SkinnedMeshRenderer skinnedMeshRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.get_skinnedMeshRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_skinnedMeshRenderer_Injected(ref this, value);
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600018B RID: 395 RVA: 0x000031B0 File Offset: 0x000013B0
			// (set) Token: 0x0600018C RID: 396 RVA: 0x000031B8 File Offset: 0x000013B8
			public Sprite sprite
			{
				get
				{
					return ParticleSystem.ShapeModule.get_sprite_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_sprite_Injected(ref this, value);
				}
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600018D RID: 397 RVA: 0x000031C1 File Offset: 0x000013C1
			// (set) Token: 0x0600018E RID: 398 RVA: 0x000031C9 File Offset: 0x000013C9
			public SpriteRenderer spriteRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.get_spriteRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_spriteRenderer_Injected(ref this, value);
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x0600018F RID: 399 RVA: 0x000031D2 File Offset: 0x000013D2
			// (set) Token: 0x06000190 RID: 400 RVA: 0x000031DA File Offset: 0x000013DA
			public bool useMeshMaterialIndex
			{
				get
				{
					return ParticleSystem.ShapeModule.get_useMeshMaterialIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_useMeshMaterialIndex_Injected(ref this, value);
				}
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000191 RID: 401 RVA: 0x000031E3 File Offset: 0x000013E3
			// (set) Token: 0x06000192 RID: 402 RVA: 0x000031EB File Offset: 0x000013EB
			public int meshMaterialIndex
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshMaterialIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshMaterialIndex_Injected(ref this, value);
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x06000193 RID: 403 RVA: 0x000031F4 File Offset: 0x000013F4
			// (set) Token: 0x06000194 RID: 404 RVA: 0x000031FC File Offset: 0x000013FC
			public bool useMeshColors
			{
				get
				{
					return ParticleSystem.ShapeModule.get_useMeshColors_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_useMeshColors_Injected(ref this, value);
				}
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000195 RID: 405 RVA: 0x00003205 File Offset: 0x00001405
			// (set) Token: 0x06000196 RID: 406 RVA: 0x0000320D File Offset: 0x0000140D
			public float normalOffset
			{
				get
				{
					return ParticleSystem.ShapeModule.get_normalOffset_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_normalOffset_Injected(ref this, value);
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000197 RID: 407 RVA: 0x00003216 File Offset: 0x00001416
			// (set) Token: 0x06000198 RID: 408 RVA: 0x0000321E File Offset: 0x0000141E
			public ParticleSystemShapeMultiModeValue meshSpawnMode
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshSpawnMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnMode_Injected(ref this, value);
				}
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000199 RID: 409 RVA: 0x00003227 File Offset: 0x00001427
			// (set) Token: 0x0600019A RID: 410 RVA: 0x0000322F File Offset: 0x0000142F
			public float meshSpawnSpread
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshSpawnSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpread_Injected(ref this, value);
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x0600019B RID: 411 RVA: 0x00003238 File Offset: 0x00001438
			// (set) Token: 0x0600019C RID: 412 RVA: 0x0000324E File Offset: 0x0000144E
			public ParticleSystem.MinMaxCurve meshSpawnSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.ShapeModule.get_meshSpawnSpeed_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x0600019D RID: 413 RVA: 0x00003258 File Offset: 0x00001458
			// (set) Token: 0x0600019E RID: 414 RVA: 0x00003260 File Offset: 0x00001460
			public float meshSpawnSpeedMultiplier
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshSpawnSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x0600019F RID: 415 RVA: 0x00003269 File Offset: 0x00001469
			// (set) Token: 0x060001A0 RID: 416 RVA: 0x00003271 File Offset: 0x00001471
			public float arc
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arc_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arc_Injected(ref this, value);
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000327A File Offset: 0x0000147A
			// (set) Token: 0x060001A2 RID: 418 RVA: 0x00003282 File Offset: 0x00001482
			public ParticleSystemShapeMultiModeValue arcMode
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arcMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcMode_Injected(ref this, value);
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000328B File Offset: 0x0000148B
			// (set) Token: 0x060001A4 RID: 420 RVA: 0x00003293 File Offset: 0x00001493
			public float arcSpread
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arcSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpread_Injected(ref this, value);
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000329C File Offset: 0x0000149C
			// (set) Token: 0x060001A6 RID: 422 RVA: 0x000032B2 File Offset: 0x000014B2
			public ParticleSystem.MinMaxCurve arcSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.ShapeModule.get_arcSpeed_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060001A7 RID: 423 RVA: 0x000032BC File Offset: 0x000014BC
			// (set) Token: 0x060001A8 RID: 424 RVA: 0x000032C4 File Offset: 0x000014C4
			public float arcSpeedMultiplier
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arcSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x060001A9 RID: 425 RVA: 0x000032CD File Offset: 0x000014CD
			// (set) Token: 0x060001AA RID: 426 RVA: 0x000032D5 File Offset: 0x000014D5
			public float donutRadius
			{
				get
				{
					return ParticleSystem.ShapeModule.get_donutRadius_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_donutRadius_Injected(ref this, value);
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060001AB RID: 427 RVA: 0x000032E0 File Offset: 0x000014E0
			// (set) Token: 0x060001AC RID: 428 RVA: 0x000032F6 File Offset: 0x000014F6
			public Vector3 position
			{
				get
				{
					Vector3 vector;
					ParticleSystem.ShapeModule.get_position_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_position_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060001AD RID: 429 RVA: 0x00003300 File Offset: 0x00001500
			// (set) Token: 0x060001AE RID: 430 RVA: 0x00003316 File Offset: 0x00001516
			public Vector3 rotation
			{
				get
				{
					Vector3 vector;
					ParticleSystem.ShapeModule.get_rotation_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_rotation_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060001AF RID: 431 RVA: 0x00003320 File Offset: 0x00001520
			// (set) Token: 0x060001B0 RID: 432 RVA: 0x00003336 File Offset: 0x00001536
			public Vector3 scale
			{
				get
				{
					Vector3 vector;
					ParticleSystem.ShapeModule.get_scale_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_scale_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060001B1 RID: 433 RVA: 0x00003340 File Offset: 0x00001540
			// (set) Token: 0x060001B2 RID: 434 RVA: 0x00003348 File Offset: 0x00001548
			public Texture2D texture
			{
				get
				{
					return ParticleSystem.ShapeModule.get_texture_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_texture_Injected(ref this, value);
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060001B3 RID: 435 RVA: 0x00003351 File Offset: 0x00001551
			// (set) Token: 0x060001B4 RID: 436 RVA: 0x00003359 File Offset: 0x00001559
			public ParticleSystemShapeTextureChannel textureClipChannel
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureClipChannel_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureClipChannel_Injected(ref this, value);
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060001B5 RID: 437 RVA: 0x00003362 File Offset: 0x00001562
			// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000336A File Offset: 0x0000156A
			public float textureClipThreshold
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureClipThreshold_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureClipThreshold_Injected(ref this, value);
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060001B7 RID: 439 RVA: 0x00003373 File Offset: 0x00001573
			// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000337B File Offset: 0x0000157B
			public bool textureColorAffectsParticles
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureColorAffectsParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureColorAffectsParticles_Injected(ref this, value);
				}
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060001B9 RID: 441 RVA: 0x00003384 File Offset: 0x00001584
			// (set) Token: 0x060001BA RID: 442 RVA: 0x0000338C File Offset: 0x0000158C
			public bool textureAlphaAffectsParticles
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureAlphaAffectsParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureAlphaAffectsParticles_Injected(ref this, value);
				}
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060001BB RID: 443 RVA: 0x00003395 File Offset: 0x00001595
			// (set) Token: 0x060001BC RID: 444 RVA: 0x0000339D File Offset: 0x0000159D
			public bool textureBilinearFiltering
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureBilinearFiltering_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureBilinearFiltering_Injected(ref this, value);
				}
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060001BD RID: 445 RVA: 0x000033A6 File Offset: 0x000015A6
			// (set) Token: 0x060001BE RID: 446 RVA: 0x000033AE File Offset: 0x000015AE
			public int textureUVChannel
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureUVChannel_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureUVChannel_Injected(ref this, value);
				}
			}

			// Token: 0x060001BF RID: 447
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001C0 RID: 448
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x060001C1 RID: 449
			[MethodImpl(4096)]
			private static extern ParticleSystemShapeType get_shapeType_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001C2 RID: 450
			[MethodImpl(4096)]
			private static extern void set_shapeType_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeType value);

			// Token: 0x060001C3 RID: 451
			[MethodImpl(4096)]
			private static extern float get_randomDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001C4 RID: 452
			[MethodImpl(4096)]
			private static extern void set_randomDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001C5 RID: 453
			[MethodImpl(4096)]
			private static extern float get_sphericalDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001C6 RID: 454
			[MethodImpl(4096)]
			private static extern void set_sphericalDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001C7 RID: 455
			[MethodImpl(4096)]
			private static extern float get_randomPositionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001C8 RID: 456
			[MethodImpl(4096)]
			private static extern void set_randomPositionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001C9 RID: 457
			[MethodImpl(4096)]
			private static extern bool get_alignToDirection_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001CA RID: 458
			[MethodImpl(4096)]
			private static extern void set_alignToDirection_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x060001CB RID: 459
			[MethodImpl(4096)]
			private static extern float get_radius_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001CC RID: 460
			[MethodImpl(4096)]
			private static extern void set_radius_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001CD RID: 461
			[MethodImpl(4096)]
			private static extern ParticleSystemShapeMultiModeValue get_radiusMode_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001CE RID: 462
			[MethodImpl(4096)]
			private static extern void set_radiusMode_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			// Token: 0x060001CF RID: 463
			[MethodImpl(4096)]
			private static extern float get_radiusSpread_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001D0 RID: 464
			[MethodImpl(4096)]
			private static extern void set_radiusSpread_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001D1 RID: 465
			[MethodImpl(4096)]
			private static extern void get_radiusSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060001D2 RID: 466
			[MethodImpl(4096)]
			private static extern void set_radiusSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060001D3 RID: 467
			[MethodImpl(4096)]
			private static extern float get_radiusSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001D4 RID: 468
			[MethodImpl(4096)]
			private static extern void set_radiusSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001D5 RID: 469
			[MethodImpl(4096)]
			private static extern float get_radiusThickness_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001D6 RID: 470
			[MethodImpl(4096)]
			private static extern void set_radiusThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001D7 RID: 471
			[MethodImpl(4096)]
			private static extern float get_angle_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001D8 RID: 472
			[MethodImpl(4096)]
			private static extern void set_angle_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001D9 RID: 473
			[MethodImpl(4096)]
			private static extern float get_length_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001DA RID: 474
			[MethodImpl(4096)]
			private static extern void set_length_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001DB RID: 475
			[MethodImpl(4096)]
			private static extern void get_boxThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x060001DC RID: 476
			[MethodImpl(4096)]
			private static extern void set_boxThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			// Token: 0x060001DD RID: 477
			[MethodImpl(4096)]
			private static extern ParticleSystemMeshShapeType get_meshShapeType_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001DE RID: 478
			[MethodImpl(4096)]
			private static extern void set_meshShapeType_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemMeshShapeType value);

			// Token: 0x060001DF RID: 479
			[MethodImpl(4096)]
			private static extern Mesh get_mesh_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E0 RID: 480
			[MethodImpl(4096)]
			private static extern void set_mesh_Injected(ref ParticleSystem.ShapeModule _unity_self, Mesh value);

			// Token: 0x060001E1 RID: 481
			[MethodImpl(4096)]
			private static extern MeshRenderer get_meshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E2 RID: 482
			[MethodImpl(4096)]
			private static extern void set_meshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, MeshRenderer value);

			// Token: 0x060001E3 RID: 483
			[MethodImpl(4096)]
			private static extern SkinnedMeshRenderer get_skinnedMeshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E4 RID: 484
			[MethodImpl(4096)]
			private static extern void set_skinnedMeshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, SkinnedMeshRenderer value);

			// Token: 0x060001E5 RID: 485
			[MethodImpl(4096)]
			private static extern Sprite get_sprite_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E6 RID: 486
			[MethodImpl(4096)]
			private static extern void set_sprite_Injected(ref ParticleSystem.ShapeModule _unity_self, Sprite value);

			// Token: 0x060001E7 RID: 487
			[MethodImpl(4096)]
			private static extern SpriteRenderer get_spriteRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E8 RID: 488
			[MethodImpl(4096)]
			private static extern void set_spriteRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, SpriteRenderer value);

			// Token: 0x060001E9 RID: 489
			[MethodImpl(4096)]
			private static extern bool get_useMeshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001EA RID: 490
			[MethodImpl(4096)]
			private static extern void set_useMeshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x060001EB RID: 491
			[MethodImpl(4096)]
			private static extern int get_meshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001EC RID: 492
			[MethodImpl(4096)]
			private static extern void set_meshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self, int value);

			// Token: 0x060001ED RID: 493
			[MethodImpl(4096)]
			private static extern bool get_useMeshColors_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001EE RID: 494
			[MethodImpl(4096)]
			private static extern void set_useMeshColors_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x060001EF RID: 495
			[MethodImpl(4096)]
			private static extern float get_normalOffset_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F0 RID: 496
			[MethodImpl(4096)]
			private static extern void set_normalOffset_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001F1 RID: 497
			[MethodImpl(4096)]
			private static extern ParticleSystemShapeMultiModeValue get_meshSpawnMode_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F2 RID: 498
			[MethodImpl(4096)]
			private static extern void set_meshSpawnMode_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			// Token: 0x060001F3 RID: 499
			[MethodImpl(4096)]
			private static extern float get_meshSpawnSpread_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F4 RID: 500
			[MethodImpl(4096)]
			private static extern void set_meshSpawnSpread_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001F5 RID: 501
			[MethodImpl(4096)]
			private static extern void get_meshSpawnSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060001F6 RID: 502
			[MethodImpl(4096)]
			private static extern void set_meshSpawnSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060001F7 RID: 503
			[MethodImpl(4096)]
			private static extern float get_meshSpawnSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F8 RID: 504
			[MethodImpl(4096)]
			private static extern void set_meshSpawnSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001F9 RID: 505
			[MethodImpl(4096)]
			private static extern float get_arc_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001FA RID: 506
			[MethodImpl(4096)]
			private static extern void set_arc_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001FB RID: 507
			[MethodImpl(4096)]
			private static extern ParticleSystemShapeMultiModeValue get_arcMode_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001FC RID: 508
			[MethodImpl(4096)]
			private static extern void set_arcMode_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			// Token: 0x060001FD RID: 509
			[MethodImpl(4096)]
			private static extern float get_arcSpread_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001FE RID: 510
			[MethodImpl(4096)]
			private static extern void set_arcSpread_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001FF RID: 511
			[MethodImpl(4096)]
			private static extern void get_arcSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000200 RID: 512
			[MethodImpl(4096)]
			private static extern void set_arcSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000201 RID: 513
			[MethodImpl(4096)]
			private static extern float get_arcSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000202 RID: 514
			[MethodImpl(4096)]
			private static extern void set_arcSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x06000203 RID: 515
			[MethodImpl(4096)]
			private static extern float get_donutRadius_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000204 RID: 516
			[MethodImpl(4096)]
			private static extern void set_donutRadius_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x06000205 RID: 517
			[MethodImpl(4096)]
			private static extern void get_position_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x06000206 RID: 518
			[MethodImpl(4096)]
			private static extern void set_position_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			// Token: 0x06000207 RID: 519
			[MethodImpl(4096)]
			private static extern void get_rotation_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x06000208 RID: 520
			[MethodImpl(4096)]
			private static extern void set_rotation_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			// Token: 0x06000209 RID: 521
			[MethodImpl(4096)]
			private static extern void get_scale_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x0600020A RID: 522
			[MethodImpl(4096)]
			private static extern void set_scale_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			// Token: 0x0600020B RID: 523
			[MethodImpl(4096)]
			private static extern Texture2D get_texture_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x0600020C RID: 524
			[MethodImpl(4096)]
			private static extern void set_texture_Injected(ref ParticleSystem.ShapeModule _unity_self, Texture2D value);

			// Token: 0x0600020D RID: 525
			[MethodImpl(4096)]
			private static extern ParticleSystemShapeTextureChannel get_textureClipChannel_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x0600020E RID: 526
			[MethodImpl(4096)]
			private static extern void set_textureClipChannel_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeTextureChannel value);

			// Token: 0x0600020F RID: 527
			[MethodImpl(4096)]
			private static extern float get_textureClipThreshold_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000210 RID: 528
			[MethodImpl(4096)]
			private static extern void set_textureClipThreshold_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x06000211 RID: 529
			[MethodImpl(4096)]
			private static extern bool get_textureColorAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000212 RID: 530
			[MethodImpl(4096)]
			private static extern void set_textureColorAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x06000213 RID: 531
			[MethodImpl(4096)]
			private static extern bool get_textureAlphaAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000214 RID: 532
			[MethodImpl(4096)]
			private static extern void set_textureAlphaAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x06000215 RID: 533
			[MethodImpl(4096)]
			private static extern bool get_textureBilinearFiltering_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000216 RID: 534
			[MethodImpl(4096)]
			private static extern void set_textureBilinearFiltering_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x06000217 RID: 535
			[MethodImpl(4096)]
			private static extern int get_textureUVChannel_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000218 RID: 536
			[MethodImpl(4096)]
			private static extern void set_textureUVChannel_Injected(ref ParticleSystem.ShapeModule _unity_self, int value);

			// Token: 0x04000006 RID: 6
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000007 RID: 7
		public struct SubEmittersModule
		{
			// Token: 0x17000098 RID: 152
			// (get) Token: 0x06000219 RID: 537 RVA: 0x000033B8 File Offset: 0x000015B8
			// (set) Token: 0x0600021A RID: 538 RVA: 0x000033D1 File Offset: 0x000015D1
			[Obsolete("birth0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x0600021B RID: 539 RVA: 0x000033DC File Offset: 0x000015DC
			// (set) Token: 0x0600021C RID: 540 RVA: 0x000033D1 File Offset: 0x000015D1
			[Obsolete("birth1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x0600021D RID: 541 RVA: 0x000033F8 File Offset: 0x000015F8
			// (set) Token: 0x0600021E RID: 542 RVA: 0x000033D1 File Offset: 0x000015D1
			[Obsolete("collision0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x0600021F RID: 543 RVA: 0x00003414 File Offset: 0x00001614
			// (set) Token: 0x06000220 RID: 544 RVA: 0x000033D1 File Offset: 0x000015D1
			[Obsolete("collision1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x06000221 RID: 545 RVA: 0x00003430 File Offset: 0x00001630
			// (set) Token: 0x06000222 RID: 546 RVA: 0x000033D1 File Offset: 0x000015D1
			[Obsolete("death0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x06000223 RID: 547 RVA: 0x0000344C File Offset: 0x0000164C
			// (set) Token: 0x06000224 RID: 548 RVA: 0x000033D1 File Offset: 0x000015D1
			[Obsolete("death1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x06000225 RID: 549 RVA: 0x00003465 File Offset: 0x00001665
			private static void ThrowNotImplemented()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000226 RID: 550 RVA: 0x0000346D File Offset: 0x0000166D
			internal SubEmittersModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x06000227 RID: 551 RVA: 0x00003477 File Offset: 0x00001677
			// (set) Token: 0x06000228 RID: 552 RVA: 0x0000347F File Offset: 0x0000167F
			public bool enabled
			{
				get
				{
					return ParticleSystem.SubEmittersModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SubEmittersModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000229 RID: 553 RVA: 0x00003488 File Offset: 0x00001688
			public int subEmittersCount
			{
				get
				{
					return ParticleSystem.SubEmittersModule.get_subEmittersCount_Injected(ref this);
				}
			}

			// Token: 0x0600022A RID: 554 RVA: 0x00003490 File Offset: 0x00001690
			[NativeThrows]
			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability)
			{
				ParticleSystem.SubEmittersModule.AddSubEmitter_Injected(ref this, subEmitter, type, properties, emitProbability);
			}

			// Token: 0x0600022B RID: 555 RVA: 0x0000349D File Offset: 0x0000169D
			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties)
			{
				this.AddSubEmitter(subEmitter, type, properties, 1f);
			}

			// Token: 0x0600022C RID: 556 RVA: 0x000034AF File Offset: 0x000016AF
			[NativeThrows]
			public void RemoveSubEmitter(int index)
			{
				ParticleSystem.SubEmittersModule.RemoveSubEmitter_Injected(ref this, index);
			}

			// Token: 0x0600022D RID: 557 RVA: 0x000034B8 File Offset: 0x000016B8
			[NativeThrows]
			public void SetSubEmitterSystem(int index, ParticleSystem subEmitter)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterSystem_Injected(ref this, index, subEmitter);
			}

			// Token: 0x0600022E RID: 558 RVA: 0x000034C2 File Offset: 0x000016C2
			[NativeThrows]
			public void SetSubEmitterType(int index, ParticleSystemSubEmitterType type)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterType_Injected(ref this, index, type);
			}

			// Token: 0x0600022F RID: 559 RVA: 0x000034CC File Offset: 0x000016CC
			[NativeThrows]
			public void SetSubEmitterProperties(int index, ParticleSystemSubEmitterProperties properties)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterProperties_Injected(ref this, index, properties);
			}

			// Token: 0x06000230 RID: 560 RVA: 0x000034D6 File Offset: 0x000016D6
			[NativeThrows]
			public void SetSubEmitterEmitProbability(int index, float emitProbability)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterEmitProbability_Injected(ref this, index, emitProbability);
			}

			// Token: 0x06000231 RID: 561 RVA: 0x000034E0 File Offset: 0x000016E0
			[NativeThrows]
			public ParticleSystem GetSubEmitterSystem(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterSystem_Injected(ref this, index);
			}

			// Token: 0x06000232 RID: 562 RVA: 0x000034E9 File Offset: 0x000016E9
			[NativeThrows]
			public ParticleSystemSubEmitterType GetSubEmitterType(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterType_Injected(ref this, index);
			}

			// Token: 0x06000233 RID: 563 RVA: 0x000034F2 File Offset: 0x000016F2
			[NativeThrows]
			public ParticleSystemSubEmitterProperties GetSubEmitterProperties(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterProperties_Injected(ref this, index);
			}

			// Token: 0x06000234 RID: 564 RVA: 0x000034FB File Offset: 0x000016FB
			[NativeThrows]
			public float GetSubEmitterEmitProbability(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterEmitProbability_Injected(ref this, index);
			}

			// Token: 0x06000235 RID: 565
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.SubEmittersModule _unity_self);

			// Token: 0x06000236 RID: 566
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.SubEmittersModule _unity_self, bool value);

			// Token: 0x06000237 RID: 567
			[MethodImpl(4096)]
			private static extern int get_subEmittersCount_Injected(ref ParticleSystem.SubEmittersModule _unity_self);

			// Token: 0x06000238 RID: 568
			[MethodImpl(4096)]
			private static extern void AddSubEmitter_Injected(ref ParticleSystem.SubEmittersModule _unity_self, ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability);

			// Token: 0x06000239 RID: 569
			[MethodImpl(4096)]
			private static extern void RemoveSubEmitter_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x0600023A RID: 570
			[MethodImpl(4096)]
			private static extern void SetSubEmitterSystem_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, ParticleSystem subEmitter);

			// Token: 0x0600023B RID: 571
			[MethodImpl(4096)]
			private static extern void SetSubEmitterType_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, ParticleSystemSubEmitterType type);

			// Token: 0x0600023C RID: 572
			[MethodImpl(4096)]
			private static extern void SetSubEmitterProperties_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, ParticleSystemSubEmitterProperties properties);

			// Token: 0x0600023D RID: 573
			[MethodImpl(4096)]
			private static extern void SetSubEmitterEmitProbability_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, float emitProbability);

			// Token: 0x0600023E RID: 574
			[MethodImpl(4096)]
			private static extern ParticleSystem GetSubEmitterSystem_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x0600023F RID: 575
			[MethodImpl(4096)]
			private static extern ParticleSystemSubEmitterType GetSubEmitterType_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x06000240 RID: 576
			[MethodImpl(4096)]
			private static extern ParticleSystemSubEmitterProperties GetSubEmitterProperties_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x06000241 RID: 577
			[MethodImpl(4096)]
			private static extern float GetSubEmitterEmitProbability_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x04000007 RID: 7
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000008 RID: 8
		public struct TextureSheetAnimationModule
		{
			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x06000242 RID: 578 RVA: 0x00003504 File Offset: 0x00001704
			// (set) Token: 0x06000243 RID: 579 RVA: 0x0000352C File Offset: 0x0000172C
			[Obsolete("flipU property is deprecated. Use ParticleSystemRenderer.flip.x instead.", false)]
			public float flipU
			{
				get
				{
					return this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.x;
				}
				set
				{
					ParticleSystemRenderer component = this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.x = value;
					component.flip = flip;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x06000244 RID: 580 RVA: 0x00003560 File Offset: 0x00001760
			// (set) Token: 0x06000245 RID: 581 RVA: 0x00003588 File Offset: 0x00001788
			[Obsolete("flipV property is deprecated. Use ParticleSystemRenderer.flip.y instead.", false)]
			public float flipV
			{
				get
				{
					return this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.y;
				}
				set
				{
					ParticleSystemRenderer component = this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.y = value;
					component.flip = flip;
				}
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x06000247 RID: 583 RVA: 0x000035CC File Offset: 0x000017CC
			// (set) Token: 0x06000246 RID: 582 RVA: 0x000035B9 File Offset: 0x000017B9
			[Obsolete("useRandomRow property is deprecated. Use rowMode instead.", false)]
			public bool useRandomRow
			{
				get
				{
					return this.rowMode == ParticleSystemAnimationRowMode.Random;
				}
				set
				{
					this.rowMode = (value ? ParticleSystemAnimationRowMode.Random : ParticleSystemAnimationRowMode.Custom);
				}
			}

			// Token: 0x06000248 RID: 584 RVA: 0x000035E7 File Offset: 0x000017E7
			internal TextureSheetAnimationModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x06000249 RID: 585 RVA: 0x000035F1 File Offset: 0x000017F1
			// (set) Token: 0x0600024A RID: 586 RVA: 0x000035F9 File Offset: 0x000017F9
			public bool enabled
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x0600024B RID: 587 RVA: 0x00003602 File Offset: 0x00001802
			// (set) Token: 0x0600024C RID: 588 RVA: 0x0000360A File Offset: 0x0000180A
			public ParticleSystemAnimationMode mode
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_mode_Injected(ref this, value);
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x0600024D RID: 589 RVA: 0x00003613 File Offset: 0x00001813
			// (set) Token: 0x0600024E RID: 590 RVA: 0x0000361B File Offset: 0x0000181B
			public ParticleSystemAnimationTimeMode timeMode
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_timeMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_timeMode_Injected(ref this, value);
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x0600024F RID: 591 RVA: 0x00003624 File Offset: 0x00001824
			// (set) Token: 0x06000250 RID: 592 RVA: 0x0000362C File Offset: 0x0000182C
			public float fps
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_fps_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_fps_Injected(ref this, value);
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x06000251 RID: 593 RVA: 0x00003635 File Offset: 0x00001835
			// (set) Token: 0x06000252 RID: 594 RVA: 0x0000363D File Offset: 0x0000183D
			public int numTilesX
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_numTilesX_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_numTilesX_Injected(ref this, value);
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x06000253 RID: 595 RVA: 0x00003646 File Offset: 0x00001846
			// (set) Token: 0x06000254 RID: 596 RVA: 0x0000364E File Offset: 0x0000184E
			public int numTilesY
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_numTilesY_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_numTilesY_Injected(ref this, value);
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x06000255 RID: 597 RVA: 0x00003657 File Offset: 0x00001857
			// (set) Token: 0x06000256 RID: 598 RVA: 0x0000365F File Offset: 0x0000185F
			public ParticleSystemAnimationType animation
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_animation_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_animation_Injected(ref this, value);
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x06000257 RID: 599 RVA: 0x00003668 File Offset: 0x00001868
			// (set) Token: 0x06000258 RID: 600 RVA: 0x00003670 File Offset: 0x00001870
			public ParticleSystemAnimationRowMode rowMode
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_rowMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_rowMode_Injected(ref this, value);
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x06000259 RID: 601 RVA: 0x0000367C File Offset: 0x0000187C
			// (set) Token: 0x0600025A RID: 602 RVA: 0x00003692 File Offset: 0x00001892
			public ParticleSystem.MinMaxCurve frameOverTime
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.TextureSheetAnimationModule.get_frameOverTime_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_frameOverTime_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x0600025B RID: 603 RVA: 0x0000369C File Offset: 0x0000189C
			// (set) Token: 0x0600025C RID: 604 RVA: 0x000036A4 File Offset: 0x000018A4
			public float frameOverTimeMultiplier
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_frameOverTimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_frameOverTimeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x0600025D RID: 605 RVA: 0x000036B0 File Offset: 0x000018B0
			// (set) Token: 0x0600025E RID: 606 RVA: 0x000036C6 File Offset: 0x000018C6
			public ParticleSystem.MinMaxCurve startFrame
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.TextureSheetAnimationModule.get_startFrame_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_startFrame_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x0600025F RID: 607 RVA: 0x000036D0 File Offset: 0x000018D0
			// (set) Token: 0x06000260 RID: 608 RVA: 0x000036D8 File Offset: 0x000018D8
			public float startFrameMultiplier
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_startFrameMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_startFrameMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000261 RID: 609 RVA: 0x000036E1 File Offset: 0x000018E1
			// (set) Token: 0x06000262 RID: 610 RVA: 0x000036E9 File Offset: 0x000018E9
			public int cycleCount
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_cycleCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_cycleCount_Injected(ref this, value);
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000263 RID: 611 RVA: 0x000036F2 File Offset: 0x000018F2
			// (set) Token: 0x06000264 RID: 612 RVA: 0x000036FA File Offset: 0x000018FA
			public int rowIndex
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_rowIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_rowIndex_Injected(ref this, value);
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000265 RID: 613 RVA: 0x00003703 File Offset: 0x00001903
			// (set) Token: 0x06000266 RID: 614 RVA: 0x0000370B File Offset: 0x0000190B
			public UVChannelFlags uvChannelMask
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_uvChannelMask_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_uvChannelMask_Injected(ref this, value);
				}
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x06000267 RID: 615 RVA: 0x00003714 File Offset: 0x00001914
			public int spriteCount
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_spriteCount_Injected(ref this);
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x06000268 RID: 616 RVA: 0x0000371C File Offset: 0x0000191C
			// (set) Token: 0x06000269 RID: 617 RVA: 0x00003732 File Offset: 0x00001932
			public Vector2 speedRange
			{
				get
				{
					Vector2 vector;
					ParticleSystem.TextureSheetAnimationModule.get_speedRange_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_speedRange_Injected(ref this, ref value);
				}
			}

			// Token: 0x0600026A RID: 618 RVA: 0x0000373C File Offset: 0x0000193C
			[NativeThrows]
			public void AddSprite(Sprite sprite)
			{
				ParticleSystem.TextureSheetAnimationModule.AddSprite_Injected(ref this, sprite);
			}

			// Token: 0x0600026B RID: 619 RVA: 0x00003745 File Offset: 0x00001945
			[NativeThrows]
			public void RemoveSprite(int index)
			{
				ParticleSystem.TextureSheetAnimationModule.RemoveSprite_Injected(ref this, index);
			}

			// Token: 0x0600026C RID: 620 RVA: 0x0000374E File Offset: 0x0000194E
			[NativeThrows]
			public void SetSprite(int index, Sprite sprite)
			{
				ParticleSystem.TextureSheetAnimationModule.SetSprite_Injected(ref this, index, sprite);
			}

			// Token: 0x0600026D RID: 621 RVA: 0x00003758 File Offset: 0x00001958
			[NativeThrows]
			public Sprite GetSprite(int index)
			{
				return ParticleSystem.TextureSheetAnimationModule.GetSprite_Injected(ref this, index);
			}

			// Token: 0x0600026E RID: 622
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600026F RID: 623
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, bool value);

			// Token: 0x06000270 RID: 624
			[MethodImpl(4096)]
			private static extern ParticleSystemAnimationMode get_mode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000271 RID: 625
			[MethodImpl(4096)]
			private static extern void set_mode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationMode value);

			// Token: 0x06000272 RID: 626
			[MethodImpl(4096)]
			private static extern ParticleSystemAnimationTimeMode get_timeMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000273 RID: 627
			[MethodImpl(4096)]
			private static extern void set_timeMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationTimeMode value);

			// Token: 0x06000274 RID: 628
			[MethodImpl(4096)]
			private static extern float get_fps_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000275 RID: 629
			[MethodImpl(4096)]
			private static extern void set_fps_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, float value);

			// Token: 0x06000276 RID: 630
			[MethodImpl(4096)]
			private static extern int get_numTilesX_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000277 RID: 631
			[MethodImpl(4096)]
			private static extern void set_numTilesX_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			// Token: 0x06000278 RID: 632
			[MethodImpl(4096)]
			private static extern int get_numTilesY_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000279 RID: 633
			[MethodImpl(4096)]
			private static extern void set_numTilesY_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			// Token: 0x0600027A RID: 634
			[MethodImpl(4096)]
			private static extern ParticleSystemAnimationType get_animation_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600027B RID: 635
			[MethodImpl(4096)]
			private static extern void set_animation_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationType value);

			// Token: 0x0600027C RID: 636
			[MethodImpl(4096)]
			private static extern ParticleSystemAnimationRowMode get_rowMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600027D RID: 637
			[MethodImpl(4096)]
			private static extern void set_rowMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationRowMode value);

			// Token: 0x0600027E RID: 638
			[MethodImpl(4096)]
			private static extern void get_frameOverTime_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600027F RID: 639
			[MethodImpl(4096)]
			private static extern void set_frameOverTime_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000280 RID: 640
			[MethodImpl(4096)]
			private static extern float get_frameOverTimeMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000281 RID: 641
			[MethodImpl(4096)]
			private static extern void set_frameOverTimeMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, float value);

			// Token: 0x06000282 RID: 642
			[MethodImpl(4096)]
			private static extern void get_startFrame_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000283 RID: 643
			[MethodImpl(4096)]
			private static extern void set_startFrame_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000284 RID: 644
			[MethodImpl(4096)]
			private static extern float get_startFrameMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000285 RID: 645
			[MethodImpl(4096)]
			private static extern void set_startFrameMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, float value);

			// Token: 0x06000286 RID: 646
			[MethodImpl(4096)]
			private static extern int get_cycleCount_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000287 RID: 647
			[MethodImpl(4096)]
			private static extern void set_cycleCount_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			// Token: 0x06000288 RID: 648
			[MethodImpl(4096)]
			private static extern int get_rowIndex_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000289 RID: 649
			[MethodImpl(4096)]
			private static extern void set_rowIndex_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			// Token: 0x0600028A RID: 650
			[MethodImpl(4096)]
			private static extern UVChannelFlags get_uvChannelMask_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600028B RID: 651
			[MethodImpl(4096)]
			private static extern void set_uvChannelMask_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, UVChannelFlags value);

			// Token: 0x0600028C RID: 652
			[MethodImpl(4096)]
			private static extern int get_spriteCount_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600028D RID: 653
			[MethodImpl(4096)]
			private static extern void get_speedRange_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out Vector2 ret);

			// Token: 0x0600028E RID: 654
			[MethodImpl(4096)]
			private static extern void set_speedRange_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ref Vector2 value);

			// Token: 0x0600028F RID: 655
			[MethodImpl(4096)]
			private static extern void AddSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, Sprite sprite);

			// Token: 0x06000290 RID: 656
			[MethodImpl(4096)]
			private static extern void RemoveSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index);

			// Token: 0x06000291 RID: 657
			[MethodImpl(4096)]
			private static extern void SetSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index, Sprite sprite);

			// Token: 0x06000292 RID: 658
			[MethodImpl(4096)]
			private static extern Sprite GetSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index);

			// Token: 0x04000008 RID: 8
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000009 RID: 9
		[RequiredByNativeCode("particleSystemParticle", Optional = true)]
		public struct Particle
		{
			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000293 RID: 659 RVA: 0x00003764 File Offset: 0x00001964
			// (set) Token: 0x06000294 RID: 660 RVA: 0x0000377C File Offset: 0x0000197C
			[Obsolete("Please use Particle.remainingLifetime instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/Particle.remainingLifetime", false)]
			public float lifetime
			{
				get
				{
					return this.remainingLifetime;
				}
				set
				{
					this.remainingLifetime = value;
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000295 RID: 661 RVA: 0x00003788 File Offset: 0x00001988
			// (set) Token: 0x06000296 RID: 662 RVA: 0x000037AB File Offset: 0x000019AB
			[Obsolete("randomValue property is deprecated. Use randomSeed instead to control random behavior of particles.", false)]
			public float randomValue
			{
				get
				{
					return BitConverter.ToSingle(BitConverter.GetBytes(this.m_RandomSeed), 0);
				}
				set
				{
					this.m_RandomSeed = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
				}
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000297 RID: 663 RVA: 0x000037C0 File Offset: 0x000019C0
			// (set) Token: 0x06000298 RID: 664 RVA: 0x000037D8 File Offset: 0x000019D8
			[Obsolete("size property is deprecated. Use startSize or GetCurrentSize() instead.", false)]
			public float size
			{
				get
				{
					return this.startSize;
				}
				set
				{
					this.startSize = value;
				}
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000299 RID: 665 RVA: 0x000037E4 File Offset: 0x000019E4
			// (set) Token: 0x0600029A RID: 666 RVA: 0x000037FC File Offset: 0x000019FC
			[Obsolete("color property is deprecated. Use startColor or GetCurrentColor() instead.", false)]
			public Color32 color
			{
				get
				{
					return this.startColor;
				}
				set
				{
					this.startColor = value;
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x0600029B RID: 667 RVA: 0x00003808 File Offset: 0x00001A08
			// (set) Token: 0x0600029C RID: 668 RVA: 0x00003820 File Offset: 0x00001A20
			public Vector3 position
			{
				get
				{
					return this.m_Position;
				}
				set
				{
					this.m_Position = value;
				}
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600029D RID: 669 RVA: 0x0000382C File Offset: 0x00001A2C
			// (set) Token: 0x0600029E RID: 670 RVA: 0x00003844 File Offset: 0x00001A44
			public Vector3 velocity
			{
				get
				{
					return this.m_Velocity;
				}
				set
				{
					this.m_Velocity = value;
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x0600029F RID: 671 RVA: 0x00003850 File Offset: 0x00001A50
			public Vector3 animatedVelocity
			{
				get
				{
					return this.m_AnimatedVelocity;
				}
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060002A0 RID: 672 RVA: 0x00003868 File Offset: 0x00001A68
			public Vector3 totalVelocity
			{
				get
				{
					return this.m_Velocity + this.m_AnimatedVelocity;
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000388C File Offset: 0x00001A8C
			// (set) Token: 0x060002A2 RID: 674 RVA: 0x000038A4 File Offset: 0x00001AA4
			public float remainingLifetime
			{
				get
				{
					return this.m_Lifetime;
				}
				set
				{
					this.m_Lifetime = value;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060002A3 RID: 675 RVA: 0x000038B0 File Offset: 0x00001AB0
			// (set) Token: 0x060002A4 RID: 676 RVA: 0x000038C8 File Offset: 0x00001AC8
			public float startLifetime
			{
				get
				{
					return this.m_StartLifetime;
				}
				set
				{
					this.m_StartLifetime = value;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060002A5 RID: 677 RVA: 0x000038D4 File Offset: 0x00001AD4
			// (set) Token: 0x060002A6 RID: 678 RVA: 0x000038EC File Offset: 0x00001AEC
			public Color32 startColor
			{
				get
				{
					return this.m_StartColor;
				}
				set
				{
					this.m_StartColor = value;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060002A7 RID: 679 RVA: 0x000038F8 File Offset: 0x00001AF8
			// (set) Token: 0x060002A8 RID: 680 RVA: 0x00003910 File Offset: 0x00001B10
			public uint randomSeed
			{
				get
				{
					return this.m_RandomSeed;
				}
				set
				{
					this.m_RandomSeed = value;
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000391C File Offset: 0x00001B1C
			// (set) Token: 0x060002AA RID: 682 RVA: 0x00003934 File Offset: 0x00001B34
			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_AxisOfRotation;
				}
				set
				{
					this.m_AxisOfRotation = value;
				}
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x060002AB RID: 683 RVA: 0x00003940 File Offset: 0x00001B40
			// (set) Token: 0x060002AC RID: 684 RVA: 0x0000395D File Offset: 0x00001B5D
			public float startSize
			{
				get
				{
					return this.m_StartSize.x;
				}
				set
				{
					this.m_StartSize = new Vector3(value, value, value);
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060002AD RID: 685 RVA: 0x00003970 File Offset: 0x00001B70
			// (set) Token: 0x060002AE RID: 686 RVA: 0x00003988 File Offset: 0x00001B88
			public Vector3 startSize3D
			{
				get
				{
					return this.m_StartSize;
				}
				set
				{
					this.m_StartSize = value;
					this.m_Flags |= 1U;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060002AF RID: 687 RVA: 0x000039A0 File Offset: 0x00001BA0
			// (set) Token: 0x060002B0 RID: 688 RVA: 0x000039C3 File Offset: 0x00001BC3
			public float rotation
			{
				get
				{
					return this.m_Rotation.z * 57.29578f;
				}
				set
				{
					this.m_Rotation = new Vector3(0f, 0f, value * 0.017453292f);
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060002B1 RID: 689 RVA: 0x000039E4 File Offset: 0x00001BE4
			// (set) Token: 0x060002B2 RID: 690 RVA: 0x00003A06 File Offset: 0x00001C06
			public Vector3 rotation3D
			{
				get
				{
					return this.m_Rotation * 57.29578f;
				}
				set
				{
					this.m_Rotation = value * 0.017453292f;
					this.m_Flags |= 2U;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x00003A28 File Offset: 0x00001C28
			// (set) Token: 0x060002B4 RID: 692 RVA: 0x00003A4B File Offset: 0x00001C4B
			public float angularVelocity
			{
				get
				{
					return this.m_AngularVelocity.z * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = new Vector3(0f, 0f, value * 0.017453292f);
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060002B5 RID: 693 RVA: 0x00003A6C File Offset: 0x00001C6C
			// (set) Token: 0x060002B6 RID: 694 RVA: 0x00003A8E File Offset: 0x00001C8E
			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_AngularVelocity * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = value * 0.017453292f;
					this.m_Flags |= 2U;
				}
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x00003AB0 File Offset: 0x00001CB0
			public float GetCurrentSize(ParticleSystem system)
			{
				return system.GetParticleCurrentSize(ref this);
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x00003ACC File Offset: 0x00001CCC
			public Vector3 GetCurrentSize3D(ParticleSystem system)
			{
				return system.GetParticleCurrentSize3D(ref this);
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x00003AE8 File Offset: 0x00001CE8
			public Color32 GetCurrentColor(ParticleSystem system)
			{
				return system.GetParticleCurrentColor(ref this);
			}

			// Token: 0x060002BA RID: 698 RVA: 0x00003B01 File Offset: 0x00001D01
			public void SetMeshIndex(int index)
			{
				this.m_MeshIndex = index;
				this.m_Flags |= 4U;
			}

			// Token: 0x060002BB RID: 699 RVA: 0x00003B1C File Offset: 0x00001D1C
			public int GetMeshIndex(ParticleSystem system)
			{
				return system.GetParticleMeshIndex(ref this);
			}

			// Token: 0x04000009 RID: 9
			private Vector3 m_Position;

			// Token: 0x0400000A RID: 10
			private Vector3 m_Velocity;

			// Token: 0x0400000B RID: 11
			private Vector3 m_AnimatedVelocity;

			// Token: 0x0400000C RID: 12
			private Vector3 m_InitialVelocity;

			// Token: 0x0400000D RID: 13
			private Vector3 m_AxisOfRotation;

			// Token: 0x0400000E RID: 14
			private Vector3 m_Rotation;

			// Token: 0x0400000F RID: 15
			private Vector3 m_AngularVelocity;

			// Token: 0x04000010 RID: 16
			private Vector3 m_StartSize;

			// Token: 0x04000011 RID: 17
			private Color32 m_StartColor;

			// Token: 0x04000012 RID: 18
			private uint m_RandomSeed;

			// Token: 0x04000013 RID: 19
			private uint m_ParentRandomSeed;

			// Token: 0x04000014 RID: 20
			private float m_Lifetime;

			// Token: 0x04000015 RID: 21
			private float m_StartLifetime;

			// Token: 0x04000016 RID: 22
			private int m_MeshIndex;

			// Token: 0x04000017 RID: 23
			private float m_EmitAccumulator0;

			// Token: 0x04000018 RID: 24
			private float m_EmitAccumulator1;

			// Token: 0x04000019 RID: 25
			private uint m_Flags;

			// Token: 0x0200000A RID: 10
			[Flags]
			private enum Flags
			{
				// Token: 0x0400001B RID: 27
				Size3D = 1,
				// Token: 0x0400001C RID: 28
				Rotation3D = 2,
				// Token: 0x0400001D RID: 29
				MeshIndex = 4
			}
		}

		// Token: 0x0200000B RID: 11
		[NativeType(CodegenOptions.Custom, "MonoBurst", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		public struct Burst
		{
			// Token: 0x060002BC RID: 700 RVA: 0x00003B35 File Offset: 0x00001D35
			public Burst(float _time, short _count)
			{
				this.m_Time = _time;
				this.m_Count = (float)_count;
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			// Token: 0x060002BD RID: 701 RVA: 0x00003B69 File Offset: 0x00001D69
			public Burst(float _time, short _minCount, short _maxCount)
			{
				this.m_Time = _time;
				this.m_Count = new ParticleSystem.MinMaxCurve((float)_minCount, (float)_maxCount);
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			// Token: 0x060002BE RID: 702 RVA: 0x00003B9F File Offset: 0x00001D9F
			public Burst(float _time, short _minCount, short _maxCount, int _cycleCount, float _repeatInterval)
			{
				this.m_Time = _time;
				this.m_Count = new ParticleSystem.MinMaxCurve((float)_minCount, (float)_maxCount);
				this.m_RepeatCount = _cycleCount - 1;
				this.m_RepeatInterval = _repeatInterval;
				this.m_InvProbability = 0f;
			}

			// Token: 0x060002BF RID: 703 RVA: 0x00003BD5 File Offset: 0x00001DD5
			public Burst(float _time, ParticleSystem.MinMaxCurve _count)
			{
				this.m_Time = _time;
				this.m_Count = _count;
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x00003C03 File Offset: 0x00001E03
			public Burst(float _time, ParticleSystem.MinMaxCurve _count, int _cycleCount, float _repeatInterval)
			{
				this.m_Time = _time;
				this.m_Count = _count;
				this.m_RepeatCount = _cycleCount - 1;
				this.m_RepeatInterval = _repeatInterval;
				this.m_InvProbability = 0f;
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060002C1 RID: 705 RVA: 0x00003C30 File Offset: 0x00001E30
			// (set) Token: 0x060002C2 RID: 706 RVA: 0x00003C48 File Offset: 0x00001E48
			public float time
			{
				get
				{
					return this.m_Time;
				}
				set
				{
					this.m_Time = value;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060002C3 RID: 707 RVA: 0x00003C54 File Offset: 0x00001E54
			// (set) Token: 0x060002C4 RID: 708 RVA: 0x00003C6C File Offset: 0x00001E6C
			public ParticleSystem.MinMaxCurve count
			{
				get
				{
					return this.m_Count;
				}
				set
				{
					this.m_Count = value;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060002C5 RID: 709 RVA: 0x00003C78 File Offset: 0x00001E78
			// (set) Token: 0x060002C6 RID: 710 RVA: 0x00003C96 File Offset: 0x00001E96
			public short minCount
			{
				get
				{
					return (short)this.m_Count.constantMin;
				}
				set
				{
					this.m_Count.constantMin = (float)value;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x00003CA8 File Offset: 0x00001EA8
			// (set) Token: 0x060002C8 RID: 712 RVA: 0x00003CC6 File Offset: 0x00001EC6
			public short maxCount
			{
				get
				{
					return (short)this.m_Count.constantMax;
				}
				set
				{
					this.m_Count.constantMax = (float)value;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x060002C9 RID: 713 RVA: 0x00003CD8 File Offset: 0x00001ED8
			// (set) Token: 0x060002CA RID: 714 RVA: 0x00003CF4 File Offset: 0x00001EF4
			public int cycleCount
			{
				get
				{
					return this.m_RepeatCount + 1;
				}
				set
				{
					bool flag = value < 0;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("cycleCount", "cycleCount must be at least 0: " + value);
					}
					this.m_RepeatCount = value - 1;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x060002CB RID: 715 RVA: 0x00003D30 File Offset: 0x00001F30
			// (set) Token: 0x060002CC RID: 716 RVA: 0x00003D48 File Offset: 0x00001F48
			public float repeatInterval
			{
				get
				{
					return this.m_RepeatInterval;
				}
				set
				{
					bool flag = value <= 0f;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("repeatInterval", "repeatInterval must be greater than 0.0f: " + value);
					}
					this.m_RepeatInterval = value;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060002CD RID: 717 RVA: 0x00003D88 File Offset: 0x00001F88
			// (set) Token: 0x060002CE RID: 718 RVA: 0x00003DA8 File Offset: 0x00001FA8
			public float probability
			{
				get
				{
					return 1f - this.m_InvProbability;
				}
				set
				{
					bool flag = value < 0f || value > 1f;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("probability", "probability must be between 0.0f and 1.0f: " + value);
					}
					this.m_InvProbability = 1f - value;
				}
			}

			// Token: 0x0400001E RID: 30
			private float m_Time;

			// Token: 0x0400001F RID: 31
			private ParticleSystem.MinMaxCurve m_Count;

			// Token: 0x04000020 RID: 32
			private int m_RepeatCount;

			// Token: 0x04000021 RID: 33
			private float m_RepeatInterval;

			// Token: 0x04000022 RID: 34
			private float m_InvProbability;
		}

		// Token: 0x0200000C RID: 12
		[NativeType(CodegenOptions.Custom, "MonoMinMaxCurve", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[Serializable]
		public struct MinMaxCurve
		{
			// Token: 0x060002CF RID: 719 RVA: 0x00003DF5 File Offset: 0x00001FF5
			public MinMaxCurve(float constant)
			{
				this.m_Mode = ParticleSystemCurveMode.Constant;
				this.m_CurveMultiplier = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = constant;
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x00003E2A File Offset: 0x0000202A
			public MinMaxCurve(float multiplier, AnimationCurve curve)
			{
				this.m_Mode = ParticleSystemCurveMode.Curve;
				this.m_CurveMultiplier = multiplier;
				this.m_CurveMin = null;
				this.m_CurveMax = curve;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x00003E5F File Offset: 0x0000205F
			public MinMaxCurve(float multiplier, AnimationCurve min, AnimationCurve max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoCurves;
				this.m_CurveMultiplier = multiplier;
				this.m_CurveMin = min;
				this.m_CurveMax = max;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x00003E94 File Offset: 0x00002094
			public MinMaxCurve(float min, float max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoConstants;
				this.m_CurveMultiplier = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = min;
				this.m_ConstantMax = max;
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060002D3 RID: 723 RVA: 0x00003EC8 File Offset: 0x000020C8
			// (set) Token: 0x060002D4 RID: 724 RVA: 0x00003EE0 File Offset: 0x000020E0
			public ParticleSystemCurveMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060002D5 RID: 725 RVA: 0x00003EEC File Offset: 0x000020EC
			// (set) Token: 0x060002D6 RID: 726 RVA: 0x00003F04 File Offset: 0x00002104
			public float curveMultiplier
			{
				get
				{
					return this.m_CurveMultiplier;
				}
				set
				{
					this.m_CurveMultiplier = value;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060002D7 RID: 727 RVA: 0x00003F10 File Offset: 0x00002110
			// (set) Token: 0x060002D8 RID: 728 RVA: 0x00003F28 File Offset: 0x00002128
			public AnimationCurve curveMax
			{
				get
				{
					return this.m_CurveMax;
				}
				set
				{
					this.m_CurveMax = value;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x060002D9 RID: 729 RVA: 0x00003F34 File Offset: 0x00002134
			// (set) Token: 0x060002DA RID: 730 RVA: 0x00003F4C File Offset: 0x0000214C
			public AnimationCurve curveMin
			{
				get
				{
					return this.m_CurveMin;
				}
				set
				{
					this.m_CurveMin = value;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x060002DB RID: 731 RVA: 0x00003F58 File Offset: 0x00002158
			// (set) Token: 0x060002DC RID: 732 RVA: 0x00003F70 File Offset: 0x00002170
			public float constantMax
			{
				get
				{
					return this.m_ConstantMax;
				}
				set
				{
					this.m_ConstantMax = value;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x060002DD RID: 733 RVA: 0x00003F7C File Offset: 0x0000217C
			// (set) Token: 0x060002DE RID: 734 RVA: 0x00003F94 File Offset: 0x00002194
			public float constantMin
			{
				get
				{
					return this.m_ConstantMin;
				}
				set
				{
					this.m_ConstantMin = value;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x060002DF RID: 735 RVA: 0x00003FA0 File Offset: 0x000021A0
			// (set) Token: 0x060002E0 RID: 736 RVA: 0x00003F70 File Offset: 0x00002170
			public float constant
			{
				get
				{
					return this.m_ConstantMax;
				}
				set
				{
					this.m_ConstantMax = value;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x060002E1 RID: 737 RVA: 0x00003FB8 File Offset: 0x000021B8
			// (set) Token: 0x060002E2 RID: 738 RVA: 0x00003F28 File Offset: 0x00002128
			public AnimationCurve curve
			{
				get
				{
					return this.m_CurveMax;
				}
				set
				{
					this.m_CurveMax = value;
				}
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x00003FD0 File Offset: 0x000021D0
			public float Evaluate(float time)
			{
				return this.Evaluate(time, 1f);
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x00003FF0 File Offset: 0x000021F0
			public float Evaluate(float time, float lerpFactor)
			{
				switch (this.mode)
				{
				case ParticleSystemCurveMode.Constant:
					return this.m_ConstantMax;
				case ParticleSystemCurveMode.TwoCurves:
					return Mathf.Lerp(this.m_CurveMin.Evaluate(time), this.m_CurveMax.Evaluate(time), lerpFactor) * this.m_CurveMultiplier;
				case ParticleSystemCurveMode.TwoConstants:
					return Mathf.Lerp(this.m_ConstantMin, this.m_ConstantMax, lerpFactor);
				}
				return this.m_CurveMax.Evaluate(time) * this.m_CurveMultiplier;
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000407C File Offset: 0x0000227C
			public static implicit operator ParticleSystem.MinMaxCurve(float constant)
			{
				return new ParticleSystem.MinMaxCurve(constant);
			}

			// Token: 0x04000023 RID: 35
			[SerializeField]
			private ParticleSystemCurveMode m_Mode;

			// Token: 0x04000024 RID: 36
			[SerializeField]
			private float m_CurveMultiplier;

			// Token: 0x04000025 RID: 37
			[SerializeField]
			private AnimationCurve m_CurveMin;

			// Token: 0x04000026 RID: 38
			[SerializeField]
			private AnimationCurve m_CurveMax;

			// Token: 0x04000027 RID: 39
			[SerializeField]
			private float m_ConstantMin;

			// Token: 0x04000028 RID: 40
			[SerializeField]
			private float m_ConstantMax;
		}

		// Token: 0x0200000D RID: 13
		[NativeType(CodegenOptions.Custom, "MonoMinMaxGradient", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[Serializable]
		public struct MinMaxGradient
		{
			// Token: 0x060002E6 RID: 742 RVA: 0x00004094 File Offset: 0x00002294
			public MinMaxGradient(Color color)
			{
				this.m_Mode = ParticleSystemGradientMode.Color;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = color;
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x000040BE File Offset: 0x000022BE
			public MinMaxGradient(Gradient gradient)
			{
				this.m_Mode = ParticleSystemGradientMode.Gradient;
				this.m_GradientMin = null;
				this.m_GradientMax = gradient;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x000040EC File Offset: 0x000022EC
			public MinMaxGradient(Color min, Color max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoColors;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = min;
				this.m_ColorMax = max;
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x00004112 File Offset: 0x00002312
			public MinMaxGradient(Gradient min, Gradient max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoGradients;
				this.m_GradientMin = min;
				this.m_GradientMax = max;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x060002EA RID: 746 RVA: 0x00004140 File Offset: 0x00002340
			// (set) Token: 0x060002EB RID: 747 RVA: 0x00004158 File Offset: 0x00002358
			public ParticleSystemGradientMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x060002EC RID: 748 RVA: 0x00004164 File Offset: 0x00002364
			// (set) Token: 0x060002ED RID: 749 RVA: 0x0000417C File Offset: 0x0000237C
			public Gradient gradientMax
			{
				get
				{
					return this.m_GradientMax;
				}
				set
				{
					this.m_GradientMax = value;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x060002EE RID: 750 RVA: 0x00004188 File Offset: 0x00002388
			// (set) Token: 0x060002EF RID: 751 RVA: 0x000041A0 File Offset: 0x000023A0
			public Gradient gradientMin
			{
				get
				{
					return this.m_GradientMin;
				}
				set
				{
					this.m_GradientMin = value;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x000041AC File Offset: 0x000023AC
			// (set) Token: 0x060002F1 RID: 753 RVA: 0x000041C4 File Offset: 0x000023C4
			public Color colorMax
			{
				get
				{
					return this.m_ColorMax;
				}
				set
				{
					this.m_ColorMax = value;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x060002F2 RID: 754 RVA: 0x000041D0 File Offset: 0x000023D0
			// (set) Token: 0x060002F3 RID: 755 RVA: 0x000041E8 File Offset: 0x000023E8
			public Color colorMin
			{
				get
				{
					return this.m_ColorMin;
				}
				set
				{
					this.m_ColorMin = value;
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x060002F4 RID: 756 RVA: 0x000041F4 File Offset: 0x000023F4
			// (set) Token: 0x060002F5 RID: 757 RVA: 0x000041C4 File Offset: 0x000023C4
			public Color color
			{
				get
				{
					return this.m_ColorMax;
				}
				set
				{
					this.m_ColorMax = value;
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000420C File Offset: 0x0000240C
			// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000417C File Offset: 0x0000237C
			public Gradient gradient
			{
				get
				{
					return this.m_GradientMax;
				}
				set
				{
					this.m_GradientMax = value;
				}
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x00004224 File Offset: 0x00002424
			public Color Evaluate(float time)
			{
				return this.Evaluate(time, 1f);
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x00004244 File Offset: 0x00002444
			public Color Evaluate(float time, float lerpFactor)
			{
				switch (this.m_Mode)
				{
				case ParticleSystemGradientMode.Color:
					return this.m_ColorMax;
				case ParticleSystemGradientMode.TwoColors:
					return Color.Lerp(this.m_ColorMin, this.m_ColorMax, lerpFactor);
				case ParticleSystemGradientMode.TwoGradients:
					return Color.Lerp(this.m_GradientMin.Evaluate(time), this.m_GradientMax.Evaluate(time), lerpFactor);
				case ParticleSystemGradientMode.RandomColor:
					return this.m_GradientMax.Evaluate(lerpFactor);
				}
				return this.m_GradientMax.Evaluate(time);
			}

			// Token: 0x060002FA RID: 762 RVA: 0x000042D4 File Offset: 0x000024D4
			public static implicit operator ParticleSystem.MinMaxGradient(Color color)
			{
				return new ParticleSystem.MinMaxGradient(color);
			}

			// Token: 0x060002FB RID: 763 RVA: 0x000042EC File Offset: 0x000024EC
			public static implicit operator ParticleSystem.MinMaxGradient(Gradient gradient)
			{
				return new ParticleSystem.MinMaxGradient(gradient);
			}

			// Token: 0x04000029 RID: 41
			[SerializeField]
			private ParticleSystemGradientMode m_Mode;

			// Token: 0x0400002A RID: 42
			[SerializeField]
			private Gradient m_GradientMin;

			// Token: 0x0400002B RID: 43
			[SerializeField]
			private Gradient m_GradientMax;

			// Token: 0x0400002C RID: 44
			[SerializeField]
			private Color m_ColorMin;

			// Token: 0x0400002D RID: 45
			[SerializeField]
			private Color m_ColorMax;
		}

		// Token: 0x0200000E RID: 14
		public struct EmitParams
		{
			// Token: 0x170000DD RID: 221
			// (get) Token: 0x060002FC RID: 764 RVA: 0x00004304 File Offset: 0x00002504
			// (set) Token: 0x060002FD RID: 765 RVA: 0x0000431C File Offset: 0x0000251C
			public ParticleSystem.Particle particle
			{
				get
				{
					return this.m_Particle;
				}
				set
				{
					this.m_Particle = value;
					this.m_PositionSet = true;
					this.m_VelocitySet = true;
					this.m_AxisOfRotationSet = true;
					this.m_RotationSet = true;
					this.m_AngularVelocitySet = true;
					this.m_StartSizeSet = true;
					this.m_StartColorSet = true;
					this.m_RandomSeedSet = true;
					this.m_StartLifetimeSet = true;
					this.m_MeshIndexSet = true;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x060002FE RID: 766 RVA: 0x00004378 File Offset: 0x00002578
			// (set) Token: 0x060002FF RID: 767 RVA: 0x00004395 File Offset: 0x00002595
			public Vector3 position
			{
				get
				{
					return this.m_Particle.position;
				}
				set
				{
					this.m_Particle.position = value;
					this.m_PositionSet = true;
				}
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000300 RID: 768 RVA: 0x000043AC File Offset: 0x000025AC
			// (set) Token: 0x06000301 RID: 769 RVA: 0x000043C4 File Offset: 0x000025C4
			public bool applyShapeToPosition
			{
				get
				{
					return this.m_ApplyShapeToPosition;
				}
				set
				{
					this.m_ApplyShapeToPosition = value;
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000302 RID: 770 RVA: 0x000043D0 File Offset: 0x000025D0
			// (set) Token: 0x06000303 RID: 771 RVA: 0x000043ED File Offset: 0x000025ED
			public Vector3 velocity
			{
				get
				{
					return this.m_Particle.velocity;
				}
				set
				{
					this.m_Particle.velocity = value;
					this.m_VelocitySet = true;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000304 RID: 772 RVA: 0x00004404 File Offset: 0x00002604
			// (set) Token: 0x06000305 RID: 773 RVA: 0x00004421 File Offset: 0x00002621
			public float startLifetime
			{
				get
				{
					return this.m_Particle.startLifetime;
				}
				set
				{
					this.m_Particle.startLifetime = value;
					this.m_StartLifetimeSet = true;
				}
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000306 RID: 774 RVA: 0x00004438 File Offset: 0x00002638
			// (set) Token: 0x06000307 RID: 775 RVA: 0x00004455 File Offset: 0x00002655
			public float startSize
			{
				get
				{
					return this.m_Particle.startSize;
				}
				set
				{
					this.m_Particle.startSize = value;
					this.m_StartSizeSet = true;
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000308 RID: 776 RVA: 0x0000446C File Offset: 0x0000266C
			// (set) Token: 0x06000309 RID: 777 RVA: 0x00004489 File Offset: 0x00002689
			public Vector3 startSize3D
			{
				get
				{
					return this.m_Particle.startSize3D;
				}
				set
				{
					this.m_Particle.startSize3D = value;
					this.m_StartSizeSet = true;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600030A RID: 778 RVA: 0x000044A0 File Offset: 0x000026A0
			// (set) Token: 0x0600030B RID: 779 RVA: 0x000044BD File Offset: 0x000026BD
			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_Particle.axisOfRotation;
				}
				set
				{
					this.m_Particle.axisOfRotation = value;
					this.m_AxisOfRotationSet = true;
				}
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x0600030C RID: 780 RVA: 0x000044D4 File Offset: 0x000026D4
			// (set) Token: 0x0600030D RID: 781 RVA: 0x000044F1 File Offset: 0x000026F1
			public float rotation
			{
				get
				{
					return this.m_Particle.rotation;
				}
				set
				{
					this.m_Particle.rotation = value;
					this.m_RotationSet = true;
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x0600030E RID: 782 RVA: 0x00004508 File Offset: 0x00002708
			// (set) Token: 0x0600030F RID: 783 RVA: 0x00004525 File Offset: 0x00002725
			public Vector3 rotation3D
			{
				get
				{
					return this.m_Particle.rotation3D;
				}
				set
				{
					this.m_Particle.rotation3D = value;
					this.m_RotationSet = true;
				}
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000310 RID: 784 RVA: 0x0000453C File Offset: 0x0000273C
			// (set) Token: 0x06000311 RID: 785 RVA: 0x00004559 File Offset: 0x00002759
			public float angularVelocity
			{
				get
				{
					return this.m_Particle.angularVelocity;
				}
				set
				{
					this.m_Particle.angularVelocity = value;
					this.m_AngularVelocitySet = true;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000312 RID: 786 RVA: 0x00004570 File Offset: 0x00002770
			// (set) Token: 0x06000313 RID: 787 RVA: 0x0000458D File Offset: 0x0000278D
			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_Particle.angularVelocity3D;
				}
				set
				{
					this.m_Particle.angularVelocity3D = value;
					this.m_AngularVelocitySet = true;
				}
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000314 RID: 788 RVA: 0x000045A4 File Offset: 0x000027A4
			// (set) Token: 0x06000315 RID: 789 RVA: 0x000045C1 File Offset: 0x000027C1
			public Color32 startColor
			{
				get
				{
					return this.m_Particle.startColor;
				}
				set
				{
					this.m_Particle.startColor = value;
					this.m_StartColorSet = true;
				}
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000316 RID: 790 RVA: 0x000045D8 File Offset: 0x000027D8
			// (set) Token: 0x06000317 RID: 791 RVA: 0x000045F5 File Offset: 0x000027F5
			public uint randomSeed
			{
				get
				{
					return this.m_Particle.randomSeed;
				}
				set
				{
					this.m_Particle.randomSeed = value;
					this.m_RandomSeedSet = true;
				}
			}

			// Token: 0x170000EB RID: 235
			// (set) Token: 0x06000318 RID: 792 RVA: 0x0000460C File Offset: 0x0000280C
			public int meshIndex
			{
				set
				{
					this.m_Particle.SetMeshIndex(value);
					this.m_MeshIndexSet = true;
				}
			}

			// Token: 0x06000319 RID: 793 RVA: 0x00004623 File Offset: 0x00002823
			public void ResetPosition()
			{
				this.m_PositionSet = false;
			}

			// Token: 0x0600031A RID: 794 RVA: 0x0000462D File Offset: 0x0000282D
			public void ResetVelocity()
			{
				this.m_VelocitySet = false;
			}

			// Token: 0x0600031B RID: 795 RVA: 0x00004637 File Offset: 0x00002837
			public void ResetAxisOfRotation()
			{
				this.m_AxisOfRotationSet = false;
			}

			// Token: 0x0600031C RID: 796 RVA: 0x00004641 File Offset: 0x00002841
			public void ResetRotation()
			{
				this.m_RotationSet = false;
			}

			// Token: 0x0600031D RID: 797 RVA: 0x0000464B File Offset: 0x0000284B
			public void ResetAngularVelocity()
			{
				this.m_AngularVelocitySet = false;
			}

			// Token: 0x0600031E RID: 798 RVA: 0x00004655 File Offset: 0x00002855
			public void ResetStartSize()
			{
				this.m_StartSizeSet = false;
			}

			// Token: 0x0600031F RID: 799 RVA: 0x0000465F File Offset: 0x0000285F
			public void ResetStartColor()
			{
				this.m_StartColorSet = false;
			}

			// Token: 0x06000320 RID: 800 RVA: 0x00004669 File Offset: 0x00002869
			public void ResetRandomSeed()
			{
				this.m_RandomSeedSet = false;
			}

			// Token: 0x06000321 RID: 801 RVA: 0x00004673 File Offset: 0x00002873
			public void ResetStartLifetime()
			{
				this.m_StartLifetimeSet = false;
			}

			// Token: 0x06000322 RID: 802 RVA: 0x0000467D File Offset: 0x0000287D
			public void ResetMeshIndex()
			{
				this.m_MeshIndexSet = false;
			}

			// Token: 0x0400002E RID: 46
			[NativeName("particle")]
			private ParticleSystem.Particle m_Particle;

			// Token: 0x0400002F RID: 47
			[NativeName("positionSet")]
			private bool m_PositionSet;

			// Token: 0x04000030 RID: 48
			[NativeName("velocitySet")]
			private bool m_VelocitySet;

			// Token: 0x04000031 RID: 49
			[NativeName("axisOfRotationSet")]
			private bool m_AxisOfRotationSet;

			// Token: 0x04000032 RID: 50
			[NativeName("rotationSet")]
			private bool m_RotationSet;

			// Token: 0x04000033 RID: 51
			[NativeName("rotationalSpeedSet")]
			private bool m_AngularVelocitySet;

			// Token: 0x04000034 RID: 52
			[NativeName("startSizeSet")]
			private bool m_StartSizeSet;

			// Token: 0x04000035 RID: 53
			[NativeName("startColorSet")]
			private bool m_StartColorSet;

			// Token: 0x04000036 RID: 54
			[NativeName("randomSeedSet")]
			private bool m_RandomSeedSet;

			// Token: 0x04000037 RID: 55
			[NativeName("startLifetimeSet")]
			private bool m_StartLifetimeSet;

			// Token: 0x04000038 RID: 56
			[NativeName("meshIndexSet")]
			private bool m_MeshIndexSet;

			// Token: 0x04000039 RID: 57
			[NativeName("applyShapeToPosition")]
			private bool m_ApplyShapeToPosition;
		}

		// Token: 0x0200000F RID: 15
		public struct PlaybackState
		{
			// Token: 0x0400003A RID: 58
			internal float m_AccumulatedDt;

			// Token: 0x0400003B RID: 59
			internal float m_StartDelay;

			// Token: 0x0400003C RID: 60
			internal float m_PlaybackTime;

			// Token: 0x0400003D RID: 61
			internal int m_RingBufferIndex;

			// Token: 0x0400003E RID: 62
			internal ParticleSystem.PlaybackState.Emission m_Emission;

			// Token: 0x0400003F RID: 63
			internal ParticleSystem.PlaybackState.Initial m_Initial;

			// Token: 0x04000040 RID: 64
			internal ParticleSystem.PlaybackState.Shape m_Shape;

			// Token: 0x04000041 RID: 65
			internal ParticleSystem.PlaybackState.Force m_Force;

			// Token: 0x04000042 RID: 66
			internal ParticleSystem.PlaybackState.Collision m_Collision;

			// Token: 0x04000043 RID: 67
			internal ParticleSystem.PlaybackState.Noise m_Noise;

			// Token: 0x04000044 RID: 68
			internal ParticleSystem.PlaybackState.Lights m_Lights;

			// Token: 0x04000045 RID: 69
			internal ParticleSystem.PlaybackState.Trail m_Trail;

			// Token: 0x02000010 RID: 16
			internal struct Seed
			{
				// Token: 0x04000046 RID: 70
				public uint x;

				// Token: 0x04000047 RID: 71
				public uint y;

				// Token: 0x04000048 RID: 72
				public uint z;

				// Token: 0x04000049 RID: 73
				public uint w;
			}

			// Token: 0x02000011 RID: 17
			internal struct Seed4
			{
				// Token: 0x0400004A RID: 74
				public ParticleSystem.PlaybackState.Seed x;

				// Token: 0x0400004B RID: 75
				public ParticleSystem.PlaybackState.Seed y;

				// Token: 0x0400004C RID: 76
				public ParticleSystem.PlaybackState.Seed z;

				// Token: 0x0400004D RID: 77
				public ParticleSystem.PlaybackState.Seed w;
			}

			// Token: 0x02000012 RID: 18
			internal struct Emission
			{
				// Token: 0x0400004E RID: 78
				public float m_ParticleSpacing;

				// Token: 0x0400004F RID: 79
				public float m_ToEmitAccumulator;

				// Token: 0x04000050 RID: 80
				public ParticleSystem.PlaybackState.Seed m_Random;
			}

			// Token: 0x02000013 RID: 19
			internal struct Initial
			{
				// Token: 0x04000051 RID: 81
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			// Token: 0x02000014 RID: 20
			internal struct Shape
			{
				// Token: 0x04000052 RID: 82
				public ParticleSystem.PlaybackState.Seed4 m_Random;

				// Token: 0x04000053 RID: 83
				public float m_RadiusTimer;

				// Token: 0x04000054 RID: 84
				public float m_RadiusTimerPrev;

				// Token: 0x04000055 RID: 85
				public float m_ArcTimer;

				// Token: 0x04000056 RID: 86
				public float m_ArcTimerPrev;

				// Token: 0x04000057 RID: 87
				public float m_MeshSpawnTimer;

				// Token: 0x04000058 RID: 88
				public float m_MeshSpawnTimerPrev;

				// Token: 0x04000059 RID: 89
				public int m_OrderedMeshVertexIndex;
			}

			// Token: 0x02000015 RID: 21
			internal struct Force
			{
				// Token: 0x0400005A RID: 90
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			// Token: 0x02000016 RID: 22
			internal struct Collision
			{
				// Token: 0x0400005B RID: 91
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			// Token: 0x02000017 RID: 23
			internal struct Noise
			{
				// Token: 0x0400005C RID: 92
				public float m_ScrollOffset;
			}

			// Token: 0x02000018 RID: 24
			internal struct Lights
			{
				// Token: 0x0400005D RID: 93
				public ParticleSystem.PlaybackState.Seed m_Random;

				// Token: 0x0400005E RID: 94
				public float m_ParticleEmissionCounter;
			}

			// Token: 0x02000019 RID: 25
			internal struct Trail
			{
				// Token: 0x0400005F RID: 95
				public float m_Timer;
			}
		}

		// Token: 0x0200001A RID: 26
		[NativeType(CodegenOptions.Custom, "MonoParticleTrails")]
		public struct Trails
		{
			// Token: 0x04000060 RID: 96
			internal List<Vector4> positions;

			// Token: 0x04000061 RID: 97
			internal List<int> frontPositions;

			// Token: 0x04000062 RID: 98
			internal List<int> backPositions;

			// Token: 0x04000063 RID: 99
			internal List<int> positionCounts;

			// Token: 0x04000064 RID: 100
			internal int maxTrailCount;

			// Token: 0x04000065 RID: 101
			internal int maxPositionsPerTrailCount;
		}

		// Token: 0x0200001B RID: 27
		public struct VelocityOverLifetimeModule
		{
			// Token: 0x06000323 RID: 803 RVA: 0x00004687 File Offset: 0x00002887
			internal VelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000324 RID: 804 RVA: 0x00004691 File Offset: 0x00002891
			// (set) Token: 0x06000325 RID: 805 RVA: 0x00004699 File Offset: 0x00002899
			public bool enabled
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x06000326 RID: 806 RVA: 0x000046A4 File Offset: 0x000028A4
			// (set) Token: 0x06000327 RID: 807 RVA: 0x000046BA File Offset: 0x000028BA
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_x_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06000328 RID: 808 RVA: 0x000046C4 File Offset: 0x000028C4
			// (set) Token: 0x06000329 RID: 809 RVA: 0x000046DA File Offset: 0x000028DA
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_y_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x0600032A RID: 810 RVA: 0x000046E4 File Offset: 0x000028E4
			// (set) Token: 0x0600032B RID: 811 RVA: 0x000046FA File Offset: 0x000028FA
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_z_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x0600032C RID: 812 RVA: 0x00004704 File Offset: 0x00002904
			// (set) Token: 0x0600032D RID: 813 RVA: 0x0000470C File Offset: 0x0000290C
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x0600032E RID: 814 RVA: 0x00004715 File Offset: 0x00002915
			// (set) Token: 0x0600032F RID: 815 RVA: 0x0000471D File Offset: 0x0000291D
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000330 RID: 816 RVA: 0x00004726 File Offset: 0x00002926
			// (set) Token: 0x06000331 RID: 817 RVA: 0x0000472E File Offset: 0x0000292E
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000332 RID: 818 RVA: 0x00004738 File Offset: 0x00002938
			// (set) Token: 0x06000333 RID: 819 RVA: 0x0000474E File Offset: 0x0000294E
			public ParticleSystem.MinMaxCurve orbitalX
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalX_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalX_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000334 RID: 820 RVA: 0x00004758 File Offset: 0x00002958
			// (set) Token: 0x06000335 RID: 821 RVA: 0x0000476E File Offset: 0x0000296E
			public ParticleSystem.MinMaxCurve orbitalY
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalY_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalY_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x06000336 RID: 822 RVA: 0x00004778 File Offset: 0x00002978
			// (set) Token: 0x06000337 RID: 823 RVA: 0x0000478E File Offset: 0x0000298E
			public ParticleSystem.MinMaxCurve orbitalZ
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalZ_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000338 RID: 824 RVA: 0x00004798 File Offset: 0x00002998
			// (set) Token: 0x06000339 RID: 825 RVA: 0x000047A0 File Offset: 0x000029A0
			public float orbitalXMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x0600033A RID: 826 RVA: 0x000047A9 File Offset: 0x000029A9
			// (set) Token: 0x0600033B RID: 827 RVA: 0x000047B1 File Offset: 0x000029B1
			public float orbitalYMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x0600033C RID: 828 RVA: 0x000047BA File Offset: 0x000029BA
			// (set) Token: 0x0600033D RID: 829 RVA: 0x000047C2 File Offset: 0x000029C2
			public float orbitalZMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x0600033E RID: 830 RVA: 0x000047CC File Offset: 0x000029CC
			// (set) Token: 0x0600033F RID: 831 RVA: 0x000047E2 File Offset: 0x000029E2
			public ParticleSystem.MinMaxCurve orbitalOffsetX
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetX_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetX_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000340 RID: 832 RVA: 0x000047EC File Offset: 0x000029EC
			// (set) Token: 0x06000341 RID: 833 RVA: 0x00004802 File Offset: 0x00002A02
			public ParticleSystem.MinMaxCurve orbitalOffsetY
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetY_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetY_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000342 RID: 834 RVA: 0x0000480C File Offset: 0x00002A0C
			// (set) Token: 0x06000343 RID: 835 RVA: 0x00004822 File Offset: 0x00002A22
			public ParticleSystem.MinMaxCurve orbitalOffsetZ
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetZ_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000344 RID: 836 RVA: 0x0000482C File Offset: 0x00002A2C
			// (set) Token: 0x06000345 RID: 837 RVA: 0x00004834 File Offset: 0x00002A34
			public float orbitalOffsetXMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000346 RID: 838 RVA: 0x0000483D File Offset: 0x00002A3D
			// (set) Token: 0x06000347 RID: 839 RVA: 0x00004845 File Offset: 0x00002A45
			public float orbitalOffsetYMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000348 RID: 840 RVA: 0x0000484E File Offset: 0x00002A4E
			// (set) Token: 0x06000349 RID: 841 RVA: 0x00004856 File Offset: 0x00002A56
			public float orbitalOffsetZMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600034A RID: 842 RVA: 0x00004860 File Offset: 0x00002A60
			// (set) Token: 0x0600034B RID: 843 RVA: 0x00004876 File Offset: 0x00002A76
			public ParticleSystem.MinMaxCurve radial
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_radial_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_radial_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x0600034C RID: 844 RVA: 0x00004880 File Offset: 0x00002A80
			// (set) Token: 0x0600034D RID: 845 RVA: 0x00004888 File Offset: 0x00002A88
			public float radialMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_radialMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_radialMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x0600034E RID: 846 RVA: 0x00004894 File Offset: 0x00002A94
			// (set) Token: 0x0600034F RID: 847 RVA: 0x000048AA File Offset: 0x00002AAA
			public ParticleSystem.MinMaxCurve speedModifier
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.VelocityOverLifetimeModule.get_speedModifier_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_speedModifier_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000350 RID: 848 RVA: 0x000048B4 File Offset: 0x00002AB4
			// (set) Token: 0x06000351 RID: 849 RVA: 0x000048BC File Offset: 0x00002ABC
			public float speedModifierMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_speedModifierMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_speedModifierMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06000352 RID: 850 RVA: 0x000048C5 File Offset: 0x00002AC5
			// (set) Token: 0x06000353 RID: 851 RVA: 0x000048CD File Offset: 0x00002ACD
			public ParticleSystemSimulationSpace space
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_space_Injected(ref this, value);
				}
			}

			// Token: 0x06000354 RID: 852
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000355 RID: 853
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x06000356 RID: 854
			[MethodImpl(4096)]
			private static extern void get_x_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000357 RID: 855
			[MethodImpl(4096)]
			private static extern void set_x_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000358 RID: 856
			[MethodImpl(4096)]
			private static extern void get_y_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000359 RID: 857
			[MethodImpl(4096)]
			private static extern void set_y_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600035A RID: 858
			[MethodImpl(4096)]
			private static extern void get_z_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600035B RID: 859
			[MethodImpl(4096)]
			private static extern void set_z_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600035C RID: 860
			[MethodImpl(4096)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x0600035D RID: 861
			[MethodImpl(4096)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600035E RID: 862
			[MethodImpl(4096)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x0600035F RID: 863
			[MethodImpl(4096)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000360 RID: 864
			[MethodImpl(4096)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000361 RID: 865
			[MethodImpl(4096)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000362 RID: 866
			[MethodImpl(4096)]
			private static extern void get_orbitalX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000363 RID: 867
			[MethodImpl(4096)]
			private static extern void set_orbitalX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000364 RID: 868
			[MethodImpl(4096)]
			private static extern void get_orbitalY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000365 RID: 869
			[MethodImpl(4096)]
			private static extern void set_orbitalY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000366 RID: 870
			[MethodImpl(4096)]
			private static extern void get_orbitalZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000367 RID: 871
			[MethodImpl(4096)]
			private static extern void set_orbitalZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000368 RID: 872
			[MethodImpl(4096)]
			private static extern float get_orbitalXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000369 RID: 873
			[MethodImpl(4096)]
			private static extern void set_orbitalXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600036A RID: 874
			[MethodImpl(4096)]
			private static extern float get_orbitalYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x0600036B RID: 875
			[MethodImpl(4096)]
			private static extern void set_orbitalYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600036C RID: 876
			[MethodImpl(4096)]
			private static extern float get_orbitalZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x0600036D RID: 877
			[MethodImpl(4096)]
			private static extern void set_orbitalZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600036E RID: 878
			[MethodImpl(4096)]
			private static extern void get_orbitalOffsetX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600036F RID: 879
			[MethodImpl(4096)]
			private static extern void set_orbitalOffsetX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000370 RID: 880
			[MethodImpl(4096)]
			private static extern void get_orbitalOffsetY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000371 RID: 881
			[MethodImpl(4096)]
			private static extern void set_orbitalOffsetY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000372 RID: 882
			[MethodImpl(4096)]
			private static extern void get_orbitalOffsetZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000373 RID: 883
			[MethodImpl(4096)]
			private static extern void set_orbitalOffsetZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000374 RID: 884
			[MethodImpl(4096)]
			private static extern float get_orbitalOffsetXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000375 RID: 885
			[MethodImpl(4096)]
			private static extern void set_orbitalOffsetXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000376 RID: 886
			[MethodImpl(4096)]
			private static extern float get_orbitalOffsetYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000377 RID: 887
			[MethodImpl(4096)]
			private static extern void set_orbitalOffsetYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000378 RID: 888
			[MethodImpl(4096)]
			private static extern float get_orbitalOffsetZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000379 RID: 889
			[MethodImpl(4096)]
			private static extern void set_orbitalOffsetZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600037A RID: 890
			[MethodImpl(4096)]
			private static extern void get_radial_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600037B RID: 891
			[MethodImpl(4096)]
			private static extern void set_radial_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600037C RID: 892
			[MethodImpl(4096)]
			private static extern float get_radialMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x0600037D RID: 893
			[MethodImpl(4096)]
			private static extern void set_radialMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600037E RID: 894
			[MethodImpl(4096)]
			private static extern void get_speedModifier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600037F RID: 895
			[MethodImpl(4096)]
			private static extern void set_speedModifier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000380 RID: 896
			[MethodImpl(4096)]
			private static extern float get_speedModifierMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000381 RID: 897
			[MethodImpl(4096)]
			private static extern void set_speedModifierMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000382 RID: 898
			[MethodImpl(4096)]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000383 RID: 899
			[MethodImpl(4096)]
			private static extern void set_space_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			// Token: 0x04000066 RID: 102
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200001C RID: 28
		public struct LimitVelocityOverLifetimeModule
		{
			// Token: 0x06000384 RID: 900 RVA: 0x000048D6 File Offset: 0x00002AD6
			internal LimitVelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000385 RID: 901 RVA: 0x000048E0 File Offset: 0x00002AE0
			// (set) Token: 0x06000386 RID: 902 RVA: 0x000048E8 File Offset: 0x00002AE8
			public bool enabled
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000387 RID: 903 RVA: 0x000048F4 File Offset: 0x00002AF4
			// (set) Token: 0x06000388 RID: 904 RVA: 0x0000490A File Offset: 0x00002B0A
			public ParticleSystem.MinMaxCurve limitX
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitX_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitX_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000389 RID: 905 RVA: 0x00004914 File Offset: 0x00002B14
			// (set) Token: 0x0600038A RID: 906 RVA: 0x0000491C File Offset: 0x00002B1C
			public float limitXMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x0600038B RID: 907 RVA: 0x00004928 File Offset: 0x00002B28
			// (set) Token: 0x0600038C RID: 908 RVA: 0x0000493E File Offset: 0x00002B3E
			public ParticleSystem.MinMaxCurve limitY
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitY_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitY_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x0600038D RID: 909 RVA: 0x00004948 File Offset: 0x00002B48
			// (set) Token: 0x0600038E RID: 910 RVA: 0x00004950 File Offset: 0x00002B50
			public float limitYMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x0600038F RID: 911 RVA: 0x0000495C File Offset: 0x00002B5C
			// (set) Token: 0x06000390 RID: 912 RVA: 0x00004972 File Offset: 0x00002B72
			public ParticleSystem.MinMaxCurve limitZ
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitZ_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000391 RID: 913 RVA: 0x0000497C File Offset: 0x00002B7C
			// (set) Token: 0x06000392 RID: 914 RVA: 0x00004984 File Offset: 0x00002B84
			public float limitZMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000393 RID: 915 RVA: 0x00004990 File Offset: 0x00002B90
			// (set) Token: 0x06000394 RID: 916 RVA: 0x000049A6 File Offset: 0x00002BA6
			[NativeName("Magnitude")]
			public ParticleSystem.MinMaxCurve limit
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limit_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limit_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000395 RID: 917 RVA: 0x000049B0 File Offset: 0x00002BB0
			// (set) Token: 0x06000396 RID: 918 RVA: 0x000049B8 File Offset: 0x00002BB8
			[NativeName("MagnitudeMultiplier")]
			public float limitMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000397 RID: 919 RVA: 0x000049C1 File Offset: 0x00002BC1
			// (set) Token: 0x06000398 RID: 920 RVA: 0x000049C9 File Offset: 0x00002BC9
			public float dampen
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_dampen_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_dampen_Injected(ref this, value);
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000399 RID: 921 RVA: 0x000049D2 File Offset: 0x00002BD2
			// (set) Token: 0x0600039A RID: 922 RVA: 0x000049DA File Offset: 0x00002BDA
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600039B RID: 923 RVA: 0x000049E3 File Offset: 0x00002BE3
			// (set) Token: 0x0600039C RID: 924 RVA: 0x000049EB File Offset: 0x00002BEB
			public ParticleSystemSimulationSpace space
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_space_Injected(ref this, value);
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x0600039D RID: 925 RVA: 0x000049F4 File Offset: 0x00002BF4
			// (set) Token: 0x0600039E RID: 926 RVA: 0x00004A0A File Offset: 0x00002C0A
			public ParticleSystem.MinMaxCurve drag
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_drag_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_drag_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600039F RID: 927 RVA: 0x00004A14 File Offset: 0x00002C14
			// (set) Token: 0x060003A0 RID: 928 RVA: 0x00004A1C File Offset: 0x00002C1C
			public float dragMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_dragMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_dragMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060003A1 RID: 929 RVA: 0x00004A25 File Offset: 0x00002C25
			// (set) Token: 0x060003A2 RID: 930 RVA: 0x00004A2D File Offset: 0x00002C2D
			public bool multiplyDragByParticleSize
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_multiplyDragByParticleSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_multiplyDragByParticleSize_Injected(ref this, value);
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x060003A3 RID: 931 RVA: 0x00004A36 File Offset: 0x00002C36
			// (set) Token: 0x060003A4 RID: 932 RVA: 0x00004A3E File Offset: 0x00002C3E
			public bool multiplyDragByParticleVelocity
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_multiplyDragByParticleVelocity_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_multiplyDragByParticleVelocity_Injected(ref this, value);
				}
			}

			// Token: 0x060003A5 RID: 933
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003A6 RID: 934
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x060003A7 RID: 935
			[MethodImpl(4096)]
			private static extern void get_limitX_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060003A8 RID: 936
			[MethodImpl(4096)]
			private static extern void set_limitX_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060003A9 RID: 937
			[MethodImpl(4096)]
			private static extern float get_limitXMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003AA RID: 938
			[MethodImpl(4096)]
			private static extern void set_limitXMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x060003AB RID: 939
			[MethodImpl(4096)]
			private static extern void get_limitY_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060003AC RID: 940
			[MethodImpl(4096)]
			private static extern void set_limitY_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060003AD RID: 941
			[MethodImpl(4096)]
			private static extern float get_limitYMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003AE RID: 942
			[MethodImpl(4096)]
			private static extern void set_limitYMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x060003AF RID: 943
			[MethodImpl(4096)]
			private static extern void get_limitZ_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060003B0 RID: 944
			[MethodImpl(4096)]
			private static extern void set_limitZ_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060003B1 RID: 945
			[MethodImpl(4096)]
			private static extern float get_limitZMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003B2 RID: 946
			[MethodImpl(4096)]
			private static extern void set_limitZMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x060003B3 RID: 947
			[MethodImpl(4096)]
			private static extern void get_limit_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060003B4 RID: 948
			[MethodImpl(4096)]
			private static extern void set_limit_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060003B5 RID: 949
			[MethodImpl(4096)]
			private static extern float get_limitMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003B6 RID: 950
			[MethodImpl(4096)]
			private static extern void set_limitMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x060003B7 RID: 951
			[MethodImpl(4096)]
			private static extern float get_dampen_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003B8 RID: 952
			[MethodImpl(4096)]
			private static extern void set_dampen_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x060003B9 RID: 953
			[MethodImpl(4096)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003BA RID: 954
			[MethodImpl(4096)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x060003BB RID: 955
			[MethodImpl(4096)]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003BC RID: 956
			[MethodImpl(4096)]
			private static extern void set_space_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			// Token: 0x060003BD RID: 957
			[MethodImpl(4096)]
			private static extern void get_drag_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060003BE RID: 958
			[MethodImpl(4096)]
			private static extern void set_drag_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060003BF RID: 959
			[MethodImpl(4096)]
			private static extern float get_dragMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003C0 RID: 960
			[MethodImpl(4096)]
			private static extern void set_dragMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x060003C1 RID: 961
			[MethodImpl(4096)]
			private static extern bool get_multiplyDragByParticleSize_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003C2 RID: 962
			[MethodImpl(4096)]
			private static extern void set_multiplyDragByParticleSize_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x060003C3 RID: 963
			[MethodImpl(4096)]
			private static extern bool get_multiplyDragByParticleVelocity_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x060003C4 RID: 964
			[MethodImpl(4096)]
			private static extern void set_multiplyDragByParticleVelocity_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x04000067 RID: 103
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200001D RID: 29
		public struct InheritVelocityModule
		{
			// Token: 0x060003C5 RID: 965 RVA: 0x00004A47 File Offset: 0x00002C47
			internal InheritVelocityModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x060003C6 RID: 966 RVA: 0x00004A51 File Offset: 0x00002C51
			// (set) Token: 0x060003C7 RID: 967 RVA: 0x00004A59 File Offset: 0x00002C59
			public bool enabled
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x00004A62 File Offset: 0x00002C62
			// (set) Token: 0x060003C9 RID: 969 RVA: 0x00004A6A File Offset: 0x00002C6A
			public ParticleSystemInheritVelocityMode mode
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_mode_Injected(ref this, value);
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x060003CA RID: 970 RVA: 0x00004A74 File Offset: 0x00002C74
			// (set) Token: 0x060003CB RID: 971 RVA: 0x00004A8A File Offset: 0x00002C8A
			public ParticleSystem.MinMaxCurve curve
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.InheritVelocityModule.get_curve_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_curve_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060003CC RID: 972 RVA: 0x00004A94 File Offset: 0x00002C94
			// (set) Token: 0x060003CD RID: 973 RVA: 0x00004A9C File Offset: 0x00002C9C
			public float curveMultiplier
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.get_curveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_curveMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x060003CE RID: 974
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.InheritVelocityModule _unity_self);

			// Token: 0x060003CF RID: 975
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, bool value);

			// Token: 0x060003D0 RID: 976
			[MethodImpl(4096)]
			private static extern ParticleSystemInheritVelocityMode get_mode_Injected(ref ParticleSystem.InheritVelocityModule _unity_self);

			// Token: 0x060003D1 RID: 977
			[MethodImpl(4096)]
			private static extern void set_mode_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, ParticleSystemInheritVelocityMode value);

			// Token: 0x060003D2 RID: 978
			[MethodImpl(4096)]
			private static extern void get_curve_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060003D3 RID: 979
			[MethodImpl(4096)]
			private static extern void set_curve_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060003D4 RID: 980
			[MethodImpl(4096)]
			private static extern float get_curveMultiplier_Injected(ref ParticleSystem.InheritVelocityModule _unity_self);

			// Token: 0x060003D5 RID: 981
			[MethodImpl(4096)]
			private static extern void set_curveMultiplier_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, float value);

			// Token: 0x04000068 RID: 104
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200001E RID: 30
		public struct LifetimeByEmitterSpeedModule
		{
			// Token: 0x060003D6 RID: 982 RVA: 0x00004AA5 File Offset: 0x00002CA5
			internal LifetimeByEmitterSpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060003D7 RID: 983 RVA: 0x00004AAF File Offset: 0x00002CAF
			// (set) Token: 0x060003D8 RID: 984 RVA: 0x00004AB7 File Offset: 0x00002CB7
			public bool enabled
			{
				get
				{
					return ParticleSystem.LifetimeByEmitterSpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x060003D9 RID: 985 RVA: 0x00004AC0 File Offset: 0x00002CC0
			// (set) Token: 0x060003DA RID: 986 RVA: 0x00004AD6 File Offset: 0x00002CD6
			public ParticleSystem.MinMaxCurve curve
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.LifetimeByEmitterSpeedModule.get_curve_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_curve_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060003DB RID: 987 RVA: 0x00004AE0 File Offset: 0x00002CE0
			// (set) Token: 0x060003DC RID: 988 RVA: 0x00004AE8 File Offset: 0x00002CE8
			public float curveMultiplier
			{
				get
				{
					return ParticleSystem.LifetimeByEmitterSpeedModule.get_curveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_curveMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060003DD RID: 989 RVA: 0x00004AF4 File Offset: 0x00002CF4
			// (set) Token: 0x060003DE RID: 990 RVA: 0x00004B0A File Offset: 0x00002D0A
			public Vector2 range
			{
				get
				{
					Vector2 vector;
					ParticleSystem.LifetimeByEmitterSpeedModule.get_range_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x060003DF RID: 991
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self);

			// Token: 0x060003E0 RID: 992
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, bool value);

			// Token: 0x060003E1 RID: 993
			[MethodImpl(4096)]
			private static extern void get_curve_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060003E2 RID: 994
			[MethodImpl(4096)]
			private static extern void set_curve_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060003E3 RID: 995
			[MethodImpl(4096)]
			private static extern float get_curveMultiplier_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self);

			// Token: 0x060003E4 RID: 996
			[MethodImpl(4096)]
			private static extern void set_curveMultiplier_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, float value);

			// Token: 0x060003E5 RID: 997
			[MethodImpl(4096)]
			private static extern void get_range_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, out Vector2 ret);

			// Token: 0x060003E6 RID: 998
			[MethodImpl(4096)]
			private static extern void set_range_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, ref Vector2 value);

			// Token: 0x04000069 RID: 105
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200001F RID: 31
		public struct ForceOverLifetimeModule
		{
			// Token: 0x060003E7 RID: 999 RVA: 0x00004B14 File Offset: 0x00002D14
			internal ForceOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00004B1E File Offset: 0x00002D1E
			// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00004B26 File Offset: 0x00002D26
			public bool enabled
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060003EA RID: 1002 RVA: 0x00004B30 File Offset: 0x00002D30
			// (set) Token: 0x060003EB RID: 1003 RVA: 0x00004B46 File Offset: 0x00002D46
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.ForceOverLifetimeModule.get_x_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060003EC RID: 1004 RVA: 0x00004B50 File Offset: 0x00002D50
			// (set) Token: 0x060003ED RID: 1005 RVA: 0x00004B66 File Offset: 0x00002D66
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.ForceOverLifetimeModule.get_y_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x00004B70 File Offset: 0x00002D70
			// (set) Token: 0x060003EF RID: 1007 RVA: 0x00004B86 File Offset: 0x00002D86
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.ForceOverLifetimeModule.get_z_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00004B90 File Offset: 0x00002D90
			// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00004B98 File Offset: 0x00002D98
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00004BA1 File Offset: 0x00002DA1
			// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00004BA9 File Offset: 0x00002DA9
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00004BB2 File Offset: 0x00002DB2
			// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00004BBA File Offset: 0x00002DBA
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00004BC3 File Offset: 0x00002DC3
			// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00004BCB File Offset: 0x00002DCB
			public ParticleSystemSimulationSpace space
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_space_Injected(ref this, value);
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00004BD4 File Offset: 0x00002DD4
			// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00004BDC File Offset: 0x00002DDC
			public bool randomized
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_randomized_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_randomized_Injected(ref this, value);
				}
			}

			// Token: 0x060003FA RID: 1018
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x060003FB RID: 1019
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, bool value);

			// Token: 0x060003FC RID: 1020
			[MethodImpl(4096)]
			private static extern void get_x_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060003FD RID: 1021
			[MethodImpl(4096)]
			private static extern void set_x_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060003FE RID: 1022
			[MethodImpl(4096)]
			private static extern void get_y_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060003FF RID: 1023
			[MethodImpl(4096)]
			private static extern void set_y_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000400 RID: 1024
			[MethodImpl(4096)]
			private static extern void get_z_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000401 RID: 1025
			[MethodImpl(4096)]
			private static extern void set_z_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000402 RID: 1026
			[MethodImpl(4096)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x06000403 RID: 1027
			[MethodImpl(4096)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, float value);

			// Token: 0x06000404 RID: 1028
			[MethodImpl(4096)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x06000405 RID: 1029
			[MethodImpl(4096)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, float value);

			// Token: 0x06000406 RID: 1030
			[MethodImpl(4096)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x06000407 RID: 1031
			[MethodImpl(4096)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, float value);

			// Token: 0x06000408 RID: 1032
			[MethodImpl(4096)]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x06000409 RID: 1033
			[MethodImpl(4096)]
			private static extern void set_space_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			// Token: 0x0600040A RID: 1034
			[MethodImpl(4096)]
			private static extern bool get_randomized_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x0600040B RID: 1035
			[MethodImpl(4096)]
			private static extern void set_randomized_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, bool value);

			// Token: 0x0400006A RID: 106
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000020 RID: 32
		public struct ColorOverLifetimeModule
		{
			// Token: 0x0600040C RID: 1036 RVA: 0x00004BE5 File Offset: 0x00002DE5
			internal ColorOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x0600040D RID: 1037 RVA: 0x00004BEF File Offset: 0x00002DEF
			// (set) Token: 0x0600040E RID: 1038 RVA: 0x00004BF7 File Offset: 0x00002DF7
			public bool enabled
			{
				get
				{
					return ParticleSystem.ColorOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x0600040F RID: 1039 RVA: 0x00004C00 File Offset: 0x00002E00
			// (set) Token: 0x06000410 RID: 1040 RVA: 0x00004C16 File Offset: 0x00002E16
			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					ParticleSystem.MinMaxGradient minMaxGradient;
					ParticleSystem.ColorOverLifetimeModule.get_color_Injected(ref this, out minMaxGradient);
					return minMaxGradient;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorOverLifetimeModule.set_color_Injected(ref this, ref value);
				}
			}

			// Token: 0x06000411 RID: 1041
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self);

			// Token: 0x06000412 RID: 1042
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, bool value);

			// Token: 0x06000413 RID: 1043
			[MethodImpl(4096)]
			private static extern void get_color_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x06000414 RID: 1044
			[MethodImpl(4096)]
			private static extern void set_color_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x0400006B RID: 107
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000021 RID: 33
		public struct ColorBySpeedModule
		{
			// Token: 0x06000415 RID: 1045 RVA: 0x00004C20 File Offset: 0x00002E20
			internal ColorBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000416 RID: 1046 RVA: 0x00004C2A File Offset: 0x00002E2A
			// (set) Token: 0x06000417 RID: 1047 RVA: 0x00004C32 File Offset: 0x00002E32
			public bool enabled
			{
				get
				{
					return ParticleSystem.ColorBySpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000418 RID: 1048 RVA: 0x00004C3C File Offset: 0x00002E3C
			// (set) Token: 0x06000419 RID: 1049 RVA: 0x00004C52 File Offset: 0x00002E52
			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					ParticleSystem.MinMaxGradient minMaxGradient;
					ParticleSystem.ColorBySpeedModule.get_color_Injected(ref this, out minMaxGradient);
					return minMaxGradient;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_color_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x0600041A RID: 1050 RVA: 0x00004C5C File Offset: 0x00002E5C
			// (set) Token: 0x0600041B RID: 1051 RVA: 0x00004C72 File Offset: 0x00002E72
			public Vector2 range
			{
				get
				{
					Vector2 vector;
					ParticleSystem.ColorBySpeedModule.get_range_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x0600041C RID: 1052
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self);

			// Token: 0x0600041D RID: 1053
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, bool value);

			// Token: 0x0600041E RID: 1054
			[MethodImpl(4096)]
			private static extern void get_color_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x0600041F RID: 1055
			[MethodImpl(4096)]
			private static extern void set_color_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x06000420 RID: 1056
			[MethodImpl(4096)]
			private static extern void get_range_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, out Vector2 ret);

			// Token: 0x06000421 RID: 1057
			[MethodImpl(4096)]
			private static extern void set_range_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, ref Vector2 value);

			// Token: 0x0400006C RID: 108
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000022 RID: 34
		public struct SizeOverLifetimeModule
		{
			// Token: 0x06000422 RID: 1058 RVA: 0x00004C7C File Offset: 0x00002E7C
			internal SizeOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x06000423 RID: 1059 RVA: 0x00004C86 File Offset: 0x00002E86
			// (set) Token: 0x06000424 RID: 1060 RVA: 0x00004C8E File Offset: 0x00002E8E
			public bool enabled
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000425 RID: 1061 RVA: 0x00004C98 File Offset: 0x00002E98
			// (set) Token: 0x06000426 RID: 1062 RVA: 0x00004CAE File Offset: 0x00002EAE
			[NativeName("X")]
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.SizeOverLifetimeModule.get_size_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_size_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x00004CB8 File Offset: 0x00002EB8
			// (set) Token: 0x06000428 RID: 1064 RVA: 0x00004CC0 File Offset: 0x00002EC0
			[NativeName("XMultiplier")]
			public float sizeMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_sizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_sizeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000429 RID: 1065 RVA: 0x00004CCC File Offset: 0x00002ECC
			// (set) Token: 0x0600042A RID: 1066 RVA: 0x00004CE2 File Offset: 0x00002EE2
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.SizeOverLifetimeModule.get_x_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x00004CEC File Offset: 0x00002EEC
			// (set) Token: 0x0600042C RID: 1068 RVA: 0x00004CF4 File Offset: 0x00002EF4
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x0600042D RID: 1069 RVA: 0x00004D00 File Offset: 0x00002F00
			// (set) Token: 0x0600042E RID: 1070 RVA: 0x00004D16 File Offset: 0x00002F16
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.SizeOverLifetimeModule.get_y_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x0600042F RID: 1071 RVA: 0x00004D20 File Offset: 0x00002F20
			// (set) Token: 0x06000430 RID: 1072 RVA: 0x00004D28 File Offset: 0x00002F28
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x00004D34 File Offset: 0x00002F34
			// (set) Token: 0x06000432 RID: 1074 RVA: 0x00004D4A File Offset: 0x00002F4A
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.SizeOverLifetimeModule.get_z_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x00004D54 File Offset: 0x00002F54
			// (set) Token: 0x06000434 RID: 1076 RVA: 0x00004D5C File Offset: 0x00002F5C
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x00004D65 File Offset: 0x00002F65
			// (set) Token: 0x06000436 RID: 1078 RVA: 0x00004D6D File Offset: 0x00002F6D
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x06000437 RID: 1079
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x06000438 RID: 1080
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, bool value);

			// Token: 0x06000439 RID: 1081
			[MethodImpl(4096)]
			private static extern void get_size_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600043A RID: 1082
			[MethodImpl(4096)]
			private static extern void set_size_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600043B RID: 1083
			[MethodImpl(4096)]
			private static extern float get_sizeMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x0600043C RID: 1084
			[MethodImpl(4096)]
			private static extern void set_sizeMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			// Token: 0x0600043D RID: 1085
			[MethodImpl(4096)]
			private static extern void get_x_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600043E RID: 1086
			[MethodImpl(4096)]
			private static extern void set_x_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600043F RID: 1087
			[MethodImpl(4096)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x06000440 RID: 1088
			[MethodImpl(4096)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			// Token: 0x06000441 RID: 1089
			[MethodImpl(4096)]
			private static extern void get_y_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000442 RID: 1090
			[MethodImpl(4096)]
			private static extern void set_y_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000443 RID: 1091
			[MethodImpl(4096)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x06000444 RID: 1092
			[MethodImpl(4096)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			// Token: 0x06000445 RID: 1093
			[MethodImpl(4096)]
			private static extern void get_z_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000446 RID: 1094
			[MethodImpl(4096)]
			private static extern void set_z_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000447 RID: 1095
			[MethodImpl(4096)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x06000448 RID: 1096
			[MethodImpl(4096)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			// Token: 0x06000449 RID: 1097
			[MethodImpl(4096)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x0600044A RID: 1098
			[MethodImpl(4096)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, bool value);

			// Token: 0x0400006D RID: 109
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000023 RID: 35
		public struct SizeBySpeedModule
		{
			// Token: 0x0600044B RID: 1099 RVA: 0x00004D76 File Offset: 0x00002F76
			internal SizeBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600044C RID: 1100 RVA: 0x00004D80 File Offset: 0x00002F80
			// (set) Token: 0x0600044D RID: 1101 RVA: 0x00004D88 File Offset: 0x00002F88
			public bool enabled
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600044E RID: 1102 RVA: 0x00004D94 File Offset: 0x00002F94
			// (set) Token: 0x0600044F RID: 1103 RVA: 0x00004DAA File Offset: 0x00002FAA
			[NativeName("X")]
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.SizeBySpeedModule.get_size_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_size_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06000450 RID: 1104 RVA: 0x00004DB4 File Offset: 0x00002FB4
			// (set) Token: 0x06000451 RID: 1105 RVA: 0x00004DBC File Offset: 0x00002FBC
			[NativeName("XMultiplier")]
			public float sizeMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_sizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_sizeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000452 RID: 1106 RVA: 0x00004DC8 File Offset: 0x00002FC8
			// (set) Token: 0x06000453 RID: 1107 RVA: 0x00004DDE File Offset: 0x00002FDE
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.SizeBySpeedModule.get_x_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000454 RID: 1108 RVA: 0x00004DE8 File Offset: 0x00002FE8
			// (set) Token: 0x06000455 RID: 1109 RVA: 0x00004DF0 File Offset: 0x00002FF0
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000456 RID: 1110 RVA: 0x00004DFC File Offset: 0x00002FFC
			// (set) Token: 0x06000457 RID: 1111 RVA: 0x00004E12 File Offset: 0x00003012
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.SizeBySpeedModule.get_y_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000458 RID: 1112 RVA: 0x00004E1C File Offset: 0x0000301C
			// (set) Token: 0x06000459 RID: 1113 RVA: 0x00004E24 File Offset: 0x00003024
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x0600045A RID: 1114 RVA: 0x00004E30 File Offset: 0x00003030
			// (set) Token: 0x0600045B RID: 1115 RVA: 0x00004E46 File Offset: 0x00003046
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.SizeBySpeedModule.get_z_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x0600045C RID: 1116 RVA: 0x00004E50 File Offset: 0x00003050
			// (set) Token: 0x0600045D RID: 1117 RVA: 0x00004E58 File Offset: 0x00003058
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x0600045E RID: 1118 RVA: 0x00004E61 File Offset: 0x00003061
			// (set) Token: 0x0600045F RID: 1119 RVA: 0x00004E69 File Offset: 0x00003069
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000460 RID: 1120 RVA: 0x00004E74 File Offset: 0x00003074
			// (set) Token: 0x06000461 RID: 1121 RVA: 0x00004E8A File Offset: 0x0000308A
			public Vector2 range
			{
				get
				{
					Vector2 vector;
					ParticleSystem.SizeBySpeedModule.get_range_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x06000462 RID: 1122
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x06000463 RID: 1123
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, bool value);

			// Token: 0x06000464 RID: 1124
			[MethodImpl(4096)]
			private static extern void get_size_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000465 RID: 1125
			[MethodImpl(4096)]
			private static extern void set_size_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000466 RID: 1126
			[MethodImpl(4096)]
			private static extern float get_sizeMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x06000467 RID: 1127
			[MethodImpl(4096)]
			private static extern void set_sizeMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			// Token: 0x06000468 RID: 1128
			[MethodImpl(4096)]
			private static extern void get_x_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000469 RID: 1129
			[MethodImpl(4096)]
			private static extern void set_x_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600046A RID: 1130
			[MethodImpl(4096)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x0600046B RID: 1131
			[MethodImpl(4096)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			// Token: 0x0600046C RID: 1132
			[MethodImpl(4096)]
			private static extern void get_y_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600046D RID: 1133
			[MethodImpl(4096)]
			private static extern void set_y_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600046E RID: 1134
			[MethodImpl(4096)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x0600046F RID: 1135
			[MethodImpl(4096)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			// Token: 0x06000470 RID: 1136
			[MethodImpl(4096)]
			private static extern void get_z_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000471 RID: 1137
			[MethodImpl(4096)]
			private static extern void set_z_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000472 RID: 1138
			[MethodImpl(4096)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x06000473 RID: 1139
			[MethodImpl(4096)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			// Token: 0x06000474 RID: 1140
			[MethodImpl(4096)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x06000475 RID: 1141
			[MethodImpl(4096)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, bool value);

			// Token: 0x06000476 RID: 1142
			[MethodImpl(4096)]
			private static extern void get_range_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out Vector2 ret);

			// Token: 0x06000477 RID: 1143
			[MethodImpl(4096)]
			private static extern void set_range_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref Vector2 value);

			// Token: 0x0400006E RID: 110
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000024 RID: 36
		public struct RotationOverLifetimeModule
		{
			// Token: 0x06000478 RID: 1144 RVA: 0x00004E94 File Offset: 0x00003094
			internal RotationOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000479 RID: 1145 RVA: 0x00004E9E File Offset: 0x0000309E
			// (set) Token: 0x0600047A RID: 1146 RVA: 0x00004EA6 File Offset: 0x000030A6
			public bool enabled
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x0600047B RID: 1147 RVA: 0x00004EB0 File Offset: 0x000030B0
			// (set) Token: 0x0600047C RID: 1148 RVA: 0x00004EC6 File Offset: 0x000030C6
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.RotationOverLifetimeModule.get_x_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x0600047D RID: 1149 RVA: 0x00004ED0 File Offset: 0x000030D0
			// (set) Token: 0x0600047E RID: 1150 RVA: 0x00004ED8 File Offset: 0x000030D8
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x00004EE4 File Offset: 0x000030E4
			// (set) Token: 0x06000480 RID: 1152 RVA: 0x00004EFA File Offset: 0x000030FA
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.RotationOverLifetimeModule.get_y_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000481 RID: 1153 RVA: 0x00004F04 File Offset: 0x00003104
			// (set) Token: 0x06000482 RID: 1154 RVA: 0x00004F0C File Offset: 0x0000310C
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000483 RID: 1155 RVA: 0x00004F18 File Offset: 0x00003118
			// (set) Token: 0x06000484 RID: 1156 RVA: 0x00004F2E File Offset: 0x0000312E
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.RotationOverLifetimeModule.get_z_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000485 RID: 1157 RVA: 0x00004F38 File Offset: 0x00003138
			// (set) Token: 0x06000486 RID: 1158 RVA: 0x00004F40 File Offset: 0x00003140
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000487 RID: 1159 RVA: 0x00004F49 File Offset: 0x00003149
			// (set) Token: 0x06000488 RID: 1160 RVA: 0x00004F51 File Offset: 0x00003151
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x06000489 RID: 1161
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x0600048A RID: 1162
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, bool value);

			// Token: 0x0600048B RID: 1163
			[MethodImpl(4096)]
			private static extern void get_x_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600048C RID: 1164
			[MethodImpl(4096)]
			private static extern void set_x_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600048D RID: 1165
			[MethodImpl(4096)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x0600048E RID: 1166
			[MethodImpl(4096)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, float value);

			// Token: 0x0600048F RID: 1167
			[MethodImpl(4096)]
			private static extern void get_y_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000490 RID: 1168
			[MethodImpl(4096)]
			private static extern void set_y_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000491 RID: 1169
			[MethodImpl(4096)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x06000492 RID: 1170
			[MethodImpl(4096)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, float value);

			// Token: 0x06000493 RID: 1171
			[MethodImpl(4096)]
			private static extern void get_z_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000494 RID: 1172
			[MethodImpl(4096)]
			private static extern void set_z_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000495 RID: 1173
			[MethodImpl(4096)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x06000496 RID: 1174
			[MethodImpl(4096)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, float value);

			// Token: 0x06000497 RID: 1175
			[MethodImpl(4096)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x06000498 RID: 1176
			[MethodImpl(4096)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, bool value);

			// Token: 0x0400006F RID: 111
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000025 RID: 37
		public struct RotationBySpeedModule
		{
			// Token: 0x06000499 RID: 1177 RVA: 0x00004F5A File Offset: 0x0000315A
			internal RotationBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x0600049A RID: 1178 RVA: 0x00004F64 File Offset: 0x00003164
			// (set) Token: 0x0600049B RID: 1179 RVA: 0x00004F6C File Offset: 0x0000316C
			public bool enabled
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x0600049C RID: 1180 RVA: 0x00004F78 File Offset: 0x00003178
			// (set) Token: 0x0600049D RID: 1181 RVA: 0x00004F8E File Offset: 0x0000318E
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.RotationBySpeedModule.get_x_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x00004F98 File Offset: 0x00003198
			// (set) Token: 0x0600049F RID: 1183 RVA: 0x00004FA0 File Offset: 0x000031A0
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00004FAC File Offset: 0x000031AC
			// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00004FC2 File Offset: 0x000031C2
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.RotationBySpeedModule.get_y_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00004FCC File Offset: 0x000031CC
			// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00004FD4 File Offset: 0x000031D4
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00004FE0 File Offset: 0x000031E0
			// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00004FF6 File Offset: 0x000031F6
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.RotationBySpeedModule.get_z_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00005000 File Offset: 0x00003200
			// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00005008 File Offset: 0x00003208
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00005011 File Offset: 0x00003211
			// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00005019 File Offset: 0x00003219
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060004AA RID: 1194 RVA: 0x00005024 File Offset: 0x00003224
			// (set) Token: 0x060004AB RID: 1195 RVA: 0x0000503A File Offset: 0x0000323A
			public Vector2 range
			{
				get
				{
					Vector2 vector;
					ParticleSystem.RotationBySpeedModule.get_range_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x060004AC RID: 1196
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x060004AD RID: 1197
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, bool value);

			// Token: 0x060004AE RID: 1198
			[MethodImpl(4096)]
			private static extern void get_x_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004AF RID: 1199
			[MethodImpl(4096)]
			private static extern void set_x_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004B0 RID: 1200
			[MethodImpl(4096)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x060004B1 RID: 1201
			[MethodImpl(4096)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, float value);

			// Token: 0x060004B2 RID: 1202
			[MethodImpl(4096)]
			private static extern void get_y_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004B3 RID: 1203
			[MethodImpl(4096)]
			private static extern void set_y_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004B4 RID: 1204
			[MethodImpl(4096)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x060004B5 RID: 1205
			[MethodImpl(4096)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, float value);

			// Token: 0x060004B6 RID: 1206
			[MethodImpl(4096)]
			private static extern void get_z_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004B7 RID: 1207
			[MethodImpl(4096)]
			private static extern void set_z_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004B8 RID: 1208
			[MethodImpl(4096)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x060004B9 RID: 1209
			[MethodImpl(4096)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, float value);

			// Token: 0x060004BA RID: 1210
			[MethodImpl(4096)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x060004BB RID: 1211
			[MethodImpl(4096)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, bool value);

			// Token: 0x060004BC RID: 1212
			[MethodImpl(4096)]
			private static extern void get_range_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out Vector2 ret);

			// Token: 0x060004BD RID: 1213
			[MethodImpl(4096)]
			private static extern void set_range_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref Vector2 value);

			// Token: 0x04000070 RID: 112
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000026 RID: 38
		public struct ExternalForcesModule
		{
			// Token: 0x060004BE RID: 1214 RVA: 0x00005044 File Offset: 0x00003244
			internal ExternalForcesModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000504E File Offset: 0x0000324E
			// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00005056 File Offset: 0x00003256
			public bool enabled
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000505F File Offset: 0x0000325F
			// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00005067 File Offset: 0x00003267
			public float multiplier
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_multiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_multiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00005070 File Offset: 0x00003270
			// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00005086 File Offset: 0x00003286
			public ParticleSystem.MinMaxCurve multiplierCurve
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.ExternalForcesModule.get_multiplierCurve_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_multiplierCurve_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00005090 File Offset: 0x00003290
			// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00005098 File Offset: 0x00003298
			public ParticleSystemGameObjectFilter influenceFilter
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_influenceFilter_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_influenceFilter_Injected(ref this, value);
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x060004C7 RID: 1223 RVA: 0x000050A4 File Offset: 0x000032A4
			// (set) Token: 0x060004C8 RID: 1224 RVA: 0x000050BA File Offset: 0x000032BA
			public LayerMask influenceMask
			{
				get
				{
					LayerMask layerMask;
					ParticleSystem.ExternalForcesModule.get_influenceMask_Injected(ref this, out layerMask);
					return layerMask;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_influenceMask_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000050C4 File Offset: 0x000032C4
			public int influenceCount
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_influenceCount_Injected(ref this);
				}
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x000050CC File Offset: 0x000032CC
			public bool IsAffectedBy(ParticleSystemForceField field)
			{
				return ParticleSystem.ExternalForcesModule.IsAffectedBy_Injected(ref this, field);
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x000050D5 File Offset: 0x000032D5
			[NativeThrows]
			public void AddInfluence([NotNull] ParticleSystemForceField field)
			{
				ParticleSystem.ExternalForcesModule.AddInfluence_Injected(ref this, field);
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x000050DE File Offset: 0x000032DE
			[NativeThrows]
			private void RemoveInfluenceAtIndex(int index)
			{
				ParticleSystem.ExternalForcesModule.RemoveInfluenceAtIndex_Injected(ref this, index);
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x000050E7 File Offset: 0x000032E7
			public void RemoveInfluence(int index)
			{
				this.RemoveInfluenceAtIndex(index);
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x000050F2 File Offset: 0x000032F2
			[NativeThrows]
			public void RemoveInfluence([NotNull] ParticleSystemForceField field)
			{
				ParticleSystem.ExternalForcesModule.RemoveInfluence_Injected(ref this, field);
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x000050FB File Offset: 0x000032FB
			public void RemoveAllInfluences()
			{
				ParticleSystem.ExternalForcesModule.RemoveAllInfluences_Injected(ref this);
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x00005103 File Offset: 0x00003303
			[NativeThrows]
			public void SetInfluence(int index, [NotNull] ParticleSystemForceField field)
			{
				ParticleSystem.ExternalForcesModule.SetInfluence_Injected(ref this, index, field);
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x0000510D File Offset: 0x0000330D
			[NativeThrows]
			public ParticleSystemForceField GetInfluence(int index)
			{
				return ParticleSystem.ExternalForcesModule.GetInfluence_Injected(ref this, index);
			}

			// Token: 0x060004D2 RID: 1234
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x060004D3 RID: 1235
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, bool value);

			// Token: 0x060004D4 RID: 1236
			[MethodImpl(4096)]
			private static extern float get_multiplier_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x060004D5 RID: 1237
			[MethodImpl(4096)]
			private static extern void set_multiplier_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, float value);

			// Token: 0x060004D6 RID: 1238
			[MethodImpl(4096)]
			private static extern void get_multiplierCurve_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004D7 RID: 1239
			[MethodImpl(4096)]
			private static extern void set_multiplierCurve_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004D8 RID: 1240
			[MethodImpl(4096)]
			private static extern ParticleSystemGameObjectFilter get_influenceFilter_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x060004D9 RID: 1241
			[MethodImpl(4096)]
			private static extern void set_influenceFilter_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemGameObjectFilter value);

			// Token: 0x060004DA RID: 1242
			[MethodImpl(4096)]
			private static extern void get_influenceMask_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, out LayerMask ret);

			// Token: 0x060004DB RID: 1243
			[MethodImpl(4096)]
			private static extern void set_influenceMask_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ref LayerMask value);

			// Token: 0x060004DC RID: 1244
			[MethodImpl(4096)]
			private static extern int get_influenceCount_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x060004DD RID: 1245
			[MethodImpl(4096)]
			private static extern bool IsAffectedBy_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemForceField field);

			// Token: 0x060004DE RID: 1246
			[MethodImpl(4096)]
			private static extern void AddInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemForceField field);

			// Token: 0x060004DF RID: 1247
			[MethodImpl(4096)]
			private static extern void RemoveInfluenceAtIndex_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index);

			// Token: 0x060004E0 RID: 1248
			[MethodImpl(4096)]
			private static extern void RemoveInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemForceField field);

			// Token: 0x060004E1 RID: 1249
			[MethodImpl(4096)]
			private static extern void RemoveAllInfluences_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x060004E2 RID: 1250
			[MethodImpl(4096)]
			private static extern void SetInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index, ParticleSystemForceField field);

			// Token: 0x060004E3 RID: 1251
			[MethodImpl(4096)]
			private static extern ParticleSystemForceField GetInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index);

			// Token: 0x04000071 RID: 113
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000027 RID: 39
		public struct NoiseModule
		{
			// Token: 0x060004E4 RID: 1252 RVA: 0x00005116 File Offset: 0x00003316
			internal NoiseModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00005120 File Offset: 0x00003320
			// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00005128 File Offset: 0x00003328
			public bool enabled
			{
				get
				{
					return ParticleSystem.NoiseModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00005131 File Offset: 0x00003331
			// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00005139 File Offset: 0x00003339
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.NoiseModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00005144 File Offset: 0x00003344
			// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000515A File Offset: 0x0000335A
			[NativeName("StrengthX")]
			public ParticleSystem.MinMaxCurve strength
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_strength_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strength_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060004EB RID: 1259 RVA: 0x00005164 File Offset: 0x00003364
			// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000516C File Offset: 0x0000336C
			[NativeName("StrengthXMultiplier")]
			public float strengthMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060004ED RID: 1261 RVA: 0x00005178 File Offset: 0x00003378
			// (set) Token: 0x060004EE RID: 1262 RVA: 0x0000518E File Offset: 0x0000338E
			public ParticleSystem.MinMaxCurve strengthX
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_strengthX_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthX_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x00005198 File Offset: 0x00003398
			// (set) Token: 0x060004F0 RID: 1264 RVA: 0x000051A0 File Offset: 0x000033A0
			public float strengthXMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000051AC File Offset: 0x000033AC
			// (set) Token: 0x060004F2 RID: 1266 RVA: 0x000051C2 File Offset: 0x000033C2
			public ParticleSystem.MinMaxCurve strengthY
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_strengthY_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthY_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060004F3 RID: 1267 RVA: 0x000051CC File Offset: 0x000033CC
			// (set) Token: 0x060004F4 RID: 1268 RVA: 0x000051D4 File Offset: 0x000033D4
			public float strengthYMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000051E0 File Offset: 0x000033E0
			// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000051F6 File Offset: 0x000033F6
			public ParticleSystem.MinMaxCurve strengthZ
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_strengthZ_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00005200 File Offset: 0x00003400
			// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00005208 File Offset: 0x00003408
			public float strengthZMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00005211 File Offset: 0x00003411
			// (set) Token: 0x060004FA RID: 1274 RVA: 0x00005219 File Offset: 0x00003419
			public float frequency
			{
				get
				{
					return ParticleSystem.NoiseModule.get_frequency_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_frequency_Injected(ref this, value);
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060004FB RID: 1275 RVA: 0x00005222 File Offset: 0x00003422
			// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000522A File Offset: 0x0000342A
			public bool damping
			{
				get
				{
					return ParticleSystem.NoiseModule.get_damping_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_damping_Injected(ref this, value);
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060004FD RID: 1277 RVA: 0x00005233 File Offset: 0x00003433
			// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000523B File Offset: 0x0000343B
			public int octaveCount
			{
				get
				{
					return ParticleSystem.NoiseModule.get_octaveCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_octaveCount_Injected(ref this, value);
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x060004FF RID: 1279 RVA: 0x00005244 File Offset: 0x00003444
			// (set) Token: 0x06000500 RID: 1280 RVA: 0x0000524C File Offset: 0x0000344C
			public float octaveMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_octaveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_octaveMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000501 RID: 1281 RVA: 0x00005255 File Offset: 0x00003455
			// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000525D File Offset: 0x0000345D
			public float octaveScale
			{
				get
				{
					return ParticleSystem.NoiseModule.get_octaveScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_octaveScale_Injected(ref this, value);
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000503 RID: 1283 RVA: 0x00005266 File Offset: 0x00003466
			// (set) Token: 0x06000504 RID: 1284 RVA: 0x0000526E File Offset: 0x0000346E
			public ParticleSystemNoiseQuality quality
			{
				get
				{
					return ParticleSystem.NoiseModule.get_quality_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_quality_Injected(ref this, value);
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000505 RID: 1285 RVA: 0x00005278 File Offset: 0x00003478
			// (set) Token: 0x06000506 RID: 1286 RVA: 0x0000528E File Offset: 0x0000348E
			public ParticleSystem.MinMaxCurve scrollSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_scrollSpeed_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_scrollSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000507 RID: 1287 RVA: 0x00005298 File Offset: 0x00003498
			// (set) Token: 0x06000508 RID: 1288 RVA: 0x000052A0 File Offset: 0x000034A0
			public float scrollSpeedMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_scrollSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_scrollSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000509 RID: 1289 RVA: 0x000052A9 File Offset: 0x000034A9
			// (set) Token: 0x0600050A RID: 1290 RVA: 0x000052B1 File Offset: 0x000034B1
			public bool remapEnabled
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapEnabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapEnabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x0600050B RID: 1291 RVA: 0x000052BC File Offset: 0x000034BC
			// (set) Token: 0x0600050C RID: 1292 RVA: 0x000052D2 File Offset: 0x000034D2
			[NativeName("RemapX")]
			public ParticleSystem.MinMaxCurve remap
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_remap_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remap_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x0600050D RID: 1293 RVA: 0x000052DC File Offset: 0x000034DC
			// (set) Token: 0x0600050E RID: 1294 RVA: 0x000052E4 File Offset: 0x000034E4
			[NativeName("RemapXMultiplier")]
			public float remapMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x0600050F RID: 1295 RVA: 0x000052F0 File Offset: 0x000034F0
			// (set) Token: 0x06000510 RID: 1296 RVA: 0x00005306 File Offset: 0x00003506
			public ParticleSystem.MinMaxCurve remapX
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_remapX_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapX_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000511 RID: 1297 RVA: 0x00005310 File Offset: 0x00003510
			// (set) Token: 0x06000512 RID: 1298 RVA: 0x00005318 File Offset: 0x00003518
			public float remapXMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000513 RID: 1299 RVA: 0x00005324 File Offset: 0x00003524
			// (set) Token: 0x06000514 RID: 1300 RVA: 0x0000533A File Offset: 0x0000353A
			public ParticleSystem.MinMaxCurve remapY
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_remapY_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapY_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000515 RID: 1301 RVA: 0x00005344 File Offset: 0x00003544
			// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000534C File Offset: 0x0000354C
			public float remapYMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000517 RID: 1303 RVA: 0x00005358 File Offset: 0x00003558
			// (set) Token: 0x06000518 RID: 1304 RVA: 0x0000536E File Offset: 0x0000356E
			public ParticleSystem.MinMaxCurve remapZ
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_remapZ_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000519 RID: 1305 RVA: 0x00005378 File Offset: 0x00003578
			// (set) Token: 0x0600051A RID: 1306 RVA: 0x00005380 File Offset: 0x00003580
			public float remapZMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000538C File Offset: 0x0000358C
			// (set) Token: 0x0600051C RID: 1308 RVA: 0x000053A2 File Offset: 0x000035A2
			public ParticleSystem.MinMaxCurve positionAmount
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_positionAmount_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_positionAmount_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x0600051D RID: 1309 RVA: 0x000053AC File Offset: 0x000035AC
			// (set) Token: 0x0600051E RID: 1310 RVA: 0x000053C2 File Offset: 0x000035C2
			public ParticleSystem.MinMaxCurve rotationAmount
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_rotationAmount_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_rotationAmount_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x0600051F RID: 1311 RVA: 0x000053CC File Offset: 0x000035CC
			// (set) Token: 0x06000520 RID: 1312 RVA: 0x000053E2 File Offset: 0x000035E2
			public ParticleSystem.MinMaxCurve sizeAmount
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.NoiseModule.get_sizeAmount_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_sizeAmount_Injected(ref this, ref value);
				}
			}

			// Token: 0x06000521 RID: 1313
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000522 RID: 1314
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			// Token: 0x06000523 RID: 1315
			[MethodImpl(4096)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000524 RID: 1316
			[MethodImpl(4096)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			// Token: 0x06000525 RID: 1317
			[MethodImpl(4096)]
			private static extern void get_strength_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000526 RID: 1318
			[MethodImpl(4096)]
			private static extern void set_strength_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000527 RID: 1319
			[MethodImpl(4096)]
			private static extern float get_strengthMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000528 RID: 1320
			[MethodImpl(4096)]
			private static extern void set_strengthMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x06000529 RID: 1321
			[MethodImpl(4096)]
			private static extern void get_strengthX_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600052A RID: 1322
			[MethodImpl(4096)]
			private static extern void set_strengthX_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600052B RID: 1323
			[MethodImpl(4096)]
			private static extern float get_strengthXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x0600052C RID: 1324
			[MethodImpl(4096)]
			private static extern void set_strengthXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x0600052D RID: 1325
			[MethodImpl(4096)]
			private static extern void get_strengthY_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600052E RID: 1326
			[MethodImpl(4096)]
			private static extern void set_strengthY_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600052F RID: 1327
			[MethodImpl(4096)]
			private static extern float get_strengthYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000530 RID: 1328
			[MethodImpl(4096)]
			private static extern void set_strengthYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x06000531 RID: 1329
			[MethodImpl(4096)]
			private static extern void get_strengthZ_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000532 RID: 1330
			[MethodImpl(4096)]
			private static extern void set_strengthZ_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000533 RID: 1331
			[MethodImpl(4096)]
			private static extern float get_strengthZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000534 RID: 1332
			[MethodImpl(4096)]
			private static extern void set_strengthZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x06000535 RID: 1333
			[MethodImpl(4096)]
			private static extern float get_frequency_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000536 RID: 1334
			[MethodImpl(4096)]
			private static extern void set_frequency_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x06000537 RID: 1335
			[MethodImpl(4096)]
			private static extern bool get_damping_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000538 RID: 1336
			[MethodImpl(4096)]
			private static extern void set_damping_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			// Token: 0x06000539 RID: 1337
			[MethodImpl(4096)]
			private static extern int get_octaveCount_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x0600053A RID: 1338
			[MethodImpl(4096)]
			private static extern void set_octaveCount_Injected(ref ParticleSystem.NoiseModule _unity_self, int value);

			// Token: 0x0600053B RID: 1339
			[MethodImpl(4096)]
			private static extern float get_octaveMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x0600053C RID: 1340
			[MethodImpl(4096)]
			private static extern void set_octaveMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x0600053D RID: 1341
			[MethodImpl(4096)]
			private static extern float get_octaveScale_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x0600053E RID: 1342
			[MethodImpl(4096)]
			private static extern void set_octaveScale_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x0600053F RID: 1343
			[MethodImpl(4096)]
			private static extern ParticleSystemNoiseQuality get_quality_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000540 RID: 1344
			[MethodImpl(4096)]
			private static extern void set_quality_Injected(ref ParticleSystem.NoiseModule _unity_self, ParticleSystemNoiseQuality value);

			// Token: 0x06000541 RID: 1345
			[MethodImpl(4096)]
			private static extern void get_scrollSpeed_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000542 RID: 1346
			[MethodImpl(4096)]
			private static extern void set_scrollSpeed_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000543 RID: 1347
			[MethodImpl(4096)]
			private static extern float get_scrollSpeedMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000544 RID: 1348
			[MethodImpl(4096)]
			private static extern void set_scrollSpeedMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x06000545 RID: 1349
			[MethodImpl(4096)]
			private static extern bool get_remapEnabled_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000546 RID: 1350
			[MethodImpl(4096)]
			private static extern void set_remapEnabled_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			// Token: 0x06000547 RID: 1351
			[MethodImpl(4096)]
			private static extern void get_remap_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000548 RID: 1352
			[MethodImpl(4096)]
			private static extern void set_remap_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000549 RID: 1353
			[MethodImpl(4096)]
			private static extern float get_remapMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x0600054A RID: 1354
			[MethodImpl(4096)]
			private static extern void set_remapMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x0600054B RID: 1355
			[MethodImpl(4096)]
			private static extern void get_remapX_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600054C RID: 1356
			[MethodImpl(4096)]
			private static extern void set_remapX_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600054D RID: 1357
			[MethodImpl(4096)]
			private static extern float get_remapXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x0600054E RID: 1358
			[MethodImpl(4096)]
			private static extern void set_remapXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x0600054F RID: 1359
			[MethodImpl(4096)]
			private static extern void get_remapY_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000550 RID: 1360
			[MethodImpl(4096)]
			private static extern void set_remapY_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000551 RID: 1361
			[MethodImpl(4096)]
			private static extern float get_remapYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000552 RID: 1362
			[MethodImpl(4096)]
			private static extern void set_remapYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x06000553 RID: 1363
			[MethodImpl(4096)]
			private static extern void get_remapZ_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000554 RID: 1364
			[MethodImpl(4096)]
			private static extern void set_remapZ_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000555 RID: 1365
			[MethodImpl(4096)]
			private static extern float get_remapZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x06000556 RID: 1366
			[MethodImpl(4096)]
			private static extern void set_remapZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x06000557 RID: 1367
			[MethodImpl(4096)]
			private static extern void get_positionAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000558 RID: 1368
			[MethodImpl(4096)]
			private static extern void set_positionAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000559 RID: 1369
			[MethodImpl(4096)]
			private static extern void get_rotationAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600055A RID: 1370
			[MethodImpl(4096)]
			private static extern void set_rotationAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600055B RID: 1371
			[MethodImpl(4096)]
			private static extern void get_sizeAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600055C RID: 1372
			[MethodImpl(4096)]
			private static extern void set_sizeAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x04000072 RID: 114
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000028 RID: 40
		public struct CollisionModule
		{
			// Token: 0x0600055D RID: 1373 RVA: 0x000053EC File Offset: 0x000035EC
			internal CollisionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x0600055E RID: 1374 RVA: 0x000053F6 File Offset: 0x000035F6
			// (set) Token: 0x0600055F RID: 1375 RVA: 0x000053FE File Offset: 0x000035FE
			public bool enabled
			{
				get
				{
					return ParticleSystem.CollisionModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000560 RID: 1376 RVA: 0x00005407 File Offset: 0x00003607
			// (set) Token: 0x06000561 RID: 1377 RVA: 0x0000540F File Offset: 0x0000360F
			public ParticleSystemCollisionType type
			{
				get
				{
					return ParticleSystem.CollisionModule.get_type_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_type_Injected(ref this, value);
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x06000562 RID: 1378 RVA: 0x00005418 File Offset: 0x00003618
			// (set) Token: 0x06000563 RID: 1379 RVA: 0x00005420 File Offset: 0x00003620
			public ParticleSystemCollisionMode mode
			{
				get
				{
					return ParticleSystem.CollisionModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_mode_Injected(ref this, value);
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000542C File Offset: 0x0000362C
			// (set) Token: 0x06000565 RID: 1381 RVA: 0x00005442 File Offset: 0x00003642
			public ParticleSystem.MinMaxCurve dampen
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.CollisionModule.get_dampen_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_dampen_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000544C File Offset: 0x0000364C
			// (set) Token: 0x06000567 RID: 1383 RVA: 0x00005454 File Offset: 0x00003654
			public float dampenMultiplier
			{
				get
				{
					return ParticleSystem.CollisionModule.get_dampenMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_dampenMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x06000568 RID: 1384 RVA: 0x00005460 File Offset: 0x00003660
			// (set) Token: 0x06000569 RID: 1385 RVA: 0x00005476 File Offset: 0x00003676
			public ParticleSystem.MinMaxCurve bounce
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.CollisionModule.get_bounce_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_bounce_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x0600056A RID: 1386 RVA: 0x00005480 File Offset: 0x00003680
			// (set) Token: 0x0600056B RID: 1387 RVA: 0x00005488 File Offset: 0x00003688
			public float bounceMultiplier
			{
				get
				{
					return ParticleSystem.CollisionModule.get_bounceMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_bounceMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x0600056C RID: 1388 RVA: 0x00005494 File Offset: 0x00003694
			// (set) Token: 0x0600056D RID: 1389 RVA: 0x000054AA File Offset: 0x000036AA
			public ParticleSystem.MinMaxCurve lifetimeLoss
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.CollisionModule.get_lifetimeLoss_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_lifetimeLoss_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x0600056E RID: 1390 RVA: 0x000054B4 File Offset: 0x000036B4
			// (set) Token: 0x0600056F RID: 1391 RVA: 0x000054BC File Offset: 0x000036BC
			public float lifetimeLossMultiplier
			{
				get
				{
					return ParticleSystem.CollisionModule.get_lifetimeLossMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_lifetimeLossMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000570 RID: 1392 RVA: 0x000054C5 File Offset: 0x000036C5
			// (set) Token: 0x06000571 RID: 1393 RVA: 0x000054CD File Offset: 0x000036CD
			public float minKillSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.get_minKillSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_minKillSpeed_Injected(ref this, value);
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x000054D6 File Offset: 0x000036D6
			// (set) Token: 0x06000573 RID: 1395 RVA: 0x000054DE File Offset: 0x000036DE
			public float maxKillSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.get_maxKillSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_maxKillSpeed_Injected(ref this, value);
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000574 RID: 1396 RVA: 0x000054E8 File Offset: 0x000036E8
			// (set) Token: 0x06000575 RID: 1397 RVA: 0x000054FE File Offset: 0x000036FE
			public LayerMask collidesWith
			{
				get
				{
					LayerMask layerMask;
					ParticleSystem.CollisionModule.get_collidesWith_Injected(ref this, out layerMask);
					return layerMask;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_collidesWith_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x06000576 RID: 1398 RVA: 0x00005508 File Offset: 0x00003708
			// (set) Token: 0x06000577 RID: 1399 RVA: 0x00005510 File Offset: 0x00003710
			public bool enableDynamicColliders
			{
				get
				{
					return ParticleSystem.CollisionModule.get_enableDynamicColliders_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_enableDynamicColliders_Injected(ref this, value);
				}
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x06000578 RID: 1400 RVA: 0x00005519 File Offset: 0x00003719
			// (set) Token: 0x06000579 RID: 1401 RVA: 0x00005521 File Offset: 0x00003721
			public int maxCollisionShapes
			{
				get
				{
					return ParticleSystem.CollisionModule.get_maxCollisionShapes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_maxCollisionShapes_Injected(ref this, value);
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000552A File Offset: 0x0000372A
			// (set) Token: 0x0600057B RID: 1403 RVA: 0x00005532 File Offset: 0x00003732
			public ParticleSystemCollisionQuality quality
			{
				get
				{
					return ParticleSystem.CollisionModule.get_quality_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_quality_Injected(ref this, value);
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000553B File Offset: 0x0000373B
			// (set) Token: 0x0600057D RID: 1405 RVA: 0x00005543 File Offset: 0x00003743
			public float voxelSize
			{
				get
				{
					return ParticleSystem.CollisionModule.get_voxelSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_voxelSize_Injected(ref this, value);
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000554C File Offset: 0x0000374C
			// (set) Token: 0x0600057F RID: 1407 RVA: 0x00005554 File Offset: 0x00003754
			public float radiusScale
			{
				get
				{
					return ParticleSystem.CollisionModule.get_radiusScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_radiusScale_Injected(ref this, value);
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000555D File Offset: 0x0000375D
			// (set) Token: 0x06000581 RID: 1409 RVA: 0x00005565 File Offset: 0x00003765
			public bool sendCollisionMessages
			{
				get
				{
					return ParticleSystem.CollisionModule.get_sendCollisionMessages_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_sendCollisionMessages_Injected(ref this, value);
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000556E File Offset: 0x0000376E
			// (set) Token: 0x06000583 RID: 1411 RVA: 0x00005576 File Offset: 0x00003776
			public float colliderForce
			{
				get
				{
					return ParticleSystem.CollisionModule.get_colliderForce_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_colliderForce_Injected(ref this, value);
				}
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000557F File Offset: 0x0000377F
			// (set) Token: 0x06000585 RID: 1413 RVA: 0x00005587 File Offset: 0x00003787
			public bool multiplyColliderForceByCollisionAngle
			{
				get
				{
					return ParticleSystem.CollisionModule.get_multiplyColliderForceByCollisionAngle_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_multiplyColliderForceByCollisionAngle_Injected(ref this, value);
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000586 RID: 1414 RVA: 0x00005590 File Offset: 0x00003790
			// (set) Token: 0x06000587 RID: 1415 RVA: 0x00005598 File Offset: 0x00003798
			public bool multiplyColliderForceByParticleSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.get_multiplyColliderForceByParticleSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_multiplyColliderForceByParticleSpeed_Injected(ref this, value);
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000588 RID: 1416 RVA: 0x000055A1 File Offset: 0x000037A1
			// (set) Token: 0x06000589 RID: 1417 RVA: 0x000055A9 File Offset: 0x000037A9
			public bool multiplyColliderForceByParticleSize
			{
				get
				{
					return ParticleSystem.CollisionModule.get_multiplyColliderForceByParticleSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_multiplyColliderForceByParticleSize_Injected(ref this, value);
				}
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x000055B2 File Offset: 0x000037B2
			public void SetPlane(int index, Transform transform)
			{
				ParticleSystem.CollisionModule.SetPlane_Injected(ref this, index, transform);
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x000055BC File Offset: 0x000037BC
			public Transform GetPlane(int index)
			{
				return ParticleSystem.CollisionModule.GetPlane_Injected(ref this, index);
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x0600058C RID: 1420 RVA: 0x000055C5 File Offset: 0x000037C5
			public int maxPlaneCount
			{
				get
				{
					return ParticleSystem.CollisionModule.get_maxPlaneCount_Injected(ref this);
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x000055CD File Offset: 0x000037CD
			// (set) Token: 0x0600058E RID: 1422 RVA: 0x000055D5 File Offset: 0x000037D5
			[Obsolete("enableInteriorCollisions property is deprecated and is no longer required and has no effect on the particle system.", false)]
			public bool enableInteriorCollisions
			{
				get
				{
					return ParticleSystem.CollisionModule.get_enableInteriorCollisions_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_enableInteriorCollisions_Injected(ref this, value);
				}
			}

			// Token: 0x0600058F RID: 1423
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000590 RID: 1424
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x06000591 RID: 1425
			[MethodImpl(4096)]
			private static extern ParticleSystemCollisionType get_type_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000592 RID: 1426
			[MethodImpl(4096)]
			private static extern void set_type_Injected(ref ParticleSystem.CollisionModule _unity_self, ParticleSystemCollisionType value);

			// Token: 0x06000593 RID: 1427
			[MethodImpl(4096)]
			private static extern ParticleSystemCollisionMode get_mode_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000594 RID: 1428
			[MethodImpl(4096)]
			private static extern void set_mode_Injected(ref ParticleSystem.CollisionModule _unity_self, ParticleSystemCollisionMode value);

			// Token: 0x06000595 RID: 1429
			[MethodImpl(4096)]
			private static extern void get_dampen_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000596 RID: 1430
			[MethodImpl(4096)]
			private static extern void set_dampen_Injected(ref ParticleSystem.CollisionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000597 RID: 1431
			[MethodImpl(4096)]
			private static extern float get_dampenMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000598 RID: 1432
			[MethodImpl(4096)]
			private static extern void set_dampenMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x06000599 RID: 1433
			[MethodImpl(4096)]
			private static extern void get_bounce_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600059A RID: 1434
			[MethodImpl(4096)]
			private static extern void set_bounce_Injected(ref ParticleSystem.CollisionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600059B RID: 1435
			[MethodImpl(4096)]
			private static extern float get_bounceMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600059C RID: 1436
			[MethodImpl(4096)]
			private static extern void set_bounceMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x0600059D RID: 1437
			[MethodImpl(4096)]
			private static extern void get_lifetimeLoss_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600059E RID: 1438
			[MethodImpl(4096)]
			private static extern void set_lifetimeLoss_Injected(ref ParticleSystem.CollisionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600059F RID: 1439
			[MethodImpl(4096)]
			private static extern float get_lifetimeLossMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005A0 RID: 1440
			[MethodImpl(4096)]
			private static extern void set_lifetimeLossMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x060005A1 RID: 1441
			[MethodImpl(4096)]
			private static extern float get_minKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005A2 RID: 1442
			[MethodImpl(4096)]
			private static extern void set_minKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x060005A3 RID: 1443
			[MethodImpl(4096)]
			private static extern float get_maxKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005A4 RID: 1444
			[MethodImpl(4096)]
			private static extern void set_maxKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x060005A5 RID: 1445
			[MethodImpl(4096)]
			private static extern void get_collidesWith_Injected(ref ParticleSystem.CollisionModule _unity_self, out LayerMask ret);

			// Token: 0x060005A6 RID: 1446
			[MethodImpl(4096)]
			private static extern void set_collidesWith_Injected(ref ParticleSystem.CollisionModule _unity_self, ref LayerMask value);

			// Token: 0x060005A7 RID: 1447
			[MethodImpl(4096)]
			private static extern bool get_enableDynamicColliders_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005A8 RID: 1448
			[MethodImpl(4096)]
			private static extern void set_enableDynamicColliders_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x060005A9 RID: 1449
			[MethodImpl(4096)]
			private static extern int get_maxCollisionShapes_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005AA RID: 1450
			[MethodImpl(4096)]
			private static extern void set_maxCollisionShapes_Injected(ref ParticleSystem.CollisionModule _unity_self, int value);

			// Token: 0x060005AB RID: 1451
			[MethodImpl(4096)]
			private static extern ParticleSystemCollisionQuality get_quality_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005AC RID: 1452
			[MethodImpl(4096)]
			private static extern void set_quality_Injected(ref ParticleSystem.CollisionModule _unity_self, ParticleSystemCollisionQuality value);

			// Token: 0x060005AD RID: 1453
			[MethodImpl(4096)]
			private static extern float get_voxelSize_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005AE RID: 1454
			[MethodImpl(4096)]
			private static extern void set_voxelSize_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x060005AF RID: 1455
			[MethodImpl(4096)]
			private static extern float get_radiusScale_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005B0 RID: 1456
			[MethodImpl(4096)]
			private static extern void set_radiusScale_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x060005B1 RID: 1457
			[MethodImpl(4096)]
			private static extern bool get_sendCollisionMessages_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005B2 RID: 1458
			[MethodImpl(4096)]
			private static extern void set_sendCollisionMessages_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x060005B3 RID: 1459
			[MethodImpl(4096)]
			private static extern float get_colliderForce_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005B4 RID: 1460
			[MethodImpl(4096)]
			private static extern void set_colliderForce_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x060005B5 RID: 1461
			[MethodImpl(4096)]
			private static extern bool get_multiplyColliderForceByCollisionAngle_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005B6 RID: 1462
			[MethodImpl(4096)]
			private static extern void set_multiplyColliderForceByCollisionAngle_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x060005B7 RID: 1463
			[MethodImpl(4096)]
			private static extern bool get_multiplyColliderForceByParticleSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005B8 RID: 1464
			[MethodImpl(4096)]
			private static extern void set_multiplyColliderForceByParticleSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x060005B9 RID: 1465
			[MethodImpl(4096)]
			private static extern bool get_multiplyColliderForceByParticleSize_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005BA RID: 1466
			[MethodImpl(4096)]
			private static extern void set_multiplyColliderForceByParticleSize_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x060005BB RID: 1467
			[MethodImpl(4096)]
			private static extern void SetPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index, Transform transform);

			// Token: 0x060005BC RID: 1468
			[MethodImpl(4096)]
			private static extern Transform GetPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index);

			// Token: 0x060005BD RID: 1469
			[MethodImpl(4096)]
			private static extern int get_maxPlaneCount_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005BE RID: 1470
			[MethodImpl(4096)]
			private static extern bool get_enableInteriorCollisions_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x060005BF RID: 1471
			[MethodImpl(4096)]
			private static extern void set_enableInteriorCollisions_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x04000073 RID: 115
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000029 RID: 41
		public struct TriggerModule
		{
			// Token: 0x060005C0 RID: 1472 RVA: 0x000055DE File Offset: 0x000037DE
			internal TriggerModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000055E8 File Offset: 0x000037E8
			// (set) Token: 0x060005C2 RID: 1474 RVA: 0x000055F0 File Offset: 0x000037F0
			public bool enabled
			{
				get
				{
					return ParticleSystem.TriggerModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000055F9 File Offset: 0x000037F9
			// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00005601 File Offset: 0x00003801
			public ParticleSystemOverlapAction inside
			{
				get
				{
					return ParticleSystem.TriggerModule.get_inside_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_inside_Injected(ref this, value);
				}
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000560A File Offset: 0x0000380A
			// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00005612 File Offset: 0x00003812
			public ParticleSystemOverlapAction outside
			{
				get
				{
					return ParticleSystem.TriggerModule.get_outside_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_outside_Injected(ref this, value);
				}
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000561B File Offset: 0x0000381B
			// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00005623 File Offset: 0x00003823
			public ParticleSystemOverlapAction enter
			{
				get
				{
					return ParticleSystem.TriggerModule.get_enter_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_enter_Injected(ref this, value);
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000562C File Offset: 0x0000382C
			// (set) Token: 0x060005CA RID: 1482 RVA: 0x00005634 File Offset: 0x00003834
			public ParticleSystemOverlapAction exit
			{
				get
				{
					return ParticleSystem.TriggerModule.get_exit_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_exit_Injected(ref this, value);
				}
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000563D File Offset: 0x0000383D
			// (set) Token: 0x060005CC RID: 1484 RVA: 0x00005645 File Offset: 0x00003845
			public float radiusScale
			{
				get
				{
					return ParticleSystem.TriggerModule.get_radiusScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_radiusScale_Injected(ref this, value);
				}
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x0000564E File Offset: 0x0000384E
			[NativeThrows]
			public void SetCollider(int index, Component collider)
			{
				ParticleSystem.TriggerModule.SetCollider_Injected(ref this, index, collider);
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x00005658 File Offset: 0x00003858
			[NativeThrows]
			public Component GetCollider(int index)
			{
				return ParticleSystem.TriggerModule.GetCollider_Injected(ref this, index);
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x060005CF RID: 1487 RVA: 0x00005661 File Offset: 0x00003861
			public int maxColliderCount
			{
				get
				{
					return ParticleSystem.TriggerModule.get_maxColliderCount_Injected(ref this);
				}
			}

			// Token: 0x060005D0 RID: 1488
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060005D1 RID: 1489
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.TriggerModule _unity_self, bool value);

			// Token: 0x060005D2 RID: 1490
			[MethodImpl(4096)]
			private static extern ParticleSystemOverlapAction get_inside_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060005D3 RID: 1491
			[MethodImpl(4096)]
			private static extern void set_inside_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			// Token: 0x060005D4 RID: 1492
			[MethodImpl(4096)]
			private static extern ParticleSystemOverlapAction get_outside_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060005D5 RID: 1493
			[MethodImpl(4096)]
			private static extern void set_outside_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			// Token: 0x060005D6 RID: 1494
			[MethodImpl(4096)]
			private static extern ParticleSystemOverlapAction get_enter_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060005D7 RID: 1495
			[MethodImpl(4096)]
			private static extern void set_enter_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			// Token: 0x060005D8 RID: 1496
			[MethodImpl(4096)]
			private static extern ParticleSystemOverlapAction get_exit_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060005D9 RID: 1497
			[MethodImpl(4096)]
			private static extern void set_exit_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			// Token: 0x060005DA RID: 1498
			[MethodImpl(4096)]
			private static extern float get_radiusScale_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060005DB RID: 1499
			[MethodImpl(4096)]
			private static extern void set_radiusScale_Injected(ref ParticleSystem.TriggerModule _unity_self, float value);

			// Token: 0x060005DC RID: 1500
			[MethodImpl(4096)]
			private static extern void SetCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index, Component collider);

			// Token: 0x060005DD RID: 1501
			[MethodImpl(4096)]
			private static extern Component GetCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index);

			// Token: 0x060005DE RID: 1502
			[MethodImpl(4096)]
			private static extern int get_maxColliderCount_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x04000074 RID: 116
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002A RID: 42
		public struct LightsModule
		{
			// Token: 0x060005DF RID: 1503 RVA: 0x00005669 File Offset: 0x00003869
			internal LightsModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00005673 File Offset: 0x00003873
			// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0000567B File Offset: 0x0000387B
			public bool enabled
			{
				get
				{
					return ParticleSystem.LightsModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00005684 File Offset: 0x00003884
			// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0000568C File Offset: 0x0000388C
			public float ratio
			{
				get
				{
					return ParticleSystem.LightsModule.get_ratio_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_ratio_Injected(ref this, value);
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00005695 File Offset: 0x00003895
			// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000569D File Offset: 0x0000389D
			public bool useRandomDistribution
			{
				get
				{
					return ParticleSystem.LightsModule.get_useRandomDistribution_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_useRandomDistribution_Injected(ref this, value);
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000056A6 File Offset: 0x000038A6
			// (set) Token: 0x060005E7 RID: 1511 RVA: 0x000056AE File Offset: 0x000038AE
			public Light light
			{
				get
				{
					return ParticleSystem.LightsModule.get_light_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_light_Injected(ref this, value);
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000056B7 File Offset: 0x000038B7
			// (set) Token: 0x060005E9 RID: 1513 RVA: 0x000056BF File Offset: 0x000038BF
			public bool useParticleColor
			{
				get
				{
					return ParticleSystem.LightsModule.get_useParticleColor_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_useParticleColor_Injected(ref this, value);
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x060005EA RID: 1514 RVA: 0x000056C8 File Offset: 0x000038C8
			// (set) Token: 0x060005EB RID: 1515 RVA: 0x000056D0 File Offset: 0x000038D0
			public bool sizeAffectsRange
			{
				get
				{
					return ParticleSystem.LightsModule.get_sizeAffectsRange_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_sizeAffectsRange_Injected(ref this, value);
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060005EC RID: 1516 RVA: 0x000056D9 File Offset: 0x000038D9
			// (set) Token: 0x060005ED RID: 1517 RVA: 0x000056E1 File Offset: 0x000038E1
			public bool alphaAffectsIntensity
			{
				get
				{
					return ParticleSystem.LightsModule.get_alphaAffectsIntensity_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_alphaAffectsIntensity_Injected(ref this, value);
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060005EE RID: 1518 RVA: 0x000056EC File Offset: 0x000038EC
			// (set) Token: 0x060005EF RID: 1519 RVA: 0x00005702 File Offset: 0x00003902
			public ParticleSystem.MinMaxCurve range
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.LightsModule.get_range_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000570C File Offset: 0x0000390C
			// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00005714 File Offset: 0x00003914
			public float rangeMultiplier
			{
				get
				{
					return ParticleSystem.LightsModule.get_rangeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_rangeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00005720 File Offset: 0x00003920
			// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00005736 File Offset: 0x00003936
			public ParticleSystem.MinMaxCurve intensity
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.LightsModule.get_intensity_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_intensity_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00005740 File Offset: 0x00003940
			// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00005748 File Offset: 0x00003948
			public float intensityMultiplier
			{
				get
				{
					return ParticleSystem.LightsModule.get_intensityMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_intensityMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00005751 File Offset: 0x00003951
			// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00005759 File Offset: 0x00003959
			public int maxLights
			{
				get
				{
					return ParticleSystem.LightsModule.get_maxLights_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_maxLights_Injected(ref this, value);
				}
			}

			// Token: 0x060005F8 RID: 1528
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x060005F9 RID: 1529
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x060005FA RID: 1530
			[MethodImpl(4096)]
			private static extern float get_ratio_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x060005FB RID: 1531
			[MethodImpl(4096)]
			private static extern void set_ratio_Injected(ref ParticleSystem.LightsModule _unity_self, float value);

			// Token: 0x060005FC RID: 1532
			[MethodImpl(4096)]
			private static extern bool get_useRandomDistribution_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x060005FD RID: 1533
			[MethodImpl(4096)]
			private static extern void set_useRandomDistribution_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x060005FE RID: 1534
			[MethodImpl(4096)]
			private static extern Light get_light_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x060005FF RID: 1535
			[MethodImpl(4096)]
			private static extern void set_light_Injected(ref ParticleSystem.LightsModule _unity_self, Light value);

			// Token: 0x06000600 RID: 1536
			[MethodImpl(4096)]
			private static extern bool get_useParticleColor_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000601 RID: 1537
			[MethodImpl(4096)]
			private static extern void set_useParticleColor_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x06000602 RID: 1538
			[MethodImpl(4096)]
			private static extern bool get_sizeAffectsRange_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000603 RID: 1539
			[MethodImpl(4096)]
			private static extern void set_sizeAffectsRange_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x06000604 RID: 1540
			[MethodImpl(4096)]
			private static extern bool get_alphaAffectsIntensity_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000605 RID: 1541
			[MethodImpl(4096)]
			private static extern void set_alphaAffectsIntensity_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x06000606 RID: 1542
			[MethodImpl(4096)]
			private static extern void get_range_Injected(ref ParticleSystem.LightsModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000607 RID: 1543
			[MethodImpl(4096)]
			private static extern void set_range_Injected(ref ParticleSystem.LightsModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000608 RID: 1544
			[MethodImpl(4096)]
			private static extern float get_rangeMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000609 RID: 1545
			[MethodImpl(4096)]
			private static extern void set_rangeMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self, float value);

			// Token: 0x0600060A RID: 1546
			[MethodImpl(4096)]
			private static extern void get_intensity_Injected(ref ParticleSystem.LightsModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600060B RID: 1547
			[MethodImpl(4096)]
			private static extern void set_intensity_Injected(ref ParticleSystem.LightsModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600060C RID: 1548
			[MethodImpl(4096)]
			private static extern float get_intensityMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x0600060D RID: 1549
			[MethodImpl(4096)]
			private static extern void set_intensityMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self, float value);

			// Token: 0x0600060E RID: 1550
			[MethodImpl(4096)]
			private static extern int get_maxLights_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x0600060F RID: 1551
			[MethodImpl(4096)]
			private static extern void set_maxLights_Injected(ref ParticleSystem.LightsModule _unity_self, int value);

			// Token: 0x04000075 RID: 117
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002B RID: 43
		public struct TrailModule
		{
			// Token: 0x06000610 RID: 1552 RVA: 0x00005762 File Offset: 0x00003962
			internal TrailModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000576C File Offset: 0x0000396C
			// (set) Token: 0x06000612 RID: 1554 RVA: 0x00005774 File Offset: 0x00003974
			public bool enabled
			{
				get
				{
					return ParticleSystem.TrailModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000577D File Offset: 0x0000397D
			// (set) Token: 0x06000614 RID: 1556 RVA: 0x00005785 File Offset: 0x00003985
			public ParticleSystemTrailMode mode
			{
				get
				{
					return ParticleSystem.TrailModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_mode_Injected(ref this, value);
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000578E File Offset: 0x0000398E
			// (set) Token: 0x06000616 RID: 1558 RVA: 0x00005796 File Offset: 0x00003996
			public float ratio
			{
				get
				{
					return ParticleSystem.TrailModule.get_ratio_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_ratio_Injected(ref this, value);
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000617 RID: 1559 RVA: 0x000057A0 File Offset: 0x000039A0
			// (set) Token: 0x06000618 RID: 1560 RVA: 0x000057B6 File Offset: 0x000039B6
			public ParticleSystem.MinMaxCurve lifetime
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.TrailModule.get_lifetime_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_lifetime_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06000619 RID: 1561 RVA: 0x000057C0 File Offset: 0x000039C0
			// (set) Token: 0x0600061A RID: 1562 RVA: 0x000057C8 File Offset: 0x000039C8
			public float lifetimeMultiplier
			{
				get
				{
					return ParticleSystem.TrailModule.get_lifetimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_lifetimeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x0600061B RID: 1563 RVA: 0x000057D1 File Offset: 0x000039D1
			// (set) Token: 0x0600061C RID: 1564 RVA: 0x000057D9 File Offset: 0x000039D9
			public float minVertexDistance
			{
				get
				{
					return ParticleSystem.TrailModule.get_minVertexDistance_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_minVertexDistance_Injected(ref this, value);
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x0600061D RID: 1565 RVA: 0x000057E2 File Offset: 0x000039E2
			// (set) Token: 0x0600061E RID: 1566 RVA: 0x000057EA File Offset: 0x000039EA
			public ParticleSystemTrailTextureMode textureMode
			{
				get
				{
					return ParticleSystem.TrailModule.get_textureMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_textureMode_Injected(ref this, value);
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x0600061F RID: 1567 RVA: 0x000057F3 File Offset: 0x000039F3
			// (set) Token: 0x06000620 RID: 1568 RVA: 0x000057FB File Offset: 0x000039FB
			public bool worldSpace
			{
				get
				{
					return ParticleSystem.TrailModule.get_worldSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_worldSpace_Injected(ref this, value);
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000621 RID: 1569 RVA: 0x00005804 File Offset: 0x00003A04
			// (set) Token: 0x06000622 RID: 1570 RVA: 0x0000580C File Offset: 0x00003A0C
			public bool dieWithParticles
			{
				get
				{
					return ParticleSystem.TrailModule.get_dieWithParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_dieWithParticles_Injected(ref this, value);
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000623 RID: 1571 RVA: 0x00005815 File Offset: 0x00003A15
			// (set) Token: 0x06000624 RID: 1572 RVA: 0x0000581D File Offset: 0x00003A1D
			public bool sizeAffectsWidth
			{
				get
				{
					return ParticleSystem.TrailModule.get_sizeAffectsWidth_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_sizeAffectsWidth_Injected(ref this, value);
				}
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000625 RID: 1573 RVA: 0x00005826 File Offset: 0x00003A26
			// (set) Token: 0x06000626 RID: 1574 RVA: 0x0000582E File Offset: 0x00003A2E
			public bool sizeAffectsLifetime
			{
				get
				{
					return ParticleSystem.TrailModule.get_sizeAffectsLifetime_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_sizeAffectsLifetime_Injected(ref this, value);
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000627 RID: 1575 RVA: 0x00005837 File Offset: 0x00003A37
			// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000583F File Offset: 0x00003A3F
			public bool inheritParticleColor
			{
				get
				{
					return ParticleSystem.TrailModule.get_inheritParticleColor_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_inheritParticleColor_Injected(ref this, value);
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000629 RID: 1577 RVA: 0x00005848 File Offset: 0x00003A48
			// (set) Token: 0x0600062A RID: 1578 RVA: 0x0000585E File Offset: 0x00003A5E
			public ParticleSystem.MinMaxGradient colorOverLifetime
			{
				get
				{
					ParticleSystem.MinMaxGradient minMaxGradient;
					ParticleSystem.TrailModule.get_colorOverLifetime_Injected(ref this, out minMaxGradient);
					return minMaxGradient;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_colorOverLifetime_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x0600062B RID: 1579 RVA: 0x00005868 File Offset: 0x00003A68
			// (set) Token: 0x0600062C RID: 1580 RVA: 0x0000587E File Offset: 0x00003A7E
			public ParticleSystem.MinMaxCurve widthOverTrail
			{
				get
				{
					ParticleSystem.MinMaxCurve minMaxCurve;
					ParticleSystem.TrailModule.get_widthOverTrail_Injected(ref this, out minMaxCurve);
					return minMaxCurve;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_widthOverTrail_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x0600062D RID: 1581 RVA: 0x00005888 File Offset: 0x00003A88
			// (set) Token: 0x0600062E RID: 1582 RVA: 0x00005890 File Offset: 0x00003A90
			public float widthOverTrailMultiplier
			{
				get
				{
					return ParticleSystem.TrailModule.get_widthOverTrailMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_widthOverTrailMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000589C File Offset: 0x00003A9C
			// (set) Token: 0x06000630 RID: 1584 RVA: 0x000058B2 File Offset: 0x00003AB2
			public ParticleSystem.MinMaxGradient colorOverTrail
			{
				get
				{
					ParticleSystem.MinMaxGradient minMaxGradient;
					ParticleSystem.TrailModule.get_colorOverTrail_Injected(ref this, out minMaxGradient);
					return minMaxGradient;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_colorOverTrail_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06000631 RID: 1585 RVA: 0x000058BC File Offset: 0x00003ABC
			// (set) Token: 0x06000632 RID: 1586 RVA: 0x000058C4 File Offset: 0x00003AC4
			public bool generateLightingData
			{
				get
				{
					return ParticleSystem.TrailModule.get_generateLightingData_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_generateLightingData_Injected(ref this, value);
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000633 RID: 1587 RVA: 0x000058CD File Offset: 0x00003ACD
			// (set) Token: 0x06000634 RID: 1588 RVA: 0x000058D5 File Offset: 0x00003AD5
			public int ribbonCount
			{
				get
				{
					return ParticleSystem.TrailModule.get_ribbonCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_ribbonCount_Injected(ref this, value);
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000635 RID: 1589 RVA: 0x000058DE File Offset: 0x00003ADE
			// (set) Token: 0x06000636 RID: 1590 RVA: 0x000058E6 File Offset: 0x00003AE6
			public float shadowBias
			{
				get
				{
					return ParticleSystem.TrailModule.get_shadowBias_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_shadowBias_Injected(ref this, value);
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000637 RID: 1591 RVA: 0x000058EF File Offset: 0x00003AEF
			// (set) Token: 0x06000638 RID: 1592 RVA: 0x000058F7 File Offset: 0x00003AF7
			public bool splitSubEmitterRibbons
			{
				get
				{
					return ParticleSystem.TrailModule.get_splitSubEmitterRibbons_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_splitSubEmitterRibbons_Injected(ref this, value);
				}
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000639 RID: 1593 RVA: 0x00005900 File Offset: 0x00003B00
			// (set) Token: 0x0600063A RID: 1594 RVA: 0x00005908 File Offset: 0x00003B08
			public bool attachRibbonsToTransform
			{
				get
				{
					return ParticleSystem.TrailModule.get_attachRibbonsToTransform_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_attachRibbonsToTransform_Injected(ref this, value);
				}
			}

			// Token: 0x0600063B RID: 1595
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600063C RID: 1596
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x0600063D RID: 1597
			[MethodImpl(4096)]
			private static extern ParticleSystemTrailMode get_mode_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600063E RID: 1598
			[MethodImpl(4096)]
			private static extern void set_mode_Injected(ref ParticleSystem.TrailModule _unity_self, ParticleSystemTrailMode value);

			// Token: 0x0600063F RID: 1599
			[MethodImpl(4096)]
			private static extern float get_ratio_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000640 RID: 1600
			[MethodImpl(4096)]
			private static extern void set_ratio_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x06000641 RID: 1601
			[MethodImpl(4096)]
			private static extern void get_lifetime_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000642 RID: 1602
			[MethodImpl(4096)]
			private static extern void set_lifetime_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000643 RID: 1603
			[MethodImpl(4096)]
			private static extern float get_lifetimeMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000644 RID: 1604
			[MethodImpl(4096)]
			private static extern void set_lifetimeMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x06000645 RID: 1605
			[MethodImpl(4096)]
			private static extern float get_minVertexDistance_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000646 RID: 1606
			[MethodImpl(4096)]
			private static extern void set_minVertexDistance_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x06000647 RID: 1607
			[MethodImpl(4096)]
			private static extern ParticleSystemTrailTextureMode get_textureMode_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000648 RID: 1608
			[MethodImpl(4096)]
			private static extern void set_textureMode_Injected(ref ParticleSystem.TrailModule _unity_self, ParticleSystemTrailTextureMode value);

			// Token: 0x06000649 RID: 1609
			[MethodImpl(4096)]
			private static extern bool get_worldSpace_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600064A RID: 1610
			[MethodImpl(4096)]
			private static extern void set_worldSpace_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x0600064B RID: 1611
			[MethodImpl(4096)]
			private static extern bool get_dieWithParticles_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600064C RID: 1612
			[MethodImpl(4096)]
			private static extern void set_dieWithParticles_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x0600064D RID: 1613
			[MethodImpl(4096)]
			private static extern bool get_sizeAffectsWidth_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600064E RID: 1614
			[MethodImpl(4096)]
			private static extern void set_sizeAffectsWidth_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x0600064F RID: 1615
			[MethodImpl(4096)]
			private static extern bool get_sizeAffectsLifetime_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000650 RID: 1616
			[MethodImpl(4096)]
			private static extern void set_sizeAffectsLifetime_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x06000651 RID: 1617
			[MethodImpl(4096)]
			private static extern bool get_inheritParticleColor_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000652 RID: 1618
			[MethodImpl(4096)]
			private static extern void set_inheritParticleColor_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x06000653 RID: 1619
			[MethodImpl(4096)]
			private static extern void get_colorOverLifetime_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x06000654 RID: 1620
			[MethodImpl(4096)]
			private static extern void set_colorOverLifetime_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x06000655 RID: 1621
			[MethodImpl(4096)]
			private static extern void get_widthOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000656 RID: 1622
			[MethodImpl(4096)]
			private static extern void set_widthOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000657 RID: 1623
			[MethodImpl(4096)]
			private static extern float get_widthOverTrailMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000658 RID: 1624
			[MethodImpl(4096)]
			private static extern void set_widthOverTrailMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x06000659 RID: 1625
			[MethodImpl(4096)]
			private static extern void get_colorOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x0600065A RID: 1626
			[MethodImpl(4096)]
			private static extern void set_colorOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x0600065B RID: 1627
			[MethodImpl(4096)]
			private static extern bool get_generateLightingData_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600065C RID: 1628
			[MethodImpl(4096)]
			private static extern void set_generateLightingData_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x0600065D RID: 1629
			[MethodImpl(4096)]
			private static extern int get_ribbonCount_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600065E RID: 1630
			[MethodImpl(4096)]
			private static extern void set_ribbonCount_Injected(ref ParticleSystem.TrailModule _unity_self, int value);

			// Token: 0x0600065F RID: 1631
			[MethodImpl(4096)]
			private static extern float get_shadowBias_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000660 RID: 1632
			[MethodImpl(4096)]
			private static extern void set_shadowBias_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x06000661 RID: 1633
			[MethodImpl(4096)]
			private static extern bool get_splitSubEmitterRibbons_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000662 RID: 1634
			[MethodImpl(4096)]
			private static extern void set_splitSubEmitterRibbons_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x06000663 RID: 1635
			[MethodImpl(4096)]
			private static extern bool get_attachRibbonsToTransform_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000664 RID: 1636
			[MethodImpl(4096)]
			private static extern void set_attachRibbonsToTransform_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x04000076 RID: 118
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002C RID: 44
		public struct CustomDataModule
		{
			// Token: 0x06000665 RID: 1637 RVA: 0x00005911 File Offset: 0x00003B11
			internal CustomDataModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000666 RID: 1638 RVA: 0x0000591B File Offset: 0x00003B1B
			// (set) Token: 0x06000667 RID: 1639 RVA: 0x00005923 File Offset: 0x00003B23
			public bool enabled
			{
				get
				{
					return ParticleSystem.CustomDataModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CustomDataModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x0000592C File Offset: 0x00003B2C
			[NativeThrows]
			public void SetMode(ParticleSystemCustomData stream, ParticleSystemCustomDataMode mode)
			{
				ParticleSystem.CustomDataModule.SetMode_Injected(ref this, stream, mode);
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x00005936 File Offset: 0x00003B36
			[NativeThrows]
			public ParticleSystemCustomDataMode GetMode(ParticleSystemCustomData stream)
			{
				return ParticleSystem.CustomDataModule.GetMode_Injected(ref this, stream);
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x0000593F File Offset: 0x00003B3F
			[NativeThrows]
			public void SetVectorComponentCount(ParticleSystemCustomData stream, int count)
			{
				ParticleSystem.CustomDataModule.SetVectorComponentCount_Injected(ref this, stream, count);
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x00005949 File Offset: 0x00003B49
			[NativeThrows]
			public int GetVectorComponentCount(ParticleSystemCustomData stream)
			{
				return ParticleSystem.CustomDataModule.GetVectorComponentCount_Injected(ref this, stream);
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x00005952 File Offset: 0x00003B52
			[NativeThrows]
			public void SetVector(ParticleSystemCustomData stream, int component, ParticleSystem.MinMaxCurve curve)
			{
				ParticleSystem.CustomDataModule.SetVector_Injected(ref this, stream, component, ref curve);
			}

			// Token: 0x0600066D RID: 1645 RVA: 0x00005960 File Offset: 0x00003B60
			[NativeThrows]
			public ParticleSystem.MinMaxCurve GetVector(ParticleSystemCustomData stream, int component)
			{
				ParticleSystem.MinMaxCurve minMaxCurve;
				ParticleSystem.CustomDataModule.GetVector_Injected(ref this, stream, component, out minMaxCurve);
				return minMaxCurve;
			}

			// Token: 0x0600066E RID: 1646 RVA: 0x00005978 File Offset: 0x00003B78
			[NativeThrows]
			public void SetColor(ParticleSystemCustomData stream, ParticleSystem.MinMaxGradient gradient)
			{
				ParticleSystem.CustomDataModule.SetColor_Injected(ref this, stream, ref gradient);
			}

			// Token: 0x0600066F RID: 1647 RVA: 0x00005984 File Offset: 0x00003B84
			[NativeThrows]
			public ParticleSystem.MinMaxGradient GetColor(ParticleSystemCustomData stream)
			{
				ParticleSystem.MinMaxGradient minMaxGradient;
				ParticleSystem.CustomDataModule.GetColor_Injected(ref this, stream, out minMaxGradient);
				return minMaxGradient;
			}

			// Token: 0x06000670 RID: 1648
			[MethodImpl(4096)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.CustomDataModule _unity_self);

			// Token: 0x06000671 RID: 1649
			[MethodImpl(4096)]
			private static extern void set_enabled_Injected(ref ParticleSystem.CustomDataModule _unity_self, bool value);

			// Token: 0x06000672 RID: 1650
			[MethodImpl(4096)]
			private static extern void SetMode_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, ParticleSystemCustomDataMode mode);

			// Token: 0x06000673 RID: 1651
			[MethodImpl(4096)]
			private static extern ParticleSystemCustomDataMode GetMode_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream);

			// Token: 0x06000674 RID: 1652
			[MethodImpl(4096)]
			private static extern void SetVectorComponentCount_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int count);

			// Token: 0x06000675 RID: 1653
			[MethodImpl(4096)]
			private static extern int GetVectorComponentCount_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream);

			// Token: 0x06000676 RID: 1654
			[MethodImpl(4096)]
			private static extern void SetVector_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000677 RID: 1655
			[MethodImpl(4096)]
			private static extern void GetVector_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000678 RID: 1656
			[MethodImpl(4096)]
			private static extern void SetColor_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, ref ParticleSystem.MinMaxGradient gradient);

			// Token: 0x06000679 RID: 1657
			[MethodImpl(4096)]
			private static extern void GetColor_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x04000077 RID: 119
			internal ParticleSystem m_ParticleSystem;
		}
	}
}
