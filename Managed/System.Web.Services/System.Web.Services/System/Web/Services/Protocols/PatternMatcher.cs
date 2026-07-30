using System;
using System.Security.Permissions;

namespace System.Web.Services.Protocols
{
	/// <summary>Searches HTTP response text for return values for Web service clients.</summary>
	// Token: 0x02000047 RID: 71
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class PatternMatcher
	{
		/// <summary>Creates a <see cref="T:System.Web.Services.Protocols.PatternMatcher" /> instance based on the input type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that specifies the return type for a Web method.</param>
		// Token: 0x06000184 RID: 388 RVA: 0x00006C3E File Offset: 0x00004E3E
		public PatternMatcher(Type type)
		{
			this.matchType = MatchType.Reflect(type);
		}

		/// <summary>Searches a text input to deserialize an object representing a Web method return value.</summary>
		/// <returns>An object representing a Web method return value.</returns>
		/// <param name="text">The text to search, which is the body of the HTTP response.</param>
		// Token: 0x06000185 RID: 389 RVA: 0x00006C52 File Offset: 0x00004E52
		public object Match(string text)
		{
			return this.matchType.Match(text);
		}

		// Token: 0x04000216 RID: 534
		private MatchType matchType;
	}
}
