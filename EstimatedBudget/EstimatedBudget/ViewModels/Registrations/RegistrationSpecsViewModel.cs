using EstimatedBudget.Helpers;
using EstimatedBudget.POCO.DAL;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EstimatedBudget.Errors;
using EstimatedBudget.POCO.Models;


namespace EstimatedBudget.ViewModels.Registrations
{
    public partial class RegistrationViewModel
    {
        /// <summary>
        /// SPECS PROPERTIES
        /// </summary>
        /// 
        [RaisePropertyChanged]
        public virtual int? Id { get; set; }

        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual string Wording { get; set; }

        [RaisePropertyChanged]
        public virtual string Description { get; set; }

        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual DateTime DateR { get; set; }

      
        [Range(-99999.99,99999.99,ErrorMessage = ErrorsMessages.Euro)]
        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual decimal Price { get; set; }


        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual Nullable<int> C_Id { get; set; }


        [RaisePropertyChanged]
        public virtual int? L_Id { get; set; }

        [RaisePropertyChanged]
        public virtual int B_Code { get; set; }

        [Required(ErrorMessage =ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual Category SelectedCategory { get; set; }

        /// <summary>
        /// ERRORS
        /// </summary>
        /// 
        private string error = string.Empty;
        public string Error { get { return this.error; } }

        public string this[string propertyName]
        {

            get { return OnValidate(propertyName); }
        }


        protected virtual string OnValidate(string propertyName)
        {
            Type objectType = GetType();
            PropertyInfo propertyInfo = objectType.GetProperty(propertyName);
            object propertyValue = propertyInfo.GetValue(this, null);

            var results = new List<ValidationResult>();

            if (propertyName == "Price")
            {
                if ((decimal)propertyValue == (decimal)0.00M)
                    return ErrorsMessages.Required;
            }

            var validationContext = new ValidationContext(this, null, null);
            validationContext.MemberName = propertyName;

            return Validator.TryValidateProperty(propertyValue, validationContext, results) ?
                String.Empty : results[0].ErrorMessage;
        }

        public bool IsValid
        {
            get
            {
                //Replace Properties Specs to an Object for improve Reading
                SpecsPropertiesToObject();
                foreach (string property in ProprertiesName.Registration)
                {
                    if (OnValidate(property) != "")
                        return false;
                }
                
                return true;
            }
        }

        public void SpecsPropertiesToObject()
        {
            if (SelectedCategory == null)
                SelectedCategory = new Category();

            SpecsRegistration = new Registration
            {
                Id = this.Id,
                Wording = this.Wording,
                Description = this.Description,
                DateR = this.DateR,
                Price = this.Price,
                B_Code = Global.BankAccountCode,
                C_Id = this.SelectedCategory.Id,
                L_Id = this.L_Id,
                Category = this.SelectedCategory
            };
        }
    }
}
