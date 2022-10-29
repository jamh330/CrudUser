using CrudUser.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.Repositorios
{
    public class UsuarioRepositorio
    {
        public void InsertUsuario(string nombreUsuario, string nombres, string apellidos, int rolId, int paisId, string password)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_insertar_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombreUsuario", nombreUsuario));
            cmd.Parameters.Add(new SqlParameter("@nombres", nombres));
            cmd.Parameters.Add(new SqlParameter("@apellidos", apellidos));
            cmd.Parameters.Add(new SqlParameter("@rolId", rolId));
            cmd.Parameters.Add(new SqlParameter("@paisId", paisId));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            sql.Open();
            cmd.ExecuteNonQuery();

        }

        public List<UsuarioListaModel> obtenerUsuarios()
        {
            var listaUsuarios = new List<UsuarioListaModel>();
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_obtener_usuarios", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoUsuario = new UsuarioListaModel()
                    {
                        Id = (int)reader["Id"],
                        nombreUsuario = reader["nombreUsuario"].ToString(),
                        nombres = reader["nombres"].ToString(),
                        apellidos = reader["apellidos"].ToString(),
                        RolId = (int)reader["rolId"],
                        PaisId = (int)reader["cod_pais"],
                        Rol = reader["rolname"].ToString(),
                        Pais = reader["nom_pais"].ToString()
                    };

                    listaUsuarios.Add(nuevoUsuario);
                }
            }

            return listaUsuarios;
        }

        public UsuarioListaModel obtenerUsuarioPorId(int id)
        {
            var usuario = new UsuarioListaModel();
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_obtener_usuario_por_id", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoUsuario = new UsuarioListaModel()
                    {
                        Id = (int)reader["Id"],
                        nombreUsuario = reader["nombreUsuario"].ToString(),
                        nombres = reader["nombres"].ToString(),
                        apellidos = reader["apellidos"].ToString(),
                        RolId = (int)reader["rolId"],
                        PaisId = (int)reader["paisId"]
                    };

                    usuario = nuevoUsuario;
                }
            }

            return usuario;
        }

        public bool nombreUsuarioExiste(string nombreUsuario)
        {
            var respuesta = false;
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_check_nombre_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombreUsuario", nombreUsuario));
            sql.Open();
            var resultadoQuery = (int)cmd.ExecuteScalar();
            if (resultadoQuery > 0)
            {
                respuesta = true;
            }
            return respuesta;
        }
        public void ActualizarUsuario(int id, string nombres, string apellidos, int rolId, int paisId)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_actualizar_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@nombres", nombres));
            cmd.Parameters.Add(new SqlParameter("@apellidos", apellidos));
            cmd.Parameters.Add(new SqlParameter("@rolId", rolId));
            cmd.Parameters.Add(new SqlParameter("@paisId", paisId));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public void ActualizarPassword(int id, string password)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_cambio_password", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public void EliminarUsuario(int id)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_eliminar_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

    }
}
