using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebService.DBAccess;
using WebService.Models;

namespace WebService.Controllers
{
    public class UserController : ApiController
	{
		[System.Web.Http.ActionName("UserValidation")]
		public HttpResponseMessage UserValidation([FromBody]string data)
        {
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();

			GetUserResponse userResponse = new GetUserResponse();

			User user = UserDBAccess.ValidateUser(email, password);

			if(user != null)
			{
				userResponse.User = user;
				userResponse.ResponseCode = 1;
				userResponse.ResponseMessage = "Usuario Valido.";
			}
			else
			{
				userResponse.User = null;
				userResponse.ResponseCode = 9;
				userResponse.ResponseMessage = "Usuario Invalido.";
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(userResponse))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("CreateUser")]
		public HttpResponseMessage CreateUser([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string adminEmail = dataobject.GetValue("Email").ToString();
			string adminPassword = dataobject.GetValue("Password").ToString();
			JObject user = (JObject)dataobject.GetValue("User").ToString();

			GenericResponse response = new GenericResponse();

			if (!UserDBAccess.ValidateAdminUser(adminEmail, adminPassword))
			{
				response.ResponseCode = 9;
				response.ResponseMessage = "Usuario sin permisos para la creación de Usuarios.";
			}
			else
			{
				string firstName = user.GetValue("FirstName").ToString();
				string lastName = user.GetValue("LastName").ToString();
				string eMail = user.GetValue("EMail").ToString();
				string phone = user.GetValue("Phone").ToString();
				int role = (int)user.GetValue("Role");
				string userName = user.GetValue("UserName").ToString();
				long DNI = (long)user.GetValue("DNI");
				DateTime birthDate = (DateTime)user.GetValue("BirthDate");
				string password = user.GetValue("Password").ToString();
				int measurer1 = (int)user.GetValue("Measurer1ID");
				int measurer2 = (int)user.GetValue("Measurer2ID");

				if (UserDBAccess.CreateUser(adminEmail, adminPassword, userName, eMail, password, firstName, lastName, DNI, birthDate, phone, role, measurer1, measurer2))
				{
					response.ResponseCode = 1;
					response.ResponseMessage = "Usuario creado.";
				}
				else
				{
					response.ResponseCode = 9;
					response.ResponseMessage = "Error al crear usuario.";
				}
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(response))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("ChangeUserDataAdmin")]
		public HttpResponseMessage ChangeUserDataAdmin([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string adminEmail = dataobject.GetValue("Email").ToString();
			string adminPassword = dataobject.GetValue("Password").ToString();
			JObject user = (JObject)dataobject.GetValue("User").ToString();

			GenericResponse response = new GenericResponse();

			if (!UserDBAccess.ValidateAdminUser(adminEmail, adminPassword))
			{
				response.ResponseCode = 9;
				response.ResponseMessage = "Usuario sin permisos para la creación de Usuarios.";
			}
			else
			{
				int id = (int)user.GetValue("ID");
				string firstName = user.GetValue("FirstName").ToString();
				string lastName = user.GetValue("LastName").ToString();
				string eMail = user.GetValue("EMail").ToString();
				string phone = user.GetValue("Phone").ToString();
				int role = (int)user.GetValue("Role");
				string userName = user.GetValue("UserName").ToString();
				long DNI = (long)user.GetValue("DNI");
				DateTime birthDate = (DateTime)user.GetValue("BirthDate");
				string password = user.GetValue("Password").ToString();
				int measurer1 = (int)user.GetValue("Measurer1ID");
				int measurer2 = (int)user.GetValue("Measurer2ID");

				if (UserDBAccess.UpdateUserAdmin(adminEmail, adminPassword, id, userName, eMail, password, firstName, lastName, DNI, birthDate, phone, role, measurer1, measurer2))
				{
					response.ResponseCode = 1;
					response.ResponseMessage = "Usuario creado.";
				}
				else
				{
					response.ResponseCode = 9;
					response.ResponseMessage = "Error al crear usuario.";
				}
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(response))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("ChangeUserData")]
		public HttpResponseMessage ChangeUserData([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();
			JObject user = (JObject)dataobject.GetValue("User").ToString();

			GenericResponse response = new GenericResponse();

			if (UserDBAccess.ValidateUser(email, password) == null)
			{
				response.ResponseCode = 9;
				response.ResponseMessage = "Usuario sin permisos para la creación de Usuarios.";
			}
			else
			{
				string firstName = user.GetValue("FirstName").ToString();
				string lastName = user.GetValue("LastName").ToString();
				string eMail = user.GetValue("EMail").ToString();
				string phone = user.GetValue("Phone").ToString();
				long DNI = (long)user.GetValue("DNI");
				DateTime birthDate = (DateTime)user.GetValue("BirthDate");

				if (UserDBAccess.UpdateUser(eMail, firstName, lastName, DNI, birthDate, phone))
				{
					response.ResponseCode = 1;
					response.ResponseMessage = "Usuario modificado.";
				}
				else
				{
					response.ResponseCode = 9;
					response.ResponseMessage = "Error al modificar usuario.";
				}
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(response))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("ChangePassword")]
		public HttpResponseMessage ChangePassword([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();
			string newPassword = dataobject.GetValue("NewPassword").ToString();

			GenericResponse response = new GenericResponse();

			if (!UserDBAccess.ValidateAdminUser(email, password))
			{
				response.ResponseCode = 9;
				response.ResponseMessage = "Password invalida.";
			}
			else
			{
				if (UserDBAccess.ChangePassword(email, newPassword))
				{
					response.ResponseCode = 1;
					response.ResponseMessage = "Password cambiada.";
				}
				else
				{
					response.ResponseCode = 9;
					response.ResponseMessage = "Error al cambiar password.";
				}
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(response))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("DeleteUser")]
		public HttpResponseMessage DeleteUser([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string adminEmail = dataobject.GetValue("Email").ToString();
			string adminPassword = dataobject.GetValue("Password").ToString();
			int userID = (int)dataobject.GetValue("UserID");

			GenericResponse response = new GenericResponse();

			if (!UserDBAccess.ValidateAdminUserDelete(adminEmail, adminPassword, userID))
			{
				response.ResponseCode = 9;
				response.ResponseMessage = "Usuario sin permisos para la eliminación del Usuario.";
			}
			else
			{
				if (UserDBAccess.DeleteUser(userID))
				{
					response.ResponseCode = 1;
					response.ResponseMessage = "Usuario eliminado.";
				}
				else
				{
					response.ResponseCode = 9;
					response.ResponseMessage = "Error al eliminar usuario.";
				}
			}

				var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(response))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("GetUsers")]
		public HttpResponseMessage ListUsers([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string adminEmail = dataobject.GetValue("Email").ToString();
			string adminPassword = dataobject.GetValue("Password").ToString();

			GetUsersResponse userResponse = new GetUsersResponse();
			if (!UserDBAccess.ValidateAdminUser(adminEmail, adminPassword))
			{
				userResponse.ResponseCode = 9;
				userResponse.ResponseMessage = "Usuario sin permisos para la creación de Usuarios.";
			}
			else
			{
				userResponse.ResponseCode = 1;
				userResponse.ResponseMessage = "Lista de Usuarios.";
				userResponse.Users = UserDBAccess.ListUsers(adminEmail, adminPassword);
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(userResponse))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("GetMeasurers")]
		public HttpResponseMessage ListMeasurers([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string adminEmail = dataobject.GetValue("Email").ToString();
			string adminPassword = dataobject.GetValue("Password").ToString();

			GetMeasurerResponse measurersResponse = new GetMeasurerResponse();
			if (!UserDBAccess.ValidateAdminUser(adminEmail, adminPassword))
			{
				measurersResponse.ResponseCode = 9;
				measurersResponse.ResponseMessage = "Usuario sin permisos para la obtencion de Medidores.";
			}
			else
			{
				measurersResponse.ResponseCode = 1;
				measurersResponse.ResponseMessage = "Lista de Usuarios.";
				measurersResponse.Measurers = UserDBAccess.ListMeasurers(adminEmail, adminPassword);
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(measurersResponse))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}
	}
}
