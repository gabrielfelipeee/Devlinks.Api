namespace Api.Domain.Models
{
    public class UserModel
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _avatar;
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        private string _slug;
        public string Slug
        {
            get { return _slug; }
            set { _slug = value; }
        }
        

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
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
