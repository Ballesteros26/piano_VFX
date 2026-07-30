using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event.</summary>
	// Token: 0x020003FC RID: 1020
	public class SerialDataReceivedEventArgs : EventArgs
	{
		// Token: 0x06001F19 RID: 7961 RVA: 0x0007A5CE File Offset: 0x000787CE
		internal SerialDataReceivedEventArgs(SerialData eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialData" /> values.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0007A5DD File Offset: 0x000787DD
		public SerialData EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal SerialDataReceivedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B27 RID: 6951
		private SerialData eventType;
	}
}
