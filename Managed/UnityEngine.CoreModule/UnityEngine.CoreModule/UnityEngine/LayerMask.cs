using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AB RID: 427
	[NativeClass("BitField", "struct BitField;")]
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	[NativeHeader("Runtime/BaseClasses/BitField.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct LayerMask
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x0001FE6C File Offset: 0x0001E06C
		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0001FE84 File Offset: 0x0001E084
		public static implicit operator LayerMask(int intVal)
		{
			LayerMask layerMask;
			layerMask.m_Mask = intVal;
			return layerMask;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0001FEA0 File Offset: 0x0001E0A0
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		public int value
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x0600139F RID: 5023
		[NativeMethod("LayerToString")]
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		public static extern string LayerToName(int layer);

		// Token: 0x060013A0 RID: 5024
		[NativeMethod("StringToLayer")]
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		public static extern int NameToLayer(string layerName);

		// Token: 0x060013A1 RID: 5025 RVA: 0x0001FEC4 File Offset: 0x0001E0C4
		public static int GetMask(params string[] layerNames)
		{
			bool flag = layerNames == null;
			if (flag)
			{
				throw new ArgumentNullException("layerNames");
			}
			int num = 0;
			foreach (string text in layerNames)
			{
				int num2 = LayerMask.NameToLayer(text);
				bool flag2 = num2 != -1;
				if (flag2)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}

		// Token: 0x0400064C RID: 1612
		[NativeName("m_Bits")]
		private int m_Mask;
	}
}
