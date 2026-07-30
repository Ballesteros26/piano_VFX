using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the collection of exceptions.</summary>
	// Token: 0x02000125 RID: 293
	[Serializable]
	public sealed class ExceptionCollection : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ExceptionCollection" /> class.</summary>
		/// <param name="exceptions">An array of type <see cref="T:System.Exception" />, containing the objects to populate the collection.</param>
		// Token: 0x060008BE RID: 2238 RVA: 0x0000F0C8 File Offset: 0x0000D2C8
		[MonoTODO]
		public ExceptionCollection(ArrayList exceptions)
		{
			this.exceptions = exceptions;
			throw new NotImplementedException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the <see cref="T:System.ComponentModel.Design.ExceptionCollection" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060008BF RID: 2239 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the array of <see cref="T:System.Exception" /> objects that represent the collection of exceptions.</summary>
		/// <returns>An <see cref="T:System.Exception" /> array that represent the collection of exceptions.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public ArrayList Exceptions
		{
			get
			{
				return this.exceptions;
			}
		}

		// Token: 0x040001F4 RID: 500
		private ArrayList exceptions;
	}
}
