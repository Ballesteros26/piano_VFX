using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x0200002D RID: 45
	[StaticAccessor("AudioSampleProviderBindings", StaticAccessorType.DoubleColon)]
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.h")]
	public class AudioSampleProvider : IDisposable
	{
		// Token: 0x060001CA RID: 458 RVA: 0x000036A4 File Offset: 0x000018A4
		[VisibleToOtherModules]
		internal static AudioSampleProvider Lookup(uint providerId, Object ownerObj, ushort trackIndex)
		{
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.InternalGetScriptingPtr(providerId);
			bool flag = audioSampleProvider != null || !AudioSampleProvider.InternalIsValid(providerId);
			AudioSampleProvider audioSampleProvider2;
			if (flag)
			{
				audioSampleProvider2 = audioSampleProvider;
			}
			else
			{
				audioSampleProvider2 = new AudioSampleProvider(providerId, ownerObj, trackIndex);
			}
			return audioSampleProvider2;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000036DC File Offset: 0x000018DC
		internal static AudioSampleProvider Create(ushort channelCount, uint sampleRate)
		{
			uint num = AudioSampleProvider.InternalCreateSampleProvider(channelCount, sampleRate);
			bool flag = !AudioSampleProvider.InternalIsValid(num);
			AudioSampleProvider audioSampleProvider;
			if (flag)
			{
				audioSampleProvider = null;
			}
			else
			{
				audioSampleProvider = new AudioSampleProvider(num, null, 0);
			}
			return audioSampleProvider;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00003710 File Offset: 0x00001910
		private AudioSampleProvider(uint providerId, Object ownerObj, ushort trackIdx)
		{
			this.owner = ownerObj;
			this.id = providerId;
			this.trackIndex = trackIdx;
			this.m_ConsumeSampleFramesNativeFunction = (AudioSampleProvider.ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(AudioSampleProvider.InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(AudioSampleProvider.ConsumeSampleFramesNativeFunction));
			ushort num = 0;
			uint num2 = 0U;
			AudioSampleProvider.InternalGetFormatInfo(providerId, out num, out num2);
			this.channelCount = num;
			this.sampleRate = num2;
			AudioSampleProvider.InternalSetScriptingPtr(providerId, this);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00003784 File Offset: 0x00001984
		~AudioSampleProvider()
		{
			this.owner = null;
			this.Dispose();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000037BC File Offset: 0x000019BC
		public void Dispose()
		{
			bool flag = this.id > 0U;
			if (flag)
			{
				AudioSampleProvider.InternalSetScriptingPtr(this.id, null);
				bool flag2 = this.owner == null;
				if (flag2)
				{
					AudioSampleProvider.InternalRemove(this.id);
				}
				this.id = 0U;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00003811 File Offset: 0x00001A11
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00003819 File Offset: 0x00001A19
		public uint id { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00003822 File Offset: 0x00001A22
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000382A File Offset: 0x00001A2A
		public ushort trackIndex { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00003833 File Offset: 0x00001A33
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000383B File Offset: 0x00001A3B
		public Object owner { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00003844 File Offset: 0x00001A44
		public bool valid
		{
			get
			{
				return AudioSampleProvider.InternalIsValid(this.id);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00003861 File Offset: 0x00001A61
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00003869 File Offset: 0x00001A69
		public ushort channelCount { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00003872 File Offset: 0x00001A72
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000387A File Offset: 0x00001A7A
		public uint sampleRate { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00003884 File Offset: 0x00001A84
		public uint maxSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetMaxSampleFrameCount(this.id);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000038A4 File Offset: 0x00001AA4
		public uint availableSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetAvailableSampleFrameCount(this.id);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000038C4 File Offset: 0x00001AC4
		public uint freeSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetFreeSampleFrameCount(this.id);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000038E4 File Offset: 0x00001AE4
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00003901 File Offset: 0x00001B01
		public uint freeSampleFrameCountLowThreshold
		{
			get
			{
				return AudioSampleProvider.InternalGetFreeSampleFrameCountLowThreshold(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetFreeSampleFrameCountLowThreshold(this.id, value);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00003914 File Offset: 0x00001B14
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00003931 File Offset: 0x00001B31
		public bool enableSampleFramesAvailableEvents
		{
			get
			{
				return AudioSampleProvider.InternalGetEnableSampleFramesAvailableEvents(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetEnableSampleFramesAvailableEvents(this.id, value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00003944 File Offset: 0x00001B44
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00003961 File Offset: 0x00001B61
		public bool enableSilencePadding
		{
			get
			{
				return AudioSampleProvider.InternalGetEnableSilencePadding(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetEnableSilencePadding(this.id, value);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00003974 File Offset: 0x00001B74
		public uint ConsumeSampleFrames(NativeArray<float> sampleFrames)
		{
			bool flag = this.channelCount == 0;
			uint num;
			if (flag)
			{
				num = 0U;
			}
			else
			{
				num = this.m_ConsumeSampleFramesNativeFunction(this.id, (IntPtr)sampleFrames.GetUnsafePtr<float>(), (uint)(sampleFrames.Length / (int)this.channelCount));
			}
			return num;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000039C4 File Offset: 0x00001BC4
		public static AudioSampleProvider.ConsumeSampleFramesNativeFunction consumeSampleFramesNativeFunction
		{
			get
			{
				return (AudioSampleProvider.ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(AudioSampleProvider.InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(AudioSampleProvider.ConsumeSampleFramesNativeFunction));
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000039F0 File Offset: 0x00001BF0
		internal uint QueueSampleFrames(NativeArray<float> sampleFrames)
		{
			bool flag = this.channelCount == 0;
			uint num;
			if (flag)
			{
				num = 0U;
			}
			else
			{
				num = AudioSampleProvider.InternalQueueSampleFrames(this.id, (IntPtr)sampleFrames.GetUnsafeReadOnlyPtr<float>(), (uint)(sampleFrames.Length / (int)this.channelCount));
			}
			return num;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001E6 RID: 486 RVA: 0x00003A38 File Offset: 0x00001C38
		// (remove) Token: 0x060001E7 RID: 487 RVA: 0x00003A70 File Offset: 0x00001C70
		[field: DebuggerBrowsable(0)]
		public event AudioSampleProvider.SampleFramesHandler sampleFramesAvailable;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001E8 RID: 488 RVA: 0x00003AA8 File Offset: 0x00001CA8
		// (remove) Token: 0x060001E9 RID: 489 RVA: 0x00003AE0 File Offset: 0x00001CE0
		[field: DebuggerBrowsable(0)]
		public event AudioSampleProvider.SampleFramesHandler sampleFramesOverflow;

		// Token: 0x060001EA RID: 490 RVA: 0x00003B15 File Offset: 0x00001D15
		public void SetSampleFramesAvailableNativeHandler(AudioSampleProvider.SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			AudioSampleProvider.InternalSetSampleFramesAvailableNativeHandler(this.id, Marshal.GetFunctionPointerForDelegate(handler), userData);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00003B2B File Offset: 0x00001D2B
		public void ClearSampleFramesAvailableNativeHandler()
		{
			AudioSampleProvider.InternalClearSampleFramesAvailableNativeHandler(this.id);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00003B3A File Offset: 0x00001D3A
		public void SetSampleFramesOverflowNativeHandler(AudioSampleProvider.SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			AudioSampleProvider.InternalSetSampleFramesOverflowNativeHandler(this.id, Marshal.GetFunctionPointerForDelegate(handler), userData);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00003B50 File Offset: 0x00001D50
		public void ClearSampleFramesOverflowNativeHandler()
		{
			AudioSampleProvider.InternalClearSampleFramesOverflowNativeHandler(this.id);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00003B60 File Offset: 0x00001D60
		[RequiredByNativeCode]
		private void InvokeSampleFramesAvailable(int sampleFrameCount)
		{
			bool flag = this.sampleFramesAvailable != null;
			if (flag)
			{
				this.sampleFramesAvailable(this, (uint)sampleFrameCount);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00003B8C File Offset: 0x00001D8C
		[RequiredByNativeCode]
		private void InvokeSampleFramesOverflow(int droppedSampleFrameCount)
		{
			bool flag = this.sampleFramesOverflow != null;
			if (flag)
			{
				this.sampleFramesOverflow(this, (uint)droppedSampleFrameCount);
			}
		}

		// Token: 0x060001F0 RID: 496
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern uint InternalCreateSampleProvider(ushort channelCount, uint sampleRate);

		// Token: 0x060001F1 RID: 497
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		internal static extern void InternalRemove(uint providerId);

		// Token: 0x060001F2 RID: 498
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void InternalGetFormatInfo(uint providerId, out ushort chCount, out uint sRate);

		// Token: 0x060001F3 RID: 499
		[MethodImpl(4096)]
		private static extern AudioSampleProvider InternalGetScriptingPtr(uint providerId);

		// Token: 0x060001F4 RID: 500
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void InternalSetScriptingPtr(uint providerId, AudioSampleProvider provider);

		// Token: 0x060001F5 RID: 501
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		internal static extern bool InternalIsValid(uint providerId);

		// Token: 0x060001F6 RID: 502
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern uint InternalGetMaxSampleFrameCount(uint providerId);

		// Token: 0x060001F7 RID: 503
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern uint InternalGetAvailableSampleFrameCount(uint providerId);

		// Token: 0x060001F8 RID: 504
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern uint InternalGetFreeSampleFrameCount(uint providerId);

		// Token: 0x060001F9 RID: 505
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern uint InternalGetFreeSampleFrameCountLowThreshold(uint providerId);

		// Token: 0x060001FA RID: 506
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void InternalSetFreeSampleFrameCountLowThreshold(uint providerId, uint sampleFrameCount);

		// Token: 0x060001FB RID: 507
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern bool InternalGetEnableSampleFramesAvailableEvents(uint providerId);

		// Token: 0x060001FC RID: 508
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void InternalSetEnableSampleFramesAvailableEvents(uint providerId, bool enable);

		// Token: 0x060001FD RID: 509
		[MethodImpl(4096)]
		private static extern void InternalSetSampleFramesAvailableNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		// Token: 0x060001FE RID: 510
		[MethodImpl(4096)]
		private static extern void InternalClearSampleFramesAvailableNativeHandler(uint providerId);

		// Token: 0x060001FF RID: 511
		[MethodImpl(4096)]
		private static extern void InternalSetSampleFramesOverflowNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		// Token: 0x06000200 RID: 512
		[MethodImpl(4096)]
		private static extern void InternalClearSampleFramesOverflowNativeHandler(uint providerId);

		// Token: 0x06000201 RID: 513
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern bool InternalGetEnableSilencePadding(uint id);

		// Token: 0x06000202 RID: 514
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void InternalSetEnableSilencePadding(uint id, bool enabled);

		// Token: 0x06000203 RID: 515
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr InternalGetConsumeSampleFramesNativeFunctionPtr();

		// Token: 0x06000204 RID: 516
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern uint InternalQueueSampleFrames(uint id, IntPtr interleavedSampleFrames, uint sampleFrameCount);

		// Token: 0x0400006B RID: 107
		private AudioSampleProvider.ConsumeSampleFramesNativeFunction m_ConsumeSampleFramesNativeFunction;

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x06000206 RID: 518
		[UnmanagedFunctionPointer(2)]
		public delegate uint ConsumeSampleFramesNativeFunction(uint providerId, IntPtr interleavedSampleFrames, uint sampleFrameCount);

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x0600020A RID: 522
		public delegate void SampleFramesHandler(AudioSampleProvider provider, uint sampleFrameCount);

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x0600020E RID: 526
		[UnmanagedFunctionPointer(2)]
		public delegate void SampleFramesEventNativeFunction(IntPtr userData, uint providerId, uint sampleFrameCount);
	}
}
