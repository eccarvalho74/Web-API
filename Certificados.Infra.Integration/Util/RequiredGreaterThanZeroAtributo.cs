﻿using System.ComponentModel.DataAnnotations;

namespace Certificados.Infra.Integration.Util
{
    public class RequiredGreaterThanZeroAtributo : ValidationAttribute
    {
        /// <summary>
        /// Designed for dropdowns to ensure that a selection is valid and not the dummy "SELECT" entry
        /// </summary>
        /// <param name="value">The integer value of the selection</param>
        /// <returns>True if value is greater than zero</returns>
        public override bool IsValid(object value)
        {
            // return true if value is a non-null number > 0, otherwise return false
            int i;
            return value != null && int.TryParse(value.ToString(), out i) && i > 0;
        }
    }
}
