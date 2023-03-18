using Certificados.Domain.Core.dto;
using Certificados.Domain.Dto.Requests;
using Certificados.Domain.Dto.Responses;
using Certificados.Domain.Entities;
using Certificados.Domain.Interfaces;
using Certificados.Infra.Integration.Util;
using Certificados.Services.interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Services.services
{
    public  class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository repository;
        private readonly IConfiguration configuration;
        public UsuarioService(IUsuarioRepository repository,  IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }



        public async Task<ServiceResult<AutenticacaoUsuarioResponse>> AutenticarUsuario(AutenticacaoUsuarioRequest autenticacaoUsuarioRequest)
        {
            var result = new ServiceResult<AutenticacaoUsuarioResponse>();


            //ObterUsuario
               var usuario = (await repository.Get(x=> x.Email == autenticacaoUsuarioRequest.Email)).FirstOrDefault();

            //VerificarSenhaConfereComHash
            bool autenticado = false;
            if (usuario != null)
            {
               
                autenticado = GeradorDeSenha.ObterHash(autenticacaoUsuarioRequest.Senha, usuario.Salt) == usuario.Senha;
            }
            else
            {
                result.AddError("UC", "Usuário e/ou senha inválidos");
                return result;
            }

            if (!autenticado)
            {

                result.AddError("UC", "Usuário e/ou senha inválidos");
                return result;
            }
            else
            {
                //GerarTokenJwt
                //string token = GerarTokenJwt(autenticacaoUsuarioRequest.CPF, usuario.Email, usuario.Id.ToString(), usuario.Nome);
                string token = await GerarTokenJwtAsync(usuario);

            
                //RetornaDadosUsuario
                result.Data = new AutenticacaoUsuarioResponse
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,                    
                    Token = token,
                   
                };            


                return result;
            }
        }



        public virtual async Task<string> GerarTokenJwtAsync(Usuario usuario)
        // public virtual string GerarTokenJwt(string login, string email, string id, string nome)
        {
            try
            {

                
                string email = usuario.Email;
                string id = usuario.Id.ToString();
                string nome = usuario.Nome;

                //Definindo Role
                var usuarioRole = "Administrator";

                var _jwtSecurityKey = configuration.GetValue<string>("JwtSecurityKey");
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecurityKey);


                var Claims = new List<Claim>(){
                        new Claim(ClaimTypes.NameIdentifier, email),
                        new Claim(ClaimTypes.Email, email),
                        new Claim(ClaimTypes.PrimarySid, id),
                        new Claim(ClaimTypes.Name, nome),
                        new Claim(ClaimTypes.Role, "Padrao")
                    };

                if (!string.IsNullOrEmpty(usuarioRole))
                {
                    Claims.Add(new Claim(ClaimTypes.Role, usuarioRole));
                }

                //var coordenacacoes = await eventoPerfilUsuarioRepository.GetPermissionsByUsuarioId<EventoPerfilUsuarioResponse>(usuario.Id);
                //if (coordenacacoes != null)
                //{
                //    coordenacacoes.ToList()
                //           .ForEach((X) =>
                //           {
                //               Claims.Add(
                //               new Claim("CoordenacoesId", X.CoordenacaoId.ToString())
                //           );
                //           });
                //}

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(Claims.ToArray()),
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };


                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    String.Format("Erro ao gerar token. ({0}: {1}) - {2}",
                    nameof(usuario.Email), usuario.Email,
                    ex.StackTrace)
                 );

            }
        }








        public async Task<ServiceResult<IEnumerable<UsuarioResponse>>> Get()
        {
            var retorno = new  ServiceResult<IEnumerable<UsuarioResponse>>();

            retorno.Data = await  repository.Get<UsuarioResponse>(); 

            return retorno;
        }
    }
}
