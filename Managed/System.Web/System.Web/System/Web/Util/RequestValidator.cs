using System;
using System.Configuration;
using System.Web.Configuration;
using Unity;

namespace System.Web.Util
{
	/// <summary>Defines base methods for custom request validation. </summary>
	// Token: 0x02000143 RID: 323
	public class RequestValidator
	{
		/// <summary>Gets or sets a reference to the current <see cref="T:System.Web.Util.RequestValidator" /> instance that will be used in an application. </summary>
		/// <returns>An instance of the <see cref="T:System.Web.Util.RequestValidator" /> class.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is null. </exception>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00029E2C File Offset: 0x0002802C
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x00029E49 File Offset: 0x00028049
		public static RequestValidator Current
		{
			get
			{
				if (RequestValidator.current == null)
				{
					RequestValidator.current = RequestValidator.lazyLoader.Value;
				}
				return RequestValidator.current;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				RequestValidator.current = value;
			}
		}

		/// <summary>Validates a string that contains HTTP request data.</summary>
		/// <returns>true if the string to be validated is valid; otherwise, false.</returns>
		/// <param name="context">The context of the current request.</param>
		/// <param name="value">The HTTP request data to validate.</param>
		/// <param name="requestValidationSource">An enumeration that represents the source of request data that is being validated. The following are possible values for the enumeration:QueryStringForm CookiesFilesRawUrlPathPathInfoHeaders</param>
		/// <param name="collectionKey">The key in the request collection of the item to validate. This parameter is optional. This parameter is used if the data to validate is obtained from a collection. If the data to validate is not from a collection, <paramref name="collectionKey" /> can be null. </param>
		/// <param name="validationFailureIndex">When this method returns, indicates the zero-based starting point of the problematic or invalid text in the request collection. This parameter is passed uninitialized.</param>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x00029E77 File Offset: 0x00028077
		protected internal virtual bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
		{
			validationFailureIndex = 0;
			return !HttpRequest.IsInvalidString(value, out validationFailureIndex);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00029E88 File Offset: 0x00028088
		private static void ParseTypeName(string spec, out string typeName, out string assemblyName)
		{
			try
			{
				if (string.IsNullOrEmpty(spec))
				{
					typeName = null;
					assemblyName = null;
				}
				else
				{
					int num = spec.IndexOf(',');
					if (num == -1)
					{
						typeName = spec;
						assemblyName = null;
					}
					else
					{
						typeName = spec.Substring(0, num).Trim();
						assemblyName = spec.Substring(num + 1).Trim();
					}
				}
			}
			catch
			{
				typeName = spec;
				assemblyName = null;
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00029EF4 File Offset: 0x000280F4
		private static RequestValidator LoadConfiguredValidator()
		{
			HttpRuntimeSection section = HttpRuntime.Section;
			Type type = null;
			string requestValidationType = section.RequestValidationType;
			try
			{
				type = HttpApplication.LoadType<RequestValidator>(requestValidationType, true);
			}
			catch (TypeLoadException ex)
			{
				string text;
				string text2;
				RequestValidator.ParseTypeName(requestValidationType, out text, out text2);
				throw new ConfigurationErrorsException(string.Format("Could not load type '{0}' from assembly '{1}'.", text, text2), ex);
			}
			return (RequestValidator)Activator.CreateInstance(type);
		}

		/// <summary>Provides a public method that calls the protected <see cref="M:System.Web.Util.RequestValidator.IsValidRequestString(System.Web.HttpContext,System.String,System.Web.Util.RequestValidationSource,System.String,System.Int32@)" /> method in order to validate HTTP request data. </summary>
		/// <returns>true if the string to validate does not contain unencoded characters that could be used in a malicious scripting attack; otherwise, false.</returns>
		/// <param name="context">The HTTP context of the request.</param>
		/// <param name="value">The HTTP request data to validate.</param>
		/// <param name="requestValidationSource">An enumeration that represents the source of request data that is being validated. The following are possible values for the enumeration:QueryStringForm CookiesFilesPathPathInfoHeaders</param>
		/// <param name="collectionKey">(Optional) The key in the request collection of the item to validate. This parameter is used if the data to validate is obtained from a collection. If the data to validate is not from a collection, this parameter can be null. </param>
		/// <param name="validationFailureIndex">When this method returns, indicates the zero-based starting point of the problematic or invalid text in the request collection. This parameter is passed uninitialized.</param>
		// Token: 0x06000EBB RID: 3771 RVA: 0x00029F54 File Offset: 0x00028154
		public bool InvokeIsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04001205 RID: 4613
		private static RequestValidator current;

		// Token: 0x04001206 RID: 4614
		private static Lazy<RequestValidator> lazyLoader = new Lazy<RequestValidator>(new Func<RequestValidator>(RequestValidator.LoadConfiguredValidator));
	}
}
