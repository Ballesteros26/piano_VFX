using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies which <see cref="T:System.Type" /> exclusively uses an interface. This class cannot be inherited.</summary>
	// Token: 0x020008AD RID: 2221
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class TypeLibImportClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibImportClassAttribute" /> class specifying the <see cref="T:System.Type" /> that exclusively uses an interface. </summary>
		/// <param name="importClass">The <see cref="T:System.Type" /> object that exclusively uses an interface.</param>
		// Token: 0x060054E7 RID: 21735 RVA: 0x001283C6 File Offset: 0x001265C6
		public TypeLibImportClassAttribute(Type importClass)
		{
			this._importClassName = importClass.ToString();
		}

		/// <summary>Gets the name of a <see cref="T:System.Type" /> object that exclusively uses an interface.</summary>
		/// <returns>The name of a <see cref="T:System.Type" /> object that exclusively uses an interface.</returns>
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x060054E8 RID: 21736 RVA: 0x001283DA File Offset: 0x001265DA
		public string Value
		{
			get
			{
				return this._importClassName;
			}
		}

		// Token: 0x04002C03 RID: 11267
		internal string _importClassName;
	}
}
