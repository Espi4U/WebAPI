using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.ReportsControllerRequests;
using WebAPI.Models.APIModels.Responses;

namespace WebAPI.Services
{
    public class ReportService : BaseService
    {
        public ListReportsResponse GetAllReportsById(IdRequest request)
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
                            response.BaseMessage = "Bad request";
                        }
                        else
                        {
                            response.Reports = request.PersonId == null ? db.Reports.Where(x => x.FamilyId == request.FamilyId).ToList() :
                                db.Reports.Where(x => x.PersonId == request.PersonId).ToList();
                        }
                    }

                    return response;
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                    return response;
                }
            });
        }

        public BaseResponse AddNewReport(ReportRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Report == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Cannot add empty report";
                            return response;
                        }
                        else if (db.Reports.Contains(request.Report))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Report is already exist";
                            return response;
                        }
                        else
                        {
                            db.Reports.Add(request.Report);
                            db.SaveChanges();
                        }
                    }

                    return response;
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                    return response;
                }
            });
        }

        public BaseResponse DeleteReportById(ReportRequest request)
        {
            return GetResponse(() => {
            var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var report = db.Reports.Where(x => x.Id == request.Report.Id).FirstOrDefault();

                        db.Reports.Remove(report);
                        db.SaveChanges();
                    }

                    return response;
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                    return response;
                }
            });
        }
    }
}