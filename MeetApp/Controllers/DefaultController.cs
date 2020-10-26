using MeetApp.Actions;
using MeetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace MeetApp.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpPost]      
        public async Task<AddUserOutput> AddUser(User User)
        {
           return await UserManager.AddUser(User);
        }


        [HttpPost]
        public async Task<List<User>> GetUser(GetUserInput Parameters)
        {
            return await UserManager.GetUser(Parameters);
        }

        [HttpPost]
        public async Task<UpdateUserOutput> UpdateUser(UpdateUserInput Parameters)
        {
            return await UserManager.UpdateUser(Parameters);
        }

        [HttpPost]
        public async Task<DeleteUserOutput> DeleteUser(DeleteUserInput Parameters)
        {
            return await UserManager.DeleteUser(Parameters);
        }




    [HttpPost]
        public async Task<AddSalloonOutput> AddSalloon(Salloon Salloon)
        {
            return await SalloonManager.AddSalloon(Salloon);
            //SalloonManager.AddSalloon(Salloon);
            //return new AddSalloonOutput() { Type = 1, Message = "Kayit Basarıyla Eklendi..." };
        }

        [HttpPost]
        public async Task<List<Salloon>> GetSalloon(GetSallooninput Parameters)
        {
            return await SalloonManager.GetSalloon(Parameters);
        }

        [HttpPost]
        public async Task<UpdateSalloonOutput> UpdateSalloon(UpdateSallooninput Parameters)
        {
            return await SalloonManager.UpdateSalloon(Parameters);
            
        }

        [HttpPost]
        public async Task<DeleteSalloonOutput> DeleteSalloon(DeleteSallooninput Parameters)
        {
            return await SalloonManager.DeleteSalloon(Parameters);

        }

        [HttpPost]
        public async Task<AddBranchOutput> AddBranch(Branch Branch)
        {
            return await BranchManager.Add(Branch);
        }

        [HttpPost]
        public async Task<List<Branch>> GetAllBranches(GetBranchInput Parameters)
        {
            return await BranchManager.GetBranch(Parameters);

        }
        [HttpPost]
        public async Task<UpdateBranchOutput> UpdateBranch(UpdateBranchInput Parameters)
        {
            return await BranchManager.Update(Parameters);

        }
        [HttpPost]
        public async Task<DeleteBranchOutput> DeleteBranch(DeleteBranchInput Parameters)
        {
            return await BranchManager.Delete(Parameters);

        }

        [HttpPost]
        public async Task<AddMeetingHistoryOutput> AddMeetingHistory(MeetingHistory MeetingHistory)
        {
            return await MeetHistoryManager.AddMeetingHistory(MeetingHistory);

        }
        [HttpPost]
        public async Task<List<MeetingHistory>> GetMeetingHistory(GetMeetingHistoryInput Parameters)
        {
            return await MeetHistoryManager.GetMeetingHistory(Parameters);
        }

        [HttpPost]

        public async Task<UpdateMeetingHistoryOutput> UpdateMeetingHistory(UpdateMeetingHistoryInput Parameters)
        {
            return await MeetHistoryManager.UpdateMeetingHistory(Parameters);
        }

     
        [HttpPost]

        public async Task<DeleteMeetingHistoryOutput> Delete(DeleteMeetingHistoryInput Parameters)
        {
            return await MeetHistoryManager.Delete(Parameters);
        }



    }
}