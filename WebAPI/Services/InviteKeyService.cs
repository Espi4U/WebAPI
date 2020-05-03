using Shared;
using Shared.Models;
using Shared.Models.Requests;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class InviteKeyService : BaseService
    {
        public BaseResponse AddInviteKey(InviteKeyRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var family = db.Families.Where(x => x.Id == request.FamilyId).FirstOrDefault();

                        var inviteKey = new InviteKey
                        {
                            Key = request.Key,
                            Family = family
                        };
                        db.InviteKeys.Add(inviteKey);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Constants.BAD_REQUEST;
                }

                return response;
            });
        }
    }
}
