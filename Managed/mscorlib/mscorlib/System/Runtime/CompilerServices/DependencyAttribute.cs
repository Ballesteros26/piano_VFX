using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates when a dependency is to be loaded by the referring assembly. This class cannot be inherited. </summary>
	// Token: 0x02000865 RID: 2149
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value. </summary>
		/// <param name="dependentAssemblyArgument">The dependent assembly to bind to.</param>
		/// <param name="loadHintArgument">One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</param>
		// Token: 0x0600543E RID: 21566 RVA: 0x001272CC File Offset: 0x001254CC
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.loadHint = loadHintArgument;
		}

		/// <summary>Gets the value of the dependent assembly. </summary>
		/// <returns>The name of the dependent assembly.</returns>
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x001272E2 File Offset: 0x001254E2
		public string DependentAssembly
		{
			get
			{
				return this.dependentAssembly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value that indicates when an assembly is to load a dependency. </summary>
		/// <returns>One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</returns>
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06005440 RID: 21568 RVA: 0x001272EA File Offset: 0x001254EA
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002BB8 RID: 11192
		private string dependentAssembly;

		// Token: 0x04002BB9 RID: 11193
		private LoadHint loadHint;
	}
}
