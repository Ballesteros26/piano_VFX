using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures a <see cref="T:System.Web.Configuration.CustomError" /> section to map an ASP.NET error code to a custom page. This class cannot be inherited.</summary>
	// Token: 0x02000596 RID: 1430
	public sealed class CustomError : ConfigurationElement
	{
		// Token: 0x06003C92 RID: 15506 RVA: 0x000A14A8 File Offset: 0x0009F6A8
		static CustomError()
		{
			CustomError.properties.Add(CustomError.redirectProp);
			CustomError.properties.Add(CustomError.statusCodeProp);
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x0009F629 File Offset: 0x0009D829
		internal CustomError()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CustomError" /> class. </summary>
		/// <param name="statusCode">The HTTP status code that will result in redirection to the error page.</param>
		/// <param name="redirect">The URL of the custom page mapped to the error code.</param>
		// Token: 0x06003C94 RID: 15508 RVA: 0x000A1543 File Offset: 0x0009F743
		public CustomError(int statusCode, string redirect)
		{
			this.StatusCode = statusCode;
			this.Redirect = redirect;
		}

		/// <summary>Compares <see cref="T:System.Web.Configuration.CustomError" /> errors.</summary>
		/// <returns>true if the errors  are equal; otherwise, false.</returns>
		/// <param name="customError">The error to compare against.</param>
		// Token: 0x06003C95 RID: 15509 RVA: 0x000A155C File Offset: 0x0009F75C
		public override bool Equals(object customError)
		{
			CustomError customError2 = customError as CustomError;
			return customError2 != null && this.Redirect == customError2.Redirect && this.StatusCode == customError2.StatusCode;
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> object hash code.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> object hash code.</returns>
		// Token: 0x06003C96 RID: 15510 RVA: 0x000A1598 File Offset: 0x0009F798
		public override int GetHashCode()
		{
			return this.Redirect.GetHashCode() + this.StatusCode;
		}

		/// <summary>Gets or sets the redirection URL.</summary>
		/// <returns>The URL to which the application is redirected when an error occurs.</returns>
		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06003C97 RID: 15511 RVA: 0x000A15AC File Offset: 0x0009F7AC
		// (set) Token: 0x06003C98 RID: 15512 RVA: 0x000A15BE File Offset: 0x0009F7BE
		[ConfigurationProperty("redirect", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string Redirect
		{
			get
			{
				return (string)base[CustomError.redirectProp];
			}
			set
			{
				base[CustomError.redirectProp] = value;
			}
		}

		/// <summary>Gets or sets the HTTP error status code.</summary>
		/// <returns>The HTTP error status code that causes the redirection to the custom error page.</returns>
		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06003C99 RID: 15513 RVA: 0x000A15CC File Offset: 0x0009F7CC
		// (set) Token: 0x06003C9A RID: 15514 RVA: 0x000A15DE File Offset: 0x0009F7DE
		[ConfigurationProperty("statusCode", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[IntegerValidator(MinValue = 100, MaxValue = 999)]
		public int StatusCode
		{
			get
			{
				return (int)base[CustomError.statusCodeProp];
			}
			set
			{
				base[CustomError.statusCodeProp] = value;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x000A15F1 File Offset: 0x0009F7F1
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomError.properties;
			}
		}

		// Token: 0x040020D0 RID: 8400
		private static ConfigurationProperty redirectProp = new ConfigurationProperty("redirect", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), new StringValidator(1), ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020D1 RID: 8401
		private static ConfigurationProperty statusCodeProp = new ConfigurationProperty("statusCode", typeof(int), null, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(100, 999), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020D2 RID: 8402
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
