using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000EB RID: 235
	public sealed class InputDevice : MidiDevice, IInputDevice
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060005BE RID: 1470 RVA: 0x00018E44 File Offset: 0x00017044
		// (remove) Token: 0x060005BF RID: 1471 RVA: 0x00018E7C File Offset: 0x0001707C
		public event EventHandler<MidiEventReceivedEventArgs> EventReceived;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060005C0 RID: 1472 RVA: 0x00018EB4 File Offset: 0x000170B4
		// (remove) Token: 0x060005C1 RID: 1473 RVA: 0x00018EEC File Offset: 0x000170EC
		public event EventHandler<MidiTimeCodeReceivedEventArgs> MidiTimeCodeReceived;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060005C2 RID: 1474 RVA: 0x00018F24 File Offset: 0x00017124
		// (remove) Token: 0x060005C3 RID: 1475 RVA: 0x00018F5C File Offset: 0x0001715C
		public event EventHandler<InvalidSysExEventReceivedEventArgs> InvalidSysExEventReceived;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060005C4 RID: 1476 RVA: 0x00018F94 File Offset: 0x00017194
		// (remove) Token: 0x060005C5 RID: 1477 RVA: 0x00018FCC File Offset: 0x000171CC
		public event EventHandler<InvalidShortEventReceivedEventArgs> InvalidShortEventReceived;

		// Token: 0x060005C6 RID: 1478 RVA: 0x00019004 File Offset: 0x00017204
		private InputDevice(int id)
			: base(id)
		{
			this._bytesToMidiEventConverter.ReadingSettings.SilentNoteOnPolicy = SilentNoteOnPolicy.NoteOn;
			this.SetDeviceInformation();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00019058 File Offset: 0x00017258
		~InputDevice()
		{
			this.Dispose(false);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00019088 File Offset: 0x00017288
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x00019090 File Offset: 0x00017290
		public bool RaiseMidiTimeCodeReceived { get; set; } = true;

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00019099 File Offset: 0x00017299
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x000190A1 File Offset: 0x000172A1
		public bool IsListeningForEvents { get; private set; }

		// Token: 0x060005CC RID: 1484 RVA: 0x000190AA File Offset: 0x000172AA
		public void StartEventsListening()
		{
			base.EnsureDeviceIsNotDisposed();
			this.EnsureHandleIsCreated();
			this.PrepareSysExBuffer();
			base.ProcessMmResult(MidiInWinApi.midiInStart(this._handle));
			this.IsListeningForEvents = true;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000190D6 File Offset: 0x000172D6
		public void StopEventsListening()
		{
			base.EnsureDeviceIsNotDisposed();
			if (this._handle == IntPtr.Zero)
			{
				return;
			}
			this.IsListeningForEvents = false;
			base.ProcessMmResult(MidiInWinApi.midiInStop(this._handle));
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00019109 File Offset: 0x00017309
		public void Reset()
		{
			base.EnsureDeviceIsNotDisposed();
			if (this._handle == IntPtr.Zero)
			{
				return;
			}
			base.ProcessMmResult(MidiInWinApi.midiInReset(this._handle));
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00019135 File Offset: 0x00017335
		public static int GetDevicesCount()
		{
			return (int)MidiInWinApi.midiInGetNumDevs();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001913C File Offset: 0x0001733C
		public static IEnumerable<InputDevice> GetAll()
		{
			int devicesCount = InputDevice.GetDevicesCount();
			int num;
			for (int deviceId = 0; deviceId < devicesCount; deviceId = num + 1)
			{
				yield return new InputDevice(deviceId);
				num = deviceId;
			}
			yield break;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019148 File Offset: 0x00017348
		public static InputDevice GetByName(string name)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("name", name, "Device name");
			InputDevice inputDevice = InputDevice.GetAll().FirstOrDefault((InputDevice d) => d.Name == name);
			if (inputDevice == null)
			{
				throw new ArgumentException("There is no MIDI input device '" + name + "'.", "name");
			}
			return inputDevice;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000191B0 File Offset: 0x000173B0
		public static InputDevice GetById(int id)
		{
			ThrowIfArgument.IsOutOfRange("id", id, 0, InputDevice.GetDevicesCount() - 1, "Device ID is out of range.");
			return new InputDevice(id);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000191D0 File Offset: 0x000173D0
		protected override uint GetErrorText(uint mmrError, StringBuilder pszText, uint cchText)
		{
			return MidiInWinApi.midiInGetErrorText(mmrError, pszText, cchText);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000191DA File Offset: 0x000173DA
		private void OnEventReceived(MidiEvent midiEvent)
		{
			EventHandler<MidiEventReceivedEventArgs> eventReceived = this.EventReceived;
			if (eventReceived == null)
			{
				return;
			}
			eventReceived(this, new MidiEventReceivedEventArgs(midiEvent));
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000191F3 File Offset: 0x000173F3
		private void OnMidiTimeCodeReceived(MidiTimeCodeType timeCodeType, int hours, int minutes, int seconds, int frames)
		{
			EventHandler<MidiTimeCodeReceivedEventArgs> midiTimeCodeReceived = this.MidiTimeCodeReceived;
			if (midiTimeCodeReceived == null)
			{
				return;
			}
			midiTimeCodeReceived(this, new MidiTimeCodeReceivedEventArgs(timeCodeType, hours, minutes, seconds, frames));
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00019212 File Offset: 0x00017412
		private void OnInvalidSysExEventReceived(byte[] data)
		{
			EventHandler<InvalidSysExEventReceivedEventArgs> invalidSysExEventReceived = this.InvalidSysExEventReceived;
			if (invalidSysExEventReceived == null)
			{
				return;
			}
			invalidSysExEventReceived(this, new InvalidSysExEventReceivedEventArgs(data));
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001922B File Offset: 0x0001742B
		private void OnInvalidShortEventReceived(byte statusByte, byte firstDataByte, byte secondDataByte)
		{
			EventHandler<InvalidShortEventReceivedEventArgs> invalidShortEventReceived = this.InvalidShortEventReceived;
			if (invalidShortEventReceived == null)
			{
				return;
			}
			invalidShortEventReceived(this, new InvalidShortEventReceivedEventArgs(statusByte, firstDataByte, secondDataByte));
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00019248 File Offset: 0x00017448
		private void PrepareSysExBuffer()
		{
			MidiWinApi.MIDIHDR midihdr = new MidiWinApi.MIDIHDR
			{
				lpData = Marshal.AllocHGlobal(2048),
				dwBufferLength = 2048,
				dwBytesRecorded = 2048
			};
			this._sysExHeaderPointer = Marshal.AllocHGlobal(MidiWinApi.MidiHeaderSize);
			Marshal.StructureToPtr<MidiWinApi.MIDIHDR>(midihdr, this._sysExHeaderPointer, false);
			base.ProcessMmResult(MidiInWinApi.midiInPrepareHeader(this._handle, this._sysExHeaderPointer, MidiWinApi.MidiHeaderSize));
			base.ProcessMmResult(MidiInWinApi.midiInAddBuffer(this._handle, this._sysExHeaderPointer, MidiWinApi.MidiHeaderSize));
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000192DC File Offset: 0x000174DC
		private void UnprepareSysExBuffer(IntPtr headerPointer)
		{
			if (headerPointer == IntPtr.Zero)
			{
				return;
			}
			MidiInWinApi.midiInUnprepareHeader(this._handle, headerPointer, MidiWinApi.MidiHeaderSize);
			Marshal.FreeHGlobal(((MidiWinApi.MIDIHDR)Marshal.PtrToStructure(headerPointer, typeof(MidiWinApi.MIDIHDR))).lpData);
			Marshal.FreeHGlobal(headerPointer);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00019330 File Offset: 0x00017530
		private void EnsureHandleIsCreated()
		{
			if (this._handle != IntPtr.Zero)
			{
				return;
			}
			this._callback = new MidiWinApi.MidiMessageCallback(this.OnMessage);
			base.ProcessMmResult(MidiInWinApi.midiInOpen(out this._handle, base.Id, this._callback, IntPtr.Zero, 196608U));
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00019389 File Offset: 0x00017589
		private void DestroyHandle()
		{
			if (this._handle == IntPtr.Zero)
			{
				return;
			}
			MidiInWinApi.midiInReset(this._handle);
			MidiInWinApi.midiInClose(this._handle);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000193B8 File Offset: 0x000175B8
		private void SetDeviceInformation()
		{
			MidiInWinApi.MIDIINCAPS midiincaps = default(MidiInWinApi.MIDIINCAPS);
			base.ProcessMmResult(MidiInWinApi.midiInGetDevCaps(new IntPtr(base.Id), ref midiincaps, (uint)Marshal.SizeOf<MidiInWinApi.MIDIINCAPS>(midiincaps)));
			base.SetBasicDeviceInformation(midiincaps.wMid, midiincaps.wPid, midiincaps.vDriverVersion, midiincaps.szPname);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001940C File Offset: 0x0001760C
		private void OnMessage(IntPtr hMidi, MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2)
		{
			if (!this.IsListeningForEvents)
			{
				return;
			}
			switch (wMsg)
			{
			case MidiMessage.MIM_DATA:
				break;
			case MidiMessage.MIM_LONGDATA:
				this.OnSysExMessage(dwParam1);
				return;
			case MidiMessage.MIM_ERROR:
			{
				byte b;
				byte b2;
				byte b3;
				MidiWinApi.UnpackShortEventBytes(dwParam1.ToInt32(), out b, out b2, out b3);
				this.OnInvalidShortEventReceived(b, b2, b3);
				return;
			}
			case MidiMessage.MIM_LONGERROR:
				this.OnInvalidSysExEventReceived(MidiWinApi.UnpackSysExBytes(dwParam1));
				return;
			default:
				if (wMsg != MidiMessage.MIM_MOREDATA)
				{
					return;
				}
				break;
			}
			this.OnShortMessage(dwParam1.ToInt32());
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00019488 File Offset: 0x00017688
		private void OnShortMessage(int message)
		{
			try
			{
				byte b;
				byte b2;
				byte b3;
				MidiWinApi.UnpackShortEventBytes(message, out b, out b2, out b3);
				MidiEvent midiEvent = this._bytesToMidiEventConverter.Convert(b, new byte[] { b2, b3 });
				this.OnEventReceived(midiEvent);
				if (this.RaiseMidiTimeCodeReceived)
				{
					MidiTimeCodeEvent midiTimeCodeEvent = midiEvent as MidiTimeCodeEvent;
					if (midiTimeCodeEvent != null)
					{
						this.TryRaiseMidiTimeCodeReceived(midiTimeCodeEvent);
					}
				}
			}
			catch (Exception ex)
			{
				base.OnError(new MidiDeviceException("Failed to parse short message.", ex)
				{
					Data = { { "Message", message } }
				});
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00019524 File Offset: 0x00017724
		private void OnSysExMessage(IntPtr sysExHeaderPointer)
		{
			byte[] array = null;
			try
			{
				array = MidiWinApi.UnpackSysExBytes(sysExHeaderPointer);
				NormalSysExEvent normalSysExEvent = new NormalSysExEvent(array);
				this.OnEventReceived(normalSysExEvent);
				this.UnprepareSysExBuffer(sysExHeaderPointer);
				this.PrepareSysExBuffer();
			}
			catch (Exception ex)
			{
				base.OnError(new MidiDeviceException("Failed to parse system exclusive message.", ex)
				{
					Data = { { "Data", array } }
				});
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00019590 File Offset: 0x00017790
		private void TryRaiseMidiTimeCodeReceived(MidiTimeCodeEvent midiTimeCodeEvent)
		{
			MidiTimeCodeComponent component = midiTimeCodeEvent.Component;
			FourBitNumber componentValue = midiTimeCodeEvent.ComponentValue;
			this._midiTimeCodeComponents[component] = componentValue;
			if (this._midiTimeCodeComponents.Count != InputDevice.MidiTimeCodeComponentsCount)
			{
				return;
			}
			byte b = DataTypesUtilities.Combine(this._midiTimeCodeComponents[MidiTimeCodeComponent.FramesMsb], this._midiTimeCodeComponents[MidiTimeCodeComponent.FramesLsb]);
			byte b2 = DataTypesUtilities.Combine(this._midiTimeCodeComponents[MidiTimeCodeComponent.MinutesMsb], this._midiTimeCodeComponents[MidiTimeCodeComponent.MinutesLsb]);
			byte b3 = DataTypesUtilities.Combine(this._midiTimeCodeComponents[MidiTimeCodeComponent.SecondsMsb], this._midiTimeCodeComponents[MidiTimeCodeComponent.SecondsLsb]);
			byte b4 = DataTypesUtilities.Combine(this._midiTimeCodeComponents[MidiTimeCodeComponent.HoursMsbAndTimeCodeType], this._midiTimeCodeComponents[MidiTimeCodeComponent.HoursLsb]);
			int num = (int)(b4 & 31);
			MidiTimeCodeType midiTimeCodeType = (MidiTimeCodeType)((b4 >> 5) & 3);
			this.OnMidiTimeCodeReceived(midiTimeCodeType, num, (int)b2, (int)b3, (int)b);
			this._midiTimeCodeComponents.Clear();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00019669 File Offset: 0x00017869
		protected override void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this._bytesToMidiEventConverter.Dispose();
			}
			this.StopEventsListening();
			this.DestroyHandle();
			this.UnprepareSysExBuffer(this._sysExHeaderPointer);
			this._disposed = true;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000196A1 File Offset: 0x000178A1
		internal override IntPtr GetHandle()
		{
			this.EnsureHandleIsCreated();
			return this._handle;
		}

		// Token: 0x04000755 RID: 1877
		private const int SysExBufferLength = 2048;

		// Token: 0x04000756 RID: 1878
		private const int ChannelParametersBufferSize = 2;

		// Token: 0x04000757 RID: 1879
		private static readonly int MidiTimeCodeComponentsCount = Enum.GetValues(typeof(MidiTimeCodeComponent)).Length;

		// Token: 0x0400075C RID: 1884
		private readonly BytesToMidiEventConverter _bytesToMidiEventConverter = new BytesToMidiEventConverter(2);

		// Token: 0x0400075D RID: 1885
		private IntPtr _sysExHeaderPointer = IntPtr.Zero;

		// Token: 0x0400075E RID: 1886
		private MidiWinApi.MidiMessageCallback _callback;

		// Token: 0x0400075F RID: 1887
		private readonly Dictionary<MidiTimeCodeComponent, FourBitNumber> _midiTimeCodeComponents = new Dictionary<MidiTimeCodeComponent, FourBitNumber>();
	}
}
