using ApiTickets.Models;
using ApiTickets.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTickets.Controllers
{
    public class LogicAppsController : Controller
    {
        private ServiceLogicApps service;

        public LogicAppsController(ServiceLogicApps service)
        {
            this.service = service;
        }

       

       


    }
}
