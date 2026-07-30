using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020001D8 RID: 472
	internal class XmlWrappingWriter : XmlWriter
	{
		// Token: 0x0600106D RID: 4205 RVA: 0x0006304A File Offset: 0x0006124A
		internal XmlWrappingWriter(XmlWriter baseWriter)
		{
			this.writer = baseWriter;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00063059 File Offset: 0x00061259
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writer.Settings;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00063066 File Offset: 0x00061266
		public override WriteState WriteState
		{
			get
			{
				return this.writer.WriteState;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00063073 File Offset: 0x00061273
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.writer.XmlSpace;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00063080 File Offset: 0x00061280
		public override string XmlLang
		{
			get
			{
				return this.writer.XmlLang;
			}
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0006308D File Offset: 0x0006128D
		public override void WriteStartDocument()
		{
			this.writer.WriteStartDocument();
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0006309A File Offset: 0x0006129A
		public override void WriteStartDocument(bool standalone)
		{
			this.writer.WriteStartDocument(standalone);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000630A8 File Offset: 0x000612A8
		public override void WriteEndDocument()
		{
			this.writer.WriteEndDocument();
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000630B5 File Offset: 0x000612B5
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x000630C7 File Offset: 0x000612C7
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.writer.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000630D7 File Offset: 0x000612D7
		public override void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x000630E4 File Offset: 0x000612E4
		public override void WriteFullEndElement()
		{
			this.writer.WriteFullEndElement();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000630F1 File Offset: 0x000612F1
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00063101 File Offset: 0x00061301
		public override void WriteEndAttribute()
		{
			this.writer.WriteEndAttribute();
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0006310E File Offset: 0x0006130E
		public override void WriteCData(string text)
		{
			this.writer.WriteCData(text);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0006311C File Offset: 0x0006131C
		public override void WriteComment(string text)
		{
			this.writer.WriteComment(text);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0006312A File Offset: 0x0006132A
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00063139 File Offset: 0x00061339
		public override void WriteEntityRef(string name)
		{
			this.writer.WriteEntityRef(name);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00063147 File Offset: 0x00061347
		public override void WriteCharEntity(char ch)
		{
			this.writer.WriteCharEntity(ch);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00063155 File Offset: 0x00061355
		public override void WriteWhitespace(string ws)
		{
			this.writer.WriteWhitespace(ws);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00063163 File Offset: 0x00061363
		public override void WriteString(string text)
		{
			this.writer.WriteString(text);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0001F202 File Offset: 0x0001D402
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00063171 File Offset: 0x00061371
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.writer.WriteChars(buffer, index, count);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00063181 File Offset: 0x00061381
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.writer.WriteRaw(buffer, index, count);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00063191 File Offset: 0x00061391
		public override void WriteRaw(string data)
		{
			this.writer.WriteRaw(data);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0006319F File Offset: 0x0006139F
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.writer.WriteBase64(buffer, index, count);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000631AF File Offset: 0x000613AF
		public override void Close()
		{
			this.writer.Close();
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x000631BC File Offset: 0x000613BC
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x000631C9 File Offset: 0x000613C9
		public override string LookupPrefix(string ns)
		{
			return this.writer.LookupPrefix(ns);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000631D7 File Offset: 0x000613D7
		public override void WriteValue(object value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000631E5 File Offset: 0x000613E5
		public override void WriteValue(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000631F3 File Offset: 0x000613F3
		public override void WriteValue(bool value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00063201 File Offset: 0x00061401
		public override void WriteValue(DateTime value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0006320F File Offset: 0x0006140F
		public override void WriteValue(DateTimeOffset value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0006321D File Offset: 0x0006141D
		public override void WriteValue(double value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0006322B File Offset: 0x0006142B
		public override void WriteValue(float value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00063239 File Offset: 0x00061439
		public override void WriteValue(decimal value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00063247 File Offset: 0x00061447
		public override void WriteValue(int value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00063255 File Offset: 0x00061455
		public override void WriteValue(long value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00063263 File Offset: 0x00061463
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDisposable)this.writer).Dispose();
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00063273 File Offset: 0x00061473
		public override Task WriteStartDocumentAsync()
		{
			return this.writer.WriteStartDocumentAsync();
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00063280 File Offset: 0x00061480
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			return this.writer.WriteStartDocumentAsync(standalone);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0006328E File Offset: 0x0006148E
		public override Task WriteEndDocumentAsync()
		{
			return this.writer.WriteEndDocumentAsync();
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0006329B File Offset: 0x0006149B
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			return this.writer.WriteDocTypeAsync(name, pubid, sysid, subset);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000632AD File Offset: 0x000614AD
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			return this.writer.WriteStartElementAsync(prefix, localName, ns);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000632BD File Offset: 0x000614BD
		public override Task WriteEndElementAsync()
		{
			return this.writer.WriteEndElementAsync();
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000632CA File Offset: 0x000614CA
		public override Task WriteFullEndElementAsync()
		{
			return this.writer.WriteFullEndElementAsync();
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000632D7 File Offset: 0x000614D7
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			return this.writer.WriteStartAttributeAsync(prefix, localName, ns);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000632E7 File Offset: 0x000614E7
		protected internal override Task WriteEndAttributeAsync()
		{
			return this.writer.WriteEndAttributeAsync();
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000632F4 File Offset: 0x000614F4
		public override Task WriteCDataAsync(string text)
		{
			return this.writer.WriteCDataAsync(text);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00063302 File Offset: 0x00061502
		public override Task WriteCommentAsync(string text)
		{
			return this.writer.WriteCommentAsync(text);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00063310 File Offset: 0x00061510
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			return this.writer.WriteProcessingInstructionAsync(name, text);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0006331F File Offset: 0x0006151F
		public override Task WriteEntityRefAsync(string name)
		{
			return this.writer.WriteEntityRefAsync(name);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0006332D File Offset: 0x0006152D
		public override Task WriteCharEntityAsync(char ch)
		{
			return this.writer.WriteCharEntityAsync(ch);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0006333B File Offset: 0x0006153B
		public override Task WriteWhitespaceAsync(string ws)
		{
			return this.writer.WriteWhitespaceAsync(ws);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00063349 File Offset: 0x00061549
		public override Task WriteStringAsync(string text)
		{
			return this.writer.WriteStringAsync(text);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0001F9B6 File Offset: 0x0001DBB6
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			return this.writer.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00063357 File Offset: 0x00061557
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			return this.writer.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00063367 File Offset: 0x00061567
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			return this.writer.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00063377 File Offset: 0x00061577
		public override Task WriteRawAsync(string data)
		{
			return this.writer.WriteRawAsync(data);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00063385 File Offset: 0x00061585
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			return this.writer.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00063395 File Offset: 0x00061595
		public override Task FlushAsync()
		{
			return this.writer.FlushAsync();
		}

		// Token: 0x04000BE6 RID: 3046
		protected XmlWriter writer;
	}
}
