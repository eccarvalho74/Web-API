using Newtonsoft.Json;
using System.Text;

namespace Certificados.Infra.Integration.Util
{
    /// <summary>
    /// Classe responsavel por comunicar com as APIs
    /// </summary>
    /// <typeparam name="TEntity">Onde T representa um classe</typeparam>
    public class APIClient<TEntity> where TEntity : class, new()
    {

        #region Propriedades      
        /// <summary>
        /// Armazena o TOKEN a ser enviar para API
        /// </summary>
        public string Token { get; set; }

        #endregion Propriedades

        #region Construtor da classe

        public APIClient(string token = "")
        {
            this.Token = token;
        }

        public APIClient()
        {
        }

        #endregion Construtor da classe

        #region DELETE

        /// <summary>
        /// Envia requisição HTTP Delete para excluir objeto
        /// </summary>
        /// <param name="URI">URL completa para excluir objeto</param>
        /// <returns>True = sucesso; False= Falha na exclusão</returns>
        public async Task<bool> DELETE(string URI)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 5, 0);
                if (!string.IsNullOrEmpty(this.Token))
                    client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", this.Token));


                var result = await client.DeleteAsync(URI);
                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        #endregion DELETE

        #region GET

        /// <summary>
        /// Envia requisição HTTP GET solicitando relação de objecto que atendam a condição
        /// <param name="URI">URL completa contendo ou não o id a ser retornado. Se id não informado retorna todos registro</param>
        /// <returns>Lista de objetos</returns>
        public async Task<List<TEntity>> GET(string URI)
        {
            List<TEntity> obj = new List<TEntity>();
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 5, 0);
                if (!string.IsNullOrEmpty(this.Token))
                    client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", this.Token));


                HttpResponseMessage response = await client.GetAsync(URI);
                if (response.IsSuccessStatusCode)
                {
                    var ret = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<List<TEntity>>(ret);
                    return obj;
                }
                return obj;
            }
        }

        /// <summary>
        /// Envia requisição HTTP GET solicitando relação de objecto que atendam a condição
        /// <param name="URI">URL completa contendo ou não o id a ser retornado.
        /// </param>
        /// <returns>objeto</returns>
        ///

        public async Task<TEntity> GETSingle(string URI)
        {
            TEntity obj = new TEntity();
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 5, 0);
                if (!string.IsNullOrEmpty(this.Token))
                    client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", this.Token));


                HttpResponseMessage response = await client.GetAsync(URI);
                if (response.IsSuccessStatusCode)
                {
                    var ret = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<TEntity>(ret);
                    return obj;
                }
                return obj;
            }
        }

        #endregion GET

        #region POST

        /// <summary>
        /// Envia requisição HTTP POST para incluir objeto
        /// </summary>
        /// <param name="URI">Caminho da url </param>
        /// <param name="obj">Objecto a ser incluido no banco</param>
        /// <returns>True/False</returns>
        public async Task<bool> POST(string URI, TEntity obj)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 5, 0);
                if (!string.IsNullOrEmpty(this.Token))
                    client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", this.Token));


                var objSerializado = JsonConvert.SerializeObject(obj);
                var content = new StringContent(objSerializado, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(URI, content);
                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="URI"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<TEntity> POSTWithResult<T>(string URI, T obj) where T : class
        {
            TEntity objRetorno = new TEntity();
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 5, 0);

                if (!string.IsNullOrEmpty(this.Token))
                    client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", this.Token));

                var objSerializado = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                var content = new StringContent(objSerializado, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(URI, content);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    objRetorno = JsonConvert.DeserializeObject<TEntity>(result);

                }
                else
                {
                    throw new Exception(result.ToString());

                }
                return objRetorno;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="URI"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<TEntity> POSTWithResult(string URI, object obj)
        {
            TEntity objRetorno = new TEntity();
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 5, 0);
                client.DefaultRequestHeaders.Add("token", this.Token);

                var objSerializado = JsonConvert.SerializeObject(obj);
                var content = new StringContent(objSerializado, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(URI, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    objRetorno = JsonConvert.DeserializeObject<TEntity>(result);
                    return objRetorno;
                }
                return objRetorno;
            }
        }

        #endregion POST

        #region PUT

        /// <summary>
        /// Envia requisição HTTP PUT para atualizar objeto
        /// </summary>
        /// <param name="URI">Caminho da url </param>
        /// <param name="obj">Objecto a ser editado</param>
        /// <returns>True/False</returns>
        public async Task<bool> PUT(string URI, TEntity obj)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 5, 0);
                client.DefaultRequestHeaders.Add("token", this.Token);

                var objSerializado = JsonConvert.SerializeObject(obj);
                var content = new StringContent(objSerializado, Encoding.UTF8, "application/json");
                var result = await client.PutAsync(URI, content);
                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        #endregion PUT

        public void Dispose()
        {
        }
    }
}
