using System;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000FA RID: 250
	internal abstract class XmlRawWriter : XmlWriter
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteStartDocument()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteStartDocument(bool standalone)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteEndDocument()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteFullEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000292DF File Offset: 0x000274DF
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (this.base64Encoder == null)
			{
				this.base64Encoder = new XmlRawWriterBase64Encoder(this);
			}
			this.base64Encoder.Encode(buffer, index, count);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00016C08 File Offset: 0x00014E08
		public override string LookupPrefix(string ns)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00016C08 File Offset: 0x00014E08
		public override WriteState WriteState
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00016C08 File Offset: 0x00014E08
		public override XmlSpace XmlSpace
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00016C08 File Offset: 0x00014E08
		public override string XmlLang
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteNmToken(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteName(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteQualifiedName(string localName, string ns)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00028CF6 File Offset: 0x00026EF6
		public override void WriteCData(string text)
		{
			this.WriteString(text);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00029303 File Offset: 0x00027503
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(new string(new char[] { ch }));
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002931A File Offset: 0x0002751A
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.WriteString(new string(new char[] { lowChar, highChar }));
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00028CF6 File Offset: 0x00026EF6
		public override void WriteWhitespace(string ws)
		{
			this.WriteString(ws);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00028CF6 File Offset: 0x00026EF6
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00029335 File Offset: 0x00027535
		public override void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, this.resolver));
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00028CF6 File Offset: 0x00026EF6
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002935C File Offset: 0x0002755C
		public override void WriteValue(DateTimeOffset value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000184B8 File Offset: 0x000166B8
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0002936A File Offset: 0x0002756A
		internal virtual IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x060008FF RID: 2303
		internal abstract void StartElementContent();

		// Token: 0x06000900 RID: 2304 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void OnRootElement(ConformanceLevel conformanceLevel)
		{
		}

		// Token: 0x06000901 RID: 2305
		internal abstract void WriteEndElement(string prefix, string localName, string ns);

		// Token: 0x06000902 RID: 2306 RVA: 0x00029373 File Offset: 0x00027573
		internal virtual void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002937E File Offset: 0x0002757E
		internal virtual void WriteQualifiedName(string prefix, string localName, string ns)
		{
			if (prefix.Length != 0)
			{
				this.WriteString(prefix);
				this.WriteString(":");
			}
			this.WriteString(localName);
		}

		// Token: 0x06000904 RID: 2308
		internal abstract void WriteNamespaceDeclaration(string prefix, string ns);

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0000226C File Offset: 0x0000046C
		internal virtual bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00010C4A File Offset: 0x0000EE4A
		internal virtual void WriteStartNamespaceDeclaration(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00010C4A File Offset: 0x0000EE4A
		internal virtual void WriteEndNamespaceDeclaration()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000293A1 File Offset: 0x000275A1
		internal virtual void WriteEndBase64()
		{
			this.base64Encoder.Flush();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000293AE File Offset: 0x000275AE
		internal virtual void Close(WriteState currentState)
		{
			this.Close();
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteStartDocumentAsync()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteEndDocumentAsync()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000293B6 File Offset: 0x000275B6
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteEndElementAsync()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteFullEndElementAsync()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000293BD File Offset: 0x000275BD
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			if (this.base64Encoder == null)
			{
				this.base64Encoder = new XmlRawWriterBase64Encoder(this);
			}
			return this.base64Encoder.EncodeAsync(buffer, index, count);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteNmTokenAsync(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteNameAsync(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000293E1 File Offset: 0x000275E1
		public override Task WriteCDataAsync(string text)
		{
			return this.WriteStringAsync(text);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000293EA File Offset: 0x000275EA
		public override Task WriteCharEntityAsync(char ch)
		{
			return this.WriteStringAsync(new string(new char[] { ch }));
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00029401 File Offset: 0x00027601
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			return this.WriteStringAsync(new string(new char[] { lowChar, highChar }));
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000293E1 File Offset: 0x000275E1
		public override Task WriteWhitespaceAsync(string ws)
		{
			return this.WriteStringAsync(ws);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002941C File Offset: 0x0002761C
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			return this.WriteStringAsync(new string(buffer, index, count));
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002941C File Offset: 0x0002761C
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			return this.WriteStringAsync(new string(buffer, index, count));
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000293E1 File Offset: 0x000275E1
		public override Task WriteRawAsync(string data)
		{
			return this.WriteStringAsync(data);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteAttributesAsync(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteNodeAsync(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00016C08 File Offset: 0x00014E08
		public override Task WriteNodeAsync(XPathNavigator navigator, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000293B6 File Offset: 0x000275B6
		internal virtual Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000293B6 File Offset: 0x000275B6
		internal virtual Task WriteXmlDeclarationAsync(string xmldecl)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0000A533 File Offset: 0x00008733
		internal virtual Task StartElementContentAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0000A533 File Offset: 0x00008733
		internal virtual Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002942C File Offset: 0x0002762C
		internal virtual Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			return this.WriteEndElementAsync(prefix, localName, ns);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00029438 File Offset: 0x00027638
		internal virtual async Task WriteQualifiedNameAsync(string prefix, string localName, string ns)
		{
			if (prefix.Length != 0)
			{
				await this.WriteStringAsync(prefix).ConfigureAwait(false);
				await this.WriteStringAsync(":").ConfigureAwait(false);
			}
			await this.WriteStringAsync(localName).ConfigureAwait(false);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000A533 File Offset: 0x00008733
		internal virtual Task WriteNamespaceDeclarationAsync(string prefix, string ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00010C4A File Offset: 0x0000EE4A
		internal virtual Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00010C4A File Offset: 0x0000EE4A
		internal virtual Task WriteEndNamespaceDeclarationAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002948D File Offset: 0x0002768D
		internal virtual Task WriteEndBase64Async()
		{
			return this.base64Encoder.FlushAsync();
		}

		// Token: 0x04000562 RID: 1378
		protected XmlRawWriterBase64Encoder base64Encoder;

		// Token: 0x04000563 RID: 1379
		protected IXmlNamespaceResolver resolver;
	}
}
