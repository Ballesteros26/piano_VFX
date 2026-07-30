using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Xsl.Runtime;

namespace System.Xml
{
	// Token: 0x020000F6 RID: 246
	internal sealed class XmlEventCache : XmlRawWriter
	{
		// Token: 0x06000890 RID: 2192 RVA: 0x000286BE File Offset: 0x000268BE
		public XmlEventCache(string baseUri, bool hasRootNode)
		{
			this.baseUri = baseUri;
			this.hasRootNode = hasRootNode;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000286D4 File Offset: 0x000268D4
		public void EndEvents()
		{
			if (this.singleText.Count == 0)
			{
				this.AddEvent(XmlEventCache.XmlEventType.Unknown);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000286EA File Offset: 0x000268EA
		public string BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x000286F2 File Offset: 0x000268F2
		public bool HasRootNode
		{
			get
			{
				return this.hasRootNode;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000286FC File Offset: 0x000268FC
		public void EventsToWriter(XmlWriter writer)
		{
			if (this.singleText.Count != 0)
			{
				writer.WriteString(this.singleText.GetResult());
				return;
			}
			XmlRawWriter xmlRawWriter = writer as XmlRawWriter;
			for (int i = 0; i < this.pages.Count; i++)
			{
				XmlEventCache.XmlEvent[] array = this.pages[i];
				for (int j = 0; j < array.Length; j++)
				{
					switch (array[j].EventType)
					{
					case XmlEventCache.XmlEventType.Unknown:
						return;
					case XmlEventCache.XmlEventType.DocType:
						writer.WriteDocType(array[j].String1, array[j].String2, array[j].String3, (string)array[j].Object);
						break;
					case XmlEventCache.XmlEventType.StartElem:
						writer.WriteStartElement(array[j].String1, array[j].String2, array[j].String3);
						break;
					case XmlEventCache.XmlEventType.StartAttr:
						writer.WriteStartAttribute(array[j].String1, array[j].String2, array[j].String3);
						break;
					case XmlEventCache.XmlEventType.EndAttr:
						writer.WriteEndAttribute();
						break;
					case XmlEventCache.XmlEventType.CData:
						writer.WriteCData(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.Comment:
						writer.WriteComment(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.PI:
						writer.WriteProcessingInstruction(array[j].String1, array[j].String2);
						break;
					case XmlEventCache.XmlEventType.Whitespace:
						writer.WriteWhitespace(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.String:
						writer.WriteString(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.Raw:
						writer.WriteRaw(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.EntRef:
						writer.WriteEntityRef(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.CharEnt:
						writer.WriteCharEntity((char)array[j].Object);
						break;
					case XmlEventCache.XmlEventType.SurrCharEnt:
					{
						char[] array2 = (char[])array[j].Object;
						writer.WriteSurrogateCharEntity(array2[0], array2[1]);
						break;
					}
					case XmlEventCache.XmlEventType.Base64:
					{
						byte[] array3 = (byte[])array[j].Object;
						writer.WriteBase64(array3, 0, array3.Length);
						break;
					}
					case XmlEventCache.XmlEventType.BinHex:
					{
						byte[] array3 = (byte[])array[j].Object;
						writer.WriteBinHex(array3, 0, array3.Length);
						break;
					}
					case XmlEventCache.XmlEventType.XmlDecl1:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteXmlDeclaration((XmlStandalone)array[j].Object);
						}
						break;
					case XmlEventCache.XmlEventType.XmlDecl2:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteXmlDeclaration(array[j].String1);
						}
						break;
					case XmlEventCache.XmlEventType.StartContent:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.StartElementContent();
						}
						break;
					case XmlEventCache.XmlEventType.EndElem:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteEndElement(array[j].String1, array[j].String2, array[j].String3);
						}
						else
						{
							writer.WriteEndElement();
						}
						break;
					case XmlEventCache.XmlEventType.FullEndElem:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteFullEndElement(array[j].String1, array[j].String2, array[j].String3);
						}
						else
						{
							writer.WriteFullEndElement();
						}
						break;
					case XmlEventCache.XmlEventType.Nmsp:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteNamespaceDeclaration(array[j].String1, array[j].String2);
						}
						else
						{
							writer.WriteAttributeString("xmlns", array[j].String1, "http://www.w3.org/2000/xmlns/", array[j].String2);
						}
						break;
					case XmlEventCache.XmlEventType.EndBase64:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteEndBase64();
						}
						break;
					case XmlEventCache.XmlEventType.Close:
						writer.Close();
						break;
					case XmlEventCache.XmlEventType.Flush:
						writer.Flush();
						break;
					case XmlEventCache.XmlEventType.Dispose:
						((IDisposable)writer).Dispose();
						break;
					}
				}
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00028B0C File Offset: 0x00026D0C
		public string EventsToString()
		{
			if (this.singleText.Count != 0)
			{
				return this.singleText.GetResult();
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			for (int i = 0; i < this.pages.Count; i++)
			{
				XmlEventCache.XmlEvent[] array = this.pages[i];
				for (int j = 0; j < array.Length; j++)
				{
					switch (array[j].EventType)
					{
					case XmlEventCache.XmlEventType.Unknown:
						return stringBuilder.ToString();
					case XmlEventCache.XmlEventType.StartAttr:
						flag = true;
						break;
					case XmlEventCache.XmlEventType.EndAttr:
						flag = false;
						break;
					case XmlEventCache.XmlEventType.CData:
					case XmlEventCache.XmlEventType.Whitespace:
					case XmlEventCache.XmlEventType.String:
					case XmlEventCache.XmlEventType.Raw:
						if (!flag)
						{
							stringBuilder.Append(array[j].String1);
						}
						break;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0000365F File Offset: 0x0000185F
		public override XmlWriterSettings Settings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00028BE2 File Offset: 0x00026DE2
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.AddEvent(XmlEventCache.XmlEventType.DocType, name, pubid, sysid, subset);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00028BF0 File Offset: 0x00026DF0
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartElem, prefix, localName, ns);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00028BFC File Offset: 0x00026DFC
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartAttr, prefix, localName, ns);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00028C08 File Offset: 0x00026E08
		public override void WriteEndAttribute()
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndAttr);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00028C11 File Offset: 0x00026E11
		public override void WriteCData(string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.CData, text);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00028C1B File Offset: 0x00026E1B
		public override void WriteComment(string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Comment, text);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00028C25 File Offset: 0x00026E25
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.PI, name, text);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00028C30 File Offset: 0x00026E30
		public override void WriteWhitespace(string ws)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Whitespace, ws);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00028C3A File Offset: 0x00026E3A
		public override void WriteString(string text)
		{
			if (this.pages == null)
			{
				this.singleText.ConcatNoDelimiter(text);
				return;
			}
			this.AddEvent(XmlEventCache.XmlEventType.String, text);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001C9DF File Offset: 0x0001ABDF
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteRaw(new string(buffer, index, count));
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00028C5A File Offset: 0x00026E5A
		public override void WriteRaw(string data)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Raw, data);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00028C65 File Offset: 0x00026E65
		public override void WriteEntityRef(string name)
		{
			this.AddEvent(XmlEventCache.XmlEventType.EntRef, name);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00028C70 File Offset: 0x00026E70
		public override void WriteCharEntity(char ch)
		{
			this.AddEvent(XmlEventCache.XmlEventType.CharEnt, ch);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00028C80 File Offset: 0x00026E80
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			char[] array = new char[] { lowChar, highChar };
			this.AddEvent(XmlEventCache.XmlEventType.SurrCharEnt, array);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00028CA5 File Offset: 0x00026EA5
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Base64, XmlEventCache.ToBytes(buffer, index, count));
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00028CB7 File Offset: 0x00026EB7
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.AddEvent(XmlEventCache.XmlEventType.BinHex, XmlEventCache.ToBytes(buffer, index, count));
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00028CC9 File Offset: 0x00026EC9
		public override void Close()
		{
			this.AddEvent(XmlEventCache.XmlEventType.Close);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00028CD3 File Offset: 0x00026ED3
		public override void Flush()
		{
			this.AddEvent(XmlEventCache.XmlEventType.Flush);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00028CDD File Offset: 0x00026EDD
		public override void WriteValue(object value)
		{
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, this.resolver));
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00028CF6 File Offset: 0x00026EF6
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00028D00 File Offset: 0x00026F00
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.AddEvent(XmlEventCache.XmlEventType.Dispose);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00028D34 File Offset: 0x00026F34
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.AddEvent(XmlEventCache.XmlEventType.XmlDecl1, standalone);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00028D44 File Offset: 0x00026F44
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.AddEvent(XmlEventCache.XmlEventType.XmlDecl2, xmldecl);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00028D4F File Offset: 0x00026F4F
		internal override void StartElementContent()
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartContent);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00028D59 File Offset: 0x00026F59
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndElem, prefix, localName, ns);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00028D66 File Offset: 0x00026F66
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.FullEndElem, prefix, localName, ns);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00028D73 File Offset: 0x00026F73
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Nmsp, prefix, ns);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00028D7F File Offset: 0x00026F7F
		internal override void WriteEndBase64()
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndBase64);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00028D8C File Offset: 0x00026F8C
		private void AddEvent(XmlEventCache.XmlEventType eventType)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00028DB4 File Offset: 0x00026FB4
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00028DDC File Offset: 0x00026FDC
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00028E04 File Offset: 0x00027004
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2, s3);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00028E30 File Offset: 0x00027030
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3, object o)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2, s3, o);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00028E5C File Offset: 0x0002705C
		private void AddEvent(XmlEventCache.XmlEventType eventType, object o)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, o);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00028E84 File Offset: 0x00027084
		private int NewEvent()
		{
			if (this.pages == null)
			{
				this.pages = new List<XmlEventCache.XmlEvent[]>();
				this.pageCurr = new XmlEventCache.XmlEvent[32];
				this.pages.Add(this.pageCurr);
				if (this.singleText.Count != 0)
				{
					this.pageCurr[0].InitEvent(XmlEventCache.XmlEventType.String, this.singleText.GetResult());
					this.pageSize++;
					this.singleText.Clear();
				}
			}
			else if (this.pageSize >= this.pageCurr.Length)
			{
				this.pageCurr = new XmlEventCache.XmlEvent[this.pageSize * 2];
				this.pages.Add(this.pageCurr);
				this.pageSize = 0;
			}
			int num = this.pageSize;
			this.pageSize = num + 1;
			return num;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00028F54 File Offset: 0x00027154
		private static byte[] ToBytes(byte[] buffer, int index, int count)
		{
			if (index != 0 || count != buffer.Length)
			{
				if (buffer.Length - index > count)
				{
					count = buffer.Length - index;
				}
				byte[] array = new byte[count];
				Array.Copy(buffer, index, array, 0, count);
				return array;
			}
			return buffer;
		}

		// Token: 0x04000531 RID: 1329
		private List<XmlEventCache.XmlEvent[]> pages;

		// Token: 0x04000532 RID: 1330
		private XmlEventCache.XmlEvent[] pageCurr;

		// Token: 0x04000533 RID: 1331
		private int pageSize;

		// Token: 0x04000534 RID: 1332
		private bool hasRootNode;

		// Token: 0x04000535 RID: 1333
		private StringConcat singleText;

		// Token: 0x04000536 RID: 1334
		private string baseUri;

		// Token: 0x04000537 RID: 1335
		private const int InitialPageSize = 32;

		// Token: 0x020000F7 RID: 247
		private enum XmlEventType
		{
			// Token: 0x04000539 RID: 1337
			Unknown,
			// Token: 0x0400053A RID: 1338
			DocType,
			// Token: 0x0400053B RID: 1339
			StartElem,
			// Token: 0x0400053C RID: 1340
			StartAttr,
			// Token: 0x0400053D RID: 1341
			EndAttr,
			// Token: 0x0400053E RID: 1342
			CData,
			// Token: 0x0400053F RID: 1343
			Comment,
			// Token: 0x04000540 RID: 1344
			PI,
			// Token: 0x04000541 RID: 1345
			Whitespace,
			// Token: 0x04000542 RID: 1346
			String,
			// Token: 0x04000543 RID: 1347
			Raw,
			// Token: 0x04000544 RID: 1348
			EntRef,
			// Token: 0x04000545 RID: 1349
			CharEnt,
			// Token: 0x04000546 RID: 1350
			SurrCharEnt,
			// Token: 0x04000547 RID: 1351
			Base64,
			// Token: 0x04000548 RID: 1352
			BinHex,
			// Token: 0x04000549 RID: 1353
			XmlDecl1,
			// Token: 0x0400054A RID: 1354
			XmlDecl2,
			// Token: 0x0400054B RID: 1355
			StartContent,
			// Token: 0x0400054C RID: 1356
			EndElem,
			// Token: 0x0400054D RID: 1357
			FullEndElem,
			// Token: 0x0400054E RID: 1358
			Nmsp,
			// Token: 0x0400054F RID: 1359
			EndBase64,
			// Token: 0x04000550 RID: 1360
			Close,
			// Token: 0x04000551 RID: 1361
			Flush,
			// Token: 0x04000552 RID: 1362
			Dispose
		}

		// Token: 0x020000F8 RID: 248
		private struct XmlEvent
		{
			// Token: 0x060008BC RID: 2236 RVA: 0x00028F8D File Offset: 0x0002718D
			public void InitEvent(XmlEventCache.XmlEventType eventType)
			{
				this.eventType = eventType;
			}

			// Token: 0x060008BD RID: 2237 RVA: 0x00028F96 File Offset: 0x00027196
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1)
			{
				this.eventType = eventType;
				this.s1 = s1;
			}

			// Token: 0x060008BE RID: 2238 RVA: 0x00028FA6 File Offset: 0x000271A6
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
			}

