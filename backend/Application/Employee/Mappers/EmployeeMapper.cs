using Application.Employee.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Mappers
{
    public static class EmployeeMapper
    {
        //pretvaramo createproduccommand(nosi api podatke) u product
        public static Employees ToDomainEntity(this CreateEmployeesCommand command)
        {
            return new Employees
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PasswordHash = command.Password,
                IsAdmin = false
            };
        }

        public static Employees ToDomainEntity(this RegisterEmployeeCommand command)
        {
            return new Employees
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                //PasswordHash = command.Password,
                IsAdmin = false
            };
        }
    }
}
