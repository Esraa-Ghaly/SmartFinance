using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFinance.Core.DTOs.Auth
{
    internal class ResetPasswordRequest
   
       
    {
        
        
            public string Email { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
        }
    }


