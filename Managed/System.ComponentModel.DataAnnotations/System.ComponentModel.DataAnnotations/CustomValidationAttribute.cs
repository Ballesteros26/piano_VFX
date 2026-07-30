using System;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies a custom validation method that is used to validate a property or class instance.</summary>
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
	public sealed class CustomValidationAttribute : ValidationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.CustomValidationAttribute" /> class.</summary>
		/// <param name="validatorType">The type that contains the method that performs custom validation.</param>
		/// <param name="method">The method that performs custom validation.</param>
		// Token: 0x06000031 RID: 49 RVA: 0x00002734 File Offset: 0x00000934
		public CustomValidationAttribute(Type validatorType, string method)
			: base(() => "{0} is not valid.")
		{
			this._validatorType = validatorType;
			this._method = method;
			this._malformedErrorMessage = new Lazy<string>(new Func<string>(this.CheckAttributeWellFormed));
		}

		/// <summary>Gets the type that performs custom validation.</summary>
		/// <returns>The type that performs custom validation.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000278B File Offset: 0x0000098B
		public Type ValidatorType
		{
			get
			{
				return this._validatorType;
			}
		}

		/// <summary>Gets the validation method.</summary>
		/// <returns>The name of the validation method.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002793 File Offset: 0x00000993
		public string Method
		{
			get
			{
				return this._method;
			}
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>The object that identifies this attribute.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000279B File Offset: 0x0000099B
		public override object TypeId
		{
			get
			{
				if (this._typeId == null)
				{
					this._typeId = new Tuple<string, Type>(this._method, this._validatorType);
				}
				return this._typeId;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027C4 File Offset: 0x000009C4
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			this.ThrowIfAttributeNotWellFormed();
			MethodInfo methodInfo = this._methodInfo;
			object obj;
			if (!this.TryConvertValue(value, out obj))
			{
				return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Could not convert the value of type '{0}' to '{1}' as expected by method {2}.{3}.", new object[]
				{
					(value != null) ? value.GetType().ToString() : "null",
					this._valuesType,
					this._validatorType,
					this._method
				}));
			}
			ValidationResult validationResult2;
			try
			{
				object[] array2;
				if (!this._isSingleArgumentMethod)
				{
					object[] array = new object[2];
					array[0] = obj;
					array2 = array;
					array[1] = validationContext;
				}
				else
				{
					(array2 = new object[1])[0] = obj;
				}
				object[] array3 = array2;
				ValidationResult validationResult = (ValidationResult)methodInfo.Invoke(null, array3);
				this._lastMessage = null;
				if (validationResult != null)
				{
					this._lastMessage = validationResult.ErrorMessage;
				}
				validationResult2 = validationResult;
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return validationResult2;
		}

		/// <summary>Formats a validation error message.</summary>
		/// <returns>An instance of the formatted error message.</returns>
		/// <param name="name">The name to include in the formatted message.</param>
		// Token: 0x06000036 RID: 54 RVA: 0x000028AC File Offset: 0x00000AAC
		public override string FormatErrorMessage(string name)
		{
			this.ThrowIfAttributeNotWellFormed();
			if (!string.IsNullOrEmpty(this._lastMessage))
			{
				return string.Format(CultureInfo.CurrentCulture, this._lastMessage, name);
			}
			return base.FormatErrorMessage(name);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028DA File Offset: 0x00000ADA
		private string CheckAttributeWellFormed()
		{
			return this.ValidateValidatorTypeParameter() ?? this.ValidateMethodParameter();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028EC File Offset: 0x00000AEC
		private string ValidateValidatorTypeParameter()
		{
			if (this._validatorType == null)
			{
				return "The CustomValidationAttribute.ValidatorType was not specified.";
			}
			if (!this._validatorType.IsVisible)
			{
				return string.Format(CultureInfo.CurrentCulture, "The custom validation type '{0}' must be public.", this._validatorType.Name);
			}
			return null;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000292C File Offset: 0x00000B2C
		private string ValidateMethodParameter()
		{
			if (string.IsNullOrEmpty(this._method))
			{
				return "The CustomValidationAttribute.Method was not specified.";
			}
			MethodInfo method = this._validatorType.GetMethod(this._method, BindingFlags.Static | BindingFlags.Public);
			if (method == null)
			{
				return string.Format(CultureInfo.CurrentCulture, "The CustomValidationAttribute method '{0}' does not exist in type '{1}' or is not public and static.", this._method, this._validatorType.Name);
			}
			if (method.ReturnType != typeof(ValidationResult))
			{
				return string.Format(CultureInfo.CurrentCulture, "The CustomValidationAttribute method '{0}' in type '{1}' must return System.ComponentModel.DataAnnotations.ValidationResult.  Use System.ComponentModel.DataAnnotations.ValidationResult.Success to represent success.", this._method, this._validatorType.Name);
			}
			ParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length == 0 || parameters[0].ParameterType.IsByRef)
			{
				return string.Format(CultureInfo.CurrentCulture, "The CustomValidationAttribute method '{0}' in type '{1}' must match the expected signature: public static ValidationResult {0}(object value, ValidationContext context).  The value can be strongly typed.  The ValidationContext parameter is optional.", this._method, this._validatorType.Name);
			}
			this._isSingleArgumentMethod = parameters.Length == 1;
			if (!this._isSingleArgumentMethod && (parameters.Length != 2 || parameters[1].ParameterType != typeof(ValidationContext)))
			{
				return string.Format(CultureInfo.CurrentCulture, "The CustomValidationAttribute method '{0}' in type '{1}' must match the expected signature: public static ValidationResult {0}(object value, ValidationContext context).  The value can be strongly typed.  The ValidationContext parameter is optional.", this._method, this._validatorType.Name);
			}
			this._methodInfo = method;
			this._valuesType = parameters[0].ParameterType;
			return null;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A68 File Offset: 0x00000C68
		private void ThrowIfAttributeNotWellFormed()
		{
			string value = this._malformedErrorMessage.Value;
			if (value != null)
			{
				throw new InvalidOperationException(value);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A8C File Offset: 0x00000C8C
		private bool TryConvertValue(object value, out object convertedValue)
		{
			convertedValue = null;
			Type valuesType = this._valuesType;
			if (value == null)
			{
				return !valuesType.IsValueType || (valuesType.IsGenericType && !(valuesType.GetGenericTypeDefinition() != typeof(Nullable<>)));
			}
			if (valuesType.IsAssignableFrom(value.GetType()))
			{
				convertedValue = value;
				return true;
			}
			bool flag;
			try
			{
				convertedValue = Convert.ChangeType(value, valuesType, CultureInfo.CurrentCulture);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			catch (InvalidCastException)
			{
				flag = false;
			}
			catch (NotSupportedException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400003D RID: 61
		private Type _validatorType;

		// Token: 0x0400003E RID: 62
		private string _method;

		// Token: 0x0400003F RID: 63
		private MethodInfo _methodInfo;

		// Token: 0x04000040 RID: 64
		private bool _isSingleArgumentMethod;

		// Token: 0x04000041 RID: 65
		private string _lastMessage;

		// Token: 0x04000042 RID: 66
		private Type _valuesType;

		// Token: 0x04000043 RID: 67
		private Lazy<string> _malformedErrorMessage;

		// Token: 0x04000044 RID: 68
		private Tuple<string, Type> _typeId;
	}
}
