using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploMahApp.Model
{
    public class Usuario
    {
        public Usuario() {
            UsuarioID = 0;
            nombreUsuario = "";
            apellidoUsuario = "";
            nickUsuario = "";
            contrasenaUsuario = "";
        }
        public Usuario(int ID, String nombre, String apellido, String nick, String contrasena)
        {
            UsuarioID = ID;
            nombreUsuario = nombre;
            apellidoUsuario = apellido;
            nickUsuario = nick;
            contrasenaUsuario = contrasena;
        }
        public int UsuarioID { get; set; }
        public String nombreUsuario { get; set; }
        public String apellidoUsuario { get; set; }
        public String nickUsuario { get; set; }
        public String contrasenaUsuario { get; set; }
    }
}
