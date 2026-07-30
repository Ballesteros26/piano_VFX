using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for storing serialization data for the <see cref="T:System.ComponentModel.Design.Serialization.ComponentSerializationService" />.</summary>
	// Token: 0x0200035B RID: 859
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class SerializationStore : IDisposable
	{
		/// <summary>Gets a collection of errors that occurred during serialization or deserialization.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains errors that occurred during serialization or deserialization.</returns>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001AA6 RID: 6822
		public abstract ICollection Errors { get; }

		/// <summary>Closes the serialization store.</summary>
		// Token: 0x06001AA7 RID: 6823
		public abstract void Close();

		/// <summary>Saves the store to the given stream.</summary>
		/// <param name="stream">The stream to which the store will be serialized.</param>
		// Token: 0x06001AA8 RID: 6824
		public abstract void Save(Stream stream);

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		// Token: 0x06001AA9 RID: 6825 RVA: 0x0006B7DF File Offset: 0x000699DF
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001AAA RID: 6826 RVA: 0x0006B7E8 File Offset: 0x000699E8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}
	}
}