			// Token: 0x060008BF RID: 2239 RVA: 0x00028FBD File Offset: 0x000271BD
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
				this.s3 = s3;
			}

			// Token: 0x060008C0 RID: 2240 RVA: 0x00028FDC File Offset: 0x000271DC
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3, object o)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
				this.s3 = s3;
				this.o = o;
			}

			// Token: 0x060008C1 RID: 2241 RVA: 0x00029003 File Offset: 0x00027203
			public void InitEvent(XmlEventCache.XmlEventType eventType, object o)
			{
				this.eventType = eventType;
				this.o = o;
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00029013 File Offset: 0x00027213
			public XmlEventCache.XmlEventType EventType
			{
				get
				{
					return this.eventType;
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0002901B File Offset: 0x0002721B
			public string String1
			{
				get
				{
					return this.s1;
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00029023 File Offset: 0x00027223
			public string String2
			{
				get
				{
					return this.s2;
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0002902B File Offset: 0x0002722B
			public string String3
			{
				get
				{
					return this.s3;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00029033 File Offset: 0x00027233
			public object Object
			{
				get
				{
					return this.o;
				}
			}

			// Token: 0x04000553 RID: 1363
			private XmlEventCache.XmlEventType eventType;

			// Token: 0x04000554 RID: 1364
			private string s1;

			// Token: 0x04000555 RID: 1365
			private string s2;

			// Token: 0x04000556 RID: 1366
			private string s3;

			// Token: 0x04000557 RID: 1367
			private object o;
		}
	}
}
