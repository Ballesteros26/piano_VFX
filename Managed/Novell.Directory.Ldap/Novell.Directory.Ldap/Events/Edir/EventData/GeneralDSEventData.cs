using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000C1 RID: 193
	public class GeneralDSEventData : BaseEdirEventData
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00015967 File Offset: 0x00013B67
		public int DSTime
		{
			get
			{
				return this.ds_time;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0001596F File Offset: 0x00013B6F
		public int MilliSeconds
		{
			get
			{
				return this.milli_seconds;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00015977 File Offset: 0x00013B77
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0001597F File Offset: 0x00013B7F
		public int CurrentProcess
		{
			get
			{
				return this.current_process;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00015987 File Offset: 0x00013B87
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0001598F File Offset: 0x00013B8F
		public int[] IntegerValues
		{
			get
			{
				return this.integer_values;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00015997 File Offset: 0x00013B97
		public string[] StringValues
		{
			get
			{
				return this.string_values;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000159A0 File Offset: 0x00013BA0
		public GeneralDSEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.ds_time = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_DSTIME);
			this.milli_seconds = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_MILLISEC);
			this.nVerb = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_VERB);
			this.current_process = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_CURRPROC);
			this.strPerpetratorDN = this.getTaggedStringValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_PERP);
			Asn1Tagged asn1Tagged = (Asn1Tagged)this.decoder.decode(this.decodedData, array);
			if (asn1Tagged.getIdentifier().Tag == 6)
			{
				Asn1Object[] array2 = this.getTaggedSequence(asn1Tagged, GeneralEventField.EVT_TAG_GEN_INTEGERS).toArray();
				this.integer_values = new int[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					this.integer_values[i] = ((Asn1Integer)array2[i]).intValue();
				}
				asn1Tagged = (Asn1Tagged)this.decoder.decode(this.decodedData, array);
			}
			else
			{
				this.integer_values = null;
			}
			if (asn1Tagged.getIdentifier().Tag == 7 && asn1Tagged.getIdentifier().Constructed)
			{
				Asn1Object[] array3 = this.getTaggedSequence(asn1Tagged, GeneralEventField.EVT_TAG_GEN_STRINGS).toArray();
				this.string_values = new string[array3.Length];
				for (int j = 0; j < array3.Length; j++)
				{
					this.string_values[j] = ((Asn1OctetString)array3[j]).stringValue();
				}
			}
			else
			{
				this.string_values = null;
			}
			base.DataInitDone();
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00015B6C File Offset: 0x00013D6C
		protected int getTaggedIntValue(Asn1Tagged tagvalue, GeneralEventField tagid)
		{
			Asn1Object asn1Object = tagvalue.taggedValue();
			if (tagid != (GeneralEventField)tagvalue.getIdentifier().Tag)
			{
				throw new IOException("Unknown Tagged Data");
			}
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			LBERDecoder lberdecoder = new LBERDecoder();
			int num = array.Length;
			return (int)lberdecoder.decodeNumeric(memoryStream, num);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00015BC4 File Offset: 0x00013DC4
		protected string getTaggedStringValue(Asn1Tagged tagvalue, GeneralEventField tagid)
		{
			Asn1Object asn1Object = tagvalue.taggedValue();
			if (tagid != (GeneralEventField)tagvalue.getIdentifier().Tag)
			{
				throw new IOException("Unknown Tagged Data");
			}
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			LBERDecoder lberdecoder = new LBERDecoder();
			int num = array.Length;
			return (string)lberdecoder.decodeCharacterString(memoryStream, num);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00015C1C File Offset: 0x00013E1C
		protected Asn1Sequence getTaggedSequence(Asn1Tagged tagvalue, GeneralEventField tagid)
		{
			Asn1Object asn1Object = tagvalue.taggedValue();
			if (tagid != (GeneralEventField)tagvalue.getIdentifier().Tag)
			{
				throw new IOException("Unknown Tagged Data");
			}
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			Asn1Decoder asn1Decoder = new LBERDecoder();
			int num = array.Length;
			return new Asn1Sequence(asn1Decoder, memoryStream, num);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00015C70 File Offset: 0x00013E70
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[GeneralDSEventData");
			stringBuilder.AppendFormat("(DSTime={0})", this.ds_time);
			stringBuilder.AppendFormat("(MilliSeconds={0})", this.milli_seconds);
			stringBuilder.AppendFormat("(verb={0})", this.nVerb);
			stringBuilder.AppendFormat("(currentProcess={0})", this.current_process);
			stringBuilder.AppendFormat("(PerpetartorDN={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(Integer Values={0})", this.integer_values);
			stringBuilder.AppendFormat("(String Values={0})", this.string_values);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400044F RID: 1103
		protected int ds_time;

		// Token: 0x04000450 RID: 1104
		protected int milli_seconds;

		// Token: 0x04000451 RID: 1105
		protected int nVerb;

		// Token: 0x04000452 RID: 1106
		protected int current_process;

		// Token: 0x04000453 RID: 1107
		protected string strPerpetratorDN;

		// Token: 0x04000454 RID: 1108
		protected int[] integer_values;

		// Token: 0x04000455 RID: 1109
		protected string[] string_values;
	}
}
