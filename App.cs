using System;
using System.IO;
using System.Text;

namespace SharePoint.Integration.ACS
{
    public class App
    {
        private readonly ISharePointManager _sharePointManager;

        public App(ISharePointManager sharePointManager)
        {
            _sharePointManager = sharePointManager;
        }

        public void Run()
        {
            var site = "https://barnardosau.sharepoint.com/sites/WF";
            //Console.WriteLine($"Title: {_sharePointManager.GetTitle(site)}");

            var file = @"UploadFiles/Sunset.jpg";
            using var stream = new MemoryStream(File.ReadAllBytes(file));
            var fileUrl = _sharePointManager.AddDocument(Path.GetFileName(file), $"Compliance/{DateTime.Now:yyyy MMM}", stream, site);
            Console.WriteLine($"File Uploaded: {fileUrl}");
        }
    }
}
