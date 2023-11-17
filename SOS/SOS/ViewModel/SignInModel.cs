#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.DataForm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
namespace SOS.ViewModel
{
    public class SignInModel
    {

        public SignInModel() 
        {
            this.Username = string.Empty;
            this.Password = string.Empty;
        }
        #region Property
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username should not be empty")]
        public string Username { get; set; }

        
        [Display(Name = "Password", Prompt = "Enter your password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter the password")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Password length muste be greater then 4 characters")]
        public string Password { get; set; }
        #endregion
    }
}
