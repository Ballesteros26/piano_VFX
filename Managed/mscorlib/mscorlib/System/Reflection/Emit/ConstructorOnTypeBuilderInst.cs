using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200034B RID: 843
	[StructLayout(LayoutKind.Sequential)]
	internal class ConstructorOnTypeBuilderInst : ConstructorInfo
	{
		// Token: 0x06002576 RID: 9590 RVA: 0x000868CE File Offset: 0x00084ACE
		public ConstructorOnTypeBuilderInst(TypeBuilderInstantiation instantiation, ConstructorInfo cb)
		{
			this.instantiation = instantiation;
			this.cb = cb;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x000868E4 File Offset: 0x00084AE4
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x000868EC File Offset: 0x00084AEC
		public override string Name
		{
			get
			{
				return this.cb.Name;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x000868E4 File Offset: 0x00084AE4
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x000868F9 File Offset: 0x00084AF9
		public override Module Module
		{
			get
			{
				return this.cb.Module;
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00086906 File Offset: 0x00084B06
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.cb.IsDefined(attributeType, inherit);
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x00086915 File Offset: 0x00084B15
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.cb.GetCustomAttributes(inherit);
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x00086923 File Offset: 0x00084B23
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.cb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00086932 File Offset: 0x00084B32
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.cb.GetMethodImplementationFlags();
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x0008693F File Offset: 0x00084B3F
		public override ParameterInfo[] GetParameters()
		{
			if (!this.instantiation.IsCreated)
			{
				throw new NotSupportedException();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x0008695C File Offset: 0x00084B5C
		internal override ParameterInfo[] GetParametersInternal()
		{
			ParameterInfo[] array;
			if (this.cb is ConstructorBuilder)
			{
				ConstructorBuilder constructorBuilder = (ConstructorBuilder)this.cb;
				array = new ParameterInfo[constructorBuilder.parameters.Length];
				for (int i = 0; i < constructorBuilder.parameters.Length; i++)
				{
					Type type = this.instantiation.InflateType(constructorBuilder.parameters[i]);
					array[i] = ParameterInfo.New((constructorBuilder.pinfo == null) ? null : constructorBuilder.pinfo[i], type, this, i + 1);
				}
			}
			else
			{
				ParameterInfo[] parameters = this.cb.GetParameters();
				array = new ParameterInfo[parameters.Length];
				for (int j = 0; j < parameters.Length; j++)
				{
					Type type2 = this.instantiation.InflateType(parameters[j].ParameterType);
					array[j] = ParameterInfo.New(parameters[j], type2, this, j + 1);
				}
			}
			return array;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00086A30 File Offset: 0x00084C30
		internal override Type[] GetParameterTypes()
		{
			if (this.cb is ConstructorBuilder)
			{
				return (this.cb as ConstructorBuilder).parameters;
			}
			ParameterInfo[] parameters = this.cb.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00086A8A File Offset: 0x00084C8A
		internal ConstructorInfo RuntimeResolve()
		{
			return this.instantiation.InternalResolve().GetConstructor(this.cb);
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x00086AA2 File Offset: 0x00084CA2
		public override int MetadataToken
		{
			get
			{
				return base.MetadataToken;
			}
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00086AAA File Offset: 0x00084CAA
		internal override int GetParametersCount()
		{
			return this.cb.GetParametersCount();
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x00086AB7 File Offset: 0x00084CB7
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.cb.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x00086ACB File Offset: 0x00084CCB
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.cb.MethodHandle;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x00086AD8 File Offset: 0x00084CD8
		public override MethodAttributes Attributes
		{
			get
			{
				return this.cb.Attributes;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x00086AE5 File Offset: 0x00084CE5
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.cb.CallingConvention;
			}
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00086AF2 File Offset: 0x00084CF2
		public override Type[] GetGenericArguments()
		{
			return this.cb.GetGenericArguments();
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x0007EA26 File Offset: 0x0007CC26
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040013D1 RID: 5073
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x040013D2 RID: 5074
		internal ConstructorInfo cb;
	}
}
