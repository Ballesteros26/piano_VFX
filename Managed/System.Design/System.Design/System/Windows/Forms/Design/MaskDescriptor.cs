using System;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	/// <summary>Defines a set of members for derived classes to provide options for the masked text box UI type editor.</summary>
	// Token: 0x0200002E RID: 46
	public abstract class MaskDescriptor
	{
		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> representing the locale the mask is authored for.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the locale the mask is authored for.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual CultureInfo Culture
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the mask being defined.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the mask being defined.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600017B RID: 379
		public abstract string Mask { get; }

		/// <summary>Gets the user-friendly name of the mask.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name or brief description of the <see cref="P:System.Windows.Forms.Design.MaskDescriptor.Mask" />.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600017C RID: 380
		public abstract string Name { get; }

		/// <summary>Gets a sample of a formatted string for the mask.</summary>
		/// <returns>A <see cref="T:System.String" /> containing text that is formatted by using the <see cref="P:System.Windows.Forms.Design.MaskDescriptor.Mask" />.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600017D RID: 381
		public abstract string Sample { get; }

		/// <summary>Gets the type providing validation associated with the mask.</summary>
		/// <returns>The <see cref="T:System.Type" /> that the formatted string is validated against.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017E RID: 382
		public abstract Type ValidatingType { get; }

		/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.Design.MaskDescriptor" /> is equal to the current <see cref="T:System.Windows.Forms.Design.MaskDescriptor" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Windows.Forms.Design.MaskDescriptor" /> is equal to the current <see cref="T:System.Windows.Forms.Design.MaskDescriptor" />; otherwise, false. </returns>
		/// <param name="maskDescriptor">The <see cref="T:System.Windows.Forms.Design.MaskDescriptor" /> to compare with the current <see cref="T:System.Windows.Forms.Design.MaskDescriptor" />.</param>
		// Token: 0x0600017F RID: 383 RVA: 0x00005142 File Offset: 0x00003342
		[MonoTODO]
		public override bool Equals(object maskDescriptor)
		{
			return base.Equals(maskDescriptor);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000514B File Offset: 0x0000334B
		[MonoTODO]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a value indicating whether the specified mask descriptor is valid and can be added to the masks list.</summary>
		/// <returns>true if <paramref name="maskDescriptor" /> is valid; otherwise, false. </returns>
		/// <param name="maskDescriptor">The mask descriptor to test for validity.</param>
		// Token: 0x06000181 RID: 385 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static bool IsValidMaskDescriptor(MaskDescriptor maskDescriptor)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a value indicating whether the specified mask descriptor is valid, and provides an error description if it is not valid.</summary>
		/// <returns>true if <paramref name="maskDescriptor" /> is valid; otherwise, false. </returns>
		/// <param name="maskDescriptor">The mask descriptor to test for validity.</param>
		/// <param name="validationErrorDescription">A string representing a validation error. If no validation error occurred, the <paramref name="validationErrorDescription" /> is <see cref="F:System.String.Empty" />.</param>
		// Token: 0x06000182 RID: 386 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static bool IsValidMaskDescriptor(MaskDescriptor maskDescriptor, out string validationErrorDescription)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005153 File Offset: 0x00003353
		[MonoTODO]
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
