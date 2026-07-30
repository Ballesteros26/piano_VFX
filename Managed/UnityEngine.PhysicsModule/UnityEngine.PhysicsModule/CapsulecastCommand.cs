using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	[NativeHeader("Modules/Physics/BatchCommands/CapsulecastCommand.h")]
	public struct CapsulecastCommand
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x00005B09 File Offset: 0x00003D09
		public CapsulecastCommand(Vector3 p1, Vector3 p2, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.point1 = p1;
			this.point2 = p2;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00005B47 File Offset: 0x00003D47
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00005B4F File Offset: 0x00003D4F
		public Vector3 point1 { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00005B58 File Offset: 0x00003D58
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00005B60 File Offset: 0x00003D60
		public Vector3 point2 { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00005B69 File Offset: 0x00003D69
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x00005B71 File Offset: 0x00003D71
		public float radius { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00005B7A File Offset: 0x00003D7A
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00005B82 File Offset: 0x00003D82
		public Vector3 direction { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00005B8B File Offset: 0x00003D8B
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x00005B93 File Offset: 0x00003D93
		public float distance { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00005B9C File Offset: 0x00003D9C
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public int layerMask { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00005BAD File Offset: 0x00003DAD
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00005BB5 File Offset: 0x00003DB5
		internal int maxHits { get; set; }

		// Token: 0x06000405 RID: 1029 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public static JobHandle ScheduleBatch(NativeArray<CapsulecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<CapsulecastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<CapsulecastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<CapsulecastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<CapsulecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return CapsulecastCommand.ScheduleCapsulecastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<CapsulecastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00005C14 File Offset: 0x00003E14
		[FreeFunction("ScheduleCapsulecastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleCapsulecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle jobHandle;
			CapsulecastCommand.ScheduleCapsulecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000407 RID: 1031
		[MethodImpl(4096)]
		private unsafe static extern void ScheduleCapsulecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
