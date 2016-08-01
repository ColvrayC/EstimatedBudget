using System;
using System.ComponentModel.DataAnnotations;

namespace EstimatedBudget.Errors
{
    public class Euro : ValidationAttribute
    {
        private decimal Property;
        public Euro()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Property = (decimal)value;
            if (Property == null)
            {
                return new ValidationResult(
                    string.Format("La valeure doit être au format Euro.")
                );
            }
           // var otherValue = property.GetValue(validationContext.ObjectInstance, null);

            // at this stage you have "value" and "otherValue" pointing
            // to the value of the property on which this attribute
            // is applied and the value of the other property respectively
            // => you could do some checks
          /*  if (!object.Equals(value, otherValue))
            {
                // here we are verifying whether the 2 values are equal
                // but you could do any custom validation you like
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }*/
            return null;

          
        }
        protected bool EuroValue()
        {

            return true;
        }
    }
}
