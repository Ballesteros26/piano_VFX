using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000733 RID: 1843
	internal sealed class WriteObjectInfo
	{
		// Token: 0x06004C4C RID: 19532 RVA: 0x00002111 File Offset: 0x00000311
		internal WriteObjectInfo()
		{
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0011064B File Offset: 0x0010E84B
		internal void ObjectEnd()
		{
			WriteObjectInfo.PutObjectInfo(this.serObjectInfoInit, this);
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0011065C File Offset: 0x0010E85C
		private void InternalInit()
		{
			this.obj = null;
			this.objectType = null;
			this.isSi = false;
			this.isNamed = false;
			this.isTyped = false;
			this.isArray = false;
			this.si = null;
			this.cache = null;
			this.memberData = null;
			this.objectId = 0L;
			this.assemId = 0L;
			this.binderTypeName = null;
			this.binderAssemblyString = null;
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x001106C8 File Offset: 0x0010E8C8
		[SecurityCritical]
		internal static WriteObjectInfo Serialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(obj, surrogateSelector, context, serObjectInfoInit, converter, objectWriter, binder);
			return objectInfo;
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x001106EC File Offset: 0x0010E8EC
		[SecurityCritical]
		internal void InitSerialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
		{
			this.context = context;
			this.obj = obj;
			this.serObjectInfoInit = serObjectInfoInit;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				this.objectType = Converter.typeofMarshalByRefObject;
			}
			else
			{
				this.objectType = obj.GetType();
			}
			if (this.objectType.IsArray)
			{
				this.isArray = true;
				this.InitNoMembers();
				return;
			}
			this.InvokeSerializationBinder(binder);
			objectWriter.ObjectManager.RegisterObject(obj);
			ISurrogateSelector surrogateSelector2;
			if (surrogateSelector != null && (this.serializationSurrogate = surrogateSelector.GetSurrogate(this.objectType, context, out surrogateSelector2)) != null)
			{
				this.si = new SerializationInfo(this.objectType, converter);
				if (!this.objectType.IsPrimitive)
				{
					this.serializationSurrogate.GetObjectData(obj, this.si, context);
				}
				this.InitSiWrite();
				return;
			}
			if (!(obj is ISerializable))
			{
				this.InitMemberInfo();
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
				return;
			}
			if (!this.objectType.IsSerializable)
			{
				throw new SerializationException(Environment.GetResourceString("Type '{0}' in Assembly '{1}' is not marked as serializable.", new object[]
				{
					this.objectType.FullName,
					this.objectType.Assembly.FullName
				}));
			}
			this.si = new SerializationInfo(this.objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
			((ISerializable)obj).GetObjectData(this.si, context);
			this.InitSiWrite();
			WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x00110870 File Offset: 0x0010EA70
		[Conditional("SER_LOGGING")]
		private void DumpMemberInfo()
		{
			for (int i = 0; i < this.cache.memberInfos.Length; i++)
			{
			}
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x00110895 File Offset: 0x0010EA95
		[SecurityCritical]
		internal static WriteObjectInfo Serialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(objectType, surrogateSelector, context, serObjectInfoInit, converter, binder);
			return objectInfo;
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x001108AC File Offset: 0x0010EAAC
		[SecurityCritical]
		internal void InitSerialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
		{
			this.objectType = objectType;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			this.InvokeSerializationBinder(binder);
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.si = new SerializationInfo(objectType, converter);
				this.cache = new SerObjectInfoCache(objectType);
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.si = new SerializationInfo(objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
				this.cache = new SerObjectInfoCache(objectType);
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
				this.isSi = true;
			}
			if (!this.isSi)
			{
				this.InitMemberInfo();
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
			}
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x00110998 File Offset: 0x0010EB98
		private void InitSiWrite()
		{
			this.isSi = true;
			SerializationInfoEnumerator serializationInfoEnumerator = this.si.GetEnumerator();
			int memberCount = this.si.MemberCount;
			TypeInformation typeInformation = null;
			string text = this.si.FullTypeName;
			string text2 = this.si.AssemblyName;
			bool flag = false;
			if (!this.si.IsFullTypeNameSetExplicit)
			{
				typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
				text = typeInformation.FullTypeName;
				flag = typeInformation.HasTypeForwardedFrom;
			}
			if (!this.si.IsAssemblyNameSetExplicit)
			{
				if (typeInformation == null)
				{
					typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
				}
				text2 = typeInformation.AssemblyString;
				flag = typeInformation.HasTypeForwardedFrom;
			}
			this.cache = new SerObjectInfoCache(text, text2, flag);
			this.cache.memberNames = new string[memberCount];
			this.cache.memberTypes = new Type[memberCount];
			this.memberData = new object[memberCount];
			serializationInfoEnumerator = this.si.GetEnumerator();
			int num = 0;
			while (serializationInfoEnumerator.MoveNext())
			{
				this.cache.memberNames[num] = serializationInfoEnumerator.Name;
				this.cache.memberTypes[num] = serializationInfoEnumerator.ObjectType;
				this.memberData[num] = serializationInfoEnumerator.Value;
				num++;
			}
			this.isNamed = true;
			this.isTyped = false;
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x00110AE4 File Offset: 0x0010ECE4
		private static void CheckTypeForwardedFrom(SerObjectInfoCache cache, Type objectType, string binderAssemblyString)
		{
			if (cache.hasTypeForwardedFrom && binderAssemblyString == null && !FormatterServices.UnsafeTypeForwardersIsEnabled())
			{
				Assembly assembly = objectType.Assembly;
				if (!SerializationInfo.IsAssemblyNameAssignmentSafe(assembly.FullName, cache.assemblyString) && !assembly.IsFullyTrusted)
				{
					throw new SecurityException(Environment.GetResourceString("A type '{0}' that is defined in a partially trusted assembly cannot be type forwarded from an assembly with a different Public Key Token or without a public key token. To fix this, please either turn on unsafeTypeForwarding flag in the configuration file or remove the TypeForwardedFrom attribute.", new object[] { objectType }));
				}
			}
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x00110B40 File Offset: 0x0010ED40
		private void InitNoMembers()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache(this.objectType);
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x00110BA4 File Offset: 0x0010EDA4
		[SecurityCritical]
		private void InitMemberInfo()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache(this.objectType);
				this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
				int num = this.cache.memberInfos.Length;
				this.cache.memberNames = new string[num];
				this.cache.memberTypes = new Type[num];
				for (int i = 0; i < num; i++)
				{
					this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
			if (this.obj != null)
			{
				this.memberData = FormatterServices.GetObjectData(this.obj, this.cache.memberInfos);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x00110CD3 File Offset: 0x0010EED3
		internal string GetTypeFullName()
		{
			return this.binderTypeName ?? this.cache.fullTypeName;
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x00110CEA File Offset: 0x0010EEEA
		internal string GetAssemblyString()
		{
			return this.binderAssemblyString ?? this.cache.assemblyString;
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x00110D01 File Offset: 0x0010EF01
		private void InvokeSerializationBinder(SerializationBinder binder)
		{
			if (binder != null)
			{
				binder.BindToName(this.objectType, out this.binderAssemblyString, out this.binderTypeName);
			}
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x00110D20 File Offset: 0x0010EF20
		internal Type GetMemberType(MemberInfo objMember)
		{
			Type type;
			if (objMember is FieldInfo)
			{
				type = ((FieldInfo)objMember).FieldType;
			}
			else
			{
				if (!(objMember is PropertyInfo))
				{
					throw new SerializationException(Environment.GetResourceString("MemberInfo type {0} cannot be serialized.", new object[] { objMember.GetType() }));
				}
				type = ((PropertyInfo)objMember).PropertyType;
			}
			return type;
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x00110D7C File Offset: 0x0010EF7C
		internal void GetMemberInfo(out string[] outMemberNames, out Type[] outMemberTypes, out object[] outMemberData)
		{
			outMemberNames = this.cache.memberNames;
			outMemberTypes = this.cache.memberTypes;
			outMemberData = this.memberData;
			if (this.isSi && !this.isNamed)
			{
				throw new SerializationException(Environment.GetResourceString("MemberInfo requested for ISerializable type."));
			}
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x00110DCC File Offset: 0x0010EFCC
		private static WriteObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			WriteObjectInfo writeObjectInfo;
			if (!serObjectInfoInit.oiPool.IsEmpty())
			{
				writeObjectInfo = (WriteObjectInfo)serObjectInfoInit.oiPool.Pop();
				writeObjectInfo.InternalInit();
			}
			else
			{
				writeObjectInfo = new WriteObjectInfo();
				WriteObjectInfo writeObjectInfo2 = writeObjectInfo;
				int objectInfoIdCount = serObjectInfoInit.objectInfoIdCount;
				serObjectInfoInit.objectInfoIdCount = objectInfoIdCount + 1;
				writeObjectInfo2.objectInfoId = objectInfoIdCount;
			}
			return writeObjectInfo;
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x00110E1F File Offset: 0x0010F01F
		private static void PutObjectInfo(SerObjectInfoInit serObjectInfoInit, WriteObjectInfo objectInfo)
		{
			serObjectInfoInit.oiPool.Push(objectInfo);
		}

		// Token: 0x040028AD RID: 10413
		internal int objectInfoId;

		// Token: 0x040028AE RID: 10414
		internal object obj;

		// Token: 0x040028AF RID: 10415
		internal Type objectType;

		// Token: 0x040028B0 RID: 10416
		internal bool isSi;

		// Token: 0x040028B1 RID: 10417
		internal bool isNamed;

		// Token: 0x040028B2 RID: 10418
		internal bool isTyped;

		// Token: 0x040028B3 RID: 10419
		internal bool isArray;

		// Token: 0x040028B4 RID: 10420
		internal SerializationInfo si;

		// Token: 0x040028B5 RID: 10421
		internal SerObjectInfoCache cache;

		// Token: 0x040028B6 RID: 10422
		internal object[] memberData;

		// Token: 0x040028B7 RID: 10423
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x040028B8 RID: 10424
		internal StreamingContext context;

		// Token: 0x040028B9 RID: 10425
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x040028BA RID: 10426
		internal long objectId;

		// Token: 0x040028BB RID: 10427
		internal long assemId;

		// Token: 0x040028BC RID: 10428
		private string binderTypeName;

		// Token: 0x040028BD RID: 10429
		private string binderAssemblyString;
	}
}
