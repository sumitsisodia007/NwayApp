using App2.Model;

namespace App2.Interface
{
    public interface IIosMethods
    {
        string GetIdentifier();
        string GetTokan();
        void ShowToast(string msg);
        UserModel RetriveLocalData();
        void SaveLocalData(UserModel um);
        void DeleteLocalData();
    }
}
