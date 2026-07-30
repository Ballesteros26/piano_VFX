using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap
{
	// Token: 0x02000011 RID: 17
	internal class SoapWriter : IComparer
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003FC0 File Offset: 0x000021C0
		~SoapWriter()
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003FE8 File Offset: 0x000021E8
		internal SoapWriter(Stream outStream, ISurrogateSelector selector, StreamingContext context, ISoapMessage soapMessage)
		{
			this._xmlWriter = new XmlTextWriter(outStream, null);
			this._xmlWriter.Formatting = Formatting.Indented;
			this._surrogateSelector = selector;
			this._context = context;
			this._manager = new SerializationObjectManager(this._context);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000407B File Offset: 0x0000227B
		public SoapTypeMapper Mapper
		{
			get
			{
				return this._mapper;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004083 File Offset: 0x00002283
		public XmlTextWriter XmlWriter
		{
			get
			{
				return this._xmlWriter;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000408B File Offset: 0x0000228B
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00004093 File Offset: 0x00002293
		internal FormatterAssemblyStyle AssemblyFormat
		{
			get
			{
				return this._assemblyFormat;
			}
			set
			{
				this._assemblyFormat = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000409C File Offset: 0x0000229C
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000040A4 File Offset: 0x000022A4
		internal FormatterTypeStyle TypeFormat
		{
			get
			{
				return this._typeFormat;
			}
			set
			{
				this._typeFormat = value;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000040AD File Offset: 0x000022AD
		private void Id(long id)
		{
			this._xmlWriter.WriteAttributeString(null, "id", null, "ref-" + id.ToString());
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000040D2 File Offset: 0x000022D2
		private void Href(long href)
		{
			this._xmlWriter.WriteAttributeString(null, "href", null, "#ref-" + href.ToString());
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000040F7 File Offset: 0x000022F7
		private void Null()
		{
			this._xmlWriter.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "1");
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004118 File Offset: 0x00002318
		private bool IsEncodingNeeded(object componentObject, Type componentType)
		{
			if (componentObject == null)
			{
				return false;
			}
			if (this._typeFormat == FormatterTypeStyle.TypesAlways)
			{
				return true;
			}
			if (componentType == null)
			{
				componentType = componentObject.GetType();
				return !componentType.IsPrimitive && !(componentType == typeof(string));
			}
			return componentType == typeof(object) || componentType != componentObject.GetType();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004188 File Offset: 0x00002388
		internal void Serialize(object objGraph, Header[] headers, FormatterTypeStyle typeFormat, FormatterAssemblyStyle assemblyFormat)
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			try
			{
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
				this.Serialize_inner(objGraph, headers, typeFormat, assemblyFormat);
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
			this._manager.RaiseOnSerializedEvent();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000041E4 File Offset: 0x000023E4
		private void Serialize_inner(object objGraph, Header[] headers, FormatterTypeStyle typeFormat, FormatterAssemblyStyle assemblyFormat)
		{
			this._typeFormat = typeFormat;
			this._assemblyFormat = assemblyFormat;
			this._mapper = new SoapTypeMapper(this._xmlWriter, assemblyFormat, typeFormat);
			this._xmlWriter.WriteStartElement(SoapTypeMapper.SoapEnvelopePrefix, "Envelope", SoapTypeMapper.SoapEnvelopeNamespace);
			this._xmlWriter.WriteAttributeString("xmlns", "xsi", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2001/XMLSchema-instance");
			this._xmlWriter.WriteAttributeString("xmlns", "xsd", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2001/XMLSchema");
			this._xmlWriter.WriteAttributeString("xmlns", SoapTypeMapper.SoapEncodingPrefix, "http://www.w3.org/2000/xmlns/", SoapTypeMapper.SoapEncodingNamespace);
			this._xmlWriter.WriteAttributeString("xmlns", SoapTypeMapper.SoapEnvelopePrefix, "http://www.w3.org/2000/xmlns/", SoapTypeMapper.SoapEnvelopeNamespace);
			this._xmlWriter.WriteAttributeString("xmlns", "clr", "http://www.w3.org/2000/xmlns/", SoapServices.XmlNsForClrType);
			this._xmlWriter.WriteAttributeString(SoapTypeMapper.SoapEnvelopePrefix, "encodingStyle", SoapTypeMapper.SoapEnvelopeNamespace, "http://schemas.xmlsoap.org/soap/encoding/");
			ISoapMessage soapMessage = objGraph as ISoapMessage;
			if (soapMessage != null)
			{
				headers = soapMessage.Headers;
			}
			if (headers != null && headers.Length != 0)
			{
				this._xmlWriter.WriteStartElement(SoapTypeMapper.SoapEnvelopePrefix, "Header", SoapTypeMapper.SoapEnvelopeNamespace);
				foreach (Header header in headers)
				{
					this.SerializeHeader(header);
				}
				this.WriteObjectQueue();
				this._xmlWriter.WriteEndElement();
			}
			this._xmlWriter.WriteStartElement(SoapTypeMapper.SoapEnvelopePrefix, "Body", SoapTypeMapper.SoapEnvelopeNamespace);
			bool flag = false;
			if (soapMessage != null)
			{
				this.SerializeMessage(soapMessage);
			}
			else
			{
				this._objectQueue.Enqueue(new SoapWriter.EnqueuedObject(objGraph, this.idGen.GetId(objGraph, out flag)));
			}
			this.WriteObjectQueue();
			this._xmlWriter.WriteFullEndElement();
			this._xmlWriter.WriteFullEndElement();
			this._xmlWriter.Flush();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000043BC File Offset: 0x000025BC
		private void WriteObjectQueue()
		{
			while (this._objectQueue.Count > 0)
			{
				SoapWriter.EnqueuedObject enqueuedObject = (SoapWriter.EnqueuedObject)this._objectQueue.Dequeue();
				object @object = enqueuedObject.Object;
				Type type = @object.GetType();
				if (!type.IsValueType)
				{
					this._objectToIdTable[@object] = enqueuedObject.Id;
				}
				if (type.IsArray)
				{
					this.SerializeArray((Array)@object, enqueuedObject.Id);
				}
				else
				{
					this.SerializeObject(@object, enqueuedObject.Id);
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004444 File Offset: 0x00002644
		private void SerializeMessage(ISoapMessage message)
		{
			string text = ((message.XmlNameSpace != null) ? message.XmlNameSpace : SoapWriter.defaultMessageNamespace);
			this._xmlWriter.WriteStartElement("i2", message.MethodName, text);
			bool flag;
			this.Id(this.idGen.GetId(message, out flag));
			string[] paramNames = message.ParamNames;
			object[] paramValues = message.ParamValues;
			int num = ((paramNames != null) ? paramNames.Length : 0);
			for (int i = 0; i < num; i++)
			{
				this._xmlWriter.WriteStartElement(paramNames[i]);
				this.SerializeComponent(paramValues[i], true);
				this._xmlWriter.WriteEndElement();
			}
			this._xmlWriter.WriteFullEndElement();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000044EC File Offset: 0x000026EC
		private void SerializeHeader(Header header)
		{
			string text = ((header.HeaderNamespace != null) ? header.HeaderNamespace : "http://schemas.microsoft.com/clr/soap");
			this._xmlWriter.WriteStartElement("h4", header.Name, text);
			if (header.MustUnderstand)
			{
				this._xmlWriter.WriteAttributeString("mustUnderstand", SoapTypeMapper.SoapEnvelopeNamespace, "1");
			}
			this._xmlWriter.WriteAttributeString("root", SoapTypeMapper.SoapEncodingNamespace, "1");
			if (header.Name == "__MethodSignature")
			{
				Type[] array = header.Value as Type[];
				if (array == null)
				{
					throw new SerializationException("Invalid method signature.");
				}
				this.SerializeComponent(new MethodSignature(array), true);
			}
			else
			{
				this.SerializeComponent(header.Value, true);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000045B8 File Offset: 0x000027B8
		private void SerializeObject(object currentObject, long currentObjectId)
		{
			bool flag = false;
			ISerializationSurrogate serializationSurrogate = null;
			if (this._surrogateSelector != null)
			{
				ISurrogateSelector surrogateSelector;
				serializationSurrogate = this._surrogateSelector.GetSurrogate(currentObject.GetType(), this._context, out surrogateSelector);
			}
			if (currentObject is ISerializable || serializationSurrogate != null)
			{
				flag = true;
			}
			this._manager.RegisterObject(currentObject);
			if (flag)
			{
				this.SerializeISerializableObject(currentObject, currentObjectId, serializationSurrogate);
				return;
			}
			if (!currentObject.GetType().IsSerializable)
			{
				throw new SerializationException(string.Format("Type {0} in assembly {1} is not marked as serializable.", currentObject.GetType(), currentObject.GetType().Assembly.FullName));
			}
			this.SerializeSimpleObject(currentObject, currentObjectId);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000464C File Offset: 0x0000284C
		public int Compare(object x, object y)
		{
			MemberInfo memberInfo = x as MemberInfo;
			MemberInfo memberInfo2 = y as MemberInfo;
			return string.Compare(memberInfo.Name, memberInfo2.Name);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004678 File Offset: 0x00002878
		private void SerializeSimpleObject(object currentObject, long currentObjectId)
		{
			Type type = currentObject.GetType();
			if (currentObjectId > 0L)
			{
				Element xmlElement = this._mapper.GetXmlElement(type);
				this._xmlWriter.WriteStartElement(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI);
				this.Id(currentObjectId);
			}
			if (type == typeof(TimeSpan))
			{
				this._xmlWriter.WriteString(SoapTypeMapper.GetXsdValue(currentObject));
			}
			else if (type == typeof(string))
			{
				this._xmlWriter.WriteString(currentObject.ToString());
			}
			else
			{
				MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(type, this._context);
				object[] objectData = FormatterServices.GetObjectData(currentObject, serializableMembers);
				for (int i = 0; i < serializableMembers.Length; i++)
				{
					FieldInfo fieldInfo = (FieldInfo)serializableMembers[i];
					SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
					this._xmlWriter.WriteStartElement(XmlConvert.EncodeLocalName(soapFieldAttribute.XmlElementName));
					this.SerializeComponent(objectData[i], this.IsEncodingNeeded(objectData[i], fieldInfo.FieldType));
					this._xmlWriter.WriteEndElement();
				}
			}
			if (currentObjectId > 0L)
			{
				this._xmlWriter.WriteFullEndElement();
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000479C File Offset: 0x0000299C
		private void SerializeISerializableObject(object currentObject, long currentObjectId, ISerializationSurrogate surrogate)
		{
			SerializationInfo serializationInfo = new SerializationInfo(currentObject.GetType(), new FormatterConverter());
			ISerializable serializable = currentObject as ISerializable;
			if (surrogate != null)
			{
				surrogate.GetObjectData(currentObject, serializationInfo, this._context);
			}
			else
			{
				serializable.GetObjectData(serializationInfo, this._context);
			}
			if (currentObjectId > 0L)
			{
				Element xmlElement = this._mapper.GetXmlElement(serializationInfo.FullTypeName, serializationInfo.AssemblyName);
				this._xmlWriter.WriteStartElement(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI);
				this.Id(currentObjectId);
			}
			foreach (SerializationEntry serializationEntry in serializationInfo)
			{
				this._xmlWriter.WriteStartElement(XmlConvert.EncodeLocalName(serializationEntry.Name));
				this.SerializeComponent(serializationEntry.Value, this.IsEncodingNeeded(serializationEntry.Value, null));
				this._xmlWriter.WriteEndElement();
			}
			if (currentObjectId > 0L)
			{
				this._xmlWriter.WriteFullEndElement();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004888 File Offset: 0x00002A88
		private void SerializeArray(Array currentArray, long currentArrayId)
		{
			Element xmlElement = this._mapper.GetXmlElement(typeof(Array));
			Type elementType = currentArray.GetType().GetElementType();
			Element xmlElement2 = this._mapper.GetXmlElement(elementType);
			this._xmlWriter.WriteStartElement(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI);
			if (currentArrayId > 0L)
			{
				this.Id(currentArrayId);
			}
			if (elementType == typeof(byte))
			{
				this.EncodeType(currentArray.GetType());
				this._xmlWriter.WriteString(Convert.ToBase64String((byte[])currentArray));
				this._xmlWriter.WriteFullEndElement();
				return;
			}
			string namespacePrefix = this.GetNamespacePrefix(xmlElement2);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}:{1}[", namespacePrefix, xmlElement2.LocalName);
			for (int i = 0; i < currentArray.Rank; i++)
			{
				stringBuilder.AppendFormat("{0},", currentArray.GetUpperBound(i) + 1);
			}
			stringBuilder.Replace(',', ']', stringBuilder.Length - 1, 1);
			this._xmlWriter.WriteAttributeString(SoapTypeMapper.SoapEncodingPrefix, "arrayType", SoapTypeMapper.SoapEncodingNamespace, stringBuilder.ToString());
			int num = 0;
			int num2 = 0;
			foreach (object obj in currentArray)
			{
				if (obj != null)
				{
					for (int j = num; j < num2; j++)
					{
						this._xmlWriter.WriteStartElement("item");
						this.Null();
						this._xmlWriter.WriteEndElement();
					}
					num = num2 + 1;
					this._xmlWriter.WriteStartElement("item");
					this.SerializeComponent(obj, this.IsEncodingNeeded(obj, elementType));
					this._xmlWriter.WriteEndElement();
				}
				num2++;
			}
			this._xmlWriter.WriteFullEndElement();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004A80 File Offset: 0x00002C80
		private void SerializeComponent(object obj, bool specifyEncoding)
		{
			if (this._typeFormat == FormatterTypeStyle.TypesAlways)
			{
				specifyEncoding = true;
			}
			if (obj == null)
			{
				this.Null();
				return;
			}
			Type type = obj.GetType();
			bool flag = this._mapper.IsInternalSoapType(type);
			bool flag2;
			if (this.idGen.HasId(obj, out flag2) != 0L)
			{
				this.Href(this.idGen.GetId(obj, out flag2));
				return;
			}
			if (type == typeof(string) && this._typeFormat != FormatterTypeStyle.XsdString)
			{
				long id = this.idGen.GetId(obj, out flag2);
				this.Id(id);
			}
			if (!flag && !type.IsValueType)
			{
				long id2 = this.idGen.GetId(obj, out flag2);
				this.Href(id2);
				this._objectQueue.Enqueue(new SoapWriter.EnqueuedObject(obj, id2));
				return;
			}
			if (specifyEncoding)
			{
				this.EncodeType(type);
			}
			if (!flag && type.IsValueType)
			{
				this.SerializeObject(obj, 0L);
				return;
			}
			this._xmlWriter.WriteString(this._mapper.GetInternalSoapValue(this, obj));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004B88 File Offset: 0x00002D88
		private void EncodeType(Type type)
		{
			if (type == null)
			{
				throw new SerializationException("Oooops");
			}
			Element xmlElement = this._mapper.GetXmlElement(type);
			string namespacePrefix = this.GetNamespacePrefix(xmlElement);
			this._xmlWriter.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", namespacePrefix + ":" + xmlElement.LocalName);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004BEC File Offset: 0x00002DEC
		public string GetNamespacePrefix(Element xmlType)
		{
			string text = this._xmlWriter.LookupPrefix(xmlType.NamespaceURI);
			if (text == null || text == string.Empty)
			{
				this._xmlWriter.WriteAttributeString("xmlns", xmlType.Prefix, "http://www.w3.org/2000/xmlns/", xmlType.NamespaceURI);
				return xmlType.Prefix;
			}
			return text;
		}

		// Token: 0x04000054 RID: 84
		private XmlTextWriter _xmlWriter;

		// Token: 0x04000055 RID: 85
		private Queue _objectQueue = new Queue();

		// Token: 0x04000056 RID: 86
		private Hashtable _objectToIdTable = new Hashtable();

		// Token: 0x04000057 RID: 87
		private ISurrogateSelector _surrogateSelector;

		// Token: 0x04000058 RID: 88
		private SoapTypeMapper _mapper;

		// Token: 0x04000059 RID: 89
		private StreamingContext _context;

		// Token: 0x0400005A RID: 90
		private ObjectIDGenerator idGen = new ObjectIDGenerator();

		// Token: 0x0400005B RID: 91
		private FormatterAssemblyStyle _assemblyFormat = FormatterAssemblyStyle.Full;

		// Token: 0x0400005C RID: 92
		private FormatterTypeStyle _typeFormat;

		// Token: 0x0400005D RID: 93
		private static string defaultMessageNamespace = typeof(SoapWriter).Assembly.GetName().FullName;

		// Token: 0x0400005E RID: 94
		private SerializationObjectManager _manager;

		// Token: 0x02000012 RID: 18
		private struct EnqueuedObject
		{
			// Token: 0x06000071 RID: 113 RVA: 0x00004C44 File Offset: 0x00002E44
			public EnqueuedObject(object currentObject, long id)
			{
				this._id = id;
				this._object = currentObject;
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000072 RID: 114 RVA: 0x00004C54 File Offset: 0x00002E54
			public long Id
			{
				get
				{
					return this._id;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000073 RID: 115 RVA: 0x00004C5C File Offset: 0x00002E5C
			public object Object
			{
				get
				{
					return this._object;
				}
			}

			// Token: 0x0400005F RID: 95
			public long _id;

			// Token: 0x04000060 RID: 96
			public object _object;
		}
	}
}
