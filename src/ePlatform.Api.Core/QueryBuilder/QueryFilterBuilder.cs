using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ePlatform.Api.Core
{
    // İtemleri filtrelemek için üretilmesi gereken class
    /// <summary>
    /// Adds generic Model for search.
    /// </summary>
    public class QueryFilterBuilder<T> where T : new()
    {
        public List<QueryFilter> Filters { get; private set; } = new List<QueryFilter>();

        PagingModel model = new PagingModel()
        {
            PageIndex = 1,
            PageSize = 50
        };


        /// <summary>
        /// Adds three parameters and returns the result.
        /// </summary>
        /// <returns>
        /// The result of QueryFilterBuilder to Build()
        /// </returns>
        /// <example>
        /// <code>
        ///  var queryFilter = new QueryFilterBuilder<OutboxInvoiceGetModel>()
        /// .QueryFor(q => q.ExecutionDate, Operator.LessThan, new DateTime())
        /// .QueryFor(q => q.InvoiceNumber, Operator.Equal, "EPK2019000001731")
        /// .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Error)
        /// .Buil();
        /// </code>
        /// </example>
        public QueryFilterBuilder<T> QueryFor<TProperty>(Expression<Func<T, TProperty>> expression, Operator @operator, object value)
        {
            MemberExpression memeberExpression = expression.Body as MemberExpression;
            object convertedObj = value;

            if (typeof(TProperty) == typeof(DateTime) || typeof(TProperty) == typeof(DateTime?))
            {
                if (value is DateTime)
                    convertedObj = Convert.ToDateTime(value).ToString("dd.MM.yyyy");
                else
                    throw new Exception(memeberExpression.Member.Name + " value should be DateTime class");
            }

            Filters.Add(new QueryFilter(memeberExpression.Member.Name, @operator, convertedObj));

            return this;
        }
        public QueryFilterBuilder<T> PageIndex(int index = 1)
        {
            if (index == 0)
                throw new Exception("PageIndex can not be zero");
            this.model.PageIndex = index;
            return this;
        }
        public QueryFilterBuilder<T> PageSize(int size = 50)
        {
            if (size == 0)
                throw new Exception("PageSize can not be zero");
            this.model.PageSize = size;
            return this;
        }
        public QueryFilterBuilder<T> IsDesc(bool isDesc)
        {
            this.model.IsDesc = isDesc;
            return this;
        }
        public PagingModel Build()
        {
            model.QueryFilter = JsonConvert.SerializeObject(Filters);
            return model;
        }
    }
}
