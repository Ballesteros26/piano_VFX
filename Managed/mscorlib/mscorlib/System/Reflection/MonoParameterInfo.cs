using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000331 RID: 817
	[ComDefaultInterface(typeof(_ParameterInfo))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoParameterInfo : RuntimeParameterInfo
	{
		// Token: 0x0600240C RID: 9228 RVA: 0x000834A0 File Offset: 0x000816A0
		internal MonoParameterInfo(ParameterBuilder pb, Type type, MemberInfo member, int position)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			if (pb != null)
			{
				this.NameImpl = pb.Name;
				this.PositionImpl = pb.Position - 1;
				this.AttrsImpl = (ParameterAttributes)pb.Attributes;
				return;
			}
			this.NameImpl = null;
			this.PositionImpl = position - 1;
			this.AttrsImpl = ParameterAttributes.None;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00083504 File Offset: 0x00081704
		internal MonoParameterInfo(ParameterInfo pinfo, Type type, MemberInfo member, int position)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			if (pinfo != null)
			{
				this.NameImpl = pinfo.Name;
				this.PositionImpl = pinfo.Position - 1;
				this.AttrsImpl = pinfo.Attributes;
				return;
			}
			this.NameImpl = null;
			this.PositionImpl = position - 1;
			this.AttrsImpl = ParameterAttributes.None;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00083568 File Offset: 0x00081768
		internal MonoParameterInfo(ParameterInfo pinfo, MemberInfo member)
		{
			this.ClassImpl = pinfo.ParameterType;
			this.MemberImpl = member;
			this.NameImpl = pinfo.Name;
			this.PositionImpl = pinfo.Position;
			this.AttrsImpl = pinfo.Attributes;
			this.DefaultValueImpl = pinfo.GetDefaultValueImpl();
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000835BE File Offset: 0x000817BE
		internal MonoParameterInfo(Type type, MemberInfo member, MarshalAsAttribute marshalAs)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			this.NameImpl = "";
			this.PositionImpl = -1;
			this.AttrsImpl = ParameterAttributes.Retval;
			this.marshalAs = marshalAs;
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x000835F4 File Offset: 0x000817F4
		public override object DefaultValue
		{
			get
			{
				if (this.ClassImpl == typeof(decimal))
				{
					DecimalConstantAttribute[] array = (DecimalConstantAttribute[])this.GetCustomAttributes(typeof(DecimalConstantAttribute), false);
					if (array.Length != 0)
					{
						return array[0].Value;
					}
				}
				else if (this.ClassImpl == typeof(DateTime))
				{
					DateTimeConstantAttribute[] array2 = (DateTimeConstantAttribute[])this.GetCustomAttributes(typeof(DateTimeConstantAttribute), false);
					if (array2.Length != 0)
					{
						return array2[0].Value;
					}
				}
				return this.DefaultValueImpl;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x00083682 File Offset: 0x00081882
		public override object RawDefaultValue
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x0008368C File Offset: 0x0008188C
		public override int MetadataToken
		{
			get
			{
				if (this.MemberImpl is PropertyInfo)
				{
					PropertyInfo propertyInfo = (PropertyInfo)this.MemberImpl;
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = propertyInfo.GetSetMethod(true);
					}
					return methodInfo.GetParametersInternal()[this.PositionImpl].MetadataToken;
				}
				if (this.MemberImpl is MethodBase)
				{
					return base.GetMetadataToken();
				}
				throw new ArgumentException("Can't produce MetadataToken for member of type " + this.MemberImpl.GetType());
			}
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0007F7D9 File Offset: 0x0007D9D9
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x0007F7E2 File Offset: 0x0007D9E2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000330F9 File Offset: 0x000312F9
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x0008370C File Offset: 0x0008190C
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x00083714 File Offset: 0x00081914
		public override Type[] GetOptionalCustomModifiers()
		{
			Type[] typeModifiers = base.GetTypeModifiers(true);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x00083734 File Offset: 0x00081934
		public override Type[] GetRequiredCustomModifiers()
		{
			Type[] typeModifiers = base.GetTypeModifiers(false);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x00083754 File Offset: 0x00081954
		public override bool HasDefaultValue
		{
			get
			{
				object defaultValue = this.DefaultValue;
				return defaultValue == null || (!(defaultValue.GetType() == typeof(DBNull)) && !(defaultValue.GetType() == typeof(Missing)));
			}
		}
	}
}
