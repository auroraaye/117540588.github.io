using cyber_project.Models;
using cyber_project.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cyber_project.Services.Business
{
    public class DataService
    {
        InputData dataService = new InputData();
        public bool Acc(RegisterModel user)
        {
            return dataService.CheckUser(user);
        }
    }
}