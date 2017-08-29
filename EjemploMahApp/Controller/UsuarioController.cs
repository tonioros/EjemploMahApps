using System.Windows.Input;
using System;
using System.ComponentModel;
using System.Windows;
using EjemploMahApp.Model;
using EjemploMahApp.View;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace EjemploMahApp.Controller
{
    public class UsuarioController : INotifyPropertyChanged, ICommand
    {
        #region "Propiedades"
        public Usuario Usuario { get; set; }
        public Usuario UsuarioLog { get; set; }
        public ObservableCollection<Usuario> Usuarios { get ; set; }
        #endregion

        #region "Implementacion de Interfaces"
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public void Notificar(String NombrePropiedad)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(NombrePropiedad));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "LogIn":
                    Usuario.contrasenaUsuario = MainWindow.Instancia.PassUS;
                    Console.WriteLine(Usuario.nickUsuario + Usuario.contrasenaUsuario);
                    if (Login())
                    {
                        Usuario = null;
                        Usuario = new  Usuario();
                        PrincipalUsuario pr = new PrincipalUsuario();
                        MainWindow.Instancia.closeMe();
                        pr.Show();
                    }
                    else
                    {
                        Usuario = null;
                        Usuario = new Usuario();
                        MessageBox.Show("Usuario no encontrado o No disponible");
                    }
                    break;
                case "AgregarU":
                    if(!Usuario.nickUsuario.Equals("") && !Usuario.contrasenaUsuario.Equals("") && !Usuario.nombreUsuario.Equals("") && !Usuario.apellidoUsuario.Equals(""))
                    {
                        String SQL = "INSERT INTO usuario(nombre, apellido, nick, contrasena, idRol) VALUES('"+
                            Usuario.nombreUsuario+"','"+Usuario.apellidoUsuario+"','"+Usuario.nickUsuario+"','"+Usuario.contrasenaUsuario+"',1);";
                        ConexionDB.Instancia.EjecutarSQL(SQL);
                        CargarDatos();
                        Notificar("Usuarios");
                    }
                    break;
            }
        }
        #endregion

        #region "Singleton"
        public UsuarioController Instancia{get; }
        #endregion

        #region "Constructor"
        public UsuarioController()
        {
            Instancia = this;
            Usuario = new Usuario();
            Usuarios = new ObservableCollection<Usuario>();
            CargarDatos();
        }
        #endregion

        #region "CRUD de Usuario y Login"
        public bool Login()
        {
            if(!Usuario.nickUsuario.Equals("") && !Usuario.contrasenaUsuario.Equals(""))
            {
                String SQL = "SELECT * FROM Usuario WHERE nick='"+Usuario.nickUsuario+"' AND contrasena='"+Usuario.contrasenaUsuario+"' ;";
                List<object> vuelta =  ConexionDB.Instancia.EjecutarSQL(SQL, "usuario");
                if(vuelta.Count == 0) {
                    return false;
                }
                foreach(object usuario in vuelta)
                {
                    Usuario user = (Usuario)usuario;
                    if(user.nickUsuario.Equals(Usuario.nickUsuario) && user.contrasenaUsuario.Equals(Usuario.contrasenaUsuario))
                    {
                        UsuarioLog = user;
                        break;
                    }
                }
                return true;
            }
            return false;
        }

        public void CargarDatos()
        {
            String SQL = "SELECT * FROM Usuario";
            List<object> lista = ConexionDB.Instancia.EjecutarSQL(SQL, "usuario");
            foreach(object temp in lista)
            {
                Usuario user = (Usuario)temp;
                Usuarios.Add(user);
            }
        }

        #endregion 


    }
}
