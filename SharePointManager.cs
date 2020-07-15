using Microsoft.SharePoint.Client;
using System.IO;

namespace SharePoint.Integration.ACS
{
    public interface ISharePointManager
    {
        string GetTitle(string sharePointSite);
        string AddDocument(string fileName, string path, Stream documentStream, string sharePointSite);
    }

    public class SharePointManager : ISharePointManager
    {
        private readonly IContextFactory _contextFactory;

        public SharePointManager(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public string GetTitle(string sharePointSite)
        {
            using var context = _contextFactory.GetContext(sharePointSite);
            context.Load(context.Web, p => p.Title);
            context.ExecuteQuery();
            return context.Web.Title;
        }

        public string AddDocument(string fileName, string path, Stream documentStream, string sharePointSite)
        {
            using var context = _contextFactory.GetContext(sharePointSite);
            var fileInfo = new FileCreationInformation
            {
                Url = Path.GetFileName(fileName),
                ContentStream = documentStream,
                Overwrite = true
            };

            var folders = path.Split('/');
            var targetList = context.Web.Lists.GetByTitle("Documents");
            var uploadFolder = targetList.RootFolder;
            foreach (var folder in folders)
            {
                uploadFolder = uploadFolder.Folders.Add(folder);
                uploadFolder.Update();
            }
            var file = uploadFolder.Files.Add(fileInfo);
            context.Load(file.ListItemAllFields, item => item["EncodedAbsUrl"], item => item["FileRef"]);
            context.ExecuteQuery();

            return file.ListItemAllFields["EncodedAbsUrl"].ToString();
        }
    }
}