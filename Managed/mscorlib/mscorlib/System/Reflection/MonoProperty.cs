using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x02000337 RID: 823
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoProperty : RuntimePropertyInfo
	{
		// Token: 0x0600242F RID: 9263 RVA: 0x00083868 File Offset: 0x00081A68
		private void CachePropertyInfo(PInfo flags)
		{
			if ((this.cached & flags) != flags)
			{
				MonoPropertyInfo.get_property_info(this, ref this.info, flags);
				this.cached |= flags;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x00083890 File Offset: 0x00081A90
		public override PropertyAttributes Attributes
		{
			get
			{
				this.CachePropertyInfo(PInfo.Attributes);
				return this.info.attrs;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x000838A4 File Offset: 0x00081AA4
		public override bool CanRead
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod);
				return this.info.get_method != null;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x000838BE File Offset: 0x00081ABE
		public override bool CanWrite
		{
			get
			{
				this.CachePropertyInfo(PInfo.SetMethod);
				return this.info.set_method != null;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002433 RID: 9267 RVA: 0x000838D8 File Offset: 0x00081AD8
		public override Type PropertyType
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
				if (this.info.get_method != null)
				{
					return this.info.get_method.ReturnType;
				}
				ParameterInfo[] parametersInternal = this.info.set_method.GetParametersInternal();
				return parametersInternal[parametersInternal.Length - 1].ParameterType;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002434 RID: 9268 RVA: 0x0008392B File Offset: 0x00081B2B
		public override Type ReflectedType
		{
			get
			{
				this.CachePropertyInfo(PInfo.ReflectedType);
				return this.info.parent;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002435 RID: 9269 RVA: 0x0008393F File Offset: 0x00081B3F
		public override Type DeclaringType
		{
			get
			{
				this.CachePropertyInfo(PInfo.DeclaringType);
				return this.info.declaring_type;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x00083954 File Offset: 0x00081B54
		public override string Name
		{
			get
			{
				this.CachePropertyInfo(PInfo.Name);
				return this.info.name;
			}
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x0008396C File Offset: 0x00081B6C
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			int num = 0;
			int num2 = 0;
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				num2 = 1;
			}
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				num = 1;
			}
			MethodInfo[] array = new MethodInfo[num + num2];
			int num3 = 0;
			if (num2 != 0)
			{
				array[num3++] = this.info.set_method;
			}
			if (num != 0)
			{
				array[num3++] = this.info.get_method;
			}
			return array;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00083A0E File Offset: 0x00081C0E
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.GetMethod);
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				return this.info.get_method;
			}
			return null;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00083A4C File Offset: 0x00081C4C
		public override ParameterInfo[] GetIndexParameters()
		{
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			ParameterInfo[] array;
			int num;
			if (this.info.get_method != null)
			{
				array = this.info.get_method.GetParametersInternal();
				num = array.Length;
			}
			else
			{
				if (!(this.info.set_method != null))
				{
					return EmptyArray<ParameterInfo>.Value;
				}
				array = this.info.set_method.GetParametersInternal();
				num = array.Length - 1;
			}
			ParameterInfo[] array2 = new ParameterInfo[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = ParameterInfo.New(array[i], this);
			}
			return array2;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00083ADC File Offset: 0x00081CDC
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				return this.info.set_method;
			}
			return null;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00083B1A File Offset: 0x00081D1A
		public override object GetConstantValue()
		{
			return MonoPropertyInfo.get_default_value(this);
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00083B1A File Offset: 0x00081D1A
		public override object GetRawConstantValue()
		{
			return MonoPropertyInfo.get_default_value(this);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00083B22 File Offset: 0x00081D22
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, false);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00083B2C File Offset: 0x00081D2C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, false);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00083B35 File Offset: 0x00081D35
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, false);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00083B3F File Offset: 0x00081D3F
		private static object GetterAdapterFrame<T, R>(MonoProperty.Getter<T, R> getter, object obj)
		{
			return getter((T)((object)obj));
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00083B52 File Offset: 0x00081D52
		private static object StaticGetterAdapterFrame<R>(MonoProperty.StaticGetter<R> getter, object obj)
		{
			return getter();
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00083B60 File Offset: 0x00081D60
		private static MonoProperty.GetterAdapter CreateGetterDelegate(MethodInfo method)
		{
			Type[] array;
			Type type;
			string text;
			if (method.IsStatic)
			{
				array = new Type[] { method.ReturnType };
				type = typeof(MonoProperty.StaticGetter<>);
				text = "StaticGetterAdapterFrame";
			}
			else
			{
				array = new Type[] { method.DeclaringType, method.ReturnType };
				type = typeof(MonoProperty.Getter<, >);
				text = "GetterAdapterFrame";
			}
			object obj = Delegate.CreateDelegate(type.MakeGenericType(array), method);
			MethodInfo methodInfo = typeof(MonoProperty).GetMethod(text, BindingFlags.Static | BindingFlags.NonPublic);
			methodInfo = methodInfo.MakeGenericMethod(array);
			return (MonoProperty.GetterAdapter)Delegate.CreateDelegate(typeof(MonoProperty.GetterAdapter), obj, methodInfo, true);
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00083C08 File Offset: 0x00081E08
		public override object GetValue(object obj, object[] index)
		{
			if (index == null || index.Length == 0)
			{
				if (this.cached_getter == null)
				{
					MethodInfo getMethod = this.GetGetMethod(true);
					if (this.DeclaringType.IsValueType || getMethod.ContainsGenericParameters)
					{
						goto IL_008A;
					}
					if (getMethod == null)
					{
						throw new ArgumentException("Get Method not found for '" + this.Name + "'");
					}
					this.cached_getter = MonoProperty.CreateGetterDelegate(getMethod);
					try
					{
						return this.cached_getter(obj);
					}
					catch (Exception ex)
					{
						throw new TargetInvocationException(ex);
					}
				}
				try
				{
					return this.cached_getter(obj);
				}
				catch (Exception ex2)
				{
					throw new TargetInvocationException(ex2);
				}
			}
			IL_008A:
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x00083CC8 File Offset: 0x00081EC8
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			object obj2 = null;
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("Get Method not found for '" + this.Name + "'");
			}
			try
			{
				if (index == null || index.Length == 0)
				{
					obj2 = getMethod.Invoke(obj, invokeAttr, binder, null, culture);
				}
				else
				{
					obj2 = getMethod.Invoke(obj, invokeAttr, binder, index, culture);
				}
			}
			catch (SecurityException ex)
			{
				throw new TargetInvocationException(ex);
			}
			return obj2;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00083D44 File Offset: 0x00081F44
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo setMethod = this.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new ArgumentException("Set Method not found for '" + this.Name + "'");
			}
			object[] array;
			if (index == null || index.Length == 0)
			{
				array = new object[] { value };
			}
			else
			{
				int num = index.Length;
				array = new object[num + 1];
				index.CopyTo(array, 0);
				array[num] = value;
			}
			setMethod.Invoke(obj, invokeAttr, binder, array, culture);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00083DBC File Offset: 0x00081FBC
		public override Type[] GetOptionalCustomModifiers()
		{
			Type[] typeModifiers = MonoPropertyInfo.GetTypeModifiers(this, true);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00083DDC File Offset: 0x00081FDC
		public override Type[] GetRequiredCustomModifiers()
		{
			Type[] typeModifiers = MonoPropertyInfo.GetTypeModifiers(this, false);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000824A4 File Offset: 0x000806A4
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x0400135F RID: 4959
		internal IntPtr klass;

		// Token: 0x04001360 RID: 4960
		internal IntPtr prop;

		// Token: 0x04001361 RID: 4961
		private MonoPropertyInfo info;

		// Token: 0x04001362 RID: 4962
		private PInfo cached;

		// Token: 0x04001363 RID: 4963
		private MonoProperty.GetterAdapter cached_getter;

		// Token: 0x02000338 RID: 824
		// (Invoke) Token: 0x0600244B RID: 9291
		private delegate object GetterAdapter(object _this);

		// Token: 0x02000339 RID: 825
		// (Invoke) Token: 0x0600244F RID: 9295
		private delegate R Getter<T, R>(T _this);

		// Token: 0x0200033A RID: 826
		// (Invoke) Token: 0x06002453 RID: 9299
		private delegate R StaticGetter<R>();
	}
}
