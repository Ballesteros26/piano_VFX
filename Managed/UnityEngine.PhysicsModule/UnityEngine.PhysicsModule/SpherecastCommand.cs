using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	[NativeHeader("Modules/Physics/BatchCommands/SpherecastCommand.h")]
	public struct SpherecastCommand
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x000059FD File Offset: 0x00003BFD
		public SpherecastCommand(Vector3 origin, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.origin = origin;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00005A32 File Offset: 0x00003C32
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00005A3A File Offset: 0x00003C3A
		public Vector3 origin { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00005A43 File Offset: 0x00003C43
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00005A4B File Offset: 0x00003C4B
		public float radius { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00005A54 File Offset: 0x00003C54
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00005A5C File Offset: 0x00003C5C
		public Vector3 direction { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00005A65 File Offset: 0x00003C65
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00005A6D File Offset: 0x00003C6D
		public float distance { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00005A76 File Offset: 0x00003C76
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00005A7E File Offset: 0x00003C7E
		public int layerMask { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00005A87 File Offset: 0x00003C87
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00005A8F File Offset: 0x00003C8F
		internal int maxHits { get; set; }

		// Token: 0x060003F3 RID: 1011 RVA: 0x00005A98 File Offset: 0x00003C98
		public static JobHandle ScheduleBatch(NativeArray<SpherecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<SpherecastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<SpherecastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<SpherecastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<SpherecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return SpherecastCommand.ScheduleSpherecastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<SpherecastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00005AEC File Offset: 0x00003CEC
		[FreeFunction("ScheduleSpherecastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleSpherecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle jobHandle;
			SpherecastCommand.ScheduleSpherecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060003F5 RID: 1013
		[MethodImpl(4096)]
		private unsafe static extern void ScheduleSpherecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
