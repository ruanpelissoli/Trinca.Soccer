using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;
using System.Linq;
using Trinca.Soccer.Services.Util;

namespace Trinca.Soccer.Services
{
    public interface IEmployeesServices
    {
        Task<IEnumerable<Employee>> GetAll();
        Task CreateAllFromSite();
        Task<IEnumerable<Employee>> GetAllFromSite();
        Task<string> Login(string userName, string password);
    }

    public class EmployeesServices : IEmployeesServices
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IWebScraper _webScraper;

        public EmployeesServices(IEmployeesRepository employeesRepository, IWebScraper webScraper)
        {
            _employeesRepository = employeesRepository;
            _webScraper = webScraper;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeesRepository.GetAllAsync();
        }

        public async Task CreateAllFromSite()
        {
            var employees = await GetAllFromSite();

            foreach (var employee in employees)
            {
                var dbEmployee = _employeesRepository.GetAll(w => w.Username.Equals(employee.Username)).FirstOrDefault();

                if (dbEmployee != null) continue;

                employee.Username = employee.Name.ToLower().Replace(" ", ".");
                employee.Password = Cryptography.GetMd5Hash("trinca137");
                await _employeesRepository.CreateAsync(employee);
            }
        }

        public async Task<IEnumerable<Employee>> GetAllFromSite()
        {
            return await _webScraper.GetTrincaWorkers(@"http://trin.ca");
        }

        public async Task<string> Login(string userName, string password)
        {
            if (!_employeesRepository.GetAll().Any())
                await CreateAllFromSite();

            var employessFromSite = await GetAllFromSite();

            var hashPassword = Cryptography.GetMd5Hash(password);
            var employee = await _employeesRepository.GetLoggedUser(userName, hashPassword);

            if (employee == null)
                return string.Empty;

            if (employessFromSite.Select(w => w.Username).Contains(employee.Username))
                return Guid.NewGuid().ToString();

            await _employeesRepository.DeleteAsync(employee);
            return string.Empty;
        }
    }
}
