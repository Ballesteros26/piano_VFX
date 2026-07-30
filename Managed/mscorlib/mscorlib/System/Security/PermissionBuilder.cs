using System;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x02000545 RID: 1349
	internal static class PermissionBuilder
	{
		// Token: 0x06003C99 RID: 15513 RVA: 0x000D9A40 File Offset: 0x000D7C40
		public static IPermission Create(string fullname, PermissionState state)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", fullname);
			securityElement.AddAttribute("version", "1");
			if (state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return PermissionBuilder.CreatePermission(fullname, securityElement);
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000D9AA0 File Offset: 0x000D7CA0
		public static IPermission Create(SecurityElement se)
		{
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			string text = se.Attribute("class");
			if (text == null || text.Length == 0)
			{
				throw new ArgumentException("class");
			}
			return PermissionBuilder.CreatePermission(text, se);
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000D9AE4 File Offset: 0x000D7CE4
		public static IPermission Create(string fullname, SecurityElement se)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			return PermissionBuilder.CreatePermission(fullname, se);
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000D9B09 File Offset: 0x000D7D09
		public static IPermission Create(Type type)
		{
			return (IPermission)Activator.CreateInstance(type, PermissionBuilder.psNone);
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x000D9B1B File Offset: 0x000D7D1B
		internal static IPermission CreatePermission(string fullname, SecurityElement se)
		{
			Type type = Type.GetType(fullname);
			if (type == null)
			{
				throw new TypeLoadException(string.Format(Locale.GetText("Can't create an instance of permission class {0}."), fullname));
			}
			IPermission permission = PermissionBuilder.Create(type);
			permission.FromXml(se);
			return permission;
		}

		// Token: 0x04001F4C RID: 8012
		private static object[] psNone = new object[] { PermissionState.None };
	}
}
