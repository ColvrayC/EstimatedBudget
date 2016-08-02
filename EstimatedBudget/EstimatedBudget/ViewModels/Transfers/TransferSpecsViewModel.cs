using EstimatedBudget.Helpers;
using EstimatedBudget.POCO.DAL;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EstimatedBudget.Errors;
using EstimatedBudget.POCO.Models;


namespace EstimatedBudget.ViewModels.Transfers
{
    public partial class TransferViewModel
    {
        /// <summary>
        /// SPECS PROPERTIES
        /// </summary>
        /// 
        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual string Wording { get; set; }

        [RaisePropertyChanged]
        public virtual string Description { get; set; }

        [Range(-99999.99, 99999.99, ErrorMessage = ErrorsMessages.Euro)]
        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual decimal Price { get; set; }

        [RaisePropertyChanged]
        public virtual bool Way { get; set; }

        [RaisePropertyChanged]
        public virtual string F_Code { get; set; }

        [RaisePropertyChanged]
        public virtual Nullable<int> C_Id { get; set; }

        [RaisePropertyChanged]
        public virtual int B_Code { get; set; }

        [RaisePropertyChanged]
        public virtual Nullable<int> B_CodeBeneficiary { get; set; }


        [RaisePropertyChanged]
        public virtual string Beneficiary { get; set; }

        [RaisePropertyChanged]
        public virtual DateTime DateL { get; set; }

        [Required(ErrorMessage = ErrorsMessages.Required)]
        [RaisePropertyChanged]
        public virtual Frequency SelectedFrequency { get; set; }

        [Required(ErrorMessage = ErrorsMessages.Required)]
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
            if (propertyName == "F_Code")
            {
                if (SelectedFrequency == null)
                    return ErrorsMessages.Required;
            }

            if (propertyName == "C_Id")
            {
                if (SelectedCategory == null)
                    return ErrorsMessages.Required;
            }

            if (propertyName == "RdoBeneficiary")
            {
                if (RdoBeneficiary && (Beneficiary == null || (string)Beneficiary == string.Empty))
                {
                    Beneficiary = null;
                }
            }

            if (propertyName == "Beneficiary")
            {
                if (RdoBeneficiary && (propertyValue==null || (string)propertyValue==string.Empty))
                    return ErrorsMessages.Required;
            }

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
                
                foreach (string property in ProprertiesName.Transfer)
                {
                    if (OnValidate(property) != "")
                        return false;
                }
                //Replace Properties Specs to an Object for improve Reading
                SpecsPropertiesToObject();
                return true;
            }
        }

        public void SpecsPropertiesToObject()
        {
            SpecsTransfer = new Transfer
            {
                Id = this.Id,
                Wording = this.Wording,
                Description = this.Description,
                DateL = this.DateL,
                Price = this.Price,
                Way = this.Way,
                B_CodeBeneficiary = this.B_CodeBeneficiary,
                Beneficiary = this.Beneficiary,
                F_Code = this.SelectedFrequency.Code,
                C_Id = this.SelectedCategory.Id,
                B_Code = Global.BankAccountCode,
                Category = this.SelectedCategory,
                Frequency = this.SelectedFrequency
            };
            
        }
    }
}
