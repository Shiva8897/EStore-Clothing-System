﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
    public interface IEmailService
    {
        void SendMailNotification(string toEmail, string subject, string body);
    }
}
