using System;
using System.Collections;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that Web server controls use to indicate support for event validation. This class cannot be inherited.</summary>
	// Token: 0x02000193 RID: 403
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SupportsEventValidationAttribute : Attribute
	{
		// Token: 0x06000FC1 RID: 4033 RVA: 0x0002B6B8 File Offset: 0x000298B8
		internal static bool SupportsEventValidation(Type type)
		{
			object obj = SupportsEventValidationAttribute._typesSupportsEventValidation[type];
			if (obj != null)
			{
				return (bool)obj;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(SupportsEventValidationAttribute), false);
			bool flag = customAttributes != null && customAttributes.Length != 0;
			SupportsEventValidationAttribute._typesSupportsEventValidation[type] = flag;
			return flag;
		}

		// Token: 0x0400132B RID: 4907
		private static Hashtable _typesSupportsEventValidation = Hashtable.Synchronized(new Hashtable());
	}
}
