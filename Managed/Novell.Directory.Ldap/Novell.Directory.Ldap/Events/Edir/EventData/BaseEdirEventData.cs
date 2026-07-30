using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000BA RID: 186
	public class BaseEdirEventData
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00014DBA File Offset: 0x00012FBA
		public EdirEventDataType EventDataType
		{
			get
			{
				return this.event_data_type;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00014DC4 File Offset: 0x00012FC4
		public BaseEdirEventData(EdirEventDataType eventDataType, Asn1Object message)
		{
			this.event_data_type = eventDataType;
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)message).byteValue());
			this.decodedData = new MemoryStream(array);
			this.decoder = new LBERDecoder();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00014E06 File Offset: 0x00013006
		protected void DataInitDone()
		{
			this.decodedData = null;
			this.decoder = null;
		}

		// Token: 0x0400042D RID: 1069
		protected MemoryStream decodedData;

		// Token: 0x0400042E RID: 1070
		protected LBERDecoder decoder;

		// Token: 0x0400042F RID: 1071
		protected EdirEventDataType event_data_type;
	}
}
