﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;

namespace TableSoccer.Client.Components
{
	public class InputSelectNumber<T> : InputSelect<T>
	{
		protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
		{
			if (typeof(T) == typeof(int))
			{
				if (int.TryParse(value, out var resultInt))
				{
					result = (T)(object)resultInt;
					validationErrorMessage = null;
					return true;
				}
				else
				{
					result = default;
					validationErrorMessage = "The chosen value is not a valid number.";
					return false;
				}
			}
			else if (typeof(T) == typeof(long))
			{
				if (long.TryParse(value, out var resultLong))
				{
					result = (T)(object)resultLong;
					validationErrorMessage = null;
					return true;
				}
				else
				{
					result = default;
					validationErrorMessage = "The chosen value is not a valid number.";
					return false;
				}
			}
			else
			{
				return base.TryParseValueFromString(value, out result, out validationErrorMessage);
			}
		}
	}
}