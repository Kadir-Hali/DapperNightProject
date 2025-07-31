using Dapper;
using DapperNightProject.Context;
using DapperNightProject.Dtos.DepartmentDtos;

namespace DapperNightProject.Services.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DapperContext _context;

        public DepartmentService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            string query = "insert into TblDepartment (DepartmentName) values (@p1)";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", createDepartmentDto.DepartmentName);
            var conn = _context.CreateConnection();
            await conn.ExecuteAsync(query, parameters);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            string query = "Delete from TblDepartment where DepartmentId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var conn = _context.CreateConnection();
            await conn.ExecuteAsync(query, parameters);
        }

        public async Task<List<ResultDepartmentDto>> GetAllDepartmentAsync()
        {
            string query = "select * from TblDepartment";
            var conn = _context.CreateConnection();
            var values = await conn.QueryAsync<ResultDepartmentDto>(query);
            return values.ToList();
        }

        public async Task<GetDepartmentByIdDto> GetDepartmentByIdAsync(int id)
        {
            string query = "select * from TblDepartment where DepartmentId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var conn = _context.CreateConnection();
            var values = await conn.QueryFirstAsync<GetDepartmentByIdDto>(query, parameters);
            return values;
        }

        public async Task UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto)
        {
            string query = "Update TblDepartment set DepartmentName=@departmentName where DepartmentId = @departmentId";
            var parameters = new DynamicParameters();
            parameters.Add("@departmentName", updateDepartmentDto.DepartmentName);
            parameters.Add("@departmentId", updateDepartmentDto.DepartmentId);
            var conn = _context.CreateConnection();
            await conn.ExecuteAsync(query, parameters);
        }
    }
}
