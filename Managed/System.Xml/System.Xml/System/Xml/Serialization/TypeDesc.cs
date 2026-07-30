using System;
using System.Xml.Schema;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization
{
	// Token: 0x0200031D RID: 797
	internal class TypeDesc
	{
		// Token: 0x06001DD4 RID: 7636 RVA: 0x000A44E4 File Offset: 0x000A26E4
		internal TypeDesc(string name, string fullName, XmlSchemaType dataType, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags, string formatterName)
		{
			this.name = name.Replace('+', '.');
			this.fullName = fullName.Replace('+', '.');
			this.kind = kind;
			this.baseTypeDesc = baseTypeDesc;
			this.flags = flags;
			this.isXsdType = kind == TypeKind.Primitive;
			if (this.isXsdType)
			{
				this.weight = 1;
			}
			else if (kind == TypeKind.Enum)
			{
				this.weight = 2;
			}
			else if (this.kind == TypeKind.Root)
			{
				this.weight = -1;
			}
			else
			{
				this.weight = ((baseTypeDesc == null) ? 0 : (baseTypeDesc.Weight + 1));
			}
			this.dataType = dataType;
			this.formatterName = formatterName;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x000A458F File Offset: 0x000A278F
		internal TypeDesc(string name, string fullName, XmlSchemaType dataType, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags)
			: this(name, fullName, dataType, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000A45A1 File Offset: 0x000A27A1
		internal TypeDesc(string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags)
			: this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000A45B2 File Offset: 0x000A27B2
		internal TypeDesc(Type type, bool isXsdType, XmlSchemaType dataType, string formatterName, TypeFlags flags)
			: this(type.Name, type.FullName, dataType, TypeKind.Primitive, null, flags, formatterName)
		{
			this.isXsdType = isXsdType;
			this.type = type;
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x000A45DB File Offset: 0x000A27DB
		internal TypeDesc(Type type, string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags, TypeDesc arrayElementTypeDesc)
			: this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
			this.arrayElementTypeDesc = arrayElementTypeDesc;
			this.type = type;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000A45FC File Offset: 0x000A27FC
		public override string ToString()
		{
			return this.fullName;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x000A4604 File Offset: 0x000A2804
		internal TypeFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000A460C File Offset: 0x000A280C
		internal bool IsXsdType
		{
			get
			{
				return this.isXsdType;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x000A4614 File Offset: 0x000A2814
		internal bool IsMappedType
		{
			get
			{
				return this.extendedType != null;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000A461F File Offset: 0x000A281F
		internal MappedTypeDesc ExtendedType
		{
			get
			{
				return this.extendedType;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x000A4627 File Offset: 0x000A2827
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x000A45FC File Offset: 0x000A27FC
		internal string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x000A462F File Offset: 0x000A282F
		internal string CSharpName
		{
			get
			{
				if (this.cSharpName == null)
				{
					this.cSharpName = ((this.type == null) ? CodeIdentifier.GetCSharpName(this.fullName) : CodeIdentifier.GetCSharpName(this.type));
				}
				return this.cSharpName;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000A466B File Offset: 0x000A286B
		internal XmlSchemaType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x000A4673 File Offset: 0x000A2873
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000A467B File Offset: 0x000A287B
		internal string FormatterName
		{
			get
			{
				return this.formatterName;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x000A4683 File Offset: 0x000A2883
		internal TypeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x000A468B File Offset: 0x000A288B
		internal bool IsValueType
		{
			get
			{
				return (this.flags & TypeFlags.Reference) == TypeFlags.None;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x000A4698 File Offset: 0x000A2898
		internal bool CanBeAttributeValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeAttributeValue) > TypeFlags.None;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x000A46A5 File Offset: 0x000A28A5
		internal bool XmlEncodingNotRequired
		{
			get
			{
				return (this.flags & TypeFlags.XmlEncodingNotRequired) > TypeFlags.None;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x000A46B6 File Offset: 0x000A28B6
		internal bool CanBeElementValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeElementValue) > TypeFlags.None;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x000A46C4 File Offset: 0x000A28C4
		internal bool CanBeTextValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeTextValue) > TypeFlags.None;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x000A46D2 File Offset: 0x000A28D2
		// (set) Token: 0x06001DEB RID: 7659 RVA: 0x000A46E4 File Offset: 0x000A28E4
		internal bool IsMixed
		{
			get
			{
				return this.isMixed || this.CanBeTextValue;
			}
			set
			{
				this.isMixed = value;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x000A46ED File Offset: 0x000A28ED
		internal bool IsSpecial
		{
			get
			{
				return (this.flags & TypeFlags.Special) > TypeFlags.None;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x000A46FA File Offset: 0x000A28FA
		internal bool IsAmbiguousDataType
		{
			get
			{
				return (this.flags & TypeFlags.AmbiguousDataType) > TypeFlags.None;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x000A470B File Offset: 0x000A290B
		internal bool HasCustomFormatter
		{
			get
			{
				return (this.flags & TypeFlags.HasCustomFormatter) > TypeFlags.None;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x000A4719 File Offset: 0x000A2919
		internal bool HasDefaultSupport
		{
			get
			{
				return (this.flags & TypeFlags.IgnoreDefault) == TypeFlags.None;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x000A472A File Offset: 0x000A292A
		internal bool HasIsEmpty
		{
			get
			{
				return (this.flags & TypeFlags.HasIsEmpty) > TypeFlags.None;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x000A473B File Offset: 0x000A293B
		internal bool CollapseWhitespace
		{
			get
			{
				return (this.flags & TypeFlags.CollapseWhitespace) > TypeFlags.None;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x000A474C File Offset: 0x000A294C
		internal bool HasDefaultConstructor
		{
			get
			{
				return (this.flags & TypeFlags.HasDefaultConstructor) > TypeFlags.None;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000A475D File Offset: 0x000A295D
		internal bool IsUnsupported
		{
			get
			{
				return (this.flags & TypeFlags.Unsupported) > TypeFlags.None;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x000A476E File Offset: 0x000A296E
		internal bool IsGenericInterface
		{
			get
			{
				return (this.flags & TypeFlags.GenericInterface) > TypeFlags.None;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x000A477F File Offset: 0x000A297F
		internal bool IsPrivateImplementation
		{
			get
			{
				return (this.flags & TypeFlags.UsePrivateImplementation) > TypeFlags.None;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x000A4790 File Offset: 0x000A2990
		internal bool CannotNew
		{
			get
			{
				return !this.HasDefaultConstructor || this.ConstructorInaccessible;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x000A47A2 File Offset: 0x000A29A2
		internal bool IsAbstract
		{
			get
			{
				return (this.flags & TypeFlags.Abstract) > TypeFlags.None;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x000A47AF File Offset: 0x000A29AF
		internal bool IsOptionalValue
		{
			get
			{
				return (this.flags & TypeFlags.OptionalValue) > TypeFlags.None;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x000A47C0 File Offset: 0x000A29C0
		internal bool UseReflection
		{
			get
			{
				return (this.flags & TypeFlags.UseReflection) > TypeFlags.None;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x000A47D1 File Offset: 0x000A29D1
		internal bool IsVoid
		{
			get
			{
				return this.kind == TypeKind.Void;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000A47DC File Offset: 0x000A29DC
		internal bool IsClass
		{
			get
			{
				return this.kind == TypeKind.Class;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x000A47E7 File Offset: 0x000A29E7
		internal bool IsStructLike
		{
			get
			{
				return this.kind == TypeKind.Struct || this.kind == TypeKind.Class;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000A47FD File Offset: 0x000A29FD
		internal bool IsArrayLike
		{
			get
			{
				return this.kind == TypeKind.Array || this.kind == TypeKind.Collection || this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x000A481C File Offset: 0x000A2A1C
		internal bool IsCollection
		{
			get
			{
				return this.kind == TypeKind.Collection;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x000A4827 File Offset: 0x000A2A27
		internal bool IsEnumerable
		{
			get
			{
				return this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x000A4832 File Offset: 0x000A2A32
		internal bool IsArray
		{
			get
			{
				return this.kind == TypeKind.Array;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000A483D File Offset: 0x000A2A3D
		internal bool IsPrimitive
		{
			get
			{
				return this.kind == TypeKind.Primitive;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x000A4848 File Offset: 0x000A2A48
		internal bool IsEnum
		{
			get
			{
				return this.kind == TypeKind.Enum;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x000A4853 File Offset: 0x000A2A53
		internal bool IsNullable
		{
			get
			{
				return !this.IsValueType;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x000A485E File Offset: 0x000A2A5E
		internal bool IsRoot
		{
			get
			{
				return this.kind == TypeKind.Root;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x000A4869 File Offset: 0x000A2A69
		internal bool ConstructorInaccessible
		{
			get
			{
				return (this.flags & TypeFlags.CtorInaccessible) > TypeFlags.None;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x000A487A File Offset: 0x000A2A7A
		// (set) Token: 0x06001E07 RID: 7687 RVA: 0x000A4882 File Offset: 0x000A2A82
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000A488C File Offset: 0x000A2A8C
		internal TypeDesc GetNullableTypeDesc(Type type)
		{
			if (this.IsOptionalValue)
			{
				return this;
			}
			if (this.nullableTypeDesc == null)
			{
				this.nullableTypeDesc = new TypeDesc("NullableOf" + this.name, "System.Nullable`1[" + this.fullName + "]", null, TypeKind.Struct, this, this.flags | TypeFlags.OptionalValue, this.formatterName);
				this.nullableTypeDesc.type = type;
			}
			return this.nullableTypeDesc;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000A4904 File Offset: 0x000A2B04
		internal void CheckSupported()
		{
			if (!this.IsUnsupported)
			{
				if (this.baseTypeDesc != null)
				{
					this.baseTypeDesc.CheckSupported();
				}
				if (this.arrayElementTypeDesc != null)
				{
					this.arrayElementTypeDesc.CheckSupported();
				}
				return;
			}
			if (this.Exception != null)
			{
				throw this.Exception;
			}
			throw new NotSupportedException(Res.GetString("{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.", new object[] { this.FullName }));
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000A4970 File Offset: 0x000A2B70
		internal void CheckNeedConstructor()
		{
			if (!this.IsValueType && !this.IsAbstract && !this.HasDefaultConstructor)
			{
				this.flags |= TypeFlags.Unsupported;
				this.exception = new InvalidOperationException(Res.GetString("{0} cannot be serialized because it does not have a parameterless constructor.", new object[] { this.FullName }));
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x000A49CB File Offset: 0x000A2BCB
		internal string ArrayLengthName
		{
			get
			{
				if (this.kind != TypeKind.Array)
				{
					return "Count";
				}
				return "Length";
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x000A49E1 File Offset: 0x000A2BE1
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x000A49E9 File Offset: 0x000A2BE9
		internal TypeDesc ArrayElementTypeDesc
		{
			get
			{
				return this.arrayElementTypeDesc;
			}
			set
			{
				this.arrayElementTypeDesc = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x000A49F2 File Offset: 0x000A2BF2
		internal int Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000A49FC File Offset: 0x000A2BFC
		internal TypeDesc CreateArrayTypeDesc()
		{
			if (this.arrayTypeDesc == null)
			{
				this.arrayTypeDesc = new TypeDesc(null, this.name + "[]", this.fullName + "[]", TypeKind.Array, null, TypeFlags.Reference | (this.flags & TypeFlags.UseReflection), this);
			}
			return this.arrayTypeDesc;
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000A4A54 File Offset: 0x000A2C54
		internal TypeDesc CreateMappedTypeDesc(MappedTypeDesc extension)
		{
			return new TypeDesc(extension.Name, extension.Name, null, this.kind, this.baseTypeDesc, this.flags, null)
			{
				isXsdType = this.isXsdType,
				isMixed = this.isMixed,
				extendedType = extension,
				dataType = this.dataType
			};
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x000A4AB1 File Offset: 0x000A2CB1
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x000A4AB9 File Offset: 0x000A2CB9
		internal TypeDesc BaseTypeDesc
		{
			get
			{
				return this.baseTypeDesc;
			}
			set
			{
				this.baseTypeDesc = value;
				this.weight = ((this.baseTypeDesc == null) ? 0 : (this.baseTypeDesc.Weight + 1));
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000A4AE0 File Offset: 0x000A2CE0
		internal bool IsDerivedFrom(TypeDesc baseTypeDesc)
		{
			for (TypeDesc typeDesc = this; typeDesc != null; typeDesc = typeDesc.BaseTypeDesc)
			{
				if (typeDesc == baseTypeDesc)
				{
					return true;
				}
			}
			return baseTypeDesc.IsRoot;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000A4B08 File Offset: 0x000A2D08
		internal static TypeDesc FindCommonBaseTypeDesc(TypeDesc[] typeDescs)
		{
			if (typeDescs.Length == 0)
			{
				return null;
			}
			TypeDesc typeDesc = null;
			int num = int.MaxValue;
			for (int i = 0; i < typeDescs.Length; i++)
			{
				int num2 = typeDescs[i].Weight;
				if (num2 < num)
				{
					num = num2;
					typeDesc = typeDescs[i];
				}
			}
			while (typeDesc != null)
			{
				int num3 = 0;
				while (num3 < typeDescs.Length && typeDescs[num3].IsDerivedFrom(typeDesc))
				{
					num3++;
				}
				if (num3 == typeDescs.Length)
				{
					break;
				}
				typeDesc = typeDesc.BaseTypeDesc;
			}
			return typeDesc;
		}

		// Token: 0x040016DA RID: 5850
		private string name;

		// Token: 0x040016DB RID: 5851
		private string fullName;

		// Token: 0x040016DC RID: 5852
		private string cSharpName;

		// Token: 0x040016DD RID: 5853
		private TypeDesc arrayElementTypeDesc;

		// Token: 0x040016DE RID: 5854
		private TypeDesc arrayTypeDesc;

		// Token: 0x040016DF RID: 5855
		private TypeDesc nullableTypeDesc;

		// Token: 0x040016E0 RID: 5856
		private TypeKind kind;

		// Token: 0x040016E1 RID: 5857
		private XmlSchemaType dataType;

		// Token: 0x040016E2 RID: 5858
		private Type type;

		// Token: 0x040016E3 RID: 5859
		private TypeDesc baseTypeDesc;

		// Token: 0x040016E4 RID: 5860
		private TypeFlags flags;

		// Token: 0x040016E5 RID: 5861
		private string formatterName;

		// Token: 0x040016E6 RID: 5862
		private bool isXsdType;

		// Token: 0x040016E7 RID: 5863
		private bool isMixed;

		// Token: 0x040016E8 RID: 5864
		private MappedTypeDesc extendedType;

		// Token: 0x040016E9 RID: 5865
		private int weight;

		// Token: 0x040016EA RID: 5866
		private Exception exception;
	}
}
