using Nest.BaseCore.Domain.EsModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.RepositoryEs
{
    public interface IDocRepository
    {
        bool Insert(DocInfoModel model);
        bool InsertMany(List<DocInfoModel> tList);
        List<DocInfoModel> Get(string text, int pageIndex, int pageSize);
        List<DocInfoModel> GetAll();
        bool DeleteByQuery(string title);
    }
}
