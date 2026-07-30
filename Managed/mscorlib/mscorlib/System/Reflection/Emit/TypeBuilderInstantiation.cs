using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000381 RID: 897
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class TypeBuilderInstantiation : TypeInfo
	{
		// Token: 0x06002964 RID: 10596 RVA: 0x00093875 File Offset: 0x00091A75
		internal TypeBuilderInstantiation()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x00093882 File Offset: 0x00091A82
		internal TypeBuilderInstantiation(Type tb, Type[] args)
		{
			this.generic_type = tb;
			this.type_arguments = args;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x00093898 File Offset: 0x00091A98
		internal override Type InternalResolve()
		{
			Type type = this.generic_type.InternalResolve();
			Type[] array = new Type[this.type_arguments.Length];
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				array[i] = this.type_arguments[i].InternalResolve();
			}
			return type.MakeGenericType(array);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000938EC File Offset: 0x00091AEC
		internal override Type RuntimeResolve()
		{
			if (this.generic_type is TypeBuilder && !(this.generic_type as TypeBuilder).IsCreated())
			{
				AppDomain.CurrentDomain.DoTypeResolve(this.generic_type);
			}
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				Type type = this.type_arguments[i];
				if (type is TypeBuilder && !(type as TypeBuilder).IsCreated())
				{
					AppDomain.CurrentDomain.DoTypeResolve(type);
				}
			}
			return this.InternalResolve();
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x0009396C File Offset: 0x00091B6C
		internal bool IsCreated
		{
			get
			{
				TypeBuilder typeBuilder = this.generic_type as TypeBuilder;
				return !(typeBuilder != null) || typeBuilder.is_created;
			}
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x00093996 File Offset: 0x00091B96
		private Type GetParentType()
		{
			return this.InflateType(this.generic_type.BaseType);
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000939A9 File Offset: 0x00091BA9
		internal Type InflateType(Type type)
		{
			return TypeBuilderInstantiation.InflateType(type, this.type_arguments, null);
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000939B8 File Offset: 0x00091BB8
		internal Type InflateType(Type type, Type[] method_args)
		{
			return TypeBuilderInstantiation.InflateType(type, this.type_arguments, method_args);
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000939C8 File Offset: 0x00091BC8
		internal static Type InflateType(Type type, Type[] type_args, Type[] method_args)
		{
			if (type == null)
			{
				return null;
			}
			if (!type.IsGenericParameter && !type.ContainsGenericParameters)
			{
				return type;
			}
			if (type.IsGenericParameter)
			{
				if (type.DeclaringMethod == null)
				{
					if (type_args != null)
					{
						return type_args[type.GenericParameterPosition];
					}
					return type;
				}
				else
				{
					if (method_args != null)
					{
						return method_args[type.GenericParameterPosition];
					}
					return type;
				}
			}
			else
			{
				if (type.IsPointer)
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakePointerType();
				}
				if (type.IsByRef)
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeByRefType();
				}
				if (!type.IsArray)
				{
					Type[] genericArguments = type.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						genericArguments[i] = TypeBuilderInstantiation.InflateType(genericArguments[i], type_args, method_args);
					}
					return (type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition()).MakeGenericType(genericArguments);
				}
				if (type.GetArrayRank() > 1)
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeArrayType(type.GetArrayRank());
				}
				if (type.ToString().EndsWith("[*]", StringComparison.Ordinal))
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeArrayType(1);
				}
				return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeArrayType();
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x00093AF7 File Offset: 0x00091CF7
		public override Type BaseType
		{
			get
			{
				return this.generic_type.BaseType;
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00093B04 File Offset: 0x00091D04
		protected override bool IsValueTypeImpl()
		{
			return this.generic_type.IsValueType;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x00093B14 File Offset: 0x00091D14
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			if (this.methods == null)
			{
				this.methods = new Hashtable();
			}
			if (!this.methods.ContainsKey(fromNoninstanciated))
			{
				this.methods[fromNoninstanciated] = new MethodOnTypeBuilderInst(this, fromNoninstanciated);
			}
			return (MethodInfo)this.methods[fromNoninstanciated];
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x00093B68 File Offset: 0x00091D68
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			if (this.ctors == null)
			{
				this.ctors = new Hashtable();
			}
			if (!this.ctors.ContainsKey(fromNoninstanciated))
			{
				this.ctors[fromNoninstanciated] = new ConstructorOnTypeBuilderInst(this, fromNoninstanciated);
			}
			return (ConstructorInfo)this.ctors[fromNoninstanciated];
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x00093BBC File Offset: 0x00091DBC
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			if (this.fields == null)
			{
				this.fields = new Hashtable();
			}
			if (!this.fields.ContainsKey(fromNoninstanciated))
			{
				this.fields[fromNoninstanciated] = new FieldOnTypeBuilderInst(this, fromNoninstanciated);
			}
			return (FieldInfo)this.fields[fromNoninstanciated];
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override MethodInfo[] GetMethods(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override ConstructorInfo[] GetConstructors(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override FieldInfo[] GetFields(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override PropertyInfo[] GetProperties(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override EventInfo[] GetEvents(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override Type[] GetNestedTypes(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x00002119 File Offset: 0x00000319
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x00093C0E File Offset: 0x00091E0E
		public override Assembly Assembly
		{
			get
			{
				return this.generic_type.Assembly;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x00093C1B File Offset: 0x00091E1B
		public override Module Module
		{
			get
			{
				return this.generic_type.Module;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x00093C28 File Offset: 0x00091E28
		public override string Name
		{
			get
			{
				return this.generic_type.Name;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x00093C35 File Offset: 0x00091E35
		public override string Namespace
		{
			get
			{
				return this.generic_type.Namespace;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x00093C42 File Offset: 0x00091E42
		public override string FullName
		{
			get
			{
				return this.format_name(true, false);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x00093C4C File Offset: 0x00091E4C
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.format_name(true, true);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x00093C58 File Offset: 0x00091E58
		private string format_name(bool full_name, bool assembly_qualified)
		{
			StringBuilder stringBuilder = new StringBuilder(this.generic_type.FullName);
			stringBuilder.Append("[");
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				string text;
				if (full_name)
				{
					string fullName = this.type_arguments[i].Assembly.FullName;
					text = this.type_arguments[i].FullName;
					if (text != null && fullName != null)
					{
						text = text + ", " + fullName;
					}
				}
				else
				{
					text = this.type_arguments[i].ToString();
				}
				if (text == null)
				{
					return null;
				}
				if (full_name)
				{
					stringBuilder.Append("[");
				}
				stringBuilder.Append(text);
				if (full_name)
				{
					stringBuilder.Append("]");
				}
			}
			stringBuilder.Append("]");
			if (assembly_qualified)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.generic_type.Assembly.FullName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x00093D55 File Offset: 0x00091F55
		public override string ToString()
		{
			return this.format_name(false, false);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x00093D5F File Offset: 0x00091F5F
		public override Type GetGenericTypeDefinition()
		{
			return this.generic_type;
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x00093D68 File Offset: 0x00091F68
		public override Type[] GetGenericArguments()
		{
			Type[] array = new Type[this.type_arguments.Length];
			this.type_arguments.CopyTo(array, 0);
			return array;
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x00093D94 File Offset: 0x00091F94
		public override bool ContainsGenericParameters
		{
			get
			{
				Type[] array = this.type_arguments;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x00003B29 File Offset: 0x00001D29
		public override bool IsGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x00093DC3 File Offset: 0x00091FC3
		public override Type DeclaringType
		{
			get
			{
				return this.generic_type.DeclaringType;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000848C1 File Offset: 0x00082AC1
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000848CA File Offset: 0x00082ACA
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000848DD File Offset: 0x00082ADD
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000848E5 File Offset: 0x00082AE5
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x00093DD0 File Offset: 0x00091FD0
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.generic_type.Attributes;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x00014B5A File Offset: 0x00012D5A
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00014B5A File Offset: 0x00012D5A
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x00014B5A File Offset: 0x00012D5A
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x00093DDD File Offset: 0x00091FDD
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.IsCreated)
			{
				return this.generic_type.GetCustomAttributes(inherit);
			}
			throw new NotSupportedException();
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x00093DF9 File Offset: 0x00091FF9
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.IsCreated)
			{
				return this.generic_type.GetCustomAttributes(attributeType, inherit);
			}
			throw new NotSupportedException();
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x00093E18 File Offset: 0x00092018
		internal override bool IsUserType
		{
			get
			{
				Type[] array = this.type_arguments;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IsUserType)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00093E47 File Offset: 0x00092047
		internal static Type MakeGenericType(Type type, Type[] typeArguments)
		{
			return new TypeBuilderInstantiation(type, typeArguments);
		}

		// Token: 0x0400161D RID: 5661
		internal Type generic_type;

		// Token: 0x0400161E RID: 5662
		private Type[] type_arguments;

		// Token: 0x0400161F RID: 5663
		private Hashtable fields;

		// Token: 0x04001620 RID: 5664
		private Hashtable ctors;

		// Token: 0x04001621 RID: 5665
		private Hashtable methods;

		// Token: 0x04001622 RID: 5666
		private const BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
