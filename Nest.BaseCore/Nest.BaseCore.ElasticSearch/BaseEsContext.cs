using com.google.zxing.qrcode.decoder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.ElasticSearch
{
    /// <summary>
    /// es操作基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEsContext<T> : IBaseEsContext where T : class
    {
        protected IEsClientProvider _EsClientProvider;
        /// <summary>
        /// 索引名称
        /// </summary>
        public abstract string IndexName { get; }

        public BaseEsContext(IEsClientProvider provider)
        {
            _EsClientProvider = provider;
        }

        ////请求单一节点
        //var singleString = Nest.Indices.Index("db_studnet");
        //var singleTyped = Nest.Indices.Index<Student>();

        //ISearchRequest singleStringRequest = new SearchDescriptor<Student>().Index(singleString);
        //ISearchRequest singleTypedRequest = new SearchDescriptor<Student>().Index(singleTyped);

        ////请求多个节点
        //var manyStrings = Nest.Indices.Index("db_studnet", "db_other_student");
        //var manyTypes = Nest.Indices.Index<Student>().And<OtherStudent>();

        //ISearchRequest manyStringRequest = new SearchDescriptor<Student>().Index(manyStrings);
        //ISearchRequest manyTypedRequest = new SearchDescriptor<Student>().Index(manyTypes);

        ////请求所有节点
        //var indicesAll = Nest.Indices.All;
        //var allIndices = Nest.Indices.AllIndices;

        //ISearchRequest indicesAllRequest = new SearchDescriptor<Student>().Index(indicesAll);
        //ISearchRequest allIndicesRequest = new SearchDescriptor<Student>().Index(allIndices);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        public bool Insert(T model)
        {
            var client = _EsClientProvider.GetClient(IndexName);
            if (!client.Indices.Exists(IndexName).Exists)
            {
                client.CreateIndex<T>(IndexName);
            }
            var response = client.IndexDocument(model);
            //client.Create()
            return response.IsValid;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        public bool InsertMany(List<T> tList)
        {
            var client = _EsClientProvider.GetClient(IndexName);
            if (!client.Indices.Exists(IndexName).Exists)
            {
                client.CreateIndex<T>(IndexName);
            }
            var response = client.Bulk(p => p.Index(IndexName).IndexMany(tList));//var response = client.IndexMany(tList);
            return response.IsValid;
        }

        public List<T> GetAll()
        {
            return null;
        }

        public bool Update(T model, string Id)
        {
            var client = _EsClientProvider.GetClient(IndexName);
            if (client.Indices.Exists(IndexName).Exists)
            {
                client.Update<T>(Id, m => m.Doc(model));
            }
            return false;
        }


        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount(string fieldName = "")
        {
            var client = _EsClientProvider.GetClient(IndexName);
            var search = new SearchDescriptor<T>().MatchAll();
            if (!string.IsNullOrEmpty(fieldName))//指定查询字段 .Source(p => p.Includes(x => x.Field("Id")));
                search = search.Source(p => p.Includes(x => x.Field(fieldName)));

            var response = client.Search<T>(search);
            return response.Total;
        }

        /// <summary>
        /// 根据Id删除数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteById(string id)
        {
            var client = _EsClientProvider.GetClient(IndexName);
            var response = client.Delete<T>(id);
            return response.IsValid;
        }
    }
}
