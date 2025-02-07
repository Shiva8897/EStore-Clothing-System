using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos
{
    public  class PasswordResetReq
    {
        public string Email {  get; set; }
        public string NewPassword { get; set; }
        public string Token {  get; set; }
    }
}
