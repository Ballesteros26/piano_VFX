using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x02000374 RID: 884
	internal sealed class PermissionHelper
	{
		// Token: 0x06001B07 RID: 6919 RVA: 0x0006C350 File Offset: 0x0006A550
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0006C3A8 File Offset: 0x0006A5A8
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None && state != PermissionState.Unrestricted)
			{
				throw new ArgumentException(string.Format(global::Locale.GetText("Invalid enum {0}"), state), "state");
			}
			return state;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0006C3D4 File Offset: 0x0006A5D4
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Attribute("class") == null)
			{
				throw new ArgumentException(global::Locale.GetText("Missing 'class' attribute."), parameterName);
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
					throw new ArgumentException(string.Format(global::Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, ex);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				throw new ArgumentException(string.Format(global::Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}']."), num, minimumVersion, maximumVersion), parameterName);
			}
			return num;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0006C480 File Offset: 0x0006A680
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0006C4B2 File Offset: 0x0006A6B2
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			throw new ArgumentException(string.Format(global::Locale.GetText("Invalid permission type '{0}', expected type '{1}'."), target.GetType(), expected), "target");
		}
	}
}
