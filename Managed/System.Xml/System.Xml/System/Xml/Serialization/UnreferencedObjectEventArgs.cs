using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the known, but unreferenced, object found in an encoded SOAP XML stream during deserialization.</summary>
	// Token: 0x02000373 RID: 883
	public class UnreferencedObjectEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.UnreferencedObjectEventArgs" /> class.</summary>
		/// <param name="o">The unreferenced object. </param>
		/// <param name="id">A unique string used to identify the unreferenced object. </param>
		// Token: 0x0600240C RID: 9228 RVA: 0x000DCC48 File Offset: 0x000DAE48
		public UnreferencedObjectEventArgs(object o, string id)
		{
			this.o = o;
			this.id = id;
		}

		/// <summary>Gets the deserialized, but unreferenced, object.</summary>
		/// <returns>The deserialized, but unreferenced, object.</returns>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x000DCC5E File Offset: 0x000DAE5E
		public object UnreferencedObject
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets the ID of the object.</summary>
		/// <returns>The ID of the object.</returns>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x000DCC66 File Offset: 0x000DAE66
		public string UnreferencedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04001885 RID: 6277
		private object o;

		// Token: 0x04001886 RID: 6278
		private string id;
	}
}
