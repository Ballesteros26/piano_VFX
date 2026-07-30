using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Prepares data for the <see cref="E:System.IO.Ports.SerialPort.ErrorReceived" /> event.</summary>
	// Token: 0x020003F4 RID: 1012
	public class SerialErrorReceivedEventArgs : EventArgs
	{
		// Token: 0x06001E8A RID: 7818 RVA: 0x00079525 File Offset: 0x00077725
		internal SerialErrorReceivedEventArgs(SerialError eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialError" /> values.</returns>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00079534 File Offset: 0x00077734
		public SerialError EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal SerialErrorReceivedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B00 RID: 6912
		private SerialError eventType;
	}
}
