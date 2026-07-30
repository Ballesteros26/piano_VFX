using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.PinChanged" /> event.</summary>
	// Token: 0x020003F6 RID: 1014
	public class SerialPinChangedEventArgs : EventArgs
	{
		// Token: 0x06001E8D RID: 7821 RVA: 0x0007953C File Offset: 0x0007773C
		internal SerialPinChangedEventArgs(SerialPinChange eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialPinChange" /> values.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0007954B File Offset: 0x0007774B
		public SerialPinChange EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal SerialPinChangedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B07 RID: 6919
		private SerialPinChange eventType;
	}
}
