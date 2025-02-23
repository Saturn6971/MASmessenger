using DevExpress.Xpo;
using System;

namespace MasJWT.Classes
{
    public class User : XPObject
    {
        public User(Session session) : base(session) { }

        string? name;
        [Size(255)]
        public string? Name
        {
            get { return name; }
            set { SetPropertyValue(nameof(Name), ref name, value); }
        }

        string? surname;
        [Size(255)]
        public string? Surname
        {
            get { return surname; }
            set { SetPropertyValue(nameof(Surname), ref surname, value); }
        }

        string? email;
        [Size(255)]
        public string? Email
        {
            get { return email; }
            set { SetPropertyValue(nameof(Email), ref email, value); }
        }

        string? password;
        public string? Password
        {
            get { return password; }
            set { SetPropertyValue(nameof(Password), ref password, value); }
        }

        byte[]? passwordHash;
        public byte[]? PasswordHash
        {
            get { return passwordHash; }
            set { SetPropertyValue(nameof(PasswordHash), ref passwordHash, value); }
        }

        byte[]? passwordSalt;
        public byte[]? PasswordSalt
        {
            get { return passwordSalt; }
            set { SetPropertyValue(nameof(PasswordSalt), ref passwordSalt, value); }
        }

        byte[]? image;
        public byte[]? Image
        {
            get { return image; }
            set { SetPropertyValue(nameof(Image), ref image, value); }
        }

        DateTime? birthday;
        public DateTime? Birthday
        {
            get { return birthday; }
            set { SetPropertyValue(nameof(Birthday), ref birthday, value); }
        }

        string? job;
        [Size(255)]
        public string? Job
        {
            get { return job; }
            set { SetPropertyValue(nameof(Job), ref job, value); }
        }

        string? nationality;
        [Size(255)]
        public string? Nationality
        {
            get { return nationality; }
            set { SetPropertyValue(nameof(Nationality), ref nationality, value); }
        }

        string? gender;
        [Size(255)]
        public string? Gender
        {
            get { return gender; }
            set { SetPropertyValue(nameof(Gender), ref gender, value); }
        }

        string? role;
        [Size(255)]
        public string? Role
        {
            get { return role; }
            set { SetPropertyValue(nameof(Role), ref role, value); }
        }
    }
}
