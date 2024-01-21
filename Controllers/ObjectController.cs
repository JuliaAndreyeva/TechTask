using JsonTree.Interfaces;
using JsonTree.Models;
using JsonTree.Services;
using JsonTree.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JsonTree.Controllers
{
    public class ObjectController : Controller
    {
        private readonly IObjectRepository _objectRepository;
        private readonly ObjectTxtConverterService _objectTxtConverterService;
        public ObjectController(IObjectRepository objectRepository, ObjectTxtConverterService objectTxtConverterService)
        {
            _objectRepository = objectRepository;
            _objectTxtConverterService = objectTxtConverterService;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Convert(IFormFile importFile, string filePath)
        {
            try
            {
                string path = Path.GetFullPath(importFile.FileName);

                var content = System.IO.File.ReadAllText(path);

                if (importFile.ContentType.Contains("json"))
                {
                    JsonConvert.DeserializeObject<ObjectModel>(content, converters: new ObjectJsonConverterService(_objectRepository));
                    
                    return RedirectToAction("DisplayTree");
                }
                else if (importFile.FileName.Contains("txt"))
                {
                    _objectTxtConverterService.ConvertTxtToJson(content, filePath);
                }
                else
                    return Problem("Type of file is not supported");

                return Ok($"File saved, path {filePath}");
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        public IActionResult DisplayTree()
        {            
            var diagramViewModel = new DiagramViewModel
            {
                ObjectModels = _objectRepository.FindAllObjects(),
                ObjectChildrens = _objectRepository.FindAllChildrens()
            };
            return View("Tree",diagramViewModel);
        }
    }
}
