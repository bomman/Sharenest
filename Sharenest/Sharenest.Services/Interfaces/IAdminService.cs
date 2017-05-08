using System.Collections.Generic;
using Sharenest.Models.ViewModels.Admin;

namespace Sharenest.Services.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<AdminHomesViewModel> GetAllHomes();
        IEnumerable<AdminPersonsViewModel> GetAllPerons();
    }
}
