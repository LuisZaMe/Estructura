using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Core.Policies
{
    public static class Policies
    {
        //Acceso exclusivo super administrador
        public const string SuperAdministrador = "SuperAdministrador";

        //Acceso exclusivo administrador
        public const string Administrador = "Administrador";

        //Accesos particulares
        public const string InternoEntrevistador = "InternoEntrevistador";
        public const string InternoAnalista = "InternoAnalista";
        public const string Clientes = "Clientes";

        // Acceso a admin, alalista y entrevistador
        public const string InternoPlataforma = "InternoPlataforma";

        // Acceso a todo usuario de la plataforma
        public const string InternoGlobal = "InternoGlobal";
    }
}
