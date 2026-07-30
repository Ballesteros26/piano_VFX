using System;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Serves as the base class for all validation attributes.</summary>
	/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">The <see cref="P:System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceType" /> and <see cref="P:System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceName" /> properties for localized error message are set at the same time that the non-localized <see cref="P:System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessage" /> property error message is set.</exception>
	// Token: 0x02000035 RID: 53
	public abstract class ValidationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" /> class.</summary>
		// Token: 0x0600012B RID: 299 RVA: 0x000047BC File Offset: 0x000029BC
		protected ValidationAttribute()
			: this(() => "The field {0} is invalid.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" /> class by using the error message to associate with a validation control.</summary>
		/// <param name="errorMessage">The error message to associate with a validation control.</param>
		// Token: 0x0600012C RID: 300 RVA: 0x000047E4 File Offset: 0x000029E4
		protected ValidationAttribute(string errorMessage)
			: this(() => errorMessage)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" /> class by using the function that enables access to validation resources.</summary>
		/// <param name="errorMessageAccessor">The function that enables access to validation resources.</param>
		/// <exception cref="T:System:ArgumentNullException">
		///   <paramref name="errorMessageAccessor" /> is null.</exception>
		// Token: 0x0600012D RID: 301 RVA: 0x00004810 File Offset: 0x00002A10
		protected ValidationAttribute(Func<string> errorMessageAccessor)
		{
			this._errorMessageResourceAccessor = errorMessageAccessor;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000481F File Offset: 0x00002A1F
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00004827 File Offset: 0x00002A27
		internal string DefaultErrorMessage
		{
			get
			{
				return this._defaultErrorMessage;
			}
			set
			{
				this._defaultErrorMessage = value;
				this._errorMessageResourceAccessor = null;
				this.CustomErrorMessageSet = true;
			}
		}

		/// <summary>Gets the localized validation error message.</summary>
		/// <returns>The localized validation error message.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000483E File Offset: 0x00002A3E
		protected string ErrorMessageString
		{
			get
			{
				this.SetupResourceAccessor();
				return this._errorMessageResourceAccessor();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004851 File Offset: 0x00002A51
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00004859 File Offset: 0x00002A59
		internal bool CustomErrorMessageSet { get; private set; }

		/// <summary>Gets a value that indicates whether the attribute requires validation context.</summary>
		/// <returns>true if the attribute requires validation context; otherwise, false.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004862 File Offset: 0x00002A62
		public virtual bool RequiresValidationContext
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets an error message to associate with a validation control if validation fails.</summary>
		/// <returns>The error message that is associated with the validation control.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004865 File Offset: 0x00002A65
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00004877 File Offset: 0x00002A77
		public string ErrorMessage
		{
			get
			{
				return this._errorMessage ?? this._defaultErrorMessage;
			}
			set
			{
				this._errorMessage = value;
				this._errorMessageResourceAccessor = null;
				this.CustomErrorMessageSet = true;
				if (value == null)
				{
					this._defaultErrorMessage = null;
				}
			}
		}

		/// <summary>Gets or sets the error message resource name to use in order to look up the <see cref="P:System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceType" /> property value if validation fails.</summary>
		/// <returns>The error message resource that is associated with a validation control.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004898 File Offset: 0x00002A98
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000048A0 File Offset: 0x00002AA0
		public string ErrorMessageResourceName
		{
			get
			{
				return this._errorMessageResourceName;
			}
			set
			{
				this._errorMessageResourceName = value;
				this._errorMessageResourceAccessor = null;
				this.CustomErrorMessageSet = true;
			}
		}

		/// <summary>Gets or sets the resource type to use for error-message lookup if validation fails.</summary>
		/// <returns>The type of error message that is associated with a validation control.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000048B7 File Offset: 0x00002AB7
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000048BF File Offset: 0x00002ABF
		public Type ErrorMessageResourceType
		{
			get
			{
				return this._errorMessageResourceType;
			}
			set
			{
				this._errorMessageResourceType = value;
				this._errorMessageResourceAccessor = null;
				this.CustomErrorMessageSet = true;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000048D8 File Offset: 0x00002AD8
		private void SetupResourceAccessor()
		{
			if (this._errorMessageResourceAccessor == null)
			{
				string localErrorMessage = this.ErrorMessage;
				bool flag = !string.IsNullOrEmpty(this._errorMessageResourceName);
				bool flag2 = !string.IsNullOrEmpty(this._errorMessage);
				bool flag3 = this._errorMessageResourceType != null;
				bool flag4 = !string.IsNullOrEmpty(this._defaultErrorMessage);
				if ((flag && flag2) || (!flag && !flag2 && !flag4))
				{
					throw new InvalidOperationException("Either ErrorMessageString or ErrorMessageResourceName must be set, but not both.");
				}
				if (flag3 != flag)
				{
					throw new InvalidOperationException("Both ErrorMessageResourceType and ErrorMessageResourceName need to be set on this attribute.");
				}
				if (flag)
				{
					this.SetResourceAccessorByPropertyLookup();
					return;
				}
				this._errorMessageResourceAccessor = () => localErrorMessage;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000497C File Offset: 0x00002B7C
		private void SetResourceAccessorByPropertyLookup()
		{
			if (!(this._errorMessageResourceType != null) || string.IsNullOrEmpty(this._errorMessageResourceName))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Both ErrorMessageResourceType and ErrorMessageResourceName need to be set on this attribute.", Array.Empty<object>()));
			}
			PropertyInfo property = this._errorMessageResourceType.GetProperty(this._errorMessageResourceName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (property != null)
			{
				MethodInfo getMethod = property.GetGetMethod(true);
				if (getMethod == null || (!getMethod.IsAssembly && !getMethod.IsPublic))
				{
					property = null;
				}
			}
			if (property == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The resource type '{0}' does not have an accessible static property named '{1}'.", this._errorMessageResourceType.FullName, this._errorMessageResourceName));
			}
			if (property.PropertyType != typeof(string))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The property '{0}' on resource type '{1}' is not a string type.", property.Name, this._errorMessageResourceType.FullName));
			}
			this._errorMessageResourceAccessor = () => (string)property.GetValue(null, null);
		}

		/// <summary>Applies formatting to an error message, based on the data field where the error occurred. </summary>
		/// <returns>An instance of the formatted error message.</returns>
		/// <param name="name">The name to include in the formatted message.</param>
		// Token: 0x0600013C RID: 316 RVA: 0x00004AAB File Offset: 0x00002CAB
		public virtual string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString, name);
		}

		/// <summary>Determines whether the specified value of the object is valid. </summary>
		/// <returns>true if the specified value is valid; otherwise, false.</returns>
		/// <param name="value">The value of the object to validate. </param>
		// Token: 0x0600013D RID: 317 RVA: 0x00004ABE File Offset: 0x00002CBE
		public virtual bool IsValid(object value)
		{
			if (!this._hasBaseIsValid)
			{
				this._hasBaseIsValid = true;
			}
			return this.IsValid(value, null) == null;
		}

		/// <summary>Validates the specified value with respect to the current validation attribute.</summary>
		/// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class. </returns>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context information about the validation operation.</param>
		// Token: 0x0600013E RID: 318 RVA: 0x00004AE0 File Offset: 0x00002CE0
		protected virtual ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (this._hasBaseIsValid)
			{
				throw new NotImplementedException("IsValid(object value) has not been implemented by this class.  The preferred entry point is GetValidationResult() and classes should override IsValid(object value, ValidationContext context).");
			}
			ValidationResult validationResult = ValidationResult.Success;
			if (!this.IsValid(value))
			{
				object obj;
				if (validationContext.MemberName == null)
				{
					obj = null;
				}
				else
				{
					(obj = new string[1])[0] = validationContext.MemberName;
				}
				string[] array = obj;
				validationResult = new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName), array);
			}
			return validationResult;
		}

		/// <summary>Checks whether the specified value is valid with respect to the current validation attribute.</summary>
		/// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class. </returns>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context information about the validation operation.</param>
		// Token: 0x0600013F RID: 319 RVA: 0x00004B40 File Offset: 0x00002D40
		public ValidationResult GetValidationResult(object value, ValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			ValidationResult validationResult = this.IsValid(value, validationContext);
			if (validationResult != null && (validationResult == null || string.IsNullOrEmpty(validationResult.ErrorMessage)))
			{
				validationResult = new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName), validationResult.MemberNames);
			}
			return validationResult;
		}

		/// <summary>Validates the specified object.</summary>
		/// <param name="value">The value of the object to validate.</param>
		/// <param name="name">The name to include in the error message.</param>
		/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">
		///   <paramref name="value" /> is not valid.</exception>
		// Token: 0x06000140 RID: 320 RVA: 0x00004B96 File Offset: 0x00002D96
		public void Validate(object value, string name)
		{
			if (!this.IsValid(value))
			{
				throw new ValidationException(this.FormatErrorMessage(name), this, value);
			}
		}

		/// <summary>Validates the specified object.</summary>
		/// <param name="value">The object to validate.</param>
		/// <param name="validationContext">The <see cref="T:System.ComponentModel.DataAnnotations.ValidationContext" /> object that describes the context where the validation checks are performed. This parameter cannot be null.</param>
		/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">Validation failed.</exception>
		// Token: 0x06000141 RID: 321 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public void Validate(object value, ValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			ValidationResult validationResult = this.GetValidationResult(value, validationContext);
			if (validationResult != null)
			{
				throw new ValidationException(validationResult, this, value);
			}
		}

		// Token: 0x040000AA RID: 170
		private string _errorMessage;

		// Token: 0x040000AB RID: 171
		private Func<string> _errorMessageResourceAccessor;

		// Token: 0x040000AC RID: 172
		private string _errorMessageResourceName;

		// Token: 0x040000AD RID: 173
		private Type _errorMessageResourceType;

		// Token: 0x040000AE RID: 174
		private string _defaultErrorMessage;

		// Token: 0x040000AF RID: 175
		private volatile bool _hasBaseIsValid;
	}
}
