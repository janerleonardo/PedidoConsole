using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using Hefestos.Frontend.Core.HttpClient;
using System.Diagnostics;
using PedidosConsole.Model;

namespace PedidosConsole.Logica
{
    public static class WebApiEE
    {
		public static string WEBAPIEE_NO_SSL = "webapiee.nosslcheck";
		public static string WEBAPIEE_HOST = "webapiee.server";
		public static string WEBAPIEE_CONNECTION = "webapiee.connection";
		public static string WEBAPIEE_INTERNAL_TOKEN_PROPERTY = "_webapiee_login_token";

		private const string METHOD_TOKEN = "/Token"; 
		private const string METHOD_PEDIDO= "/api/pedidos/crear";
		private const string METHOD_LOGOUT = "/api/sesion/salir";
					    
		private const string AUTHORIZATION_HEADER = "Authorization";
					    

					    
		private const string  RESPONSE_PARAM_ACCESSTOKEN = "access_token";


		public static async Task<dynamic> GetTokenSiesaEnterpriseApi(string vStrUrlToken, string vStrConexion, string vStrCia, string vStrUser, string vStrPassword)
		{
			
			//Aqui se hace la peticion para obtener 
			Dictionary<string, string> LoginDict = new Dictionary<string, string>();
			LoginDict.Add("grant_type", "password");
			LoginDict.Add("connection", vStrConexion);
			LoginDict.Add("company", vStrCia);
			LoginDict.Add("username", vStrUser);
			LoginDict.Add("password", vStrPassword);

			string urlToken = vStrUrlToken + METHOD_TOKEN;
			var response = await HttpRequestFactory.PostXWwwFormUrlencoded(urlToken, LoginDict);
			var data = response.Content.ReadAsStringAsync();
			// Convierte la cadena a Json
			JObject jsonDescargas = JObject.Parse(data.Result);

			return jsonDescargas;
		}


		public static async Task<dynamic> CrearPedido(PedidoModel pedidoEncabezadoModel, string vStrURLUNOEE, string vStrConexion, string vStrCia, string vStrUser, string vStrPassword)
		{


			var parametrosEnviar = GetParamsEnviarUrlEncode(pedidoEncabezadoModel);

			string urlPedido = vStrURLUNOEE + METHOD_PEDIDO;


			//Se solicita el Token del Api de  Siesa Enterprise
			var JObjetTokenApi = Task.Run(async () => await GetTokenSiesaEnterpriseApi(vStrURLUNOEE, vStrConexion, vStrCia, vStrUser, vStrPassword)).GetAwaiter().GetResult();
			//var JObjetTokenApi = Task.Run(async () => await GetTokenApiSiesa(user, pass)).GetAwaiter().GetResult();
			string bearerToken = JObjetTokenApi["access_token"];

			//Convert to Direc

			var response = await HttpRequestFactory.PostXWwwFormUrlencoded(urlPedido, parametrosEnviar, bearerToken);
			var data = response.Content.ReadAsStringAsync();
			// Convierte la cadena a Json
			JObject jsonDescargas = JObject.Parse(data.Result);

			return jsonDescargas;
		}

		public static List<KeyValuePair<string, string>> GetParamsEnviarUrlEncode(object obj)
		{
			return ToListKeyValuePair(ToDictionary(obj));
		}
		private static List<KeyValuePair<string, string>> ToListKeyValuePair(IDictionary<string, object> dictionary, List<KeyValuePair<string, string>> parametros = null, string prefijo = "")
		{
			if (parametros == null)
			{
				parametros = new List<KeyValuePair<string, string>>();
			}


			//Aqui se debe de recorrer el diccionario para crear el KeyValuePair
			foreach (KeyValuePair<string, object> data in dictionary)
			{


				string vStrKey = data.Key;
				if (!"".Equals(prefijo))
				{
					vStrKey = prefijo + "[" + vStrKey + "]";
				}
				string info = "Tipo de dato=> " + data.Value.GetType().ToString() + " key: " + vStrKey + " isClass: " + data.Value.GetType().IsClass;
				Debug.WriteLine(info);
				try
				{
					if (data.Value.GetType().IsClass && !data.Value.GetType().Equals(typeof(string)))
					{
						//ObjectToDictionaryHelper.ToDictionary()
						List<object> lista = new List<object>((IEnumerable<object>)data.Value);
						parametros = ToListFromCollection(parametros, lista, vStrKey);
					}
					else if (data.Value.GetType().Equals(typeof(System.DateTime)))
					{
						parametros.Add(new KeyValuePair<string, string>(vStrKey, ((DateTime)data.Value).ToString("yyyy-MM-dd HH:mm:ss")));
					}
					else
					{
						parametros.Add(new KeyValuePair<string, string>(vStrKey, data.Value.ToString()));
					}

				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}

			}

			return parametros;
		}
		private static List<KeyValuePair<string, string>> ToListFromCollection(List<KeyValuePair<string, string>> data, IList<object> lista, string prefijo = "")
		{
			int indice = 0;
			foreach (var obj in lista)
			{

				ToListKeyValuePair(ToDictionary(obj), data, prefijo + "[" + indice + "]");
				indice++;
			}

			return data;
		}

		private static IDictionary<string, object> ToDictionary(object obj)
		{
			//Dictionary<string, object> dictionary = new Dictionary<string, object>();

			return ObjectToDictionaryHelper.ToDictionary(obj);


		}

	}



}
