using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000342 RID: 834
	[StructLayout(LayoutKind.Sequential)]
	internal abstract class SymbolType : TypeInfo
	{
		// Token: 0x060024BB RID: 9403 RVA: 0x0004AF8A File Offset: 0x0004918A
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x000847DC File Offset: 0x000829DC
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000847DC File Offset: 0x000829DC
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x000847F0 File Offset: 0x000829F0
		public override Module Module
		{
			get
			{
				Type type = this.m_baseType;
				while (type is SymbolType)
				{
					type = ((SymbolType)type).m_baseType;
				}
				return type.Module;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x00084820 File Offset: 0x00082A20
		public override Assembly Assembly
		{
			get
			{
				Type type = this.m_baseType;
				while (type is SymbolType)
				{
					type = ((SymbolType)type).m_baseType;
				}
				return type.Assembly;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x000847DC File Offset: 0x000829DC
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x00084850 File Offset: 0x00082A50
		public override string Namespace
		{
			get
			{
				return this.m_baseType.Namespace;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x0008485D File Offset: 0x00082A5D
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000847DC File Offset: 0x000829DC
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000847DC File Offset: 0x000829DC
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000847DC File Offset: 0x000829DC
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000847DC File Offset: 0x000829DC
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000847DC File Offset: 0x000829DC
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000847DC File Offset: 0x000829DC
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000847DC File Offset: 0x000829DC
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000847DC File Offset: 0x000829DC
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000847DC File Offset: 0x000829DC
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000847DC File Offset: 0x000829DC
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000847DC File Offset: 0x000829DC
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000847DC File Offset: 0x000829DC
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000847DC File Offset: 0x000829DC
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000847DC File Offset: 0x000829DC
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000847DC File Offset: 0x000829DC
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000847DC File Offset: 0x000829DC
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000847DC File Offset: 0x000829DC
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000847DC File Offset: 0x000829DC
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0008486C File Offset: 0x00082A6C
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			Type type = this.m_baseType;
			while (type is SymbolType)
			{
				type = ((SymbolType)type).m_baseType;
			}
			return type.Attributes;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x0008489C File Offset: 0x00082A9C
		public override Type GetElementType()
		{
			return this.m_baseType;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000848A4 File Offset: 0x00082AA4
		protected override bool HasElementTypeImpl()
		{
			return this.m_baseType != null;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000847DC File Offset: 0x000829DC
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000847DC File Offset: 0x000829DC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000847DC File Offset: 0x000829DC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000848B2 File Offset: 0x00082AB2
		internal SymbolType(Type elementType)
		{
			this.m_baseType = elementType;
		}

		// Token: 0x060024E0 RID: 9440
		internal abstract string FormatName(string elementName);

		// Token: 0x060024E1 RID: 9441 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000848C1 File Offset: 0x00082AC1
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000848CA File Offset: 0x00082ACA
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000848DD File Offset: 0x00082ADD
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000848E5 File Offset: 0x00082AE5
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000848ED File Offset: 0x00082AED
		public override string ToString()
		{
			return this.FormatName(this.m_baseType.ToString());
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x00084900 File Offset: 0x00082B00
		public override string AssemblyQualifiedName
		{
			get
			{
				string text = this.FormatName(this.m_baseType.FullName);
				if (text == null)
				{
					return null;
				}
				return text + ", " + this.m_baseType.Assembly.FullName;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x0008493F File Offset: 0x00082B3F
		public override string FullName
		{
			get
			{
				return this.FormatName(this.m_baseType.FullName);
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x00084952 File Offset: 0x00082B52
		public override string Name
		{
			get
			{
				return this.FormatName(this.m_baseType.Name);
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x00002119 File Offset: 0x00000319
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x00084965 File Offset: 0x00082B65
		internal override bool IsUserType
		{
			get
			{
				return this.m_baseType.IsUserType;
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00084972 File Offset: 0x00082B72
		internal override Type RuntimeResolve()
		{
			return this.InternalResolve();
		}

		// Token: 0x04001382 RID: 4994
		internal Type m_baseType;
	}
}
