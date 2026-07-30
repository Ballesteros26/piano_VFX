using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides a hint to the common language runtime (CLR) indicating how likely a dependency is to be loaded. This class is used in a dependent assembly to indicate what hint should be used when the parent does not specify the <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> attribute.  This class cannot be inherited. </summary>
	// Token: 0x02000864 RID: 2148
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DefaultDependencyAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.LoadHint" /> binding. </summary>
		/// <param name="loadHintArgument">One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values that indicates the default binding preference.</param>
		// Token: 0x0600543C RID: 21564 RVA: 0x001272B5 File Offset: 0x001254B5
		public DefaultDependencyAttribute(LoadHint loadHintArgument)
		{
			this.loadHint = loadHintArgument;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value that indicates when an assembly loads a dependency.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</returns>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x0600543D RID: 21565 RVA: 0x001272C4 File Offset: 0x001254C4
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002BB7 RID: 11191
		private LoadHint loadHint;
	}
}
