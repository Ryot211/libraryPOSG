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
    public class InfoFases
    {
        public string IdInfoF { get; set; }
        public string IdIns { get; set; }
        public string IdPer { get; set; }
        public string AplicacionAct { get; set; }
        public string CodAct { get; set; }
        public string CodConc { get; set; }
        public DateTime FechaIns { get; set; }
        public bool Aprueba { get; set; }
        public DateTime FechaAprueba { get; set; }
        public string Revision { get; set; }
        public string UserLog { get; set; }
        public DateTime FechaLog { get; set; }

        // ---- Métodos CRUD ----

        // Insertar un registro usando parámetros individuales
        public void Insertar(string idInfoF, string idIns, string idPer, string aplicacionAct, string codAct, string codConc, DateTime fechaIns, bool aprueba, DateTime fechaAprueba, string revision, string userLog, DateTime fechaLog)
        {
            using (var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                using (var comando = new SqlCommand("POSG_AddINFOFASES", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdInfoF", idInfoF);
                    comando.Parameters.AddWithValue("@IdIns", idIns);
                    comando.Parameters.AddWithValue("@IdPer", idPer);
                    comando.Parameters.AddWithValue("@AplicacionAct", aplicacionAct);
                    comando.Parameters.AddWithValue("@CodAct", codAct);
                    comando.Parameters.AddWithValue("@CodConc", codConc);
                    comando.Parameters.AddWithValue("@FechaIns", fechaIns);
                    comando.Parameters.AddWithValue("@Aprueba", aprueba);
                    comando.Parameters.AddWithValue("@FechaAprueba", fechaAprueba);
                    comando.Parameters.AddWithValue("@Revision", revision);
                    comando.Parameters.AddWithValue("@UserLog", userLog);
                    comando.Parameters.AddWithValue("@FechaLog", fechaLog);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        // Insertar un registro usando un objeto completo
        public void Insertar(InfoFases infoFase)
        {
            Insertar(infoFase.IdInfoF, infoFase.IdIns, infoFase.IdPer, infoFase.AplicacionAct, infoFase.CodAct, infoFase.CodConc, infoFase.FechaIns, infoFase.Aprueba, infoFase.FechaAprueba, infoFase.Revision, infoFase.UserLog, infoFase.FechaLog);
        }

        // Consultar todos los registros
        public List<InfoFases> ConsultarTodos()
        {
            return Consultar("ALL", null, null, null);
        }

        // Consultar con filtros
        public List<InfoFases> Consultar(string comodin, string filtro1 = null, string filtro2 = null, bool? filtro3 = null)
        {
            var lista = new List<InfoFases>();

            using (var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                using (var comando = new SqlCommand("POSG_GetInfoFases", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Comodin", comodin);
                    comando.Parameters.AddWithValue("@FILTRO1", (object)filtro1 ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@FILTRO2", (object)filtro2 ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@FILTRO3", (object)filtro3 ?? DBNull.Value);

                    conexion.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var infoFase = new InfoFases
                            {
                                IdInfoF = reader["strId_infof"]?.ToString(),
                                IdIns = reader["strId_ins"]?.ToString(),
                                IdPer = reader["strId_per"]?.ToString(),
                                AplicacionAct = reader["strAplicacion_act"]?.ToString(),
                                CodAct = reader["strCod_act"]?.ToString(),
                                CodConc = reader["strCod_conc"]?.ToString(),
                                FechaIns = reader["dtFechaIns_infof"] != DBNull.Value
                                           ? Convert.ToDateTime(reader["dtFechaIns_infof"])
                                           : DateTime.MinValue,
                                Aprueba = reader["bitAprueba_infof"] != DBNull.Value
                                          && Convert.ToBoolean(reader["bitAprueba_infof"]),
                                FechaAprueba = reader["dtFechaApru_infof"] != DBNull.Value
                                               ? Convert.ToDateTime(reader["dtFechaApru_infof"])
                                               : DateTime.MinValue,
                                Revision = reader["strRevision_infof"] != DBNull.Value
                                           ? reader["strRevision_infof"].ToString()
                                           : string.Empty,
                                UserLog = reader["strUserLog_infof"] != DBNull.Value
                                          ? reader["strUserLog_infof"].ToString()
                                          : string.Empty,
                                FechaLog = reader["dtFechaLog_infof"] != DBNull.Value
                                           ? Convert.ToDateTime(reader["dtFechaLog_infof"])
                                           : DateTime.MinValue
                            };
                            lista.Add(infoFase);
                        }

                    }
                }
            }

            return lista;
        }

        // Actualizar un registro
        public void Actualizar(string idInfoF, string revision, bool aprueba, DateTime fechaAprueba)
        {
            using (var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                using (var comando = new SqlCommand("POSG_UpdateINFOFASES", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdInfoF", idInfoF);
                    comando.Parameters.AddWithValue("@Revision", revision);
                    comando.Parameters.AddWithValue("@Aprueba", aprueba);
                    comando.Parameters.AddWithValue("@FechaAprueba", fechaAprueba);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        // Actualizar usando un objeto
        public void Actualizar(InfoFases infoFase)
        {
            Actualizar(infoFase.IdInfoF, infoFase.Revision, infoFase.Aprueba, infoFase.FechaAprueba);
        }

        // Eliminar un registro
        public void Eliminar(string idInfoF)
        {
            using (var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                using (var comando = new SqlCommand("POSG_DeleteINFOFASES", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdInfoF", idInfoF);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
