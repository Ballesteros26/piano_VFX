using System;

namespace System.Configuration
{
	/// <summary>Provides validation of a <see cref="T:System.TimeSpan" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000056 RID: 86
	public class PositiveTimeSpanValidator : ConfigurationValidatorBase
	{
		/// <summary>Determines whether the object type can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches a <see cref="T:System.TimeSpan" /> object; otherwise, false. </returns>
		/// <param name="type">The object type.</param>
		// Token: 0x060002E0 RID: 736 RVA: 0x000087F3 File Offset: 0x000069F3
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> cannot be resolved as a positive <see cref="T:System.TimeSpan" /> value.</exception>
		// Token: 0x060002E1 RID: 737 RVA: 0x00008805 File Offset: 0x00006A05
		public override void Validate(object value)
		{
			if ((TimeSpan)value <= new TimeSpan(0L))
			{
				throw new ArgumentException("The time span value must be positive");
			}
		}
	}
}
