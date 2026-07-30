using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000BF RID: 191
	public class DebugParameter
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001550E File Offset: 0x0001370E
		public DebugParameterType DebugType
		{
			get
			{
				return this.debug_type;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00015516 File Offset: 0x00013716
		public object Data
		{
			get
			{
				return this.objData;
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00015520 File Offset: 0x00013720
		public DebugParameter(Asn1Tagged dseObject)
		{
			switch (dseObject.getIdentifier().Tag)
			{
			case 1:
			case 4:
				this.objData = this.getTaggedIntValue(dseObject);
				break;
			case 2:
				this.objData = ((Asn1OctetString)dseObject.taggedValue()).stringValue();
				break;
			case 3:
				this.objData = ((Asn1OctetString)dseObject.taggedValue()).byteValue();
				break;
			case 5:
				this.objData = new ReferralAddress(this.getTaggedSequence(dseObject));
				break;
			case 6:
				this.objData = new DSETimeStamp(this.getTaggedSequence(dseObject));
				break;
			case 7:
			{
				ArrayList arrayList = new ArrayList();
				Asn1Sequence taggedSequence = this.getTaggedSequence(dseObject);
				int num = ((Asn1Integer)taggedSequence.get_Renamed(0)).intValue();
				if (num > 0)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)taggedSequence.get_Renamed(1);
					for (int i = 0; i < num; i++)
					{
						arrayList.Add(new DSETimeStamp((Asn1Sequence)asn1Sequence.get_Renamed(i)));
					}
				}
				this.objData = arrayList;
				break;
			}
			default:
				throw new IOException("Unknown Tag in DebugParameter..");
			}
			this.debug_type = (DebugParameterType)dseObject.getIdentifier().Tag;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00015664 File Offset: 0x00013864
		protected int getTaggedIntValue(Asn1Tagged tagVal)
		{
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)tagVal.taggedValue()).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			return (int)new LBERDecoder().decodeNumeric(memoryStream, array.Length);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000156A4 File Offset: 0x000138A4
		protected Asn1Sequence getTaggedSequence(Asn1Tagged tagVal)
		{
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)tagVal.taggedValue()).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			return new Asn1Sequence(new LBERDecoder(), memoryStream, array.Length);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000156DC File Offset: 0x000138DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DebugParameter");
			if (Enum.IsDefined(this.debug_type.GetType(), this.debug_type))
			{
				stringBuilder.AppendFormat("(type={0},", this.debug_type);
				stringBuilder.AppendFormat("value={0})", this.objData);
			}
			else
			{
				stringBuilder.Append("(type=Unknown)");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000446 RID: 1094
		protected DebugParameterType debug_type;

		// Token: 0x04000447 RID: 1095
		protected object objData;
	}
}
