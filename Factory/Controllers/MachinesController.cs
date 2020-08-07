using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
  public class MachinesControlller : Controller
  {
    private readonly FactoryContext _db;
    public MachinesControlller(FactoryContext db)
    {
      _db = db;
    }
    public ActionResult Details(int id)
    {
      var thisMachine = _db.Machines
          .Include(machine => machine.Engineers)
          .ThenInclude(join => join.Engineer)
          .FirstOrDefault(machine => machine.MachineId ==id);
      return View(thisMachine);
    }
    public ActionResult Create()
    {
      ViewBag.MachineId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View();
    }
    public ActionResult Edit(int id)
    {
      
    }
  }
}