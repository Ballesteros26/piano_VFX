using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies a list of interfaces that are exposed as COM event sources for the attributed class.</summary>
	// Token: 0x020008B5 RID: 2229
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[ComVisible(true)]
	public sealed class ComSourceInterfacesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the name of the event source interface.</summary>
		/// <param name="sourceInterfaces">A null-delimited list of fully qualified event source interface names. </param>
		// Token: 0x060054F4 RID: 21748 RVA: 0x0012843E File Offset: 0x0012663E
		public ComSourceInterfacesAttribute(string sourceInterfaces)
		{
			this._val = sourceInterfaces;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the type to use as a source interface.</summary>
		/// <param name="sourceInterface">The <see cref="T:System.Type" /> of the source interface. </param>
		// Token: 0x060054F5 RID: 21749 RVA: 0x0012844D File Offset: 0x0012664D
		public ComSourceInterfacesAttribute(Type sourceInterface)
		{
			this._val = sourceInterface.FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface. </param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface. </param>
		// Token: 0x060054F6 RID: 21750 RVA: 0x00128461 File Offset: 0x00126661
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
		{
			this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
		}

		/// <summary>Initializes a new instance of the ComSourceInterfacesAttribute class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface. </param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface. </param>
		/// <param name="sourceInterface3">The <see cref="T:System.Type" /> of a source interface. </param>
		// Token: 0x060054F7 RID: 21751 RVA: 0x00128488 File Offset: 0x00126688
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
		{
			this._val = string.Concat(new string[] { sourceInterface1.FullName, "\0", sourceInterface2.FullName, "\0", sourceInterface3.FullName });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface. </param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface. </param>
		/// <param name="sourceInterface3">The <see cref="T:System.Type" /> of a source interface. </param>
		/// <param name="sourceInterface4">The <see cref="T:System.Type" /> of a source interface. </param>
		// Token: 0x060054F8 RID: 21752 RVA: 0x001284D8 File Offset: 0x001266D8
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
		{
			this._val = string.Concat(new string[] { sourceInterface1.FullName, "\0", sourceInterface2.FullName, "\0", sourceInterface3.FullName, "\0", sourceInterface4.FullName });
		}

		/// <summary>Gets the fully qualified name of the event source interface.</summary>
		/// <returns>The fully qualified name of the event source interface.</returns>
		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x060054F9 RID: 21753 RVA: 0x00128539 File Offset: 0x00126739
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C0C RID: 11276
		internal string _val;
	}
}
