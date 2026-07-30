using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/Physics/BatchCommands/BoxcastCommand.h")]
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	public struct BoxcastCommand
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x00005C31 File Offset: 0x00003E31
		public BoxcastCommand(Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.direction = direction;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00005C6F File Offset: 0x00003E6F
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00005C77 File Offset: 0x00003E77
		public Vector3 center { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00005C80 File Offset: 0x00003E80
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00005C88 File Offset: 0x00003E88
		public Vector3 halfExtents { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00005C91 File Offset: 0x00003E91
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00005C99 File Offset: 0x00003E99
		public Quaternion orientation { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00005CA2 File Offset: 0x00003EA2
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x00005CAA File Offset: 0x00003EAA
		public Vector3 direction { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00005CB3 File Offset: 0x00003EB3
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00005CBB File Offset: 0x00003EBB
		public float distance { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00005CC4 File Offset: 0x00003EC4
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00005CCC File Offset: 0x00003ECC
		public int layerMask { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00005CD5 File Offset: 0x00003ED5
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x00005CDD File Offset: 0x00003EDD
		internal int maxHits { get; set; }

		// Token: 0x06000417 RID: 1047 RVA: 0x00005CE8 File Offset: 0x00003EE8
		public static JobHandle ScheduleBatch(NativeArray<BoxcastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<BoxcastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<BoxcastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<BoxcastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<BoxcastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return BoxcastCommand.ScheduleBoxcastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<BoxcastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00005D3C File Offset: 0x00003F3C
		[FreeFunction("ScheduleBoxcastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleBoxcastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle jobHandle;
			BoxcastCommand.ScheduleBoxcastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000419 RID: 1049
		[MethodImpl(4096)]
		private unsafe static extern void ScheduleBoxcastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
