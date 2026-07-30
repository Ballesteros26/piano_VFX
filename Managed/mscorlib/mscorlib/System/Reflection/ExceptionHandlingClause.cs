using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents a clause in a structured exception-handling block.</summary>
	// Token: 0x02000319 RID: 793
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class ExceptionHandlingClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ExceptionHandlingClause" /> class.</summary>
		// Token: 0x060022A9 RID: 8873 RVA: 0x00002111 File Offset: 0x00000311
		protected ExceptionHandlingClause()
		{
		}

		/// <summary>Gets the type of exception handled by this clause.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents that type of exception handled by this clause, or null if the <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> property is <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" /> or <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Finally" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Invalid use of property for the object's current state.</exception>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x00081A46 File Offset: 0x0007FC46
		public virtual Type CatchType
		{
			get
			{
				return this.catch_type;
			}
		}

		/// <summary>Gets the offset within the method body, in bytes, of the user-supplied filter code.</summary>
		/// <returns>The offset within the method body, in bytes, of the user-supplied filter code. The value of this property has no meaning if the <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> property has any value other than <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot get the offset because the exception handling clause is not a filter.</exception>
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x00081A4E File Offset: 0x0007FC4E
		public virtual int FilterOffset
		{
			get
			{
				return this.filter_offset;
			}
		}

		/// <summary>Gets a value indicating whether this exception-handling clause is a finally clause, a type-filtered clause, or a user-filtered clause.</summary>
		/// <returns>An <see cref="T:System.Reflection.ExceptionHandlingClauseOptions" /> value that indicates what kind of action this clause performs.</returns>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x00081A56 File Offset: 0x0007FC56
		public virtual ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return this.flags;
			}
		}

		/// <summary>Gets the length, in bytes, of the body of this exception-handling clause.</summary>
		/// <returns>An integer that represents the length, in bytes, of the MSIL that forms the body of this exception-handling clause.</returns>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x00081A5E File Offset: 0x0007FC5E
		public virtual int HandlerLength
		{
			get
			{
				return this.handler_length;
			}
		}

		/// <summary>Gets the offset within the method body, in bytes, of this exception-handling clause.</summary>
		/// <returns>An integer that represents the offset within the method body, in bytes, of this exception-handling clause.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x00081A66 File Offset: 0x0007FC66
		public virtual int HandlerOffset
		{
			get
			{
				return this.handler_offset;
			}
		}

		/// <summary>The total length, in bytes, of the try block that includes this exception-handling clause.</summary>
		/// <returns>The total length, in bytes, of the try block that includes this exception-handling clause.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x00081A6E File Offset: 0x0007FC6E
		public virtual int TryLength
		{
			get
			{
				return this.try_length;
			}
		}

		/// <summary>The offset within the method, in bytes, of the try block that includes this exception-handling clause.</summary>
		/// <returns>An integer that represents the offset within the method, in bytes, of the try block that includes this exception-handling clause.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00081A76 File Offset: 0x0007FC76
		public virtual int TryOffset
		{
			get
			{
				return this.try_offset;
			}
		}

		/// <summary>A string representation of the exception-handling clause.</summary>
		/// <returns>A string that lists appropriate property values for the filter clause type.</returns>
		// Token: 0x060022B1 RID: 8881 RVA: 0x00081A80 File Offset: 0x0007FC80
		public override string ToString()
		{
			string text = string.Format("Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", new object[] { this.flags, this.try_offset, this.try_length, this.handler_offset, this.handler_length });
			if (this.catch_type != null)
			{
				text = string.Format("{0}, CatchType={1}", text, this.catch_type);
			}
			if (this.flags == ExceptionHandlingClauseOptions.Filter)
			{
				text = string.Format("{0}, FilterOffset={1}", text, this.filter_offset);
			}
			return text;
		}

		// Token: 0x04001315 RID: 4885
		internal Type catch_type;

		// Token: 0x04001316 RID: 4886
		internal int filter_offset;

		// Token: 0x04001317 RID: 4887
		internal ExceptionHandlingClauseOptions flags;

		// Token: 0x04001318 RID: 4888
		internal int try_offset;

		// Token: 0x04001319 RID: 4889
		internal int try_length;

		// Token: 0x0400131A RID: 4890
		internal int handler_offset;

		// Token: 0x0400131B RID: 4891
		internal int handler_length;
	}
}
