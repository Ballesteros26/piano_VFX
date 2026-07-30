using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000571 RID: 1393
	internal class MonoTrustManager : IApplicationTrustManager, ISecurityEncodable
	{
		// Token: 0x06003E61 RID: 15969 RVA: 0x000DF4E0 File Offset: 0x000DD6E0
		[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
		public ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			return null;
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x000DF4F1 File Offset: 0x000DD6F1
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (e.Tag != "IApplicationTrustManager")
			{
				throw new ArgumentException("e", Locale.GetText("Invalid XML tag."));
			}
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x000DF528 File Offset: 0x000DD728
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IApplicationTrustManager");
			securityElement.AddAttribute("class", typeof(MonoTrustManager).AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x04001FE2 RID: 8162
		private const string tag = "IApplicationTrustManager";
	}
}
