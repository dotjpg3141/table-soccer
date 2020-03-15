using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableSoccer.Shared.Validations
{
	public sealed class CollectionCountAttribute : ValidationAttribute
	{
		private const int Unspecified = -1;

		public CollectionCountAttribute()
		{
			this.MinCount = Unspecified;
			this.MaxCount = Unspecified;
		}

		public CollectionCountAttribute(int count)
		{
			this.MinCount = count;
			this.MaxCount = count;
		}

		public int MinCount { get; set; }
		public int MaxCount { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return ValidationResult.Success;
			}

			var collection = (ICollection)value;

			if (MinCount != Unspecified && collection.Count < MinCount)
			{
				return new ValidationResult($"The collection must have at least {MinCount} elements.");
			}

			if (MaxCount != Unspecified && collection.Count > MaxCount)
			{
				return new ValidationResult($"The collection must have at most {MaxCount} elements.");
			}

			return ValidationResult.Success;
		}
	}
}