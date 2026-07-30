using System;

namespace System.Xml.Schema
{
	// Token: 0x0200038D RID: 909
	internal class ChameleonKey
	{
		// Token: 0x060024E0 RID: 9440 RVA: 0x000DF53B File Offset: 0x000DD73B
		public ChameleonKey(string ns, XmlSchema originalSchema)
		{
			this.targetNS = ns;
			this.chameleonLocation = originalSchema.BaseUri;
			if (this.chameleonLocation.OriginalString.Length == 0)
			{
				this.originalSchema = originalSchema;
			}
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000DF570 File Offset: 0x000DD770
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				this.hashCode = this.targetNS.GetHashCode() + this.chameleonLocation.GetHashCode() + ((this.originalSchema == null) ? 0 : this.originalSchema.GetHashCode());
			}
			return this.hashCode;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000DF5C0 File Offset: 0x000DD7C0
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			ChameleonKey chameleonKey = obj as ChameleonKey;
			return chameleonKey != null && (this.targetNS.Equals(chameleonKey.targetNS) && this.chameleonLocation.Equals(chameleonKey.chameleonLocation)) && this.originalSchema == chameleonKey.originalSchema;
		}

		// Token: 0x040018F6 RID: 6390
		internal string targetNS;

		// Token: 0x040018F7 RID: 6391
		internal Uri chameleonLocation;

		// Token: 0x040018F8 RID: 6392
		internal XmlSchema originalSchema;

		// Token: 0x040018F9 RID: 6393
		private int hashCode;
	}
}
