using System;
using System.Collections.Specialized;
using System.Reflection;

namespace System.Configuration
{
	/// <summary>Provides a method for reading values of a particular type from the configuration.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000160 RID: 352
	public class AppSettingsReader
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.AppSettingsReader" /> class.</summary>
		// Token: 0x06000AAD RID: 2733 RVA: 0x00038855 File Offset: 0x00036A55
		public AppSettingsReader()
		{
			this.appSettings = ConfigurationSettings.AppSettings;
		}

		/// <summary>Gets the value for a specified key from the <see cref="P:System.Configuration.ConfigurationSettings.AppSettings" /> property and returns an object of the specified type containing the value from the configuration.</summary>
		/// <returns>The value of the specified key.</returns>
		/// <param name="key">The key for which to get the value.</param>
		/// <param name="type">The type of the object to return.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.- or -<paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="key" /> does not exist in the &lt;appSettings&gt; configuration section.- or -The value in the &lt;appSettings&gt; configuration section for <paramref name="key" /> is not of type <paramref name="type" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AAE RID: 2734 RVA: 0x00038868 File Offset: 0x00036A68
		public object GetValue(string key, Type type)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string text = this.appSettings[key];
			if (text == null)
			{
				throw new InvalidOperationException("'" + key + "' could not be found.");
			}
			if (type == typeof(string))
			{
				return text;
			}
			MethodInfo method = type.GetMethod("Parse", new Type[] { typeof(string) });
			if (method == null)
			{
				throw new InvalidOperationException("Type " + type + " does not have a Parse method");
			}
			object obj = null;
			try
			{
				obj = method.Invoke(null, new object[] { text });
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Parse error.", ex);
			}
			return obj;
		}

		// Token: 0x04000F6B RID: 3947
		private NameValueCollection appSettings;
	}
}
