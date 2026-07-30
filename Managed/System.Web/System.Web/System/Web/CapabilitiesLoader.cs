using System;
using System.Collections;
using System.IO;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000066 RID: 102
	internal sealed class CapabilitiesLoader : MarshalByRefObject
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00007A53 File Offset: 0x00005C53
		private CapabilitiesLoader()
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00007A5C File Offset: 0x00005C5C
		static CapabilitiesLoader()
		{
			CapabilitiesLoader.defaultCaps.Add("activexcontrols", "False");
			CapabilitiesLoader.defaultCaps.Add("alpha", "False");
			CapabilitiesLoader.defaultCaps.Add("aol", "False");
			CapabilitiesLoader.defaultCaps.Add("aolversion", "0");
			CapabilitiesLoader.defaultCaps.Add("authenticodeupdate", "");
			CapabilitiesLoader.defaultCaps.Add("backgroundsounds", "False");
			CapabilitiesLoader.defaultCaps.Add("beta", "False");
			CapabilitiesLoader.defaultCaps.Add("browser", "*");
			CapabilitiesLoader.defaultCaps.Add("browsers", new ArrayList());
			CapabilitiesLoader.defaultCaps.Add("cdf", "False");
			CapabilitiesLoader.defaultCaps.Add("clrversion", "0");
			CapabilitiesLoader.defaultCaps.Add("cookies", "False");
			CapabilitiesLoader.defaultCaps.Add("crawler", "False");
			CapabilitiesLoader.defaultCaps.Add("css", "0");
			CapabilitiesLoader.defaultCaps.Add("cssversion", "0");
			CapabilitiesLoader.defaultCaps.Add("ecmascriptversion", "0.0");
			CapabilitiesLoader.defaultCaps.Add("frames", "False");
			CapabilitiesLoader.defaultCaps.Add("iframes", "False");
			CapabilitiesLoader.defaultCaps.Add("isbanned", "False");
			CapabilitiesLoader.defaultCaps.Add("ismobiledevice", "False");
			CapabilitiesLoader.defaultCaps.Add("issyndicationreader", "False");
			CapabilitiesLoader.defaultCaps.Add("javaapplets", "False");
			CapabilitiesLoader.defaultCaps.Add("javascript", "False");
			CapabilitiesLoader.defaultCaps.Add("majorver", "0");
			CapabilitiesLoader.defaultCaps.Add("minorver", "0");
			CapabilitiesLoader.defaultCaps.Add("msdomversion", "0.0");
			CapabilitiesLoader.defaultCaps.Add("netclr", "False");
			CapabilitiesLoader.defaultCaps.Add("platform", "unknown");
			CapabilitiesLoader.defaultCaps.Add("stripper", "False");
			CapabilitiesLoader.defaultCaps.Add("supportscss", "False");
			CapabilitiesLoader.defaultCaps.Add("tables", "False");
			CapabilitiesLoader.defaultCaps.Add("vbscript", "False");
			CapabilitiesLoader.defaultCaps.Add("version", "0");
			CapabilitiesLoader.defaultCaps.Add("w3cdomversion", "0.0");
			CapabilitiesLoader.defaultCaps.Add("wap", "False");
			CapabilitiesLoader.defaultCaps.Add("win16", "False");
			CapabilitiesLoader.defaultCaps.Add("win32", "False");
			CapabilitiesLoader.defaultCaps.Add("win64", "False");
			CapabilitiesLoader.defaultCaps.Add("adapters", new Hashtable());
			CapabilitiesLoader.defaultCaps.Add("cancombineformsindeck", "False");
			CapabilitiesLoader.defaultCaps.Add("caninitiatevoicecall", "False");
			CapabilitiesLoader.defaultCaps.Add("canrenderafterinputorselectelement", "False");
			CapabilitiesLoader.defaultCaps.Add("canrenderemptyselects", "False");
			CapabilitiesLoader.defaultCaps.Add("canrenderinputandselectelementstogether", "False");
			CapabilitiesLoader.defaultCaps.Add("canrendermixedselects", "False");
			CapabilitiesLoader.defaultCaps.Add("canrenderoneventandprevelementstogether", "False");
			CapabilitiesLoader.defaultCaps.Add("canrenderpostbackcards", "False");
			CapabilitiesLoader.defaultCaps.Add("canrendersetvarzerowithmultiselectionlist", "False");
			CapabilitiesLoader.defaultCaps.Add("cansendmail", "False");
			CapabilitiesLoader.defaultCaps.Add("defaultsubmitbuttonlimit", "0");
			CapabilitiesLoader.defaultCaps.Add("gatewayminorversion", "0");
			CapabilitiesLoader.defaultCaps.Add("gatewaymajorversion", "0");
			CapabilitiesLoader.defaultCaps.Add("gatewayversion", "None");
			CapabilitiesLoader.defaultCaps.Add("hasbackbutton", "True");
			CapabilitiesLoader.defaultCaps.Add("hidesrightalignedmultiselectscrollbars", "False");
			CapabilitiesLoader.defaultCaps.Add("inputtype", "telephoneKeypad");
			CapabilitiesLoader.defaultCaps.Add("iscolor", "False");
			CapabilitiesLoader.defaultCaps.Add("jscriptversion", "0.0");
			CapabilitiesLoader.defaultCaps.Add("maximumhreflength", "0");
			CapabilitiesLoader.defaultCaps.Add("maximumrenderedpagesize", "2000");
			CapabilitiesLoader.defaultCaps.Add("maximumsoftkeylabellength", "5");
			CapabilitiesLoader.defaultCaps.Add("minorversionstring", "0.0");
			CapabilitiesLoader.defaultCaps.Add("mobiledevicemanufacturer", "Unknown");
			CapabilitiesLoader.defaultCaps.Add("mobiledevicemodel", "Unknown");
			CapabilitiesLoader.defaultCaps.Add("numberofsoftkeys", "0");
			CapabilitiesLoader.defaultCaps.Add("preferredimagemime", "image/gif");
			CapabilitiesLoader.defaultCaps.Add("preferredrenderingmime", "text/html");
			CapabilitiesLoader.defaultCaps.Add("preferredrenderingtype", "html32");
			CapabilitiesLoader.defaultCaps.Add("preferredrequestencoding", "");
			CapabilitiesLoader.defaultCaps.Add("preferredresponseencoding", "");
			CapabilitiesLoader.defaultCaps.Add("rendersbreakbeforewmlselectandinput", "False");
			CapabilitiesLoader.defaultCaps.Add("rendersbreaksafterhtmllists", "True");
			CapabilitiesLoader.defaultCaps.Add("rendersbreaksafterwmlanchor", "False");
			CapabilitiesLoader.defaultCaps.Add("rendersbreaksafterwmlinput", "False");
			CapabilitiesLoader.defaultCaps.Add("renderswmldoacceptsinline", "True");
			CapabilitiesLoader.defaultCaps.Add("renderswmlselectsasmenucards", "False");
			CapabilitiesLoader.defaultCaps.Add("requiredmetatagnamevalue", "");
			CapabilitiesLoader.defaultCaps.Add("requiresattributecolonsubstitution", "False");
			CapabilitiesLoader.defaultCaps.Add("requirescontenttypemetatag", "False");
			CapabilitiesLoader.defaultCaps.Add("requirescontrolstateinsession", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresdbcscharacter", "False");
			CapabilitiesLoader.defaultCaps.Add("requireshtmladaptiveerrorreporting", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresleadingpagebreak", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresnobreakinformatting", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresoutputoptimization", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresphonenumbersasplaintext", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresspecialviewstateencoding", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresuniquefilepathsuffix", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresuniquehtmlcheckboxnames", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresuniquehtmlinputnames", "False");
			CapabilitiesLoader.defaultCaps.Add("requiresurlencodedpostfieldvalues", "False");
			CapabilitiesLoader.defaultCaps.Add("screenbitdepth", "1");
			CapabilitiesLoader.defaultCaps.Add("screencharactersheight", "6");
			CapabilitiesLoader.defaultCaps.Add("screencharacterswidth", "12");
			CapabilitiesLoader.defaultCaps.Add("screenpixelsheight", "72");
			CapabilitiesLoader.defaultCaps.Add("screenpixelswidth", "96");
			CapabilitiesLoader.defaultCaps.Add("supportsaccesskeyattribute", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsbodycolor", "True");
			CapabilitiesLoader.defaultCaps.Add("supportsbold", "False");
			CapabilitiesLoader.defaultCaps.Add("supportscachecontrolmetatag", "True");
			CapabilitiesLoader.defaultCaps.Add("supportscallback", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsdivalign", "True");
			CapabilitiesLoader.defaultCaps.Add("supportsdivnowrap", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsemptystringincookievalue", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsfontcolor", "True");
			CapabilitiesLoader.defaultCaps.Add("supportsfontname", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsfontsize", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsimagesubmit", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsimodesymbols", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsinputistyle", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsinputmode", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsitalic", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsjphonemultimediaattributes", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsjphonesymbols", "False");
			CapabilitiesLoader.defaultCaps.Add("supportsquerystringinformaction", "True");
			CapabilitiesLoader.defaultCaps.Add("supportsredirectwithcookie", "True");
			CapabilitiesLoader.defaultCaps.Add("supportsselectmultiple", "True");
			CapabilitiesLoader.defaultCaps.Add("supportsuncheck", "True");
			CapabilitiesLoader.defaultCaps.Add("supportsxmlhttp", "False");
			CapabilitiesLoader.defaultCaps.Add("type", "Unknown");
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00008408 File Offset: 0x00006608
		public static Hashtable GetCapabilities(string userAgent)
		{
			CapabilitiesLoader.Init();
			if (userAgent != null)
			{
				userAgent = userAgent.Trim();
			}
			if (CapabilitiesLoader.alldata == null || userAgent == null || userAgent.Length == 0)
			{
				return CapabilitiesLoader.defaultCaps;
			}
			Hashtable hashtable = (Hashtable)(CapabilitiesLoader.userAgentsCache.Contains(userAgent) ? CapabilitiesLoader.userAgentsCache[userAgent] : null);
			if (hashtable == null)
			{
				foreach (object obj in CapabilitiesLoader.alldata)
				{
					BrowserData browserData = (BrowserData)obj;
					if (browserData.IsMatch(userAgent))
					{
						Hashtable hashtable2 = new Hashtable(CapabilitiesLoader.defaultCaps, StringComparer.OrdinalIgnoreCase);
						hashtable = browserData.GetProperties(hashtable2);
						break;
					}
				}
				if (hashtable == null)
				{
					hashtable = CapabilitiesLoader.defaultCaps;
				}
				object obj2 = CapabilitiesLoader.lockobj;
				lock (obj2)
				{
					if (CapabilitiesLoader.userAgentsCache.Count >= 3000)
					{
						CapabilitiesLoader.userAgentsCache.Clear();
					}
				}
				CapabilitiesLoader.userAgentsCache[userAgent] = hashtable;
			}
			return hashtable;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000852C File Offset: 0x0000672C
		private static void Init()
		{
			if (CapabilitiesLoader.loaded)
			{
				return;
			}
			object obj = CapabilitiesLoader.lockobj;
			lock (obj)
			{
				if (!CapabilitiesLoader.loaded)
				{
					string text = HttpRuntime.MachineConfigurationDirectory;
					string text2 = Path.Combine(text, "browscap.ini");
					if (!File.Exists(text2))
					{
						text = Path.GetDirectoryName(text);
						text2 = Path.Combine(text, "browscap.ini");
					}
					try
					{
						CapabilitiesLoader.LoadFile(text2);
					}
					catch (Exception)
					{
					}
					CapabilitiesLoader.loaded = true;
				}
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000085C8 File Offset: 0x000067C8
		private static void LoadFile(string filename)
		{
			if (!File.Exists(filename))
			{
				return;
			}
			TextReader textReader = new StreamReader(File.OpenRead(filename));
			using (textReader)
			{
				Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
				int num = 0;
				ArrayList arrayList = new ArrayList();
				string text;
				while ((text = textReader.ReadLine()) != null)
				{
					if (text.Length != 0 && text[0] != ';')
					{
						string text2 = text.Substring(1, text.Length - 2);
						BrowserData browserData = new BrowserData(text2);
						CapabilitiesLoader.ReadCapabilities(textReader, browserData);
						if (!(text2 == "*") && !(text2 == "GJK_Browscap_Version"))
						{
							string browser = browserData.GetBrowser();
							if (browser == null || hashtable.ContainsKey(browser))
							{
								hashtable.Add(num++, browserData);
								arrayList.Add(browserData);
							}
							else
							{
								hashtable.Add(browser, browserData);
								arrayList.Add(browserData);
							}
						}
					}
				}
				CapabilitiesLoader.alldata = arrayList;
				foreach (object obj in CapabilitiesLoader.alldata)
				{
					BrowserData browserData2 = (BrowserData)obj;
					string parentName = browserData2.GetParentName();
					if (parentName != null)
					{
						browserData2.Parent = (BrowserData)hashtable[parentName];
					}
				}
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00008758 File Offset: 0x00006958
		private static void ReadCapabilities(TextReader input, BrowserData data)
		{
			string text;
			while ((text = input.ReadLine()) != null && text.Length != 0)
			{
				string[] array = text.Split(CapabilitiesLoader.eq, 2);
				string text2 = array[0].ToLower(Helpers.InvariantCulture).Trim();
				if (text2.Length != 0)
				{
					data.Add(text2, array[1]);
				}
			}
		}

		// Token: 0x04000E53 RID: 3667
		private const int userAgentsCacheSize = 3000;

		// Token: 0x04000E54 RID: 3668
		private static Hashtable defaultCaps = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000E55 RID: 3669
		private static readonly object lockobj = new object();

		// Token: 0x04000E56 RID: 3670
		private static volatile bool loaded;

		// Token: 0x04000E57 RID: 3671
		private static ICollection alldata;

		// Token: 0x04000E58 RID: 3672
		private static Hashtable userAgentsCache = Hashtable.Synchronized(new Hashtable(3010));

		// Token: 0x04000E59 RID: 3673
		private static char[] eq = new char[] { '=' };
	}
}
