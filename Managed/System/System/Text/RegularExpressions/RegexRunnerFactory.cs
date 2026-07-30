using System;
using System.ComponentModel;

namespace System.Text.RegularExpressions
{
	/// <summary>Creates a <see cref="T:System.Text.RegularExpressions.RegexRunner" /> class for a compiled regular expression.</summary>
	// Token: 0x02000157 RID: 343
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class RegexRunnerFactory
	{
		/// <summary>When overridden in a derived class, creates a <see cref="T:System.Text.RegularExpressions.RegexRunner" /> object for a specific compiled regular expression.</summary>
		/// <returns>A <see cref="T:System.Text.RegularExpressions.RegexRunner" /> object designed to execute a specific compiled regular expression. </returns>
		// Token: 0x06000A87 RID: 2695
		protected internal abstract RegexRunner CreateInstance();
	}
}
