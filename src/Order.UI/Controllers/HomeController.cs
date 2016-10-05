﻿using System;
using System.Web.Mvc;
using NServiceBus;
using Order.Processor.Commands;
using Order.UI.Models;

namespace Order.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBus _bus;

        public HomeController(IBus bus)
        {
            _bus = bus;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Order(string name)
        {
            var orderId = Guid.NewGuid();
            _bus.Send(new PlaceOrder { OrderId = orderId, CustomerId = name });
            return Json(new OrderModel { Name = name, OrderId = orderId });
        }
    }
}