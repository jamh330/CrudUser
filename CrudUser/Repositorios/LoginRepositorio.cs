using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.Repositorios
{
    public class LoginRepositorio
    {
        public bool UsuarioExiste(string nombreUsuario, string password)
        {
            var respuesta = false;
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_check_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombreUsuario", nombreUsuario));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            sql.Open();
            var resultadoQuery = (int) cmd.ExecuteScalar();
            if (resultadoQuery > 0)
            {
                respuesta = true;
            }
            return respuesta;
        }
    }
}
