using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x02000138 RID: 312
	internal sealed class webReferenceOptionsSerializer : XmlSerializer
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x000418E2 File Offset: 0x0003FAE2
		protected override XmlSerializationReader CreateReader()
		{
			return new WebReferenceOptionsSerializationReader();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000418E9 File Offset: 0x0003FAE9
		protected override XmlSerializationWriter CreateWriter()
		{
			return new WebReferenceOptionsSerializationWriter();
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00002B54 File Offset: 0x00000D54
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return true;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000418F0 File Offset: 0x0003FAF0
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((WebReferenceOptionsSerializationWriter)writer).Write5_webReferenceOptions(objectToSerialize);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000418FE File Offset: 0x0003FAFE
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((WebReferenceOptionsSerializationReader)reader).Read5_webReferenceOptions();
		}
	}
}
