using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides access to the metadata and MSIL for the body of a method.</summary>
	// Token: 0x0200031D RID: 797
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class MethodBody
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.MethodBody" /> class.</summary>
		// Token: 0x060022E3 RID: 8931 RVA: 0x00002111 File Offset: 0x00000311
		protected MethodBody()
		{
		}

		/// <summary>Gets a list that includes all the exception-handling clauses in the method body.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.ExceptionHandlingClause" /> objects representing the exception-handling clauses in the body of the method.</returns>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x00081DC4 File Offset: 0x0007FFC4
		public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				return Array.AsReadOnly<ExceptionHandlingClause>(this.clauses);
			}
		}

		/// <summary>Gets the list of local variables declared in the method body.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.LocalVariableInfo" /> objects that describe the local variables declared in the method body.</returns>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x00081DD1 File Offset: 0x0007FFD1
		public virtual IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				return Array.AsReadOnly<LocalVariableInfo>(this.locals);
			}
		}

		/// <summary>Gets a value indicating whether local variables in the method body are initialized to the default values for their types.</summary>
		/// <returns>true if the method body contains code to initialize local variables to null for reference types, or to the zero-initialized value for value types; otherwise, false.</returns>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x00081DDE File Offset: 0x0007FFDE
		public virtual bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
		}

		/// <summary>Gets a metadata token for the signature that describes the local variables for the method in metadata.</summary>
		/// <returns>An integer that represents the metadata token.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x00081DE6 File Offset: 0x0007FFE6
		public virtual int LocalSignatureMetadataToken
		{
			get
			{
				return this.sig_token;
			}
		}

		/// <summary>Gets the maximum number of items on the operand stack when the method is executing.</summary>
		/// <returns>The maximum number of items on the operand stack when the method is executing.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x00081DEE File Offset: 0x0007FFEE
		public virtual int MaxStackSize
		{
			get
			{
				return this.max_stack;
			}
		}

		/// <summary>Returns the MSIL for the method body, as an array of bytes.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains the MSIL for the method body. </returns>
		// Token: 0x060022E9 RID: 8937 RVA: 0x00081DF6 File Offset: 0x0007FFF6
		public virtual byte[] GetILAsByteArray()
		{
			return this.il;
		}

		// Token: 0x04001324 RID: 4900
		private ExceptionHandlingClause[] clauses;

		// Token: 0x04001325 RID: 4901
		private LocalVariableInfo[] locals;

		// Token: 0x04001326 RID: 4902
		private byte[] il;

		// Token: 0x04001327 RID: 4903
		private bool init_locals;

		// Token: 0x04001328 RID: 4904
		private int sig_token;

		// Token: 0x04001329 RID: 4905
		private int max_stack;
	}
}
