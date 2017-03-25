using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.RequestBin.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Models;

namespace Apiest.Features.RequestBin.Services.Services
{
    public class RequestBinService : IRequestBinService
    {
        private readonly IRequestBinUnitOfWork _requestBinUnitOfWork;

        public RequestBinService(IRequestBinUnitOfWork requestBinUnitOfWork)
        {
            _requestBinUnitOfWork = requestBinUnitOfWork;
        }

        public void CreateGroup(string testgroupname)
        {
            var group =
                _requestBinUnitOfWork.WebRequestGroup.Where(
                    p => p.Name.Equals(testgroupname, StringComparison.CurrentCultureIgnoreCase));
            if (!group.Any())
            {
                _requestBinUnitOfWork.WebRequestGroup.Add(new WebRequestGroup
                {
                    GroupUniqueId = Guid.NewGuid(),
                    Name = testgroupname
                });

                _requestBinUnitOfWork.Commit();
            }
        }

        public WebRequestGroup GetGroup(string testgroupname)
        {
            return _requestBinUnitOfWork.WebRequestGroup.Where(p => p.Name.Equals(testgroupname, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public void RemoveGroup(WebRequestGroup group)
        {
            _requestBinUnitOfWork.WebRequestGroup.Remove(group);
            _requestBinUnitOfWork.Commit();
        }

        public IEnumerable<WebRequest> GetAllRequests()
        {
            return _requestBinUnitOfWork.WebRequest.AsQueryable().ToList();
        }

        public IEnumerable<WebRequestGroup> GetAllGroups()
        {
            var query = (from reqgrp in _requestBinUnitOfWork.WebRequestGroup.AsQueryable()
                         join req in _requestBinUnitOfWork.WebRequest.AsQueryable() on reqgrp.Id equals req.WebRequestGroupId
                         select reqgrp).ToList();

            return query
                .GroupBy(p => new {p.Id, p.GroupUniqueId, p.Name})
                .Select(grp => new WebRequestGroup
                {
                     Id = grp.Key.Id,
                     Name = grp.Key.Name,
                     GroupUniqueId = grp.Key.GroupUniqueId,
                     TotalRequests = grp.Count().ToString()
                });
        }

        public void CreateNewGroup(string groupName)
        {
            _requestBinUnitOfWork.WebRequestGroup.Add(new WebRequestGroup
            {
                Name = groupName,
                GroupUniqueId = Guid.NewGuid()
            });
            _requestBinUnitOfWork.Commit(); 
        }

        public IEnumerable<WebRequestGroup> Groups
        {
            get
            {
                var groups = (from grp in _requestBinUnitOfWork.WebRequestGroup.AsQueryable() select grp).ToList();
                foreach (var group in groups)
                {
                    group.TotalRequests = CalculateTotalGroupRequests(group.Id);
                }

                return groups;
            }
        }

        private string CalculateTotalGroupRequests(int id)
        {
            return
                (from req in _requestBinUnitOfWork.WebRequest.AsQueryable() where req.WebRequestGroupId == id select req).Count()
                    .ToString();
        }

        public void DeleteGroup(WebRequestGroup webRequestGroup)
        {
            if (webRequestGroup != null)
            {
                var requests = GetGroupRequests(webRequestGroup.Id);
                foreach (var request in requests)
                {
                    if (request != null)
                        _requestBinUnitOfWork.WebRequest.Remove(request);
                }

                _requestBinUnitOfWork.WebRequestGroup.Remove(webRequestGroup);

                _requestBinUnitOfWork.Commit();
            }

        }

        public IEnumerable<WebRequest> GetGroupRequests(int groupId)
        {
            return (from req in _requestBinUnitOfWork.WebRequest.AsQueryable()
                    where req.WebRequestGroupId == groupId
                    select req).ToList();
        }

        public void UpdateGroup(WebRequestGroup group)
        {
            if (group != null)
            {
                _requestBinUnitOfWork.WebRequestGroup.Update(group);
                _requestBinUnitOfWork.Commit();
            }

        }

        public WebRequestGroup GetGroup(Guid groupId)
        {
            return (from grp in _requestBinUnitOfWork.WebRequestGroup.AsQueryable()
                    where grp.GroupUniqueId == groupId
                    select grp).FirstOrDefault();
        }

        public void DeleteGroupRequests(int groupId)
        {
            var requests = GetGroupRequests(groupId);
            foreach (var request in requests)
            {
                DeleteRequest(groupId, request.Id);
            }
        }

        public void DeleteRequest(int groupId, int requestId)
        {
            var request = (from req in _requestBinUnitOfWork.WebRequest.AsQueryable()
                           where req.WebRequestGroupId == groupId && req.Id == requestId
                           select req).FirstOrDefault();
            if (request != null)
            {
                _requestBinUnitOfWork.WebRequest.Remove(request);
                _requestBinUnitOfWork.Commit();
            }
        }

        public IEnumerable<WebRequest> GetAllGroupRequests()
        {
            return (from req in _requestBinUnitOfWork.WebRequest.AsQueryable() select req).ToList();
        }

        public void AddResponse(WebResponse response)
        {
            _requestBinUnitOfWork.WebResponce.Add(response);
            _requestBinUnitOfWork.Commit();
        }

        public IEnumerable<WebRequest> FilterGroup(Expression<Func<WebRequest,bool>> expression)
        {
            return _requestBinUnitOfWork.WebRequest.Where(expression).ToList();
        }
    }
}
