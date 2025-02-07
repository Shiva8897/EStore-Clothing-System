using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.EntityDtos
{
    public class LoginRes
    {
        public string Token {  get; set; }
        public string Role {  get; set; }   
    }
}
