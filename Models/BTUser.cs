﻿using BugTracker.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class BTUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "The{0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        public string DisplayName { get; set; }

        [Display(Name = "Full Name")]
        [NotMapped]
        public string FullName{get{ return $"{FirstName} {LastName}";}}

        [Display(Name = "Select Image")]
        [NotMapped]
        [DataType(DataType.Upload)]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile AvatarFormFile { get; set; }
        public string AvatarFileName { get; set; }
        public byte[] AvatarFileData { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Project> Projects { get; set; }




    }
}
