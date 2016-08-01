using EstimatedBudget.Helpers;
using EstimatedBudget.POCO.DAL;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EstimatedBudget.Errors;
using EstimatedBudget.POCO.Models;


namespace EstimatedBudget.ViewModels.Levies
{
    public partial class LevyViewModel
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

        [RaisePropertyChanged]
        public virtual decimal Price { get; set; }

        [RaisePropertyChanged]
        public virtual string F_Code { get; set; }

        [RaisePropertyChanged]
        public virtual Nullable<int> C_Id { get; set; }

        [RaisePropertyChanged]
        public virtual int B_Code { get; set; }

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


            var validationContext = new ValidationContext(this, null, null);
            validationContext.MemberName = propertyName;

            return Validator.TryValidateProperty(propertyValue, validationContext, results) ?
                String.Empty : results[0].ErrorMessage;
        }

        public bool IsValid
        {
            get
            {
                
                foreach (string property in ProprertiesName.Levy)
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
            SpecsLevy = new Levy
            {
                Id = this.Id,
                Wording = this.Wording,
                Description = this.Description,
                DateL = this.DateL,
                Price = this.Price,
                F_Code = this.SelectedFrequency.Code,
                C_Id = this.SelectedCategory.Id,
                B_Code = Global.BankAccountCode,
                Category = this.SelectedCategory,
                Frequency = this.SelectedFrequency
            };
            
        }
    }
}
