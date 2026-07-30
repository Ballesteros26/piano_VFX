using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000B6 RID: 182
	[NativeHeader("Runtime/Export/Camera/CullingGroup.bindings.h")]
	[StructLayout(0)]
	public class CullingGroup : IDisposable
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x000062C3 File Offset: 0x000044C3
		public CullingGroup()
		{
			this.m_Ptr = CullingGroup.Init(this);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000062E0 File Offset: 0x000044E0
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					this.FinalizerFailure();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060003F6 RID: 1014
		[FreeFunction("CullingGroup_Bindings::Dispose", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void DisposeInternal();

		// Token: 0x060003F7 RID: 1015 RVA: 0x00006328 File Offset: 0x00004528
		public void Dispose()
		{
			this.DisposeInternal();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00006340 File Offset: 0x00004540
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00006358 File Offset: 0x00004558
		public CullingGroup.StateChanged onStateChanged
		{
			get
			{
				return this.m_OnStateChanged;
			}
			set
			{
				this.m_OnStateChanged = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003FA RID: 1018
		// (set) Token: 0x060003FB RID: 1019
		public extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003FC RID: 1020
		// (set) Token: 0x060003FD RID: 1021
		public extern Camera targetCamera
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060003FE RID: 1022
		[MethodImpl(4096)]
		public extern void SetBoundingSpheres(BoundingSphere[] array);

		// Token: 0x060003FF RID: 1023
		[MethodImpl(4096)]
		public extern void SetBoundingSphereCount(int count);

		// Token: 0x06000400 RID: 1024
		[MethodImpl(4096)]
		public extern void EraseSwapBack(int index);

		// Token: 0x06000401 RID: 1025 RVA: 0x00006362 File Offset: 0x00004562
		public static void EraseSwapBack<T>(int index, T[] myArray, ref int size)
		{
			size--;
			myArray[index] = myArray[size];
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000637C File Offset: 0x0000457C
		public int QueryIndices(bool visible, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, -1, CullingQueryOptions.IgnoreDistance, result, firstIndex);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000639C File Offset: 0x0000459C
		public int QueryIndices(int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(false, distanceIndex, CullingQueryOptions.IgnoreVisibility, result, firstIndex);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000063BC File Offset: 0x000045BC
		public int QueryIndices(bool visible, int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, distanceIndex, CullingQueryOptions.Normal, result, firstIndex);
		}

		// Token: 0x06000405 RID: 1029
		[FreeFunction("CullingGroup_Bindings::QueryIndices", HasExplicitThis = true)]
		[NativeThrows]
		[MethodImpl(4096)]
		private extern int QueryIndices(bool visible, int distanceIndex, CullingQueryOptions options, int[] result, int firstIndex);

		// Token: 0x06000406 RID: 1030
		[NativeThrows]
		[FreeFunction("CullingGroup_Bindings::IsVisible", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsVisible(int index);

		// Token: 0x06000407 RID: 1031
		[FreeFunction("CullingGroup_Bindings::GetDistance", HasExplicitThis = true)]
		[NativeThrows]
		[MethodImpl(4096)]
		public extern int GetDistance(int index);

		// Token: 0x06000408 RID: 1032
		[FreeFunction("CullingGroup_Bindings::SetBoundingDistances", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetBoundingDistances(float[] distances);

		// Token: 0x06000409 RID: 1033 RVA: 0x000063DA File Offset: 0x000045DA
		[FreeFunction("CullingGroup_Bindings::SetDistanceReferencePoint", HasExplicitThis = true)]
		private void SetDistanceReferencePoint_InternalVector3(Vector3 point)
		{
			this.SetDistanceReferencePoint_InternalVector3_Injected(ref point);
		}

		// Token: 0x0600040A RID: 1034
		[NativeMethod("SetDistanceReferenceTransform")]
		[MethodImpl(4096)]
		private extern void SetDistanceReferencePoint_InternalTransform(Transform transform);

		// Token: 0x0600040B RID: 1035 RVA: 0x000063E4 File Offset: 0x000045E4
		public void SetDistanceReferencePoint(Vector3 point)
		{
			this.SetDistanceReferencePoint_InternalVector3(point);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000063EF File Offset: 0x000045EF
		public void SetDistanceReferencePoint(Transform transform)
		{
			this.SetDistanceReferencePoint_InternalTransform(transform);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000063FC File Offset: 0x000045FC
		[RequiredByNativeCode]
		[SecuritySafeCritical]
		private unsafe static void SendEvents(CullingGroup cullingGroup, IntPtr eventsPtr, int count)
		{
			CullingGroupEvent* ptr = (CullingGroupEvent*)eventsPtr.ToPointer();
			bool flag = cullingGroup.m_OnStateChanged == null;
			if (!flag)
			{
				for (int i = 0; i < count; i++)
				{
					cullingGroup.m_OnStateChanged(ptr[i]);
				}
			}
		}

		// Token: 0x0600040E RID: 1038
		[FreeFunction("CullingGroup_Bindings::Init")]
		[MethodImpl(4096)]
		private static extern IntPtr Init(object scripting);

		// Token: 0x0600040F RID: 1039
		[FreeFunction("CullingGroup_Bindings::FinalizerFailure", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void FinalizerFailure();

		// Token: 0x06000410 RID: 1040
		[MethodImpl(4096)]
		private extern void SetDistanceReferencePoint_InternalVector3_Injected(ref Vector3 point);

		// Token: 0x0400021C RID: 540
		internal IntPtr m_Ptr;

		// Token: 0x0400021D RID: 541
		private CullingGroup.StateChanged m_OnStateChanged = null;

		// Token: 0x020000B7 RID: 183
		// (Invoke) Token: 0x06000412 RID: 1042
		public delegate void StateChanged(CullingGroupEvent sphere);
	}
}
