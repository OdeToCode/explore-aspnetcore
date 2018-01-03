using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Controllers.Controllers
{
    [Route("[controller]/[action]")]
    public class StreamController : Controller
    {
        public IActionResult StaticFile()
        {
            return View();
        }

        public IActionResult FileStream()
        {
            return View();
        }


        public IActionResult SampleVideo()
        {
            return File("sample.mp4", "video/mp4");
        }

        string pathToVideos = "";
                
        public IActionResult SampleVideoStream()
        {
            var path = Path.Combine(pathToVideos, "sample.mp4");
            return File(System.IO.File.OpenRead(path), "video/mp4");
        }
    }
}