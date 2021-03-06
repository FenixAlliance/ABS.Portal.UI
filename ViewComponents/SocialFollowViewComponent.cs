using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Access.Helpers;
using FenixAlliance.ABM.Models.Holders;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class SocialFollowViewComponent : ViewComponent
    {
        private AccountUsersHelpers AccountUsersHelpers { get; set; }
        private ABMContext DataContext { get; set; }
        private TenantHelpers TenantHelpers { get; set; }

        public SocialFollowViewComponent(ABMContext context, TenantHelpers TenantHelpers, AccountUsersHelpers AccountUsersHelpers)
        {
            //Add Method Context 
            this.DataContext = context;
            this.AccountUsersHelpers = AccountUsersHelpers;
            this.TenantHelpers = TenantHelpers;
        }
        public async Task<IViewComponentResult> InvokeAsync(AccountHolder Holder, ClaimsPrincipal user, string FollowerID, string FollowedID)
        {
            if (user.Identity.IsAuthenticated)
            {
                string FollowID = null;
                if (Holder == null)
                {
                    Holder = await TenantHelpers.GetTenantWithSelectedBusinessAsync(user);
                }
                // If acting as Holder
                if (Holder.SelectedBusiness == null)
                {
                    // If looking at a Follower
                    if (!string.IsNullOrEmpty(FollowerID))
                    {
                        FollowID = (await DataContext.FollowRecord.Where(c => c.FollowedSocialProfileID == Holder.SocialProfile.ID && c.FollowerSocialProfileID == FollowerID).FirstOrDefaultAsync())?.ID;
                    }
                    // If looking at a Follow
                    if (!string.IsNullOrEmpty(FollowedID))
                    {
                        FollowID = (await DataContext.FollowRecord.Where(c => c.FollowerSocialProfileID == Holder.SocialProfile.ID && c.FollowedSocialProfileID == FollowedID).FirstOrDefaultAsync())?.ID;
                    }
                }
                // If Acting As Business
                else
                {
                    if (!string.IsNullOrEmpty(FollowerID))
                    {
                        FollowID = (await DataContext.FollowRecord.Where(c => c.FollowedSocialProfileID == Holder.SelectedBusiness.BusinessSocialProfile.ID && c.FollowerSocialProfileID == FollowerID).FirstOrDefaultAsync())?.ID;
                    }

                    if (!string.IsNullOrEmpty(FollowedID))
                    {
                        FollowID = (await DataContext.FollowRecord.Where(c => c.FollowerSocialProfileID == Holder.SelectedBusiness.BusinessSocialProfile.ID && c.FollowedSocialProfileID == FollowedID).FirstOrDefaultAsync())?.ID;
                    }
                }

                ViewData["Holder"] = Holder;
                ViewData["FollowID"] = FollowID;
                ViewData["FollowerID"] = FollowerID;
                ViewData["FollowedID"] = FollowedID;
                ViewData["SocialProfileID"] = (Holder.SelectedBusiness == null) ? Holder.SocialProfile.ID : Holder.SelectedBusiness.BusinessSocialProfile.ID;
            }
            return View();
        }
    }
}
