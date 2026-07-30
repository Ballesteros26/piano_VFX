using System;

namespace System.Security.Policy
{
	// Token: 0x02000570 RID: 1392
	internal sealed class MembershipConditionHelper
	{
		// Token: 0x06003E5D RID: 15965 RVA: 0x000DF3EC File Offset: 0x000DD5EC
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != MembershipConditionHelper.XmlTag)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid tag {0}, expected {1}."), se.Tag, MembershipConditionHelper.XmlTag), parameterName);
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
			return num;
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x000DF47C File Offset: 0x000DD67C
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement(MembershipConditionHelper.XmlTag);
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x04001FE1 RID: 8161
		private static readonly string XmlTag = "IMembershipCondition";
	}
}
