using System;
using System.Reflection;
using Mono.Mozilla;

namespace Mono.WebBrowser
{
	// Token: 0x0200001F RID: 31
	public sealed class Manager
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000243D File Offset: 0x0000063D
		public static IWebBrowser GetNewInstance()
		{
			return Manager.GetNewInstance(Platform.Winforms);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002448 File Offset: 0x00000648
		public static IWebBrowser GetNewInstance(Platform platform)
		{
			string text = Environment.GetEnvironmentVariable("MONO_BROWSER_ENGINE");
			if (text == "webkit")
			{
				try
				{
					return (IWebBrowser)Assembly.LoadWithPartialName("mono-webkit").CreateInstance("Mono.WebKit.WebBrowser");
				}
				catch
				{
					text = null;
				}
			}
			if (text == null || text == "mozilla")
			{
				return new WebBrowser(platform);
			}
			throw new Exception(Exception.ErrorCodes.EngineNotSupported, text);
		}
	}
}
