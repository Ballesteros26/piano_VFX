using System;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that ASP.NET server controls use to specify whether they participate in loading view-state information by <see cref="P:System.Web.UI.Control.ID" />. This class cannot be inherited.</summary>
	// Token: 0x0200024C RID: 588
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ViewStateModeByIdAttribute : Attribute
	{
	}
}
