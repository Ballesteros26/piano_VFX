using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003C7 RID: 967
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Camera//Director/CameraPlayable.h")]
	[StaticAccessor("CameraPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Director/CameraPlayable.bindings.h")]
	[RequiredByNativeCode]
	public struct CameraPlayable : IPlayable, IEquatable<CameraPlayable>
	{
		// Token: 0x06002197 RID: 8599 RVA: 0x000390B8 File Offset: 0x000372B8
		public static CameraPlayable Create(PlayableGraph graph, Camera camera)
		{
			PlayableHandle playableHandle = CameraPlayable.CreateHandle(graph, camera);
			return new CameraPlayable(playableHandle);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000390D8 File Offset: 0x000372D8
		private static PlayableHandle CreateHandle(PlayableGraph graph, Camera camera)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !CameraPlayable.InternalCreateCameraPlayable(ref graph, camera, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0003910C File Offset: 0x0003730C
		internal CameraPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<CameraPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an CameraPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00039148 File Offset: 0x00037348
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x00039160 File Offset: 0x00037360
		public static implicit operator Playable(CameraPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x00039180 File Offset: 0x00037380
		public static explicit operator CameraPlayable(Playable playable)
		{
			return new CameraPlayable(playable.GetHandle());
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000391A0 File Offset: 0x000373A0
		public bool Equals(CameraPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000391C4 File Offset: 0x000373C4
		public Camera GetCamera()
		{
			return CameraPlayable.GetCameraInternal(ref this.m_Handle);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000391E1 File Offset: 0x000373E1
		public void SetCamera(Camera value)
		{
			CameraPlayable.SetCameraInternal(ref this.m_Handle, value);
		}

		// Token: 0x060021A0 RID: 8608
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern Camera GetCameraInternal(ref PlayableHandle hdl);

		// Token: 0x060021A1 RID: 8609
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetCameraInternal(ref PlayableHandle hdl, Camera camera);

		// Token: 0x060021A2 RID: 8610
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool InternalCreateCameraPlayable(ref PlayableGraph graph, Camera camera, ref PlayableHandle handle);

		// Token: 0x060021A3 RID: 8611
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x04000C43 RID: 3139
		private PlayableHandle m_Handle;
	}
}
