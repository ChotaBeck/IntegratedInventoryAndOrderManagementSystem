using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public abstract class GenericController<T> : Controller where T : class
{
    private readonly ILogger<GenericController<T>> _logger;
    private readonly ServiceBase<T> _entity;

    public GenericController(ILogger<GenericController<T>> logger, ServiceBase<T> entity)
    {
        _logger = logger;
        _entity = entity;

    }

    //Get  enttites 
    [HttpGet]
    public IActionResult Index()
    { 
        return View(_entity.GetAll());
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var entity = _entity.GetById(id);
        if(id == null)
        {
            return NotFound();
        }
        return View(entity);

    }

    [HttpGet]
     public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(T entity)
    {
        if(_entity.Add(entity))
        {
            return RedirectToAction("Index");
        }
        return View(entity); ;
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        
        var entity =  _entity.GetById(id);
        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }
    [HttpPost]
    public IActionResult Edit(T entity)
    {
        if (_entity.Update(entity))
        {
            return RedirectToAction(nameof(Index)); 
        }
        return View(entity);
    }
    
        
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var entity =  _entity.GetById(id);
        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }
    [HttpPost]
    public IActionResult Delete(T entity)
    {
        if (_entity.Delete(entity))
        {
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }


   

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
