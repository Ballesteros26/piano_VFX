using System;

namespace System.Web.SessionState
{
	/// <summary>Specifies that the target HTTP handler requires only read access to session-state values. This is a marker interface and has no methods.</summary>
	// Token: 0x02000490 RID: 1168
	public interface IReadOnlySessionState : IRequiresSessionState
	{
	}
}
