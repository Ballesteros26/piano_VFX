using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000705 RID: 1797
	internal static class IOUtil
	{
		// Token: 0x06004B5C RID: 19292 RVA: 0x0010CE49 File Offset: 0x0010B049
		internal static bool FlagTest(MessageEnum flag, MessageEnum target)
		{
			return (flag & target) == target;
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x0010CE54 File Offset: 0x0010B054
		internal static void WriteStringWithCode(string value, __BinaryWriter sout)
		{
			if (value == null)
			{
				sout.WriteByte(17);
				return;
			}
			sout.WriteByte(18);
			sout.WriteString(value);
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x0010CE74 File Offset: 0x0010B074
		internal static void WriteWithCode(Type type, object value, __BinaryWriter sout)
		{
			if (type == null)
			{
				sout.WriteByte(17);
				return;
			}
			if (type == Converter.typeofString)
			{
				IOUtil.WriteStringWithCode((string)value, sout);
				return;
			}
			InternalPrimitiveTypeE internalPrimitiveTypeE = Converter.ToCode(type);
			sout.WriteByte((byte)internalPrimitiveTypeE);
			sout.WriteValue(internalPrimitiveTypeE, value);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x0010CEBC File Offset: 0x0010B0BC
		internal static object ReadWithCode(__BinaryParser input)
		{
			InternalPrimitiveTypeE internalPrimitiveTypeE = (InternalPrimitiveTypeE)input.ReadByte();
			if (internalPrimitiveTypeE == InternalPrimitiveTypeE.Null)
			{
				return null;
			}
			if (internalPrimitiveTypeE == InternalPrimitiveTypeE.String)
			{
				return input.ReadString();
			}
			return input.ReadValue(internalPrimitiveTypeE);
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x0010CEEC File Offset: 0x0010B0EC
		internal static object[] ReadArgs(__BinaryParser input)
		{
			int num = input.ReadInt32();
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = IOUtil.ReadWithCode(input);
			}
			return array;
		}
	}
}
