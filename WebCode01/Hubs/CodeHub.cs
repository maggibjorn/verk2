using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebCode01.Hubs
{
    public class CodeHub : Hub
    {
        public void OnChange(object changeData, int documentId)
        {
            //Clients.All.OnChange(changeData);
            Clients.Group(Convert.ToString(documentId), Context.ConnectionId).OnChange(changeData);
        }

        public void JoinDocument(int documentId)
        {
            Groups.Add(Context.ConnectionId, Convert.ToString(documentId));
        }
    }
}