using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Provides a base set of functionality for classes involved in parsing ASP.NET page requests and server controls.</summary>
	// Token: 0x020001A3 RID: 419
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class BaseParser
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x0002C104 File Offset: 0x0002A304
		internal string MapPath(string path)
		{
			return this.MapPath(path, true);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0002C10E File Offset: 0x0002A30E
		internal string MapPath(string path, bool allowCrossAppMapping)
		{
			if (this.context == null)
			{
				throw new HttpException("context is null!!");
			}
			return this.context.Request.MapPath(path, this.BaseVirtualDir, allowCrossAppMapping);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0002C13B File Offset: 0x0002A33B
		internal string PhysicalPath(string path)
		{
			if (Path.DirectorySeparatorChar != '/')
			{
				path = path.Replace('/', '\\');
			}
			return Path.Combine(this.BaseDir, path);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0002C160 File Offset: 0x0002A360
		internal bool GetBool(IDictionary hash, string key, bool deflt)
		{
			string text = hash[key] as string;
			if (text == null)
			{
				return deflt;
			}
			hash.Remove(key);
			bool flag = false;
			if (string.Compare(text, "true", true, Helpers.InvariantCulture) == 0)
			{
				flag = true;
			}
			else if (string.Compare(text, "false", true, Helpers.InvariantCulture) != 0)
			{
				this.ThrowParseException("Invalid value for " + key, Array.Empty<object>());
			}
			return flag;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0002C1CC File Offset: 0x0002A3CC
		internal static string GetString(IDictionary hash, string key, string deflt)
		{
			string text = hash[key] as string;
			if (text == null)
			{
				return deflt;
			}
			hash.Remove(key);
			return text;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0002C1F4 File Offset: 0x0002A3F4
		internal static bool IsDirective(string value, char directiveChar)
		{
			if (value == null || value == string.Empty)
			{
				return false;
			}
			value = value.Trim();
			if (!StrUtils.StartsWith(value, "<%") || !StrUtils.EndsWith(value, "%>"))
			{
				return false;
			}
			int i = value.IndexOf(directiveChar, 2);
			if (i == -1)
			{
				return false;
			}
			if (i == 2)
			{
				return true;
			}
			for (i--; i >= 2; i--)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0002C26B File Offset: 0x0002A46B
		internal static bool IsDataBound(string value)
		{
			return BaseParser.IsDirective(value, '#');
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0002C275 File Offset: 0x0002A475
		internal static bool IsExpression(string value)
		{
			return BaseParser.IsDirective(value, '$');
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0002C27F File Offset: 0x0002A47F
		internal void ThrowParseException(string message, params object[] parms)
		{
			if (parms == null)
			{
				throw new ParseException(this.location, message);
			}
			throw new ParseException(this.location, string.Format(message, parms));
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0002C2A3 File Offset: 0x0002A4A3
		internal void ThrowParseException(string message, Exception inner, params object[] parms)
		{
			if (parms == null || parms.Length == 0)
			{
				throw new ParseException(this.location, message, inner);
			}
			throw new ParseException(this.location, string.Format(message, parms), inner);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0002C2CD File Offset: 0x0002A4CD
		internal void ThrowParseFileNotFound(string path, params object[] parms)
		{
			this.ThrowParseException("The file '" + path + "' does not exist", parms);
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x0002C2E6 File Offset: 0x0002A4E6
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x0002C2EE File Offset: 0x0002A4EE
		internal ILocation Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0002C2F7 File Offset: 0x0002A4F7
		// (set) Token: 0x06001008 RID: 4104 RVA: 0x0002C2FF File Offset: 0x0002A4FF
		internal HttpContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0002C308 File Offset: 0x0002A508
		internal string BaseDir
		{
			get
			{
				if (this.baseDir == null)
				{
					this.baseDir = this.MapPath(this.BaseVirtualDir, false);
				}
				return this.baseDir;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0002C32B File Offset: 0x0002A52B
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x0002C356 File Offset: 0x0002A556
		internal virtual string BaseVirtualDir
		{
			get
			{
				if (this.baseVDir == null)
				{
					this.baseVDir = VirtualPathUtility.GetDirectory(this.context.Request.FilePath);
				}
				return this.baseVDir;
			}
			set
			{
				if (VirtualPathUtility.IsRooted(value))
				{
					this.baseVDir = VirtualPathUtility.ToAbsolute(value);
					return;
				}
				this.baseVDir = value;
			}
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0002C374 File Offset: 0x0002A574
		internal TSection GetConfigSection<TSection>(string section) where TSection : ConfigurationSection
		{
			VirtualPath virtualPath = this.VirtualPath;
			string text = ((virtualPath != null) ? virtualPath.Absolute : null);
			if (text == null)
			{
				return WebConfigurationManager.GetSection(section) as TSection;
			}
			return WebConfigurationManager.GetSection(section, text) as TSection;
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0002C3BA File Offset: 0x0002A5BA
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x0002C3C2 File Offset: 0x0002A5C2
		internal VirtualPath VirtualPath { get; set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0002C3CB File Offset: 0x0002A5CB
		internal CompilationSection CompilationConfig
		{
			get
			{
				return this.GetConfigSection<CompilationSection>("system.web/compilation");
			}
		}

		// Token: 0x0400134C RID: 4940
		private HttpContext context;

		// Token: 0x0400134D RID: 4941
		private string baseDir;

		// Token: 0x0400134E RID: 4942
		private string baseVDir;

		// Token: 0x0400134F RID: 4943
		private ILocation location;
	}
}
