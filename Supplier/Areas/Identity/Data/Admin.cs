﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Supplier.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Admin class
    public class Admin : IdentityUser
    {
        [PersonalData]
        [Column(TypeName= "nvarchar(100)")]
        public string Firstname { get; set; }

    }
}
