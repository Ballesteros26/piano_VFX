using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x020003A6 RID: 934
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Director/PlayableGraph.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	public struct PlayableGraph
	{
		// Token: 0x06002059 RID: 8281 RVA: 0x00036F7C File Offset: 0x0003517C
		public Playable GetRootPlayable(int index)
		{
			PlayableHandle rootPlayableInternal = this.GetRootPlayableInternal(index);
			return new Playable(rootPlayableInternal);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00036F9C File Offset: 0x0003519C
		public bool Connect<U, V>(U source, int sourceOutputPort, V destination, int destinationInputPort) where U : struct, IPlayable where V : struct, IPlayable
		{
			return this.ConnectInternal(source.GetHandle(), sourceOutputPort, destination.GetHandle(), destinationInputPort);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00036FD1 File Offset: 0x000351D1
		public void Disconnect<U>(U input, int inputPort) where U : struct, IPlayable
		{
			this.DisconnectInternal(input.GetHandle(), inputPort);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x00036FE9 File Offset: 0x000351E9
		public void DestroyPlayable<U>(U playable) where U : struct, IPlayable
		{
			this.DestroyPlayableInternal(playable.GetHandle());
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00037000 File Offset: 0x00035200
		public void DestroySubgraph<U>(U playable) where U : struct, IPlayable
		{
			this.DestroySubgraphInternal(playable.GetHandle());
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00037017 File Offset: 0x00035217
		public void DestroyOutput<U>(U output) where U : struct, IPlayableOutput
		{
			this.DestroyOutputInternal(output.GetHandle());
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00037030 File Offset: 0x00035230
		public int GetOutputCountByType<T>() where T : struct, IPlayableOutput
		{
			return this.GetOutputCountByTypeInternal(typeof(T));
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00037054 File Offset: 0x00035254
		public PlayableOutput GetOutput(int index)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !this.GetOutputInternal(index, out playableOutputHandle);
			PlayableOutput playableOutput;
			if (flag)
			{
				playableOutput = PlayableOutput.Null;
			}
			else
			{
				playableOutput = new PlayableOutput(playableOutputHandle);
			}
			return playableOutput;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00037084 File Offset: 0x00035284
		public PlayableOutput GetOutputByType<T>(int index) where T : struct, IPlayableOutput
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !this.GetOutputByTypeInternal(typeof(T), index, out playableOutputHandle);
			PlayableOutput playableOutput;
			if (flag)
			{
				playableOutput = PlayableOutput.Null;
			}
			else
			{
				playableOutput = new PlayableOutput(playableOutputHandle);
			}
			return playableOutput;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000370BE File Offset: 0x000352BE
		public void Evaluate()
		{
			this.Evaluate(0f);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000370D0 File Offset: 0x000352D0
		public static PlayableGraph Create()
		{
			return PlayableGraph.Create(null);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000370E8 File Offset: 0x000352E8
		public static PlayableGraph Create(string name)
		{
			PlayableGraph playableGraph;
			PlayableGraph.Create_Injected(name, out playableGraph);
			return playableGraph;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000370FE File Offset: 0x000352FE
		[FreeFunction("PlayableGraphBindings::Destroy", HasExplicitThis = true, ThrowsException = true)]
		public void Destroy()
		{
			PlayableGraph.Destroy_Injected(ref this);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00037106 File Offset: 0x00035306
		public bool IsValid()
		{
			return PlayableGraph.IsValid_Injected(ref this);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x0003710E File Offset: 0x0003530E
		[FreeFunction("PlayableGraphBindings::IsPlaying", HasExplicitThis = true, ThrowsException = true)]
		public bool IsPlaying()
		{
			return PlayableGraph.IsPlaying_Injected(ref this);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00037116 File Offset: 0x00035316
		[FreeFunction("PlayableGraphBindings::IsDone", HasExplicitThis = true, ThrowsException = true)]
		public bool IsDone()
		{
			return PlayableGraph.IsDone_Injected(ref this);
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0003711E File Offset: 0x0003531E
		[FreeFunction("PlayableGraphBindings::Play", HasExplicitThis = true, ThrowsException = true)]
		public void Play()
		{
			PlayableGraph.Play_Injected(ref this);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00037126 File Offset: 0x00035326
		[FreeFunction("PlayableGraphBindings::Stop", HasExplicitThis = true, ThrowsException = true)]
		public void Stop()
		{
			PlayableGraph.Stop_Injected(ref this);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0003712E File Offset: 0x0003532E
		[FreeFunction("PlayableGraphBindings::Evaluate", HasExplicitThis = true, ThrowsException = true)]
		public void Evaluate([DefaultValue("0")] float deltaTime)
		{
			PlayableGraph.Evaluate_Injected(ref this, deltaTime);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00037137 File Offset: 0x00035337
		[FreeFunction("PlayableGraphBindings::GetTimeUpdateMode", HasExplicitThis = true, ThrowsException = true)]
		public DirectorUpdateMode GetTimeUpdateMode()
		{
			return PlayableGraph.GetTimeUpdateMode_Injected(ref this);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0003713F File Offset: 0x0003533F
		[FreeFunction("PlayableGraphBindings::SetTimeUpdateMode", HasExplicitThis = true, ThrowsException = true)]
		public void SetTimeUpdateMode(DirectorUpdateMode value)
		{
			PlayableGraph.SetTimeUpdateMode_Injected(ref this, value);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00037148 File Offset: 0x00035348
		[FreeFunction("PlayableGraphBindings::GetResolver", HasExplicitThis = true, ThrowsException = true)]
		public IExposedPropertyTable GetResolver()
		{
			return PlayableGraph.GetResolver_Injected(ref this);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00037150 File Offset: 0x00035350
		[FreeFunction("PlayableGraphBindings::SetResolver", HasExplicitThis = true, ThrowsException = true)]
		public void SetResolver(IExposedPropertyTable value)
		{
			PlayableGraph.SetResolver_Injected(ref this, value);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00037159 File Offset: 0x00035359
		[FreeFunction("PlayableGraphBindings::GetPlayableCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetPlayableCount()
		{
			return PlayableGraph.GetPlayableCount_Injected(ref this);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00037161 File Offset: 0x00035361
		[FreeFunction("PlayableGraphBindings::GetRootPlayableCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetRootPlayableCount()
		{
			return PlayableGraph.GetRootPlayableCount_Injected(ref this);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00037169 File Offset: 0x00035369
		[FreeFunction("PlayableGraphBindings::GetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetOutputCount()
		{
			return PlayableGraph.GetOutputCount_Injected(ref this);
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00037174 File Offset: 0x00035374
		[FreeFunction("PlayableGraphBindings::CreatePlayableHandle", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle CreatePlayableHandle()
		{
			PlayableHandle playableHandle;
			PlayableGraph.CreatePlayableHandle_Injected(ref this, out playableHandle);
			return playableHandle;
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x0003718A File Offset: 0x0003538A
		[FreeFunction("PlayableGraphBindings::CreateScriptOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		internal bool CreateScriptOutputInternal(string name, out PlayableOutputHandle handle)
		{
			return PlayableGraph.CreateScriptOutputInternal_Injected(ref this, name, out handle);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00037194 File Offset: 0x00035394
		[FreeFunction("PlayableGraphBindings::GetRootPlayableInternal", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle GetRootPlayableInternal(int index)
		{
			PlayableHandle playableHandle;
			PlayableGraph.GetRootPlayableInternal_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000371AB File Offset: 0x000353AB
		[FreeFunction("PlayableGraphBindings::DestroyOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		internal void DestroyOutputInternal(PlayableOutputHandle handle)
		{
			PlayableGraph.DestroyOutputInternal_Injected(ref this, ref handle);
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000371B5 File Offset: 0x000353B5
		[FreeFunction("PlayableGraphBindings::GetOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool GetOutputInternal(int index, out PlayableOutputHandle handle)
		{
			return PlayableGraph.GetOutputInternal_Injected(ref this, index, out handle);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000371BF File Offset: 0x000353BF
		[FreeFunction("PlayableGraphBindings::GetOutputCountByTypeInternal", HasExplicitThis = true, ThrowsException = true)]
		private int GetOutputCountByTypeInternal(Type outputType)
		{
			return PlayableGraph.GetOutputCountByTypeInternal_Injected(ref this, outputType);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000371C8 File Offset: 0x000353C8
		[FreeFunction("PlayableGraphBindings::GetOutputByTypeInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool GetOutputByTypeInternal(Type outputType, int index, out PlayableOutputHandle handle)
		{
			return PlayableGraph.GetOutputByTypeInternal_Injected(ref this, outputType, index, out handle);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000371D3 File Offset: 0x000353D3
		[FreeFunction("PlayableGraphBindings::ConnectInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool ConnectInternal(PlayableHandle source, int sourceOutputPort, PlayableHandle destination, int destinationInputPort)
		{
			return PlayableGraph.ConnectInternal_Injected(ref this, ref source, sourceOutputPort, ref destination, destinationInputPort);
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000371E2 File Offset: 0x000353E2
		[FreeFunction("PlayableGraphBindings::DisconnectInternal", HasExplicitThis = true, ThrowsException = true)]
		private void DisconnectInternal(PlayableHandle playable, int inputPort)
		{
			PlayableGraph.DisconnectInternal_Injected(ref this, ref playable, inputPort);
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000371ED File Offset: 0x000353ED
		[FreeFunction("PlayableGraphBindings::DestroyPlayableInternal", HasExplicitThis = true, ThrowsException = true)]
		private void DestroyPlayableInternal(PlayableHandle playable)
		{
			PlayableGraph.DestroyPlayableInternal_Injected(ref this, ref playable);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000371F7 File Offset: 0x000353F7
		[FreeFunction("PlayableGraphBindings::DestroySubgraphInternal", HasExplicitThis = true, ThrowsException = true)]
		private void DestroySubgraphInternal(PlayableHandle playable)
		{
			PlayableGraph.DestroySubgraphInternal_Injected(ref this, ref playable);
		}

		// Token: 0x0600207E RID: 8318
		[MethodImpl(4096)]
		private static extern void Create_Injected(string name, out PlayableGraph ret);

		// Token: 0x0600207F RID: 8319
		[MethodImpl(4096)]
		private static extern void Destroy_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002080 RID: 8320
		[MethodImpl(4096)]
		private static extern bool IsValid_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002081 RID: 8321
		[MethodImpl(4096)]
		private static extern bool IsPlaying_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002082 RID: 8322
		[MethodImpl(4096)]
		private static extern bool IsDone_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002083 RID: 8323
		[MethodImpl(4096)]
		private static extern void Play_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002084 RID: 8324
		[MethodImpl(4096)]
		private static extern void Stop_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002085 RID: 8325
		[MethodImpl(4096)]
		private static extern void Evaluate_Injected(ref PlayableGraph _unity_self, [DefaultValue("0")] float deltaTime);

		// Token: 0x06002086 RID: 8326
		[MethodImpl(4096)]
		private static extern DirectorUpdateMode GetTimeUpdateMode_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002087 RID: 8327
		[MethodImpl(4096)]
		private static extern void SetTimeUpdateMode_Injected(ref PlayableGraph _unity_self, DirectorUpdateMode value);

		// Token: 0x06002088 RID: 8328
		[MethodImpl(4096)]
		private static extern IExposedPropertyTable GetResolver_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002089 RID: 8329
		[MethodImpl(4096)]
		private static extern void SetResolver_Injected(ref PlayableGraph _unity_self, IExposedPropertyTable value);

		// Token: 0x0600208A RID: 8330
		[MethodImpl(4096)]
		private static extern int GetPlayableCount_Injected(ref PlayableGraph _unity_self);

		// Token: 0x0600208B RID: 8331
		[MethodImpl(4096)]
		private static extern int GetRootPlayableCount_Injected(ref PlayableGraph _unity_self);

		// Token: 0x0600208C RID: 8332
		[MethodImpl(4096)]
		private static extern int GetOutputCount_Injected(ref PlayableGraph _unity_self);

		// Token: 0x0600208D RID: 8333
		[MethodImpl(4096)]
		private static extern void CreatePlayableHandle_Injected(ref PlayableGraph _unity_self, out PlayableHandle ret);

		// Token: 0x0600208E RID: 8334
		[MethodImpl(4096)]
		private static extern bool CreateScriptOutputInternal_Injected(ref PlayableGraph _unity_self, string name, out PlayableOutputHandle handle);

		// Token: 0x0600208F RID: 8335
		[MethodImpl(4096)]
		private static extern void GetRootPlayableInternal_Injected(ref PlayableGraph _unity_self, int index, out PlayableHandle ret);

		// Token: 0x06002090 RID: 8336
		[MethodImpl(4096)]
		private static extern void DestroyOutputInternal_Injected(ref PlayableGraph _unity_self, ref PlayableOutputHandle handle);

		// Token: 0x06002091 RID: 8337
		[MethodImpl(4096)]
		private static extern bool GetOutputInternal_Injected(ref PlayableGraph _unity_self, int index, out PlayableOutputHandle handle);

		// Token: 0x06002092 RID: 8338
		[MethodImpl(4096)]
		private static extern int GetOutputCountByTypeInternal_Injected(ref PlayableGraph _unity_self, Type outputType);

		// Token: 0x06002093 RID: 8339
		[MethodImpl(4096)]
		private static extern bool GetOutputByTypeInternal_Injected(ref PlayableGraph _unity_self, Type outputType, int index, out PlayableOutputHandle handle);

		// Token: 0x06002094 RID: 8340
		[MethodImpl(4096)]
		private static extern bool ConnectInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle source, int sourceOutputPort, ref PlayableHandle destination, int destinationInputPort);

		// Token: 0x06002095 RID: 8341
		[MethodImpl(4096)]
		private static extern void DisconnectInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle playable, int inputPort);

		// Token: 0x06002096 RID: 8342
		[MethodImpl(4096)]
		private static extern void DestroyPlayableInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle playable);

		// Token: 0x06002097 RID: 8343
		[MethodImpl(4096)]
		private static extern void DestroySubgraphInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle playable);

		// Token: 0x04000BA5 RID: 2981
		internal IntPtr m_Handle;

		// Token: 0x04000BA6 RID: 2982
		internal uint m_Version;
	}
}
