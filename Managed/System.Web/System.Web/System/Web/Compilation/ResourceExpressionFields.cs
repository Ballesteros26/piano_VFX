using System;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Contains the fields from a parsed resource expression.</summary>
	// Token: 0x02000667 RID: 1639
	public sealed class ResourceExpressionFields
	{
		// Token: 0x06004625 RID: 17957 RVA: 0x000C12BD File Offset: 0x000BF4BD
		internal ResourceExpressionFields(string classKey, string resourceKey)
		{
			this.classKey = classKey;
			this.resourceKey = resourceKey;
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x000C12D3 File Offset: 0x000BF4D3
		internal ResourceExpressionFields(string resourceKey)
			: this(null, resourceKey)
		{
		}

		/// <summary>Gets the class key for a parsed resource expression.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the class key, or <see cref="F:System.String.Empty" /> if the class key has not been set.</returns>
		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x000C12DD File Offset: 0x000BF4DD
		public string ClassKey
		{
			get
			{
				return this.classKey;
			}
		}

		/// <summary>Gets the resource key for a parsed resource expression.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the resource key, or <see cref="F:System.String.Empty" /> if the resource key has not been set.</returns>
		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x000C12E5 File Offset: 0x000BF4E5
		public string ResourceKey
		{
			get
			{
				return this.resourceKey;
			}
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal ResourceExpressionFields()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400252B RID: 9515
		private string classKey;

		// Token: 0x0400252C RID: 9516
		private string resourceKey;
	}
}
