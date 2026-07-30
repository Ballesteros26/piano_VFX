using System;
using System.Linq;
using System.Reflection;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001CB RID: 459
	internal static class ReflectionUtilities
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x00024E70 File Offset: 0x00023070
		public static TValue[] GetConstantsValues<TValue>(Type type)
		{
			return (from fieldInfo in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
				where fieldInfo.IsLiteral && !fieldInfo.IsInitOnly
				select fieldInfo into fi
				select (TValue)((object)fi.GetValue(null))).ToArray<TValue>();
		}
	}
}
