using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class ApiRepository
    {
        private readonly string _connectionString;

        public ApiRepository()
        {
            _connectionString =
                ConfigurationManager.ConnectionStrings["ApiDb"].ConnectionString;
        }

        public async Task InsertHttpRequestLogAsync(HttpRequestLog request)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("uspInsertRequestLog", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", request.Id);
                cmd.Parameters.AddWithValue("@UserName", request.UserName);
                cmd.Parameters.AddWithValue("@UserIpAddress", request.UserIpAddress);
                cmd.Parameters.AddWithValue("@HttpAction", request.HttpAction);
                cmd.Parameters.AddWithValue("@RequestUrl", request.RequestUrl);
                cmd.Parameters.AddWithValue("@RequestHeader", request.RequestHeader);
                cmd.Parameters.AddWithValue("@RequestBody", request.RequestBody);
                cmd.Parameters.AddWithValue("@UserAgent", request.UserAgent);
                cmd.Parameters.AddWithValue("@DeviceInfo", request.DeviceInfo);
                cmd.Parameters.AddWithValue("@BrowserInfo", request.BrowserInfo);
                cmd.Parameters.AddWithValue("@IsAnonymous", request.IsAnonymous);
                cmd.Parameters.AddWithValue("@IsAuthenticated", request.IsAuthenticated);
                cmd.Parameters.AddWithValue("@IsGuest", request.IsGuest);
                cmd.Parameters.AddWithValue("@IsSystem", request.IsSystem);
                cmd.Parameters.AddWithValue("@RequestTimeStamp", request.RequestTimeStamp);

                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }
        }

        public async Task FinalizeRequestLogAsync(HttpRequestLog request)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("uspFinalizeRequestLog", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", request.Id);
                cmd.Parameters.AddWithValue("@Response", request.Response);
                cmd.Parameters.AddWithValue("@ResponseBody", request.ResponseBody);
                cmd.Parameters.AddWithValue("@ResponseTimeStamp", request.ResposneTimeStamp);
                cmd.Parameters.AddWithValue("@RequestTotalTime", request.RequestTotalTime);

                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }
        }
    }
}