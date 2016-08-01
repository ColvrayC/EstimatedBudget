using EstimatedBudget.Helpers;
using EstimatedBudget.POCO.DAL;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EstimatedBudget.Errors;
using EstimatedBudget.POCO.Models;

namespace EstimatedBudget.ViewModels.Categories
{
    public partial class CategoryViewModel
    { 

    /// <summary>
    /// SPECS PROPERTIES
    /// </summary>
    /// 
    [RaisePropertyChanged]
    public virtual Nullable<int> Id { get; set; }

    [Required(ErrorMessage = ErrorsMessages.Required)]
    [RaisePropertyChanged]
    public virtual string Wording { get; set; }

    [Required(ErrorMessage = ErrorsMessages.Required)]
    [RaisePropertyChanged]
    public virtual decimal Targetprice { get; set; }

    [RaisePropertyChanged]
    public virtual int B_Code { get; set; }

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
            //Replace Properties Specs to an Object for improve Reading
            SpecsPropertiesToObject();
            foreach (string property in ProprertiesName.Category)
            {
                if (OnValidate(property) != "")
                    return false;
            }
            return true;
        }
    }

    public void SpecsPropertiesToObject()
    {
        SpecsCategory = new Category
        {
            Id = this.Id,
            Wording = this.Wording,
            Targetprice = this.Targetprice,
            B_Code = Global.BankAccountCode
        };
    }
}
}
