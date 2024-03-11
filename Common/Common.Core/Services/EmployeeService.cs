using Common.Core.Services.Contracts;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _repository;

        public EmployeeService(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                return await _repository.GetEmployees();
            }
            catch
            {
                throw;
            }
        }

        //public List<LCTCheckListSubLogFailedMailModel> GetLCTDashboardFailedMailProcedure()
        //{
        //    var resultList = new List<LCTCheckListSubLogFailedMailModel>();
        //    try
        //    {
        //        var connectionString = _config.GetConnectionString("AppConnection");
        //        using (var connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = "GetLCTCheckListFailedMail";
        //                command.CommandType = CommandType.StoredProcedure;

        //                using (var reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var obj = new LCTCheckListSubLogFailedMailModel();

        //                        obj.Id = reader["Id"] == DBNull.Value ? 0 : (int)reader["Id"];
        //                        obj.ClaimNumber = reader["ClaimNumber"] == DBNull.Value ? string.Empty : (string)reader["ClaimNumber"];
        //                        obj.CreatedOn = (DateTime)reader["CreatedOn"];
        //                        obj.CreatedBy = reader["CreatedBy"] == DBNull.Value ? string.Empty : (string)reader["CreatedBy"];
        //                        obj.UpdatedBy = reader["UpdatedBy"] == DBNull.Value ? string.Empty : (string)reader["UpdatedBy"];
        //                        obj.SupervisorUsername = reader["SupervisorUsername"] == DBNull.Value ? string.Empty : (string)reader["SupervisorUsername"];
        //                        obj.LCTLeadershipHandling = reader["LCTLeadershipHandling"] == DBNull.Value ? string.Empty : (string)reader["LCTLeadershipHandling"];
        //                        obj.LogEntryType = reader["LogEntryType"] == DBNull.Value ? string.Empty : (string)reader["LogEntryType"];
        //                        obj.Resolution = reader["SupLogResolution"] == DBNull.Value ? string.Empty : (string)reader["SupLogResolution"];
        //                        obj.ResolutionOtherValue = reader["ResolutionOtherValue"] == DBNull.Value ? string.Empty : (string)reader["ResolutionOtherValue"];
        //                        obj.K250Payment = reader["SupLogK250Payment"] == DBNull.Value ? string.Empty : (string)reader["SupLogK250Payment"];
        //                        obj.K250PaymentOtherValue = reader["K250PaymentOtherValue"] == DBNull.Value ? string.Empty : (string)reader["K250PaymentOtherValue"];

        //                        resultList.Add(obj);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message); // Display the exception message
        //    }
        //    return resultList;
        //}


    }
}
