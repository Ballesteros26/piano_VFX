using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Events.Edir.EventData;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000B5 RID: 181
	public class EdirEventIntermediateResponse : LdapIntermediateResponse
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x000147BE File Offset: 0x000129BE
		public EdirEventType EventType
		{
			get
			{
				return this.event_type;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000147C6 File Offset: 0x000129C6
		public EdirEventResultType EventResultType
		{
			get
			{
				return this.event_result_type;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x000147CE File Offset: 0x000129CE
		public BaseEdirEventData EventResponseDataObject
		{
			get
			{
				return this.event_response_data;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000147D6 File Offset: 0x000129D6
		public EdirEventIntermediateResponse(RfcLdapMessage message)
			: base(message)
		{
			this.ProcessMessage(base.getValue());
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000147EB File Offset: 0x000129EB
		public EdirEventIntermediateResponse(byte[] message)
			: base(new RfcLdapMessage(new Asn1Sequence()))
		{
			this.ProcessMessage(SupportClass.ToSByteArray(message));
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001480C File Offset: 0x00012A0C
		[CLSCompliant(false)]
		protected void ProcessMessage(sbyte[] returnedValue)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)new LBERDecoder().decode(returnedValue);
			this.event_type = (EdirEventType)((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			this.event_result_type = (EdirEventResultType)((Asn1Integer)asn1Sequence.get_Renamed(1)).intValue();
			if (asn1Sequence.size() > 2)
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)asn1Sequence.get_Renamed(2);
				switch (asn1Tagged.getIdentifier().Tag)
				{
				case 1:
					this.event_response_data = new EntryEventData(EdirEventDataType.EDIR_TAG_ENTRY_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				case 2:
					this.event_response_data = new ValueEventData(EdirEventDataType.EDIR_TAG_VALUE_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				case 3:
					this.event_response_data = new GeneralDSEventData(EdirEventDataType.EDIR_TAG_GENERAL_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				case 4:
					this.event_response_data = null;
					return;
				case 5:
					this.event_response_data = new BinderyObjectEventData(EdirEventDataType.EDIR_TAG_BINDERY_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				case 6:
					this.event_response_data = new SecurityEquivalenceEventData(EdirEventDataType.EDIR_TAG_DSESEV_INFO, asn1Tagged.taggedValue());
					return;
				case 7:
					this.event_response_data = new ModuleStateEventData(EdirEventDataType.EDIR_TAG_MODULE_STATE_DATA, asn1Tagged.taggedValue());
					return;
				case 8:
					this.event_response_data = new NetworkAddressEventData(EdirEventDataType.EDIR_TAG_NETWORK_ADDRESS, asn1Tagged.taggedValue());
					return;
				case 9:
					this.event_response_data = new ConnectionStateEventData(EdirEventDataType.EDIR_TAG_CONNECTION_STATE, asn1Tagged.taggedValue());
					return;
				case 10:
					this.event_response_data = new ChangeAddressEventData(EdirEventDataType.EDIR_TAG_CHANGE_SERVER_ADDRESS, asn1Tagged.taggedValue());
					return;
				case 12:
					this.event_response_data = null;
					return;
				case 14:
					this.event_response_data = new DebugEventData(EdirEventDataType.EDIR_TAG_DEBUG_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				}
				throw new IOException();
			}
			this.event_response_data = null;
		}

		// Token: 0x04000422 RID: 1058
		protected EdirEventType event_type;

		// Token: 0x04000423 RID: 1059
		protected EdirEventResultType event_result_type;

		// Token: 0x04000424 RID: 1060
		protected BaseEdirEventData event_response_data;
	}
}
