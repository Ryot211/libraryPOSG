using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ClassLibraryPOSG
{
    public class ObjetivosFases
    {
        public string IdObjF { get; set; }
        public string IdInfoF { get; set; }
        public string DescripCorta { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string Aprueba { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public string UserLog { get; set; }
        public DateTime FechaLog { get; set; }
        public int Fase { get; set; }

        // Insertar un registro usando parámetros individuales
        public void Insertar(string idObjF, string idInfoF, string descripCorta, string descripcion, bool estado, string aprueba, DateTime fechaAprobacion, string userLog, DateTime fechaLog, int fase)
        {
            using (var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                using (var comando = new SqlCommand("POSG_AddOBJETIVOSFASES", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdObjF", idObjF);
                    comando.Parameters.AddWithValue("@IdInfoF", idInfoF);
                    comando.Parameters.AddWithValue("@DescripCorta", descripCorta);
                    comando.Parameters.AddWithValue("@Descripcion", descripcion);
                    comando.Parameters.AddWithValue("@Estado", estado);
                    comando.Parameters.AddWithValue("@Aprueba", aprueba);
                    comando.Parameters.AddWithValue("@FechaAprobacion", fechaAprobacion);
                    comando.Parameters.AddWithValue("@UserLog", userLog);
                    comando.Parameters.AddWithValue("@FechaLog", fechaLog);
                    comando.Parameters.AddWithValue("@Fase", fase);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        // Insertar un registro usando un objeto completo
        public void Insertar(ObjetivosFases objetivoFase)
        {
            Insertar(objetivoFase.IdObjF, objetivoFase.IdInfoF, objetivoFase.DescripCorta, objetivoFase.Descripcion, objetivoFase.Estado, objetivoFase.Aprueba, objetivoFase.FechaAprobacion, objetivoFase.UserLog, objetivoFase.FechaLog, objetivoFase.Fase);
        }

        // Consultar todos los registros
        public List<ObjetivosFases> ConsultarTodos()
        {
            return Consultar("ALL", null, null);
        }

        // Consultar con filtros
        public List<ObjetivosFases> Consultar(string comodin, string filtro1 = null, string filtro2 = null)
        {
            var lista = new List<ObjetivosFases>();

            using (var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                using (var comando = new SqlCommand("POSG_GetObjetivosFases", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Comodin", comodin);
                    comando.Parameters.AddWithValue("@FILTRO1", (object)filtro1 ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@FILTRO2", (object)filtro2 ?? DBNull.Value);

                    conexion.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var objetivoFase = new ObjetivosFases
                            {
                                IdObjF = reader["strId_objf"] != DBNull.Value
                                         ? reader["strId_objf"].ToString()
                                         : string.Empty,
                                IdInfoF = reader["strId_infof"] != DBNull.Value
                                          ? reader["strId_infof"].ToString()
                                          : string.Empty,
                                DescripCorta = reader["strDescripCort_objf"] != DBNull.Value
                                               ? reader["strDescripCort_objf"].ToString()
                                               : string.Empty,
                                Descripcion = reader["strDescripcion_objf"] != DBNull.Value
                                              ? reader["strDescripcion_objf"].ToString()
                                              : string.Empty,
                                Estado = reader["bitEstado_objf"] != DBNull.Value
                                         && Convert.ToBoolean(reader["bitEstado_objf"]),
                                Aprueba = reader["strAprueba_objf"] != DBNull.Value
                                          ? reader["strAprueba_objf"].ToString()
                                          : string.Empty,
                                FechaAprobacion = reader["dtFechaAprob_objf"] != DBNull.Value
                                                  ? Convert.ToDateTime(reader["dtFechaAprob_objf"])
                                                  : DateTime.MinValue,
                                UserLog = reader["strUserLog_objf"] != DBNull.Value
                                          ? reader["strUserLog_objf"].ToString()
                                          : string.Empty,
                                FechaLog = reader["dtFechaLog_objf"] != DBNull.Value
                                           ? Convert.ToDateTime(reader["dtFechaLog_objf"])
                                           : DateTime.MinValue,
                                Fase = reader["strFase_objf"] != DBNull.Value
                                       ? Convert.ToInt32(reader["strFase_objf"])
                                       : 0
                            };
                            lista.Add(objetivoFase);
                        }

                    }
                }
            }

            return lista;
        }

        // Actualizar un registro
        public void Actualizar(string idObjF, string descripcion, bool estado, DateTime fechaAprobacion)
        {
            using (var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                using (var comando = new SqlCommand("POSG_UpdateOBJETIVOSFASES", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdObjF", idObjF);
                    comando.Parameters.AddWithValue("@Descripcion", descripcion);
                    comando.Parameters.AddWithValue("@Estado", estado);
                    comando.Parameters.AddWithValue("@FechaAprobacion", fechaAprobacion);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        // Actualizar usando un objeto
        public void Actualizar(ObjetivosFases objetivoFase)
        {
            Actualizar(objetivoFase.IdObjF, objetivoFase.Descripcion, objetivoFase.Estado, objetivoFase.FechaAprobacion);
        }

        // Eliminar un registro
        public void Eliminar(string idObjF)
        {
            using (var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                using (var comando = new SqlCommand("POSG_DeleteOBJETIVOSFASES", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdObjF", idObjF);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
