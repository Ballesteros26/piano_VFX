using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000E7 RID: 231
	public sealed class DevicesConnector : IDisposable
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x00018CE1 File Offset: 0x00016EE1
		public DevicesConnector(InputDevice inputDevice, params IOutputDevice[] outputDevices)
		{
			ThrowIfArgument.IsNull("inputDevice", inputDevice);
			ThrowIfArgument.IsNull("outputDevices", outputDevices);
			ThrowIfArgument.ContainsNull<IOutputDevice>("outputDevices", outputDevices);
			this.InputDevice = inputDevice;
			this.OutputDevices = outputDevices;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00018D18 File Offset: 0x00016F18
		~DevicesConnector()
		{
			this.Dispose(false);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00018D48 File Offset: 0x00016F48
		public InputDevice InputDevice { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00018D50 File Offset: 0x00016F50
		public IReadOnlyCollection<IOutputDevice> OutputDevices { get; }

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018D58 File Offset: 0x00016F58
		public void Connect()
		{
			this.InputDevice.EventReceived += this.OnEventReceived;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00018D71 File Offset: 0x00016F71
		public void Disconnect()
		{
			this.InputDevice.EventReceived -= this.OnEventReceived;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00018D8C File Offset: 0x00016F8C
		private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
		{
			foreach (IOutputDevice outputDevice in this.OutputDevices)
			{
				outputDevice.SendEvent(e.Event);
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018DDC File Offset: 0x00016FDC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00018DEB File Offset: 0x00016FEB
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this.Disconnect();
			}
			this._disposed = true;
		}

		// Token: 0x04000751 RID: 1873
		private bool _disposed;
	}
}
