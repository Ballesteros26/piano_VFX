using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200004C RID: 76
	public class ResourcesHandler
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x0000E583 File Offset: 0x0000C783
		private ResourcesHandler()
		{
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000E58B File Offset: 0x0000C78B
		public static string getMessage(string messageOrKey, object[] arguments)
		{
			return ResourcesHandler.getMessage(messageOrKey, arguments, null);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000E598 File Offset: 0x0000C798
		public static string getMessage(string messageOrKey, object[] arguments, CultureInfo locale)
		{
			if (ResourcesHandler.defaultMessages == null)
			{
				ResourcesHandler.defaultMessages = new ResourceManager("ExceptionMessages", Assembly.GetExecutingAssembly());
			}
			if (ResourcesHandler.defaultLocale == null)
			{
				ResourcesHandler.defaultLocale = Thread.CurrentThread.CurrentUICulture;
			}
			if (locale == null)
			{
				locale = ResourcesHandler.defaultLocale;
			}
			if (messageOrKey == null)
			{
				messageOrKey = "";
			}
			string text;
			try
			{
				text = ResourcesHandler.defaultMessages.GetString(messageOrKey, locale);
			}
			catch (MissingManifestResourceException)
			{
				text = messageOrKey;
			}
			if (arguments != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat(text, arguments);
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000E628 File Offset: 0x0000C828
		public static string getResultString(int code)
		{
			return ResourcesHandler.getResultString(code, null);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000E634 File Offset: 0x0000C834
		public static string getResultString(int code, CultureInfo locale)
		{
			if (ResourcesHandler.defaultResultCodes == null)
			{
				ResourcesHandler.defaultResultCodes = new ResourceManager("ResultCodeMessages", Assembly.GetExecutingAssembly());
			}
			if (ResourcesHandler.defaultLocale == null)
			{
				ResourcesHandler.defaultLocale = Thread.CurrentThread.CurrentUICulture;
			}
			if (locale == null)
			{
				locale = ResourcesHandler.defaultLocale;
			}
			string text;
			try
			{
				text = ResourcesHandler.defaultResultCodes.GetString(Convert.ToString(code), ResourcesHandler.defaultLocale);
			}
			catch (ArgumentNullException)
			{
				text = ResourcesHandler.getMessage("UNKNOWN_RESULT", new object[] { code }, locale);
			}
			return text;
		}

		// Token: 0x040001F3 RID: 499
		private static ResourceManager defaultResultCodes = null;

		// Token: 0x040001F4 RID: 500
		private static ResourceManager defaultMessages = null;

		// Token: 0x040001F5 RID: 501
		private static string pkg = "Novell.Directory.Ldap.Utilclass.";

		// Token: 0x040001F6 RID: 502
		private static CultureInfo defaultLocale = Thread.CurrentThread.CurrentUICulture;
	}
}
