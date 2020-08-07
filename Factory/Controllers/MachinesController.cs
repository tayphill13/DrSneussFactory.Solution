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
      var thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId ==id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine, int EngineerId)
    {
      var joinConfirm = _db.EngineerMachine.FirstOrDefault(join => join.MachineId == machine.MachineId && join.EngineerId == EngineerId);

      if(joinConfirm != null)
      {
        _db.Entry(machine).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = machine.MachineId});
      }
      if (EngineerId != 0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
      }
      _db.Entry(machine).State =EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = machine.MachineId});
    }

    
  }
}