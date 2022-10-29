using CrudUser.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.Repositorios
{
    public class RolRepositorio
    {
        public void InsertRol(string nombre)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_insertar_rol", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public List<RolListaModelo> obtenerRoles()
        {
            var listaRoles = new List<RolListaModelo>();
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_obtener_roles", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoRol = new RolListaModelo()
                    {
                        Id = (int)reader["id"],
                        Nombre = reader["nombre"].ToString()
                    };

                    listaRoles.Add(nuevoRol);
                }
            }

            return listaRoles;
        }

        public RolListaModelo obtenerRolPorId(int id)
        {
            var rol = new RolListaModelo();
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_buscar_rol_por_id", sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoRol = new RolListaModelo()
                    {
                        Id = (int)reader["id"],
                        Nombre = reader["nombre"].ToString()
                    };

                    rol = nuevoRol;
                }
            }

            return rol;
        }

        public void ActualizarRol(int id, string nombre)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_actualizar_rol", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public void EliminarRol(int id)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_eliminar_rol", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
