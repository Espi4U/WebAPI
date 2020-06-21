using Shared.Models.Requests;
using Shared.Models.Requests.BaseRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.ReportsResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.ReportsControllerRequests;
using WebAPI.Models.APIModels.Responses;

namespace WebAPI.Services
{
    public class ReportService : BaseService
    {
        public ListReportsResponse GetReports(BaseRequest request)
        {
            return GetResponse(()=> {
                var response = new ListReportsResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.PersonId == null && request.FamilyId == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = Shared.Constants.BAD_REQUEST;
                        }
                        else
                        {
                            response.Reports = request.PersonId == null ? db.Reports.Where(x => x.FamilyId == request.FamilyId).ToList() :
                                db.Reports.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId).ToList();
                        }
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }

        public BaseResponse DeleteReport(DeleteReportRequest request)
        {
            return GetResponse(() => {
            var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var report = db.Reports.Where(x => x.Id == request.ReportId && x.FamilyId == request.FamilyId).FirstOrDefault();

                        db.Reports.Remove(report);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }

        public BaseResponse AddReport(ReportRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var family = db.Families.Where(x => x.Id == request.FamilyId).FirstOrDefault();
                        var person = db.Persons.Where(x => x.FamilyId == request.FamilyId && x.Id == request.PersonId).FirstOrDefault();

                        var model = new Report
                        {
                            Name = request.Name,
                            Text = request.Text,
                            Date = request.Date,
                            Family = family,
                            Person = person
                        };

                        db.Reports.Add(model);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }
    }
}