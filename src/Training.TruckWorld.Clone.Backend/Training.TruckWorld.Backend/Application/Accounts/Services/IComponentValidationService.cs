using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.TruckWorld.Backend.Application.Accounts.Services
{
     public interface IComponentValidationService
    {
        bool IsValidComponentItem(string item);
        bool IsValidModel(string model);
        bool IsValidStockNumber(string stocknumber);
        bool CheckPhoneNumber(string phonenumber);
        bool CheckLocation(string country, string city, string street);
    }
}
