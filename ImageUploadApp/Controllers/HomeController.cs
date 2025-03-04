using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using ImageUploadApp.Models;
using ImageUploadApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploadApp.Controllers
{
    public class HomeController : Controller
    {
        //dependency for GridFS service to handle file storage
        private readonly GridFsService _gridFsService;

        public HomeController(GridFsService gridFsService)
        {
            _gridFsService = gridFsService;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Upload()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Upload(ImageTextModel model)
        {
            if (model.ImageFile != null)
            {
                using var stream = model.ImageFile.OpenReadStream();

                
                await _gridFsService.InsertUploadAsync(model, stream, model.ImageFile.FileName);
            }

            
            return RedirectToAction(nameof(Index));
        }

        //display All Uploaded Entries
        public async Task<IActionResult> ViewUploads()
        {
            var uploads = await _gridFsService.GetAllUploadsAsync();
            return View(uploads);
        }

        //load Edit Page for a Specific Upload
        public async Task<IActionResult> Edit(string id)
        {
            var upload = await _gridFsService.GetUploadByIdAsync(id);

            //if the upload entry is not found, return a 404 error
            if (upload == null) return NotFound();

            return View(upload);
        }

        //handle edit request 
        [HttpPost]
        public async Task<IActionResult> Edit(ImageTextModel model)
        {
            using var stream = model.ImageFile?.OpenReadStream();

            //update existing entry in MongoDB
            await _gridFsService.UpdateUploadAsync(model, stream, model.ImageFile?.FileName);

            
            return RedirectToAction(nameof(ViewUploads));
        }

        //retrieve Image File from GridFS Storage
        public async Task<IActionResult> GetImage(string id)
        {
            var imageBytes = await _gridFsService.GetImageByIdAsync(id);

            //return image file in PNG format (supports PNG & JPEG)
            return File(imageBytes, "image/png");
        }
    }
}
