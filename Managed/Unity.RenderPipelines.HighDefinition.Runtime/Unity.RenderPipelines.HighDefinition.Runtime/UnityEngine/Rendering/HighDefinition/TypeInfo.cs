using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000181 RID: 385
	internal static class TypeInfo
	{
		// Token: 0x06000AF5 RID: 2805 RVA: 0x0005474D File Offset: 0x0005294D
		public static TEnum[] GetEnumValues<TEnum>() where TEnum : struct, IConvertible
		{
			return TypeInfo.EnumInfoJITCache<TEnum>.values;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00054754 File Offset: 0x00052954
		public static int GetEnumLength<TEnum>() where TEnum : struct, IConvertible
		{
			return TypeInfo.EnumInfoJITCache<TEnum>.length;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0005475B File Offset: 0x0005295B
		public static string[] GetEnumNames<TEnum>() where TEnum : struct, IConvertible
		{
			return TypeInfo.EnumInfoJITCache<TEnum>.names;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00054762 File Offset: 0x00052962
		public static TEnum GetEnumLastValue<TEnum>() where TEnum : struct, IConvertible
		{
			return TypeInfo.GetEnumValues<TEnum>()[TypeInfo.GetEnumLength<TEnum>() - 1];
		}

		// Token: 0x020002A3 RID: 675
		private struct EnumInfoJITCache<TEnum> where TEnum : struct, IConvertible
		{
			// Token: 0x06000CDD RID: 3293 RVA: 0x0005A68C File Offset: 0x0005888C
			static EnumInfoJITCache()
			{
				if (!typeof(TEnum).IsEnum)
				{
					throw new InvalidOperationException(string.Format("{0} must be an enum type.", typeof(TEnum)));
				}
				TypeInfo.EnumInfoJITCache<TEnum>.names = Enum.GetNames(typeof(TEnum));
				TypeInfo.EnumInfoJITCache<TEnum>.length = TypeInfo.EnumInfoJITCache<TEnum>.names.Length;
				TypeInfo.EnumInfoJITCache<TEnum>.values = new TEnum[TypeInfo.EnumInfoJITCache<TEnum>.length];
				Array array = Enum.GetValues(typeof(TEnum));
				for (int i = 0; i < TypeInfo.EnumInfoJITCache<TEnum>.values.Length; i++)
				{
					TypeInfo.EnumInfoJITCache<TEnum>.values[i] = (TEnum)((object)array.GetValue(i));
				}
			}

			// Token: 0x0400172B RID: 5931
			public static readonly TEnum[] values;

			// Token: 0x0400172C RID: 5932
			public static readonly string[] names;

			// Token: 0x0400172D RID: 5933
			public static readonly int length;
		}
	}
}
