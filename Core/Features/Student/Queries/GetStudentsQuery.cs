using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using mambuquery.api.Infrastructure;
using mambuquery.api.Infrastructure.Utilities;

namespace mambuquery.api.Core.Features.Student.Queries
{
    public class GetStudentsQuery : IRequest<List<StudentModel>>
    {
        public List<ListFilter> Filters { get; set; } = new List<ListFilter>();
    }


    public class StudentModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public decimal? Balance { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<StudentModel>>
    {
        public async Task<List<StudentModel>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {


            var query = ExpressionBuilder<Entities.Student>.Build(request.Filters)?.Compile();
            // var query = ExpressionBuilder<Entities.Student>.BuildQueryFilter(request.Filters);
            var students = Entities.Student.GetStudents().AsQueryable();

            if (query != null)
            {
                students = students.Where(query).AsQueryable();
            }

            var result = students.Select(c => new StudentModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Age = c.Age,
                Balance = c.Balance,
                DateOfBirth = c.DateOfBirth
            }).ToList();




            return result;

        }



    }
}