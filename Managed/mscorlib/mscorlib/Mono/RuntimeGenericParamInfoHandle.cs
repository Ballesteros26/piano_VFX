using System;
using System.Reflection;

namespace Mono
{
	// Token: 0x02000015 RID: 21
	internal struct RuntimeGenericParamInfoHandle
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003C85 File Offset: 0x00001E85
		internal unsafe RuntimeGenericParamInfoHandle(RuntimeStructs.GenericParamInfo* value)
		{
			this.value = value;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003C8E File Offset: 0x00001E8E
		internal unsafe RuntimeGenericParamInfoHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.GenericParamInfo*)(void*)ptr;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003C9C File Offset: 0x00001E9C
		internal Type[] Constraints
		{
			get
			{
				return this.GetConstraints();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003CA4 File Offset: 0x00001EA4
		internal unsafe GenericParameterAttributes Attributes
		{
			get
			{
				return (GenericParameterAttributes)this.value->flags;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003CB4 File Offset: 0x00001EB4
		private unsafe Type[] GetConstraints()
		{
			int constraintsCount = this.GetConstraintsCount();
			Type[] array = new Type[constraintsCount];
			for (int i = 0; i < constraintsCount; i++)
			{
				RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(*(IntPtr*)(this.value->constraints + (IntPtr)i * (IntPtr)sizeof(RuntimeStructs.MonoClass*) / (IntPtr)sizeof(RuntimeStructs.MonoClass*)));
				array[i] = Type.GetTypeFromHandle(runtimeClassHandle.GetTypeHandle());
			}
			return array;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003D08 File Offset: 0x00001F08
		private unsafe int GetConstraintsCount()
		{
			int num = 0;
			RuntimeStructs.MonoClass** ptr = this.value->constraints;
			while (ptr != null && *(IntPtr*)ptr != (IntPtr)((UIntPtr)0))
			{
				ptr += sizeof(RuntimeStructs.MonoClass*) / sizeof(RuntimeStructs.MonoClass*);
				num++;
			}
			return num;
		}

		// Token: 0x04000380 RID: 896
		private unsafe RuntimeStructs.GenericParamInfo* value;
	}
}
