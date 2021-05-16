using System.Collections.Generic;

namespace Domain.Core
{
    public class DomainNotifications: IDomainNotifications
    {
        private List<string> _notifications = new List<string>();

        public DomainNotifications()
        {
            _notifications = new List<string>();
        }

        public void AddNotification(string notification)
        {
            _notifications.Add(notification);
        }

        public void CleanNotifications()
        {
            _notifications = new List<string>();
        }

        public List<string> GetAll()
        {
            return _notifications;
        }

        public bool HasNotifications()
        {
            return _notifications.Count > 0;
        }
    }
}