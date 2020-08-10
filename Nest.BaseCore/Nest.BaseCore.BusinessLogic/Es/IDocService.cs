using Nest.BaseCore.Domain.EsModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.BusinessLogic.Es
{
    public interface IDocService
    {
        bool Insert();
        bool InsertMany();
        List<DocInfoModel> Get();
        List<DocInfoModel> GetAll();
        bool DeleteByQuery();
    }
}
