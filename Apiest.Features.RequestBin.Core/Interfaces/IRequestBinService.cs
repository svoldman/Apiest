using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.RequestBin.Core.Models;

namespace Apiest.Features.RequestBin.Core.Interfaces
{
    public interface IRequestBinService
    {
        void CreateGroup(string testgroupname);
        WebRequestGroup GetGroup(string testgroupname);
        void RemoveGroup(WebRequestGroup @group);
        IEnumerable<WebRequest> GetAllRequests();
        IEnumerable<WebRequestGroup> GetAllGroups();
        void CreateNewGroup(string groupName);
        IEnumerable<WebRequestGroup> Groups { get; }
        void DeleteGroup(WebRequestGroup webRequestGroup);
        IEnumerable<WebRequest> GetGroupRequests(int groupId);
        void UpdateGroup(WebRequestGroup @group);
        WebRequestGroup GetGroup(Guid groupId);
        void DeleteGroupRequests(int id);
        void DeleteRequest(int groupId, int requestId);
        IEnumerable<WebRequest> GetAllGroupRequests();
        void AddResponse(WebResponse response);
        IEnumerable<WebRequest> FilterGroup(Expression<Func<WebRequest,bool>> expression);
    }
}
