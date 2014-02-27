using System;
using System.Web;
 
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace SecurityTrial.Models
{
   
    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ManageRoleViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
 
         public SYSTEM_ROLE GetRole()
        {
            var role = new SYSTEM_ROLE()
            {
                Description = this.Description,
                Name = this.Name
               
            };
            return role;
        }
    }
    public class ManageUserRoleViewModel
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DATE_FROM { get; set; }
        [DataType(DataType.Date)]
        public DateTime DATE_TO { get; set; }
        public SYSTEM_USER_ROLE GetUserRole()
        {
            var userRole = new SYSTEM_USER_ROLE()
            {
                DateFrom = this.DATE_FROM,
                DateTo = this.DATE_TO
            };
            return userRole;
        }
    }
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]

        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Required]
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
  

        [Required]
        [Display(Name = "Email address")]
        public string Email { get; set; }


        // Return a pre-poulated instance of AppliationUser:
        public SYSTEM_USER GetUser()
        {
            var user = new SYSTEM_USER()
            {
                UserName = this.UserName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                CreatedDate = DateTime.Now
            };
            return user;
        }

    }


    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        // Allow Initialization with an instance of SYSTEM_USER:
        public EditUserViewModel(SYSTEM_USER user)
        {
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
        }

        [Required]
        [Display(Name = "User Name")]

        public string UserName { get; set; }

        [Required]
      
        
        public string FirstName { get; set; }

        [Required]
       
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
    }


    public class SelectUserRolesViewModel
    {
        public SelectUserRolesViewModel()
        {
            this.Roles = new List<SelectRoleEditorViewModel>();
        }


        // Enable initialization with an instance of SYSTEM_USER:
        public SelectUserRolesViewModel(SYSTEM_USER user)
            : this()
        {
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;

            var Db = new ApplicationDbContext();

            var um = new UserManager<SYSTEM_USER>(
               new UserStore<SYSTEM_USER>(new ApplicationDbContext()));


            var dbUser = um.FindById(user.Id);
            
            // Add all available roles to the list of EditorViewModels:
            var allRoles = Db.Roles;
            
           
            foreach (var role in allRoles)
            {
                var rvm = new SelectRoleEditorViewModel(role);
                this.Roles.Add(rvm);
            }

             //Set the Selected property to true for those roles for 
             //which the current user is a member:
            if (user.SYSTEM_USER_ROLE.Count > 0)
            {
                foreach (var userRole in user.SYSTEM_USER_ROLE)
                {
                    var checkUserRole =
                        this.Roles.Find(r => r.RoleName == userRole.SYSTEM_ROLE.Name);
                    checkUserRole.Selected = true;
                    
                }
            }
        }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<SelectRoleEditorViewModel> Roles { get; set; }
    }

    // Used to display a single role with a checkbox, within a list structure:
    public class SelectRoleEditorViewModel
    {
        public SelectRoleEditorViewModel() { }
        public SelectRoleEditorViewModel(SYSTEM_ROLE role)
        {
            this.RoleName = role.Name;
            this.Description = role.Description;
        }
        public SelectRoleEditorViewModel(SYSTEM_USER_ROLE role)
        {
            this.RoleName = role.Role.Name;
            this.DATE_FROM = role.DateFrom;
            this.DATE_TO = role.DateTo;

        }
        public SYSTEM_USER_ROLE GetUserRole()
        {
            var userRole = new SYSTEM_USER_ROLE()
            {
                DateFrom = this.DATE_FROM,
                DateTo = this.DATE_TO
                //RoleName = this.RoleName
            };
            return userRole;
        }

        public bool Selected { get; set; }

        [Required]
        public string RoleName { get; set; }

        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DATE_FROM { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DATE_TO { get; set; }
    }
}
