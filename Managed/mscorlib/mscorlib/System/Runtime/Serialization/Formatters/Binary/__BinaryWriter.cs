using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200072F RID: 1839
	internal sealed class __BinaryWriter
	{
		// Token: 0x06004C09 RID: 19465 RVA: 0x0010F8CC File Offset: 0x0010DACC
		internal __BinaryWriter(Stream sout, ObjectWriter objectWriter, FormatterTypeStyle formatterTypeStyle)
		{
			this.sout = sout;
			this.formatterTypeStyle = formatterTypeStyle;
			this.objectWriter = objectWriter;
			this.m_nestedObjectCount = 0;
			this.dataWriter = new BinaryWriter(sout, Encoding.UTF8);
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x00002194 File Offset: 0x00000394
		internal void WriteBegin()
		{
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x0010F90C File Offset: 0x0010DB0C
		internal void WriteEnd()
		{
			this.dataWriter.Flush();
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x0010F919 File Offset: 0x0010DB19
		internal void WriteBoolean(bool value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x0010F927 File Offset: 0x0010DB27
		internal void WriteByte(byte value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x0010F935 File Offset: 0x0010DB35
		private void WriteBytes(byte[] value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x0010F943 File Offset: 0x0010DB43
		private void WriteBytes(byte[] byteA, int offset, int size)
		{
			this.dataWriter.Write(byteA, offset, size);
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x0010F953 File Offset: 0x0010DB53
		internal void WriteChar(char value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x0010F961 File Offset: 0x0010DB61
		internal void WriteChars(char[] value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x0010F96F File Offset: 0x0010DB6F
		internal void WriteDecimal(decimal value)
		{
			this.WriteString(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x0010F983 File Offset: 0x0010DB83
		internal void WriteSingle(float value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x0010F991 File Offset: 0x0010DB91
		internal void WriteDouble(double value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x0010F99F File Offset: 0x0010DB9F
		internal void WriteInt16(short value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x0010F9AD File Offset: 0x0010DBAD
		internal void WriteInt32(int value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x0010F9BB File Offset: 0x0010DBBB
		internal void WriteInt64(long value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x0010F9C9 File Offset: 0x0010DBC9
		internal void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x0010F9D3 File Offset: 0x0010DBD3
		internal void WriteString(string value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x0010F9E1 File Offset: 0x0010DBE1
		internal void WriteTimeSpan(TimeSpan value)
		{
			this.WriteInt64(value.Ticks);
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x0010F9F0 File Offset: 0x0010DBF0
		internal void WriteDateTime(DateTime value)
		{
			this.WriteInt64(value.ToBinaryRaw());
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x0010F9FF File Offset: 0x0010DBFF
		internal void WriteUInt16(ushort value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x0010FA0D File Offset: 0x0010DC0D
		internal void WriteUInt32(uint value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x0010FA1B File Offset: 0x0010DC1B
		internal void WriteUInt64(ulong value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x00002194 File Offset: 0x00000394
		internal void WriteObjectEnd(NameInfo memberNameInfo, NameInfo typeNameInfo)
		{
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x0010FA29 File Offset: 0x0010DC29
		internal void WriteSerializationHeaderEnd()
		{
			MessageEnd messageEnd = new MessageEnd();
			messageEnd.Dump(this.sout);
			messageEnd.Write(this);
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x0010FA42 File Offset: 0x0010DC42
		internal void WriteSerializationHeader(int topId, int headerId, int minorVersion, int majorVersion)
		{
			SerializationHeaderRecord serializationHeaderRecord = new SerializationHeaderRecord(BinaryHeaderEnum.SerializedStreamHeader, topId, headerId, minorVersion, majorVersion);
			serializationHeaderRecord.Dump();
			serializationHeaderRecord.Write(this);
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x0010FA5B File Offset: 0x0010DC5B
		internal void WriteMethodCall()
		{
			if (this.binaryMethodCall == null)
			{
				this.binaryMethodCall = new BinaryMethodCall();
			}
			this.binaryMethodCall.Dump();
			this.binaryMethodCall.Write(this);
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x0010FA88 File Offset: 0x0010DC88
		internal object[] WriteCallArray(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, object callContext, object[] properties)
		{
			if (this.binaryMethodCall == null)
			{
				this.binaryMethodCall = new BinaryMethodCall();
			}
			return this.binaryMethodCall.WriteArray(uri, methodName, typeName, instArgs, args, methodSignature, callContext, properties);
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0010FAC0 File Offset: 0x0010DCC0
		internal void WriteMethodReturn()
		{
			if (this.binaryMethodReturn == null)
			{
				this.binaryMethodReturn = new BinaryMethodReturn();
			}
			this.binaryMethodReturn.Dump();
			this.binaryMethodReturn.Write(this);
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0010FAEC File Offset: 0x0010DCEC
		internal object[] WriteReturnArray(object returnValue, object[] args, Exception exception, object callContext, object[] properties)
		{
			if (this.binaryMethodReturn == null)
			{
				this.binaryMethodReturn = new BinaryMethodReturn();
			}
			return this.binaryMethodReturn.WriteArray(returnValue, args, exception, callContext, properties);
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0010FB14 File Offset: 0x0010DD14
		internal void WriteObject(NameInfo nameInfo, NameInfo typeNameInfo, int numMembers, string[] memberNames, Type[] memberTypes, WriteObjectInfo[] memberObjectInfos)
		{
			this.InternalWriteItemNull();
			int num = (int)nameInfo.NIobjectId;
			string text;
			if (num < 0)
			{
				text = typeNameInfo.NIname;
			}
			else
			{
				text = nameInfo.NIname;
			}
			if (this.objectMapTable == null)
			{
				this.objectMapTable = new Hashtable();
			}
			ObjectMapInfo objectMapInfo = (ObjectMapInfo)this.objectMapTable[text];
			if (objectMapInfo != null && objectMapInfo.isCompatible(numMembers, memberNames, memberTypes))
			{
				if (this.binaryObject == null)
				{
					this.binaryObject = new BinaryObject();
				}
				this.binaryObject.Set(num, objectMapInfo.objectId);
				this.binaryObject.Write(this);
				return;
			}
			if (!typeNameInfo.NItransmitTypeOnObject)
			{
				if (this.binaryObjectWithMap == null)
				{
					this.binaryObjectWithMap = new BinaryObjectWithMap();
				}
				int num2 = (int)typeNameInfo.NIassemId;
				this.binaryObjectWithMap.Set(num, text, numMembers, memberNames, num2);
				this.binaryObjectWithMap.Dump();
				this.binaryObjectWithMap.Write(this);
				if (objectMapInfo == null)
				{
					this.objectMapTable.Add(text, new ObjectMapInfo(num, numMembers, memberNames, memberTypes));
					return;
				}
			}
			else
			{
				BinaryTypeEnum[] array = new BinaryTypeEnum[numMembers];
				object[] array2 = new object[numMembers];
				int[] array3 = new int[numMembers];
				int num2;
				for (int i = 0; i < numMembers; i++)
				{
					object obj = null;
					array[i] = BinaryConverter.GetBinaryTypeInfo(memberTypes[i], memberObjectInfos[i], null, this.objectWriter, out obj, out num2);
					array2[i] = obj;
					array3[i] = num2;
				}
				if (this.binaryObjectWithMapTyped == null)
				{
					this.binaryObjectWithMapTyped = new BinaryObjectWithMapTyped();
				}
				num2 = (int)typeNameInfo.NIassemId;
				this.binaryObjectWithMapTyped.Set(num, text, numMembers, memberNames, array, array2, array3, num2);
				this.binaryObjectWithMapTyped.Write(this);
				if (objectMapInfo == null)
				{
					this.objectMapTable.Add(text, new ObjectMapInfo(num, numMembers, memberNames, memberTypes));
				}
			}
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0010FCC8 File Offset: 0x0010DEC8
		internal void WriteObjectString(int objectId, string value)
		{
			this.InternalWriteItemNull();
			if (this.binaryObjectString == null)
			{
				this.binaryObjectString = new BinaryObjectString();
			}
			this.binaryObjectString.Set(objectId, value);
			this.binaryObjectString.Write(this);
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0010FCFC File Offset: 0x0010DEFC
		[SecurityCritical]
		internal void WriteSingleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, Array array)
		{
			this.InternalWriteItemNull();
			int[] array2 = new int[] { length };
			int[] array3 = null;
			object obj = null;
			BinaryArrayTypeEnum binaryArrayTypeEnum;
			if (lowerBound == 0)
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
			}
			else
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.SingleOffset;
				array3 = new int[] { lowerBound };
			}
			int num;
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out obj, out num);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, 1, array2, array3, binaryTypeInfo, obj, binaryArrayTypeEnum, num);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
			if (Converter.IsWriteAsByteArray(arrayElemTypeNameInfo.NIprimitiveTypeEnum) && lowerBound == 0)
			{
				if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Byte)
				{
					this.WriteBytes((byte[])array);
					return;
				}
				if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Char)
				{
					this.WriteChars((char[])array);
					return;
				}
				this.WriteArrayAsBytes(array, Converter.TypeLength(arrayElemTypeNameInfo.NIprimitiveTypeEnum));
			}
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x0010FDF0 File Offset: 0x0010DFF0
		[SecurityCritical]
		private void WriteArrayAsBytes(Array array, int typeLength)
		{
			this.InternalWriteItemNull();
			int i = 0;
			if (this.byteBuffer == null)
			{
				this.byteBuffer = new byte[this.chunkSize];
			}
			while (i < array.Length)
			{
				int num = Math.Min(this.chunkSize / typeLength, array.Length - i);
				int num2 = num * typeLength;
				Buffer.InternalBlockCopy(array, i * typeLength, this.byteBuffer, 0, num2);
				if (!BitConverter.IsLittleEndian)
				{
					for (int j = 0; j < num2; j += typeLength)
					{
						for (int k = 0; k < typeLength / 2; k++)
						{
							byte b = this.byteBuffer[j + k];
							this.byteBuffer[j + k] = this.byteBuffer[j + typeLength - 1 - k];
							this.byteBuffer[j + typeLength - 1 - k] = b;
						}
					}
				}
				this.WriteBytes(this.byteBuffer, 0, num2);
				i += num;
			}
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x0010FED0 File Offset: 0x0010E0D0
		internal void WriteJaggedArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound)
		{
			this.InternalWriteItemNull();
			int[] array = new int[] { length };
			int[] array2 = null;
			object obj = null;
			int num = 0;
			BinaryArrayTypeEnum binaryArrayTypeEnum;
			if (lowerBound == 0)
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.Jagged;
			}
			else
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.JaggedOffset;
				array2 = new int[] { lowerBound };
			}
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out obj, out num);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, 1, array, array2, binaryTypeInfo, obj, binaryArrayTypeEnum, num);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x0010FF70 File Offset: 0x0010E170
		internal void WriteRectangleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int rank, int[] lengthA, int[] lowerBoundA)
		{
			this.InternalWriteItemNull();
			BinaryArrayTypeEnum binaryArrayTypeEnum = BinaryArrayTypeEnum.Rectangular;
			object obj = null;
			int num = 0;
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out obj, out num);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			for (int i = 0; i < rank; i++)
			{
				if (lowerBoundA[i] != 0)
				{
					binaryArrayTypeEnum = BinaryArrayTypeEnum.RectangularOffset;
					break;
				}
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, rank, lengthA, lowerBoundA, binaryTypeInfo, obj, binaryArrayTypeEnum, num);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x00110009 File Offset: 0x0010E209
		[SecurityCritical]
		internal void WriteObjectByteArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, byte[] byteA)
		{
			this.InternalWriteItemNull();
			this.WriteSingleArray(memberNameInfo, arrayNameInfo, objectInfo, arrayElemTypeNameInfo, length, lowerBound, byteA);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x00110024 File Offset: 0x0010E224
		internal void WriteMember(NameInfo memberNameInfo, NameInfo typeNameInfo, object value)
		{
			this.InternalWriteItemNull();
			InternalPrimitiveTypeE niprimitiveTypeEnum = typeNameInfo.NIprimitiveTypeEnum;
			if (memberNameInfo.NItransmitTypeOnMember)
			{
				if (this.memberPrimitiveTyped == null)
				{
					this.memberPrimitiveTyped = new MemberPrimitiveTyped();
				}
				this.memberPrimitiveTyped.Set(niprimitiveTypeEnum, value);
				bool niisArrayItem = memberNameInfo.NIisArrayItem;
				this.memberPrimitiveTyped.Dump();
				this.memberPrimitiveTyped.Write(this);
				return;
			}
			if (this.memberPrimitiveUnTyped == null)
			{
				this.memberPrimitiveUnTyped = new MemberPrimitiveUnTyped();
			}
			this.memberPrimitiveUnTyped.Set(niprimitiveTypeEnum, value);
			bool niisArrayItem2 = memberNameInfo.NIisArrayItem;
			this.memberPrimitiveUnTyped.Dump();
			this.memberPrimitiveUnTyped.Write(this);
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x001100C4 File Offset: 0x0010E2C4
		internal void WriteNullMember(NameInfo memberNameInfo, NameInfo typeNameInfo)
		{
			this.InternalWriteItemNull();
			if (this.objectNull == null)
			{
				this.objectNull = new ObjectNull();
			}
			if (!memberNameInfo.NIisArrayItem)
			{
				this.objectNull.SetNullCount(1);
				this.objectNull.Dump();
				this.objectNull.Write(this);
				this.nullCount = 0;
			}
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x0011011C File Offset: 0x0010E31C
		internal void WriteMemberObjectRef(NameInfo memberNameInfo, int idRef)
		{
			this.InternalWriteItemNull();
			if (this.memberReference == null)
			{
				this.memberReference = new MemberReference();
			}
			this.memberReference.Set(idRef);
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
			this.memberReference.Dump();
			this.memberReference.Write(this);
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0011016C File Offset: 0x0010E36C
		internal void WriteMemberNested(NameInfo memberNameInfo)
		{
			this.InternalWriteItemNull();
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0011017B File Offset: 0x0010E37B
		internal void WriteMemberString(NameInfo memberNameInfo, NameInfo typeNameInfo, string value)
		{
			this.InternalWriteItemNull();
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
			this.WriteObjectString((int)typeNameInfo.NIobjectId, value);
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x00110198 File Offset: 0x0010E398
		internal void WriteItem(NameInfo itemNameInfo, NameInfo typeNameInfo, object value)
		{
			this.InternalWriteItemNull();
			this.WriteMember(itemNameInfo, typeNameInfo, value);
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x001101A9 File Offset: 0x0010E3A9
		internal void WriteNullItem(NameInfo itemNameInfo, NameInfo typeNameInfo)
		{
			this.nullCount++;
			this.InternalWriteItemNull();
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x001101BF File Offset: 0x0010E3BF
		internal void WriteDelayedNullItem()
		{
			this.nullCount++;
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x001101CF File Offset: 0x0010E3CF
		internal void WriteItemEnd()
		{
			this.InternalWriteItemNull();
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x001101D8 File Offset: 0x0010E3D8
		private void InternalWriteItemNull()
		{
			if (this.nullCount > 0)
			{
				if (this.objectNull == null)
				{
					this.objectNull = new ObjectNull();
				}
				this.objectNull.SetNullCount(this.nullCount);
				this.objectNull.Dump();
				this.objectNull.Write(this);
				this.nullCount = 0;
			}
		}

		// Token: 0x06004C37 RID: 19511 RVA: 0x00110230 File Offset: 0x0010E430
		internal void WriteItemObjectRef(NameInfo nameInfo, int idRef)
		{
			this.InternalWriteItemNull();
			this.WriteMemberObjectRef(nameInfo, idRef);
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x00110240 File Offset: 0x0010E440
		internal void WriteAssembly(Type type, string assemblyString, int assemId, bool isNew)
		{
			this.InternalWriteItemNull();
			if (assemblyString == null)
			{
				assemblyString = string.Empty;
			}
			if (isNew)
			{
				if (this.binaryAssembly == null)
				{
					this.binaryAssembly = new BinaryAssembly();
				}
				this.binaryAssembly.Set(assemId, assemblyString);
				this.binaryAssembly.Dump();
				this.binaryAssembly.Write(this);
			}
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x00110298 File Offset: 0x0010E498
		internal void WriteValue(InternalPrimitiveTypeE code, object value)
		{
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.WriteBoolean(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Byte:
				this.WriteByte(Convert.ToByte(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Char:
				this.WriteChar(Convert.ToChar(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Decimal:
				this.WriteDecimal(Convert.ToDecimal(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Double:
				this.WriteDouble(Convert.ToDouble(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int16:
				this.WriteInt16(Convert.ToInt16(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int32:
				this.WriteInt32(Convert.ToInt32(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int64:
				this.WriteInt64(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.SByte:
				this.WriteSByte(Convert.ToSByte(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Single:
				this.WriteSingle(Convert.ToSingle(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.TimeSpan:
				this.WriteTimeSpan((TimeSpan)value);
				return;
			case InternalPrimitiveTypeE.DateTime:
				this.WriteDateTime((DateTime)value);
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.WriteUInt16(Convert.ToUInt16(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.WriteUInt32(Convert.ToUInt32(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.WriteUInt64(Convert.ToUInt64(value, CultureInfo.InvariantCulture));
				return;
			}
			throw new SerializationException(Environment.GetResourceString("Invalid type code in stream '{0}'.", new object[] { code.ToString() }));
		}

		// Token: 0x04002886 RID: 10374
		internal Stream sout;

		// Token: 0x04002887 RID: 10375
		internal FormatterTypeStyle formatterTypeStyle;

		// Token: 0x04002888 RID: 10376
		internal Hashtable objectMapTable;

		// Token: 0x04002889 RID: 10377
		internal ObjectWriter objectWriter;

		// Token: 0x0400288A RID: 10378
		internal BinaryWriter dataWriter;

		// Token: 0x0400288B RID: 10379
		internal int m_nestedObjectCount;

		// Token: 0x0400288C RID: 10380
		private int nullCount;

		// Token: 0x0400288D RID: 10381
		internal BinaryMethodCall binaryMethodCall;

		// Token: 0x0400288E RID: 10382
		internal BinaryMethodReturn binaryMethodReturn;

		// Token: 0x0400288F RID: 10383
		internal BinaryObject binaryObject;

		// Token: 0x04002890 RID: 10384
		internal BinaryObjectWithMap binaryObjectWithMap;

		// Token: 0x04002891 RID: 10385
		internal BinaryObjectWithMapTyped binaryObjectWithMapTyped;

		// Token: 0x04002892 RID: 10386
		internal BinaryObjectString binaryObjectString;

		// Token: 0x04002893 RID: 10387
		internal BinaryArray binaryArray;

		// Token: 0x04002894 RID: 10388
		private byte[] byteBuffer;

		// Token: 0x04002895 RID: 10389
		private int chunkSize = 4096;

		// Token: 0x04002896 RID: 10390
		internal MemberPrimitiveUnTyped memberPrimitiveUnTyped;

		// Token: 0x04002897 RID: 10391
		internal MemberPrimitiveTyped memberPrimitiveTyped;

		// Token: 0x04002898 RID: 10392
		internal ObjectNull objectNull;

		// Token: 0x04002899 RID: 10393
		internal MemberReference memberReference;

		// Token: 0x0400289A RID: 10394
		internal BinaryAssembly binaryAssembly;
	}
}
