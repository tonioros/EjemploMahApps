using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EjemploMahApp.Model
{
    public class ConexionDB
    {
        #region "Atributos"
        private String Connection = "SERVER=localhost;DATABASE=blog_in6av;UID=root;PASSWORD=;";

        public MySqlConnection conexion;
        #endregion
        #region "Singleton"
        private static ConexionDB instacia;
        public static ConexionDB Instancia {
            get
            {
                return (instacia == null) ?(instacia = new ConexionDB()) : instacia;
            } }
        #endregion
        #region "Constructor"
        public ConexionDB()
        {
            conexion = new MySqlConnection(Connection);
            try
            {
                conexion.Open();

            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrio un problema al crear la conexion \n " + ex.Message, "Error Conexión",MessageBoxButton.OK, MessageBoxImage.Error);
                // MessageBox.Show(ex.Message);
            }
        }
        public MySqlConnection GetConnection
        {
            get { return conexion; }
        }
        #endregion
        #region "Metodos de ejecucion"
        public void EjecutarSQL(String sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.ExecuteNonQuery();
            
        }
        public List<object> EjecutarSQL(String sql,String Clase)
        {
            List<object> usuarios = null;
            switch (Clase.ToUpper())
            {
                case "USUARIO":
                    usuarios = new List<object>();
                    break;

            }

            MySqlCommand commando = new MySqlCommand(sql, conexion);
            MySqlDataReader lectura= commando.ExecuteReader();
            while (lectura.Read())
            {
            switch (Clase.ToUpper())
            {
                case "USUARIO":
                        usuarios.Add(new Usuario(lectura.GetInt32("idUsuario"), lectura.GetString("nombre"), lectura.GetString("apellido"), lectura.GetString("nick"), lectura.GetString("contrasena")));
                    break;
            }   

            }
            lectura.Close();
            return usuarios;
        }
        #endregion

    }
}
