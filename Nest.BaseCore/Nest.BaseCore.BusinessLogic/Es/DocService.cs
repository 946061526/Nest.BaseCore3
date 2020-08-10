using Nest.BaseCore.Domain.EsModel;
using Nest.BaseCore.RepositoryEs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.BusinessLogic.Es
{
    public class DocService : IDocService
    {
        private readonly IDocRepository _docRepository;
        public DocService(IDocRepository docRepository)
        {
            _docRepository = docRepository;
        }

        public bool Insert()
        {
            DocInfoModel model = new DocInfoModel()
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = "",
                Title = "文档2",
                Content = "这是测试文档2",
                UserName = "admin",
                CreateTime = DateTime.Now,
            };
            return _docRepository.Insert(model);
        }
        public bool InsertMany()
        {
            List<DocInfoModel> list = new List<DocInfoModel>();
            DocInfoModel model = new DocInfoModel()
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = "",
                Title = "文档3",
                Content = "这是测试文档3",
                UserName = "admin",
                CreateTime = DateTime.Now,
            };
            list.Add(model);
            model = new DocInfoModel()
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = "",
                Title = "文档4",
                Content = "这是测试文档4",
                UserName = "admin",
                CreateTime = DateTime.Now,
            };
            list.Add(model);
            return _docRepository.InsertMany(list);
        }
        public List<DocInfoModel> Get()
        {
            string text = "文档";
            int pageIndex = 1;
            int pageSize = 10;
            return _docRepository.Get(text, pageIndex, pageSize);
        }

        public List<DocInfoModel> GetAll()
        {
            return _docRepository.GetAll();
        }

        public bool DeleteByQuery()
        {
            string title = "文档1";
            return _docRepository.DeleteByQuery(title);
        }
    }
}
