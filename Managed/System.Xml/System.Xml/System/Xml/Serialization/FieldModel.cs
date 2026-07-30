using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002FC RID: 764
	internal class FieldModel
	{
		// Token: 0x06001C7A RID: 7290 RVA: 0x0009B98C File Offset: 0x00099B8C
		internal FieldModel(string name, Type fieldType, TypeDesc fieldTypeDesc, bool checkSpecified, bool checkShouldPersist)
			: this(name, fieldType, fieldTypeDesc, checkSpecified, checkShouldPersist, false)
		{
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0009B99C File Offset: 0x00099B9C
		internal FieldModel(string name, Type fieldType, TypeDesc fieldTypeDesc, bool checkSpecified, bool checkShouldPersist, bool readOnly)
		{
			this.fieldTypeDesc = fieldTypeDesc;
			this.name = name;
			this.fieldType = fieldType;
			this.checkSpecified = (checkSpecified ? SpecifiedAccessor.ReadWrite : SpecifiedAccessor.None);
			this.checkShouldPersist = checkShouldPersist;
			this.readOnly = readOnly;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0009B9D8 File Offset: 0x00099BD8
		internal FieldModel(MemberInfo memberInfo, Type fieldType, TypeDesc fieldTypeDesc)
		{
			this.name = memberInfo.Name;
			this.fieldType = fieldType;
			this.fieldTypeDesc = fieldTypeDesc;
			this.memberInfo = memberInfo;
			this.checkShouldPersistMethodInfo = memberInfo.DeclaringType.GetMethod("ShouldSerialize" + memberInfo.Name, new Type[0]);
			this.checkShouldPersist = this.checkShouldPersistMethodInfo != null;
			FieldInfo field = memberInfo.DeclaringType.GetField(memberInfo.Name + "Specified");
			if (field != null)
			{
				if (field.FieldType != typeof(bool))
				{
					throw new InvalidOperationException(Res.GetString("Member '{0}' of type {1} cannot be serialized.  Members with names ending on 'Specified' suffix have special meaning to the XmlSerializer: they control serialization of optional ValueType members and have to be of type {2}.", new object[]
					{
						field.Name,
						field.FieldType.FullName,
						typeof(bool).FullName
					}));
				}
				this.checkSpecified = (field.IsInitOnly ? SpecifiedAccessor.ReadOnly : SpecifiedAccessor.ReadWrite);
				this.checkSpecifiedMemberInfo = field;
			}
			else
			{
				PropertyInfo property = memberInfo.DeclaringType.GetProperty(memberInfo.Name + "Specified");
				if (property != null)
				{
					if (StructModel.CheckPropertyRead(property))
					{
						this.checkSpecified = (property.CanWrite ? SpecifiedAccessor.ReadWrite : SpecifiedAccessor.ReadOnly);
						this.checkSpecifiedMemberInfo = property;
					}
					if (this.checkSpecified != SpecifiedAccessor.None && property.PropertyType != typeof(bool))
					{
						throw new InvalidOperationException(Res.GetString("Member '{0}' of type {1} cannot be serialized.  Members with names ending on 'Specified' suffix have special meaning to the XmlSerializer: they control serialization of optional ValueType members and have to be of type {2}.", new object[]
						{
							property.Name,
							property.PropertyType.FullName,
							typeof(bool).FullName
						}));
					}
				}
			}
			if (memberInfo is PropertyInfo)
			{
				this.readOnly = !((PropertyInfo)memberInfo).CanWrite;
				this.isProperty = true;
				return;
			}
			if (memberInfo is FieldInfo)
			{
				this.readOnly = ((FieldInfo)memberInfo).IsInitOnly;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0009BBBF File Offset: 0x00099DBF
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0009BBC7 File Offset: 0x00099DC7
		internal Type FieldType
		{
			get
			{
				return this.fieldType;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0009BBCF File Offset: 0x00099DCF
		internal TypeDesc FieldTypeDesc
		{
			get
			{
				return this.fieldTypeDesc;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0009BBD7 File Offset: 0x00099DD7
		internal bool CheckShouldPersist
		{
			get
			{
				return this.checkShouldPersist;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0009BBDF File Offset: 0x00099DDF
		internal SpecifiedAccessor CheckSpecified
		{
			get
			{
				return this.checkSpecified;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0009BBE7 File Offset: 0x00099DE7
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0009BBEF File Offset: 0x00099DEF
		internal MemberInfo CheckSpecifiedMemberInfo
		{
			get
			{
				return this.checkSpecifiedMemberInfo;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0009BBF7 File Offset: 0x00099DF7
		internal MethodInfo CheckShouldPersistMethodInfo
		{
			get
			{
				return this.checkShouldPersistMethodInfo;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0009BBFF File Offset: 0x00099DFF
		internal bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x0009BC07 File Offset: 0x00099E07
		internal bool IsProperty
		{
			get
			{
				return this.isProperty;
			}
		}

		// Token: 0x04001655 RID: 5717
		private SpecifiedAccessor checkSpecified;

		// Token: 0x04001656 RID: 5718
		private MemberInfo memberInfo;

		// Token: 0x04001657 RID: 5719
		private MemberInfo checkSpecifiedMemberInfo;

		// Token: 0x04001658 RID: 5720
		private MethodInfo checkShouldPersistMethodInfo;

		// Token: 0x04001659 RID: 5721
		private bool checkShouldPersist;

		// Token: 0x0400165A RID: 5722
		private bool readOnly;

		// Token: 0x0400165B RID: 5723
		private bool isProperty;

		// Token: 0x0400165C RID: 5724
		private Type fieldType;

		// Token: 0x0400165D RID: 5725
		private string name;

		// Token: 0x0400165E RID: 5726
		private TypeDesc fieldTypeDesc;
	}
}
