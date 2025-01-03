using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClassLibraryPOSG
{
    public class Conceptos
    {
        public string AplicacionAct { get; set; }
        public string CodigoAct { get; set; }
        public string CodigoConc { get; set; }
        public string NombreConc { get; set; }
        public string ImputableConc { get; set; }
        public string Obs1Conc { get; set; }
        public string Obs2Conc { get; set; }

        // Método para consultar registros
        public List<Conceptos> Consultar(string comodin, string filtro1 = null, string filtro2 = null)
        {
            var lista = new List<Conceptos>();

            using (var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                using (var comando = new SqlCommand("POSG_GetConceptos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Comodin", comodin);
                    comando.Parameters.AddWithValue("@FILTRO1", (object)filtro1 ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@FILTRO2", (object)filtro2 ?? DBNull.Value);

                    try
                    {
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var concepto = new Conceptos
                                {
                                    AplicacionAct = reader["strAplicacion_act"] != DBNull.Value ? reader["strAplicacion_act"].ToString() : string.Empty,
                                    CodigoAct = reader["strCod_act"] != DBNull.Value ? reader["strCod_act"].ToString() : string.Empty,
                                    CodigoConc = reader["strCod_conc"] != DBNull.Value ? reader["strCod_conc"].ToString() : string.Empty,
                                    NombreConc = reader["strNombre_conc"] != DBNull.Value ? reader["strNombre_conc"].ToString() : string.Empty,
                                    ImputableConc = reader["strImputable_conc"] != DBNull.Value ? reader["strImputable_conc"].ToString() : string.Empty,
                                    Obs1Conc = reader["strObs1_conc"] != DBNull.Value ? reader["strObs1_conc"].ToString() : string.Empty,
                                    Obs2Conc = reader["strObs2_conc"] != DBNull.Value ? reader["strObs2_conc"].ToString() : string.Empty
                                };
                                lista.Add(concepto);
                            }
                        }
                    }
                    catch (SqlException er)
                    {
                        Console.WriteLine($"Error SQL: {er.Message}");
                    }
                }
            }

            return lista;
        }

          }
}
