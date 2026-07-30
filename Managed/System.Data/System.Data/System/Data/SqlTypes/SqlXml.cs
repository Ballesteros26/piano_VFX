using System;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents XML data stored in or retrieved from a server.</summary>
	// Token: 0x020002D6 RID: 726
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlXml : INullable, IXmlSerializable
	{
		/// <summary>Creates a new <see cref="T:System.Data.SqlTypes.SqlXml" /> instance.</summary>
		// Token: 0x0600217B RID: 8571 RVA: 0x0009CC26 File Offset: 0x0009AE26
		public SqlXml()
		{
			this.SetNull();
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0009CC26 File Offset: 0x0009AE26
		private SqlXml(bool fNull)
		{
			this.SetNull();
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlTypes.SqlXml" /> instance and associates it with the content of the supplied <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlReader" />-derived class instance to be used as the value of the new <see cref="T:System.Data.SqlTypes.SqlXml" /> instance.</param>
		// Token: 0x0600217D RID: 8573 RVA: 0x0009CC34 File Offset: 0x0009AE34
		public SqlXml(XmlReader value)
		{
			if (value == null)
			{
				this.SetNull();
				return;
			}
			this._fNotNull = true;
			this._firstCreateReader = true;
			this._stream = this.CreateMemoryStreamFromXmlReader(value);
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlTypes.SqlXml" /> instance, supplying the XML value from the supplied <see cref="T:System.IO.Stream" />-derived instance.</summary>
		/// <param name="value">A <see cref="T:System.IO.Stream" />-derived instance (such as <see cref="T:System.IO.FileStream" />) from which to load the <see cref="T:System.Data.SqlTypes.SqlXml" /> instance's Xml content.</param>
		// Token: 0x0600217E RID: 8574 RVA: 0x0009CC61 File Offset: 0x0009AE61
		public SqlXml(Stream value)
		{
			if (value == null)
			{
				this.SetNull();
				return;
			}
			this._firstCreateReader = true;
			this._fNotNull = true;
			this._stream = value;
		}

		/// <summary>Gets the value of the XML content of this <see cref="T:System.Data.SqlTypes.SqlXml" /> as a <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlReader" />-derived instance that contains the XML content. The actual type may vary (for example, the return value might be <see cref="T:System.Xml.XmlTextReader" />) depending on how the information is represented internally, on the server.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">Attempt was made to access this property on a null instance of <see cref="T:System.Data.SqlTypes.SqlXml" />.</exception>
		// Token: 0x0600217F RID: 8575 RVA: 0x0009CC88 File Offset: 0x0009AE88
		public XmlReader CreateReader()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			SqlXmlStreamWrapper sqlXmlStreamWrapper = new SqlXmlStreamWrapper(this._stream);
			if ((!this._firstCreateReader || sqlXmlStreamWrapper.CanSeek) && sqlXmlStreamWrapper.Position != 0L)
			{
				sqlXmlStreamWrapper.Seek(0L, SeekOrigin.Begin);
			}
			if (this._createSqlReaderMethodInfo == null)
			{
				this._createSqlReaderMethodInfo = SqlXml.CreateSqlReaderMethodInfo;
			}
			XmlReader xmlReader = SqlXml.CreateSqlXmlReader(sqlXmlStreamWrapper, false, false);
			this._firstCreateReader = false;
			return xmlReader;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0009CCFC File Offset: 0x0009AEFC
		internal static XmlReader CreateSqlXmlReader(Stream stream, bool closeInput = false, bool throwTargetInvocationExceptions = false)
		{
			XmlReaderSettings xmlReaderSettings = (closeInput ? SqlXml.s_defaultXmlReaderSettingsCloseInput : SqlXml.s_defaultXmlReaderSettings);
			XmlReader xmlReader;
			try
			{
				xmlReader = SqlXml.s_sqlReaderDelegate(stream, xmlReaderSettings, null);
			}
			catch (Exception ex)
			{
				if (!throwTargetInvocationExceptions || !ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw new TargetInvocationException(ex);
			}
			return xmlReader;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0009CD50 File Offset: 0x0009AF50
		private static Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader> CreateSqlReaderDelegate()
		{
			return (Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader>)SqlXml.CreateSqlReaderMethodInfo.CreateDelegate(typeof(Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader>));
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0009CD6B File Offset: 0x0009AF6B
		private static MethodInfo CreateSqlReaderMethodInfo
		{
			get
			{
				if (SqlXml.s_createSqlReaderMethodInfo == null)
				{
					SqlXml.s_createSqlReaderMethodInfo = typeof(XmlReader).GetMethod("CreateSqlReader", BindingFlags.Static | BindingFlags.NonPublic);
				}
				return SqlXml.s_createSqlReaderMethodInfo;
			}
		}

		/// <summary>Indicates whether this instance represents a null <see cref="T:System.Data.SqlTypes.SqlXml" /> value.</summary>
		/// <returns>true if Value is null. Otherwise, false.</returns>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0009CD9A File Offset: 0x0009AF9A
		public bool IsNull
		{
			get
			{
				return !this._fNotNull;
			}
		}

		/// <summary>Gets the string representation of the XML content of this <see cref="T:System.Data.SqlTypes.SqlXml" /> instance.</summary>
		/// <returns>The string representation of the XML content.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x0009CDA8 File Offset: 0x0009AFA8
		public string Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				StringWriter stringWriter = new StringWriter(null);
				XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
				{
					CloseOutput = false,
					ConformanceLevel = ConformanceLevel.Fragment
				});
				XmlReader xmlReader = this.CreateReader();
				if (xmlReader.ReadState == ReadState.Initial)
				{
					xmlReader.Read();
				}
				while (!xmlReader.EOF)
				{
					xmlWriter.WriteNode(xmlReader, true);
				}
				xmlWriter.Flush();
				return stringWriter.ToString();
			}
		}

		/// <summary>Represents a null instance of the <see cref="T:System.Data.SqlTypes.SqlXml" /> type.</summary>
		/// <returns>A null instance of the <see cref="T:System.Data.SqlTypes.SqlXml" /> type.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x0009CE1A File Offset: 0x0009B01A
		public static SqlXml Null
		{
			get
			{
				return new SqlXml(true);
			}
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0009CE22 File Offset: 0x0009B022
		private void SetNull()
		{
			this._fNotNull = false;
			this._stream = null;
			this._firstCreateReader = true;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0009CE3C File Offset: 0x0009B03C
		private Stream CreateMemoryStreamFromXmlReader(XmlReader reader)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.CloseOutput = false;
			xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
			xmlWriterSettings.Encoding = Encoding.GetEncoding("utf-16");
			xmlWriterSettings.OmitXmlDeclaration = true;
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
			if (reader.ReadState == ReadState.Closed)
			{
				throw new InvalidOperationException(SQLResource.ClosedXmlReaderMessage);
			}
			if (reader.ReadState == ReadState.Initial)
			{
				reader.Read();
			}
			while (!reader.EOF)
			{
				xmlWriter.WriteNode(reader, true);
			}
			xmlWriter.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.</returns>
		// Token: 0x06002188 RID: 8584 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" />.</summary>
		/// <param name="r">An XmlReader.</param>
		// Token: 0x06002189 RID: 8585 RVA: 0x0009CEC8 File Offset: 0x0009B0C8
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadInnerXml();
				this.SetNull();
				return;
			}
			this._fNotNull = true;
			this._firstCreateReader = true;
			this._stream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(this._stream);
			streamWriter.Write(r.ReadInnerXml());
			streamWriter.Flush();
			if (this._stream.CanSeek)
			{
				this._stream.Seek(0L, SeekOrigin.Begin);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" />.</summary>
		/// <param name="writer">An XmlWriter</param>
		// Token: 0x0600218A RID: 8586 RVA: 0x0009CF50 File Offset: 0x0009B150
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			}
			else
			{
				XmlReader xmlReader = this.CreateReader();
				if (xmlReader.ReadState == ReadState.Initial)
				{
					xmlReader.Read();
				}
				while (!xmlReader.EOF)
				{
					writer.WriteNode(xmlReader, true);
				}
			}
			writer.Flush();
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />. </returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x0600218B RID: 8587 RVA: 0x0009CFAF File Offset: 0x0009B1AF
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001685 RID: 5765
		private static readonly Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader> s_sqlReaderDelegate = SqlXml.CreateSqlReaderDelegate();

		// Token: 0x04001686 RID: 5766
		private static readonly XmlReaderSettings s_defaultXmlReaderSettings = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment
		};

		// Token: 0x04001687 RID: 5767
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsCloseInput = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x04001688 RID: 5768
		private static MethodInfo s_createSqlReaderMethodInfo;

		// Token: 0x04001689 RID: 5769
		private MethodInfo _createSqlReaderMethodInfo;

		// Token: 0x0400168A RID: 5770
		private bool _fNotNull;

		// Token: 0x0400168B RID: 5771
		private Stream _stream;

		// Token: 0x0400168C RID: 5772
		private bool _firstCreateReader;
	}
}
