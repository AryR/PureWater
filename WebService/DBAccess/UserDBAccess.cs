using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebService.Models;

namespace WebService.DBAccess
{
	public static class UserDBAccess
	{
		private static readonly string ConnectionString =
			ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		public static bool ValidateAdminUser(string email, string password)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				SqlCommand command = connection.CreateCommand();
				command.CommandText = "SELECT * FROM Usuario WHERE Email = @email and Pass = @password and ROL_ID = 1;";
				command.Parameters.Add(new SqlParameter("@email", email));
				command.Parameters.Add(new SqlParameter("@password", password));
				connection.Open();

				SqlDataReader dr = command.ExecuteReader();
				if (!dr.HasRows)
					return false;
				else
					return true;
			}
		}

		public static bool ValidateAdminUserDelete(string email, string password, int idUser)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				SqlCommand command = connection.CreateCommand();
				command.CommandText = "SELECT * FROM Usuario U inner join Usuario U2 ON U.Producto_ID = U2.Producto_ID WHERE U.Email = @email and U.Pass = @password and U.ROL_ID = 1 and U2.Usuario_ID = @userID;";
				command.Parameters.Add(new SqlParameter("@email", email));
				command.Parameters.Add(new SqlParameter("@password", password));
				command.Parameters.Add(new SqlParameter("@userID", idUser));
				connection.Open();

				SqlDataReader dr = command.ExecuteReader();
				if (!dr.HasRows)
					return false;
				else
					return true;
			}
		}

		public static User ValidateUser(string email, string password)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				SqlCommand command = connection.CreateCommand();
				command.CommandText = "SELECT * FROM Usuario WHERE Email = @email and Pass = @password ;";
				command.Parameters.Add(new SqlParameter("@email", email));
				command.Parameters.Add(new SqlParameter("@password", password));
				connection.Open();

				SqlDataReader dr = command.ExecuteReader();
				if (!dr.HasRows)
					return null;

				dr.Read();
				User user = new User();
				user.ID = dr.GetInt32(dr.GetOrdinal("Usuario_ID"));
				user.EMail = dr.GetString(dr.GetOrdinal("Email"));
				user.FirstName = dr.GetString(dr.GetOrdinal("Nombre"));
				user.LastName = dr.GetString(dr.GetOrdinal("Apellido"));
				user.UserName = dr.GetString(dr.GetOrdinal("Usuario"));
				user.DNI = dr.GetInt64(dr.GetOrdinal("DNI"));
				user.BirthDate = dr.GetDateTime(dr.GetOrdinal("FechaNacimiento"));
				user.Phone = dr.GetString(dr.GetOrdinal("Telefono"));
				user.IsRecolectionServiceEnabled = dr.GetBoolean(dr.GetOrdinal("ServicioRecoleccion"));
				user.Role = dr.GetInt32(dr.GetOrdinal("Rol_ID"));
				dr.Close();

				return user;
			}
		}

		public static bool CreateUser(string adminEmail, string adminPassword, string username, string email, string password, string firstname, string lastname, long dni, DateTime birthdate, string phone, int role, int idMeasurer1, int idMeasurer2)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				SqlTransaction transaction = connection.BeginTransaction("Transaction");

				SqlCommand command = connection.CreateCommand();
				command.Transaction = transaction;

				command.CommandText = "SELECT Producto_ID FROM Usuario WHERE Email = @adminEmail and Pass = @adminPassword ;";
				command.Parameters.Add(new SqlParameter("@adminEmail", adminEmail));
				command.Parameters.Add(new SqlParameter("@adminPassword", adminPassword));

				int productID = (int)command.ExecuteScalar();

				SqlCommand commandInsert = connection.CreateCommand();
				commandInsert.CommandText = "INSERT INTO Usuario (Usuario, Email, Pass, Nombre, Apellido, DNI, FechaNacimiento, Telefono, Rol_ID, Producto_ID) " +
					"output INSERTED.Usuario_ID " +
					"VALUES (@username, @email, @password, @firstname, @lastname, @dni, @birthdate, @phone, @rolid, @productid)";
				commandInsert.Parameters.Add(new SqlParameter("@username", username));
				commandInsert.Parameters.Add(new SqlParameter("@email", email));
				commandInsert.Parameters.Add(new SqlParameter("@password", password));
				commandInsert.Parameters.Add(new SqlParameter("@firstname", firstname));
				commandInsert.Parameters.Add(new SqlParameter("@lastname", lastname));
				commandInsert.Parameters.Add(new SqlParameter("@dni", dni));
				commandInsert.Parameters.Add(new SqlParameter("@birthdate", birthdate));
				commandInsert.Parameters.Add(new SqlParameter("@phone", phone));
				commandInsert.Parameters.Add(new SqlParameter("@rolid", role));
				commandInsert.Parameters.Add(new SqlParameter("@productid", productID));

				int id = (int)commandInsert.ExecuteScalar();
				if (id <= 0)
				{
					transaction.Rollback();
					connection.Close();
					return false;
				}

				SqlCommand commandMeasurer1 = connection.CreateCommand();
				commandMeasurer1.Transaction = transaction;

				commandMeasurer1.CommandText = "Update Medidor SET Usuario_ID = @id WHERE Medidor_ID = @idMeasurer1;";
				commandMeasurer1.Parameters.Add(new SqlParameter("@id", id));
				commandMeasurer1.Parameters.Add(new SqlParameter("@idMeasurer1", idMeasurer1));
				int result = commandMeasurer1.ExecuteNonQuery();
				if (result == 0)
				{
					transaction.Rollback();
					connection.Close();
					return false;
				}

				SqlCommand commandMeasurer2 = connection.CreateCommand();
				commandMeasurer2.Transaction = transaction;

				commandMeasurer2.CommandText = "Update Medidor SET Usuario_ID = @id WHERE Medidor_ID = @idMeasurer2;";
				commandMeasurer2.Parameters.Add(new SqlParameter("@id", id));
				commandMeasurer2.Parameters.Add(new SqlParameter("@idMeasurer2", idMeasurer2));
				result = commandMeasurer2.ExecuteNonQuery();
				if (result == 0)
				{
					transaction.Rollback();
					connection.Close();
					return false;
				}

				transaction.Commit();
				connection.Close();
				return true;
			}
		}

		public static bool UpdateUserAdmin(string adminEmail, string adminPassword, int id, string username, string email, string password, string firstname, string lastname, long dni, DateTime birthdate, string phone, int role, int idMeasurer1, int idMeasurer2)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				SqlTransaction transaction = connection.BeginTransaction("Transaction");
				
				SqlCommand command = connection.CreateCommand();
				command.Transaction = transaction;

				command.CommandText = "SELECT Producto_ID FROM Usuario WHERE Email = @adminEmail and Pass = @adminPassword ;";
				command.Parameters.Add(new SqlParameter("@adminEmail", adminEmail));
				command.Parameters.Add(new SqlParameter("@adminPassword", adminPassword));
				connection.BeginTransaction();

				int productID = (int)command.ExecuteScalar();

				SqlCommand commandInsert = connection.CreateCommand();
				commandInsert.Transaction = transaction;

				commandInsert.CommandText = "Update Usuario SET Usuario = @username, Email=@email, Pass=@password, Nombre=@firstname, Apellido=@lastname, DNI=@dni, FechaNacimiento=@birthdate, Telefono= @phone, Rol_ID=@rolid, Producto_ID=@productid " +
					"WHERE Usuario_ID = @id;";
				commandInsert.Parameters.Add(new SqlParameter("@username", username));
				commandInsert.Parameters.Add(new SqlParameter("@email", email));
				commandInsert.Parameters.Add(new SqlParameter("@password", password));
				commandInsert.Parameters.Add(new SqlParameter("@firstname", firstname));
				commandInsert.Parameters.Add(new SqlParameter("@lastname", lastname));
				commandInsert.Parameters.Add(new SqlParameter("@dni", dni));
				commandInsert.Parameters.Add(new SqlParameter("@birthdate", birthdate));
				commandInsert.Parameters.Add(new SqlParameter("@phone", phone));
				commandInsert.Parameters.Add(new SqlParameter("@rolid", role));
				commandInsert.Parameters.Add(new SqlParameter("@productid", productID));
				commandInsert.Parameters.Add(new SqlParameter("@id", id));
				int result = commandInsert.ExecuteNonQuery();
				if(result == 0)
				{
					transaction.Rollback();
					connection.Close();
					return false;
				}

				SqlCommand commandMeasurer1 = connection.CreateCommand();
				commandMeasurer1.Transaction = transaction;

				commandMeasurer1.CommandText = "Update Medidor SET Usuario_ID = @id WHERE Medidor_ID = @idMeasurer1;";
				commandMeasurer1.Parameters.Add(new SqlParameter("@id", id));
				commandMeasurer1.Parameters.Add(new SqlParameter("@idMeasurer1", idMeasurer1));
				result = commandMeasurer1.ExecuteNonQuery();
				if (result == 0)
				{
					transaction.Rollback();
					connection.Close();
					return false;
				}

				SqlCommand commandMeasurer2 = connection.CreateCommand();
				commandMeasurer2.Transaction = transaction;

				commandMeasurer2.CommandText = "Update Medidor SET Usuario_ID = @id WHERE Medidor_ID = @idMeasurer2;";
				commandMeasurer2.Parameters.Add(new SqlParameter("@id", id));
				commandMeasurer2.Parameters.Add(new SqlParameter("@idMeasurer2", idMeasurer2));
				result = commandMeasurer2.ExecuteNonQuery();
				if (result == 0)
				{
					transaction.Rollback();
					connection.Close();
					return false;
				}

				transaction.Commit();
				connection.Close();
				return true;
			}
		}

		public static List<User> ListUsers(string adminEmail, string adminPassword)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				SqlCommand command = connection.CreateCommand();
				command.CommandText = "SELECT Producto_ID FROM Usuario WHERE Email = @adminEmail and Pass = @adminPassword ;";
				command.Parameters.Add(new SqlParameter("@adminEmail", adminEmail));
				command.Parameters.Add(new SqlParameter("@adminPassword", adminPassword));

				int productID = (int)command.ExecuteScalar();

				SqlCommand command2 = connection.CreateCommand();
				command2.CommandText = "SELECT * FROM Usuario WHERE Producto_ID = @productID ;";
				command2.Parameters.Add(new SqlParameter("@productID", productID));

				List<User> users = new List<User>();

				SqlDataReader dr = command2.ExecuteReader();
				while (dr.Read())
				{
					User user = new User();
					user.ID = dr.GetInt32(dr.GetOrdinal("Usuario_ID"));
					user.EMail = dr.GetString(dr.GetOrdinal("Email"));
					user.FirstName = dr.GetString(dr.GetOrdinal("Nombre"));
					user.LastName = dr.GetString(dr.GetOrdinal("Apellido"));
					user.UserName = dr.GetString(dr.GetOrdinal("Usuario"));
					user.DNI = dr.GetInt64(dr.GetOrdinal("DNI"));
					user.BirthDate = dr.GetDateTime(dr.GetOrdinal("FechaNacimiento"));
					user.Phone = dr.GetString(dr.GetOrdinal("Telefono"));
					user.IsRecolectionServiceEnabled = dr.GetBoolean(dr.GetOrdinal("ServicioRecoleccion"));
					user.Role = dr.GetInt32(dr.GetOrdinal("Rol_ID"));

					SqlCommand command3 = connection.CreateCommand();
					command3.CommandText = "SELECT * FROM Medidor WHERE Usuario_ID = @usuarioID ;";
					command3.Parameters.Add(new SqlParameter("@usuarioID", user.ID));
					SqlDataReader dr2 = command3.ExecuteReader();
					int i = 0;
					while (dr2.Read())
					{
						if(i==0)
							user.Measurer1ID = dr.GetInt32(dr.GetOrdinal("Medidor_ID"));
						else if(i == 1)
							user.Measurer2ID = dr.GetInt32(dr.GetOrdinal("Medidor_ID"));
						i++;
					}
					dr2.Close();
					users.Add(user);
				}
				dr.Close();
				connection.Close();

				return users;
			}
		}

		public static bool ChangePassword(string email, string newPassword)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				SqlCommand commandInsert = connection.CreateCommand();

				commandInsert.CommandText = "Update Usuario SET Pass=@password " +
					"WHERE Email=@email;";
				commandInsert.Parameters.Add(new SqlParameter("@email", email));
				commandInsert.Parameters.Add(new SqlParameter("@password", newPassword));

				int result = commandInsert.ExecuteNonQuery();
				if (result == 0)
				{
					connection.Close();
					return false;
				}

				connection.Close();
				return true;
			}
		}

		public static bool UpdateUser(string email, string firstname, string lastname, long dni, DateTime birthdate, string phone)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				SqlCommand commandInsert = connection.CreateCommand();

				commandInsert.CommandText = "Update Usuario SET Nombre=@firstname, Apellido=@lastname, DNI=@dni, FechaNacimiento=@birthdate, Telefono= @phone " +
					"WHERE Email=@email;";
				commandInsert.Parameters.Add(new SqlParameter("@email", email));
				commandInsert.Parameters.Add(new SqlParameter("@firstname", firstname));
				commandInsert.Parameters.Add(new SqlParameter("@lastname", lastname));
				commandInsert.Parameters.Add(new SqlParameter("@dni", dni));
				commandInsert.Parameters.Add(new SqlParameter("@birthdate", birthdate));
				commandInsert.Parameters.Add(new SqlParameter("@phone", phone));
				int result = commandInsert.ExecuteNonQuery();
				if (result == 0)
				{
					connection.Close();
					return false;
				}

				connection.Close();
				return true;
			}
		}

		public static bool DeleteUser(int userId)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				SqlTransaction transaction = connection.BeginTransaction("Transaction");

				SqlCommand command = connection.CreateCommand();
				command.Transaction = transaction;

				command.CommandText = "Delete From Usuario WHERE Usuario_ID=@userId;";
				command.Parameters.Add(new SqlParameter("@userId", userId));

				int result = command.ExecuteNonQuery();
				if (result == 0)
				{
					transaction.Rollback();
					connection.Close();
					return false;
				}

				SqlCommand commandMeasurer1 = connection.CreateCommand();
				commandMeasurer1.Transaction = transaction;

				commandMeasurer1.CommandText = "Update Medidor SET Usuario_ID = null WHERE Usuario_ID=@userId;";
				commandMeasurer1.Parameters.Add(new SqlParameter("@userId", userId));
				result = commandMeasurer1.ExecuteNonQuery();
				if (result == 0)
				{
					transaction.Rollback();
					connection.Close();
					return false;
				}

				transaction.Commit();
				connection.Close();
				return true;
			}
		}

		public static List<Measurer> ListMeasurers(string adminEmail, string adminPassword)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				SqlCommand command = connection.CreateCommand();
				command.CommandText = "SELECT Producto_ID FROM Usuario WHERE Email = @adminEmail and Pass = @adminPassword ;";
				command.Parameters.Add(new SqlParameter("@adminEmail", adminEmail));
				command.Parameters.Add(new SqlParameter("@adminPassword", adminPassword));

				int productID = (int)command.ExecuteScalar();

				SqlCommand command2 = connection.CreateCommand();
				command2.CommandText = "SELECT * FROM Medidor WHERE Producto_ID = @productID ;";
				command2.Parameters.Add(new SqlParameter("@productID", productID));

				List<Measurer> measurers = new List<Measurer>();

				SqlDataReader dr = command2.ExecuteReader();
				while (dr.Read())
				{
					Measurer measurer = new Measurer();
					measurer.ID = dr.GetInt32(dr.GetOrdinal("Medidor_ID"));
					measurer.Description = dr.GetString(dr.GetOrdinal("Descripcion"));
					measurer.Pin = dr.GetString(dr.GetOrdinal("Pin"));

					measurers.Add(measurer);
				}
				dr.Close();
				connection.Close();

				return measurers;
			}
		}

	}
}