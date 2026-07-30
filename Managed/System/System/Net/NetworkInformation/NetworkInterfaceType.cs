using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies types of network interfaces.</summary>
	// Token: 0x02000623 RID: 1571
	public enum NetworkInterfaceType
	{
		/// <summary>The interface type is not known.</summary>
		// Token: 0x04002833 RID: 10291
		Unknown = 1,
		/// <summary>The network interface uses an Ethernet connection. Ethernet is defined in IEEE standard 802.3.</summary>
		// Token: 0x04002834 RID: 10292
		Ethernet = 6,
		/// <summary>The network interface uses a Token-Ring connection. Token-Ring is defined in IEEE standard 802.5.</summary>
		// Token: 0x04002835 RID: 10293
		TokenRing = 9,
		/// <summary>The network interface uses a Fiber Distributed Data Interface (FDDI) connection. FDDI is a set of standards for data transmission on fiber optic lines in a local area network.</summary>
		// Token: 0x04002836 RID: 10294
		Fddi = 15,
		/// <summary>The network interface uses a basic rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x04002837 RID: 10295
		BasicIsdn = 20,
		/// <summary>The network interface uses a primary rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x04002838 RID: 10296
		PrimaryIsdn,
		/// <summary>The network interface uses a Point-To-Point protocol (PPP) connection. PPP is a protocol for data transmission using a serial device.</summary>
		// Token: 0x04002839 RID: 10297
		Ppp = 23,
		/// <summary>The network interface is a loopback adapter. Such interfaces are often used for testing; no traffic is sent over the wire.</summary>
		// Token: 0x0400283A RID: 10298
		Loopback,
		/// <summary>The network interface uses an Ethernet 3 megabit/second connection. This version of Ethernet is defined in IETF RFC 895.</summary>
		// Token: 0x0400283B RID: 10299
		Ethernet3Megabit = 26,
		/// <summary>The network interface uses a Serial Line Internet Protocol (SLIP) connection. SLIP is defined in IETF RFC 1055.</summary>
		// Token: 0x0400283C RID: 10300
		Slip = 28,
		/// <summary>The network interface uses asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x0400283D RID: 10301
		Atm = 37,
		/// <summary>The network interface uses a modem.</summary>
		// Token: 0x0400283E RID: 10302
		GenericModem = 48,
		/// <summary>The network interface uses a Fast Ethernet connection over twisted pair and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-T.</summary>
		// Token: 0x0400283F RID: 10303
		FastEthernetT = 62,
		/// <summary>The network interface uses a connection configured for ISDN and the X.25 protocol. X.25 allows computers on public networks to communicate using an intermediary computer.</summary>
		// Token: 0x04002840 RID: 10304
		Isdn,
		/// <summary>The network interface uses a Fast Ethernet connection over optical fiber and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-FX.</summary>
		// Token: 0x04002841 RID: 10305
		FastEthernetFx = 69,
		/// <summary>The network interface uses a wireless LAN connection (IEEE 802.11 standard).</summary>
		// Token: 0x04002842 RID: 10306
		Wireless80211 = 71,
		/// <summary>The network interface uses an Asymmetric Digital Subscriber Line (ADSL).</summary>
		// Token: 0x04002843 RID: 10307
		AsymmetricDsl = 94,
		/// <summary>The network interface uses a Rate Adaptive Digital Subscriber Line (RADSL).</summary>
		// Token: 0x04002844 RID: 10308
		RateAdaptDsl,
		/// <summary>The network interface uses a Symmetric Digital Subscriber Line (SDSL).</summary>
		// Token: 0x04002845 RID: 10309
		SymmetricDsl,
		/// <summary>The network interface uses a Very High Data Rate Digital Subscriber Line (VDSL).</summary>
		// Token: 0x04002846 RID: 10310
		VeryHighSpeedDsl,
		/// <summary>The network interface uses the Internet Protocol (IP) in combination with asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x04002847 RID: 10311
		IPOverAtm = 114,
		/// <summary>The network interface uses a gigabit Ethernet connection and provides a data rate of 1,000 megabits per second (1 gigabit per second).</summary>
		// Token: 0x04002848 RID: 10312
		GigabitEthernet = 117,
		/// <summary>The network interface uses a tunnel connection.</summary>
		// Token: 0x04002849 RID: 10313
		Tunnel = 131,
		/// <summary>The network interface uses a Multirate Digital Subscriber Line.</summary>
		// Token: 0x0400284A RID: 10314
		MultiRateSymmetricDsl = 143,
		/// <summary>The network interface uses a High Performance Serial Bus.</summary>
		// Token: 0x0400284B RID: 10315
		HighPerformanceSerialBus,
		/// <summary>The network interface uses a mobile broadband interface for WiMax devices.</summary>
		// Token: 0x0400284C RID: 10316
		Wman = 237,
		/// <summary>The network interface uses a mobile broadband interface for GSM-based devices.</summary>
		// Token: 0x0400284D RID: 10317
		Wwanpp = 243,
		/// <summary>The network interface uses a mobile broadband interface for CDMA-based devices.</summary>
		// Token: 0x0400284E RID: 10318
		Wwanpp2
	}
}
