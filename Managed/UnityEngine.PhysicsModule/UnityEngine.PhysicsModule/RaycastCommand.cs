using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	[NativeHeader("Modules/Physics/BatchCommands/RaycastCommand.h")]
	public struct RaycastCommand
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x00005909 File Offset: 0x00003B09
		public RaycastCommand(Vector3 from, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5, int maxHits = 1)
		{
			this.from = from;
			this.direction = direction;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = maxHits;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00005936 File Offset: 0x00003B36
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000593E File Offset: 0x00003B3E
		public Vector3 from { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00005947 File Offset: 0x00003B47
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000594F File Offset: 0x00003B4F
		public Vector3 direction { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00005958 File Offset: 0x00003B58
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00005960 File Offset: 0x00003B60
		public float distance { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00005969 File Offset: 0x00003B69
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00005971 File Offset: 0x00003B71
		public int layerMask { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000597A File Offset: 0x00003B7A
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00005982 File Offset: 0x00003B82
		public int maxHits { get; set; }

		// Token: 0x060003E3 RID: 995 RVA: 0x0000598C File Offset: 0x00003B8C
		public static JobHandle ScheduleBatch(NativeArray<RaycastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<RaycastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<RaycastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<RaycastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<RaycastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return RaycastCommand.ScheduleRaycastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000059E0 File Offset: 0x00003BE0
		[FreeFunction("ScheduleRaycastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleRaycastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle jobHandle;
			RaycastCommand.ScheduleRaycastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060003E5 RID: 997
		[MethodImpl(4096)]
		private unsafe static extern void ScheduleRaycastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
