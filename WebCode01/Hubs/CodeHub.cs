using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebCode01.Services;
using WebCode01.ViewModels;
using Microsoft.AspNet.Identity;

namespace WebCode01.Hubs
{
    public class CodeHub : Hub
    {
        public ProjectFilesService service = new ProjectFilesService(null);
        public void OnChange(object changeData, int documentId, string content, string userId)
        {
            //Clients.All.OnChange(changeData);
            Clients.Group(Convert.ToString(documentId), Context.ConnectionId).OnChange(changeData, userId);
            
            ProjectFileViewModel model = new ProjectFileViewModel
            {
                id = documentId,
                fileContent = content
            };
            service.SaveCodeToDb(model); // Save editor changes to database
        }

        public void JoinDocument(int documentId)
        {
            Groups.Add(Context.ConnectionId, Convert.ToString(documentId));
        }
    }
}