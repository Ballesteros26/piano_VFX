using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	/// <summary>Helps build custom attributes.</summary>
	// Token: 0x0200034C RID: 844
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_CustomAttributeBuilder))]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class CustomAttributeBuilder : _CustomAttributeBuilder
	{
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x00086AFF File Offset: 0x00084CFF
		internal ConstructorInfo Ctor
		{
			get
			{
				return this.ctor;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x00086B07 File Offset: 0x00084D07
		internal byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06002590 RID: 9616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] GetBlob(Assembly asmb, ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues);

		// Token: 0x06002591 RID: 9617 RVA: 0x00086B10 File Offset: 0x00084D10
		internal object Invoke()
		{
			object obj = this.ctor.Invoke(this.args);
			for (int i = 0; i < this.namedFields.Length; i++)
			{
				this.namedFields[i].SetValue(obj, this.fieldValues[i]);
			}
			for (int j = 0; j < this.namedProperties.Length; j++)
			{
				this.namedProperties[j].SetValue(obj, this.propertyValues[j]);
			}
			return obj;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00086B84 File Offset: 0x00084D84
		internal CustomAttributeBuilder(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.ctor = con;
			this.data = (byte[])binaryAttribute.Clone();
		}

		/// <summary>Initializes an instance of the CustomAttributeBuilder class given the constructor for the custom attribute and the arguments to the constructor.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="con" /> is static or private.-or- The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.-or- The type of supplied argument does not match the type of the parameter declared in the constructor. -or-A supplied argument is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="constructorArgs" /> is null. </exception>
		// Token: 0x06002593 RID: 9619 RVA: 0x00086BD1 File Offset: 0x00084DD1
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs)
		{
			this.Initialize(con, constructorArgs, new PropertyInfo[0], new object[0], new FieldInfo[0], new object[0]);
		}

		/// <summary>Initializes an instance of the CustomAttributeBuilder class given the constructor for the custom attribute, the arguments to the constructor, and a set of named field/value pairs.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute. </param>
		/// <param name="namedFields">Named fields of the custom attribute. </param>
		/// <param name="fieldValues">Values for the named fields of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentException">The lengths of the <paramref name="namedFields" /> and <paramref name="fieldValues" /> arrays are different.-or- <paramref name="con" /> is static or private.-or- The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.-or- The type of supplied argument does not match the type of the parameter declared in the constructor.-or- The types of the field values do not match the types of the named fields.-or- The field does not belong to the same class or base class as the constructor. -or-A supplied argument or named field is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters is null. </exception>
		// Token: 0x06002594 RID: 9620 RVA: 0x00086BF9 File Offset: 0x00084DF9
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.Initialize(con, constructorArgs, new PropertyInfo[0], new object[0], namedFields, fieldValues);
		}

		/// <summary>Initializes an instance of the CustomAttributeBuilder class given the constructor for the custom attribute, the arguments to the constructor, and a set of named property or value pairs.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute. </param>
		/// <param name="namedProperties">Named properties of the custom attribute. </param>
		/// <param name="propertyValues">Values for the named properties of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentException">The lengths of the <paramref name="namedProperties" /> and <paramref name="propertyValues" /> arrays are different.-or- <paramref name="con" /> is static or private.-or- The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.-or- The type of supplied argument does not match the type of the parameter declared in the constructor.-or- The types of the property values do not match the types of the named properties.-or- A property has no setter method.-or- The property does not belong to the same class or base class as the constructor. -or-A supplied argument or named property is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters is null. </exception>
		// Token: 0x06002595 RID: 9621 RVA: 0x00086C18 File Offset: 0x00084E18
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues)
		{
			this.Initialize(con, constructorArgs, namedProperties, propertyValues, new FieldInfo[0], new object[0]);
		}

		/// <summary>Initializes an instance of the CustomAttributeBuilder class given the constructor for the custom attribute, the arguments to the constructor, a set of named property or value pairs, and a set of named field or value pairs.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute. </param>
		/// <param name="namedProperties">Named properties of the custom attribute. </param>
		/// <param name="propertyValues">Values for the named properties of the custom attribute. </param>
		/// <param name="namedFields">Named fields of the custom attribute. </param>
		/// <param name="fieldValues">Values for the named fields of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentException">The lengths of the <paramref name="namedProperties" /> and <paramref name="propertyValues" /> arrays are different.-or- The lengths of the <paramref name="namedFields" /> and <paramref name="fieldValues" /> arrays are different.-or- <paramref name="con" /> is static or private.-or- The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.-or- The type of supplied argument does not match the type of the parameter declared in the constructor.-or- The types of the property values do not match the types of the named properties.-or- The types of the field values do not match the types of the corresponding field types.-or- A property has no setter.-or- The property or field does not belong to the same class or base class as the constructor. -or-A supplied argument, named property, or named field is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters is null. </exception>
		// Token: 0x06002596 RID: 9622 RVA: 0x00086C37 File Offset: 0x00084E37
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.Initialize(con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x00086C50 File Offset: 0x00084E50
		private bool IsValidType(Type t)
		{
			if (t.IsArray && t.GetArrayRank() > 1)
			{
				return false;
			}
			if (t is TypeBuilder && t.IsEnum)
			{
				Enum.GetUnderlyingType(t);
			}
			return (!t.IsClass || t.IsArray || t == typeof(object) || t == typeof(Type) || t == typeof(string) || t.Assembly.GetName().Name == "mscorlib") && (!t.IsValueType || t.IsPrimitive || t.IsEnum || (t.Assembly is AssemblyBuilder && t.Assembly.GetName().Name == "mscorlib"));
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x00086D30 File Offset: 0x00084F30
		private bool IsValidParam(object o, Type paramType)
		{
			Type type = o.GetType();
			if (!this.IsValidType(type))
			{
				return false;
			}
			if (paramType == typeof(object))
			{
				if (type.IsArray && type.GetArrayRank() == 1)
				{
					return this.IsValidType(type.GetElementType());
				}
				if (!type.IsPrimitive && !typeof(Type).IsAssignableFrom(type) && type != typeof(string) && !type.IsEnum)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x00086DB8 File Offset: 0x00084FB8
		private static bool IsValidValue(Type type, object value)
		{
			if (type.IsValueType && value == null)
			{
				return false;
			}
			if (type.IsArray && type.GetElementType().IsValueType)
			{
				using (IEnumerator enumerator = ((Array)value).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							return false;
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00086E30 File Offset: 0x00085030
		private void Initialize(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.ctor = con;
			this.args = constructorArgs;
			this.namedProperties = namedProperties;
			this.propertyValues = propertyValues;
			this.namedFields = namedFields;
			this.fieldValues = fieldValues;
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (constructorArgs == null)
			{
				throw new ArgumentNullException("constructorArgs");
			}
			if (namedProperties == null)
			{
				throw new ArgumentNullException("namedProperties");
			}
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (namedFields == null)
			{
				throw new ArgumentNullException("namedFields");
			}
			if (fieldValues == null)
			{
				throw new ArgumentNullException("fieldValues");
			}
			if (con.GetParametersCount() != constructorArgs.Length)
			{
				throw new ArgumentException("Parameter count does not match passed in argument value count.");
			}
			if (namedProperties.Length != propertyValues.Length)
			{
				throw new ArgumentException("Array lengths must be the same.", "namedProperties, propertyValues");
			}
			if (namedFields.Length != fieldValues.Length)
			{
				throw new ArgumentException("Array lengths must be the same.", "namedFields, fieldValues");
			}
			if ((con.Attributes & MethodAttributes.Static) == MethodAttributes.Static || (con.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
			{
				throw new ArgumentException("Cannot have private or static constructor.");
			}
			Type declaringType = this.ctor.DeclaringType;
			int num = 0;
			foreach (FieldInfo fieldInfo in namedFields)
			{
				Type declaringType2 = fieldInfo.DeclaringType;
				if (declaringType != declaringType2 && !declaringType2.IsSubclassOf(declaringType) && !declaringType.IsSubclassOf(declaringType2))
				{
					throw new ArgumentException("Field '" + fieldInfo.Name + "' does not belong to the same class as the constructor");
				}
				if (!this.IsValidType(fieldInfo.FieldType))
				{
					throw new ArgumentException("Field '" + fieldInfo.Name + "' does not have a valid type.");
				}
				if (!CustomAttributeBuilder.IsValidValue(fieldInfo.FieldType, fieldValues[num]))
				{
					throw new ArgumentException("Field " + fieldInfo.Name + " is not a valid value.");
				}
				if (fieldValues[num] != null && !(fieldInfo.FieldType is TypeBuilder) && !fieldInfo.FieldType.IsEnum && !fieldInfo.FieldType.IsInstanceOfType(fieldValues[num]) && !fieldInfo.FieldType.IsArray)
				{
					throw new ArgumentException(string.Concat(new object[] { "Value of field '", fieldInfo.Name, "' does not match field type: ", fieldInfo.FieldType }));
				}
				num++;
			}
			num = 0;
			foreach (PropertyInfo propertyInfo in namedProperties)
			{
				if (!propertyInfo.CanWrite)
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not have a setter.");
				}
				Type declaringType3 = propertyInfo.DeclaringType;
				if (declaringType != declaringType3 && !declaringType3.IsSubclassOf(declaringType) && !declaringType.IsSubclassOf(declaringType3))
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not belong to the same class as the constructor");
				}
				if (!this.IsValidType(propertyInfo.PropertyType))
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not have a valid type.");
				}
				if (!CustomAttributeBuilder.IsValidValue(propertyInfo.PropertyType, propertyValues[num]))
				{
					throw new ArgumentException("Property " + propertyInfo.Name + " is not a valid value.");
				}
				if (propertyValues[num] != null && !(propertyInfo.PropertyType is TypeBuilder) && !propertyInfo.PropertyType.IsEnum && !propertyInfo.PropertyType.IsInstanceOfType(propertyValues[num]) && !propertyInfo.PropertyType.IsArray)
				{
					throw new ArgumentException(string.Concat(new object[]
					{
						"Value of property '",
						propertyInfo.Name,
						"' does not match property type: ",
						propertyInfo.PropertyType,
						" -> ",
						propertyValues[num]
					}));
				}
				num++;
			}
			num = 0;
			foreach (ParameterInfo parameterInfo in CustomAttributeBuilder.GetParameters(con))
			{
				if (parameterInfo != null)
				{
					Type parameterType = parameterInfo.ParameterType;
					if (!this.IsValidType(parameterType))
					{
						throw new ArgumentException("Parameter " + num + " does not have a valid type.");
					}
					if (!CustomAttributeBuilder.IsValidValue(parameterType, constructorArgs[num]))
					{
						throw new ArgumentException("Parameter " + num + " is not a valid value.");
					}
					if (constructorArgs[num] != null)
					{
						if (!(parameterType is TypeBuilder) && !parameterType.IsEnum && !parameterType.IsInstanceOfType(constructorArgs[num]) && !parameterType.IsArray)
						{
							throw new ArgumentException(string.Concat(new object[]
							{
								"Value of argument ",
								num,
								" does not match parameter type: ",
								parameterType,
								" -> ",
								constructorArgs[num]
							}));
						}
						if (!this.IsValidParam(constructorArgs[num], parameterType))
						{
							throw new ArgumentException("Cannot emit a CustomAttribute with argument of type " + constructorArgs[num].GetType() + ".");
						}
					}
				}
				num++;
			}
			this.data = CustomAttributeBuilder.GetBlob(declaringType.Assembly, con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x00087318 File Offset: 0x00085518
		internal static int decode_len(byte[] data, int pos, out int rpos)
		{
			int num;
			if ((data[pos] & 128) == 0)
			{
				num = (int)(data[pos++] & 127);
			}
			else if ((data[pos] & 64) == 0)
			{
				num = ((int)(data[pos] & 63) << 8) + (int)data[pos + 1];
				pos += 2;
			}
			else
			{
				num = ((int)(data[pos] & 31) << 24) + ((int)data[pos + 1] << 16) + ((int)data[pos + 2] << 8) + (int)data[pos + 3];
				pos += 4;
			}
			rpos = pos;
			return num;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x00087388 File Offset: 0x00085588
		internal static string string_from_bytes(byte[] data, int pos, int len)
		{
			return Encoding.UTF8.GetString(data, pos, len);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x00087398 File Offset: 0x00085598
		internal string string_arg()
		{
			int num = 2;
			int num2 = CustomAttributeBuilder.decode_len(this.data, num, out num);
			return CustomAttributeBuilder.string_from_bytes(this.data, num, num2);
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000873C4 File Offset: 0x000855C4
		internal static UnmanagedMarshal get_umarshal(CustomAttributeBuilder customBuilder, bool is_field)
		{
			byte[] array = customBuilder.Data;
			UnmanagedType unmanagedType = (UnmanagedType)80;
			int num = -1;
			int num2 = -1;
			bool flag = false;
			string text = null;
			Type type = null;
			string text2 = string.Empty;
			int num3 = (int)array[2];
			num3 |= (int)array[3] << 8;
			string fullName = CustomAttributeBuilder.GetParameters(customBuilder.Ctor)[0].ParameterType.FullName;
			int num4 = 6;
			if (fullName == "System.Int16")
			{
				num4 = 4;
			}
			int num5 = (int)array[num4++];
			num5 |= (int)array[num4++] << 8;
			int i = 0;
			while (i < num5)
			{
				num4++;
				if (array[num4++] == 85)
				{
					int num6 = CustomAttributeBuilder.decode_len(array, num4, out num4);
					CustomAttributeBuilder.string_from_bytes(array, num4, num6);
					num4 += num6;
				}
				int num7 = CustomAttributeBuilder.decode_len(array, num4, out num4);
				string text3 = CustomAttributeBuilder.string_from_bytes(array, num4, num7);
				num4 += num7;
				uint num8 = <PrivateImplementationDetails>.ComputeStringHash(text3);
				if (num8 <= 2523910760U)
				{
					if (num8 <= 1554623949U)
					{
						if (num8 != 67206855U)
						{
							if (num8 != 1554623949U)
							{
								goto IL_0381;
							}
							if (!(text3 == "SafeArraySubType"))
							{
								goto IL_0381;
							}
							unmanagedType = (UnmanagedType)((int)array[num4++] | ((int)array[num4++] << 8) | ((int)array[num4++] << 16) | ((int)array[num4++] << 24));
						}
						else
						{
							if (!(text3 == "MarshalCookie"))
							{
								goto IL_0381;
							}
							num7 = CustomAttributeBuilder.decode_len(array, num4, out num4);
							text2 = CustomAttributeBuilder.string_from_bytes(array, num4, num7);
							num4 += num7;
						}
					}
					else if (num8 != 1823397059U)
					{
						if (num8 != 2523910760U)
						{
							goto IL_0381;
						}
						if (!(text3 == "IidParameterIndex"))
						{
							goto IL_0381;
						}
						num4 += 4;
					}
					else
					{
						if (!(text3 == "SizeParamIndex"))
						{
							goto IL_0381;
						}
						num2 = (int)array[num4++] | ((int)array[num4++] << 8);
						flag = true;
					}
				}
				else if (num8 <= 2658176172U)
				{
					if (num8 != 2546868066U)
					{
						if (num8 != 2658176172U)
						{
							goto IL_0381;
						}
						if (!(text3 == "ArraySubType"))
						{
							goto IL_0381;
						}
						unmanagedType = (UnmanagedType)((int)array[num4++] | ((int)array[num4++] << 8) | ((int)array[num4++] << 16) | ((int)array[num4++] << 24));
					}
					else
					{
						if (!(text3 == "MarshalTypeRef"))
						{
							goto IL_0381;
						}
						num7 = CustomAttributeBuilder.decode_len(array, num4, out num4);
						text = CustomAttributeBuilder.string_from_bytes(array, num4, num7);
						type = Type.GetType(text);
						num4 += num7;
					}
				}
				else if (num8 != 2784686469U)
				{
					if (num8 != 3888525279U)
					{
						if (num8 != 4141739223U)
						{
							goto IL_0381;
						}
						if (!(text3 == "SafeArrayUserDefinedSubType"))
						{
							goto IL_0381;
						}
						num7 = CustomAttributeBuilder.decode_len(array, num4, out num4);
						CustomAttributeBuilder.string_from_bytes(array, num4, num7);
						num4 += num7;
					}
					else
					{
						if (!(text3 == "SizeConst"))
						{
							goto IL_0381;
						}
						num = (int)array[num4++] | ((int)array[num4++] << 8) | ((int)array[num4++] << 16) | ((int)array[num4++] << 24);
						flag = true;
					}
				}
				else
				{
					if (!(text3 == "MarshalType"))
					{
						goto IL_0381;
					}
					num7 = CustomAttributeBuilder.decode_len(array, num4, out num4);
					text = CustomAttributeBuilder.string_from_bytes(array, num4, num7);
					num4 += num7;
				}
				i++;
				continue;
				IL_0381:
				throw new Exception("Unknown MarshalAsAttribute field: " + text3);
			}
			UnmanagedType unmanagedType2 = (UnmanagedType)num3;
			if (unmanagedType2 <= UnmanagedType.SafeArray)
			{
				if (unmanagedType2 == UnmanagedType.ByValTStr)
				{
					return UnmanagedMarshal.DefineByValTStr(num);
				}
				if (unmanagedType2 == UnmanagedType.SafeArray)
				{
					return UnmanagedMarshal.DefineSafeArray(unmanagedType);
				}
			}
			else if (unmanagedType2 != UnmanagedType.ByValArray)
			{
				if (unmanagedType2 != UnmanagedType.LPArray)
				{
					if (unmanagedType2 == UnmanagedType.CustomMarshaler)
					{
						return UnmanagedMarshal.DefineCustom(type, text2, text, Guid.Empty);
					}
				}
				else
				{
					if (flag)
					{
						return UnmanagedMarshal.DefineLPArrayInternal(unmanagedType, num, num2);
					}
					return UnmanagedMarshal.DefineLPArray(unmanagedType);
				}
			}
			else
			{
				if (!is_field)
				{
					throw new ArgumentException("Specified unmanaged type is only valid on fields");
				}
				return UnmanagedMarshal.DefineByValArray(num);
			}
			return UnmanagedMarshal.DefineUnmanagedMarshal((UnmanagedType)num3);
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000877F0 File Offset: 0x000859F0
		private static Type elementTypeToType(int elementType)
		{
			switch (elementType)
			{
			case 2:
				return typeof(bool);
			case 3:
				return typeof(char);
			case 4:
				return typeof(sbyte);
			case 5:
				return typeof(byte);
			case 6:
				return typeof(short);
			case 7:
				return typeof(ushort);
			case 8:
				return typeof(int);
			case 9:
				return typeof(uint);
			case 10:
				return typeof(long);
			case 11:
				return typeof(ulong);
			case 12:
				return typeof(float);
			case 13:
				return typeof(double);
			case 14:
				return typeof(string);
			default:
				throw new Exception("Unknown element type '" + elementType + "'");
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000878E8 File Offset: 0x00085AE8
		private static object decode_cattr_value(Type t, byte[] data, int pos, out int rpos)
		{
			TypeCode typeCode = Type.GetTypeCode(t);
			if (typeCode <= TypeCode.Boolean)
			{
				if (typeCode != TypeCode.Object)
				{
					if (typeCode == TypeCode.Boolean)
					{
						rpos = pos + 1;
						return data[pos] != 0;
					}
				}
				else
				{
					int num = (int)data[pos];
					pos++;
					if (num >= 2 && num <= 14)
					{
						return CustomAttributeBuilder.decode_cattr_value(CustomAttributeBuilder.elementTypeToType(num), data, pos, out rpos);
					}
					throw new Exception("Subtype '" + num + "' of type object not yet handled in decode_cattr_value");
				}
			}
			else
			{
				if (typeCode == TypeCode.Int32)
				{
					rpos = pos + 4;
					return (int)data[pos] + ((int)data[pos + 1] << 8) + ((int)data[pos + 2] << 16) + ((int)data[pos + 3] << 24);
				}
				if (typeCode == TypeCode.String)
				{
					if (data[pos] == 255)
					{
						rpos = pos + 1;
						return null;
					}
					int num2 = CustomAttributeBuilder.decode_len(data, pos, out pos);
					rpos = pos + num2;
					return CustomAttributeBuilder.string_from_bytes(data, pos, num2);
				}
			}
			throw new Exception("FIXME: Type " + t + " not yet handled in decode_cattr_value.");
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000879D0 File Offset: 0x00085BD0
		internal static CustomAttributeBuilder.CustomAttributeInfo decode_cattr(CustomAttributeBuilder customBuilder)
		{
			byte[] array = customBuilder.Data;
			ConstructorInfo constructorInfo = customBuilder.Ctor;
			int num = 0;
			CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = default(CustomAttributeBuilder.CustomAttributeInfo);
			if (array.Length < 2)
			{
				throw new Exception("Custom attr length is only '" + array.Length + "'");
			}
			if (array[0] != 1 || array[1] != 0)
			{
				throw new Exception("Prolog invalid");
			}
			num = 2;
			ParameterInfo[] parameters = CustomAttributeBuilder.GetParameters(constructorInfo);
			customAttributeInfo.ctor = constructorInfo;
			customAttributeInfo.ctorArgs = new object[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				customAttributeInfo.ctorArgs[i] = CustomAttributeBuilder.decode_cattr_value(parameters[i].ParameterType, array, num, out num);
			}
			int num2 = (int)array[num] + (int)array[num + 1] * 256;
			num += 2;
			customAttributeInfo.namedParamNames = new string[num2];
			customAttributeInfo.namedParamValues = new object[num2];
			for (int j = 0; j < num2; j++)
			{
				int num3 = (int)array[num++];
				int num4 = (int)array[num++];
				string text = null;
				if (num4 == 85)
				{
					int num5 = CustomAttributeBuilder.decode_len(array, num, out num);
					text = CustomAttributeBuilder.string_from_bytes(array, num, num5);
					num += num5;
				}
				int num6 = CustomAttributeBuilder.decode_len(array, num, out num);
				string text2 = CustomAttributeBuilder.string_from_bytes(array, num, num6);
				customAttributeInfo.namedParamNames[j] = text2;
				num += num6;
				if (num3 != 83)
				{
					throw new Exception("Unknown named type: " + num3);
				}
				FieldInfo field = constructorInfo.DeclaringType.GetField(text2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field == null)
				{
					throw new Exception(string.Concat(new object[] { "Custom attribute type '", constructorInfo.DeclaringType, "' doesn't contain a field named '", text2, "'" }));
				}
				object obj = CustomAttributeBuilder.decode_cattr_value(field.FieldType, array, num, out num);
				if (text != null)
				{
					obj = Enum.ToObject(Type.GetType(text), obj);
				}
				customAttributeInfo.namedParamValues[j] = obj;
			}
			return customAttributeInfo;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060025A2 RID: 9634 RVA: 0x0002126B File Offset: 0x0001F46B
		void _CustomAttributeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060025A3 RID: 9635 RVA: 0x0002126B File Offset: 0x0001F46B
		void _CustomAttributeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060025A4 RID: 9636 RVA: 0x0002126B File Offset: 0x0001F46B
		void _CustomAttributeBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060025A5 RID: 9637 RVA: 0x0002126B File Offset: 0x0001F46B
		void _CustomAttributeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x00087BC8 File Offset: 0x00085DC8
		private static ParameterInfo[] GetParameters(ConstructorInfo ctor)
		{
			ConstructorBuilder constructorBuilder = ctor as ConstructorBuilder;
			if (constructorBuilder != null)
			{
				return constructorBuilder.GetParametersInternal();
			}
			return ctor.GetParametersInternal();
		}

		// Token: 0x040013D3 RID: 5075
		private ConstructorInfo ctor;

		// Token: 0x040013D4 RID: 5076
		private byte[] data;

		// Token: 0x040013D5 RID: 5077
		private object[] args;

		// Token: 0x040013D6 RID: 5078
		private PropertyInfo[] namedProperties;

		// Token: 0x040013D7 RID: 5079
		private object[] propertyValues;

		// Token: 0x040013D8 RID: 5080
		private FieldInfo[] namedFields;

		// Token: 0x040013D9 RID: 5081
		private object[] fieldValues;

		// Token: 0x0200034D RID: 845
		internal struct CustomAttributeInfo
		{
			// Token: 0x040013DA RID: 5082
			public ConstructorInfo ctor;

			// Token: 0x040013DB RID: 5083
			public object[] ctorArgs;

			// Token: 0x040013DC RID: 5084
			public string[] namedParamNames;

			// Token: 0x040013DD RID: 5085
			public object[] namedParamValues;
		}
	}
}
