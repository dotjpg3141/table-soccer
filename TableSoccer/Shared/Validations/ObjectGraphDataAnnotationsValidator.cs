﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See https://www.apache.org/licenses/LICENSE-2.0 for license information.
// Modified from https://github.com/dotnet/aspnetcore/blob/release/3.1/src/Components/Blazor/Validation/src/ObjectGraphDataAnnotationsValidator.cs

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace TableSoccer.Shared.Validations
{
	public class ObjectGraphDataAnnotationsValidator : ComponentBase
	{
		private static readonly object ValidationContextValidatorKey = new object();
		private static readonly object ValidatedObjectsKey = new object();
		private ValidationMessageStore _validationMessageStore;

		[CascadingParameter]
		internal EditContext EditContext { get; set; }

		public static bool TryValidateRecursive(object value, ValidationContext validationContext)
		{
			if (validationContext.Items.TryGetValue(ValidationContextValidatorKey, out var result) && result is ObjectGraphDataAnnotationsValidator validator)
			{
				var visited = (HashSet<object>)validationContext.Items[ValidatedObjectsKey];
				validator.ValidateObject(value, visited);

				return true;
			}

			return false;
		}

		internal void ValidateObject(object value, HashSet<object> visited)
		{
			if (value is null)
			{
				return;
			}

			if (!visited.Add(value))
			{
				// Already visited this object.
				return;
			}

			Console.WriteLine($"Validating {value}");

			if (value is IEnumerable<object> enumerable)
			{
				Console.WriteLine($"Validating enumerable {value}");

				foreach (var item in enumerable)
				{
					ValidateObject(item, visited);
				}

				return;
			}

			var validationResults = new List<ValidationResult>();
			ValidateObject(value, visited, validationResults);

			// Transfer results to the ValidationMessageStore
			foreach (var validationResult in validationResults)
			{
				if (!validationResult.MemberNames.Any())
				{
					_validationMessageStore.Add(new FieldIdentifier(value, string.Empty), validationResult.ErrorMessage);
					continue;
				}

				foreach (var memberName in validationResult.MemberNames)
				{
					var fieldIdentifier = new FieldIdentifier(value, memberName);
					_validationMessageStore.Add(fieldIdentifier, validationResult.ErrorMessage);
				}
			}
		}

		protected override void OnInitialized()
		{
			_validationMessageStore = new ValidationMessageStore(EditContext);

			// Perform object-level validation (starting from the root model) on request
			EditContext.OnValidationRequested += (sender, eventArgs) =>
			{
				_validationMessageStore.Clear();
				ValidateObject(EditContext.Model, new HashSet<object>());
				EditContext.NotifyValidationStateChanged();
			};

			// Perform per-field validation on each field edit
			EditContext.OnFieldChanged += (sender, eventArgs) =>
				ValidateField(EditContext, _validationMessageStore, eventArgs.FieldIdentifier);
		}

		private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier)
		{
			// DataAnnotations only validates public properties, so that's all we'll look for
			var propertyInfo = fieldIdentifier.Model.GetType().GetProperty(fieldIdentifier.FieldName);
			if (propertyInfo != null)
			{
				var propertyValue = propertyInfo.GetValue(fieldIdentifier.Model);
				var validationContext = new ValidationContext(fieldIdentifier.Model)
				{
					MemberName = propertyInfo.Name
				};
				var results = new List<ValidationResult>();

				Validator.TryValidateProperty(propertyValue, validationContext, results);
				messages.Clear(fieldIdentifier);
				messages.Add(fieldIdentifier, results.Select(result => result.ErrorMessage));

				// We have to notify even if there were no messages before and are still no messages now,
				// because the "state" that changed might be the completion of some async validation task
				editContext.NotifyValidationStateChanged();
			}
		}

		private void ValidateObject(object value, HashSet<object> visited, List<ValidationResult> validationResults)
		{
			var validationContext = new ValidationContext(value);
			validationContext.Items.Add(ValidationContextValidatorKey, this);
			validationContext.Items.Add(ValidatedObjectsKey, visited);
			Validator.TryValidateObject(value, validationContext, validationResults, validateAllProperties: true);
		}
	}
}