using System;

namespace System.Web.ModelBinding
{
	/// <summary>Defines a method that must be implemented by classes that are metadata-aware.</summary>
	// Token: 0x0200071C RID: 1820
	public interface IMetadataAware
	{
		/// <summary>Enables metadata-aware attributes to perform required processing of metadata after the metadata is created.</summary>
		/// <param name="metadata">The metadata.</param>
		// Token: 0x06004C04 RID: 19460
		void OnMetadataCreated(ModelMetadata metadata);
	}
}
