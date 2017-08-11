using App2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Interface
{
    public interface IIosMethods
    {
        string GetIdentifier();
        void ShowToast(string msg);
        ResponseModel RetriveLocalData();
        void SaveLocalData(ResponseModel um);
        void DeleteLocalData();
    }
}
