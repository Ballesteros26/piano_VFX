using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Animations
{
	// Token: 0x02000043 RID: 67
	internal struct ProcessAnimationJobStruct<T> where T : struct, IAnimationJob
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x000045A8 File Offset: 0x000027A8
		public static IntPtr GetJobReflectionData()
		{
			bool flag = ProcessAnimationJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ProcessAnimationJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.Single, new ProcessAnimationJobStruct<T>.ExecuteJobFunction(ProcessAnimationJobStruct<T>.Execute), null, null);
			}
			return ProcessAnimationJobStruct<T>.jobReflectionData;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000045F8 File Offset: 0x000027F8
		public unsafe static void Execute(ref T data, IntPtr animationStreamPtr, IntPtr methodIndex, ref JobRanges ranges, int jobIndex)
		{
			AnimationStream animationStream;
			UnsafeUtility.CopyPtrToStructure<AnimationStream>((void*)animationStreamPtr, out animationStream);
			JobMethodIndex jobMethodIndex = (JobMethodIndex)methodIndex.ToInt32();
			JobMethodIndex jobMethodIndex2 = jobMethodIndex;
			if (jobMethodIndex2 != JobMethodIndex.ProcessRootMotionMethodIndex)
			{
				if (jobMethodIndex2 != JobMethodIndex.ProcessAnimationMethodIndex)
				{
					throw new NotImplementedException("Invalid Animation jobs method index.");
				}
				data.ProcessAnimation(animationStream);
			}
			else
			{
				data.ProcessRootMotion(animationStream);
			}
		}

		// Token: 0x0400013E RID: 318
		private static IntPtr jobReflectionData;

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x060002A3 RID: 675
		public delegate void ExecuteJobFunction(ref T data, IntPtr animationStreamPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);
	}
}
