using CrudUser.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.Repositorios
{
    public class PaisRepositorio
    {
        public List<PaisListaModelo> ObtenerPaises()
        {
            var listaPaises = new List<PaisListaModelo>();
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_obtener_pais", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoPais = new PaisListaModelo()
                    {
                        Id = (int)reader["cod_pais"],
                        Nombre = reader["nom_pais"].ToString()
                    };

                    listaPaises.Add(nuevoPais);
                }
            }

            return listaPaises;
        }

        public void InsertPais(string nombre)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_crear_pais", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public PaisListaModelo ObtenerPaisPorId(int id)
        {
            var pais = new PaisListaModelo();
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_buscar_pais_por_id", sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoPais = new PaisListaModelo()
                    {
                        Id = (int)reader["cod_pais"],
                        Nombre = reader["nom_pais"].ToString()
                    };

                    pais = nuevoPais;
                }
            }

            return pais;
        }

        public void ActualizarPais(int id, string nombre)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_actualizar_pais", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public void EliminarPais(int id)
        {
            string connectionString = "server=localhost;database=SistemaBorrador2Db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_eliminar_pais", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
