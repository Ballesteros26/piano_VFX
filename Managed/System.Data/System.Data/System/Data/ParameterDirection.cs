using System;

namespace System.Data
{
	/// <summary>Specifies the type of a parameter within a query relative to the <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D9 RID: 217
	public enum ParameterDirection
	{
		/// <summary>The parameter is an input parameter.</summary>
		// Token: 0x040007EF RID: 2031
		Input = 1,
		/// <summary>The parameter is an output parameter.</summary>
		// Token: 0x040007F0 RID: 2032
		Output,
		/// <summary>The parameter is capable of both input and output.</summary>
		// Token: 0x040007F1 RID: 2033
		InputOutput,
		/// <summary>The parameter represents a return value from an operation such as a stored procedure, built-in function, or user-defined function.</summary>
		// Token: 0x040007F2 RID: 2034
		ReturnValue = 6
	}
}
