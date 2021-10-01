// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="Catel development team">
//   Copyright (c) 2008 - 2017 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Catel.Data;

namespace CatelWPFApplication1.Models
{
    [Serializable]
    public class Person : ValidatableModelBase
    {
        #region Constructors
        public Person()
        {
        }

#if !NETFX_CORE
        protected Person(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
        #endregion

        #region Properties
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
        
        #endregion

        #region Methods
        protected override void ValidateFields(List<IFieldValidationResult> validationResults)
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                validationResults.Add(FieldValidationResult.CreateError(nameof(FirstName), "First name is required"));
            }

            if (string.IsNullOrEmpty(LastName))
            {
                validationResults.Add(FieldValidationResult.CreateError(nameof(LastName), "Last name is required"));
            }

        }
        #endregion
    }

}