using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[NativeHeader("Modules/Animation/HumanPoseHandler.h")]
	public class HumanPoseHandler : IDisposable
	{
		// Token: 0x06000251 RID: 593
		[FreeFunction("AnimationBindings::CreateHumanPoseHandler")]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_CreateFromRoot(Avatar avatar, Transform root);

		// Token: 0x06000252 RID: 594
		[FreeFunction("AnimationBindings::CreateHumanPoseHandler", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_CreateFromJointPaths(Avatar avatar, string[] jointPaths);

		// Token: 0x06000253 RID: 595
		[FreeFunction("AnimationBindings::DestroyHumanPoseHandler")]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06000254 RID: 596
		[MethodImpl(4096)]
		private extern void GetHumanPose(out Vector3 bodyPosition, out Quaternion bodyRotation, [Out] float[] muscles);

		// Token: 0x06000255 RID: 597
		[MethodImpl(4096)]
		private extern void SetHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles);

		// Token: 0x06000256 RID: 598
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern void GetInternalHumanPose(out Vector3 bodyPosition, out Quaternion bodyRotation, [Out] float[] muscles);

		// Token: 0x06000257 RID: 599
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern void SetInternalHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles);

		// Token: 0x06000258 RID: 600
		[ThreadSafe]
		[MethodImpl(4096)]
		private unsafe extern void GetInternalAvatarPose(void* avatarPose, int avatarPoseLength);

		// Token: 0x06000259 RID: 601
		[ThreadSafe]
		[MethodImpl(4096)]
		private unsafe extern void SetInternalAvatarPose(void* avatarPose, int avatarPoseLength);

		// Token: 0x0600025A RID: 602 RVA: 0x00003F2C File Offset: 0x0000212C
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				HumanPoseHandler.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00003F70 File Offset: 0x00002170
		public HumanPoseHandler(Avatar avatar, Transform root)
		{
			this.m_Ptr = IntPtr.Zero;
			bool flag = root == null;
			if (flag)
			{
				throw new ArgumentNullException("HumanPoseHandler root Transform is null");
			}
			bool flag2 = avatar == null;
			if (flag2)
			{
				throw new ArgumentNullException("HumanPoseHandler avatar is null");
			}
			bool flag3 = !avatar.isValid;
			if (flag3)
			{
				throw new ArgumentException("HumanPoseHandler avatar is invalid");
			}
			bool flag4 = !avatar.isHuman;
			if (flag4)
			{
				throw new ArgumentException("HumanPoseHandler avatar is not human");
			}
			this.m_Ptr = HumanPoseHandler.Internal_CreateFromRoot(avatar, root);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00003FFC File Offset: 0x000021FC
		public HumanPoseHandler(Avatar avatar, string[] jointPaths)
		{
			this.m_Ptr = IntPtr.Zero;
			bool flag = jointPaths == null;
			if (flag)
			{
				throw new ArgumentNullException("HumanPoseHandler jointPaths array is null");
			}
			bool flag2 = avatar == null;
			if (flag2)
			{
				throw new ArgumentNullException("HumanPoseHandler avatar is null");
			}
			bool flag3 = !avatar.isValid;
			if (flag3)
			{
				throw new ArgumentException("HumanPoseHandler avatar is invalid");
			}
			bool flag4 = !avatar.isHuman;
			if (flag4)
			{
				throw new ArgumentException("HumanPoseHandler avatar is not human");
			}
			this.m_Ptr = HumanPoseHandler.Internal_CreateFromJointPaths(avatar, jointPaths);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00004084 File Offset: 0x00002284
		public void GetHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.GetHumanPose(out humanPose.bodyPosition, out humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000040D4 File Offset: 0x000022D4
		public void SetHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.SetHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00004124 File Offset: 0x00002324
		public void GetInternalHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.GetInternalHumanPose(out humanPose.bodyPosition, out humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00004174 File Offset: 0x00002374
		public void SetInternalHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.SetInternalHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000041C4 File Offset: 0x000023C4
		public void GetInternalAvatarPose(NativeArray<float> avatarPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			this.GetInternalAvatarPose(avatarPose.GetUnsafePtr<float>(), avatarPose.Length);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00004208 File Offset: 0x00002408
		public void SetInternalAvatarPose(NativeArray<float> avatarPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			this.SetInternalAvatarPose(avatarPose.GetUnsafeReadOnlyPtr<float>(), avatarPose.Length);
		}

		// Token: 0x04000135 RID: 309
		internal IntPtr m_Ptr;
	}
}
