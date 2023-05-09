using cyber_project.Models;
using cyber_project.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cyber_project.Services.Business
{
    public class SecurityService
    {
        SecurityDAO daoService = new SecurityDAO();
        public bool Authenticate(UserModel user)
        {
            return daoService.FindByUser(user);
        }
    }
}