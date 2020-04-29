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
                        if (request.PersonId == 0 && request.FamilyId == 0)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = Shared.Constants.BAD_REQUEST;
                        }
                        else
                        {
                            if(request.PersonId != 0 && request.FamilyId != 0)
                            {
                                response.Reports = db.Reports.Where(x => x.PersonId == request.PersonId || x.FamilyId == request.FamilyId).ToList();
                            }
                            else
                            {
                                response.Reports = request.PersonId == 0 ? db.Reports.Where(x => x.FamilyId == request.FamilyId).ToList() :
                                db.Reports.Where(x => x.PersonId == request.PersonId).ToList();
                            }
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

        public BaseResponse DeleteReport(ReportRequest request)
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