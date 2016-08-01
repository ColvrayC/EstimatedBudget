using EstimatedBudget.Helpers;
using EstimatedBudget.POCO.DAL;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EstimatedBudget.Errors;
using EstimatedBudget.POCO.Models;

namespace EstimatedBudget.ViewModels.BankAccounts
{
    public partial class BankAccountViewModel
    {
        /// <summary>
        /// SPECS PROPERTIES
        /// </summary>
        /// 
        [RaisePropertyChanged]
        public virtual int Code { get; set; }
        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual string Wording { get; set; }

        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual string Description { get; set; }

        [RaisePropertyChanged]
        public virtual bool Investment { get; set; }

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

            if(propertyName == "Code")
            {
                if ((int)propertyValue == 0 || propertyValue.ToString() == string.Empty ||propertyValue == null)
                    return ErrorsMessages.Required;

                //Check if the valueProperty is a number and doesn't already exist
                if (!ErrorsValidation.IsNumber(propertyValue))
                    return ErrorsMessages.Int;

                //Check if the valueProperty(Code) is already Exist in ADD Mode
                if (ActiveMode == Modes.ADD & BankAccountDAL.IsCodeExistPK(propertyValue.ToString()))
                    return ErrorsMessages.IsExist;
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
                foreach (string property in ProprertiesName.BankAccount)
                {
                    if (OnValidate(property) != "")
                        return false;
                }
                return true;
            }
        }

        public void SpecsPropertiesToObject()
        {
            SpecsBankAccount = new BankAccount
            {
                Code = this.Code,
                Wording = this.Wording,
                Description = this.Description,
                Investment = this.Investment
            };
        }
    }
}
