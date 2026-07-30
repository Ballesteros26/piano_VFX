using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Threading;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap
{
	// Token: 0x0200000C RID: 12
	internal sealed class SoapReader
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021F6 File Offset: 0x000003F6
		private long NextAvailableId
		{
			get
			{
				this._nextAvailableId -= 1L;
				return this._nextAvailableId;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002210 File Offset: 0x00000410
		public SoapReader(SerializationBinder binder, ISurrogateSelector selector, StreamingContext context)
		{
			this._binder = binder;
			this.objMgr = new ObjectManager(selector, context);
			this._context = context;
			this._surrogateSelector = selector;
			this._fieldIndices = new Hashtable();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002268 File Offset: 0x00000468
		public object Deserialize(Stream inStream, ISoapMessage soapMessage)
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			try
			{
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
				this.Deserialize_inner(inStream, soapMessage);
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
			return this.TopObject;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022BC File Offset: 0x000004BC
		private void Deserialize_inner(Stream inStream, ISoapMessage soapMessage)
		{
			ArrayList arrayList = null;
			this.xmlReader = new XmlTextReader(inStream);
			this.xmlReader.WhitespaceHandling = WhitespaceHandling.None;
			this.mapper = new SoapTypeMapper(this._binder);
			try
			{
				this.xmlReader.MoveToContent();
				this.xmlReader.ReadStartElement();
				this.xmlReader.MoveToContent();
				while (this.xmlReader.NodeType != XmlNodeType.Element || !(this.xmlReader.LocalName == "Body") || !(this.xmlReader.NamespaceURI == SoapTypeMapper.SoapEnvelopeNamespace))
				{
					if (this.xmlReader.NodeType == XmlNodeType.Element && this.xmlReader.LocalName == "Header" && this.xmlReader.NamespaceURI == SoapTypeMapper.SoapEnvelopeNamespace)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						this.DeserializeHeaders(arrayList);
					}
					else
					{
						this.xmlReader.Skip();
					}
					this.xmlReader.MoveToContent();
				}
				this.xmlReader.ReadStartElement();
				this.xmlReader.MoveToContent();
				if (soapMessage != null)
				{
					if (this.DeserializeMessage(soapMessage))
					{
						this._topObjectId = this.NextAvailableId;
						this.RegisterObject(this._topObjectId, soapMessage, null, 0L, null, null);
					}
					this.xmlReader.MoveToContent();
					if (arrayList != null)
					{
						soapMessage.Headers = (Header[])arrayList.ToArray(typeof(Header));
					}
				}
				while (this.xmlReader.NodeType != XmlNodeType.EndElement)
				{
					this.Deserialize();
				}
				this.xmlReader.ReadEndElement();
				this.xmlReader.MoveToContent();
				this.xmlReader.ReadEndElement();
			}
			finally
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002498 File Offset: 0x00000698
		public SoapTypeMapper Mapper
		{
			get
			{
				return this.mapper;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000024A0 File Offset: 0x000006A0
		public XmlTextReader XmlReader
		{
			get
			{
				return this.xmlReader;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024A8 File Offset: 0x000006A8
		private object TopObject
		{
			get
			{
				this.objMgr.DoFixups();
				this.objMgr.RaiseDeserializationEvent();
				return this.objMgr.GetObject(this._topObjectId);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024D4 File Offset: 0x000006D4
		private bool IsNull()
		{
			string text = this.xmlReader["null", "http://www.w3.org/2001/XMLSchema-instance"];
			return text != null && !(text == string.Empty);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000250C File Offset: 0x0000070C
		private long GetId()
		{
			string text = this.xmlReader["id"];
			if (text == null || text == string.Empty)
			{
				return 0L;
			}
			return Convert.ToInt64(text.Substring(4));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000254C File Offset: 0x0000074C
		private long GetHref()
		{
			string text = this.xmlReader["href"];
			if (text == null || text == string.Empty)
			{
				return 0L;
			}
			return Convert.ToInt64(text.Substring(5));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000258C File Offset: 0x0000078C
		private Type GetComponentType()
		{
			string text = this.xmlReader["type", "http://www.w3.org/2001/XMLSchema-instance"];
			if (text != null)
			{
				return this.GetTypeFromQName(text);
			}
			if (this.GetId() != 0L)
			{
				return typeof(string);
			}
			return null;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025D0 File Offset: 0x000007D0
		private bool DeserializeMessage(ISoapMessage message)
		{
			if (this.xmlReader.Name == SoapTypeMapper.SoapEnvelopePrefix + ":Fault")
			{
				this.Deserialize();
				return false;
			}
			string text;
			string text2;
			SoapServices.DecodeXmlNamespaceForClrTypeNamespace(this.xmlReader.NamespaceURI, out text, out text2);
			message.MethodName = this.xmlReader.LocalName;
			message.XmlNameSpace = this.xmlReader.NamespaceURI;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			long nextAvailableId = this.NextAvailableId;
			int[] array = new int[1];
			if (!this.xmlReader.IsEmptyElement)
			{
				int depth = this.xmlReader.Depth;
				this.xmlReader.Read();
				int num = 0;
				while (this.xmlReader.Depth > depth)
				{
					arrayList.Add(this.xmlReader.Name);
					Type type = null;
					if (message.ParamTypes != null)
					{
						if (num >= message.ParamTypes.Length)
						{
							throw new SerializationException("Not enough parameter types in SoapMessages");
						}
						type = message.ParamTypes[num];
					}
					array[0] = num;
					long num2;
					long num3;
					object obj = this.DeserializeComponent(type, out num2, out num3, nextAvailableId, null, array);
					array[0] = arrayList2.Add(obj);
					if (num3 != 0L)
					{
						this.RecordFixup(nextAvailableId, num3, arrayList2.ToArray(), null, null, null, array);
					}
					num++;
				}
				this.xmlReader.ReadEndElement();
			}
			else
			{
				this.xmlReader.Read();
			}
			message.ParamNames = (string[])arrayList.ToArray(typeof(string));
			message.ParamValues = arrayList2.ToArray();
			this.RegisterObject(nextAvailableId, message.ParamValues, null, 0L, null, null);
			return true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000277C File Offset: 0x0000097C
		private void DeserializeHeaders(ArrayList headers)
		{
			this.xmlReader.ReadStartElement();
			this.xmlReader.MoveToContent();
			while (this.xmlReader.NodeType != XmlNodeType.EndElement)
			{
				if (this.xmlReader.NodeType != XmlNodeType.Element)
				{
					this.xmlReader.Skip();
				}
				else
				{
					if (this.xmlReader.GetAttribute("root", SoapTypeMapper.SoapEncodingNamespace) == "1")
					{
						headers.Add(this.DeserializeHeader());
					}
					else
					{
						this.Deserialize();
					}
					this.xmlReader.MoveToContent();
				}
			}
			this.xmlReader.ReadEndElement();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000281C File Offset: 0x00000A1C
		private Header DeserializeHeader()
		{
			Header header = new Header(this.xmlReader.LocalName, null);
			header.HeaderNamespace = this.xmlReader.NamespaceURI;
			header.MustUnderstand = this.xmlReader.GetAttribute("mustUnderstand", SoapTypeMapper.SoapEnvelopeNamespace) == "1";
			long nextAvailableId = this.NextAvailableId;
			FieldInfo field = typeof(Header).GetField("Value");
			long num;
			long num2;
			object obj = this.DeserializeComponent(null, out num, out num2, nextAvailableId, field, null);
			header.Value = obj;
			if (num2 != 0L && obj == null)
			{
				this.RecordFixup(nextAvailableId, num2, header, null, null, field, null);
			}
			else if (obj != null && obj.GetType().IsValueType && num != 0L)
			{
				this.RecordFixup(nextAvailableId, num, header, null, null, field, null);
			}
			else if (num != 0L)
			{
				this.RegisterObject(num, obj, null, nextAvailableId, field, null);
			}
			this.RegisterObject(nextAvailableId, header, null, 0L, null, null);
			return header;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002904 File Offset: 0x00000B04
		private object DeserializeArray(long id)
		{
			if (this.GetComponentType() == typeof(byte[]))
			{
				byte[] array = Convert.FromBase64String(this.xmlReader.ReadElementString());
				this.RegisterObject(id, array, null, 0L, null, null);
				return array;
			}
			string[] array2 = this.xmlReader["arrayType", SoapTypeMapper.SoapEncodingNamespace].Split(new char[] { ':' });
			int num = array2[1].LastIndexOf('[');
			string text = array2[1].Substring(0, num);
			string text2 = array2[1].Substring(num);
			string[] array3 = text2.Substring(1, text2.Length - 2).Trim().Split(new char[] { ',' });
			int num2 = array3.Length;
			int[] array4 = new int[num2];
			for (int i = 0; i < num2; i++)
			{
				array4[i] = Convert.ToInt32(array3[i]);
			}
			int[] array5 = new int[num2];
			Array array6 = Array.CreateInstance(this.mapper.GetType(text, this.xmlReader.LookupNamespace(array2[0])), array4);
			for (int j = 0; j < num2; j++)
			{
				array5[j] = array6.GetLowerBound(j);
			}
			int depth = this.xmlReader.Depth;
			this.xmlReader.Read();
			while (this.xmlReader.Depth > depth)
			{
				Type type = this.GetComponentType();
				if (type == null)
				{
					type = array6.GetType().GetElementType();
				}
				long num3;
				long num4;
				object obj = this.DeserializeComponent(type, out num3, out num4, id, null, array5);
				if (num4 != 0L)
				{
					object @object = this.objMgr.GetObject(num4);
					if (@object != null)
					{
						array6.SetValue(@object, array5);
					}
					else
					{
						this.RecordFixup(id, num4, array6, null, null, null, array5);
					}
				}
				else if (obj != null && obj.GetType().IsValueType && num3 != 0L)
				{
					this.RecordFixup(id, num3, array6, null, null, null, array5);
				}
				else if (num3 != 0L)
				{
					this.RegisterObject(num3, obj, null, id, null, array5);
					array6.SetValue(obj, array5);
				}
				else
				{
					array6.SetValue(obj, array5);
				}
				for (int k = array6.Rank - 1; k >= 0; k--)
				{
					array5[k]++;
					if (array5[k] <= array6.GetUpperBound(k) || k <= 0)
					{
						break;
					}
					array5[k] = array6.GetLowerBound(k);
				}
			}
			this.RegisterObject(id, array6, null, 0L, null, null);
			this.xmlReader.ReadEndElement();
			return array6;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B80 File Offset: 0x00000D80
		private object Deserialize()
		{
			Type type = this.mapper.GetType(this.xmlReader.LocalName, this.xmlReader.NamespaceURI);
			long num = this.GetId();
			num = ((num == 0L) ? 1L : num);
			object obj;
			if (type == typeof(Array))
			{
				obj = this.DeserializeArray(num);
			}
			else
			{
				obj = this.DeserializeObject(type, num, 0L, null, null);
			}
			return obj;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002BEC File Offset: 0x00000DEC
		private object DeserializeObject(Type type, long id, long parentId, MemberInfo parentMemberInfo, int[] indices)
		{
			SerializationInfo serializationInfo = null;
			bool flag = false;
			if (type == typeof(string) || type == typeof(TimeSpan) || (this.mapper.IsInternalSoapType(type) && (indices != null || parentMemberInfo != null)))
			{
				object obj = this.mapper.ReadInternalSoapValue(this, type);
				if (id != 0L)
				{
					this.RegisterObject(id, obj, serializationInfo, parentId, parentMemberInfo, indices);
				}
				return obj;
			}
			object obj2 = FormatterServices.GetUninitializedObject(type);
			this.objMgr.RaiseOnDeserializingEvent(obj2);
			if (obj2 is ISerializable)
			{
				flag = true;
			}
			if (this._surrogateSelector != null && !flag)
			{
				ISurrogateSelector surrogateSelector;
				ISerializationSurrogate surrogate = this._surrogateSelector.GetSurrogate(type, this._context, out surrogateSelector);
				flag |= surrogate != null;
			}
			if (flag)
			{
				bool flag2;
				obj2 = this.DeserializeISerializableObject(obj2, id, out serializationInfo, out flag2);
			}
			else
			{
				bool flag2;
				obj2 = this.DeserializeSimpleObject(obj2, id, out flag2);
				if (!flag2 && obj2 is IObjectReference)
				{
					obj2 = ((IObjectReference)obj2).GetRealObject(this._context);
				}
			}
			this.RegisterObject(id, obj2, serializationInfo, parentId, parentMemberInfo, indices);
			this.xmlReader.ReadEndElement();
			return obj2;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D00 File Offset: 0x00000F00
		private object DeserializeSimpleObject(object obj, long id, out bool hasFixup)
		{
			hasFixup = false;
			Type type = obj.GetType();
			SoapReader.TypeMetadata typeMetadata = this.GetTypeMetadata(type);
			object[] array = new object[typeMetadata.MemberInfos.Length];
			this.xmlReader.Read();
			this.xmlReader.MoveToContent();
			while (this.xmlReader.NodeType != XmlNodeType.EndElement)
			{
				if (this.xmlReader.NodeType != XmlNodeType.Element)
				{
					this.xmlReader.Skip();
				}
				else
				{
					object obj2 = typeMetadata.Indices[this.xmlReader.LocalName];
					if (obj2 == null)
					{
						throw new SerializationException("Field \"" + this.xmlReader.LocalName + "\" not found in class " + type.FullName);
					}
					int num = (int)obj2;
					FieldInfo fieldInfo = typeMetadata.MemberInfos[num] as FieldInfo;
					long num2;
					long num3;
					object obj3 = this.DeserializeComponent(fieldInfo.FieldType, out num2, out num3, id, fieldInfo, null);
					array[num] = obj3;
					if (num3 != 0L && obj3 == null)
					{
						this.RecordFixup(id, num3, obj, null, null, fieldInfo, null);
						hasFixup = true;
					}
					else if (obj3 != null && obj3.GetType().IsValueType && num2 != 0L)
					{
						this.RecordFixup(id, num2, obj, null, null, fieldInfo, null);
						hasFixup = true;
					}
					else if (num2 != 0L)
					{
						this.RegisterObject(num2, obj3, null, id, fieldInfo, null);
					}
				}
			}
			FormatterServices.PopulateObjectMembers(obj, typeMetadata.MemberInfos, array);
			return obj;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E50 File Offset: 0x00001050
		private object DeserializeISerializableObject(object obj, long id, out SerializationInfo info, out bool hasFixup)
		{
			info = new SerializationInfo(obj.GetType(), new FormatterConverter());
			hasFixup = false;
			int depth = this.xmlReader.Depth;
			this.xmlReader.Read();
			while (this.xmlReader.Depth > depth)
			{
				Type componentType = this.GetComponentType();
				string text = XmlConvert.DecodeName(this.xmlReader.LocalName);
				long num;
				long num2;
				object obj2 = this.DeserializeComponent(componentType, out num, out num2, id, null, null);
				if (num2 != 0L && obj2 == null)
				{
					this.RecordFixup(id, num2, obj, info, text, null, null);
					hasFixup = true;
				}
				else if (num != 0L && obj2.GetType().IsValueType)
				{
					this.RecordFixup(id, num, obj, info, text, null, null);
					hasFixup = true;
				}
				else
				{
					if (num != 0L)
					{
						this.RegisterObject(num, obj2, null, id, null, null);
					}
					info.AddValue(text, obj2, (componentType != null) ? componentType : typeof(object));
				}
			}
			return obj;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F40 File Offset: 0x00001140
		private object DeserializeComponent(Type componentType, out long componentId, out long componentHref, long parentId, MemberInfo parentMemberInfo, int[] indices)
		{
			componentId = 0L;
			componentHref = 0L;
			if (this.IsNull())
			{
				this.xmlReader.Read();
				return null;
			}
			Type componentType2 = this.GetComponentType();
			if (componentType2 != null)
			{
				componentType = componentType2;
			}
			if (this.xmlReader.HasAttributes)
			{
				componentId = this.GetId();
				componentHref = this.GetHref();
			}
			if (componentId != 0L)
			{
				string text = this.xmlReader.ReadElementString();
				this.objMgr.RegisterObject(text, componentId);
				return text;
			}
			if (componentHref != 0L)
			{
				this.xmlReader.Read();
				return this.objMgr.GetObject(componentHref);
			}
			if (componentType == null)
			{
				return this.xmlReader.ReadElementString();
			}
			componentId = this.NextAvailableId;
			return this.DeserializeObject(componentType, componentId, parentId, parentMemberInfo, indices);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003008 File Offset: 0x00001208
		public void RecordFixup(long parentObjectId, long childObjectId, object parentObject, SerializationInfo info, string fieldName, MemberInfo memberInfo, int[] indices)
		{
			if (info != null)
			{
				this.objMgr.RecordDelayedFixup(parentObjectId, fieldName, childObjectId);
				return;
			}
			if (!(parentObject is Array))
			{
				this.objMgr.RecordFixup(parentObjectId, memberInfo, childObjectId);
				return;
			}
			if (indices.Length == 1)
			{
				this.objMgr.RecordArrayElementFixup(parentObjectId, indices[0], childObjectId);
				return;
			}
			this.objMgr.RecordArrayElementFixup(parentObjectId, (int[])indices.Clone(), childObjectId);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003074 File Offset: 0x00001274
		private void RegisterObject(long objectId, object objectInstance, SerializationInfo info, long parentObjectId, MemberInfo parentObjectMember, int[] indices)
		{
			if (parentObjectId == 0L)
			{
				indices = null;
			}
			if (!objectInstance.GetType().IsValueType || parentObjectId == 0L)
			{
				if (this.objMgr.GetObject(objectId) != objectInstance)
				{
					this.objMgr.RegisterObject(objectInstance, objectId, info, 0L, null, null);
					return;
				}
			}
			else
			{
				if (this.objMgr.GetObject(objectId) != null)
				{
					throw new SerializationException("Object already registered");
				}
				if (indices != null)
				{
					indices = (int[])indices.Clone();
				}
				this.objMgr.RegisterObject(objectInstance, objectId, info, parentObjectId, parentObjectMember, indices);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000030FC File Offset: 0x000012FC
		private SoapReader.TypeMetadata GetTypeMetadata(Type type)
		{
			SoapReader.TypeMetadata typeMetadata = this._fieldIndices[type] as SoapReader.TypeMetadata;
			if (typeMetadata != null)
			{
				return typeMetadata;
			}
			typeMetadata = new SoapReader.TypeMetadata();
			typeMetadata.MemberInfos = FormatterServices.GetSerializableMembers(type, this._context);
			typeMetadata.Indices = new Hashtable();
			for (int i = 0; i < typeMetadata.MemberInfos.Length; i++)
			{
				SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(typeMetadata.MemberInfos[i]);
				typeMetadata.Indices[XmlConvert.EncodeLocalName(soapFieldAttribute.XmlElementName)] = i;
			}
			this._fieldIndices[type] = typeMetadata;
			return typeMetadata;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003194 File Offset: 0x00001394
		public Type GetTypeFromQName(string qname)
		{
			string[] array = qname.Split(new char[] { ':' });
			string text = this.xmlReader.LookupNamespace(array[0]);
			return this.mapper.GetType(array[1], text);
		}

		// Token: 0x04000037 RID: 55
		private SerializationBinder _binder;

		// Token: 0x04000038 RID: 56
		private SoapTypeMapper mapper;

		// Token: 0x04000039 RID: 57
		private ObjectManager objMgr;

		// Token: 0x0400003A RID: 58
		private StreamingContext _context;

		// Token: 0x0400003B RID: 59
		private long _nextAvailableId = long.MaxValue;

		// Token: 0x0400003C RID: 60
		private ISurrogateSelector _surrogateSelector;

		// Token: 0x0400003D RID: 61
		private XmlTextReader xmlReader;

		// Token: 0x0400003E RID: 62
		private Hashtable _fieldIndices;

		// Token: 0x0400003F RID: 63
		private long _topObjectId = 1L;

		// Token: 0x0200000D RID: 13
		private class TypeMetadata
		{
			// Token: 0x04000040 RID: 64
			public MemberInfo[] MemberInfos;

			// Token: 0x04000041 RID: 65
			public Hashtable Indices;
		}
	}
}
