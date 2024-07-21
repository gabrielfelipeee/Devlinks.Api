namespace Api.Domain.Models
{
    public class LinkModel
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _platform;
        public string Platform
        {
            get { return _platform; }
            set { _platform = value; }
        }
        

        private string _link;
        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        private DateTime _updatedAt;
        public DateTime UpdataedAt
        {
            get { return _updatedAt; }
            set { _updatedAt = value; }
        }

        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }
    }
}
