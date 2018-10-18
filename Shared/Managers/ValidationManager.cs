using System;
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

        public ValidationManager ValidatePhoneNumber(string phoneNumber, string error)
        {
            var mask = @"^[+]?[\\(]{0,1}([0-9]){3}[\\)]{0,1}[ ]?([^0-1]){1}([0-9]){2}[ ]?[-]?[ ]?([0-9]){4}[ ]*((x){0,1}([0-9]){1,5}){0,1}$";
            return Validate(() => Regex.IsMatch(phoneNumber, mask), error);
        }
    }
}
