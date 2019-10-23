using System;
using System.Collections.Generic;

namespace CompanyName.ProjectName.Model
{
    public partial class User
    {
        public User()
        {

        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SmsCode { get; set; }
        public DateTime? SmsCodeExpiredOn { get; set; }
        public DateTime? SmsCodePassedOn { get; set; }
        public bool IsCubeUser { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
