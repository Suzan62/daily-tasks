﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Models
{
    public class UpdateUserRequestModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        //public string? EmailAddress { get; set; }
     
        public string? UserImage { get; set; }
        public UpdateUserRequestModel() { }
       


    }
}
