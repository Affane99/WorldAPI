using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using World.Application.Models;

namespace World.Application.Contracts.Infrastucture
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
