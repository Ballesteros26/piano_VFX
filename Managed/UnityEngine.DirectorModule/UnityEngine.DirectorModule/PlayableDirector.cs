using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000002 RID: 2
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[NativeHeader("Modules/Director/PlayableDirector.h")]
	public class PlayableDirector : Behaviour, IExposedPropertyTable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public PlayState state
		{
			get
			{
				return this.GetPlayState();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public DirectorWrapMode extrapolationMode
		{
			get
			{
				return this.GetWrapMode();
			}
			set
			{
				this.SetWrapMode(value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020A9 File Offset: 0x000002A9
		public PlayableAsset playableAsset
		{
			get
			{
				return this.Internal_GetPlayableAsset() as PlayableAsset;
			}
			set
			{
				this.SetPlayableAsset(value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020B4 File Offset: 0x000002B4
		public PlayableGraph playableGraph
		{
			get
			{
				return this.GetGraphHandle();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020CC File Offset: 0x000002CC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020E4 File Offset: 0x000002E4
		public bool playOnAwake
		{
			get
			{
				return this.GetPlayOnAwake();
			}
			set
			{
				this.SetPlayOnAwake(value);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020EF File Offset: 0x000002EF
		public void DeferredEvaluate()
		{
			this.EvaluateNextFrame();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020FC File Offset: 0x000002FC
		public void Play(PlayableAsset asset)
		{
			bool flag = asset == null;
			if (flag)
			{
				throw new ArgumentNullException("asset");
			}
			this.Play(asset, this.extrapolationMode);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002130 File Offset: 0x00000330
		public void Play(PlayableAsset asset, DirectorWrapMode mode)
		{
			bool flag = asset == null;
			if (flag)
			{
				throw new ArgumentNullException("asset");
			}
			this.playableAsset = asset;
			this.extrapolationMode = mode;
			this.Play();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000216B File Offset: 0x0000036B
		public void SetGenericBinding(Object key, Object value)
		{
			this.Internal_SetGenericBinding(key, value);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14
		// (set) Token: 0x0600000D RID: 13
		public extern DirectorUpdateMode timeUpdateMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16
		// (set) Token: 0x0600000F RID: 15
		public extern double time
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18
		// (set) Token: 0x06000011 RID: 17
		public extern double initialTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19
		public extern double duration
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000014 RID: 20
		[NativeThrows]
		[MethodImpl(4096)]
		public extern void Evaluate();

		// Token: 0x06000015 RID: 21
		[NativeThrows]
		[MethodImpl(4096)]
		public extern void Play();

		// Token: 0x06000016 RID: 22
		[MethodImpl(4096)]
		public extern void Stop();

		// Token: 0x06000017 RID: 23
		[MethodImpl(4096)]
		public extern void Pause();

		// Token: 0x06000018 RID: 24
		[MethodImpl(4096)]
		public extern void Resume();

		// Token: 0x06000019 RID: 25
		[NativeThrows]
		[MethodImpl(4096)]
		public extern void RebuildGraph();

		// Token: 0x0600001A RID: 26 RVA: 0x00002177 File Offset: 0x00000377
		public void ClearReferenceValue(PropertyName id)
		{
			this.ClearReferenceValue_Injected(ref id);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002181 File Offset: 0x00000381
		public void SetReferenceValue(PropertyName id, Object value)
		{
			this.SetReferenceValue_Injected(ref id, value);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000218C File Offset: 0x0000038C
		public Object GetReferenceValue(PropertyName id, out bool idValid)
		{
			return this.GetReferenceValue_Injected(ref id, out idValid);
		}

		// Token: 0x0600001D RID: 29
		[NativeMethod("GetBindingFor")]
		[MethodImpl(4096)]
		public extern Object GetGenericBinding(Object key);

		// Token: 0x0600001E RID: 30
		[NativeMethod("ClearBindingFor")]
		[MethodImpl(4096)]
		public extern void ClearGenericBinding(Object key);

		// Token: 0x0600001F RID: 31
		[NativeThrows]
		[MethodImpl(4096)]
		public extern void RebindPlayableGraphOutputs();

		// Token: 0x06000020 RID: 32
		[MethodImpl(4096)]
		internal extern void ProcessPendingGraphChanges();

		// Token: 0x06000021 RID: 33
		[NativeMethod("HasBinding")]
		[MethodImpl(4096)]
		internal extern bool HasGenericBinding(Object key);

		// Token: 0x06000022 RID: 34
		[MethodImpl(4096)]
		private extern PlayState GetPlayState();

		// Token: 0x06000023 RID: 35
		[MethodImpl(4096)]
		private extern void SetWrapMode(DirectorWrapMode mode);

		// Token: 0x06000024 RID: 36
		[MethodImpl(4096)]
		private extern DirectorWrapMode GetWrapMode();

		// Token: 0x06000025 RID: 37
		[NativeThrows]
		[MethodImpl(4096)]
		private extern void EvaluateNextFrame();

		// Token: 0x06000026 RID: 38 RVA: 0x00002198 File Offset: 0x00000398
		private PlayableGraph GetGraphHandle()
		{
			PlayableGraph playableGraph;
			this.GetGraphHandle_Injected(out playableGraph);
			return playableGraph;
		}

		// Token: 0x06000027 RID: 39
		[MethodImpl(4096)]
		private extern void SetPlayOnAwake(bool on);

		// Token: 0x06000028 RID: 40
		[MethodImpl(4096)]
		private extern bool GetPlayOnAwake();

		// Token: 0x06000029 RID: 41
		[NativeThrows]
		[MethodImpl(4096)]
		private extern void Internal_SetGenericBinding(Object key, Object value);

		// Token: 0x0600002A RID: 42
		[MethodImpl(4096)]
		private extern void SetPlayableAsset(ScriptableObject asset);

		// Token: 0x0600002B RID: 43
		[MethodImpl(4096)]
		private extern ScriptableObject Internal_GetPlayableAsset();

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600002C RID: 44 RVA: 0x000021B0 File Offset: 0x000003B0
		// (remove) Token: 0x0600002D RID: 45 RVA: 0x000021E8 File Offset: 0x000003E8
		[field: DebuggerBrowsable(0)]
		public event Action<PlayableDirector> played;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600002E RID: 46 RVA: 0x00002220 File Offset: 0x00000420
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x00002258 File Offset: 0x00000458
		[field: DebuggerBrowsable(0)]
		public event Action<PlayableDirector> paused;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000030 RID: 48 RVA: 0x00002290 File Offset: 0x00000490
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x000022C8 File Offset: 0x000004C8
		[field: DebuggerBrowsable(0)]
		public event Action<PlayableDirector> stopped;

		// Token: 0x06000032 RID: 50
		[NativeHeader("Runtime/Director/Core/DirectorManager.h")]
		[StaticAccessor("GetDirectorManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		internal static extern void ResetFrameTiming();

		// Token: 0x06000033 RID: 51 RVA: 0x00002300 File Offset: 0x00000500
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPlay()
		{
			bool flag = this.played != null;
			if (flag)
			{
				this.played.Invoke(this);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002328 File Offset: 0x00000528
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPause()
		{
			bool flag = this.paused != null;
			if (flag)
			{
				this.paused.Invoke(this);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002350 File Offset: 0x00000550
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorStop()
		{
			bool flag = this.stopped != null;
			if (flag)
			{
				this.stopped.Invoke(this);
			}
		}

		// Token: 0x06000037 RID: 55
		[MethodImpl(4096)]
		private extern void ClearReferenceValue_Injected(ref PropertyName id);

		// Token: 0x06000038 RID: 56
		[MethodImpl(4096)]
		private extern void SetReferenceValue_Injected(ref PropertyName id, Object value);

		// Token: 0x06000039 RID: 57
		[MethodImpl(4096)]
		private extern Object GetReferenceValue_Injected(ref PropertyName id, out bool idValid);

		// Token: 0x0600003A RID: 58
		[MethodImpl(4096)]
		private extern void GetGraphHandle_Injected(out PlayableGraph ret);
	}
}
