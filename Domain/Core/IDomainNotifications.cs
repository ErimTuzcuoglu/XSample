using System.Collections.Generic;

namespace Domain.Core
{
    public interface IDomainNotifications
    {
        void AddNotification(string notification);
        bool HasNotifications();
        List<string> GetAll();
        void CleanNotifications();
    }
}