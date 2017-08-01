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
        void ShowLoader();
        void DismissLoader();
        //UserModel RetriveLocalData();
        //void SaveLocalData(UserModel um);
        //void DeleteLocalData();

    }
}
