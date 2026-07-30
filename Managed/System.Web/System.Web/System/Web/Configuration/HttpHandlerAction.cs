using System;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Web.Util;

namespace System.Web.Configuration
{
	/// <summary>Configures an <see cref="T:System.Web.Configuration.HttpHandlersSection" /> configuration section. This class cannot be inherited.</summary>
	// Token: 0x020005AC RID: 1452
	public sealed class HttpHandlerAction : ConfigurationElement
	{
		// Token: 0x06003E1A RID: 15898 RVA: 0x000A4978 File Offset: 0x000A2B78
		static HttpHandlerAction()
		{
			HttpHandlerAction._properties.Add(HttpHandlerAction.pathProp);
			HttpHandlerAction._properties.Add(HttpHandlerAction.typeProp);
			HttpHandlerAction._properties.Add(HttpHandlerAction.validateProp);
			HttpHandlerAction._properties.Add(HttpHandlerAction.verbProp);
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x0009F629 File Offset: 0x0009D829
		internal HttpHandlerAction()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> class using the passed parameters. </summary>
		/// <param name="path">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> URL path.</param>
		/// <param name="type">A comma-separated class/assembly combination consisting of version, culture, and public-key tokens.</param>
		/// <param name="verb">A comma-separated list of HTTP verbs (for example, "GET, PUT, POST").</param>
		// Token: 0x06003E1C RID: 15900 RVA: 0x000A4A77 File Offset: 0x000A2C77
		public HttpHandlerAction(string path, string type, string verb)
			: this(path, type, verb, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> class using the passed parameters.</summary>
		/// <param name="path">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> URL path.</param>
		/// <param name="type">A comma-separated class/assembly combination consisting of version, culture, and public-key tokens.</param>
		/// <param name="verb">A comma-separated list of HTTP verbs (for example, "GET, PUT, POST").</param>
		/// <param name="validate">true to allow validation; otherwise, false. If false, ASP.NET will not attempt to load the class until the actual matching request comes, potentially delaying the error but improving the startup time.</param>
		// Token: 0x06003E1D RID: 15901 RVA: 0x000A4A83 File Offset: 0x000A2C83
		public HttpHandlerAction(string path, string type, string verb, bool validate)
		{
			this.Path = path;
			this.Type = type;
			this.Verb = verb;
			this.Validate = validate;
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> path. </summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> URL path. </returns>
		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06003E1E RID: 15902 RVA: 0x000A4AA8 File Offset: 0x000A2CA8
		// (set) Token: 0x06003E1F RID: 15903 RVA: 0x000A4ABA File Offset: 0x000A2CBA
		[ConfigurationProperty("path", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Path
		{
			get
			{
				return (string)base[HttpHandlerAction.pathProp];
			}
			set
			{
				base[HttpHandlerAction.pathProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> type.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> type.</returns>
		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x000A4AC8 File Offset: 0x000A2CC8
		// (set) Token: 0x06003E21 RID: 15905 RVA: 0x000A4ADA File Offset: 0x000A2CDA
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[HttpHandlerAction.typeProp];
			}
			set
			{
				base[HttpHandlerAction.typeProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> validation.</summary>
		/// <returns>true if the validation is allowed; otherwise, false.</returns>
		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06003E22 RID: 15906 RVA: 0x000A4AE8 File Offset: 0x000A2CE8
		// (set) Token: 0x06003E23 RID: 15907 RVA: 0x000A4AFA File Offset: 0x000A2CFA
		[ConfigurationProperty("validate", DefaultValue = true)]
		public bool Validate
		{
			get
			{
				return (bool)base[HttpHandlerAction.validateProp];
			}
			set
			{
				base[HttpHandlerAction.validateProp] = value;
			}
		}

		/// <summary>Gets or sets the verb allowed by the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object.</summary>
		/// <returns>The verb allowed by the object.</returns>
		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x000A4B0D File Offset: 0x000A2D0D
		// (set) Token: 0x06003E25 RID: 15909 RVA: 0x000A4B1F File Offset: 0x000A2D1F
		[ConfigurationProperty("verb", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Verb
		{
			get
			{
				return (string)base[HttpHandlerAction.verbProp];
			}
			set
			{
				base[HttpHandlerAction.verbProp] = value;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06003E26 RID: 15910 RVA: 0x000A4B2D File Offset: 0x000A2D2D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpHandlerAction._properties;
			}
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x000A4B34 File Offset: 0x000A2D34
		private string[] SplitVerbs()
		{
			if (this.Verb == "*")
			{
				this.cached_verbs = null;
			}
			else
			{
				this.cached_verbs = this.Verb.Split(new char[] { ',' });
			}
			return this.cached_verbs;
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x000A4B73 File Offset: 0x000A2D73
		internal string[] Verbs
		{
			get
			{
				if (this.cached_verb != this.Verb)
				{
					this.cached_verbs = this.SplitVerbs();
					this.cached_verb = this.Verb;
				}
				return this.cached_verbs;
			}
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000A4BA8 File Offset: 0x000A2DA8
		internal static Type LoadType(string type_name)
		{
			Type type = HttpApplication.LoadType(type_name, false);
			if (type == null)
			{
				throw new HttpException(string.Format("Failed to load httpHandler type `{0}'", type_name));
			}
			if (typeof(IHttpHandler).IsAssignableFrom(type) || typeof(IHttpHandlerFactory).IsAssignableFrom(type))
			{
				return type;
			}
			throw new HttpException(string.Format("Type {0} does not implement IHttpHandler or IHttpHandlerFactory", type_name));
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x000A4C10 File Offset: 0x000A2E10
		internal bool PathMatches(string pathToMatch)
		{
			if (string.IsNullOrEmpty(pathToMatch))
			{
				return false;
			}
			bool flag = false;
			string[] array = this.Path.Split(new char[] { ',' });
			int num = pathToMatch.LastIndexOf('/');
			string text = pathToMatch;
			string text2 = null;
			if (num != -1)
			{
				pathToMatch = pathToMatch.Substring(num);
			}
			SearchPattern searchPattern = null;
			foreach (string text3 in array)
			{
				if (text3.Length != 0)
				{
					if (text3 == "*")
					{
						flag = true;
						break;
					}
					string text4 = null;
					string text5 = null;
					if (text3.Length > 0)
					{
						if (text3[0] == '*' && text3.IndexOf('*', 1) == -1)
						{
							text5 = text3.Substring(1);
						}
						if (text3.IndexOf('*') == -1 && text3[0] != '/')
						{
							HttpContext httpContext = HttpContext.Current;
							HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
							string text6 = ((httpRequest != null) ? httpRequest.BaseVirtualDir : HttpRuntime.AppDomainAppVirtualPath);
							if (text6 == "/")
							{
								text6 = string.Empty;
							}
							text4 = text6 + "/" + text3;
						}
					}
					if (text4 != null)
					{
						flag = text4.Length == text.Length && StrUtils.EndsWith(text, text4, true);
						if (flag)
						{
							break;
						}
					}
					else if (text5 != null)
					{
						flag = StrUtils.EndsWith(pathToMatch, text5, true);
						if (flag)
						{
							break;
						}
					}
					else
					{
						string text7;
						if (text3[0] == '/')
						{
							text7 = text3.Substring(1);
						}
						else
						{
							text7 = text3;
						}
						if (searchPattern == null)
						{
							searchPattern = new SearchPattern(text7, true);
						}
						else
						{
							searchPattern.SetPattern(text7, true);
						}
						if (text2 == null)
						{
							if (text[0] == '/')
							{
								text2 = text.Substring(1);
							}
							else
							{
								text2 = text;
							}
						}
						if (text7.IndexOf('/') >= 0)
						{
							text2 = HttpHandlerAction.AdjustPath(text7, text2);
						}
						if (searchPattern.IsMatch(text2))
						{
							flag = true;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x000A4DF4 File Offset: 0x000A2FF4
		private static string AdjustPath(string pattern, string path)
		{
			int num = 0;
			for (int i = 0; i < pattern.Length; i++)
			{
				if (pattern[i] == '/')
				{
					num++;
				}
			}
			int j;
			for (j = path.Length - 1; j >= 0; j--)
			{
				if (path[j] == '/')
				{
					num--;
					if (num == -1)
					{
						break;
					}
				}
			}
			if (num >= 0 || j == 0)
			{
				return path;
			}
			return path.Substring(j + 1);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000A4E60 File Offset: 0x000A3060
		internal object GetHandlerInstance()
		{
			IHttpHandler httpHandler = this.instance as IHttpHandler;
			if (this.instance == null || (httpHandler != null && !httpHandler.IsReusable))
			{
				if (this.type == null)
				{
					this.type = HttpHandlerAction.LoadType(this.Type);
				}
				this.instance = Activator.CreateInstance(this.type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null);
			}
			return this.instance;
		}

		// Token: 0x04002209 RID: 8713
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x0400220A RID: 8714
		private static ConfigurationProperty pathProp = new ConfigurationProperty("path", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400220B RID: 8715
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400220C RID: 8716
		private static ConfigurationProperty validateProp = new ConfigurationProperty("validate", typeof(bool), true);

		// Token: 0x0400220D RID: 8717
		private static ConfigurationProperty verbProp = new ConfigurationProperty("verb", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400220E RID: 8718
		private object instance;

		// Token: 0x0400220F RID: 8719
		private Type type;

		// Token: 0x04002210 RID: 8720
		private string cached_verb;

		// Token: 0x04002211 RID: 8721
		private string[] cached_verbs;
	}
}
