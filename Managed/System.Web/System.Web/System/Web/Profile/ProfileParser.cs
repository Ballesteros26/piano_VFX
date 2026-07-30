using System;
using System.Web.Compilation;

namespace System.Web.Profile
{
	// Token: 0x0200050E RID: 1294
	internal sealed class ProfileParser
	{
		// Token: 0x0600398C RID: 14732 RVA: 0x00002050 File Offset: 0x00000250
		internal ProfileParser(HttpContext context)
		{
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x0009ADFC File Offset: 0x00098FFC
		public static Type GetProfileCommonType(HttpContext context)
		{
			string text;
			if (AppCodeCompiler.DefaultAppCodeAssemblyName != null)
			{
				text = "ProfileCommon, " + AppCodeCompiler.DefaultAppCodeAssemblyName;
			}
			else
			{
				text = "ProfileCommon";
			}
			Type type = Type.GetType(text);
			type == null;
			return type;
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x0009AE38 File Offset: 0x00099038
		public static Type GetProfileGroupType(HttpContext context, string groupName)
		{
			string text;
			if (AppCodeCompiler.DefaultAppCodeAssemblyName != null)
			{
				text = "ProfileGroup" + groupName + ", " + AppCodeCompiler.DefaultAppCodeAssemblyName;
			}
			else
			{
				text = "ProfileGroup" + groupName;
			}
			Type type = Type.GetType(text);
			type == null;
			return type;
		}
	}
}
