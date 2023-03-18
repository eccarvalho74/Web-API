using Certificados.Domain.Core.dto;
using Certificados.Domain.Dto.Requests;
using Certificados.Domain.Dto.Responses;
using Certificados.Infra;
using Certificados.Models.Certificados;
using Certificados.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Services.services
{
    public class CertificadoService : ICertificadoService
    {
        private List<ConfiguracaoEspecifica> _configuracaoEspecifica;
        private string ConnectionString;

        private readonly ApplicationContext context;
        public CertificadoService(ApplicationContext context, IConfiguration Configuration)
        {
            _configuracaoEspecifica = Configuration.GetSection("ConfiguracaoEspecifica").Get<List<ConfiguracaoEspecifica>>();
            ConnectionString = Configuration.GetSection("connectionStrings:appMassificadosDbConnection").Value;
            this.context = context;

            
        }

        public SqlConnection Connect()
        {
            return new SqlConnection(ConnectionString);

        }

        public async  Task<ServiceResult<IEnumerable<ListaCertificadoResponse>>> ListaOpcoes(string cpf, string dt_nascimento, string corretora = null)
        {
            var retorno = new ServiceResult<IEnumerable<ListaCertificadoResponse>>();
            var mensage = string.Empty;

            try
            {

                IEnumerable<ListaCertificadoResponse> listaOpcoes = new List<ListaCertificadoResponse>();



                using (SqlCommand cmd = new SqlCommand("proc_MAS_AGIBANK_2070U00", Connect()))
                {
                    using (SqlDataAdapter rdr = new SqlDataAdapter())
                    {
                        using (DataTable dt = new DataTable())
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 2120;
                            cmd.Parameters.AddWithValue("@P_CPF", cpf);
                            cmd.Parameters.AddWithValue("@P_DTNASCIMENTO", dt_nascimento);
                            cmd.Parameters.AddWithValue("@P_TIPO_PARCEIRO", corretora);
                            Connect().Open();

                            rdr.SelectCommand = cmd;
                            rdr.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {

                                listaOpcoes = (from DataRow dr in dt.Rows
                                               select new ListaCertificadoResponse()
                                               {
                                                   NumCertificado = dr["NumCertificado"].ToString(),
                                                   NomeSegurado = dr["NomeCliente"].ToString(),
                                                   DTNascimentoSegurado = dr["DTNascimento"].ToString(),
                                                   NrCnpjCPFSegurado = dr["NrCnpjCPF"].ToString(),
                                                   DtIniVigencia = dr["DtIniVigencia"].ToString(),
                                                   DtFimVigencia = dr["DtFimVigencia"].ToString(),
                                                   TipoPlano = dr["Tipo_plano"].ToString(),
                                                   Flag = dr["FLAG"].ToString(),
                                                   CdCliente = dr["cd_cliente"].ToString(),
                                                   NrApoliceExt = dr["nr_apolice_ext"].ToString()
                                               }).ToList();

                                retorno.Data = listaOpcoes;

                            }
                            else
                            {
                                mensage = "Não retornou certificado.";

                            }

                            retorno.Data = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.Data = null;
                throw ex;
            }
            finally
            {
                Connect().Close();
               

            }
            return retorno;

        }

        public async   Task<ServiceResult<Certificado>> ListaCertificado(CertificadoRequest certificadoRequestDTO)
        {
            var result = new ServiceResult<Certificado>();
            var mensage = string.Empty;
            List<Certificado> certificadosResult = new List<Certificado>();
            Certificado certificado = new Certificado();
            try
            {
                using (SqlCommand cmd = new SqlCommand(@"[dbo].[proc_sindnapi_resultados]", Connect()))
                {
                    using (SqlDataAdapter rdr = new SqlDataAdapter())
                    {
                        using (DataTable dt = new DataTable())
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 2120;
                            // cmd.Parameters.AddWithValue("@P_CD_CLIENTE", certificadoRequestDTO.CodigoCliente);
                            // cmd.Parameters.AddWithValue("@P_DTNASCIMENTO", certificadoRequestDTO.DataNascimento);
                            // cmd.Parameters.AddWithValue("@P_TIPO_PARCEIRO ", certificadoRequestDTO.TipoParceiro);
                            //  cmd.Parameters.AddWithValue("@P_TIPO_PLANO ", certificadoRequestDTO.TipoPlano?.Split(" ")?.Last());
                            //  cmd.Parameters.AddWithValue("@P_APOLICE_EXT ", certificadoRequestDTO.ApoliceExt);
                            cmd.Parameters.AddWithValue("@P_NUM_CERTIFICADO  ", certificadoRequestDTO.NumeroCertificado);
                            Connect().Open();

                            // cmd.Connection.Open();

                            // var a = cmd.Connection.State.ToString();

                            // SqlDataReader dttr = cmd.ExecuteReader();
                            rdr.SelectCommand = cmd;
                            rdr.Fill(dt);

                            var configuracaoEspecifica = _configuracaoEspecifica.FirstOrDefault(x => x.NApoliceEtx == certificadoRequestDTO.ApoliceExt);
                            if (dt.Rows.Count > 0)
                            {

                                //foreach (DataRow row in dt.Rows)

                                // {


                                // IList<Certificado> listaCertificado = new List<Certificado>();
                                DataRow dr = dt.Rows[0];

                                var endereco = new Endereco(
                                    dr["UF"].ToString(),
                                    dr["Cidade"].ToString(),
                                    dr["cep"].ToString(),
                                    dr["Endereco"].ToString(),
                                    dr["Numero"].ToString(),
                                    dr["Complemento"].ToString());
                                var segurado = new Pessoa(
                                    dr["NomeCliente"].ToString(),
                                    dr["DTNascimento"].ToString(),
                                    dr["NrCnpjCPF"].ToString(),
                                    dr["OrgaoExp"].ToString(),
                                    dr["Pais"].ToString(),
                                    dr["TelResidencial"].ToString(),
                                    endereco);
                                var subestipulante = new Subestipulante(
                                    dr["RazaoSub"].ToString(),
                                    dr["CnpjSub"].ToString(),
                                    dr["Telefone_sub"].ToString(),
                                    dr["EnderecoSub"].ToString(),
                                    dr["NumeroSub"].ToString(),
                                    dr["ComplementoSub"].ToString(),
                                    dr["CepSub"].ToString(),
                                    dr["CidadeSub"].ToString(),
                                    dr["UfSub"].ToString()
                                   );
                                certificado = new Certificado(
                                   configuracaoEspecifica,
                                   dr["NumCertificado"].ToString(),
                                   dr["CertificadoIndividual"].ToString(),
                                   dr["NR_SORTE"].ToString(),
                                   "",//dr["NR_SORTE_DEZEMBRO"].ToString(),
                                   dr["NumPropostaEstipulante"].ToString(),
                                   dr["DtIniVigencia"].ToString(),
                                   dr["DtFimVigencia"].ToString(),
                                   dr["DTEmissao"].ToString(),
                                   DateTime.Parse(dr["DT_ASSINATURA"].ToString()),
                                   dr["VL_LIQUIDO_COB1"].ToString(),
                                   dr["VL_LIQUIDO_COB2"].ToString(),
                                   dr["VL_LIQ_MENSALCOB1"].ToString(),
                                   "", // Valor liquido mensal
                                   dr["CAPSEGURADOCOB1"].ToString(),
                                   dr["CAPSEGURADOCOB2"].ToString(),
                                   dr["premioLiquido"].ToString(),
                                   dr["premioBruto"].ToString(),
                                   dr["IOF"].ToString(),
                                   dr["VL_PROLABORE"].ToString(),
                                   dr["VL_LIQUIDO_COB3"].ToString(),
                                   dr["CAPSEGURADOCOB3"].ToString(),
                                   segurado,
                                   subestipulante); ;

                                if (certificadoRequestDTO.TipoParceiro.ToUpper().Contains("AGIBANK"))
                                {
                                    foreach (DataRow beneficiario in dt.Rows)
                                    {
                                        certificado.Beneficiarios.Add(
                                            new Beneficiario(beneficiario["NomeBeneficiario"].ToString(),
                                                             beneficiario["CpfBeneficiario"].ToString(),
                                                             beneficiario["ParenteBene"].ToString(),
                                                             beneficiario["ParticiapacaoBene"].ToString()));
                                    }

                                }

                                if (certificadoRequestDTO.TipoParceiro.ToUpper().Contains("SINDNAPI"))
                                {
                                    foreach (DataRow parcela in dt.Rows)
                                    {
                                        certificado.Parcelas.Add(
                                            new Parcela(parcela["Nr_Parcela"].ToString(),
                                                             parcela["Dt_Pagamento"].ToString(),
                                                             parcela["vl_is"].ToString(),
                                                             parcela["vl_premio_total"].ToString()));
                                    }
                                    certificado.Parcelas = certificado.Parcelas.OrderBy(x => x.NrParcela).ToList();
                                }
                                // certificadosResult.Add(certificado);
                                //}

                            }


                            result.Data=  certificado;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ;
                throw ex;
            }
            finally
            {
                Connect().Close();

            }

            return result;

        }

        public async Task<ServiceResult<List<Certificado>>> ObterNrCertificados()
        {
            var result = new ServiceResult<List<Certificado>>();
            var mensage = string.Empty;
            List<Certificado> certificadosResult = new List<Certificado>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(@"dbo.proc_sindnapi", Connect()))
                {
                    using (SqlDataAdapter rdr = new SqlDataAdapter())
                    {
                        using (DataTable dt = new DataTable())
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 2120;
                            Connect().Open();


                            rdr.SelectCommand = cmd;
                            rdr.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {

                                foreach (DataRow row in dt.Rows)
                                {
                                    certificadosResult.Add(new Certificado
                                    {
                                        NumeroCertificado = row["NumCertificado"].ToString()
                                    }); ;
                                }

                            }

                            result.Data =  certificadosResult;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ;
                throw ex;
            }
            finally
            {
                Connect().Close();

            }

            return result;
        }

        public async Task<ServiceResult<bool>> Upload(IFormFile fileUpload)
        {
            var result = new ServiceResult<bool>();
            string path = "";
            try
            {
                if (fileUpload.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, fileUpload.FileName), FileMode.Create))
                    {
                        await fileUpload.CopyToAsync(fileStream);
                    }



                    using (var reader = new StreamReader(fileUpload.OpenReadStream()))
                    {
                        int i = 1;
                        //read CSV file line by line until End of stream
                        while (!reader.EndOfStream)
                        {
                            //read single line
                            var line = reader.ReadLine();
                            //split values by ','
                            var values = line.Split(',');

                            if (i == 1)
                            {
                                //read and print headers at first
                                var a =(values);
                                i++;
                            }
                            else
                            {
                                //read and print headers rows
                                var b = (values);
                            }
                            Console.WriteLine();
                        }
                    }




                    result.Data= true;
                }
                else
                {
                    result.Data =  false;
                }

                return result; 
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no processo de carregemento do arquivo", ex);
            }
        }


        //Upload file from HTTPClient
        //public static async Task<string> Upload(string uri, string pathFile)
        //{

        //    byte[] bytes = System.IO.File.ReadAllBytes(pathFile);

        //    using (var content = new ByteArrayContent(bytes))
        //    {
        //        content.Headers.ContentType = new MediaTypeHeaderValue("*/*");

        //        //Send it
        //        var response = await nftClient.PostAsync(uri, content);
        //        response.EnsureSuccessStatusCode();
        //        Stream responseStream = await response.Content.ReadAsStreamAsync();
        //        StreamReader reader = new StreamReader(responseStream);
        //        return reader.ReadToEnd();
        //    }
        //}
    }
}
