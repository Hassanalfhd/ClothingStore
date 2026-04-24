using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Auth.Register
{
    public class RegisterCommand: IRequest<Result<Guid>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public RegisterCommand(string email, string password, string firstName, string lastName)
        {
            Email = email;  
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
