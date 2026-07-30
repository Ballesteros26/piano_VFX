using System;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides static methods for retrieving feature information from the current system.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000162 RID: 354
	public abstract class FeatureSupport : IFeatureSupport
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x00057480 File Offset: 0x00055680
		private static IFeatureSupport FeatureObject(string class_name)
		{
			Type type = Type.GetType(class_name);
			if (type != null && typeof(IFeatureSupport).IsAssignableFrom(type))
			{
				ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
				if (constructor != null)
				{
					return (IFeatureSupport)constructor.Invoke(new object[0]);
				}
			}
			return null;
		}

		/// <summary>Gets the version of the specified feature that is available on the system.</summary>
		/// <returns>A <see cref="T:System.Version" /> with the version number of the specified feature available on the system; or null if the feature is not installed.</returns>
		/// <param name="featureClassName">The fully qualified name of the class to query for information about the specified feature. This class must implement the <see cref="T:System.Windows.Forms.IFeatureSupport" /> interface or inherit from a class that implements this interface. </param>
		/// <param name="featureConstName">The fully qualified name of the feature to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017D6 RID: 6102 RVA: 0x000574D4 File Offset: 0x000556D4
		public static Version GetVersionPresent(string featureClassName, string featureConstName)
		{
			IFeatureSupport featureSupport = FeatureSupport.FeatureObject(featureClassName);
			if (featureSupport != null)
			{
				return featureSupport.GetVersionPresent(featureConstName);
			}
			return null;
		}

		/// <summary>Determines whether any version of the specified feature is installed in the system. This method is static.</summary>
		/// <returns>true if the specified feature is present; false if the specified feature is not present or if the product containing the feature is not installed.</returns>
		/// <param name="featureClassName">The fully qualified name of the class to query for information about the specified feature. This class must implement the <see cref="T:System.Windows.Forms.IFeatureSupport" /> interface or inherit from a class that implements this interface. </param>
		/// <param name="featureConstName">The fully qualified name of the feature to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017D7 RID: 6103 RVA: 0x000574F8 File Offset: 0x000556F8
		public static bool IsPresent(string featureClassName, string featureConstName)
		{
			IFeatureSupport featureSupport = FeatureSupport.FeatureObject(featureClassName);
			return featureSupport != null && featureSupport.IsPresent(featureConstName);
		}

		/// <summary>Determines whether the specified or newer version of the specified feature is installed in the system. This method is static.</summary>
		/// <returns>true if the feature is present and its version number is greater than or equal to the specified minimum version number; false if the feature is not installed or its version number is below the specified minimum number.</returns>
		/// <param name="featureClassName">The fully qualified name of the class to query for information about the specified feature. This class must implement the <see cref="T:System.Windows.Forms.IFeatureSupport" /> interface or inherit from a class that implements this interface. </param>
		/// <param name="featureConstName">The fully qualified name of the feature to look for. </param>
		/// <param name="minimumVersion">A <see cref="T:System.Version" /> representing the minimum version number of the feature. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017D8 RID: 6104 RVA: 0x0005751C File Offset: 0x0005571C
		public static bool IsPresent(string featureClassName, string featureConstName, Version minimumVersion)
		{
			IFeatureSupport featureSupport = FeatureSupport.FeatureObject(featureClassName);
			return featureSupport != null && featureSupport.IsPresent(featureConstName, minimumVersion);
		}

		/// <summary>When overridden in a derived class, gets the version of the specified feature that is available on the system.</summary>
		/// <returns>A <see cref="T:System.Version" /> representing the version number of the specified feature available on the system; or null if the feature is not installed.</returns>
		/// <param name="feature">The feature whose version is requested. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017D9 RID: 6105
		public abstract Version GetVersionPresent(object feature);

		/// <summary>Determines whether any version of the specified feature is installed in the system.</summary>
		/// <returns>true if the feature is present; otherwise, false.</returns>
		/// <param name="feature">The feature to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017DA RID: 6106 RVA: 0x00057540 File Offset: 0x00055740
		public virtual bool IsPresent(object feature)
		{
			return this.GetVersionPresent(feature) != null;
		}

		/// <summary>Determines whether the specified or newer version of the specified feature is installed in the system.</summary>
		/// <returns>true if the feature is present and its version number is greater than or equal to the specified minimum version number; false if the feature is not installed or its version number is below the specified minimum number.</returns>
		/// <param name="feature">The feature to look for. </param>
		/// <param name="minimumVersion">A <see cref="T:System.Version" /> representing the minimum version number of the feature to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017DB RID: 6107 RVA: 0x00057558 File Offset: 0x00055758
		public virtual bool IsPresent(object feature, Version minimumVersion)
		{
			bool flag = false;
			Version versionPresent = this.GetVersionPresent(feature);
			if (versionPresent != null && versionPresent >= minimumVersion)
			{
				flag = true;
			}
			return flag;
		}
	}
}
