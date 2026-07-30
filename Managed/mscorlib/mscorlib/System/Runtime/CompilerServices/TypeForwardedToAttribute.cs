using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies a destination <see cref="T:System.Type" /> in another assembly. </summary>
	// Token: 0x0200085B RID: 2139
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class TypeForwardedToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TypeForwardedToAttribute" /> class specifying a destination <see cref="T:System.Type" />. </summary>
		/// <param name="destination">The destination <see cref="T:System.Type" /> in another assembly.</param>
		// Token: 0x0600542C RID: 21548 RVA: 0x001271B1 File Offset: 0x001253B1
		public TypeForwardedToAttribute(Type destination)
		{
			this._destination = destination;
		}

		/// <summary>Gets the destination <see cref="T:System.Type" /> in another assembly.</summary>
		/// <returns>The destination <see cref="T:System.Type" /> in another assembly.</returns>
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x0600542D RID: 21549 RVA: 0x001271C0 File Offset: 0x001253C0
		public Type Destination
		{
			get
			{
				return this._destination;
			}
		}

		// Token: 0x04002BAF RID: 11183
		private Type _destination;
	}
}
