using System.Collections.Generic;
using System.Threading.Tasks;
using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;
using System.Linq;
using Trinca.Soccer.Services.Util;
using Trinca.Soccer.Dto.Employee;
using Trinca.Soccer.Services.Mapping;
using Trinca.Soccer.Dto.Login;

namespace Trinca.Soccer.Services
{
    public interface IEmployeesService
    {
        Task<IEnumerable<EmployeeOutputDto>> GetAll();
        Task CreateAllFromSite();
        Task<IEnumerable<Employee>> GetAllFromSite();
        Task<LoginOutputDto> Login(string userName, string password);
        Task<EmployeeOutputDto> GetById(int id);
    }

    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IWebScraper _webScraper;

        public EmployeesService(IEmployeesRepository employeesRepository, IWebScraper webScraper)
        {
            _employeesRepository = employeesRepository;
            _webScraper = webScraper;
        }

        public async Task<IEnumerable<EmployeeOutputDto>> GetAll()
        {
            var employees = await _employeesRepository.GetAllAsync();
            return MappingConfig.Mapper().Map<List<EmployeeOutputDto>>(employees);
        }

        public async Task CreateAllFromSite()
        {
            var employees = await GetAllFromSite();

            foreach (var employee in employees)
            {
                var dbEmployee = _employeesRepository.GetAll(w => w.Username.Equals(employee.Username)).FirstOrDefault();

                if (dbEmployee != null) continue;

                employee.Password = Cryptography.GetMd5Hash("trinca137");
                await _employeesRepository.CreateAsync(employee);
            }
        }

        public async Task<IEnumerable<Employee>> GetAllFromSite()
        {
            return await _webScraper.GetTrincaWorkers(@"http://trin.ca");
        }

        public async Task<LoginOutputDto> Login(string userName, string password)
        {
            if (!_employeesRepository.GetAll().Any())
                await CreateAllFromSite();

            var employessFromSite = await GetAllFromSite();

            var hashPassword = Cryptography.GetMd5Hash(password);
            var employee = await _employeesRepository.GetLoggedUser(userName, hashPassword);

            if (employee == null)
                return new LoginOutputDto();

            if (employessFromSite.Select(w => w.Username).Contains(employee.Username))
                return MappingConfig.Mapper().Map<LoginOutputDto>(employee);

            await _employeesRepository.DeleteAsync(employee);
            return new LoginOutputDto();
        }

        public async Task<EmployeeOutputDto> GetById(int id)
        {
            var employee = await _employeesRepository.FindAsync(id);
            return MappingConfig.Mapper().Map<EmployeeOutputDto>(employee);
        }
    }
}
