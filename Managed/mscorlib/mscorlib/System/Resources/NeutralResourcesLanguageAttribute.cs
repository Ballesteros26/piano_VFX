using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Informs the resource manager of an app's default culture. This class cannot be inherited.</summary>
	// Token: 0x020002A3 RID: 675
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[ComVisible(true)]
	public sealed class NeutralResourcesLanguageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> class.</summary>
		/// <param name="cultureName">The name of the culture that the current assembly's neutral resources were written in. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cultureName" /> parameter is null. </exception>
		// Token: 0x06001F06 RID: 7942 RVA: 0x00078916 File Offset: 0x00076B16
		public NeutralResourcesLanguageAttribute(string cultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			this._culture = cultureName;
			this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> class with the specified ultimate resource fallback location.</summary>
		/// <param name="cultureName">The name of the culture that the current assembly's neutral resources were written in.</param>
		/// <param name="location">One of the enumeration values that indicates the location from which to retrieve neutral fallback resources.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cultureName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="location" /> is not a member of <see cref="T:System.Resources.UltimateResourceFallbackLocation" />.</exception>
		// Token: 0x06001F07 RID: 7943 RVA: 0x0007893C File Offset: 0x00076B3C
		public NeutralResourcesLanguageAttribute(string cultureName, UltimateResourceFallbackLocation location)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			if (!Enum.IsDefined(typeof(UltimateResourceFallbackLocation), location))
			{
				throw new ArgumentException(Environment.GetResourceString("The NeutralResourcesLanguageAttribute specifies an invalid or unrecognized ultimate resource fallback location: \"{0}\".", new object[] { location }));
			}
			this._culture = cultureName;
			this._fallbackLoc = location;
		}

		/// <summary>Gets the culture name.</summary>
		/// <returns>The name of the default culture for the main assembly.</returns>
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x000789A1 File Offset: 0x00076BA1
		public string CultureName
		{
			get
			{
				return this._culture;
			}
		}

		/// <summary>Gets the location for the <see cref="T:System.Resources.ResourceManager" /> class to use to retrieve neutral resources by using the resource fallback process.</summary>
		/// <returns>One of the enumeration values that indicates the location (main assembly or satellite) from which to retrieve neutral resources.</returns>
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000789A9 File Offset: 0x00076BA9
		public UltimateResourceFallbackLocation Location
		{
			get
			{
				return this._fallbackLoc;
			}
		}

		// Token: 0x040010BC RID: 4284
		private string _culture;

		// Token: 0x040010BD RID: 4285
		private UltimateResourceFallbackLocation _fallbackLoc;
	}
}
