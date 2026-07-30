using System;

namespace System.Web.Caching
{
	/// <summary>Represents a single HTTP header that is included in a response from the output cache.</summary>
	// Token: 0x0200068F RID: 1679
	[Serializable]
	public sealed class HeaderElement
	{
		/// <summary>Gets the name of an HTTP header that is in the output cache.</summary>
		/// <returns>The name of the HTTP header. </returns>
		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x0600477D RID: 18301 RVA: 0x000C906C File Offset: 0x000C726C
		// (set) Token: 0x0600477E RID: 18302 RVA: 0x000C9074 File Offset: 0x000C7274
		public string Name { get; private set; }

		/// <summary>Gets the value of an HTTP header that is in the output cache.</summary>
		/// <returns>The value of the HTTP header.</returns>
		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x0600477F RID: 18303 RVA: 0x000C907D File Offset: 0x000C727D
		// (set) Token: 0x06004780 RID: 18304 RVA: 0x000C9085 File Offset: 0x000C7285
		public string Value { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.HeaderElement" /> class. </summary>
		/// <param name="name">The name of the HTTP header.</param>
		/// <param name="value">The value of the HTTP header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		// Token: 0x06004781 RID: 18305 RVA: 0x000C908E File Offset: 0x000C728E
		public HeaderElement(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Name = name;
			this.Value = value;
		}
	}
}
