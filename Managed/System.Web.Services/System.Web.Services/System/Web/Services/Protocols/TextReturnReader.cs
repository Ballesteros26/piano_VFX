using System;
using System.IO;
using System.Net;
using System.Security.Permissions;

namespace System.Web.Services.Protocols
{
	/// <summary>Reads return values from HTTP response text for Web service clients implemented using HTTP but without SOAP.</summary>
	// Token: 0x02000084 RID: 132
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class TextReturnReader : MimeReturnReader
	{
		/// <summary>Initializes an instance.</summary>
		/// <param name="o">A <see cref="T:System.Web.Services.Protocols.PatternMatcher" /> object for the return type of the Web method being invoked.</param>
		// Token: 0x0600038B RID: 907 RVA: 0x00010E24 File Offset: 0x0000F024
		public override void Initialize(object o)
		{
			this.matcher = (PatternMatcher)o;
		}

		/// <summary>Returns an initializer for the specified method.</summary>
		/// <returns>An initializer for the specified method</returns>
		/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600038C RID: 908 RVA: 0x00010E32 File Offset: 0x0000F032
		public override object GetInitializer(LogicalMethodInfo methodInfo)
		{
			return new PatternMatcher(methodInfo.ReturnType);
		}

		/// <summary>Parses text contained in the HTTP response.</summary>
		/// <returns>An object containing the deserialized Web method return value.</returns>
		/// <param name="response">A <see cref="T:System.Net.WebResponse" /> object  containing the output message for an operation.</param>
		/// <param name="responseStream">A <see cref="T:System.IO.Stream" /> whose content is the body of the HTTP response represented by the <paramref name="response" /> parameter.</param>
		// Token: 0x0600038D RID: 909 RVA: 0x00010E40 File Offset: 0x0000F040
		public override object Read(WebResponse response, Stream responseStream)
		{
			object obj;
			try
			{
				string text = RequestResponseUtils.ReadResponse(response);
				obj = this.matcher.Match(text);
			}
			finally
			{
				response.Close();
			}
			return obj;
		}

		// Token: 0x04000301 RID: 769
		private PatternMatcher matcher;
	}
}
