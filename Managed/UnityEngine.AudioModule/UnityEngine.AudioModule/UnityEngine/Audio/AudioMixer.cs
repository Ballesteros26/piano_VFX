using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Audio
{
	// Token: 0x02000025 RID: 37
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[NativeHeader("Modules/Audio/Public/AudioMixer.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixer.bindings.h")]
	public class AudioMixer : Object
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00003346 File Offset: 0x00001546
		internal AudioMixer()
		{
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600019F RID: 415
		// (set) Token: 0x060001A0 RID: 416
		[NativeProperty]
		public extern AudioMixerGroup outputAudioMixerGroup
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060001A1 RID: 417
		[NativeMethod("FindSnapshotFromName")]
		[MethodImpl(4096)]
		public extern AudioMixerSnapshot FindSnapshot(string name);

		// Token: 0x060001A2 RID: 418
		[NativeMethod("AudioMixerBindings::FindMatchingGroups", IsFreeFunction = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern AudioMixerGroup[] FindMatchingGroups(string subPath);

		// Token: 0x060001A3 RID: 419 RVA: 0x00003350 File Offset: 0x00001550
		internal void TransitionToSnapshot(AudioMixerSnapshot snapshot, float timeToReach)
		{
			bool flag = snapshot == null;
			if (flag)
			{
				throw new ArgumentException("null Snapshot passed to AudioMixer.TransitionToSnapshot of AudioMixer '" + base.name + "'");
			}
			bool flag2 = snapshot.audioMixer != this;
			if (flag2)
			{
				throw new ArgumentException(string.Concat(new string[] { "Snapshot '", snapshot.name, "' passed to AudioMixer.TransitionToSnapshot is not a snapshot from AudioMixer '", base.name, "'" }));
			}
			this.TransitionToSnapshotInternal(snapshot, timeToReach);
		}

		// Token: 0x060001A4 RID: 420
		[NativeMethod("TransitionToSnapshot")]
		[MethodImpl(4096)]
		private extern void TransitionToSnapshotInternal(AudioMixerSnapshot snapshot, float timeToReach);

		// Token: 0x060001A5 RID: 421
		[NativeMethod("AudioMixerBindings::TransitionToSnapshots", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void TransitionToSnapshots(AudioMixerSnapshot[] snapshots, float[] weights, float timeToReach);

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A6 RID: 422
		// (set) Token: 0x060001A7 RID: 423
		[NativeProperty]
		public extern AudioMixerUpdateMode updateMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060001A8 RID: 424
		[NativeMethod]
		[MethodImpl(4096)]
		public extern bool SetFloat(string name, float value);

		// Token: 0x060001A9 RID: 425
		[NativeMethod]
		[MethodImpl(4096)]
		public extern bool ClearFloat(string name);

		// Token: 0x060001AA RID: 426
		[NativeMethod]
		[MethodImpl(4096)]
		public extern bool GetFloat(string name, out float value);
	}
}
