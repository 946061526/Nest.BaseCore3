using Nest.BaseCore.Domain.EsModel;
using Nest.BaseCore.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.BaseCore.RepositoryEs
{
    /// <summary>
    /// 文档仓储实现
    /// </summary>
    public class DocRepository : BaseEsContext<DocInfoModel>, IDocRepository
    {
        public override string IndexName => "doc_info";
        public DocRepository(IEsClientProvider provider) : base(provider)
        {

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(DocInfoModel model)
        {
            return base.Insert(model);
        }

        public bool InsertMany(List<DocInfoModel> tList)
        {
            return base.InsertMany(tList);
        }

        //public bool Update(DocInfoModel model, string id)
        //{
        //}

        /// <summary>
        /// 获取文档
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<DocInfoModel> Get(string text, int pageIndex, int pageSize)
        {
            var client = _EsClientProvider.GetClient(IndexName);

            //排序
            Func<SortDescriptor<DocInfoModel>, IPromise<IList<ISort>>> sort = s =>
            {
                //sd.Descending(SortSpecialField.Score); //根据分值排序
                s.Descending(d => d.CreateTime);
                return s;
            };

            var search = new SearchDescriptor<DocInfoModel>();

            //var musts = new List<Func<QueryContainerDescriptor<DocInfoModel>, QueryContainer>>();
            //musts.Add(p => p.Term(m => m.Field(x => x.Title.Contains(text))));
            // search = search.Index(IndexName).Query(p => p.Bool(m => m.Must(musts))).From((pageIndex - 1) * pageSize).Take(pageSize);
            //search = search.Query(p => p.Bool(m => m.Must(musts))).From((pageIndex - 1) * pageSize).Take(pageSize);

            search.Query(q =>
                q.Bool(b =>
                    b.Must(m =>
                        m.MultiMatch(mm =>
                            mm.Fields(f => f.Field(ff => ff.Title).Field(ff => ff.Content)).Query(text)
                         )
                     ))).Sort(sort).From((pageIndex - 1) * pageSize).Take(pageSize);

            var response = client.Search<DocInfoModel>(search);

            //var response1 = client.Search<DocInfoModel>(p => p.Query(p => p.Match(m => m.Field(f => f.Title.Contains(title)))).From((pageIndex - 1) * pageSize).Take(pageSize));
            //return response1.Documents.ToList();

            return response.Documents.ToList();
        }

        /// <summary>
        /// 获取所有文档
        /// </summary>
        /// <returns></returns>
        public List<DocInfoModel> GetAll()
        {
            var client = _EsClientProvider.GetClient(IndexName);
            var searchDescriptor = new SearchDescriptor<DocInfoModel>();
            // searchDescriptor = searchDescriptor.Index(IndexName).Query(p => p.MatchAll());
            searchDescriptor = searchDescriptor.Query(p => p.MatchAll());
            var response = client.Search<DocInfoModel>(searchDescriptor);
            return response.Documents.ToList();
        }

        /// <summary>
        /// 删除指定标题的数据
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool DeleteByQuery(string title)
        {
            var client = _EsClientProvider.GetClient(IndexName);
            var musts = new List<Func<QueryContainerDescriptor<DocInfoModel>, QueryContainer>>();
            musts.Add(p => p.Term(m => m.Field(f => f.Title).Value(title)));
            var search = new DeleteByQueryDescriptor<DocInfoModel>().Index(IndexName);
            search = search.Query(p => p.Bool(m => m.Must(musts)));
            var response = client.DeleteByQuery<DocInfoModel>(p => search);
            return response.IsValid;
        }
    }
}
