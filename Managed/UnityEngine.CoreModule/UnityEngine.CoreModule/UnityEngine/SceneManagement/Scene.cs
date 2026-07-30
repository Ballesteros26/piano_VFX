using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x0200026F RID: 623
	[NativeHeader("Runtime/Export/SceneManager/Scene.bindings.h")]
	[Serializable]
	public struct Scene
	{
		// Token: 0x060019F3 RID: 6643
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern bool IsValidInternal(int sceneHandle);

		// Token: 0x060019F4 RID: 6644
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern string GetPathInternal(int sceneHandle);

		// Token: 0x060019F5 RID: 6645
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern string GetNameInternal(int sceneHandle);

		// Token: 0x060019F6 RID: 6646
		[NativeThrows]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern void SetNameInternal(int sceneHandle, string name);

		// Token: 0x060019F7 RID: 6647
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern string GetGUIDInternal(int sceneHandle);

		// Token: 0x060019F8 RID: 6648
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern bool IsSubScene(int sceneHandle);

		// Token: 0x060019F9 RID: 6649
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern void SetIsSubScene(int sceneHandle, bool value);

		// Token: 0x060019FA RID: 6650
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern bool GetIsLoadedInternal(int sceneHandle);

		// Token: 0x060019FB RID: 6651
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern Scene.LoadingState GetLoadingStateInternal(int sceneHandle);

		// Token: 0x060019FC RID: 6652
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern bool GetIsDirtyInternal(int sceneHandle);

		// Token: 0x060019FD RID: 6653
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern int GetDirtyID(int sceneHandle);

		// Token: 0x060019FE RID: 6654
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern int GetBuildIndexInternal(int sceneHandle);

		// Token: 0x060019FF RID: 6655
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern int GetRootCountInternal(int sceneHandle);

		// Token: 0x06001A00 RID: 6656
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern void GetRootGameObjectsInternal(int sceneHandle, object resultRootList);

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0002A758 File Offset: 0x00028958
		public int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0002A770 File Offset: 0x00028970
		internal Scene.LoadingState loadingState
		{
			get
			{
				return Scene.GetLoadingStateInternal(this.handle);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0002A790 File Offset: 0x00028990
		internal string guid
		{
			get
			{
				return Scene.GetGUIDInternal(this.handle);
			}
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0002A7B0 File Offset: 0x000289B0
		public bool IsValid()
		{
			return Scene.IsValidInternal(this.handle);
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0002A7D0 File Offset: 0x000289D0
		public string path
		{
			get
			{
				return Scene.GetPathInternal(this.handle);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0002A7F0 File Offset: 0x000289F0
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x0002A80D File Offset: 0x00028A0D
		public string name
		{
			get
			{
				return Scene.GetNameInternal(this.handle);
			}
			set
			{
				Scene.SetNameInternal(this.handle, value);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0002A820 File Offset: 0x00028A20
		public bool isLoaded
		{
			get
			{
				return Scene.GetIsLoadedInternal(this.handle);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0002A840 File Offset: 0x00028A40
		public int buildIndex
		{
			get
			{
				return Scene.GetBuildIndexInternal(this.handle);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0002A860 File Offset: 0x00028A60
		public bool isDirty
		{
			get
			{
				return Scene.GetIsDirtyInternal(this.handle);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x0002A880 File Offset: 0x00028A80
		internal int dirtyID
		{
			get
			{
				return Scene.GetDirtyID(this.handle);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		public int rootCount
		{
			get
			{
				return Scene.GetRootCountInternal(this.handle);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0002A8C0 File Offset: 0x00028AC0
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x0002A8DD File Offset: 0x00028ADD
		public bool isSubScene
		{
			get
			{
				return Scene.IsSubScene(this.handle);
			}
			set
			{
				Scene.SetIsSubScene(this.handle, value);
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0002A8F0 File Offset: 0x00028AF0
		public GameObject[] GetRootGameObjects()
		{
			List<GameObject> list = new List<GameObject>(this.rootCount);
			this.GetRootGameObjects(list);
			return list.ToArray();
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0002A91C File Offset: 0x00028B1C
		public void GetRootGameObjects(List<GameObject> rootGameObjects)
		{
			bool flag = rootGameObjects.Capacity < this.rootCount;
			if (flag)
			{
				rootGameObjects.Capacity = this.rootCount;
			}
			rootGameObjects.Clear();
			bool flag2 = !this.IsValid();
			if (flag2)
			{
				throw new ArgumentException("The scene is invalid.");
			}
			bool flag3 = !Application.isPlaying && !this.isLoaded;
			if (flag3)
			{
				throw new ArgumentException("The scene is not loaded.");
			}
			bool flag4 = this.rootCount == 0;
			if (!flag4)
			{
				Scene.GetRootGameObjectsInternal(this.handle, rootGameObjects);
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		public static bool operator ==(Scene lhs, Scene rhs)
		{
			return lhs.handle == rhs.handle;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0002A9CC File Offset: 0x00028BCC
		public static bool operator !=(Scene lhs, Scene rhs)
		{
			return lhs.handle != rhs.handle;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0002A9F4 File Offset: 0x00028BF4
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0002AA0C File Offset: 0x00028C0C
		public override bool Equals(object other)
		{
			bool flag = !(other is Scene);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Scene scene = (Scene)other;
				flag2 = this.handle == scene.handle;
			}
			return flag2;
		}

		// Token: 0x040007FC RID: 2044
		[SerializeField]
		private int m_Handle;

		// Token: 0x02000270 RID: 624
		internal enum LoadingState
		{
			// Token: 0x040007FE RID: 2046
			NotLoaded,
			// Token: 0x040007FF RID: 2047
			Loading,
			// Token: 0x04000800 RID: 2048
			Loaded,
			// Token: 0x04000801 RID: 2049
			Unloading
		}
	}
}
