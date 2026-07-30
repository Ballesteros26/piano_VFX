using System;
using System.Configuration.Provider;

namespace System.Web.Caching
{
	/// <summary>Represents the collection of output-cache providers that are configured for a Web site.</summary>
	// Token: 0x0200067A RID: 1658
	public sealed class OutputCacheProviderCollection : ProviderCollection
	{
		/// <summary>Returns a reference to the named output-cache provider in the collection.</summary>
		/// <returns>A provider from the collection. </returns>
		/// <param name="name">The name of a provider in the collection.</param>
		// Token: 0x170015EF RID: 5615
		public OutputCacheProvider this[string name]
		{
			get
			{
				return (OutputCacheProvider)base[name];
			}
		}

		/// <summary>Inserts a provider into the collection of output-cache providers.</summary>
		/// <param name="provider">An output cache provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="provider" /> is not of type <see cref="T:System.Web.Caching.OutputCacheProvider" />.</exception>
		// Token: 0x060046DE RID: 18142 RVA: 0x000C6D90 File Offset: 0x000C4F90
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is OutputCacheProvider))
			{
				throw new ArgumentException(global::SR.GetString("Provider must implement the class '{0}'.", new object[] { typeof(OutputCacheProvider).Name }), "provider");
			}
			base.Add(provider);
		}

		/// <summary>Copies the output-cache providers to a compatible one-dimensional array at the specified index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection The array must have zero-based indexing.</param>
		/// <param name="index">The point in the array where the copying starts.</param>
		// Token: 0x060046DF RID: 18143 RVA: 0x00091085 File Offset: 0x0008F285
		public void CopyTo(OutputCacheProvider[] array, int index)
		{
			base.CopyTo(array, index);
		}
	}
}
