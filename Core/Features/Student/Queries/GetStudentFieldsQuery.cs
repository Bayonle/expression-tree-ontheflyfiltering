using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using MediatR;
using mambuquery.api.Core.Entities;
using System.Linq;
using mambuquery.api.Infrastructure;
using ExpressionBuilderNetCore.Generics;
using ExpressionBuilderNetCore.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using RestSharp;
using ExpressionBuilderNetCore.Conditions;
using ExpressionBuilderNetCore.Common;
using System.Linq.Expressions;
using mambuquery.api.Core.Models;
using mambuquery.api.Infrastructure.Utilities;

namespace mambuquery.api.Core.Features.Student.Queries
{
    public class GetStudentFieldsQuery : IRequest<List<DataField>>
    {

    }



    public class GetStudentFieldsQueryHandler : IRequestHandler<GetStudentFieldsQuery, List<DataField>>
    {
        public Task<List<DataField>> Handle(GetStudentFieldsQuery request, CancellationToken cancellationToken)
        {
            var fields = ColumnHelper<Entities.Student>.GetFields();

            return Task.FromResult(fields);
        }
    }
}