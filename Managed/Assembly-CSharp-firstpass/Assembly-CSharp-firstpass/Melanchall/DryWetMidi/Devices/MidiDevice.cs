using System;
using System.Text;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000F2 RID: 242
	public abstract class MidiDevice : IDisposable
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060005FD RID: 1533 RVA: 0x00019784 File Offset: 0x00017984
		// (remove) Token: 0x060005FE RID: 1534 RVA: 0x000197BC File Offset: 0x000179BC
		public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;

		// Token: 0x060005FF RID: 1535 RVA: 0x000197F1 File Offset: 0x000179F1
		internal MidiDevice(int id)
		{
			this.Id = id;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001980C File Offset: 0x00017A0C
		~MidiDevice()
		{
			this.Dispose(false);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001983C File Offset: 0x00017A3C
		public int Id { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00019844 File Offset: 0x00017A44
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0001984C File Offset: 0x00017A4C
		public string Name { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00019855 File Offset: 0x00017A55
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001985D File Offset: 0x00017A5D
		public Manufacturer DriverManufacturer { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00019866 File Offset: 0x00017A66
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0001986E File Offset: 0x00017A6E
		public ushort ProductIdentifier { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00019877 File Offset: 0x00017A77
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0001987F File Offset: 0x00017A7F
		public Version DriverVersion { get; private set; }

		// Token: 0x0600060A RID: 1546 RVA: 0x00019888 File Offset: 0x00017A88
		protected void SetBasicDeviceInformation(ushort manufacturerIdentifier, ushort productIdentifier, uint driverVersion, string name)
		{
			this.Name = name;
			this.DriverManufacturer = (Manufacturer)(Enum.IsDefined(typeof(Manufacturer), manufacturerIdentifier) ? manufacturerIdentifier : 0);
			this.ProductIdentifier = productIdentifier;
			uint num = driverVersion >> 8;
			uint num2 = driverVersion & 255U;
			this.DriverVersion = new Version((int)num, (int)num2);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000198DE File Offset: 0x00017ADE
		protected void EnsureDeviceIsNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("Device is disposed.");
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000198F4 File Offset: 0x00017AF4
		protected void ProcessMmResult(uint mmResult)
		{
			if (mmResult == 0U)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			if (this.GetErrorText(mmResult, stringBuilder, 257U) != 0U)
			{
				throw new MidiDeviceException("Error occured during operation on device.");
			}
			throw new MidiDeviceException(stringBuilder.ToString());
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019935 File Offset: 0x00017B35
		protected void OnError(Exception exception)
		{
			EventHandler<ErrorOccurredEventArgs> errorOccurred = this.ErrorOccurred;
			if (errorOccurred == null)
			{
				return;
			}
			errorOccurred(this, new ErrorOccurredEventArgs(exception));
		}

		// Token: 0x0600060E RID: 1550
		protected abstract uint GetErrorText(uint mmrError, StringBuilder pszText, uint cchText);

		// Token: 0x0600060F RID: 1551
		internal abstract IntPtr GetHandle();

		// Token: 0x06000610 RID: 1552 RVA: 0x0001994E File Offset: 0x00017B4E
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00019956 File Offset: 0x00017B56
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000612 RID: 1554
		protected abstract void Dispose(bool disposing);

		// Token: 0x040007B6 RID: 1974
		protected IntPtr _handle = IntPtr.Zero;

		// Token: 0x040007B7 RID: 1975
		protected bool _disposed;
	}
}
