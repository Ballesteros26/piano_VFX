using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200038B RID: 907
	[UsedByNativeCode]
	public struct ShaderKeywordSet
	{
		// Token: 0x06001FB7 RID: 8119 RVA: 0x00036168 File Offset: 0x00034368
		private void ComputeSliceAndMask(ShaderKeyword keyword, out uint slice, out uint mask)
		{
			int index = keyword.index;
			slice = (uint)(index / 32);
			mask = 1U << index % 32;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00036190 File Offset: 0x00034390
		public unsafe bool IsEnabled(ShaderKeyword keyword)
		{
			bool flag = !keyword.IsValid();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				uint num;
				uint num2;
				this.ComputeSliceAndMask(keyword, out num, out num2);
				fixed (uint* ptr = &this.m_Bits.FixedElementField)
				{
					uint* ptr2 = ptr;
					flag2 = (ptr2[(ulong)num * 4UL / 4UL] & num2) > 0U;
				}
			}
			return flag2;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000361E4 File Offset: 0x000343E4
		public unsafe void Enable(ShaderKeyword keyword)
		{
			bool flag = !keyword.IsValid();
			if (!flag)
			{
				uint num;
				uint num2;
				this.ComputeSliceAndMask(keyword, out num, out num2);
				fixed (uint* ptr = &this.m_Bits.FixedElementField)
				{
					uint* ptr2 = ptr;
					ptr2[(ulong)num * 4UL / 4UL] |= num2;
				}
			}
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00036234 File Offset: 0x00034434
		public unsafe void Disable(ShaderKeyword keyword)
		{
			bool flag = !keyword.IsValid();
			if (!flag)
			{
				uint num;
				uint num2;
				this.ComputeSliceAndMask(keyword, out num, out num2);
				fixed (uint* ptr = &this.m_Bits.FixedElementField)
				{
					uint* ptr2 = ptr;
					ptr2[(ulong)num * 4UL / 4UL] &= ~num2;
				}
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00036284 File Offset: 0x00034484
		public ShaderKeyword[] GetShaderKeywords()
		{
			ShaderKeyword[] array = new ShaderKeyword[320];
			int num = 0;
			for (int i = 0; i < 320; i++)
			{
				ShaderKeyword shaderKeyword = new ShaderKeyword(i);
				bool flag = this.IsEnabled(shaderKeyword);
				if (flag)
				{
					array[num] = shaderKeyword;
					num++;
				}
			}
			Array.Resize<ShaderKeyword>(ref array, num);
			return array;
		}

		// Token: 0x04000B61 RID: 2913
		private const int k_SizeInBits = 32;

		// Token: 0x04000B62 RID: 2914
		[FixedBuffer(typeof(uint), 10)]
		internal ShaderKeywordSet.<m_Bits>e__FixedBuffer m_Bits;

		// Token: 0x0200038C RID: 908
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 40)]
		public struct <m_Bits>e__FixedBuffer
		{
			// Token: 0x04000B63 RID: 2915
			public uint FixedElementField;
		}
	}
}
