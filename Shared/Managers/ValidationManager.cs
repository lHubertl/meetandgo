﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MeetAndGo.Shared.Managers
{
    public class ValidationManager
    {
        public bool IsValid { get; set; }
        public IList<string> Errors { get; set; }

        public ValidationManager()
        {
            IsValid = true;
            Errors = new List<string>(); 
        }

        public static ValidationManager Create()
        {
            return new ValidationManager();
        }

        public ValidationManager Validate(Func<bool> expression, string error)
        {
            if (expression is null)
            {
                Errors.Add(error);
                IsValid = false;
                return this;
            }

            var result = expression.Invoke();
            if (!result)
            {
                Errors.Add(error);
            }

            if (IsValid == true && result == false)
            {
                IsValid = false;
            }

            return this;
        }

        public ValidationManager ValidatePhoneNumber(string value, string error)
        {
            if (value is null)
            {
                Errors.Add(error);
                IsValid = false;
                return this;
            }

            // TODO: check and update mask
            var mask = @"^[+]?([0-9]){3}[ ]?([0-9]){4}[ ]?([0-9]){2}[ ]?([0-9]){3}$";
            return Validate(() => Regex.IsMatch(value, mask), error);
        }

        public ValidationManager ValidatePassword(string value, string error)
        {
            if (value is null)
            {
                Errors.Add(error);
                IsValid = false;
                return this;
            }

            // Minimum six characters, at least one letter and one number, could be one special character
            var mask = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*#?&?]{6,}$";
            return Validate(() => Regex.IsMatch(value, mask), error);
        }

        public ValidationManager ValidateSmsCode(string value, string error)
        {
            if (value is null)
            {
                Errors.Add(error);
                IsValid = false;
                return this;
            }

            // Only six digits
            var mask = @"^[0-9, ]{6,11}$";
            return Validate(() => Regex.IsMatch(value, mask), error);
        }

        public ValidationManager ValidateEmail(string value, string error)
        {
            if (value is null)
            {
                Errors.Add(error);
                IsValid = false;
                return this;
            }

            // Only six digits
            var mask = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            return Validate(() => Regex.IsMatch(value, mask), error);
        }

        public override string ToString()
        {
            if (Errors == null)
            {
                return string.Empty;
            }

            return string.Join("\n", Errors);
        }
    }
}
