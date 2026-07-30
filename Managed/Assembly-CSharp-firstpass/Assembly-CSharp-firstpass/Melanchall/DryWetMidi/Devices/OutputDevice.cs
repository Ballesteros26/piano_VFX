using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000F7 RID: 247
	public sealed class OutputDevice : MidiDevice, IOutputDevice
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000627 RID: 1575 RVA: 0x00019998 File Offset: 0x00017B98
		// (remove) Token: 0x06000628 RID: 1576 RVA: 0x000199D0 File Offset: 0x00017BD0
		public event EventHandler<MidiEventSentEventArgs> EventSent;

		// Token: 0x06000629 RID: 1577 RVA: 0x00019A05 File Offset: 0x00017C05
		internal OutputDevice(int id)
			: base(id)
		{
			this.SetDeviceInformation();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00019A38 File Offset: 0x00017C38
		~OutputDevice()
		{
			this.Dispose(false);
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00019A68 File Offset: 0x00017C68
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00019A70 File Offset: 0x00017C70
		public OutputDeviceType DeviceType { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00019A79 File Offset: 0x00017C79
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00019A81 File Offset: 0x00017C81
		public int VoicesNumber { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00019A8A File Offset: 0x00017C8A
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00019A92 File Offset: 0x00017C92
		public int NotesNumber { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00019A9B File Offset: 0x00017C9B
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00019AA3 File Offset: 0x00017CA3
		public IEnumerable<FourBitNumber> Channels { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00019AAC File Offset: 0x00017CAC
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00019AB4 File Offset: 0x00017CB4
		public bool SupportsPatchCaching { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00019ABD File Offset: 0x00017CBD
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00019AC5 File Offset: 0x00017CC5
		public bool SupportsLeftRightVolumeControl { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00019ACE File Offset: 0x00017CCE
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00019AD6 File Offset: 0x00017CD6
		public bool SupportsVolumeControl { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00019AE0 File Offset: 0x00017CE0
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x00019B48 File Offset: 0x00017D48
		public Volume Volume
		{
			get
			{
				base.EnsureDeviceIsNotDisposed();
				this.EnsureHandleIsCreated();
				if (!this.SupportsVolumeControl)
				{
					throw new InvalidOperationException("Device doesn't support volume control.");
				}
				uint num = 0U;
				base.ProcessMmResult(MidiOutWinApi.midiOutGetVolume(this._handle, ref num));
				ushort tail = num.GetTail();
				ushort head = num.GetHead();
				if (!this.SupportsLeftRightVolumeControl)
				{
					return new Volume(tail, tail);
				}
				return new Volume(tail, head);
			}
			set
			{
				base.EnsureDeviceIsNotDisposed();
				this.EnsureHandleIsCreated();
				if (!this.SupportsVolumeControl)
				{
					throw new InvalidOperationException("Device doesn't support volume control.");
				}
				ushort leftVolume = value.LeftVolume;
				ushort rightVolume = value.RightVolume;
				if (!this.SupportsLeftRightVolumeControl && leftVolume != rightVolume)
				{
					throw new ArgumentException("Device doesn't support separate volume control for each channel.", "value");
				}
				uint num = DataTypesUtilities.Combine(rightVolume, leftVolume);
				base.ProcessMmResult(MidiOutWinApi.midiOutSetVolume(this._handle, num));
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00019BBC File Offset: 0x00017DBC
		public void SendEvent(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			base.EnsureDeviceIsNotDisposed();
			this.EnsureHandleIsCreated();
			if (midiEvent is ChannelEvent || midiEvent is SystemCommonEvent || midiEvent is SystemRealTimeEvent)
			{
				this.SendShortEvent(midiEvent);
				this.OnEventSent(midiEvent);
				return;
			}
			SysExEvent sysExEvent = midiEvent as SysExEvent;
			if (sysExEvent != null)
			{
				this.SendSysExEvent(sysExEvent);
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00019C18 File Offset: 0x00017E18
		public void TurnAllNotesOff()
		{
			base.EnsureDeviceIsNotDisposed();
			this.EnsureHandleIsCreated();
			foreach (NoteOffEvent noteOffEvent in from channel in FourBitNumber.Values
				from noteNumber in SevenBitNumber.Values
				select new NoteOffEvent(noteNumber, SevenBitNumber.MinValue)
				{
					Channel = channel
				})
			{
				this.SendEvent(noteOffEvent);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00019CB4 File Offset: 0x00017EB4
		public void PrepareForEventsSending()
		{
			this.EnsureHandleIsCreated();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00019CBC File Offset: 0x00017EBC
		public static int GetDevicesCount()
		{
			return (int)MidiOutWinApi.midiOutGetNumDevs();
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00019CC3 File Offset: 0x00017EC3
		public static IEnumerable<OutputDevice> GetAll()
		{
			int devicesCount = OutputDevice.GetDevicesCount();
			int num;
			for (int deviceId = 0; deviceId < devicesCount; deviceId = num + 1)
			{
				yield return new OutputDevice(deviceId);
				num = deviceId;
			}
			yield break;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00019CCC File Offset: 0x00017ECC
		public static OutputDevice GetByName(string name)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("name", name, "Device name");
			OutputDevice outputDevice = OutputDevice.GetAll().FirstOrDefault((OutputDevice d) => d.Name == name);
			if (outputDevice == null)
			{
				throw new ArgumentException("There is no output MIDI device '" + name + "'.", "name");
			}
			return outputDevice;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00019D34 File Offset: 0x00017F34
		public static OutputDevice GetById(int id)
		{
			ThrowIfArgument.IsOutOfRange("id", id, 0, OutputDevice.GetDevicesCount() - 1, "Device ID is out of range.");
			return new OutputDevice(id);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00019D54 File Offset: 0x00017F54
		protected override uint GetErrorText(uint mmrError, StringBuilder pszText, uint cchText)
		{
			return MidiOutWinApi.midiOutGetErrorText(mmrError, pszText, cchText);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00019D60 File Offset: 0x00017F60
		private void EnsureHandleIsCreated()
		{
			if (this._handle != IntPtr.Zero)
			{
				return;
			}
			this._callback = new MidiWinApi.MidiMessageCallback(this.OnMessage);
			base.ProcessMmResult(MidiOutWinApi.midiOutOpen(out this._handle, base.Id, this._callback, IntPtr.Zero, 196608U));
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00019DB9 File Offset: 0x00017FB9
		private void DestroyHandle()
		{
			if (this._handle == IntPtr.Zero)
			{
				return;
			}
			MidiOutWinApi.midiOutClose(this._handle);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00019DDC File Offset: 0x00017FDC
		private void SetDeviceInformation()
		{
			MidiOutWinApi.MIDIOUTCAPS caps = default(MidiOutWinApi.MIDIOUTCAPS);
			base.ProcessMmResult(MidiOutWinApi.midiOutGetDevCaps(new IntPtr(base.Id), ref caps, (uint)Marshal.SizeOf<MidiOutWinApi.MIDIOUTCAPS>(caps)));
			base.SetBasicDeviceInformation(caps.wMid, caps.wPid, caps.vDriverVersion, caps.szPname);
			this.DeviceType = (OutputDeviceType)caps.wTechnology;
			this.VoicesNumber = (int)caps.wVoices;
			this.NotesNumber = (int)caps.wNotes;
			this.Channels = (from channel in FourBitNumber.Values
				let isChannelSupported = (caps.wChannelMask >> (int)channel) & 1
				where isChannelSupported == 1
				select channel).ToArray<FourBitNumber>();
			MidiOutWinApi.MIDICAPS dwSupport = (MidiOutWinApi.MIDICAPS)caps.dwSupport;
			this.SupportsPatchCaching = dwSupport.HasFlag(MidiOutWinApi.MIDICAPS.MIDICAPS_CACHE);
			this.SupportsVolumeControl = dwSupport.HasFlag(MidiOutWinApi.MIDICAPS.MIDICAPS_VOLUME);
			this.SupportsLeftRightVolumeControl = dwSupport.HasFlag(MidiOutWinApi.MIDICAPS.MIDICAPS_LRVOLUME);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00019F44 File Offset: 0x00018144
		private void SendShortEvent(MidiEvent midiEvent)
		{
			int num = this.PackShortEvent(midiEvent);
			base.ProcessMmResult(MidiOutWinApi.midiOutShortMsg(this._handle, (uint)num));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00019F6C File Offset: 0x0001816C
		private void SendSysExEvent(SysExEvent sysExEvent)
		{
			byte[] data = sysExEvent.Data;
			if (data == null || !data.Any<byte>())
			{
				return;
			}
			IntPtr intPtr = this.PrepareSysExBuffer(data);
			this._sysExHeadersPointers.Add(intPtr);
			base.ProcessMmResult(MidiOutWinApi.midiOutLongMsg(this._handle, intPtr, MidiWinApi.MidiHeaderSize));
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00019FB8 File Offset: 0x000181B8
		private int PackShortEvent(MidiEvent midiEvent)
		{
			byte[] array = this._midiEventToBytesConverter.Convert(midiEvent, 3);
			return (int)array[0] + ((int)array[1] << 8) + ((int)array[2] << 16);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00019FE3 File Offset: 0x000181E3
		private void OnMessage(IntPtr hMidi, MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2)
		{
			if (wMsg == MidiMessage.MOM_DONE)
			{
				this.OnSysExEventSent(dwParam1);
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00019FF8 File Offset: 0x000181F8
		private void OnSysExEventSent(IntPtr sysExHeaderPointer)
		{
			byte[] array = null;
			try
			{
				array = MidiWinApi.UnpackSysExBytes(sysExHeaderPointer);
				NormalSysExEvent normalSysExEvent = new NormalSysExEvent(array);
				this.OnEventSent(normalSysExEvent);
				this.UnprepareSysExBuffer(sysExHeaderPointer);
				this._sysExHeadersPointers.Remove(sysExHeaderPointer);
			}
			catch (Exception ex)
			{
				base.OnError(new MidiDeviceException("Failed to parse sent system exclusive event.", ex)
				{
					Data = { { "Data", array } }
				});
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001A06C File Offset: 0x0001826C
		private IntPtr PrepareSysExBuffer(byte[] data)
		{
			int num = data.Length + 1;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.WriteByte(intPtr, 240);
			Marshal.Copy(data, 0, IntPtr.Add(intPtr, 1), data.Length);
			MidiWinApi.MIDIHDR midihdr = new MidiWinApi.MIDIHDR
			{
				lpData = intPtr,
				dwBufferLength = num,
				dwBytesRecorded = num
			};
			IntPtr intPtr2 = Marshal.AllocHGlobal(MidiWinApi.MidiHeaderSize);
			Marshal.StructureToPtr<MidiWinApi.MIDIHDR>(midihdr, intPtr2, false);
			base.ProcessMmResult(MidiOutWinApi.midiOutPrepareHeader(this._handle, intPtr2, MidiWinApi.MidiHeaderSize));
			return intPtr2;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001A0F0 File Offset: 0x000182F0
		private void UnprepareSysExBuffer(IntPtr headerPointer)
		{
			if (headerPointer == IntPtr.Zero)
			{
				return;
			}
			MidiOutWinApi.midiOutUnprepareHeader(this._handle, headerPointer, MidiWinApi.MidiHeaderSize);
			Marshal.FreeHGlobal(((MidiWinApi.MIDIHDR)Marshal.PtrToStructure(headerPointer, typeof(MidiWinApi.MIDIHDR))).lpData);
			Marshal.FreeHGlobal(headerPointer);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001A142 File Offset: 0x00018342
		private void OnEventSent(MidiEvent midiEvent)
		{
			EventHandler<MidiEventSentEventArgs> eventSent = this.EventSent;
			if (eventSent == null)
			{
				return;
			}
			eventSent(this, new MidiEventSentEventArgs(midiEvent));
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001A15C File Offset: 0x0001835C
		protected override void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this._midiEventToBytesConverter.Dispose();
				this._bytesToMidiEventConverter.Dispose();
			}
			this.DestroyHandle();
			foreach (IntPtr intPtr in this._sysExHeadersPointers)
			{
				this.UnprepareSysExBuffer(intPtr);
			}
			this._disposed = true;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001A1E0 File Offset: 0x000183E0
		internal override IntPtr GetHandle()
		{
			this.EnsureHandleIsCreated();
			return this._handle;
		}

		// Token: 0x040007BE RID: 1982
		private const int ChannelEventBufferSize = 3;

		// Token: 0x040007C0 RID: 1984
		private readonly MidiEventToBytesConverter _midiEventToBytesConverter = new MidiEventToBytesConverter(3);

		// Token: 0x040007C1 RID: 1985
		private readonly BytesToMidiEventConverter _bytesToMidiEventConverter = new BytesToMidiEventConverter();

		// Token: 0x040007C2 RID: 1986
		private MidiWinApi.MidiMessageCallback _callback;

		// Token: 0x040007C3 RID: 1987
		private readonly HashSet<IntPtr> _sysExHeadersPointers = new HashSet<IntPtr>();
	}
}
