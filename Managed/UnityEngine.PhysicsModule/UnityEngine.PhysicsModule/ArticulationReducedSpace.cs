using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public struct ArticulationReducedSpace
	{
		// Token: 0x170000CF RID: 207
		public unsafe float this[int i]
		{
			get
			{
				bool flag = i < 0 || i >= this.dofCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				return *((ref this.x.FixedElementField) + (IntPtr)i * 4);
			}
			set
			{
				bool flag = i < 0 || i >= this.dofCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				*((ref this.x.FixedElementField) + (IntPtr)i * 4) = value;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00003EC1 File Offset: 0x000020C1
		public ArticulationReducedSpace(float a)
		{
			this.x.FixedElementField = a;
			this.dofCount = 1;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00003ED8 File Offset: 0x000020D8
		public unsafe ArticulationReducedSpace(float a, float b)
		{
			this.x.FixedElementField = a;
			*((ref this.x.FixedElementField) + 4) = b;
			this.dofCount = 2;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00003EFE File Offset: 0x000020FE
		public unsafe ArticulationReducedSpace(float a, float b, float c)
		{
			this.x.FixedElementField = a;
			*((ref this.x.FixedElementField) + 4) = b;
			*((ref this.x.FixedElementField) + (IntPtr)2 * 4) = c;
			this.dofCount = 3;
		}

		// Token: 0x04000089 RID: 137
		[FixedBuffer(typeof(float), 3)]
		private ArticulationReducedSpace.<x>e__FixedBuffer x;

		// Token: 0x0400008A RID: 138
		public int dofCount;

		// Token: 0x0200002E RID: 46
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(0, Size = 12)]
		public struct <x>e__FixedBuffer
		{
			// Token: 0x0400008B RID: 139
			public float FixedElementField;
		}
	}
}
