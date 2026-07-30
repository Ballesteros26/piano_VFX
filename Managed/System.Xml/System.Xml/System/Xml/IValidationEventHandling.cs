using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000A2 RID: 162
	internal interface IValidationEventHandling
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000570 RID: 1392
		object EventHandler { get; }

		// Token: 0x06000571 RID: 1393
		void SendEvent(Exception exception, XmlSeverityType severity);
	}
}
