using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000734 RID: 1844
	internal sealed class ReadObjectInfo
	{
		// Token: 0x06004C5F RID: 19551 RVA: 0x00002111 File Offset: 0x00000311
		internal ReadObjectInfo()
		{
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x00002194 File Offset: 0x00000394
		internal void ObjectEnd()
		{
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x00110E2D File Offset: 0x0010F02D
		internal void PrepareForReuse()
		{
			this.lastPosition = 0;
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x00110E38 File Offset: 0x0010F038
		[SecurityCritical]
		internal static ReadObjectInfo Create(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x00110E5C File Offset: 0x0010F05C
		[SecurityCritical]
		internal void Init(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			this.InitReadConstructor(objectType, surrogateSelector, context);
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x00110E98 File Offset: 0x0010F098
		[SecurityCritical]
		internal static ReadObjectInfo Create(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, memberNames, memberTypes, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x00110EC0 File Offset: 0x0010F0C0
		[SecurityCritical]
		internal void Init(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.wireMemberNames = memberNames;
			this.wireMemberTypes = memberTypes;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			if (memberNames != null)
			{
				this.isNamed = true;
			}
			if (memberTypes != null)
			{
				this.isTyped = true;
			}
			if (objectType != null)
			{
				this.InitReadConstructor(objectType, surrogateSelector, context);
			}
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x00110F2C File Offset: 0x0010F12C
		[SecurityCritical]
		private void InitReadConstructor(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context)
		{
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.isSi = true;
			}
			if (this.isSi)
			{
				this.InitSiRead();
				return;
			}
			this.InitMemberInfo();
		}

		// Token: 0x06004C67 RID: 19559 RVA: 0x00110F9F File Offset: 0x0010F19F
		private void InitSiRead()
		{
			if (this.memberTypesList != null)
			{
				this.memberTypesList = new List<Type>(20);
			}
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x00110FB6 File Offset: 0x0010F1B6
		private void InitNoMembers()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x00110FCC File Offset: 0x0010F1CC
		[SecurityCritical]
		private void InitMemberInfo()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
			this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
			this.count = this.cache.memberInfos.Length;
			this.cache.memberNames = new string[this.count];
			this.cache.memberTypes = new Type[this.count];
			for (int i = 0; i < this.count; i++)
			{
				this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
				this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x001110A4 File Offset: 0x0010F2A4
		internal MemberInfo GetMemberInfo(string name)
		{
			if (this.cache == null)
			{
				return null;
			}
			if (this.isSi)
			{
				throw new SerializationException(Environment.GetResourceString("MemberInfo cannot be obtained for ISerialized Object '{0}'.", new object[] { this.objectType + " " + name }));
			}
			if (this.cache.memberInfos == null)
			{
				throw new SerializationException(Environment.GetResourceString("No MemberInfo for Object {0}.", new object[] { this.objectType + " " + name }));
			}
			if (this.Position(name) != -1)
			{
				return this.cache.memberInfos[this.Position(name)];
			}
			return null;
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x00111144 File Offset: 0x0010F344
		internal Type GetType(string name)
		{
			int num = this.Position(name);
			if (num == -1)
			{
				return null;
			}
			Type type;
			if (this.isTyped)
			{
				type = this.cache.memberTypes[num];
			}
			else
			{
				type = this.memberTypesList[num];
			}
			if (type == null)
			{
				throw new SerializationException(Environment.GetResourceString("Types not available for ISerializable object '{0}'.", new object[] { this.objectType + " " + name }));
			}
			return type;
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x001111B4 File Offset: 0x0010F3B4
		internal void AddValue(string name, object value, ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				si.AddValue(name, value);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				memberData[num] = value;
			}
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x001111E8 File Offset: 0x0010F3E8
		internal void InitDataStore(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				if (si == null)
				{
					si = new SerializationInfo(this.objectType, this.formatterConverter);
					return;
				}
			}
			else if (memberData == null && this.cache != null)
			{
				memberData = new object[this.cache.memberNames.Length];
			}
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x00111238 File Offset: 0x0010F438
		internal void RecordFixup(long objectId, string name, long idRef)
		{
			if (this.isSi)
			{
				this.objectManager.RecordDelayedFixup(objectId, name, idRef);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				this.objectManager.RecordFixup(objectId, this.cache.memberInfos[num], idRef);
			}
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x00111282 File Offset: 0x0010F482
		[SecurityCritical]
		internal void PopulateObjectMembers(object obj, object[] memberData)
		{
			if (!this.isSi && memberData != null)
			{
				FormatterServices.PopulateObjectMembers(obj, this.cache.memberInfos, memberData);
			}
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x001112A4 File Offset: 0x0010F4A4
		[Conditional("SER_LOGGING")]
		private void DumpPopulate(MemberInfo[] memberInfos, object[] memberData)
		{
			for (int i = 0; i < memberInfos.Length; i++)
			{
			}
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("SER_LOGGING")]
		private void DumpPopulateSi()
		{
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x001112C0 File Offset: 0x0010F4C0
		private int Position(string name)
		{
			if (this.cache == null)
			{
				return -1;
			}
			if (this.cache.memberNames.Length != 0 && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			int num = this.lastPosition + 1;
			this.lastPosition = num;
			if (num < this.cache.memberNames.Length && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			for (int i = 0; i < this.cache.memberNames.Length; i++)
			{
				if (this.cache.memberNames[i].Equals(name))
				{
					this.lastPosition = i;
					return this.lastPosition;
				}
			}
			this.lastPosition = 0;
			return -1;
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0011138C File Offset: 0x0010F58C
		internal Type[] GetMemberTypes(string[] inMemberNames, Type objectType)
		{
			if (this.isSi)
			{
				throw new SerializationException(Environment.GetResourceString("Types not available for ISerializable object '{0}'.", new object[] { objectType }));
			}
			if (this.cache == null)
			{
				return null;
			}
			if (this.cache.memberTypes == null)
			{
				this.cache.memberTypes = new Type[this.count];
				for (int i = 0; i < this.count; i++)
				{
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
			}
			bool flag = false;
			if (inMemberNames.Length < this.cache.memberInfos.Length)
			{
				flag = true;
			}
			Type[] array = new Type[this.cache.memberInfos.Length];
			for (int j = 0; j < this.cache.memberInfos.Length; j++)
			{
				if (!flag && inMemberNames[j].Equals(this.cache.memberInfos[j].Name))
				{
					array[j] = this.cache.memberTypes[j];
				}
				else
				{
					bool flag2 = false;
					for (int k = 0; k < inMemberNames.Length; k++)
					{
						if (this.cache.memberInfos[j].Name.Equals(inMemberNames[k]))
						{
							array[j] = this.cache.memberTypes[j];
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						object[] customAttributes = this.cache.memberInfos[j].GetCustomAttributes(typeof(OptionalFieldAttribute), false);
						if ((customAttributes == null || customAttributes.Length == 0) && !this.bSimpleAssembly)
						{
							throw new SerializationException(Environment.GetResourceString("Member '{0}' in class '{1}' is not present in the serialized stream and is not marked with {2}.", new object[]
							{
								this.cache.memberNames[j],
								objectType,
								typeof(OptionalFieldAttribute).FullName
							}));
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x00111558 File Offset: 0x0010F758
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

		// Token: 0x06004C75 RID: 19573 RVA: 0x001115B3 File Offset: 0x0010F7B3
		private static ReadObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			return new ReadObjectInfo
			{
				objectInfoId = Interlocked.Increment(ref ReadObjectInfo.readObjectInfoCounter)
			};
		}

		// Token: 0x040028BE RID: 10430
		internal int objectInfoId;

		// Token: 0x040028BF RID: 10431
		internal static int readObjectInfoCounter;

		// Token: 0x040028C0 RID: 10432
		internal Type objectType;

		// Token: 0x040028C1 RID: 10433
		internal ObjectManager objectManager;

		// Token: 0x040028C2 RID: 10434
		internal int count;

		// Token: 0x040028C3 RID: 10435
		internal bool isSi;

		// Token: 0x040028C4 RID: 10436
		internal bool isNamed;

		// Token: 0x040028C5 RID: 10437
		internal bool isTyped;

		// Token: 0x040028C6 RID: 10438
		internal bool bSimpleAssembly;

		// Token: 0x040028C7 RID: 10439
		internal SerObjectInfoCache cache;

		// Token: 0x040028C8 RID: 10440
		internal string[] wireMemberNames;

		// Token: 0x040028C9 RID: 10441
		internal Type[] wireMemberTypes;

		// Token: 0x040028CA RID: 10442
		private int lastPosition;

		// Token: 0x040028CB RID: 10443
		internal ISurrogateSelector surrogateSelector;

		// Token: 0x040028CC RID: 10444
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x040028CD RID: 10445
		internal StreamingContext context;

		// Token: 0x040028CE RID: 10446
		internal List<Type> memberTypesList;

		// Token: 0x040028CF RID: 10447
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x040028D0 RID: 10448
		internal IFormatterConverter formatterConverter;
	}
}
