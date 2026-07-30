using System;

namespace System.Net
{
	/// <summary>Provides the base interface to load and execute scripts for automatic proxy detection.</summary>
	// Token: 0x02000537 RID: 1335
	public interface IWebProxyScript
	{
		/// <summary>Closes a script.</summary>
		// Token: 0x06002958 RID: 10584
		void Close();

		/// <summary>Loads a script.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> indicating whether the script was successfully loaded.</returns>
		/// <param name="scriptLocation">Internal only.</param>
		/// <param name="script">Internal only.</param>
		/// <param name="helperType">Internal only.</param>
		// Token: 0x06002959 RID: 10585
		bool Load(Uri scriptLocation, string script, Type helperType);

		/// <summary>Runs a script.</summary>
		/// <returns>A <see cref="T:System.String" />.An internal-only value returned.</returns>
		/// <param name="url">Internal only.</param>
		/// <param name="host">Internal only.</param>
		// Token: 0x0600295A RID: 10586
		string Run(string url, string host);
	}
}
