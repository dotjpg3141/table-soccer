// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See https://www.apache.org/licenses/LICENSE-2.0 for license information.
// Modified from https://github.com/dotnet/aspnetcore/blob/release/3.1/src/Components/Blazor/Validation/src/ValidateComplexTypeAttribute.cs

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableSoccer.Shared.Validations
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class ValidateComplexTypeAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			ObjectGraphDataAnnotationsValidator.TryValidateRecursive(value, validationContext);
			return ValidationResult.Success;
		}
	}
}