using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x02000012 RID: 18
	internal sealed class PermissionHelper
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002C1C File Offset: 0x00000E1C
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C74 File Offset: 0x00000E74
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None)
			{
				if (state != PermissionState.Unrestricted)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), state), "state");
				}
				if (!allowUnrestricted)
				{
					throw new ArgumentException(Locale.GetText("Unrestricted isn't not allowed for identity permissions."), "state");
				}
			}
			return state;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002CC4 File Offset: 0x00000EC4
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Attribute("class") == null)
			{
				throw new ArgumentException(Locale.GetText("Missing 'class' attribute."), parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception ex)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, ex);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}']."), num, minimumVersion, maximumVersion), parameterName);
			}
			return num;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D70 File Offset: 0x00000F70
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002DA2 File Offset: 0x00000FA2
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			throw new ArgumentException(string.Format(Locale.GetText("Invalid permission type '{0}', expected type '{1}'."), target.GetType(), expected), "target");
		}
	}
}
