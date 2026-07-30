using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200071A RID: 1818
	internal sealed class ObjectMap
	{
		// Token: 0x06004BD0 RID: 19408 RVA: 0x0010E7A4 File Offset: 0x0010C9A4
		[SecurityCritical]
		internal ObjectMap(string objectName, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			this.objectName = objectName;
			this.objectType = objectType;
			this.memberNames = memberNames;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			this.objectInfo = objectReader.CreateReadObjectInfo(objectType);
			this.memberTypes = this.objectInfo.GetMemberTypes(memberNames, objectType);
			this.binaryTypeEnumA = new BinaryTypeEnum[this.memberTypes.Length];
			this.typeInformationA = new object[this.memberTypes.Length];
			for (int i = 0; i < this.memberTypes.Length; i++)
			{
				object obj = null;
				BinaryTypeEnum parserBinaryTypeInfo = BinaryConverter.GetParserBinaryTypeInfo(this.memberTypes[i], out obj);
				this.binaryTypeEnumA[i] = parserBinaryTypeInfo;
				this.typeInformationA[i] = obj;
			}
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x0010E86C File Offset: 0x0010CA6C
		[SecurityCritical]
		internal ObjectMap(string objectName, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			this.objectName = objectName;
			this.memberNames = memberNames;
			this.binaryTypeEnumA = binaryTypeEnumA;
			this.typeInformationA = typeInformationA;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			if (assemblyInfo == null)
			{
				throw new SerializationException(Environment.GetResourceString("No assembly information is available for object on the wire, '{0}'.", new object[] { objectName }));
			}
			this.objectType = objectReader.GetType(assemblyInfo, objectName);
			this.memberTypes = new Type[memberNames.Length];
			for (int i = 0; i < memberNames.Length; i++)
			{
				InternalPrimitiveTypeE internalPrimitiveTypeE;
				string text;
				Type type;
				bool flag;
				BinaryConverter.TypeFromInfo(binaryTypeEnumA[i], typeInformationA[i], objectReader, (BinaryAssemblyInfo)assemIdToAssemblyTable[memberAssemIds[i]], out internalPrimitiveTypeE, out text, out type, out flag);
				this.memberTypes[i] = type;
			}
			this.objectInfo = objectReader.CreateReadObjectInfo(this.objectType, memberNames, null);
			if (!this.objectInfo.isSi)
			{
				this.objectInfo.GetMemberTypes(memberNames, this.objectInfo.objectType);
			}
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x0010E970 File Offset: 0x0010CB70
		internal ReadObjectInfo CreateObjectInfo(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isInitObjectInfo)
			{
				this.isInitObjectInfo = false;
				this.objectInfo.InitDataStore(ref si, ref memberData);
				return this.objectInfo;
			}
			this.objectInfo.PrepareForReuse();
			this.objectInfo.InitDataStore(ref si, ref memberData);
			return this.objectInfo;
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0010E9BE File Offset: 0x0010CBBE
		[SecurityCritical]
		internal static ObjectMap Create(string name, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			return new ObjectMap(name, objectType, memberNames, objectReader, objectId, assemblyInfo);
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x0010E9D0 File Offset: 0x0010CBD0
		[SecurityCritical]
		internal static ObjectMap Create(string name, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			return new ObjectMap(name, memberNames, binaryTypeEnumA, typeInformationA, memberAssemIds, objectReader, objectId, assemblyInfo, assemIdToAssemblyTable);
		}

		// Token: 0x04002799 RID: 10137
		internal string objectName;

		// Token: 0x0400279A RID: 10138
		internal Type objectType;

		// Token: 0x0400279B RID: 10139
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x0400279C RID: 10140
		internal object[] typeInformationA;

		// Token: 0x0400279D RID: 10141
		internal Type[] memberTypes;

		// Token: 0x0400279E RID: 10142
		internal string[] memberNames;

		// Token: 0x0400279F RID: 10143
		internal ReadObjectInfo objectInfo;

		// Token: 0x040027A0 RID: 10144
		internal bool isInitObjectInfo = true;

		// Token: 0x040027A1 RID: 10145
		internal ObjectReader objectReader;

		// Token: 0x040027A2 RID: 10146
		internal int objectId;

		// Token: 0x040027A3 RID: 10147
		internal BinaryAssemblyInfo assemblyInfo;
	}
}
